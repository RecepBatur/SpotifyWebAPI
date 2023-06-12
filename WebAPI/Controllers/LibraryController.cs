using Business.Abstract;
using Entities.Dtos.LibraryDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class LibraryController : ControllerBase
    {
        private ILibraryService _libraryService;
        private ILibraryPlaylistService _libraryPlaylistService;
        private ILibraryAlbumService _libraryAlbumService;
        public LibraryController(ILibraryService libraryService, ILibraryPlaylistService libraryPlaylistService, ILibraryAlbumService libraryAlbumService)
        {
            _libraryService = libraryService;
            _libraryPlaylistService = libraryPlaylistService;
            _libraryAlbumService = libraryAlbumService;
        }

        [HttpGet(template: "userlibrarygetlist")]
        public IActionResult GetList(string token)
        {
            var result = _libraryService.GetList(token);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        /*[Authorize(Roles = "Member")]*/ //buraya bakılacak.
        [HttpGet(template: "userlibraryplaylist")]
        public IActionResult UserLibraryPlaylist(string token)
        {
            var result = _libraryPlaylistService.UserLibraryPlaylist(token);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpGet(template: "getalbumlistapi")]
        public async Task<IActionResult> GetAlbumListApi(string albumId)
        {
            var result = await _libraryAlbumService.GetAlbumListApi(albumId);
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
        public IActionResult Add(LibraryAddDto library)
        {
            var result = _libraryService.Add(library);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpGet(template: "albumaddapi")]
        public async Task<IActionResult> AlbumAddApi(string albumId)
        {
            var result = await _libraryAlbumService.AlbumAddApi(albumId);
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
        public IActionResult Update(LibraryUpdateDto library)
        {
            var result = _libraryService.Update(library);
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
        public IActionResult UpdateStatus(int libraryId)
        {
            var result = _libraryService.UpdateStatus(libraryId);
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
