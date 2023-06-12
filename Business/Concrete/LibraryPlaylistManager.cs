using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Concrete;
using Entities.Dtos.LibraryPlaylistDtos;
using Entities.Dtos.PlaylistDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    
    public class LibraryPlaylistManager : ILibraryPlaylistService
    {
        private readonly IUserDal _userDal;
        private readonly ITokenHelper _tokenHelper;
        private readonly ILibraryPlaylistDal _libraryPlaylistDal;

        public LibraryPlaylistManager(IUserDal userDal, ITokenHelper tokenHelper, ILibraryPlaylistDal libraryPlaylistDal)
        {
            _userDal = userDal;
            _tokenHelper = tokenHelper;
            _libraryPlaylistDal = libraryPlaylistDal;
        }

        public IDataResult<bool> Add(LibraryPlaylistAddDto playlist, string token)
        {
            var userToken = _tokenHelper.GetAuthenticatedUser(token);
            var users = _userDal.Get(x => x.Id == userToken);
            if (users == null)
            {
                return new ErrorDataResult<bool>(Messages.UserNotFound);
            }
            var addedPlaylist = new LibraryPlaylist
            {
                PlaylistId = playlist.PlaylistId,
                UserId = userToken,
                Status = playlist.Status,
                CreatedDate = playlist.CreatedDate,
                ModifiedDate = playlist.ModifiedDate,
            };
            _libraryPlaylistDal.Add(addedPlaylist);

            var playlistResult = new LibraryPlaylistAddDto
            {
                PlaylistId = addedPlaylist.Id,
            };

            return new SuccessDataResult<bool>(true, Messages.PlaylistAdded);
        }

        public IDataResult<List<LibraryPlaylistDto>> UserLibraryPlaylist(string token)
        {
            var userToken = _tokenHelper.GetAuthenticatedUser(token);
            var users = _userDal.Get(x => x.Id == userToken);
            if (users == null)
            {
                return new ErrorDataResult<List<LibraryPlaylistDto>>(Messages.UserNotFound);
            }
            var list = _libraryPlaylistDal.GetList(x => x.UserId == userToken);

            if (list == null || list.Count == 0)
            {
                return new ErrorDataResult<List<LibraryPlaylistDto>>(Messages.UserPlaylistNotFound);
            }

            var libraryPlaylist = new List<LibraryPlaylistDto>();

            foreach (var item in list)
            {
                libraryPlaylist.Add(new LibraryPlaylistDto
                {
                    Id = item.Id,
                    UserId = userToken,
                    PlaylistId = item.PlaylistId,
                    Status = item.Status,
                    CreatedDate = item.CreatedDate,
                    ModifiedDate = item.ModifiedDate,
                });
                
            }

            return new SuccessDataResult<List<LibraryPlaylistDto>>(libraryPlaylist, Messages.PlaylistListed);


        }
    }
}
