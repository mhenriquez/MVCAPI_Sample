using System;

namespace ITD.WebApi.Models
{
    public class SqlDataArray : SqlData
    {
        //Instance variables
        public string[] SqlArray { get; set; }

        //constructors
        public SqlDataArray() : base() { }

        //methods
        public void FillArrayValues(string[] sqlSelectStrings)
        {
            SqlArray = new string[sqlSelectStrings.Length];

            using (conn)
            {
                conn.Open();

                for (int i = 0; i < sqlSelectStrings.Length; ++i)
                {
                    base.newAdapter(sqlSelectStrings[i]);

                    using (sql)
                    {
                        try
                        {
                            SqlArray[i] = sql.SelectCommand.ExecuteScalar().ToString();
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