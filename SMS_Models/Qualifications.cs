using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_Models
{
    class Qualifications
    {
        private static string Command;
        private static Database_Connection db = new Database_Connection();
        public int Id { get; set; }
        public string QualificationName { get; set; }
        public override string ToString()
        {
            return "[ id: " + Id + ", QualificationName: " + QualificationName + " ]";
        }
        public static List<Qualifications> ListOfQualifications
        {
            get
            {
                return _GetQualifications();
            }
            private set
            {

            }
        }

        private static List<Qualifications> _GetQualifications()
        {
            List<Qualifications> Qualifications = new List<Qualifications>();


            try
            {
                Command = "select * from tblQualifications";
                db.cmd.CommandText = Command;
                db.con.Open();

                SqlDataReader rdr = db.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Qualifications singleQualifications = new Qualifications();
                    singleQualifications.Id = (int)rdr[0];
                    singleQualifications.QualificationName = rdr[1].ToString();


                    Qualifications.Add(singleQualifications);
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
            return Qualifications;
        }
        public static int Add(Qualifications qualifications)
        {
            int retvalue = -1;
            try
            {
    db.cmd.CommandType = CommandType.StoredProcedure;
            db.cmd.CommandText = "procQualifications_AddQualifications ";
            db.cmd.Parameters.AddWithValue("@QualificationName", qualifications.QualificationName);
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
        public static void Update(int id, Qualifications qualifications)
        {
            try
            {
    db.cmd.CommandText = "update tblQualifications set QualificationName = @QualificationName where Id=@id";
            db.cmd.Parameters.AddWithValue("@Qualificationname", qualifications.QualificationName);
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
           db.cmd.CommandText = "delete from tblQualifications  where Id=@id";
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
