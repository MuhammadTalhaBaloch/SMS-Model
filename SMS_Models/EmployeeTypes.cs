using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SMS_Models
{
    public class EmployeeTypes
    {
        private static string Command;
        private static Database_Connection db = new Database_Connection();
        public int Id { get; set; }
        public string EmployeeTypeName { get; set; }
        public override string ToString()
        {
            return "[ id: " + Id + ", Employeetypename : " + EmployeeTypeName + " ]";
        }
        public static List<EmployeeTypes> ListOfEmployeeTypes
        {
            get
            {
                return _GetEmployeeTypes();
            }
            private set
            {
            }
        }

        private static List<EmployeeTypes> _GetEmployeeTypes()
        {
            List<EmployeeTypes>employeeTypes = new List<EmployeeTypes>();


            try
            {
                Command = "select * from tblEmployeeTypes";
                db.cmd.CommandText = Command;
                db.con.Open();

                SqlDataReader rdr = db.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    EmployeeTypes singleEmployeeTypes = new EmployeeTypes();
                    singleEmployeeTypes.Id = (int)rdr[0];
                    singleEmployeeTypes.EmployeeTypeName = rdr[1].ToString();
                    employeeTypes.Add(singleEmployeeTypes);
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
            return employeeTypes;
        }
        public static int Add(EmployeeTypes employeetypes)
        {
            int retvalue = -1;
            try
            {

            db.cmd.CommandType = CommandType.StoredProcedure;
            db.cmd.CommandText = "procEmployeeTypes_EmployeeTypes";
            db.cmd.Parameters.AddWithValue("@EmployeeTypeName", employeetypes.EmployeeTypeName);
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
        public static void Update(int id, EmployeeTypes employeetypes)
        {
            try
            {

  db.cmd.CommandText = "update tblEmployeeTypes set EmployeeTypeName = @EmployeeTypeName where Id=@id";
            db.cmd.Parameters.AddWithValue("@EmployeeTypeName", employeetypes.EmployeeTypeName);
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
 db.cmd.CommandText = "delete from tblEmployeeTypes where Id=@id";
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