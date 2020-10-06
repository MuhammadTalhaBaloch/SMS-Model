using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_Models
{
    class ExpenseRecords
    {
        private static string Command;
        private static Database_Connection db = new Database_Connection();
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public bool Paid { get; set; }
        public int AddedByEmployeesId { get; set; }
        public int ExpenseTypesId { get; set; }
        public ExpenseTypes ExpenseTypes { get; set; }
        public Employees AddedByEmployees { get; set; }
        public override string ToString()
        {
            return "{ [ id: " + Id + ", datetime: " + DateTime + ", paid: " + Paid
                        + ", addedbyemployeesid: " + AddedByEmployeesId + ", expensetypesId: " + ExpenseTypesId + "  ]"
                        + ExpenseTypes + AddedByEmployees + "}";
        }
        public static List<ExpenseRecords> ListOfExpenseRecords
        {
            get
            {
                return _GetExpenseRecords();
            }
            private set
            {
            }
        }
        public ExpenseRecords()
        {
            ExpenseTypes = new ExpenseTypes();
            AddedByEmployees = new Employees();
        }
        private static List<ExpenseRecords> _GetExpenseRecords()
        {
            List<ExpenseRecords> ExpenseRecords = new List<ExpenseRecords>();


            try
            {
                Command = "select * from tblExpenseRecords";
                db.cmd.CommandText = Command;
                db.con.Open();

                SqlDataReader rdr = db.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    ExpenseRecords singleExpenseRecords = new ExpenseRecords();
                    singleExpenseRecords.Id = (int)rdr[0];
                    singleExpenseRecords.DateTime =Convert.ToDateTime( rdr[1]);
                    singleExpenseRecords.Paid = Convert.ToBoolean( rdr[2]);
                    singleExpenseRecords.AddedByEmployeesId= (int)rdr[3];
                    singleExpenseRecords.ExpenseTypesId= (int)rdr[4];

                    var emp = new Employees();
                    singleExpenseRecords.AddedByEmployees = Employees.ListOfEmployees.SingleOrDefault(e => e.Id == singleExpenseRecords.AddedByEmployeesId);

                    var exptype = new ExpenseTypes();
                    singleExpenseRecords.ExpenseTypes = ExpenseTypes.ListOfExpenseTypes.SingleOrDefault(et => et.Id == singleExpenseRecords.ExpenseTypesId);

                    ExpenseRecords.Add(singleExpenseRecords);
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
            return ExpenseRecords;
        }
        public static int Add(ExpenseRecords expenseRecords)
        {
            int retvalue = -1;
            try
            {

                db.cmd.CommandType = CommandType.StoredProcedure;
                db.cmd.CommandText = "procExpenseRecords_AddExpenseRecords";
                db.cmd.Parameters.AddWithValue("@DateTime", expenseRecords.DateTime);
                db.cmd.Parameters.AddWithValue("@Paid", expenseRecords.Paid);
                db.cmd.Parameters.AddWithValue("@AddedByEmployee_ID", expenseRecords.AddedByEmployeesId);
                db.cmd.Parameters.AddWithValue("@ExpenseType_ID", expenseRecords.ExpenseTypesId);
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
        public static int Add(ExpenseRecords expenseRecords,ExpenseTypes expenseTypes)
        {
            int retvalue = -1;

            expenseRecords.ExpenseTypesId = ExpenseTypes.Add(expenseTypes);
            retvalue = ExpenseRecords.Add(expenseRecords);

            return retvalue;
        }

        public static void Delete(int id)
        {
            try
            {
   db.cmd.CommandText = "delete from tblExpenseRecords  where Id=@id";
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
        public static void Update(int id, ExpenseRecords expenserecords)
        {
            try
            {
                db.cmd.CommandText = @"update tblExpenseRecords set DateTime=@dt,
                                       Paid=@PD,AddedByEmployee_ID=@abid,ExpenseType_ID=@etid where Id=@id;";

                db.cmd.Parameters.AddWithValue("@dt", expenserecords.DateTime);
                db.cmd.Parameters.AddWithValue("@PD", expenserecords.Paid);
                db.cmd.Parameters.AddWithValue("@abid", expenserecords.AddedByEmployeesId);
                db.cmd.Parameters.AddWithValue("@etid", expenserecords.ExpenseTypesId);


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
        public static void Update(int Id, ExpenseRecords expenserecords, ExpenseTypes expensetypes)
        {
            expenserecords.ExpenseTypesId = ExpenseTypes.Add(expensetypes);
            ExpenseRecords.Update(Id, expenserecords);
        }

    }
}
