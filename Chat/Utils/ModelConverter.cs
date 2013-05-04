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
              Email = viewModel.Email.ToLowerInvariant(),
              FirstName = viewModel.FirstName,
              LastName = viewModel.LastName,
            };
      return result;
    }

    public static ChatMessage ToViewModel(this Message entity)
    {
      var result = new ChatMessage { Text = entity.Text, WhenPosted = entity.WhenPosted, WhoPosted = entity.WhoPosted };
      return result;
    }
  }
}