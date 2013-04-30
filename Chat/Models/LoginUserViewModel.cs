using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Chat.Models
{
  public class LoginUserViewModel
  {
    [Required(ErrorMessage = "email is required")]
    [DisplayName("email")]
    [RegularExpression("^[_A-Za-z0-9-\\+]+(\\.[_A-Za-z0-9-]+)*@[A-Za-z0-9-]+(\\.[A-Za-z0-9]+)*(\\.[A-Za-z]{2,})$", ErrorMessage = "the email address is invalid")]
    public string Email { set; get; }

    [Required(ErrorMessage = "password is required")]
    [DisplayName("password")]
    public string Password { set; get; }

    [DisplayName("remember me")]
    public bool RememberMe { set; get; }
  }
}