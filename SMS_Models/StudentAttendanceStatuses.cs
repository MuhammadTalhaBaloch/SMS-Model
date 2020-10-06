using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_Models
{
    class StudentAttendanceStatuses
    {
        private static string Command;
        private static Database_Connection db = new Database_Connection();
        public int Id { get; set; }
        public string StatusName { get; set; }
        public override string ToString()
        {
            return "[ id: " + Id + ", statusname : " + StatusName + " ]";
        }
        public List<StudentAttendanceStatuses> ListOfStudentAttendanceStatuses
        {
            get
            {
                return _GetStudentAttendanceStatuses();
            }
            private set
            {

            }
        }

        private List<StudentAttendanceStatuses> _GetStudentAttendanceStatuses()
        {
            List<StudentAttendanceStatuses> StudentAttendanceStatuses = new List<StudentAttendanceStatuses>();


            try
            {
                Command = "select * from tblStudentAttendanceStatuses";
                db.cmd.CommandText = Command;
                db.con.Open();

                SqlDataReader rdr = db.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    StudentAttendanceStatuses singleStudentAttendanceStatuses = new StudentAttendanceStatuses();
                    singleStudentAttendanceStatuses.Id = (int)rdr[0];
                    singleStudentAttendanceStatuses.StatusName = rdr[1].ToString();


                    StudentAttendanceStatuses.Add(singleStudentAttendanceStatuses);
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
            return StudentAttendanceStatuses;
        }
        public static int Add(StudentAttendanceStatuses studentattendancestatuses)
        {
            int retvalue = -1;
            try
            {

                db.cmd.CommandType = CommandType.StoredProcedure;
                db.cmd.CommandText = "procStudentAttendanceStatuses_AddStudentAttendanceStatuses";
                db.cmd.Parameters.AddWithValue("@StatusName", studentattendancestatuses.StatusName);
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
        public static void Update(int id, StudentAttendanceStatuses studentattendancestatuses)
        {
            try
            {

        db.cmd.CommandText = "update tblStudentAttendanceStatuses set StatusName = @StatusName where Id=@id";
            db.cmd.Parameters.AddWithValue("@Statusname", studentattendancestatuses.StatusName);
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
    db.cmd.CommandText = "delete from tblStudentAttendanceStatuses where Id=@id";
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
