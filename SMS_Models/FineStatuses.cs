using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_Models
{
    class FineStatuses
    {
        private static string Command;
        private static Database_Connection db = new Database_Connection();
        public int Id { get; set; }
        public string FineStatusName { get; set; }
        public override string ToString()
        {
            return "[ id: " + Id + ", finestatusname : " + FineStatusName + " ]";
        }
        public static List<FineStatuses> ListOfFineStatuses
        {
            get
            {
                return _GetListOfFineStatuses();
            }
            set
            {

            }
        }

        private static List<FineStatuses> _GetListOfFineStatuses()
        {
            List<FineStatuses> FineStatuses = new List<FineStatuses>();
            try
            {
                Command = @"select * from tblFineStatuses";
                db.cmd.CommandText = Command;
                db.con.Open();

                SqlDataReader rdr = db.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    FineStatuses singleFineStatuses = new FineStatuses();

                    singleFineStatuses.Id = (int)rdr[0];
                    singleFineStatuses.FineStatusName = rdr[1].ToString();




                    FineStatuses.Add(singleFineStatuses);
                }
            }
            catch (SqlException sqlex)
            {
                SqlExceptionErrorHandling rh = new SqlExceptionErrorHandling();
                rh.GetError(sqlex);
            }
            finally
            {
                db.con.Close();
            }

            return FineStatuses;
        }
        public static int Add(FineStatuses finestatuses)
        {
            int retvalue = -1;
            try
            {
      db.cmd.CommandType = CommandType.StoredProcedure;
            db.cmd.CommandText = "procFineStatuses_FineStatuses";
            db.cmd.Parameters.AddWithValue("@FineStatusName", finestatuses.FineStatusName);
            db.cmd.Parameters.Add("@Id", SqlDbType.Int);
            db.cmd.Parameters["@Id"].Direction = ParameterDirection.Output;
            db.con.Open();
            db.cmd.ExecuteNonQuery();
            retvalue= Convert.ToInt32(db.cmd.Parameters["@Id"].Value);

            }

            catch (SqlException sqlex)
            {
                SqlExceptionErrorHandling rh = new SqlExceptionErrorHandling();
                rh.GetError(sqlex);
            }
            finally
            {
                db.CloseDb(db.con, db.cmd);
            }

            return retvalue;
      

        }
        public static void Update(int id, FineStatuses finestatuses)
        {
            try
            {
    db.cmd.CommandText = "update tblFineStatuses set FineStatusName = @FineStatusName where Id=@id";
            db.cmd.Parameters.AddWithValue("@FineStatusname", finestatuses.FineStatusName);
            db.cmd.Parameters.AddWithValue("@id", id);
            db.con.Open();
            db.cmd.ExecuteNonQuery();

            }

            catch (SqlException sqlex)
            {
                SqlExceptionErrorHandling rh = new SqlExceptionErrorHandling();
                rh.GetError(sqlex);
            }
            finally
            {
                db.CloseDb(db.con, db.cmd);
            }

        
        }
        public static void Delete(int id)
        {
            try
            {
   db.cmd.CommandText = "delete from tblFineStatuses where Id=@id";
            db.cmd.Parameters.AddWithValue("@id", id);
            db.con.Open();
            db.cmd.ExecuteNonQuery();

            }

            catch (SqlException sqlex)
            {
                SqlExceptionErrorHandling rh = new SqlExceptionErrorHandling();
                rh.GetError(sqlex);
            }
            finally
            {
                db.CloseDb(db.con, db.cmd);
            }


        }
    }
}
