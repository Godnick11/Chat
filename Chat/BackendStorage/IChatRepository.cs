using Chat.BackendStorage.Entities;
using Chat.Models;
using System.Collections.Generic;

namespace Chat.BackendStorage
{
  public interface IChatRepository
  {
    bool IsUserRegistered(string email);
    User GetUserByEmail(string email);
    void AddNewUser(RegisterUserViewModel userData);
    bool IsCredentialsValid(string email, string password);
    Message SaveMessage(string userEmail, string messageBody);
    IEnumerable<Message> GetLastMessages(int messageCount);
    void UpdateUser(User user);
  }
}