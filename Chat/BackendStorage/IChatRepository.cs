using Chat.BackendStorage.Entities;
using Chat.Models;

namespace Chat.BackendStorage
{
  public interface IChatRepository
  {
    User GetUserByEmail(string email);
    void AddNewUser(RegisterUserViewModel userData);
  }
}