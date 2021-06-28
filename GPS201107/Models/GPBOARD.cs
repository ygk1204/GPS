using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPS201107.Models
{
    public class GPBOARD
    {
        public string _AF_IDEN          {get;set;}
	    public string _AF_NUMBER        {get;set;}
	    public string _AF_VINUMBER      {get;set;}
	    public string _AF_RENUMBER      {get;set;}
	    public string _AF_ID            {get;set;}
	    public string _AF_NAME          {get;set;}
	    public string _AF_EMAIL         {get;set;}
	    public string _AF_FILENAME      {get;set;}
	    public string _AF_TITLE         {get;set;}
	    public string _AF_DATE          {get;set;}
	    public string _AF_READ          {get;set;}
	    public string _AF_COMMENT       {get;set;}
	    public string _AF_PASS          {get;set;}
	    public string _AF_IP            {get;set;}
	    public string _AF_CHECK         {get;set;}
	    public string _AF_REALNUMBER    {get;set;}
	    public string _AF_BOARDNAME     {get;set;}
	    public string _AF_EMPNO         {get;set;}

        public GPBOARD()
        {
        }
        public object Copy()
        {
            return base.MemberwiseClone();
        }
    }

}