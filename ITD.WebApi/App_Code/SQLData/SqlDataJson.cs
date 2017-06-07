using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITD.WebApi.Models
{
    public class SqlDataJson : SqlData
    {
        //Instance variables
        public string[] SqlJson { get; set; }

        //constructors
        public SqlDataJson() : base() { }

        //methods
        public void FillArrayValues(string[] jsonField, string[] jsonValue)
        {
            SqlJson = new string[jsonValue.Length];

            using (conn)
            {
                conn.Open();

                for (int i = 0; i < jsonValue.Length; ++i)
                {
                    base.newAdapter(jsonValue[i]);

                    using (sql)
                    {
                        try
                        {
                            
                            SqlJson[i] = "'" + jsonField[i] + "':'" + sql.SelectCommand.ExecuteScalar() + "'";
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                    sql.Dispose();
                }
            }
        }

        public override bool hasData()
        {
            throw new NotImplementedException();
        }
    }
}