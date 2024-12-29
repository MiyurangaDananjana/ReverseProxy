using App1.Helper;
using App1.Model.DTO;
using App1.Model.Entity;

namespace App1.Data.Repositories.Interfaces
{
    public interface IUserRepositories
    {
        // CRUD operations
        User GetUserById(int userId);
        IEnumerable<User> GetAllUsers();
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int userId);

        // Additional methods (optional)
        User GetUserByUsername(string userName);
        IEnumerable<User> GetUsersByRole(UserRoleStatus role);
        bool UserExists(string userName, string password);

        bool CheckUserNameAlreadyUse(string userName);

        int GetUserIdByUserName(string userName);

        string GetUserPassword(string userName);

        UserDTO GetUserDetailsByUsername(string userName);

    }
}
