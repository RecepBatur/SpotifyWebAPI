using Core.Utilities.Results;
using Entities.Dtos.PlaylistDtos;
using Entities.Dtos.SongDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ISongService
    {
        IDataResult<bool> Add(SongAddDto song);
        IDataResult<bool> Update(SongUpdateDto song);
        IDataResult<bool> UpdateStatus(int songId);
        IDataResult<List<SongListDto>> GetList();

        Task<IDataResult<SongListApiDto.Root>> GetSongListApi(string albumId);
        Task<IDataResult<bool>> SongAddApi(string albumId);
    }
}
