using Core.Utilities.Results;
using Entities.Dtos.LibraryPlaylistDtos;
using Entities.Dtos.PlaylistDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ILibraryPlaylistService
    {
        IDataResult<bool> Add(LibraryPlaylistAddDto playlist, string token);
        IDataResult<List<LibraryPlaylistDto>> UserLibraryPlaylist(string token);
    }
}
