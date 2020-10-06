using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SMS_Models
{
    public class ClassTimings
    {
        private static string Command;
        private static Database_Connection db = new Database_Connection();
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Name { get; set; }
        public override string ToString()
        {
            return "[ id: " + Id + ", startname : " + StartTime + ", endtime : " + EndTime + ", name : " + Name + " ]";
        }

        public static List<ClassTimings> ListOfClassTimings {
            get
            {
                return _GetListOfClassTimings();
            }
            
        }

        private static List<ClassTimings> _GetListOfClassTimings()
        {
            List<ClassTimings> classTimings = new List<ClassTimings>();
            try
            {
                Command = @"select * from tblClassTimings";
                db.cmd.CommandText = Command;
                db.con.Open();

                SqlDataReader rdr = db.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    ClassTimings singleClassTimings = new ClassTimings();
                    singleClassTimings.Id = (int)rdr[0];
                    singleClassTimings.StartTime =Convert.ToDateTime( rdr[1].ToString());
                    singleClassTimings.EndTime = Convert.ToDateTime( rdr[2].ToString());
                    singleClassTimings.Name = rdr[3].ToString();
                    classTimings.Add(singleClassTimings);
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

            return classTimings;
        }
        public static int Add(ClassTimings classtimings)
        {
            int retvalue = -1;
            try
            {
      db.cmd.CommandType = CommandType.StoredProcedure;
            db.cmd.CommandText = "procClassTimings_ClassTimings";
            db.cmd.Parameters.AddWithValue("@StartTime", classtimings.StartTime);
            db.cmd.Parameters.AddWithValue("@Endtime", classtimings.EndTime);
            db.cmd.Parameters.AddWithValue("@Name", classtimings.Name);
            db.cmd.Parameters.Add("@id", SqlDbType.Int);
            db.cmd.Parameters["@id"].Direction = ParameterDirection.Output;
            db.con.Open();
            db.cmd.ExecuteNonQuery();
            return Convert.ToInt32(db.cmd.Parameters["@id"].Value);

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
        public static void Update(int id, ClassTimings classtimings)
        {
            try
            {
            db.cmd.CommandText = "update tblClassTimings set StartTime = @StartTime,Endtime=@Endtime,Name=@Name where Id=@id";
            db.cmd.Parameters.AddWithValue("@StartTime", classtimings.StartTime);
            db.cmd.Parameters.AddWithValue("@Endtime", classtimings.EndTime);
            db.cmd.Parameters.AddWithValue("@Name", classtimings.Name);
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
     db.cmd.CommandText = "delete from tblClassTimings  where Id=@id";
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