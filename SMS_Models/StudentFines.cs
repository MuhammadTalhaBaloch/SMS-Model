using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_Models
{
    class StudentFines
    {
        private static string Command;
        private static Database_Connection db = new Database_Connection();
        public int Id { get; set; }
        public float Fine { get; set; }
        public DateTime DateTime { get; set; }
        public string Note { get; set; }
        public int StudentsId { get; set; }
        public int AddedByEmployeesId { get; set; }
        public int FineStatusesId { get; set; }
        public Students Students { get; set; }
        public FineStatuses FineStatus { get; set; }
        public Employees AddedByEmployees { get; set; }
        public override string ToString()
        {
            return "{ [ id: " + Id + ", fine: " + Fine + ", datetime: " + DateTime + ", note: " + Note
                        + ", addedbyemployeesid: " + AddedByEmployeesId + ", studentsid: " + StudentsId + ", feestatusesid: " + FineStatusesId + "  ]"
                        + Students + FineStatus + AddedByEmployees + " }";
        }
        public static List<StudentFines> ListOfStudentFines
        {
            get
            {
                return _GetStudentFines();
            }
            private set
            {

            }
        }
        public StudentFines()
        {
            Students = new Students();
            FineStatus = new FineStatuses();
            AddedByEmployees = new Employees();
        }
        private static List<StudentFines> _GetStudentFines()
        {
            List<StudentFines> StudentFines = new List<StudentFines>();


            try
            {
                Command = "select * from tblStudentFines";
                db.cmd.CommandText = Command;
                db.con.Open();

                SqlDataReader rdr = db.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    StudentFines singleStudentFines = new StudentFines();
                    singleStudentFines.Id = (int)rdr[0];
                    singleStudentFines.Fine = Convert.ToSingle(rdr[1]);
                    singleStudentFines.DateTime =Convert.ToDateTime(rdr[2]);
                    singleStudentFines.Note = rdr[3].ToString();
                    singleStudentFines.StudentsId = (int)rdr[4];
                    singleStudentFines.AddedByEmployeesId = (int)rdr[5];
                    singleStudentFines.FineStatusesId = (int)rdr[6];


                    var student = new Students();
                    singleStudentFines.Students = Students.ListOfStudents.SingleOrDefault(s => s.Id == singleStudentFines.StudentsId);


                    var finestatus = new FineStatuses();
                    singleStudentFines.FineStatus = FineStatuses.ListOfFineStatuses.SingleOrDefault(f => f.Id == singleStudentFines.FineStatusesId);

                    var emp = new Employees();
                    singleStudentFines.AddedByEmployees = Employees.ListOfEmployees.SingleOrDefault(e => e.Id == singleStudentFines.AddedByEmployeesId);



                    StudentFines.Add(singleStudentFines);
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
            return StudentFines;
        }
        public static int Add(StudentFines studentFines)
        {
            int retvalue = -1;
            try
            {

                db.cmd.CommandType = CommandType.StoredProcedure;
                db.cmd.CommandText = "procStudentFines_AddStudentFines";
                db.cmd.Parameters.AddWithValue("@Fine", studentFines.Fine);
                db.cmd.Parameters.AddWithValue("@DateTime", studentFines.DateTime);
                db.cmd.Parameters.AddWithValue("@Note", studentFines.Note);
                db.cmd.Parameters.AddWithValue("@Student_ID", studentFines.StudentsId);
                db.cmd.Parameters.AddWithValue("@AddedByEmployee_ID", studentFines.AddedByEmployeesId);
                db.cmd.Parameters.AddWithValue("@FineStatus_ID", studentFines.FineStatusesId);
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
        public static int Add(StudentFines studentFines,FineStatuses fineStatuses)
        {
            int retvalue = -1;
            studentFines.FineStatusesId = FineStatuses.Add(fineStatuses);
            retvalue = StudentFines.Add(studentFines);
            return retvalue;
        }
        public static void Delete(int id)
        {
            try
            {
         db.cmd.CommandText = "delete from tblStudentFines  where Id=@id";
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
        public static void Update(int id, StudentFines studentfines)
        {
            try
            {
                db.cmd.CommandText = @"update tblStudentFines set Fine = @fine, 
                    DateTime=@datetime,
                Note=@note,
                Student_ID=@student_id,
                AddedByEmployee_ID=@addedbyemployeeid,
                FineStatus_ID=@finestatusid
                    where Id=@id";
                db.cmd.Parameters.AddWithValue("@fine", studentfines.Fine);
                db.cmd.Parameters.AddWithValue("@datetime", studentfines.DateTime);
                db.cmd.Parameters.AddWithValue("@note", studentfines.Note);
                db.cmd.Parameters.AddWithValue("@student_id", studentfines.StudentsId);
                db.cmd.Parameters.AddWithValue("@addedbyemployeeid", studentfines.AddedByEmployeesId);
                db.cmd.Parameters.AddWithValue("@finestatusid", studentfines.FineStatusesId);
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
        public static void Update(int Id, StudentFines studentfines, FineStatuses finestatus)
        {
            studentfines.FineStatusesId = FineStatuses.Add(finestatus);
            StudentFines.Update(Id, studentfines);
        }
    }
}
