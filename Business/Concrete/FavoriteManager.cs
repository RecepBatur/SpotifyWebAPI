using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Concrete;
using Entities.Dtos.FavoriteDtos;
using Entities.Dtos.SongDtos;
using Entities.Dtos.UserDtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class FavoriteManager : IFavoriteService
    {
        private readonly IFavoriteDal _favoriteDal;
        private readonly ISongDal _songdal;
        private readonly IUserDal _userDal;
        private readonly ITokenHelper _tokenHelper;

        public FavoriteManager(IFavoriteDal favoriteDal, ISongDal songdal, IUserDal userDal, ITokenHelper tokenHelper)
        {
            _favoriteDal = favoriteDal;
            _songdal = songdal;
            _userDal = userDal;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<bool> Add(FavoriteAddDto favorite)
        {
            var favoriteAdd = new Favorite
            {
                Name = favorite.Name,
                Status = true,
                CreatedDate = favorite.CreatedDate,
                ModifiedDate = favorite.ModifiedDate,
                UserId = favorite.UserId,
            };
            _favoriteDal.Add(favoriteAdd);
            return new SuccessDataResult<bool>(true, Messages.FavoriteAdded);
        }

        public IDataResult<bool> Delete(int favoriteId)
        {
            var deletedFavorite = _favoriteDal.Get(x => x.Id == favoriteId);
            return new SuccessDataResult<bool>(true, Messages.FavoriteDeleted);
        }

        public IDataResult<bool> FavoriteSongAdd(FavoriteSongAddDto favoriteAddDto, string token)
        {
            var userToken = _tokenHelper.GetAuthenticatedUser(token);
            var users = _userDal.Get(x => x.Id == userToken);
            if (users == null)
            {
                return new ErrorDataResult<bool>(Messages.UserNotFound);
            }

            var songIds = favoriteAddDto.SongId;
            if (songIds == null || songIds.Count == 0)
            {
                return new ErrorDataResult<bool>(Messages.FavoriteError);
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
                    var favoriteAdd = new Favorite
                    {
                        Name = song.Name,
                        SongId = song.Id,
                        UserId = userToken,
                        Status = song.Status,
                        CreatedDate = song.CreatedDate,
                        ModifiedDate = song.ModifiedDate,
                    };
                    _favoriteDal.Add(favoriteAdd);

                    var favoriteAddResult = new FavoriteAddDto
                    {
                        Id = favoriteAdd.Id,
                        Name = favoriteAdd.Name,
                        SongId = new List<int> { favoriteAdd.SongId },
                        UserId = userToken,
                        Status = favoriteAdd.Status,
                        CreatedDate = favoriteAdd.CreatedDate,
                        ModifiedDate = favoriteAdd.ModifiedDate
                    };
                }
            }

            return new SuccessDataResult<bool>(true, Messages.FavoriteAdded);
        }

        //public IDataResult<bool> FavoriteSongAdd(int songId, UserDto user)//FavoriteAddDto => list int songId
        //{
        //    var users = _userDal.Get(x => x.Id == user.UserId);
        //    var song = _songdal.Get(x => x.Id == songId);
        //    if (user == null || song == null || song.Status == false)
        //    {
        //        return new ErrorDataResult<bool>(Messages.FavoriteError);
        //    }
        //    else
        //    {
        //        var favoriteAdd = new Favorite
        //        {
        //            Name = song.Name,
        //            SongId = song.Id,
        //            UserId = user.UserId,
        //            Status = song.Status,
        //            CreatedDate = song.CreatedDate,
        //            ModifiedDate = song.ModifiedDate,
        //        };
        //        _favoriteDal.Add(favoriteAdd);

        //        return new SuccessDataResult<bool>(true, Messages.FavoriteAdded);

        //    }
        //}

        public IDataResult<List<SongListDto>> GetSongList()
        {
            var list = _songdal.GetList();
            var songList = new List<SongListDto>();
            foreach (var song in list)
            {
                songList.Add(new()
                {
                    Id = song.Id,
                    Name = song.Name,
                    Status = song.Status,
                    CreatedDate = song.CreatedDate,
                    ModifiedDate = song.ModifiedDate,
                });
            }
            return new SuccessDataResult<List<SongListDto>>(songList, Messages.SongList);
        }

        public IDataResult<bool> Update(FavoriteUpdateDto favorite)
        {
            var updatedFavorite = new Favorite
            {
                Id = favorite.Id,
                Name = favorite.Name,
                Status = favorite.Status,
                CreatedDate = favorite.CreatedDate,
                ModifiedDate = favorite.ModifiedDate,
            };
            _favoriteDal.Update(updatedFavorite);
            return new SuccessDataResult<bool>(true, Messages.FavoriteUpdated);
        }

        public IDataResult<bool> UpdateStatus(int favoriteId)
        {
            var favorite = _favoriteDal.Get(x => x.Id == favoriteId);
            if (favorite != null)
            {
                favorite.Status = !favorite.Status;
                _favoriteDal.Update(favorite);
                return new SuccessDataResult<bool>(true, Messages.FavoriteUpdated);
            }
            else
            {

                return new ErrorDataResult<bool>(false, Messages.FavoriteNotFound);
            }
        }

        public IDataResult<List<UserFavoriteListDto>> UserFavoriteList(string token)
        {
            var userToken = _tokenHelper.GetAuthenticatedUser(token);
            var user = _userDal.Get(u => u.Id == userToken);

            if (user == null)
            {
                return new ErrorDataResult<List<UserFavoriteListDto>>(Messages.UserNotFound);
            }

            var userFavorites = _favoriteDal.GetList(f => f.UserId == user.Id);

            var favoriteList = userFavorites.Select(favorite => new UserFavoriteListDto
            {
                Name = favorite.Name,
                SongId = favorite.SongId,
                UserId = favorite.UserId,
                CreatedDate = favorite.CreatedDate,
                ModifiedDate = favorite.ModifiedDate,
                Status = favorite.Status
            }).ToList();

            return new SuccessDataResult<List<UserFavoriteListDto>>(favoriteList, Messages.FavoritesListed);
        }

    }
}
