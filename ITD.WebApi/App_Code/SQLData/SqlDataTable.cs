using System;
using System.Data;

namespace ITD.WebApi.Models
{
    public class SqlDataTable : SqlData
    {
        //Instance variables
        public DataTable sqlTable { get; set; }

        //constructors
        public SqlDataTable() : base() {
            this.sqlTable = new DataTable();
        }

        //methods
        public void FillTableValues(string sqlSelectString)
        {
            using (conn)
            {
                conn.Open();
                base.newAdapter(sqlSelectString);

                using (sql)
                {
                    try
                    {
                        sql.Fill(sqlTable);
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
        }

        public override bool hasData()
        {
            if (sqlTable.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }
    }
}