using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos.SongDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Template;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class SongController : ControllerBase
    {
        private ISongService _songService;

        public SongController(ISongService songService)
        {
            _songService = songService;
        }

        [HttpGet(template: "getall")]
        //[Authorize(Roles = "Member")]
        public IActionResult GetList()
        {
            var result = _songService.GetList();
            if (result.Success)
            {
                return Ok(result); //burada result.Data silmiştik.
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpGet(template: "getsonglist")]
        public async Task<IActionResult> GetSongList(string albumId)
        {
            var result = await _songService.GetSongListApi(albumId);
            if (result.Success)
            {
                return Ok(result); //burada result.Data silmiştik.
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpPost(template: "add")]
        public IActionResult Add(SongAddDto song)
        {
            var result = _songService.Add(song);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpPost(template: "songaddapi")]
        public async Task<IActionResult> SongAddApi(string albumId)
        {
            var result = await _songService.SongAddApi(albumId);
            //if (result.Success)
            //{
            return Ok(result);
            //}
            //else
            //{
            //    return BadRequest(result.Message);
            //}
        }
        [HttpPost(template: "update")]
        public IActionResult Update(SongUpdateDto song)
        {
            var result = _songService.Update(song);
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
        public IActionResult UpdateStatus(int songId)
        {
            var result = _songService.UpdateStatus(songId);
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
