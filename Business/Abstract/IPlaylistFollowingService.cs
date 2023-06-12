using Core.Utilities.Results;
using Entities.Dtos.PlaylistFollowingDtos;
using Entities.Dtos.PlaylistSongDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IPlaylistFollowingService
    {
        IDataResult<bool> AddFollowPlaylist(PlaylistFollowAddDto playlistFollowAddDto, string token);
    }
}
