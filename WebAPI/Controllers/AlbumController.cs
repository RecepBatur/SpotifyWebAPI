using Autofac.Core.Lifetime;
using Business.Abstract;
using Entities.Concrete;
using Entities.Dtos.AlbumDtos;
using Entities.Dtos.SongDtos;
using Entities.Dtos.UserAlbumDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class AlbumController : ControllerBase
    {
        private IAlbumService _albumService;
        private IUserAlbumService _userAlbumService;

        public AlbumController(IAlbumService albumService, IUserAlbumService userAlbumService)
        {
            _albumService = albumService;
            _userAlbumService = userAlbumService;
        }

        [HttpGet(template: "getall")]
        public IActionResult GetList()
        {
            var result = _albumService.GetList();
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpPost(template: "add")]
        public IActionResult Add(AlbumAddDto album, string token)
        {
            var result = _albumService.Add(album, token);
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
        public IActionResult Update(AlbumUpdateDto album)
        {
            var result = _albumService.Update(album);
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
        public IActionResult UpdateStatus(int albumId)
        {
            var result = _albumService.UpdateStatus(albumId);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpPost(template: "useralbumadd")]
        public IActionResult UserAlbumAdd(UserAlbumAddDto album, string token)
        {
            var result = _userAlbumService.Add(album, token);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpGet(template: "useralbumlist")]
        public IActionResult UserAlbumList(string token)
        {
            var result = _userAlbumService.UserAlbumList(token);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
    }
}
