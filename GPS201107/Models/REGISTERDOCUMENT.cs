using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPS201107.Models
{
    public class REGISTERDOCUMENT
    {
        public List<GPSDOCUMENTLIST> DocumenList { get; set; }
        public REGISTERDOCUMENT()
        {
            DocumenList = new List<GPSDOCUMENTLIST>();
            GPSDOCUMENTLIST document = new GPSDOCUMENTLIST();

            DocumenList.Add(document);
        }
    }
}