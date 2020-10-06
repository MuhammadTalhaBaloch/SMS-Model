using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_Models
{
    public class FeeTypes
    {
        private static string Command;
        private static Database_Connection db = new Database_Connection();
        public int Id { get; set; }
        public string FeeTypeName { get; set; }
        public override string ToString()
        {
            return "[ id: " + Id + ", Feetypename : " + FeeTypeName + " ]";
        }
        public static List<FeeTypes> ListOfFeeTypes
        {
            get
            {
                return _GetListOfFeeTypes();
            }
            set
            {

            }
        }

        private static List<FeeTypes> _GetListOfFeeTypes()
        {
            List<FeeTypes> FeeTypes = new List<FeeTypes>();
            try
            {
                Command = @"select * from tblFeeTypes";
                db.cmd.CommandText = Command;
                db.con.Open();

                SqlDataReader rdr = db.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    FeeTypes singleFeeTypes = new FeeTypes();

                    singleFeeTypes.Id = (int)rdr[0];
                    singleFeeTypes.FeeTypeName = rdr[1].ToString();
                    



                    FeeTypes.Add(singleFeeTypes);
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

            return FeeTypes;
        }

        public static int Add(FeeTypes feetypes)
        {
            int retvalue = -1;
            try
            {

            db.cmd.CommandType = CommandType.StoredProcedure;
            db.cmd.CommandText = "procFeeTypes_FeeTypes";
            db.cmd.Parameters.AddWithValue("@FeeTypeName", feetypes.FeeTypeName);
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

        public static void Update(int id, FeeTypes feetypes)
        {
            try
            {
            db.cmd.CommandText = "update tblFeeTypes set FeeTypeName = @FeeTypeName where Id=@id";
            db.cmd.Parameters.AddWithValue("@FeeTypename", feetypes.FeeTypeName);
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

    db.cmd.CommandText = "delete from  tblFeeTypes where Id=@id";
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