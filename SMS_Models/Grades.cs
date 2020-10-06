using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SMS_Models
{
    public class Grades
    {
        private static Database_Connection db = new Database_Connection();
        private static string Command;

        public int Id { get; set; }
        public string GradeName { get; set; }

        public override string ToString()
        {
            return "[ id: " + Id + ", Name: " + GradeName + " ]";
        }

        public static List<Grades> ListOfGrades {
            get
            {
                return _getListOfGrades();
            }
        }

        private static List<Grades> _getListOfGrades()
        {
            List<Grades> grades = new List<Grades>();
          
            try
            {
                Command = "select * from tblGrades;";
                db.cmd.CommandText = Command;
                db.con.Open();

                SqlDataReader rdr = db.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Grades singlegrade = new Grades();
                    singlegrade.Id = (int)rdr[0];
                    singlegrade.GradeName = rdr[1].ToString();
                    grades.Add(singlegrade);
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
            return grades;
        }
        public static int Add(Grades grades)
        {
            int retvalue = -1;
            try
            {
            db.cmd.CommandType = CommandType.StoredProcedure;
            db.cmd.CommandText = "procGrades_Grades";
            db.cmd.Parameters.AddWithValue("@GradeName", grades.GradeName);
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
        public static void Update(int id, Grades grades)
        {
            try
            {
            db.cmd.CommandText = "update tblGrades set GradeName = @GradeName where Id=@id";
            db.cmd.Parameters.AddWithValue("@GradeName", grades.GradeName);
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

            db.cmd.CommandText = "delete from tblGrades where Id=@id";
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