using Core.Utilities.Results;
using Entities.Dtos;
using Entities.Dtos.FollowerDtos;
using Entities.Dtos.PlaylistDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IPlaylistService
    {
        IDataResult<bool> Add(PlaylistAddDto playlist, string token);
        IDataResult<bool> Update(PlaylistUpdateDto playlist);
        IDataResult<bool> UpdateStatus(int playlistId);
        IDataResult<List<PlaylistListDto>> UserGetPlaylist(string token);
        IDataResult<List<PlaylistListDto>> GetList();
        IDataResult<List<PlaylistListDto>> UsersGetList(int userId);
        //IDataResult<bool> PlaylistSongAdd(int songId, FollowerDto follower);
    }
}
