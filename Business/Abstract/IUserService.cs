using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos.PlaylistDtos;
using Entities.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
        List<OperationClaim> GetClaims(User user);
        IResult Add(User user);
        User GetByMail(string email);
        User GetByUser(string username);
        IDataResult<User> GetById(int userId);
        IDataResult<bool> UpdateStatus(int userId);
        IDataResult<List<UserListDto>> GetList();

    }
}
