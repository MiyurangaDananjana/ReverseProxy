using App1.Data.Repositories.Interfaces;
using App1.Helper;
using App1.Model.DTO;
using App1.Model.Entity;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace App1.Data.Repositories.Implementations
{
    public class UserRepositories : IUserRepositories
    {

        private readonly AppDbContext _context;

        public UserRepositories(AppDbContext context)
        {
            _context = context;
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public bool CheckUserNameAlreadyUse(string userName)
        {
            bool isUser = _context.Users.AsNoTracking().Where(x=>x.UserName == userName).Any();
            return isUser;
        }

        public void DeleteUser(int userId)
        {
            var user = _context.Users.Find(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public User GetUserById(int userId)
        {
            return _context.Users.Find(userId);
        }

        public User GetUserByUsername(string userName)
        {

            return _context.Users.FirstOrDefault(u => u.UserName == userName);
        }

        public UserDTO GetUserDetailsByUsername(string userName)
        {
            var result = (from u in _context.Users
                         join r in _context.RoleStatuses on u.Role equals r.Id
                         where u.UserName == userName
                         select new UserDTO
                         {
                             Id = u.UserId,
                             Name = u.UserName,
                             UserRole = r.Name,

                         }).FirstOrDefault();
            return result;
        }


        public int GetUserIdByUserName(string userName)
        {
          return _context.Users.AsNoTracking().Where(_ => _.UserName == userName).Select(x=>x.UserId).FirstOrDefault();  
        }

        public string GetUserPassword(string userName)
        {
            return _context.Users.AsNoTracking().Where(x => x.UserName == userName).Select(x => x.Password).FirstOrDefault();
        }

      

        public IEnumerable<User> GetUsersByRole(UserRoleStatus role)
        {
            return _context.Users.Where(u => u.Role == role.Id);
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public bool UserExists(string userName, string password)
        {
            return _context.Users.Any(u => u.UserName == userName && u.Password == password);
        }

       



      
    }
}
