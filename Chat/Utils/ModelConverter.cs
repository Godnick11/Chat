using Chat.BackendStorage.Entities;
using Chat.Models;

namespace Chat.Utils
{
  public static class ModelConverter
  {
    public static User ToEntity(this RegisterUserViewModel viewModel)
    {
      var result = new User
            {
              Email = viewModel.Email,
              FirstName = viewModel.FirstName,
              LastName = viewModel.LastName,
            };
      return result;
    }
  }
}