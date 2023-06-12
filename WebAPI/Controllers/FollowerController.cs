using Business.Abstract;
using Entities.Concrete;
using Entities.Dtos.FollowerDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class FollowerController : ControllerBase
    {
        private IFollowerService _followerService;

        public FollowerController(IFollowerService followerService)
        {
            _followerService = followerService;
        }

        [HttpGet(template: "getall")]
        public IActionResult GetList()
        {
            var result = _followerService.GetList();
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        //[HttpPost(template: "add")]
        //public IActionResult Add(FollowerAddDto follower)
        //{
        //    var result = _followerService.Add(follower);
        //    if (result.Success)
        //    {
        //        return Ok(result.Message);
        //    }
        //    else
        //    {
        //        return BadRequest(result.Message);
        //    }
        //}
        [HttpPost(template: "update")]
        public IActionResult Update(FollowerUpdateDto follower)
        {
            var result = _followerService.Update(follower);
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
        public IActionResult UpdateStatus(int followerId)
        {
            var result = _followerService.UpdateStatus(followerId);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpPost(template: "follow")]
        public IActionResult Follow(FollowerAddDto follower, string token)
        {
            var result = _followerService.AddFollower(follower, token);
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
