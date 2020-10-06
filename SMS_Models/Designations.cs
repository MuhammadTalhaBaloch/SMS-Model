using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SMS_Models
{
    public class Designations
    {
        private static string Command;
        private static Database_Connection db = new Database_Connection();
        public int Id { get; set; }
        public string DesignationName { get; set; }
        public override string ToString()
        {
            return "[ id: " + Id + ", designation name : " + DesignationName + " ]";
        }
        public static List<Designations> ListOfDesignations
        {
            get
            {
                return _GetDesignations();
            }
            private set
            {
            }
        }

        private static List<Designations> _GetDesignations()
        {
            List<Designations> designations = new List<Designations>();


            try
            {
                Command = "select * from tblDesignations";
                db.cmd.CommandText = Command;
                db.con.Open();

                SqlDataReader rdr = db.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Designations singleDesignations = new Designations();
                    singleDesignations.Id = (int)rdr[0];
                    singleDesignations.DesignationName = rdr[1].ToString();
                    designations.Add(singleDesignations);
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
            return designations;
        }

        public static int Add(Designations designations)
        {
            int retvalue = -1;
            try
            {
   db.cmd.CommandType = CommandType.StoredProcedure;
            db.cmd.CommandText = "procDesignations_Designations";
            db.cmd.Parameters.AddWithValue("@DesignationName", designations.DesignationName);
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

        public static void Update(int id, Designations designations)
        {
            try
            {

    db.cmd.CommandText = "update tblDesignations set DesignationName = @DesignationName where Id=@id";
            db.cmd.Parameters.AddWithValue("@DesignationName", designations.DesignationName);
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

  db.cmd.CommandText = "delete from tblDesignations where Id=@id";
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