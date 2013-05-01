using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Chat.Models
{
  public class RegisterUserViewModel
  {
    [Required(ErrorMessage = "first name is required")]
    [StringLength(50, ErrorMessage = "too long")]
    [DisplayName("first name")]
    public string FirstName { set; get; }

    [Required(ErrorMessage = "first name is required")]
    [StringLength(50, ErrorMessage = "too long")]
    [DisplayName("last name")]
    public string LastName { set; get; }

    [Required(ErrorMessage = "email is required")]
    [StringLength(50, ErrorMessage = "too long")]
    [DisplayName("email")]
    [RegularExpression("^[_A-Za-z0-9-\\+]+(\\.[_A-Za-z0-9-]+)*@[A-Za-z0-9-]+(\\.[A-Za-z0-9]+)*(\\.[A-Za-z]{2,})$", ErrorMessage = "the email address is invalid")]
    [Remote("IsEmailAvailable", "Home", ErrorMessage = "the email is not available")]
    public string Email { set; get; }

    [Required(ErrorMessage = "password is required")]
    [DisplayName("password")]
    public string Password { set; get; }

    [Required(ErrorMessage = "you need to repeat the password")]
    [DisplayName("repeat password")]
    public string RepeatPassword { set; get; }

    [DisplayName("remember me")]
    public bool RememberMe { set; get; }
  }
}