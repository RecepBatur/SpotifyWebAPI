using Core.Utilities.Results;
using Entities.Dtos.LibraryPlaylistDtos;
using Entities.Dtos.UserAlbumDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserAlbumService
    {
        IDataResult<bool> Add(UserAlbumAddDto playlist, string token);
        IDataResult<List<UserAlbumListDto>> UserAlbumList(string token);
    }
}
