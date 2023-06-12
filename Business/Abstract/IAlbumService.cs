using Core.Utilities.Results;
using Entities.Dtos.AlbumDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAlbumService
    {
        IDataResult<bool> Add(AlbumAddDto album, string token);
        IDataResult<bool> Update(AlbumUpdateDto album);
        IDataResult<List<AlbumListDto>> GetList();
        IDataResult<bool> UpdateStatus(int favoriteId);
        //Task<IDataResult<AlbumListApiDto.Root>> GetListApi();
        //Task<IDataResult<bool>> AddAlbumApi(AlbumAddDto album);
    }
}
