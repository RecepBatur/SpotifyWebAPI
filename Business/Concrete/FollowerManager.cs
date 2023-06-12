using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Concrete;
using Entities.Dtos.AlbumDtos;
using Entities.Dtos.FollowerDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class FollowerManager : IFollowerService
    {
        private readonly IFollowerDal _followerDal;
        private readonly IUserDal _userDal;
        private readonly ITokenHelper _tokenHelper;

        public FollowerManager(IFollowerDal followerDal, IUserDal userDal, ITokenHelper tokenHelper)
        {
            _followerDal = followerDal;
            _userDal = userDal;
            _tokenHelper = tokenHelper;
        }

        //public IDataResult<bool> Add(FollowerAddDto follower)
        //{
        //    var addedFollower = new Follower
        //    {
        //        UserId = follower.UserId,
        //        FollowerId = follower.FollowerId,
        //        Status = follower.Status,
        //        CreatedDate = follower.CreatedDate,
        //        ModifiedDate = follower.ModifiedDate,
        //    };
        //    _followerDal.Add(addedFollower);
        //    return new SuccessDataResult<bool>(true, Messages.FollowerAdded);
        //}

        public IDataResult<List<FollowerListDto>> GetList()
        {
            var list = _followerDal.GetList();
            var followerList = new List<FollowerListDto>();
            foreach (var follower in list)
            {
                followerList.Add(new()
                {
                    Id = follower.Id,
                    UserId = follower.UserId,
                    FollowerId = follower.FollowerId,
                    Status = follower.Status,
                    CreatedDate = follower.CreatedDate,
                    ModifiedDate = follower.ModifiedDate,
                });
            }
            return new SuccessDataResult<List<FollowerListDto>>(followerList, Messages.FollowerList);
        }

        public IDataResult<bool> Update(FollowerUpdateDto follower)
        {
            var updatedFollower = new Follower
            {
                Id = follower.Id,
                UserId = follower.UserId,
                Status = follower.Status,
                CreatedDate = follower.CreatedDate,
                ModifiedDate = follower.ModifiedDate,
            };
            _followerDal.Update(updatedFollower);
            return new SuccessDataResult<bool>(true, Messages.FollowerUpdated);
        }

        public IDataResult<bool> UpdateStatus(int followerId)
        {
            var follower = _followerDal.Get(x => x.Id == followerId);
            if (follower != null)
            {
                follower.Status = !follower.Status;
                _followerDal.Update(follower);
                return new SuccessDataResult<bool>(true, Messages.FollowerStatus);
            }
            else
            {

                return new ErrorDataResult<bool>(false, Messages.FollowerStatusNotFound);
            }
        }
        public IDataResult<bool> AddFollower(FollowerAddDto follower, string token)
        {
            var userToken = _tokenHelper.GetAuthenticatedUser(token);
            var user = _userDal.Get(x => x.Id == userToken);
            var followerUser = _userDal.Get(x => x.Id == follower.FollowerId);
            if (user == null || user.Status == false || followerUser == null || followerUser.Status == false)
            {
                return new ErrorDataResult<bool>(Messages.FollowerError);
            }
            else
            {
                var followerAdd = new Follower
                {
                    UserId = user.Id,
                    FollowerId = follower.FollowerId,
                    Status = user.Status,
                    CreatedDate = user.CreatedDate,
                    ModifiedDate = user.ModifiedDate,
                };
                _followerDal.Add(followerAdd);

                var followerAddResult = new FollowerAddDto
                {
                    FollowerId = followerAdd.FollowerId,
                };

                return new SuccessDataResult<bool>(true, Messages.FollowerAdded);
            }
        }

    }
}
