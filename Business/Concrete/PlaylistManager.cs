using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Concrete;
using Entities.Dtos;
using Entities.Dtos.AlbumDtos;
using Entities.Dtos.FollowerDtos;
using Entities.Dtos.PlaylistDtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class PlaylistManager : IPlaylistService
    {
        private readonly IPlaylistDal _playlistDal;
        private readonly ISongDal _songDal;
        private readonly IUserDal _userDal;
        private readonly IFollowerDal _followerDal;
        private readonly ILibraryDal _libraryDal;
        private readonly ILibraryPlaylistDal _libraryPlaylistDal;
        private readonly ITokenHelper _tokenHelper;

        public PlaylistManager(IPlaylistDal playlistDal, ISongDal songDal, IUserDal userDal, IFollowerDal followerDal, ILibraryDal libraryDal, ITokenHelper tokenHelper, ILibraryPlaylistDal libraryPlaylistDal)
        {
            _playlistDal = playlistDal;
            _songDal = songDal;
            _userDal = userDal;
            _followerDal = followerDal;
            _libraryDal = libraryDal;
            _tokenHelper = tokenHelper;
            _libraryPlaylistDal = libraryPlaylistDal;
        }

        public IDataResult<bool> Add(PlaylistAddDto playlist, string token)
        {
            var userToken = _tokenHelper.GetAuthenticatedUser(token);
            var users = _userDal.Get(x => x.Id == userToken);
            if (users == null)
            {
                return new ErrorDataResult<bool>(Messages.UserNotFound);
            }
            var addedPlaylist = new Playlist
            {
                Name = playlist.PlaylistName,
                UserId = userToken,
                Status = playlist.Status,
                CreatedDate = playlist.CreatedDate,
                ModifiedDate = playlist.ModifiedDate,
            };
            _playlistDal.Add(addedPlaylist);

            var playlistResult = new PlaylistAddDto
            {
                PlaylistName = addedPlaylist.Name,
            };

            var playlistAddLibrary = new LibraryPlaylist
            {
                PlaylistId = addedPlaylist.Id,
                UserId = addedPlaylist.UserId,
                Status= addedPlaylist.Status,
            };
            _libraryPlaylistDal.Add(playlistAddLibrary);

            return new SuccessDataResult<bool>(true, Messages.PlaylistAdded);
        }


        public IDataResult<List<PlaylistListDto>> GetList()
        {
            var list = _playlistDal.GetList();
            var playlistList = new List<PlaylistListDto>();
            foreach (var playlist in list)
            {
                playlistList.Add(new()
                {
                    Id = playlist.Id,
                    UserId = playlist.UserId,
                    Name = playlist.Name,
                    Status = playlist.Status,
                    CreatedDate = playlist.CreatedDate,
                    ModifiedDate = playlist.ModifiedDate,
                });
            }
            return new SuccessDataResult<List<PlaylistListDto>>(playlistList, Messages.PlaylistList);
        }

        public IDataResult<List<PlaylistListDto>> UsersGetList(int userId)
        {
            var user = _userDal.Get(u => u.Id == userId);
            if (user == null)
            {
                return new ErrorDataResult<List<PlaylistListDto>>(Messages.UserNotFound);
            }

            var list = _playlistDal.GetList(p => p.UserId == userId);
            var playlistList = new List<PlaylistListDto>();
            foreach (var playlist in list)
            {
                playlistList.Add(new()
                {
                    Id = playlist.Id,
                    UserId = playlist.UserId,
                    Name = playlist.Name,
                    Status = playlist.Status,
                    CreatedDate = playlist.CreatedDate,
                    ModifiedDate = playlist.ModifiedDate,
                });
            }

            return new SuccessDataResult<List<PlaylistListDto>>(playlistList, Messages.PlaylistList);
        }


        public IDataResult<bool> Update(PlaylistUpdateDto playlist)
        {
            var updatedPlaylist = new Playlist
            {
                Status = playlist.Status,
                CreatedDate = playlist.CreatedDate,
                ModifiedDate = playlist.ModifiedDate,
            };
            _playlistDal.Add(updatedPlaylist);
            return new SuccessDataResult<bool>(true, Messages.PlaylistUpdated);
        }

        public IDataResult<bool> UpdateStatus(int playlistId)
        {
            var playlist = _playlistDal.Get(x => x.Id == playlistId);
            if (playlist != null)
            {
                playlist.Status = !playlist.Status;
                _playlistDal.Update(playlist);
                return new SuccessDataResult<bool>(true, Messages.PlaylistStatus);
            }
            else
            {

                return new ErrorDataResult<bool>(false, Messages.PlaylistStatusNotFound);
            }
        }

        public IDataResult<List<PlaylistListDto>> UserGetPlaylist(string token)
        {
            var userToken = _tokenHelper.GetAuthenticatedUser(token);
            var users = _userDal.Get(x => x.Id == userToken);
            if (users == null)
            {
                return new ErrorDataResult<List<PlaylistListDto>>(Messages.UserNotFound);
            }

            var list = _playlistDal.GetList(x => x.UserId == userToken);
            
            if (list == null || list.Count == 0)
            {
                return new ErrorDataResult<List<PlaylistListDto>>(Messages.UserPlaylistNotFound);
            }
            var playlistList = new List<PlaylistListDto>();

            foreach (var playlist in list)
            {
                playlistList.Add(new PlaylistListDto
                {
                    Id = playlist.Id,
                    UserId = userToken,
                    Name = playlist.Name,
                    Status = playlist.Status,
                    CreatedDate = playlist.CreatedDate,
                    ModifiedDate = playlist.ModifiedDate,
                });
            }

            return new SuccessDataResult<List<PlaylistListDto>>(playlistList, Messages.PlaylistListed);
        }

    }
}
