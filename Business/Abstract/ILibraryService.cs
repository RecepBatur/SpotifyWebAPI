using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos.AlbumDtos;
using Entities.Dtos.LibraryDtos;
using Entities.Dtos.LibraryPlaylistDtos;
using Entities.Dtos.PlaylistDtos;
using Entities.Dtos.SongDtos;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ILibraryService
    {
        IDataResult<bool> Add(LibraryAddDto library);
        IDataResult<bool> Update(LibraryUpdateDto library);
        IDataResult<List<LibraryListDto>> GetList(string token);
        IDataResult<bool> UpdateStatus(int libraryId);

    }
}
