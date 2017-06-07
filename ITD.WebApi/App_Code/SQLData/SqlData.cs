using System.Configuration;
using System.Data.SqlClient;

namespace ITD.WebApi.Models
{
    public abstract class SqlData
    {
        protected SqlConnection conn { get; set; }
        protected SqlDataAdapter sql { get; set; }

        private const string connString = "TL_Connection_String";

        //default constructor
        public SqlData()
        {
            this.conn = new SqlConnection(ConfigurationManager.ConnectionStrings[connString].ConnectionString);
        }

        protected void newAdapter(string str)
        {
            this.sql = new SqlDataAdapter(str, conn);
        }

        //methods
        public abstract bool hasData();
    }
}