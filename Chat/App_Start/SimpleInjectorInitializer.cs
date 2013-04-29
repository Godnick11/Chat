[assembly: WebActivator.PostApplicationStartMethod(typeof(Chat.App_Start.SimpleInjectorInitializer), "Initialize")]

namespace Chat.App_Start
{
  using Chat.BackendStorage;
  using SimpleInjector;
  using SimpleInjector.Integration.Web.Mvc;
  using System.Configuration;
  using System.Reflection;
  using System.Web.Mvc;

  public static class SimpleInjectorInitializer
  {
    /// <summary>Initialize the container and register it as MVC3 Dependency Resolver.</summary>
    public static void Initialize()
    {
      // Did you know the container can diagnose your configuration? Go to: http://bit.ly/YE8OJj.
      var container = new Container();
      InitializeContainer(container);
      container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
      container.RegisterMvcAttributeFilterProvider();
      container.Verify();
      DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
    }

    private static void InitializeContainer(Container container)
    {
      container.RegisterPerWebRequest<IChatRepository>(() => new MongoChatRepository(ConfigurationManager.ConnectionStrings["mongo"].ConnectionString));
    }
  }
}