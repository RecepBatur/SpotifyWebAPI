using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos.FavoriteDtos;
using Entities.Dtos.SongDtos;
using Entities.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IFavoriteService
    {
        IDataResult<bool> Add(FavoriteAddDto favorite);
        IDataResult<bool> Update(FavoriteUpdateDto favorite);
        IDataResult<bool> UpdateStatus(int favoriteId);
        IDataResult<List<UserFavoriteListDto>> UserFavoriteList(string token);
        IDataResult<List<SongListDto>> GetSongList();
        IDataResult<bool> FavoriteSongAdd(FavoriteSongAddDto favoriteAddDto, string token);
    }
}
