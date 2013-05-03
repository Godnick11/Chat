using Microsoft.AspNet.SignalR;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Chat.Infrastructure
{
  public class HubDependencyResolver : DefaultDependencyResolver
  {
    private readonly Container _container;

    public HubDependencyResolver(Container container)
    {
      _container = container;
    }

    public override object GetService(Type serviceType)
    {
      var result = ((IServiceProvider)_container).GetService(serviceType) ?? base.GetService(serviceType);
      return result;
    }

    public override IEnumerable<object> GetServices(Type serviceType)
    {
      var containerServices = _container.GetAllInstances(serviceType);
      var defaultResolverServices = base.GetServices(serviceType);
      var result = containerServices.Concat(defaultResolverServices).Distinct();
      return result;
    }
  }
}