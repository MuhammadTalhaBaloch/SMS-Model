using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_Models
{
    class ClassSchedules
    {
        private static string Command;
        private static Database_Connection db = new Database_Connection();

        public int Id { get; set; }
        public string WeekDay { get; set; }
        public int SubjectsId { get; set; }
        public int ClassesId { get; set; }
        public int ClassTimingsId { get; set; }
        public Subjects Subjects { get; set; }
        public Classes Classes { get; set; }
        public ClassTimings ClassTimings { get; set; }
        public override string ToString()
        {
            return "{ [ id: " + Id + ", weekdays: " + WeekDay + ", subjectsid: " + SubjectsId
                        + ", classid: " + ClassesId + ", classtimingsid: " + ClassTimingsId + "  ]"
                        + Subjects + Classes + ClassTimings + " }";
        }
        public static List<ClassSchedules> ListOfClassSchedules {
            get
            {
                return _GetListOfClassSchedules();
            }
            set
            {

            }
        }
       
        //public ClassSchedules()
        //{
        //    Subjects = new Subjects();
        //    Classes = new Classes();
        //    ClassTimings = new ClassTimings();
        //}

        private static List<ClassSchedules> _GetListOfClassSchedules()
        {
            List<ClassSchedules> classSchedules = new List<ClassSchedules>();
            try
            {
                Command = @"select * from tblClassSchedules";
                db.cmd.CommandText = Command;
                db.con.Open();

                SqlDataReader rdr = db.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    ClassSchedules singleClassSchedules = new ClassSchedules();
                    singleClassSchedules.Id = (int)rdr[0];
                    singleClassSchedules.WeekDay = rdr[1].ToString();
                    singleClassSchedules.SubjectsId= (int)rdr[2];
                    singleClassSchedules.ClassesId = (int)rdr[3];
                    singleClassSchedules.ClassTimingsId = (int)rdr[4];

                    var subject = new Subjects();
                    singleClassSchedules.Subjects = Subjects.ListOfSubjects.SingleOrDefault(sub => sub.Id == singleClassSchedules.SubjectsId);
                    var classes = new Classes();
                    singleClassSchedules.Classes = Classes.ListOfClasses.SingleOrDefault(cls => cls.Id == singleClassSchedules.ClassesId);
                    var classtiming = new ClassTimings();
                    singleClassSchedules.ClassTimings = ClassTimings.ListOfClassTimings.SingleOrDefault(ct => ct.Id == singleClassSchedules.ClassTimingsId);

                    classSchedules.Add(singleClassSchedules);
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

            return classSchedules;


        }
        public static int Add(ClassSchedules classSchedules)
        {
            int retvalue = -1;
            try
            {

                db.cmd.CommandType = CommandType.StoredProcedure;
                db.cmd.CommandText = "procClassSchedules_AddClassSchedules";
                db.cmd.Parameters.AddWithValue("@WeekDay",classSchedules.WeekDay);
                db.cmd.Parameters.AddWithValue("@Subject_ID",classSchedules.SubjectsId);
                db.cmd.Parameters.AddWithValue("@Class_ID",classSchedules.ClassesId);
                db.cmd.Parameters.AddWithValue("@ClassTiming_ID",classSchedules.ClassTimingsId);
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
  db.cmd.CommandText = "delete from tblClassSchedules  where Id=@id";
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
        public static void Update(int id, ClassSchedules classschedules)
        {
            try
            {
                db.cmd.CommandText = @"update tblClassSchedules set WeekDay = @weekday, 
                    Subject_ID = @subjectid,
                Class_ID = @classid,
                ClassTiming_ID = @classtimingid
                 where Id = @id; ";

                db.cmd.Parameters.AddWithValue("@weekday", classschedules.WeekDay);
                db.cmd.Parameters.AddWithValue("@subjectid", classschedules.SubjectsId);
                db.cmd.Parameters.AddWithValue("@classid", classschedules.ClassesId);
                db.cmd.Parameters.AddWithValue("@classtimingid", classschedules.ClassTimingsId);
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
