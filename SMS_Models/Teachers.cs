using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_Models
{
    class Teachers
    {
        private static string Command;
        private static Database_Connection db = new Database_Connection();
        public int Id { get; set; }
        public int EmployeesId { get; set; }
        public Employees Employees { get; set; }
        public override string ToString()
        {
            return "{ [ id: " + Id + ", employeeid: " + EmployeesId + "  ]"
                        + Employees + "}";
        }

        public static List<Teachers> ListOfTeachers
        {
            get
            {
                return _GetListOfTeachers();
            }
            set
            {

            }
        }
        public Teachers()
        {
            Employees = new Employees();
        }
        private static List<Teachers> _GetListOfTeachers()
        {
            List<Teachers> teachers = new List<Teachers>();
            try
            {
                Command = @"   select * from tblTeachers ts join tblEmployees emp
                                    on ts.Employee_ID=emp.Id";
                db.cmd.CommandText = Command;
                db.con.Open();

                SqlDataReader rdr = db.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Teachers singleTeacher = new Teachers();
                    singleTeacher.Id = (int)rdr[0];
                    singleTeacher.EmployeesId = (int)rdr[1];

                    var emp = new Employees();
                    singleTeacher.Employees = Employees.ListOfEmployees.SingleOrDefault(e => e.Id == singleTeacher.EmployeesId);

                    teachers.Add(singleTeacher);
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

            return teachers;

        }

        public static int Add(Teachers teachers)
        {
            int retvalue = -1;
            try
            {

                db.cmd.CommandType = CommandType.StoredProcedure;
                db.cmd.CommandText = "procTeachers_AddTeachers";
                db.cmd.Parameters.AddWithValue("@Employee_ID", teachers.EmployeesId);
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
         db.cmd.CommandText = "delete from tblTeachers  where Id=@id";
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
