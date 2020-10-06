using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_Models
{
    class StudentDocuments
    {
        private static string Command;
        private static Database_Connection db = new Database_Connection();
        public override string ToString()
        {
            return "{ [ id: " + Id + ", studentid: " + StudentsId + ", DocumentUrlsId: " + DocumentUrls_Id + "  ]"
                        + Students + DocumentUrls + "}";
        }

        public int Id { get; set; }
        public int StudentsId { get; set; }
        public int DocumentUrls_Id { get; set; }
        public Students Students { get; set; }
        public DocumentUrls DocumentUrls { get; set; }
        public static List<StudentDocuments> ListOfStudentDocuments
        {
            get
            {
                return _GetStudentDocuments();
            }
            private set
            {

            }
        }
        public StudentDocuments()
        {
            Students = new Students();
            DocumentUrls = new DocumentUrls();
        }
        private static List<StudentDocuments> _GetStudentDocuments()
        {
            List<StudentDocuments> StudentDocuments = new List<StudentDocuments>();


            try
            {
                Command = "select * from tblStudentDocuments";
                db.cmd.CommandText = Command;
                db.con.Open();

                SqlDataReader rdr = db.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    StudentDocuments singleStudentDocuments = new StudentDocuments();
                    singleStudentDocuments.Id = (int)rdr[0];
                    singleStudentDocuments.StudentsId = (int)rdr[1];
                    singleStudentDocuments.DocumentUrls_Id= (int)rdr[2];
                    singleStudentDocuments.Students = Students.
                                                      ListOfStudents.
                                                      SingleOrDefault(s => s.Id == singleStudentDocuments.StudentsId);
                    singleStudentDocuments.DocumentUrls = DocumentUrls.
                                                          ListOfDocumentUrls.
                                                          SingleOrDefault(u => u.Id == singleStudentDocuments.DocumentUrls_Id);
                    StudentDocuments.Add(singleStudentDocuments);
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
            return StudentDocuments;
        }
        public static int Add(StudentDocuments studentDocuments)
        {
            int retvalue = -1;
            try
            {


                studentDocuments.DocumentUrls_Id= DocumentUrls.Add(studentDocuments.DocumentUrls);
                


                db.cmd.CommandType = CommandType.StoredProcedure;
                db.cmd.CommandText = "procStudentDocuments_AddStudentDocuments";
                db.cmd.Parameters.AddWithValue("@Student_ID", studentDocuments.StudentsId);
                db.cmd.Parameters.AddWithValue("@DocumentUrl_ID", studentDocuments.DocumentUrls_Id);
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
        public static int Add(StudentDocuments studentDocuments,DocumentUrls documentsUrl)
        {
            int retvalue = -1;

            studentDocuments.DocumentUrls_Id = DocumentUrls.Add(documentsUrl);
            retvalue = StudentDocuments.Add(studentDocuments);
            return retvalue;

        }

        public static void Delete(int id)
        {
            try
            {

    db.cmd.CommandText = "delete from tblStudentDocuments  where Id=@id";
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
        public static void Update(int id, StudentDocuments studoc)
        {
            try
            {
                db.cmd.CommandText = @"   update tblStudentDocuments set Student_ID = @studentid, 
                   DocumentUrl_ID=@docurlid
                    where Id=@id;";

                db.cmd.Parameters.AddWithValue("@studentid", studoc.StudentsId);
                db.cmd.Parameters.AddWithValue("@docurlid", studoc.DocumentUrls_Id);
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
