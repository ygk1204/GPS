using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GPS201107.Models
{
    public class FileUploader
    {
        public FileUploader()
        {
        }
        public string GenerateFilename(string _sPath, string _sFilename)
        {
            string sFilenameHead = "";
            string sFilenameTail = "";
            string[] lFilename = _sFilename.Split('.');

            sFilenameTail = lFilename[lFilename.Length - 1];

            for (int i = 0; i < lFilename.Length - 1; i++)
            {
                if (i != lFilename.Length - 2)
                {
                    sFilenameHead += lFilename[i] + ".";
                }
                else
                {
                    sFilenameHead += lFilename[i];
                }
            }
            string path = Path.Combine(_sPath, sFilenameHead + "." + sFilenameTail);
            FileInfo fileinfo = new FileInfo(path);
            while (fileinfo.Exists == true) //같은 이름의 파일이 있을 경우에 처리방법 : 기존파일이름에 "-1"을 붙인다.
            {
                sFilenameHead += "-1";
                path = Path.Combine(_sPath, sFilenameHead + "." + sFilenameTail);
                fileinfo = new FileInfo(path);
            }

            return sFilenameHead + "." + sFilenameTail;
        }
    }
}
