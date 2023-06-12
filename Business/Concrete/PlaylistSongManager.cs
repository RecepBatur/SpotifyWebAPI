using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Concrete;
using Entities.Dtos.FavoriteDtos;
using Entities.Dtos.PlaylistSongDtos;
using IdentityServer3.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class PlaylistSongManager : IPlaylistSongService
    {
        private readonly IPlaylistSongDal _playlistSongDal;
        private readonly ISongDal _songdal;
        private readonly IUserDal _userDal;
        private readonly ITokenHelper _tokenHelper;
        private readonly IPlaylistDal _playlistDal;

        public PlaylistSongManager(IPlaylistSongDal playlistSongDal, ISongDal songdal, IUserDal userDal, ITokenHelper tokenHelper, IPlaylistDal playlistDal)
        {
            _playlistSongDal = playlistSongDal;
            _songdal = songdal;
            _userDal = userDal;
            _tokenHelper = tokenHelper;
            _playlistDal = playlistDal;
        }

        public IDataResult<bool> AddSongPlaylist(PlaylistSongAddDto playlistSongAddDto, string token)
        {
            var userToken = _tokenHelper.GetAuthenticatedUser(token);
            var users = _userDal.Get(x => x.Id == userToken);
            if (users == null)
            {
                return new ErrorDataResult<bool>(Messages.UserNotFound);
            }
            var songIds = playlistSongAddDto.SongId;
            if (songIds == null || songIds.Count == 0)
            {
                return new ErrorDataResult<bool>(Messages.FavoriteError);
            }
            var playlists = _playlistDal.Get(x => x.Id == playlistSongAddDto.PlaylistId);
            if (playlists == null)
            {
                return new ErrorDataResult<bool>(Messages.PlaylistNotFound);
            }

            foreach (var songId in songIds)
            {
                var song = _songdal.Get(x => x.Id == songId);
                if (song == null || song.Status == false)
                {
                    return new ErrorDataResult<bool>(Messages.FavoriteError);
                }
                else
                {
                    var playlistSongAdd = new PlaylistSong
                    {
                        PlaylistName = playlists.Name,
                        PlaylistId = playlists.Id,
                        SongId = song.Id,
                        UserId = userToken,
                        Status = song.Status,
                        CreatedDate = song.CreatedDate,
                        ModifiedDate = song.ModifiedDate,
                    };
                    _playlistSongDal.Add(playlistSongAdd);

                    var favoriteAddResult = new PlaylistSongAddDto
                    {
                        //Id = playlistSongAdd.Id,
                        //PlaylistName = playlistSongAdd.PlaylistName,
                        SongId = new List<int> { playlistSongAdd.SongId },
                        PlaylistId = playlists.Id,
                        //UserId = userToken,

                    };
                }
            }

            return new SuccessDataResult<bool>(true, Messages.PlaylistSongAdd);

        }
    }
}
