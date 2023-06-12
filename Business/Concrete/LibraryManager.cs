using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Concrete;
using Entities.Dtos.AlbumDtos;
using Entities.Dtos.FavoriteDtos;
using Entities.Dtos.LibraryDtos;
using Entities.Dtos.LibraryPlaylistDtos;
using Entities.Dtos.PlaylistDtos;
using Entities.Dtos.SongDtos;
using Entities.Dtos.UserDtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class LibraryManager : ILibraryService
    {
        private readonly ILibraryDal _libraryDal;
        private readonly IUserDal _userDal;
        private readonly IPlaylistDal _playlistDal;
        private readonly ILibraryPlaylistDal _libraryplaylistDal;
        private readonly ITokenHelper _tokenHelper;
        public LibraryManager(ILibraryDal libraryDal, IUserDal userDal, IPlaylistDal playlistDal, ITokenHelper tokenHelper, ILibraryPlaylistDal libraryplaylistDal)
        {
            _libraryDal = libraryDal;
            _userDal = userDal;
            _playlistDal = playlistDal;
            _tokenHelper = tokenHelper;
            _libraryplaylistDal = libraryplaylistDal;
        }

        public IDataResult<bool> Add(LibraryAddDto library)
        {
            var addedLibrary = new Library
            {
                Status = true,
                CreatedDate = library.CreatedDate,
                ModifiedDate = library.ModifiedDate,
                UserId = library.UserId,
            };
            _libraryDal.Add(addedLibrary);
            return new SuccessDataResult<bool>(true, Messages.LibraryAdded);
        }

        public IDataResult<List<LibraryListDto>> GetList(string token)
        {
            var userToken = _tokenHelper.GetAuthenticatedUser(token);
            var users = _userDal.Get(x => x.Id == userToken);
            if (users == null)
            {
                return new ErrorDataResult<List<LibraryListDto>>(Messages.UserNotFound);
            }
            var list = _libraryDal.GetList(x=>x.UserId == userToken);
            if (list == null || list.Count == 0)
            {
                return new ErrorDataResult<List<LibraryListDto>>(Messages.UserLibraryNotFound);
            }
            var libraryList = new List<LibraryListDto>();
            foreach (var library in list)
            {
                libraryList.Add(new()
                {
                    Id = library.Id,
                    UserId = library.UserId,
                    Status = library.Status,
                    CreatedDate = library.CreatedDate,
                    ModifiedDate = library.ModifiedDate,
                });
            }
            return new SuccessDataResult<List<LibraryListDto>>(libraryList, Messages.LibraryList);
        }

        public IDataResult<bool> Update(LibraryUpdateDto library)
        {
            var updatedLibrary = new Library
            {
                Id = library.Id,
                Status = library.Status,
                CreatedDate = library.CreatedDate,
                ModifiedDate = library.ModifiedDate,
            };
            _libraryDal.Update(updatedLibrary);
            return new SuccessDataResult<bool>(true, Messages.LibraryUpdated);
        }

        public IDataResult<bool> UpdateStatus(int libraryId)
        {
            var library = _libraryDal.Get(x => x.Id == libraryId);
            if (library != null)
            {
                library.Status = !library.Status;
                _libraryDal.Update(library);
                return new SuccessDataResult<bool>(true, Messages.LibraryStatus);
            }
            else
            {

                return new ErrorDataResult<bool>(false, Messages.LibraryStatusNotFound);
            }
        }
    }
}
