using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Concrete;
using Entities.Dtos.PlaylistDtos;
using Entities.Dtos.UserDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IResult Add(User user)
        {
            UserValidator userValidator = new UserValidator();
            var result = userValidator.Validate(user);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            _userDal.Add(user);
            return new SuccessResult(Messages.UserAdded);
        }

        public User GetByMail(string email)
        {
            return _userDal.Get(filter: u => u.Email == email);
        }

        public List<OperationClaim> GetClaims(User user)
        {
            return _userDal.GetClaims(user);
        }

        public IDataResult<List<UserListDto>> GetList()
        {
            var list = _userDal.GetList();
            var userlist = new List<UserListDto>();
            foreach (var user in list)
            {
                userlist.Add(new()
                {
                    Id = user.Id,
                    Status = user.Status,
                    CreatedDate = user.CreatedDate,
                    ModifiedDate = user.ModifiedDate,
                });
            }
            return new SuccessDataResult<List<UserListDto>>(userlist, Messages.PlaylistList);
        }

        public IDataResult<bool> UpdateStatus(int userId)
        {
            var user = _userDal.Get(x => x.Id == userId);
            if (user != null)
            {
                user.Status = !user.Status;
                _userDal.Update(user);
                return new SuccessDataResult<bool>(true, Messages.UserStatus);
            }
            else
            {

                return new ErrorDataResult<bool>(false, Messages.UserNotFound);
            }
        }

        public User GetByUser(string username)
        {
            return _userDal.Get(filter: u => u.UserName == username);
        }

        public IDataResult<User> GetById(int userId)
        {
            return (IDataResult<User>)_userDal.Get(x => x.Id == userId);

        }
    }
}
