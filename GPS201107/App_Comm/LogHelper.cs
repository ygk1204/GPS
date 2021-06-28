using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
namespace GPS201107.App_Comm
{
    public class LogHelper
    {

        public static void WriteErrorLog(string txt)
        {
            ILog log = LogManager.GetLogger("log4netlogger");
            log.Error(txt);
        }

        public static void WriteLog(string txt)
        {
            ILog log = LogManager.GetLogger("log4netlogger");
            log.Debug(txt);
        }
    }
}