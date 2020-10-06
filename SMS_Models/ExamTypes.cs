using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_Models
{
    public class ExamTypes
    {
        private static string Command;
        private static Database_Connection db = new Database_Connection();
        public int Id { get; set; }
        public string ExamTypeName { get; set; }
        public override string ToString()
        {
            return "[ id: " + Id + ", examtypename : " + ExamTypeName + " ]";
        }
        public static List<ExamTypes> ListOfExamTypes
        {
            get
            {
                return _GetExamTypes();
            }
            private set
            {
            }
        }

        private static List<ExamTypes> _GetExamTypes()
        {
            List<ExamTypes> ExamTypes = new List<ExamTypes>();


            try
            {
                Command = "select * from tblExamTypes";
                db.cmd.CommandText = Command;
                db.con.Open();

                SqlDataReader rdr = db.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    ExamTypes singleExamTypes = new ExamTypes();
                    singleExamTypes.Id = (int)rdr[0];
                    singleExamTypes.ExamTypeName = rdr[1].ToString();
                    ExamTypes.Add(singleExamTypes);
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
            return ExamTypes;
        }
        public static int Add(ExamTypes examtypes)
        {
            int retvalue = -1;
            try
            {

            db.cmd.CommandType = CommandType.StoredProcedure;
            db.cmd.CommandText = "procExamTypes_ExamTypes";
            db.cmd.Parameters.AddWithValue("@ExamTypeName", examtypes.ExamTypeName);
            db.cmd.Parameters.Add("@Id", SqlDbType.Int);
            db.cmd.Parameters["@Id"].Direction = ParameterDirection.Output;
            db.con.Open();
            db.cmd.ExecuteNonQuery();
            db.con.Close();
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
        public static void Update(int id, ExamTypes examtypes)
        {
            try
            {
            db.cmd.CommandText = "update tblExamTypes set ExamTypeName = @ExamTypeName where Id=@id";
            db.cmd.Parameters.AddWithValue("@ExamTypename", examtypes.ExamTypeName);
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

     db.cmd.CommandText = "delete from tblExamTypes where Id=@id";
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