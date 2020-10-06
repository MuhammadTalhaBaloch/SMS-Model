using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_Models
{
    public class EmployeeAttendanceStatuses
    {
        private static string Command;
        private static Database_Connection db = new Database_Connection();
        public int Id { get; set; }
        public string StatusName { get; set; }
        public override string ToString()
        {
            return "[ id: " + Id + ", Status Name: " + StatusName + " ]";
        }

        public static List<EmployeeAttendanceStatuses> ListOfEmployeeAttendanceStatuses
        {
            get
            {
                return _GetEmployeeAttendanceStatuses();
            }
            private set
            {
            }
        }

        private static List<EmployeeAttendanceStatuses> _GetEmployeeAttendanceStatuses()
        {
            List<EmployeeAttendanceStatuses> EmployeeAttendanceStatuses = new List<EmployeeAttendanceStatuses>();


            try
            {
                Command = "select * from tblEmployeeAttendanceStatuses";
                db.cmd.CommandText = Command;
                db.con.Open();

                SqlDataReader rdr = db.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    EmployeeAttendanceStatuses singleEmployeeAttendanceStatuses = new EmployeeAttendanceStatuses();
                    singleEmployeeAttendanceStatuses.Id = (int)rdr[0];
                    singleEmployeeAttendanceStatuses.StatusName = rdr[1].ToString();
                    EmployeeAttendanceStatuses.Add(singleEmployeeAttendanceStatuses);
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
            return EmployeeAttendanceStatuses;
        }

        public static int Add(EmployeeAttendanceStatuses employeeattendancestatuses)
        {
            int retvalue = -1;
            try
            {

    db.cmd.CommandType = CommandType.StoredProcedure;
            db.cmd.CommandText = "procEmployeeAttendanceStatuses_AddEmployeeAttendanceStatuses";
            db.cmd.Parameters.AddWithValue("@StatusName", employeeattendancestatuses.StatusName);
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
        public static void Update(int id, EmployeeAttendanceStatuses employeeattendancestatuses)
        {
            try
            {

  db.cmd.CommandText = "update tblEmployeeAttendanceStatuses set StatusName = @StatusName where Id=@id";
            db.cmd.Parameters.AddWithValue("@Statusname", employeeattendancestatuses.StatusName);
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

db.cmd.CommandText = "delete from tblEmployeeAttendanceStatuses where Id=@id";
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