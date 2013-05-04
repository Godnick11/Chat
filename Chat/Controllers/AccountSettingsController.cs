using Chat.BackendStorage;
using Chat.BackendStorage.Entities;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace Chat.Controllers
{
  [System.Web.Http.Authorize]
  public class AccountSettingsController : BaseApiController
  {
    private readonly IChatRepository _chatRepository;

    private User CurrentUser
    {
      get
      {
        var result = _chatRepository.GetUserByEmail(User.Identity.Name.ToLowerInvariant());
        return result;
      }
    }

    public AccountSettingsController()
    {
      // TODO: implement proper DI for WebApi controllers
      _chatRepository = System.Web.Mvc.DependencyResolver.Current.GetService<IChatRepository>();
    }

    [System.Web.Http.HttpGet]
    public string GetFirstName()
    {
      return CurrentUser.FirstName;
    }

    [System.Web.Http.HttpGet]
    public string GetLastName()
    {
      return CurrentUser.LastName;
    }

    [System.Web.Http.HttpPost]
    public HttpResponseMessage UpdateFirstName([FromBody] string firstName)
    {
      var user = CurrentUser;
      if (string.IsNullOrEmpty(firstName))
        return Request.CreateResponse<string>(HttpStatusCode.NotModified, user.FirstName);
      user.FirstName = firstName;
      _chatRepository.UpdateUser(user);
      return Request.CreateResponse<string>(HttpStatusCode.OK, user.FirstName);
    }

    [System.Web.Http.HttpPost]
    public HttpResponseMessage UpdateLastName([FromBody] string lastName)
    {
      var user = CurrentUser;
      if (string.IsNullOrEmpty(lastName))
        return Request.CreateResponse<string>(HttpStatusCode.NotModified, user.LastName);
      user.LastName = lastName;
      _chatRepository.UpdateUser(user);
      return Request.CreateResponse<string>(HttpStatusCode.OK, user.LastName);
    }
  }
}
