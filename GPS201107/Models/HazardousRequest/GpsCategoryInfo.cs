using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ASEWCFServiceLibrary.App_Code;

namespace GPS201107.Models.HazardousRequest
{
    public class GpsCategoryInfo
    {
        public string _CATEGORYNAME { get; set; }
        public string _ITEMNAME { get; set; }
        public string _SEQ { get; set; }
        public string _ALIAS { get; set; }
        public string _DESCRIPTION { get; set; }
       public string _ITEMVALUE { get; set; }
        public string _DEFAULTVALUE { get; set; }

        public GpsCategoryInfo()
        {
        }


        public int CompareTo(GpsCategoryInfo c1)
        {
            return this._CATEGORYNAME.CompareTo(c1._CATEGORYNAME);
        }


        public object Copy()
        {
            return base.MemberwiseClone();
        }
    }
}