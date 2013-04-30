using System.Configuration;

namespace Chat.Utils
{
  public class ConfigurationHelper : IConfigurationHelper
  {
    public string MongoDbConnectionString { get { return ConfigurationManager.ConnectionStrings["mongo"].ConnectionString; } }
  }
}