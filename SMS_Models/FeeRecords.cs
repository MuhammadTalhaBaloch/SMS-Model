using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_Models
{
    class FeeRecords
    {
        private static Database_Connection db = new Database_Connection();
        private static string Command;
        public int Id { get; set; }
        public string DateTime { get; set; }
        public bool Paid { get; set; }
        public int AddedByEmployeesId { get; set; }
        public int StudentsId { get; set; }
        public int FeeSchedulesId { get; set; }
        public Employees Employee { get; set; }
        public Students Students { get; set; }
        public FeeSchedules FeeSchedules { get; set; }
        public override string ToString()
        {
            return "{ [ id: " + Id + ", datetime: " + DateTime + ", paid: " + Paid
                        + ", addedbyemployeesid: " + AddedByEmployeesId + ", studentsid: " + StudentsId + ", feeschedulesid: " + FeeSchedulesId + "  ]"
                        + Employee + Students + FeeSchedules + "}";
        }

        public static List<FeeRecords> ListOfFeeRecords
        {
            get
            {
                return _getListOfFeeRecords();
            }
            set
            {

            }
        }
        public FeeRecords()
        {
            Employee = new Employees();
            Students = new Students();
            FeeSchedules = new FeeSchedules();
        }

        private static List<FeeRecords> _getListOfFeeRecords()
        {
            List<FeeRecords> FeeRecords = new List<FeeRecords>();

            try
            {
                Command = @"select * from tblFeeRecords;";
                db.cmd.CommandText = Command;
                db.con.Open();

                SqlDataReader rdr = db.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    FeeRecords singleFeeRecords = new FeeRecords();
                    singleFeeRecords.Id = (int)rdr[0];
                    singleFeeRecords.DateTime = rdr[1].ToString();
                    singleFeeRecords.Paid=Convert.ToBoolean(rdr[2].ToString());
                    singleFeeRecords.AddedByEmployeesId= (int)rdr[3];
                    singleFeeRecords.StudentsId=(int)rdr[4];
                    singleFeeRecords.FeeSchedulesId= (int)rdr[5];

                    Students std = new Students();
                    FeeSchedules fsch = new FeeSchedules();
                    Employees emp = new Employees();
                    singleFeeRecords.Employee = Employees.ListOfEmployees.SingleOrDefault(e => e.Id == singleFeeRecords.AddedByEmployeesId);
                    singleFeeRecords.Students = Students.ListOfStudents.SingleOrDefault(s => s.Id == singleFeeRecords.StudentsId);
                    singleFeeRecords.FeeSchedules = FeeSchedules.ListOfFeeSchedules.SingleOrDefault(f => f.Id == singleFeeRecords.FeeSchedulesId);
                    FeeRecords.Add(singleFeeRecords);
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
            return FeeRecords;
        }
        public static int Add(FeeRecords feeRecords)
        {
            int retvalue = -1;
            try
            {

                db.cmd.CommandType = CommandType.StoredProcedure;
                db.cmd.CommandText = "procFeeRecords_AddFeeRecords";
                db.cmd.Parameters.AddWithValue("@DateTime", feeRecords.DateTime);
                db.cmd.Parameters.AddWithValue("@Paid", feeRecords.Paid);
                db.cmd.Parameters.AddWithValue("@AddedByEmployee_ID", feeRecords.AddedByEmployeesId);
                db.cmd.Parameters.AddWithValue("@Student_ID", feeRecords.StudentsId);
                db.cmd.Parameters.AddWithValue("@FeeSchedule_ID", feeRecords.FeeSchedulesId);
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
 db.cmd.CommandText = "delete from tblFeeRecords  where Id=@id";
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
        public static void Update(int id, FeeRecords feerecords)
        {
            try
            {
                db.cmd.CommandText = @"update tblFeeRecords set DateTime=@dt,Paid=@pd,AddedByEmployee_ID=@abid,
                                       Student_ID=@sid,FeeSchedule_ID=@fsid where Id=@id;";

                db.cmd.Parameters.AddWithValue("@dt", feerecords.DateTime);
                db.cmd.Parameters.AddWithValue("@pd", feerecords.Paid);
                db.cmd.Parameters.AddWithValue("@abid", feerecords.AddedByEmployeesId);
                db.cmd.Parameters.AddWithValue("@sid", feerecords.StudentsId);
                db.cmd.Parameters.AddWithValue("@fsid", feerecords.FeeSchedulesId);
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
