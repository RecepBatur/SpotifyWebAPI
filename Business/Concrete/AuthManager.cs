using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using Entities.Concrete;
using Entities.Dtos.FavoriteDtos;
using Entities.Dtos.LibraryDtos;
using Entities.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    //Register işlemleri yani kullanıcı kayıt işlemleri.
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;
        private IFavoriteService _favoriteService;
        private ILibraryService _libraryService;
        private IUserRoleService _userRoleService;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper, IFavoriteService favoriteService, ILibraryService libraryService, IUserRoleService userRoleService)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _favoriteService = favoriteService;
            _libraryService = libraryService;
            _userRoleService = userRoleService;
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            //kullanıcının kayıt ya da login olduktan sonra verilen token.
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            //kullanıcıdan bilgileri isteyip böyle bir kullanıcı var mı yok mu maili kontrol ettik.
            var userToCheck = _userService.GetByMail(userForLoginDto.Email);
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }
            //Kullanıcı giriş yaptıktan sonra hashli olan şifre db'deki şifre ile aynı mı onu kontrol edeceğiz.
            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }
            return new SuccessDataResult<User>(userToCheck, Messages.SuccessDefaultLogin);
        }

        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Email = userForRegisterDto.Email,
                UserName = userForRegisterDto.UserName,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true,
                CreatedDate = userForRegisterDto.CreatedDate,
                ModifiedDate = userForRegisterDto.ModifiedDate,
            };

            _userService.Add(user);

            var favorite = new FavoriteAddDto
            {
                UserId = user.Id,
            };
            _favoriteService.Add(favorite);

            
            var library = new LibraryAddDto
            {
                UserId = user.Id,
            };
            _libraryService.Add(library);

            var role = new UserOperationClaim
            {
                OperationClaimId = 2,
                UserId = user.Id,
            };
            _userRoleService.Add(role);


            return new SuccessDataResult<User>(user, Messages.UserRegistered);
        }

        public IResult UserExists(string email)
        {
            if (_userService.GetByMail(email) != null)
            {
                return new ErrorResult(Messages.UserAlreadyExists);
            }
            return new SuccessResult();
        }

        public IResult UsernameControl(string username)
        {
            if (_userService.GetByUser(username) != null)
            {
                return new ErrorResult(Messages.UserControl);
            }
            return new SuccessResult();
        }
    }
}
