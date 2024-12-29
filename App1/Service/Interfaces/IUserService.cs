namespace App1.Service.Interfaces
{
    public interface IUserService
    {
        bool ValidateCredentials(string userName, string password);
    }
}
