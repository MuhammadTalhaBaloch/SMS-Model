using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_Models
{
    public class ExpenseTypes
    {
        private static string Command;
        private static Database_Connection db = new Database_Connection();
        public int Id { get; set; }
        public string ExpenseTypeName { get; set; }
        public override string ToString()
        {
            return "[ id: " + Id + ", expensetypename : " + ExpenseTypeName + " ]";
        }
        public static List<ExpenseTypes> ListOfExpenseTypes
        {
            get
            {
                return _GetExpenseTypes();
            }
            private set
            {
            }
        }

        private static List<ExpenseTypes> _GetExpenseTypes()
        {
            List<ExpenseTypes> ExpenseTypes = new List<ExpenseTypes>();


            try
            {
                Command = "select * from tblExamTypes";
                db.cmd.CommandText = Command;
                db.con.Open();

                SqlDataReader rdr = db.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    ExpenseTypes singleExpenseTypes = new ExpenseTypes();
                    singleExpenseTypes.Id = (int)rdr[0];
                    singleExpenseTypes.ExpenseTypeName = rdr[1].ToString();



                    ExpenseTypes.Add(singleExpenseTypes);
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
            return ExpenseTypes;
        }
        public static int Add(ExpenseTypes expensetypes)
        {
            int retvalue = -1;
            try
            {
          db.cmd.CommandType = CommandType.StoredProcedure;
            db.cmd.CommandText = "procExpenseTypes_ExpenseTypes";
            db.cmd.Parameters.AddWithValue("@ExpenseTypeName", expensetypes.ExpenseTypeName);
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
        public static void Update(int id, ExpenseTypes expensetypes)
        {
            try
            {
            db.cmd.CommandText = "update tblExpenseTypes set ExpenseTypeName = @ExpenseTypeName where Id=@id";
            db.cmd.Parameters.AddWithValue("@ExpenseTypename", expensetypes.ExpenseTypeName);
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
            db.cmd.CommandText = "delete from tblExpenseTypes where Id=@id";
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