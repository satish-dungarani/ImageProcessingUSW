using log4net;
using log4net.Config;
using System.Reflection;
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace ImageProcessing.Web.Helpers
{
    public class LogHelper
    {
        public static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);
    }
}

