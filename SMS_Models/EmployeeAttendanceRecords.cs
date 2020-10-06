using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_Models
{
    class EmployeeAttendanceRecords
    {
        private static string Command;
        private static Database_Connection db = new Database_Connection();
        public int Id { get; set; }
        public string Date { get; set; }
        public int EmployeesId { get; set; }
        public int EmployeesAttendanceStatusesId { get; set; }
        public Employees Employees { get; set; }
        public override string ToString()
        {
            return "{ [ id: " + Id + ", date: " + Date + ", employeesid: " + EmployeesId
                        + ", employeesattendancestatus: " + EmployeesAttendanceStatusesId + "  ]"
                        + Employees + EmployeeAttendanceStatuses + "}";
        }

        public EmployeeAttendanceStatuses EmployeeAttendanceStatuses { get; set; }

        public static List<EmployeeAttendanceRecords> ListOfEmployeeAttendanceRecords
        {
            get
            {
                return _GetEmployeeAttendanceRecords();
            }
            private set
            {
            }
        }
        //public EmployeeAttendanceRecords()
        //{
        //    Employees = new Employees();
        //    EmployeeAttendanceStatuses = new EmployeeAttendanceStatuses();

        //}

        private static List<EmployeeAttendanceRecords> _GetEmployeeAttendanceRecords()
        {
            List<EmployeeAttendanceRecords> EmployeeAttendanceRecords = new List<EmployeeAttendanceRecords>();


            try
            {
                Command = "select * from tblEmployeeAttendanceReords";
                db.cmd.CommandText = Command;
                db.con.Open();

                SqlDataReader rdr = db.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    EmployeeAttendanceRecords singleEmployeeAttendanceRecords = new EmployeeAttendanceRecords();
                    singleEmployeeAttendanceRecords.Id = (int)rdr[0];
                    singleEmployeeAttendanceRecords.Date = rdr[1].ToString();
                    singleEmployeeAttendanceRecords.EmployeesId = (int)rdr[2];
                    singleEmployeeAttendanceRecords.EmployeesAttendanceStatusesId = (int)rdr[3];

                    var emp = new Employees();
                    singleEmployeeAttendanceRecords.Employees = Employees.ListOfEmployees.SingleOrDefault(e => e.Id == singleEmployeeAttendanceRecords.EmployeesId);

                    var empattstat = new EmployeeAttendanceStatuses();
                    singleEmployeeAttendanceRecords.EmployeeAttendanceStatuses = EmployeeAttendanceStatuses.ListOfEmployeeAttendanceStatuses.SingleOrDefault(eas => eas.Id == singleEmployeeAttendanceRecords.EmployeesAttendanceStatusesId);

                    EmployeeAttendanceRecords.Add(singleEmployeeAttendanceRecords);
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
            return EmployeeAttendanceRecords;
        }
        public static int Add(EmployeeAttendanceRecords employeeAttendanceRecords)
        {
            int retvalue = -1;
            try
            {

                db.cmd.CommandType = CommandType.StoredProcedure;
                db.cmd.CommandText = "procEmployeeAttendanceRecords_AddEmployeeAttendanceRecords";
                db.cmd.Parameters.AddWithValue("@Date", employeeAttendanceRecords.Date);
                db.cmd.Parameters.AddWithValue("@Employee_ID", employeeAttendanceRecords.EmployeesId);
                db.cmd.Parameters.AddWithValue("@EmployeeAttendanceSatus_ID", employeeAttendanceRecords.EmployeesAttendanceStatusesId);
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
        public static int Add(EmployeeAttendanceRecords employeeAttendanceRecords,EmployeeAttendanceStatuses employeeAttendanceStatuses)
        {
            int retvalue = -1;
            employeeAttendanceRecords.EmployeesAttendanceStatusesId = EmployeeAttendanceStatuses.Add(employeeAttendanceStatuses);
            retvalue = EmployeeAttendanceRecords.Add(employeeAttendanceRecords);

            return retvalue;
        }
        public static float AmountDetection(int employeesId, int Month)
        {
            var empatt = new EmployeeAttendanceRecords();
            var emp = new Employees();
            var absentRecordCount = EmployeeAttendanceRecords
                                   .ListOfEmployeeAttendanceRecords
                                   .FindAll(rdr => rdr.EmployeesId == employeesId &&
                                                   rdr.EmployeesAttendanceStatusesId==2 &&
                                                   Convert.ToDateTime(rdr.Date).Month == Month)
                                    .Count;

            var empSalary = (Employees.ListOfEmployees.SingleOrDefault(e => e.Id == employeesId)).Salary;
            var detectionAmount=0f;
            if (absentRecordCount > 3)
            {
                var detecDayscount = absentRecordCount - 3;
                float a = empSalary;
                float b = 30f;
                float c = detecDayscount;
                detectionAmount = (a / b) * c;
            }
            else
                detectionAmount = 0;
            return detectionAmount;
        }

        public static void Delete(int id)
        {
            try
            {
    db.cmd.CommandText = "delete from tblEmployeeAttendanceReords  where Id=@id";
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
        public static void Update(int id, EmployeeAttendanceRecords empattrecord)
        {
            try
            {
                db.cmd.CommandText = @"update tblEmployeeAttendanceReords set Date=@dt,
                                       Employee_ID=@eid,EmployeeAttendanceSatus_ID=@easid where Id=@id; ";

                db.cmd.Parameters.AddWithValue("@dt", empattrecord.Date);
                db.cmd.Parameters.AddWithValue("@eid", empattrecord.EmployeesId);
                db.cmd.Parameters.AddWithValue("@easid", empattrecord.EmployeesAttendanceStatusesId);
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
