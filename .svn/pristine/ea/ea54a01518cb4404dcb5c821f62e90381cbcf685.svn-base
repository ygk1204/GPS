using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ASEWCFServiceLibrary.App_Code;
using System.Data.OleDb;


namespace GPS201107.Models.HazardousRequest
{


    public class GPSHmRequestItem
    {
        public string _HMREQID { get; set; }
        public string _ITEMNAME { get; set; }
        public string _SEQ { get; set; }

        public string _ITEMVALUE { get; set; } //count 정보
        public string _DESCRIPTION { get; set; }
        public string _CREATEDATE { get; set; }
        public string _CREATEUSER { get; set; }
        public string _MODIFYDATE { get; set; }
        public string _MODIFYUSER { get; set; }

        public string _PARTNAME { get; set; }
        public string _COMPANYNAME { get; set; } //업체명
        public string _INGREDIENT { get; set; } //성분        
        public string _COUNT { get; set; } // 수량
        public string _DENSITY { get; set; } //밀도
        public string _DIAMETER { get; set; } //직경

        public GPSHmRequestItem()
        {


        }

        //FIle Type에 따라서 Value를 반환한다. 
        //Shield part와 Bump Die는 구분자로 나뉘어 있어 값이 1개도 없는 경우는 _ITEMVALUE(원래값) 을 반환한다.
        public string GetItemValue()
        {
            if (_ITEMNAME == clsConst.GpshmrequestItems.shieldpart.ToString() || _ITEMNAME == clsConst.GpshmrequestItems.ballpart.ToString())
            {
                if (string.IsNullOrEmpty(_PARTNAME) == true && string.IsNullOrEmpty(_COMPANYNAME) == true)
                    return _ITEMVALUE;
                else
                    return _PARTNAME + clsConst.HazarodusItemSeparator + _COMPANYNAME;

            }else if (_ITEMNAME == clsConst.GpshmrequestItems.bumpdie.ToString())
            {
                if (string.IsNullOrEmpty(_INGREDIENT) == true && string.IsNullOrEmpty(_COUNT) == true && string.IsNullOrEmpty(_DENSITY) && string.IsNullOrEmpty(_DIAMETER))
                    return _ITEMVALUE;
                else
                    return _INGREDIENT + clsConst.HazarodusItemSeparator + _COUNT + clsConst.HazarodusItemSeparator + _DENSITY + clsConst.HazarodusItemSeparator + _DIAMETER;
            }else
                return _ITEMVALUE;
        }

        //ItemValue값을 ItemName에 따라 세부항목으로 분류한다.
        public void ParsingItemValue()
        {
            if (string.IsNullOrEmpty(_ITEMVALUE) == false)
            {
                string[] temp = _ITEMVALUE.Split(new string[] { clsConst.HazarodusItemSeparator }, StringSplitOptions.None);
                if (_ITEMNAME == clsConst.GpshmrequestItems.shieldpart.ToString() || _ITEMNAME == clsConst.GpshmrequestItems.ballpart.ToString())
                {
                    if (temp != null && temp.Length == 2)
                    {
                        _PARTNAME = temp[0];
                        _COMPANYNAME = temp[1];
                    }
                }
                else if (_ITEMNAME == clsConst.GpshmrequestItems.bumpdie.ToString())
                {
                    if (temp != null && temp.Length == 4)
                    {
                        _INGREDIENT = temp[0];
                        _COUNT = temp[1];
                        _DENSITY = temp[2];
                        _DIAMETER = temp[3];
                    }
                }
            }
        }
        //public bool SaveGPSItemData(OleDbCommand cmd, GPSHmRequestItem item)
        //{
        //    bool bReturn = false;
        //    string sQuery = "";

        //    try
        //    {
        //            sQuery += " INSERT INTO Gpshmrequestitem ( HMREQID, ITEMNAME, SEQ, ITEMVALUE, CREATEDATE, CREATEUSER ) ";
        //            sQuery += " VALUES ( '" + item._HMREQID + "', '" + item._ITEMNAME + "', '" + item._SEQ + "','" + item._ITEMVALUE + "'," + "'";
        //            sQuery += item._CREATEDATE + "','" + item._CREATEUSER + "' " + " ) ";

        //            cmd.CommandText = sQuery;
        //            cmd.ExecuteNonQuery();
        //            return bReturn;
        //    }

        //    catch (Exception e)
        //    {
        //        bReturn = false;
        //        throw e;
        //    }

        //}

        public bool Save(OleDbCommand cmd)
        {
            bool bReturn = false;
            string sQuery = "";

            try
            {
                sQuery += " INSERT INTO Gpshmrequestitem ( HMREQID, ITEMNAME, SEQ, ITEMVALUE, CREATEDATE, CREATEUSER ) ";
                sQuery += " VALUES ( '" + _HMREQID + "', '" + _ITEMNAME + "', '" + _SEQ + "','" + _ITEMVALUE + "'," + "to_char(sysdate, 'yyyy-mm-dd hh24:mi:ss'),'" + _CREATEUSER + "' " + " ) ";

                cmd.CommandText = sQuery;
                cmd.ExecuteNonQuery();
                return bReturn;
            }

            catch (Exception e)
            {
                bReturn = false;
                throw e;
            }
        }



        public int CompareTo(GPSHmRequestItem c1)
        {
            return this._HMREQID.CompareTo(c1._HMREQID);
        }


        public object Copy()
        {
            return base.MemberwiseClone();
        }

    }
}