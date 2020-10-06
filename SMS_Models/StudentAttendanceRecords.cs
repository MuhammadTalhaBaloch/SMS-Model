using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_Models
{
    class StudentAttendanceRecords
    {
        private static string Command;
        private static Database_Connection db = new Database_Connection();
        public int Id { get; set; }
        public string Date { get; set; }
        public int StudentsId { get; set; }
        public int StudentAttendanceStatusesId { get; set; }
        public Students Students { get; set; }
        public StudentAttendanceStatuses StudentAttendanceStatuses { get; set; }

        public override string ToString()
        {
            return "{ [ id: " + Id + ", date: " + Date + ", StudentsId: " + StudentsId
                        + ", StudentAttendanceStatusesId: " + StudentAttendanceStatusesId + "  ]"
                        + Students + StudentAttendanceStatuses + "}";
        }
        public static List<StudentAttendanceRecords> ListOfStudentAttendanceRecords
        {
            get
            {
                return _GetStudentAttendanceRecords();
            }
            private set
            {

            }
        }
        public StudentAttendanceRecords()
        {
            Students = new Students();
            StudentAttendanceStatuses = new StudentAttendanceStatuses();
        }
        private static List<StudentAttendanceRecords> _GetStudentAttendanceRecords()
        {
            List<StudentAttendanceRecords> StudentAttendanceRecords = new List<StudentAttendanceRecords>();


            try
            {
                Command = "select * from tblStudentAttendanceRecords";
                db.cmd.CommandText = Command;
                db.con.Open();

                SqlDataReader rdr = db.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    StudentAttendanceRecords singleStudentAttendanceRecords = new StudentAttendanceRecords();
                    singleStudentAttendanceRecords.Id = (int)rdr[0];
                    singleStudentAttendanceRecords.Date = rdr[1].ToString();
                    singleStudentAttendanceRecords.StudentsId = (int)rdr[2];
                    singleStudentAttendanceRecords.StudentAttendanceStatusesId = (int)rdr[3];

                    var student = new Students();
                    singleStudentAttendanceRecords.Students = Students.ListOfStudents.SingleOrDefault(s => s.Id == singleStudentAttendanceRecords.StudentsId);

                    var Sas = new StudentAttendanceStatuses();
                    singleStudentAttendanceRecords.StudentAttendanceStatuses = Sas.ListOfStudentAttendanceStatuses.SingleOrDefault(s => s.Id == singleStudentAttendanceRecords.StudentAttendanceStatusesId);

                    StudentAttendanceRecords.Add(singleStudentAttendanceRecords);
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
            return StudentAttendanceRecords;
        }
        public static int Add(StudentAttendanceRecords studentAttendanceRecords)
        {
            int retvalue = -1;
            try
            {

                db.cmd.CommandType = CommandType.StoredProcedure;
                db.cmd.CommandText = "procStudentAttendanceRecords_AddStudentAttendanceRecords";
                db.cmd.Parameters.AddWithValue("@Date", studentAttendanceRecords.Date);
                db.cmd.Parameters.AddWithValue("@Student_ID", studentAttendanceRecords.StudentsId);
                db.cmd.Parameters.AddWithValue("@StudentAttendanceStatus_ID", studentAttendanceRecords.StudentAttendanceStatusesId);
                db.cmd.Parameters.Add("@id", SqlDbType.Int);
                db.cmd.Parameters["@id"].Direction = ParameterDirection.Output;
                db.con.Open();
                db.cmd.ExecuteNonQuery();
                retvalue = Convert.ToInt32(db.cmd.Parameters["@id"].Value);

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
        public static int Add(StudentAttendanceRecords studentAttendanceRecords,StudentAttendanceStatuses studentAttendanceStatuses)
        {
            int retvalue = -1;
            studentAttendanceRecords.StudentAttendanceStatusesId = StudentAttendanceStatuses.Add(studentAttendanceStatuses);
            retvalue = StudentAttendanceRecords.Add(studentAttendanceRecords);
            return retvalue;
        }

        public static void Delete(int id)
        {
            try
            {
  db.cmd.CommandText = "delete from tblStudentAttendanceRecords  where Id=@id";
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

        public static void Update(int id, StudentAttendanceRecords stdattrecord)
        {
            try
            {
                db.cmd.CommandText = @"UPDATE [dbo].[tblStudentAttendanceRecords]
   SET [Date] =@dte
      ,[Student_ID] = @stdid
      ,[StudentAttendanceStatus_ID] = @stdstatid
 WHERE Id=id";

                db.cmd.Parameters.AddWithValue("@dte", stdattrecord.Date);
                db.cmd.Parameters.AddWithValue("@stdid", stdattrecord.StudentsId);
                db.cmd.Parameters.AddWithValue("@stdstatid", stdattrecord.StudentAttendanceStatusesId);
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
