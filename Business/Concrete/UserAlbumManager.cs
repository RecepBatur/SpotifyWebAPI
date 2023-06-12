using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Concrete;
using Entities.Dtos.LibraryPlaylistDtos;
using Entities.Dtos.UserAlbumDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserAlbumManager : IUserAlbumService
    {
        private readonly IUserAlbumDal _userAlbumDal;
        private readonly IUserDal _userDal;
        private readonly ITokenHelper _tokenHelper;

        public UserAlbumManager(IUserAlbumDal userAlbumDal, IUserDal userDal, ITokenHelper tokenHelper)
        {
            _userAlbumDal = userAlbumDal;
            _userDal = userDal;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<bool> Add(UserAlbumAddDto album, string token)
        {
            var userToken = _tokenHelper.GetAuthenticatedUser(token);
            var users = _userDal.Get(x => x.Id == userToken);
            if (users == null)
            {
                return new ErrorDataResult<bool>(Messages.UserNotFound);
            }
            var addedPlaylist = new UserAlbum
            {
                AlbumId = album.AlbumId,
                UserId = userToken,
                Status = album.Status,
                CreatedDate = album.CreatedDate,
                ModifiedDate = album.ModifiedDate,
            };
            _userAlbumDal.Add(addedPlaylist);

            var playlistResult = new UserAlbumAddDto
            {
                AlbumId = addedPlaylist.AlbumId,
            };

            return new SuccessDataResult<bool>(true, Messages.AlbumAdded);
        }

        public IDataResult<List<UserAlbumListDto>> UserAlbumList(string token)
        {
            var userToken = _tokenHelper.GetAuthenticatedUser(token);
            var users = _userDal.Get(x => x.Id == userToken);
            if (users == null)
            {
                return new ErrorDataResult<List<UserAlbumListDto>>(Messages.UserNotFound);
            }
            var list = _userAlbumDal.GetList(x => x.UserId == userToken);

            if (list == null || list.Count == 0)
            {
                return new ErrorDataResult<List<UserAlbumListDto>>(Messages.UserPlaylistNotFound);
            }

            var userAlbumList = new List<UserAlbumListDto>();

            foreach (var item in list)
            {
                userAlbumList.Add(new UserAlbumListDto
                {
                    Id = item.Id,
                    AlbumId = item.AlbumId,
                    UserId = userToken,
                    Status = item.Status,
                    CreatedDate = item.CreatedDate,
                    ModifiedDate = item.ModifiedDate,
                });

            }

            return new SuccessDataResult<List<UserAlbumListDto>>(userAlbumList, Messages.UserAlbumList);
        }
    }
}
