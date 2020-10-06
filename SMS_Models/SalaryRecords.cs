using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_Models
{
    class SalaryRecords
    {
        private static string Command;
        private static Database_Connection db = new Database_Connection();
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public bool Paid { get; set; }
        public decimal PayableAmount { get; set; }
        public int EmployeesId { get; set; }
        public int AddedByEmployesId { get; set; }
        public Employees Employees { get; set; }
        public Employees AddedByEmployees { get; set; }
        public override string ToString()
        {
            return "{ [ id: " + Id + ", dateandtime: " + DateTime + ", paidornot: " + Paid
                        + ", payableamount: " + PayableAmount + ", employeeid: " + EmployeesId + ", addedbyemployeeid: " + AddedByEmployesId + "  ]"
                        + Employees + AddedByEmployees + "}";
        }

        public static List<SalaryRecords> ListOfSalaryRecords
        {
            get
            {
                return _GetSalaryRecords();
            }
            private set
            {

            }
        }
        public SalaryRecords()
        {
            Employees = new Employees();
            AddedByEmployees = new Employees();
        }
        private static List<SalaryRecords> _GetSalaryRecords()
        {
            List<SalaryRecords> SalaryRecords = new List<SalaryRecords>();


            try
            {
                Command = "select * from tblSalaryRecords";
                db.cmd.CommandText = Command;
                db.con.Open();

                SqlDataReader rdr = db.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    SalaryRecords singleSalaryRecords = new SalaryRecords();
                    singleSalaryRecords.Id = (int)rdr[0];
                    singleSalaryRecords.DateTime = Convert.ToDateTime( rdr[1]);
                    singleSalaryRecords.Paid= Convert.ToBoolean(rdr[2]);
                    singleSalaryRecords.PayableAmount = Convert.ToDecimal(rdr[3]);
                    singleSalaryRecords.EmployeesId= (int)rdr[4];
                    singleSalaryRecords.AddedByEmployesId= (int)rdr[5];

                    var emp = new Employees();
                    singleSalaryRecords.Employees = Employees.ListOfEmployees.SingleOrDefault(e => e.Id == singleSalaryRecords.EmployeesId);

                    var addedbyemp = new Employees();
                    singleSalaryRecords.AddedByEmployees = Employees.ListOfEmployees.SingleOrDefault(e => e.Id == singleSalaryRecords.AddedByEmployesId);


                    SalaryRecords.Add(singleSalaryRecords);
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
            return SalaryRecords;
        }
        public static int Add(SalaryRecords salaryRecords)
        {
            int retvalue = -1;
            try
            {
                int month = Convert.ToDateTime(salaryRecords.DateTime).Month;
                Employees emp1 = new Employees();
                
                EmployeeAttendanceRecords empr1 = new EmployeeAttendanceRecords();
                EmployeeFines empf1 = new EmployeeFines();

                salaryRecords.PayableAmount = Convert.ToDecimal(Employees.ListOfEmployees.SingleOrDefault(e => e.Id == salaryRecords.EmployeesId).Salary - EmployeeAttendanceRecords.AmountDetection(salaryRecords.EmployeesId, month) - EmployeeFines.FineAmount(salaryRecords.EmployeesId, month));

                db.cmd.CommandType = CommandType.StoredProcedure;
                db.cmd.CommandText = "procSalaryRecords_AddSalaryRecords";
                db.cmd.Parameters.AddWithValue("@DateTime", salaryRecords.DateTime);
                db.cmd.Parameters.AddWithValue("@Paid", salaryRecords.Paid);
                db.cmd.Parameters.AddWithValue("@PayableAmount", salaryRecords.PayableAmount);
                db.cmd.Parameters.AddWithValue("@Employee_ID", salaryRecords.EmployeesId);
                db.cmd.Parameters.AddWithValue("@AddedByEmployee_ID", salaryRecords.AddedByEmployesId);
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
      db.cmd.CommandText = "delete from tblSalaryRecords  where Id=@id";
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
        public static void Update(int id, SalaryRecords salaryrecords)
        {
            try
            {
                db.cmd.CommandText = @"update tblSalaryRecords set DateTime = @datetime, 
                    Paid = @paid,
					PayableAmount = @Payableamount,
					Employee_ID =@empid,
					AddedByEmployee_ID = @addedbyempid
                    where Id = @id; ";
                db.cmd.Parameters.AddWithValue("@datetime", salaryrecords.DateTime);
                db.cmd.Parameters.AddWithValue("@paid", salaryrecords.Paid);
                db.cmd.Parameters.AddWithValue("@Payableamount", salaryrecords.PayableAmount);
                db.cmd.Parameters.AddWithValue("@empid", salaryrecords.EmployeesId);
                db.cmd.Parameters.AddWithValue("@addedbyempid", salaryrecords.AddedByEmployesId);
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
