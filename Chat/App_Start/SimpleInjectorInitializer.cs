[assembly: WebActivator.PostApplicationStartMethod(typeof(Chat.App_Start.SimpleInjectorInitializer), "Initialize")]

namespace Chat.App_Start
{
  using Chat.BackendStorage;
  using Chat.Hubs;
  using Chat.Infrastructure;
  using Chat.Utils;
  using Microsoft.AspNet.SignalR;
  using SimpleInjector;
  using SimpleInjector.Integration.Web.Mvc;
  using System.Reflection;
  using System.Web.Mvc;

  public static class SimpleInjectorInitializer
  {
    /// <summary>
    /// Initialize the container and register it as MVC3 Dependency Resolver.
    /// </summary>
    public static void Initialize()
    {
      var container = new Container();
      InitializeContainer(container);
      container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
      container.RegisterMvcAttributeFilterProvider();
      container.Verify();
      DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
      GlobalHost.DependencyResolver = new HubDependencyResolver(container);
    }

    private static void InitializeContainer(Container container)
    {
      container.Register<IConfigurationHelper, ConfigurationHelper>();
      container.Register<ISecurityManager, SecurityManager>();
      container.Register<IChatRepository, MongoChatRepository>();
      container.Register<ChatHub>();
    }
  }
}