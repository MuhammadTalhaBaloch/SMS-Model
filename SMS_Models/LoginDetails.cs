using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_Models
{
    public class LoginDetails
    {
        private static string Command;
        private static Database_Connection db = new Database_Connection();

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool AllowAccess { get; set; }

        public override string ToString()
        {
            return "[ id: " + Id + ", username : " + Username + ",password : " + Password + ",allowaccess : " + AllowAccess + " ]";
        }
        public static List<LoginDetails> ListOfLoginDetails
        {
            get
            {
                return _GetListOfLoginDetails();
            }
            set
            {

            }
        }
     
        private static List<LoginDetails> _GetListOfLoginDetails()
        {
            List<LoginDetails> loginDetails = new List<LoginDetails>();
            try
            {
                Command = @"select * from tblLoginDetails";
                db.cmd.CommandText = Command;
                db.con.Open();

                SqlDataReader rdr = db.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    LoginDetails singleLoginDetails = new LoginDetails();
                    singleLoginDetails.Id = (int)rdr[0];
                    singleLoginDetails.Username = rdr[1].ToString();
                    singleLoginDetails.Password = rdr[2].ToString();
                    singleLoginDetails.AllowAccess = Convert.ToBoolean( rdr[3]);

                    loginDetails.Add(singleLoginDetails);
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

            return loginDetails;
        }

        public static int Add(LoginDetails logindetails)
        {
            int retvalue = -1;
            try
            {
      db.cmd.CommandType = CommandType.StoredProcedure;
            db.cmd.CommandText = "procLoginDetails_LoginDetails";
            db.cmd.Parameters.AddWithValue("@Username", logindetails.Username);
            db.cmd.Parameters.AddWithValue("@Password", logindetails.Password);
            db.cmd.Parameters.AddWithValue("@AllowAccess", logindetails.AllowAccess);
            db.cmd.Parameters.Add("@id", SqlDbType.Int);
            db.cmd.Parameters["@id"].Direction = ParameterDirection.Output;
            db.con.Open();
            db.cmd.ExecuteNonQuery();
            retvalue= Convert.ToInt32(db.cmd.Parameters["@id"].Value);

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
        public static void Update(int id, LoginDetails logindetails)
        {
            try
            {
 db.cmd.CommandText = "update tblLoginDetails set Username = @Username,Password=@Password,AllowAccess=@AllowAccess where Id=@id";
            db.cmd.Parameters.AddWithValue("@Username", logindetails.Username);
            db.cmd.Parameters.AddWithValue("@Password", logindetails.Password);
            db.cmd.Parameters.AddWithValue("@AllowAccess", logindetails.AllowAccess);
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
  db.cmd.CommandText = "delete from tblLoginDetails where Id=@id";
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