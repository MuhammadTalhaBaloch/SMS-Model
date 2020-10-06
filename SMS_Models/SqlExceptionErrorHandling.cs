using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_Models
{
    class SqlExceptionErrorHandling
    {
        public void GetError(SqlException ex)
        {
            string Error = "";
            if (ex.Number == 2)
            {
                Error = "Server Agent not Running";
            }
            //else if (ex.Number >= 100 && ex.Number <= 999)
            //{
            //    Error = "Error In Sql Syntex";
            //}
            //else if (ex.Number == 8114)
            //{
            //    Error = "Parameter Type MissMatch";
            //}
            //else if (ex.Number == 53)
            //{
            //    Error = "Connection string error";
            //}
            //else if (ex.Number == 2812)
            //{
            //    Error = "Procedure Not Found";
            //}
            //else if (ex.Number == 512)
            //{
            //    Error = "In SubQuery More Than One values returns";
            //}
            else
            {
                Error = ex.Message;
            }
            throw new Exception(Error,ex.InnerException);

        }
    }
}
