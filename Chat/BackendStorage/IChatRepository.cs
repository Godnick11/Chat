using Chat.BackendStorage.Entities;
using Chat.Models;

namespace Chat.BackendStorage
{
  public interface IChatRepository
  {
    bool IsUserRegistered(string email);
    User GetUserByEmail(string email);
    void AddNewUser(RegisterUserViewModel userData);
    bool IsCredentialsValid(string email, string password);
  }
}