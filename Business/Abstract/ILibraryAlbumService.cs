using Core.Utilities.Results;
using Entities.Dtos.LibraryDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ILibraryAlbumService
    {
        Task<IDataResult<LibraryListApiDto.Root>> GetAlbumListApi(string albumId);
        Task<IDataResult<bool>> AlbumAddApi(string albumId);
    }
}
