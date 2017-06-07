using System;

namespace ITD.WebApi.Models
{
    public class SqlDataString : SqlData
    {
        //Instance variables
        private string sqlString { get; set; }

        //constructors
        public SqlDataString(string sqlstring) : base()
        {
            sqlString = sqlstring;
        }

        //methods
        public override string ToString()
        {
            using (conn)
            {
                conn.Open();
                base.newAdapter(sqlString);

                using (sql)
                {
                    try
                    {
                        sqlString = sql.SelectCommand.ExecuteScalar().ToString();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

            return sqlString;
        }

        public override bool hasData()
        {
            if (sqlString != null)
            {
                return true;
            }
            return false;
        }

    }
}