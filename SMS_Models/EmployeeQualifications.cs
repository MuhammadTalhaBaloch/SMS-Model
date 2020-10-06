using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_Models
{
    class EmployeeQualifications
    {
        private static string Command;
        private static Database_Connection db = new Database_Connection();
        public int Id { get; set; }
        public int EmployeesId { get; set; }
        public int QualificationsId { get; set; }
        public Employees Employees { get; set; }
        public Qualifications Qualifications { get; set; }
        public override string ToString()
        {
            return "{ [ id: " + Id + ", employeeid: " + EmployeesId + ", QualificationsId " + QualificationsId + "  ]"
                        + Employees + Qualifications + "}";
        }
        public static List<EmployeeQualifications> ListOfEmployeeQualifications
        {
            get
            {
                return _GetEmployeeQualifications();
            }
            private set
            {

            }
        }
        //public EmployeeQualifications()
        //{
        //    Employees = new Employees();
        //    Qualifications = new Qualifications();
        //}
        private static List<EmployeeQualifications> _GetEmployeeQualifications()
        {
            List<EmployeeQualifications> EmployeeQualifications = new List<EmployeeQualifications>();


            try
            {
                Command = "select * from tblEmployeeQualifications";
                db.cmd.CommandText = Command;
                db.con.Open();

                SqlDataReader rdr = db.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    EmployeeQualifications singleEmployeeQualifications = new EmployeeQualifications();
                    singleEmployeeQualifications.Id = (int)rdr[0];
                    singleEmployeeQualifications.EmployeesId = (int)rdr[1];
                    singleEmployeeQualifications.QualificationsId = (int)rdr[2];

                    var emp = new Employees();
                    singleEmployeeQualifications.Employees = Employees.ListOfEmployees.SingleOrDefault(e => e.Id == singleEmployeeQualifications.EmployeesId);

                    var qualification = new Qualifications();
                    singleEmployeeQualifications.Qualifications = Qualifications.ListOfQualifications.SingleOrDefault(q => q.Id == singleEmployeeQualifications.QualificationsId);


                    EmployeeQualifications.Add(singleEmployeeQualifications);
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
            return EmployeeQualifications;
        }

        public static int Add(EmployeeQualifications employeeQualifications)
        {
            int retvalue = -1;
            try
            {

                db.cmd.CommandType = CommandType.StoredProcedure;
                db.cmd.CommandText = "procEmployeeQualifications_AddEmployeeQualifications";
                db.cmd.Parameters.AddWithValue("@Employee_ID", employeeQualifications.EmployeesId);
                db.cmd.Parameters.AddWithValue("@Qualification_ID", employeeQualifications.QualificationsId);
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
        public static int Add(EmployeeQualifications employeeQualifications,Qualifications qualifications)
        {
            int retvalue = -1;
            employeeQualifications.QualificationsId = Qualifications.Add(qualifications);
            retvalue = EmployeeQualifications.Add(employeeQualifications);
            return retvalue;
        }
        public static void Update(int id, EmployeeQualifications empQual)
        {
            try
            {
     db.cmd.CommandText = @"update tblEmployeeQualifications 
                                     set Employee_ID = @eid,
                                     Qualification_ID = @qid
                                  where id = @cid";
            db.cmd.Parameters.AddWithValue("@eid", empQual.EmployeesId);
            db.cmd.Parameters.AddWithValue("@qid", empQual.QualificationsId);
            db.cmd.Parameters.AddWithValue("@cid", empQual.Id);
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
        public static void Update(int id, EmployeeQualifications empQuall, Qualifications qual)
        {
            Update(id, empQuall);
            Qualifications.Add(qual);
        }

        public static void Delete(int id)
        {
            try
            {
            db.cmd.CommandText = "delete from tblEmployeeQualifications  where Id=@id";
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