using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos.FollowerDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IFollowerService
    {
        //IDataResult<bool> Add(FollowerAddDto follower);
        IDataResult<bool> Update(FollowerUpdateDto follower);
        IDataResult<bool> UpdateStatus(int followerId);
        IDataResult<List<FollowerListDto>> GetList();
        IDataResult<bool> AddFollower(FollowerAddDto follower,string token);
    }
}
