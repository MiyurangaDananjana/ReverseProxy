using App1.Data.Repositories.Interfaces;
using App1.Helper;
using App1.Service.Interfaces;
using StackExchange.Redis;

namespace App1.Service.Implementations
{
    public class UserService : IUserService
    {

        private readonly IUserRepositories _userRepositories;
        public UserService(IUserRepositories userRepositories)
        {
            _userRepositories = userRepositories;
        }

        public bool ValidateCredentials(string userName, string password)
        {
            if (!_userRepositories.CheckUserNameAlreadyUse(userName))
            {
                return false;
            }
            bool verifyPassword = PasswordHasherHelper.VerifyPassword(password, _userRepositories.GetUserPassword(userName));

            if (verifyPassword)
            {
                return true;
            }
            return false;

        }
    }
}
