using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_Models
{
    class EmployeeFines
    {
        private static string Command;
        private static Database_Connection db = new Database_Connection();
        public int Id { get; set; }
        public float Fine { get; set; }
        public string DateTime { get; set; }
        public string Note { get; set; }
        public int AddedByEmployeesId { get; set; }
        public int EmployeesId { get; set; }
        public int FineStatusesId { get; set; }
        public Employees Employees { get; set; }
        public Employees AddedByEmployees { get; set; }
        public FineStatuses FineStatuses { get; set; }
        public override string ToString()
        {
            return "{ [ id: " + Id + ", fine: " + Fine + ", datetime: " + DateTime + ", paid: " + Note
                        + ", addedbyemployeesid: " + AddedByEmployeesId + ", studentsid: " + EmployeesId + ", feeschedulesid: " + FineStatusesId + "  ]"
                        + Employees + AddedByEmployees + FineStatuses + "}";
        }
        public static List<EmployeeFines> ListOfEmployeeFines
        {
            get
            {
                return _GetEmployeeFines();
            }
            private set
            {

            }
        }
        //public EmployeeFines()
        //{
        //    Employees = new Employees();
        //    AddedByEmployees = new Employees();
        //}
        private static List<EmployeeFines> _GetEmployeeFines()
        {
            List<EmployeeFines> EmployeeFines = new List<EmployeeFines>();


            try
            {
                Command = "select * from tblEmployeeFines";
                db.cmd.CommandText = Command;
                db.con.Open();

                SqlDataReader rdr = db.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    EmployeeFines singleEmployeeFines = new EmployeeFines();
                    singleEmployeeFines.Id = (int)rdr[0];
                    singleEmployeeFines.Fine = Convert.ToSingle(rdr[1]);
                    singleEmployeeFines.DateTime = rdr[2].ToString();
                    singleEmployeeFines.Note= rdr[3].ToString();
                    singleEmployeeFines.AddedByEmployeesId= (int)rdr[4];
                    singleEmployeeFines.EmployeesId= (int)rdr[5];
                    singleEmployeeFines.FineStatusesId= (int)rdr[6];


                    var emp = new Employees();
                    singleEmployeeFines.Employees = Employees.ListOfEmployees.SingleOrDefault(e => e.Id == singleEmployeeFines.EmployeesId);

                    var addedbyemp = new Employees();
                    singleEmployeeFines.Employees = Employees.ListOfEmployees.SingleOrDefault(e => e.Id == singleEmployeeFines.AddedByEmployeesId);

                    singleEmployeeFines.FineStatuses = FineStatuses.ListOfFineStatuses.SingleOrDefault(f => f.Id == singleEmployeeFines.FineStatusesId);

                    EmployeeFines.Add(singleEmployeeFines);
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
            return EmployeeFines;
        }
        public static int Add(EmployeeFines employeeFines)
        {
            int retvalue = -1;
            try
            {

                db.cmd.CommandType = CommandType.StoredProcedure;
                db.cmd.CommandText = "procEmployeeFines_AddEmployeeFines";
                db.cmd.Parameters.AddWithValue("@Fine", employeeFines.Fine);
                db.cmd.Parameters.AddWithValue("@DateTime", employeeFines.DateTime);
                db.cmd.Parameters.AddWithValue("@Note", employeeFines.Note);
                db.cmd.Parameters.AddWithValue("@AddedByEmployee_ID", employeeFines.AddedByEmployeesId);
                db.cmd.Parameters.AddWithValue("@Employee_ID", employeeFines.EmployeesId);
                db.cmd.Parameters.AddWithValue("@FineStatus_ID", employeeFines.FineStatusesId);
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
        public static int Add(EmployeeFines employeeFines, FineStatuses fineStatuses)
        {
            int retvalue = -1;
            employeeFines.FineStatusesId = FineStatuses.Add(fineStatuses);
            retvalue = EmployeeFines.Add(employeeFines);
            return retvalue;
        }
        public static float FineAmount(int employeesId, int Month)
        {
            float emptotal=0;
            var empfine = new EmployeeFines();
            var empfinedata = EmployeeFines.ListOfEmployeeFines.
                                            FindAll(e => e.EmployeesId == employeesId &&
                                                   Convert.ToDateTime(e.DateTime).Month == Month).
                                                   Select(e=>e.Fine);
           emptotal= empfinedata.Sum();

            return emptotal;
        }

        public static void Delete(int id)
        {
            try
            {

   db.cmd.CommandText = "delete from tblEmployeeFines  where Id=@id";
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
        public static void Update(int id, EmployeeFines empfine)
        {
            try
            {
                db.cmd.CommandText = @"update tblEmployeeFines set Fine=@fn,DateTime=@dt,Note=@nt,
                                     AddedByEmployee_ID=@abid,Employee_ID=@empid,FineStatus_ID=@fsid where Id=@id; ";

                db.cmd.Parameters.AddWithValue("@fn", empfine.Fine);
                db.cmd.Parameters.AddWithValue("@dt", empfine.DateTime);
                db.cmd.Parameters.AddWithValue("@nt", empfine.Note);
                db.cmd.Parameters.AddWithValue("@abid", empfine.AddedByEmployeesId);
                db.cmd.Parameters.AddWithValue("@empid", empfine.EmployeesId);
                db.cmd.Parameters.AddWithValue("@fsid", empfine.FineStatusesId);
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
        public static void Update(int Id, EmployeeFines empfine, FineStatuses finestatus)
        {
            empfine.FineStatusesId = FineStatuses.Add(finestatus);
            EmployeeFines.Update(Id, empfine);
        }
    }

}
