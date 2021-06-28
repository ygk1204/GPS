using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace GPS201107.Models.Grid
{
    [DataContract]
    public class Rule
    {
        [DataMember]
        public string field { get; set; }
        [DataMember]
        public string op { get; set; }
        [DataMember]
        public string data { get; set; }

        public string ConditionStmt()
        {
            string results = string.Empty;
            if (data.Contains("'"))
            {
                data = "error";
            }

            switch (op)
            {

                case "ep": results = " " + field + " = '" + data + "' "; break;
                case "ne": results = " " + field + " <> '" + data + "' "; break;
                case "cn": results = " upper(" + field + ") like '%" + data.ToUpper() + "%' "; break;
                case "lt": results = " " + field + " < '" + data + "' "; break;
                case "le": results = " " + field + " <= '" + data + "' "; break;
                case "gt": results = " " + field + " > '" + data + "' "; break;
                case "ge": results = " " + field + " >= '" + data + "' "; break;
                default: results = " " + field + " = '" + data + "' "; break;

            }

            return results;
        }
        public string ConditionStmt(string name)
        {
            string results = string.Empty;

            if (data.Contains("'"))
            {
                data = "error";
            }

            switch (op)
            {

                case "ep": results = " " + name + field + " = '" + data + "' "; break;
                case "ne": results = " " + name + field + " <> '" + data + "' "; break;
                case "cn": results = " " + name + field + " like '%" + data + "%' "; break;
                case "lt": results = " " + name + field + " < '" + data + "' "; break;
                case "le": results = " " + name + field + " <= '" + data + "' "; break;
                case "gt": results = " " + name + field + " > '" + data + "' "; break;
                case "ge": results = " " + name + field + " >= '" + data + "' "; break;
                default: results = " " + name + field + " = '" + data + "' "; break;

            }

            return results;
        }
    }
}
