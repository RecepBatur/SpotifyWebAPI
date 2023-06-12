using Business.Abstract;
using Entities.Concrete;
using Entities.Dtos.FollowerDtos;
using Entities.Dtos.PlaylistDtos;
using Entities.Dtos.PlaylistFollowingDtos;
using Entities.Dtos.PlaylistSongDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class PlaylistController : ControllerBase
    {
        private IPlaylistService _playlistService;
        private IPlaylistSongService _playlistSongService;
        private readonly IPlaylistFollowingService _playlistFollowingService;
        public PlaylistController(IPlaylistService playlistService, IPlaylistSongService playlistSongService, IPlaylistFollowingService playlistFollowingService)
        {
            _playlistService = playlistService;
            _playlistSongService = playlistSongService;
            _playlistFollowingService = playlistFollowingService;
        }
        [HttpGet(template: "getall")]
        public IActionResult GetList()
        {
            var result = _playlistService.GetList();
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpGet(template: "usergetplaylist")]
        public IActionResult GetList(string token)
        {
            var result = _playlistService.UserGetPlaylist(token);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpGet(template: "usersgetlist")]
        public IActionResult UsersGetList(int userId)
        {
            var result = _playlistService.UsersGetList(userId);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpPost(template: "playlistsongadd")]
        public IActionResult PlaylistSongAdd(PlaylistSongAddDto playlistSongAddDto, string token)
        {
            var result = _playlistSongService.AddSongPlaylist(playlistSongAddDto, token);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpPost(template: "followplaylist")]
        public IActionResult FollowPlaylist(PlaylistFollowAddDto playlistFollowAddDto, string token)
        {
            var result = _playlistFollowingService.AddFollowPlaylist(playlistFollowAddDto, token);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpPost(template: "add")]
        public IActionResult Add(PlaylistAddDto playlist, string token)
        {
            var result = _playlistService.Add(playlist, token);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpPost(template: "update")]
        public IActionResult Update(PlaylistUpdateDto playlist)
        {
            var result = _playlistService.Update(playlist);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpPost(template: "updatestatus")]
        public IActionResult UpdateStatus(int playlistId)
        {
            var result = _playlistService.UpdateStatus(playlistId);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
    }
}
