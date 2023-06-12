using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Concrete;
using Entities.Dtos.PlaylistFollowingDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class PlaylistFollowingManager : IPlaylistFollowingService
    {
        private readonly IPlaylistFollowingDal _playlistFollowingDal;
        private readonly IPlaylistDal _playlistDal;
        private readonly IUserDal _userDal;
        private readonly ITokenHelper _tokenHelper;

        public PlaylistFollowingManager(IPlaylistFollowingDal playlistFollowingDal, IUserDal userDal, ITokenHelper tokenHelper, IPlaylistDal playlistDal)
        {
            _playlistFollowingDal = playlistFollowingDal;
            _userDal = userDal;
            _tokenHelper = tokenHelper;
            _playlistDal = playlistDal;
        }

        public IDataResult<bool> AddFollowPlaylist(PlaylistFollowAddDto playlistFollowAddDto, string token)
        {
            var userToken = _tokenHelper.GetAuthenticatedUser(token);
            var users = _userDal.Get(x => x.Id == userToken);
            if (users == null)
            {
                return new ErrorDataResult<bool>(Messages.UserNotFound);
            }
            var playlists = _playlistDal.Get(x => x.Id == playlistFollowAddDto.PlaylistId);
            if (playlists == null)
            {
                return new ErrorDataResult<bool>(Messages.PlaylistNotFound);
            }
            else
            {
                var playlistFollow = new PlaylistFollowing
                {
                    PlaylistId = playlists.Id,
                    PlaylistName = playlists.Name,
                    FollowId = userToken,
                    Status = playlists.Status,
                    CreatedDate = playlists.CreatedDate,
                    ModifiedDate = playlists.ModifiedDate,

                };
                _playlistFollowingDal.Add(playlistFollow);

                var playlistFollowResult = new PlaylistFollowAddDto
                {
                    PlaylistId = playlistFollow.Id,
                    //FollowerId = userToken,
                };
            }

            return new SuccessDataResult<bool>(true, Messages.PlaylistFollowed);
        }
    }
}
