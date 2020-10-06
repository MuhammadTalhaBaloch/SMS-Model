using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_Models
{
    class Subjects
    {
        private static string Command;
        private static Database_Connection db = new Database_Connection();
        public int Id { get; set; }
        public string SubjectName { get; set; }
        public int TeachersId { get; set; }
        public int ClassesId { get; set; }
        public Teachers Teachers { get; set; }
        public Classes Classes { get; set; }
        public override string ToString()
        {
            return "{ [ id: " + Id + ", subjectname: " + SubjectName + ", teacherid: " + TeachersId
                        + ", classesid: " + ClassesId + "  ]"
                        + Classes + Teachers + "}";
        }
        public static List<Subjects> ListOfSubjects
        {
            get
            {
                return _GetListOfSubjects();
            }
            set
            {

            }
        }

        public Subjects()
        {
            Teachers = new Teachers();
            Classes = new Classes();
        }

        private static List<Subjects> _GetListOfSubjects()
        {
            List<Subjects> Subjects = new List<Subjects>();
            try
            {
                Command = @"select * from tblSubjects sub join tblTeachers ts
                                on sub.Teacher_ID=ts.Id join tblClasses cls
                                on sub.Class_ID=cls.Id";
                db.cmd.CommandText = Command;
                db.con.Open();

                SqlDataReader rdr = db.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Subjects singleSubjects = new Subjects();

                    singleSubjects.Id = (int)rdr[0];
                    singleSubjects.SubjectName = rdr[1].ToString();
                    singleSubjects.TeachersId= (int)rdr[2];
                    singleSubjects.ClassesId = (int)rdr[3];

                    var teachers = new Teachers();
                    singleSubjects.Teachers = Teachers.ListOfTeachers.SingleOrDefault(t => t.Id == singleSubjects.TeachersId);
                    //singleSubjects.Teachers.Id = (int)rdr[4];
                    //singleSubjects.Teachers.EmployeesId = (int)rdr[5];

                    var classes = new Classes();
                    singleSubjects.Classes = Classes.ListOfClasses.SingleOrDefault(cs => cs.Id == singleSubjects.ClassesId);
                    //singleSubjects.Classes.Id = (int)rdr[6];
                    //singleSubjects.Classes.ClassesName = rdr[7].ToString();
                    //singleSubjects.Classes.GradesId = (int)rdr[8];



                    Subjects.Add(singleSubjects);
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

            return Subjects;
        }
        public static int Add(Subjects subjects)
        {
            int retvalue = -1;
            try
            {

                db.cmd.CommandType = CommandType.StoredProcedure;
                db.cmd.CommandText = "procSubjects_AddSubjects";
                db.cmd.Parameters.AddWithValue("@SubjectName", subjects.SubjectName);
                db.cmd.Parameters.AddWithValue("@Teacher_ID", subjects.TeachersId);
                db.cmd.Parameters.AddWithValue("@Class_ID", subjects.ClassesId);
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

        public static void Delete(int id)
        {
            try
            {
      db.cmd.CommandText = "delete from tblSubjects  where Id=@id";
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
        public static void Update(int id, Subjects subjects)
        {
            try
            {
                db.cmd.CommandText = @" update tblSubjects set SubjectName = @subjectname, 
                    Teacher_ID=@teacherid,
					Class_ID=@classid
                    where Id=@id; ";

                db.cmd.Parameters.AddWithValue("@subjectname", subjects.SubjectName);
                db.cmd.Parameters.AddWithValue("@teacherid", subjects.TeachersId);
                db.cmd.Parameters.AddWithValue("@classid", subjects.ClassesId);
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
        public static void Update(int Id, Subjects subjects, Classes classes)
        {
            subjects.ClassesId = Classes.Add(classes);
            Subjects.Update(Id, subjects);
        }

    }
}
