using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_Models
{
    class Database_Connection
    {
        public SqlConnection con;
        public SqlCommand cmd;
        private string ConString= @"Data Source=DESKTOP-DNS4RSM;Initial Catalog=SMS;Integrated Security=True";
        public string CommandText { get; set; }
        public Database_Connection()
        {
            con = new SqlConnection(ConString);
            cmd = new SqlCommand();
            cmd.Connection = con;
        } 
        public void CloseDb(SqlConnection conn, SqlCommand cmdd)
        {
            conn.Close();
            cmdd.CommandType = CommandType.Text;
            cmdd.Parameters.Clear();
        }
       
    }
}
