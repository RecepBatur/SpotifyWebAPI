using Business.Abstract;
using Entities.Concrete;
using Entities.Dtos.FavoriteDtos;
using Entities.Dtos.SongDtos;
using Entities.Dtos.UserDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    //[Authorize(Roles = "Member")]
    public class FavoriteController : ControllerBase
    {
        private IFavoriteService _favoriteService;
        private ISongService _songService;
        private IUserService _userService;

        public FavoriteController(IFavoriteService favoriteService, ISongService songService, IUserService userService)
        {
            _favoriteService = favoriteService;
            _songService = songService;
            _userService = userService;
        }
        [HttpGet(template: "userfavoritesonglist")]
        public IActionResult UserFavoriteSongList(string token)
        {
            var result = _favoriteService.UserFavoriteList(token);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpGet(template: "getsonglist")]
        public IActionResult GetSongList()
        {
            var result = _songService.GetList();
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
        public IActionResult Add(FavoriteAddDto favorite)
        {
            var result = _favoriteService.Add(favorite);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpPost(template: "favoritesongadd")]
        public IActionResult FavoriteSongAdd(FavoriteSongAddDto favoriteAddDto, string token)
        {
            var result = _favoriteService.FavoriteSongAdd(favoriteAddDto,token);
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
        public IActionResult Update(FavoriteUpdateDto favorite)
        {
            var result = _favoriteService.Update(favorite);
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
        public IActionResult UpdateStatus(int favoriteId)
        {
            var result = _favoriteService.UpdateStatus(favoriteId);
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
