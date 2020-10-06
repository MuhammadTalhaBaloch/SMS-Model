using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_Models
{
    class EmployeeDocuments
    {
        private static string Command;
        private static Database_Connection db = new Database_Connection();
        public int Id { get; set; }
        public int EmployeesId { get; set; }
        public int DocumentUrlsId { get; set; }
        public Employees Employees { get; set; }
        public DocumentUrls DocumentUrls { get; set; }
        public override string ToString()
        {
            return "{ [ id: " + Id + ", employeeid: " + EmployeesId + ", DocumentUrlsId: " + DocumentUrlsId + "  ]"
                        + Employees + DocumentUrls + "}";
        }
        public static List<EmployeeDocuments> ListOfEmployeeDocuments
        {
            get
            {
                return _GetEmployeeDocuments();
            }
            private set
            {

            }
        }
        public EmployeeDocuments()
        {
            Employees = new Employees();
            DocumentUrls = new DocumentUrls();
        }
        private static List<EmployeeDocuments> _GetEmployeeDocuments()
        {
            List<EmployeeDocuments> EmployeeDocuments = new List<EmployeeDocuments>();


            try
            {
                Command = "select * from tblEmployeeDocuments";
                db.cmd.CommandText = Command;
                db.con.Open();

                SqlDataReader rdr = db.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    EmployeeDocuments singleEmployeeDocuments = new EmployeeDocuments();
                    singleEmployeeDocuments.Id = (int)rdr[0];
                    singleEmployeeDocuments.EmployeesId = (int)rdr[1];
                    singleEmployeeDocuments.DocumentUrlsId= (int)rdr[2];

                    var emp = new Employees();
                    singleEmployeeDocuments.Employees = Employees.ListOfEmployees.SingleOrDefault(e => e.Id == singleEmployeeDocuments.EmployeesId);

                    var docurl = new DocumentUrls();
                    singleEmployeeDocuments.DocumentUrls = DocumentUrls.ListOfDocumentUrls.SingleOrDefault(du => du.Id == singleEmployeeDocuments.DocumentUrlsId);


                    EmployeeDocuments.Add(singleEmployeeDocuments);
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
            return EmployeeDocuments;
        }

        public static int Add(EmployeeDocuments employeeDocuments)
        {
            int retvalue = -1;
            try
            {


                employeeDocuments.DocumentUrlsId = DocumentUrls.Add(employeeDocuments.DocumentUrls);



                db.cmd.CommandType = CommandType.StoredProcedure;
                db.cmd.CommandText = "procEmployeeDocuments_AddEmployeeDocuments";
                db.cmd.Parameters.AddWithValue("@Employee_ID", employeeDocuments.EmployeesId);
                db.cmd.Parameters.AddWithValue("@DocumentUrl_ID", employeeDocuments.DocumentUrlsId);
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

        public static int Add(EmployeeDocuments employeeDocuments,DocumentUrls documentUrls)
        {
            int retvalue = -1;
            employeeDocuments.DocumentUrlsId = DocumentUrls.Add(documentUrls);
            retvalue = EmployeeDocuments.Add(employeeDocuments);
            return retvalue;
        }

        public static void Delete(int id)
        {
            try
            {
                 db.cmd.CommandText = "delete from tblEmployeeDocuments  where Id=@id";
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
