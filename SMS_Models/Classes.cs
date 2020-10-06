using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
namespace SMS_Models
{
    public class Classes
    {
        private static string Command;
        private static Database_Connection  db = new Database_Connection();
        public int Id { get; set; }
        public string ClassesName { get; set; }
        public int GradesId { get; set; }
        public Grades Grades { get; set; }
        public static List<Classes> ListOfClasses
        {
            get
            {
                return _GetClasses();
            }
        }
        public override string ToString()
        {
            return "{[ id: " + Id + ", Class Name: " + ClassesName + ", Grade Id :" + GradesId + " ]" + Grades + " }";
        }
        
        private static List<Classes> _GetClasses()
        {
            List<Classes> classes = new List<Classes>();

            try
            {
                db.cmd.CommandType = CommandType.Text;
                Command = "select * from tblClasses;";
                db.cmd.CommandText = Command;
                db.con.Open();

                SqlDataReader rdr = db.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Classes singleclass = new Classes();
                    singleclass.Id = (int)rdr[0];
                    singleclass.ClassesName = rdr[1].ToString();
                    singleclass.GradesId = (int)rdr[2];
                    singleclass.Grades = Grades.ListOfGrades.SingleOrDefault(gr => gr.Id == singleclass.GradesId);
                    classes.Add(singleclass);
                }
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
            return classes;
        }
        public static int Add(Classes classes)
        {
            int retvalue = -1;
            try
            {
                db.cmd.CommandType = CommandType.StoredProcedure;
                db.cmd.CommandText = "procClasses_AddClasses";
                db.cmd.Parameters.AddWithValue("@ClassName", classes.ClassesName);
                db.cmd.Parameters.AddWithValue("@Grade_ID", classes.GradesId);
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
        public static int Add(Classes classes, Grades grades)
        {
            int retvalue = -1;
            
            classes.GradesId = Grades.Add(grades);
            retvalue = Classes.Add(classes);

            return retvalue;
        }
        public static void Delete(int id)
        {
            
            try
            {
                db.cmd.CommandType = CommandType.Text;
                db.cmd.CommandText = "delete from tblClasses  where Id=@id";
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
        public static void Update(int id, Classes classes)
        {
            try
            {
                
                db.cmd.CommandText = @" update tblClasses set ClassName = @classname, 
                    Grade_ID=@gradeid
                    where Id=@id; ";

                db.cmd.Parameters.AddWithValue("@classname", classes.ClassesName);
                db.cmd.Parameters.AddWithValue("@gradeid", classes.GradesId);
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
        public static void Update(int Id, Classes classes, Grades grades)
        {
            classes.GradesId = Grades.Add(grades);
            Classes.Update(Id, classes);
        }

    }
}