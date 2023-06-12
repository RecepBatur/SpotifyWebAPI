using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Concrete;
using Entities.Dtos.AlbumDtos;
using Google.Protobuf.WellKnownTypes;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AlbumManager : IAlbumService
    {
        private readonly IAlbumDal _albumDal;
        private readonly IUserDal _userDal;
        private readonly ITokenHelper _tokenHelper;

        public AlbumManager(IAlbumDal albumDal, IUserDal userDal, ITokenHelper tokenHelper)
        {
            _albumDal = albumDal;
            _userDal = userDal;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<bool> Add(AlbumAddDto album, string token)
        {
            var userToken = _tokenHelper.GetAuthenticatedUser(token);
            var users = _userDal.Get(x => x.Id == userToken);
            if (users == null)
            {
                return new ErrorDataResult<bool>(Messages.UserNotFound);
            }
            var addedAlbum = new Album
            {
                UserId = userToken,
                Name = album.Name,
                Status = true,
                
            };
            _albumDal.Add(addedAlbum);

            var albumResult = new AlbumAddDto
            {
                Name = addedAlbum.Name,
            };


            return new SuccessDataResult<bool>(true, Messages.AlbumAdded);
        }

        //public IDataResult<bool> Delete(int albumId)
        //{
        //    var deletedAlbum = _albumDal.Get(x => x.Id == albumId);

        //    _albumDal.Delete(deletedAlbum);
        //    return new SuccessDataResult<bool>(true, Messages.AlbumDeleted);
        //}

        public IDataResult<List<AlbumListDto>> GetList()
        {
            var list = _albumDal.GetList();
            var albumList = new List<AlbumListDto>();
            foreach (var album in list)
            {
                albumList.Add(new()
                {
                    Id = album.Id,
                    Name = album.Name,
                    Status = album.Status,
                    CreatedDate = album.CreatedDate,
                    ModifiedDate = album.ModifiedDate,
                });
            }
            return new SuccessDataResult<List<AlbumListDto>>(albumList, Messages.AlbumList);
        }

        public IDataResult<bool> Update(AlbumUpdateDto album)
        {
            var existingAlbum = _albumDal.Get(a => a.Id == album.Id);
            if (existingAlbum == null)
            {
                return new ErrorDataResult<bool>(Messages.AlbumNotFound);
            }

            var updatedAlbum = new Album
            {
                Id = album.Id,
                Name = album.Name,
                Status = album.Status,
                CreatedDate = album.CreatedDate,
                ModifiedDate = album.ModifiedDate,
            };
            _albumDal.Update(updatedAlbum);
            return new SuccessDataResult<bool>(true, Messages.AlbumUpdated);
        }

        public IDataResult<bool> UpdateStatus(int albumId)
        {
            var album = _albumDal.Get(x => x.Id == albumId);
            if (album != null)
            {
                album.Status = !album.Status;
                _albumDal.Update(album);
                return new SuccessDataResult<bool>(true, Messages.AlbumStatus);
            }
            else
            {

                return new ErrorDataResult<bool>(false, Messages.AlbumStatusError);
            }
        }
    }
}