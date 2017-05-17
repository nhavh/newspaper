using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using log4net;
using log4net.Config;

namespace TMV.Exceptions
{
    public class Logger
    {
        private static readonly ILog frameworkLogger;

        static Logger()
        {
            frameworkLogger = LogManager.GetLogger("FrameworkLog");
            XmlConfigurator.Configure();
        }

        public static void Error(Exception ex)
        {
            frameworkLogger.Error(ex.Message, ex);
            frameworkLogger.Debug(ex.StackTrace, ex);
        }
    }
}
