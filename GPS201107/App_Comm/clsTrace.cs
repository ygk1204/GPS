using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ASEWCFServiceLibrary.App_Data;

namespace ASEWCFServiceLibrary.App_Code
{
    class clsTrace
    {
        private string m_LogFileName;
      private string m_ProgramName;

      public clsTrace(string psLogFileName, string psProgramName)
      {
         m_LogFileName = psLogFileName;
         m_ProgramName = psProgramName;
      }

      public void StartTrace()
      {
         Trace(LogMessage.LOGLINE.ToString());
         Trace(LogMessage.STARTPROGRAM.ToString().Replace("$1", m_ProgramName));
      }

      public void EndTrace()
      {
         Trace(LogMessage.ENDPROGRAM.ToString().Replace("$1", m_ProgramName));
         Trace(LogMessage.LOGLINE.ToString());
      }

      public string Trace(string psLogMessage)
      {
         object[] oText = { DateTime.Now.ToLongDateString(),
                            DateTime.Now.ToLongTimeString(),
                            psLogMessage };

         return WriteLine(m_LogFileName, "{0} {1} : {2}", oText);

      }

      public static string WriteLine(string psFileName,
                                     string psTextFormat,
                                     object[] poText)
      {
         try
         {
            StreamWriter oFileWriter = File.AppendText(psFileName);
            oFileWriter.WriteLine(psTextFormat, poText);
            oFileWriter.Flush();
            oFileWriter.Close();
            return string.Empty;
         }
         catch (Exception ex)
         {
            return ex.Message;
         }

      }
    }
}
