using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ASEWCFServiceLibrary.App_Code;
using System.Reflection;
using System.Data;
using System.Collections;

namespace GPS201107.Models
{
    public class EntityMapper
    {
        public Dictionary<string, dynamic> Table_entity { get; set; }
        public List<List<dynamic>> Result { get; set; }
        public string WhereCondition { get; set; }
        public clsDBControl oDB { get; set; }
        public string sSql { get; set; }

        public EntityMapper()
        {
            Table_entity = new Dictionary<string, dynamic>();
            Result = new List<List<dynamic>>();
        }
        public string Create(dynamic obj, string table_name)
        {
            Type t = obj.GetType(); //객체의 타입 가져오기
            //sql 구문 만들기 시작

            PropertyInfo[] Properties = t.GetProperties(BindingFlags.Instance | BindingFlags.Public);

            //insert into table명 (컬럼1,컬럽2,...)
            string sSql_insert = "insert into ";
            sSql_insert += table_name + "(";
            string sSql_values = "values (";

            for (int i = 0; i < Properties.Length; i++)
            {


                string firstofField = Properties[i].Name.ToString().Substring(0, 1);
                string object_member = string.Empty;
                string table_field = string.Empty;

                string sObjMember = Properties[i].GetValue(obj, null);
                string sFieldName = Properties[i].Name.ToString();
                // 20181213 JHLee: insert, update시, sql value의 '를 ''를 변환해준다.
                if (sObjMember != null && sObjMember.Length > 0 &&
                    (sFieldName.ToUpper() != "FILEID" ))
                {
                    if (sObjMember.Contains("'") == true)
                    {
                        sObjMember = sObjMember.Replace("'", "''");
                    }
                }

                if (firstofField == "_")
                {
                    table_field = Properties[i].Name.ToString().Substring(1);

                    object_member = "'" + sObjMember + "'";
                }
                else
                {
                    table_field = Properties[i].Name.ToString();
                    object_member = sObjMember;
                    if (object_member != null)
                    {
                        object_member = object_member.Trim();
                    }
                    if (object_member == "" || object_member == null)
                    {
                        object_member = "'" + object_member + "'";
                    }
                }


                sSql_insert += table_field;
                sSql_values += object_member;

                if (i < Properties.Length - 1)
                {
                    sSql_insert += ",";
                    sSql_values += ",";
                }
                else
                {
                    sSql_insert += ")";
                    sSql_values += ")";
                }

            }
            sSql = sSql_insert + sSql_values;

            //sql 구문 만들기 끝
            return sSql;

        }
        public string Save(dynamic obj, string tableName, string WhereCondition)
        {
            Type t = obj.GetType(); //객체의 타입 가져오기
            //sql 구문 만들기 시작

            PropertyInfo[] Properties = t.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            sSql = "update ";
            sSql += tableName + " set ";
            for (int i = 0; i < Properties.Length; i++)
            {
                string firstofField = Properties[i].Name.ToString().Substring(0, 1);
                string object_member = string.Empty;
                string table_field = string.Empty;

                // 20181213 JHLee: insert, update시, sql value의 '를 ''를 변환해준다.
                string sObjMember = Properties[i].GetValue(obj, null);

                if (sObjMember != null && sObjMember.Length > 0)
                {
                    if (sObjMember.Contains("'") == true)
                    {
                        sObjMember = sObjMember.Replace("'", "''");
                    }
                }


                if (firstofField == "_")
                {
                    table_field = Properties[i].Name.ToString().Substring(1);
                    object_member = "'" + sObjMember + "'";
                }
                else
                {
                    table_field = Properties[i].Name.ToString();
                    object_member = sObjMember;
                    if (object_member != null)
                    {
                        object_member = object_member.Trim();
                    }
                    if (object_member == "" || object_member == null)
                    {
                        object_member = "'" + object_member + "'";

                    }
                }


                sSql += table_field + " = " + object_member;
                if (i < Properties.Length - 1)
                {
                    sSql += ",";
                }
            }

            sSql += " " + WhereCondition;
            //sql 구문 만들기 끝
            return sSql;
        }
        public void Load()
        {
            //1. object to return 

            //2. start to write query 
            sSql = string.Empty;
            string Select_syntax = "select ";
            string From_syntax = " from ";

            //3. select 컬럼1,컬럼2,컬럼3 .... 만들기 
            foreach (KeyValuePair<string, dynamic> tablename in Table_entity)
            {
                Type Class = tablename.Value.GetType(); //객체의 타입 가져오기
                string alliance = "";
                string tableName = tablename.Key.TrimEnd();
                if (tableName.Contains(" "))
                {
                    string[] name = tableName.Split(' ');
                    alliance = name[name.Length - 1];
                    alliance = alliance + ".";
                }


                //select 구문 만들기 - 객체의 멤버 가져오기
                PropertyInfo[] Properties = Class.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                for (int i = 0; i < Properties.Length; i++)
                {

                    string firstofField = Properties[i].Name.ToString().Substring(0, 1);
                    string table_field = string.Empty;

                    if (firstofField == "_")
                    {
                        table_field = Properties[i].Name.ToString().Substring(1);
                    }
                    else
                    {
                        table_field = Properties[i].Name.ToString();
                    }


                    Select_syntax += alliance + table_field + ",";

                }


                //from 구문 만들기 - 테이블 명 가져오기 
                From_syntax += tablename.Key + ",";
            }
            Select_syntax = Select_syntax.Remove(Select_syntax.LastIndexOf(","));
            From_syntax = From_syntax.Remove(From_syntax.LastIndexOf(",")); // 마지막 ,문자 없애기

            sSql = Select_syntax + From_syntax + " " + WhereCondition;
            DataSet oDS = oDB.QueryDataSet(sSql);
            DataTable oDT = oDS.Tables[0];

            int k3 = 0; // 초기 컬럼 스타트 지점
            foreach (KeyValuePair<string, dynamic> tablename in Table_entity)
            {
                List<dynamic> result = new List<dynamic>();
                Type Class = tablename.Value.GetType(); //객체의 타입 가져오기
                PropertyInfo[] Properties = Class.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                int k2 = k3; //한 객체 row 칼럼 완전 iteration 돌고 난 다음 컬럼 스타트 지점
                for (int i = 0; i < oDT.Rows.Count; i++)
                {
                    int k1 = k2; // 객체 row 한번 iteration 돌고 난 다음 컬럼 스타트
                    for (int j = 0; j < Properties.Length; j++)
                    {
                        //객체의 멤버에 db필드값 지정
                        string member = oDT.Rows[i][k1].ToString();
                        Properties[j].SetValue(tablename.Value, member, null);
                        k1++;
                    }
                    k3 = k1; // 진행된 칼럼 순서 저장 

                    dynamic copyObject = tablename.Value.Copy();

                    result.Add(copyObject);

                }
                Result.Add(result);

            }

            oDB.Close();

            //todo
        }
        public void Load(int NumofPage, int NumofDatas)
        {
            string Page = NumofPage.ToString();
            string NumofData = NumofDatas.ToString();

            //2. start to write query 
            sSql = string.Empty;
            string Select_syntax = "select ";
            string From_syntax = " from ";

            //3. select 컬럼1,컬럼2,컬럼3 .... 만들기 
            foreach (KeyValuePair<string, dynamic> tablename in Table_entity)
            {
                Type Class = tablename.Value.GetType(); //객체의 타입 가져오기
                string alliance1 = "";       //ex abc
                string alliance2 = "";      //ex abc. 점이 있는 경우

                string tableName = tablename.Key.TrimEnd();
                if (tableName.Contains(" "))
                {
                    string[] name = tableName.Split(' ');
                    alliance1 = name[name.Length - 1];
                    alliance2 = alliance1 + ".";
                }

                if (alliance1 != "") // 테이블이 2개 이상일 경우
                {
                    alliance1 = " as " + alliance1;

                    //select 구문 만들기 - 객체의 멤버 가져오기
                    PropertyInfo[] Properties = Class.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                    for (int i = 0; i < Properties.Length; i++)
                    {
                        string firstofField = Properties[i].Name.ToString().Substring(0, 1);

                        string table_field = string.Empty;

                        if (firstofField == "_")
                        {
                            table_field = Properties[i].Name.ToString().Substring(1);

                        }
                        else
                        {
                            table_field = Properties[i].Name.ToString();

                        }


                        Select_syntax += alliance2 + table_field + alliance1 + table_field + ",";

                    }
                }
                else  // 테이블이 1개 일 경우
                {
                    //select 구문 만들기 - 객체의 멤버 가져오기
                    PropertyInfo[] Properties = Class.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                    for (int i = 0; i < Properties.Length; i++)
                    {
                        string firstofField = Properties[i].Name.ToString().Substring(0, 1);

                        string table_field = string.Empty;

                        if (firstofField == "_")
                        {
                            table_field = Properties[i].Name.ToString().Substring(1);

                        }
                        else
                        {
                            table_field = Properties[i].Name.ToString();

                        }

                        Select_syntax += table_field + ",";
                    }


                }


                //from 구문 만들기 - 테이블 명 가져오기 
                From_syntax += tablename.Key + ",";
            }
            Select_syntax = Select_syntax.Remove(Select_syntax.LastIndexOf(","));
            From_syntax = From_syntax.Remove(From_syntax.LastIndexOf(",")); // 마지막 ,문자 없애기

            // paging query 만들기 시작
            string front_paging_query = "SELECT * FROM (";
            front_paging_query += " SELECT ROWNUM AS RNUM, QUERY.* FROM (";


            sSql = Select_syntax + From_syntax + " " + WhereCondition;

            string rear_paging_query = ")  QUERY";
            rear_paging_query += " WHERE ROWNUM <=(" + Page + "*" + NumofData + ")";
            rear_paging_query += ")";
            rear_paging_query += "WHERE RNUM>=((" + Page + "-1)*" + NumofData + ")";

            //paging query 완성
            sSql = front_paging_query + sSql + rear_paging_query;

            DataSet oDS = oDB.QueryDataSet(sSql);
            DataTable oDT = oDS.Tables[0];

            int k3 = 1; // 초기 컬럼 스타트 지점
            foreach (KeyValuePair<string, dynamic> tablename in Table_entity)
            {
                List<dynamic> result = new List<dynamic>();
                Type Class = tablename.Value.GetType(); //객체의 타입 가져오기
                PropertyInfo[] Properties = Class.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                int k2 = k3; //한 객체 row 칼럼 완전 iteration 돌고 난 다음 컬럼 스타트 지점
                for (int i = 0; i < oDT.Rows.Count; i++)
                {
                    int k1 = k2; // 객체 row 한번 iteration 돌고 난 다음 컬럼 스타트
                    for (int j = 0; j < Properties.Length; j++)
                    {
                        //객체의 멤버에 db필드값 지정
                        string member = oDT.Rows[i][k1].ToString();
                        Properties[j].SetValue(tablename.Value, member, null);
                        k1++;
                    }
                    k3 = k1; // 진행된 칼럼 순서 저장 

                    dynamic copyObject = tablename.Value.Copy();
                    result.Add(copyObject);

                }
                Result.Add(result);

            }

            oDB.Close();

        }
        public string Delete(string table_name, string WhereCondition)
        {

            //insert into table명 (컬럼1,컬럽2,...)
            sSql = "Delete from ";
            sSql += table_name + " ";
            sSql += WhereCondition;

            //sql 구문 만들기 끝

            return sSql;

        }
        //sysdate로 입력된 값 수정할 때
        public string To_date(string date)
        {
            string todate = "to_date('" + date + "','YYYY-MM-DD AM HH:MI:SS')";

            return todate;
        }


        public void CopyObject(object source, object destination)
        {
            var props = source.GetType().GetProperties();

            foreach (var prop in props)
            {
                PropertyInfo info = destination.GetType().GetProperty(prop.Name);
                if (info != null)
                {
                    info.SetValue(destination, prop.GetValue(source, null), null);
                }
            }
        }
        public List<T> GetDistinctValues<T>(List<T> list)
        {
            List<T> tmp = new List<T>();
            for (int i = 0; i < list.Count; i++)
            {
                if (tmp.Contains(list[i]))

                    continue;
                tmp.Add(list[i]);
            }
            return tmp;
        }

        public List<TO_TYPE> AddRange<FROM_TYPE, TO_TYPE>(List<FROM_TYPE> listToCopyFrom, List<TO_TYPE> listToCopyTo) where FROM_TYPE : TO_TYPE
        {

            // loop through the list to copy, and  
            foreach (FROM_TYPE item in listToCopyFrom)
            {
                // add items to the copy tolist  
                listToCopyTo.Add(item);
            }

            // return the copy to list  
            return listToCopyTo;
        }
    }

}
