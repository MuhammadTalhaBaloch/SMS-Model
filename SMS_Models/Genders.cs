using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_Models
{
    class Genders
    {
        private static string Command;
        private static Database_Connection db = new Database_Connection();
        public int Id { get; set; }
        public string GenderName { get; set; }
        public override string ToString()
        {
            return "[ id: " + Id + ", GenderName: " + GenderName + " ]";
        }

        public static List<Genders> ListOfGenders
        {
            get
            {
                return _GetListOfGenders();
            }
            set
            {

            }
        }

        private static List<Genders> _GetListOfGenders()
        {
            List<Genders> Genders = new List<Genders>();
            try
            {
                Command = @"select * from tblGenders";
                db.cmd.CommandText = Command;
                db.con.Open();

                SqlDataReader rdr = db.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Genders singleGenders = new Genders();
                    singleGenders.Id = (int)rdr[0];
                    singleGenders.GenderName = rdr[1].ToString();


                    Genders.Add(singleGenders);
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

            return Genders;

        }
        public static int Add(Genders gender)
        {
            int retvalue = -1;
            try
            {
   db.cmd.CommandType = CommandType.StoredProcedure;
            db.cmd.CommandText = "procGenders_AddGender ";
            db.cmd.Parameters.AddWithValue("@GenderName", gender.GenderName);
            db.cmd.Parameters.Add("@Genderid", SqlDbType.Int);
            db.cmd.Parameters["@Genderid"].Direction = ParameterDirection.Output;
            db.con.Open();
            db.cmd.ExecuteNonQuery();
            retvalue= Convert.ToInt32(db.cmd.Parameters["@Genderid"].Value);

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
        public static void Update(int id, Genders gender)
        {
            try
            {
            db.cmd.CommandText = "update tblGenders set GenderName = @gendername where Id=@id";
            db.cmd.Parameters.AddWithValue("@gendername", gender.GenderName);
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

            db.cmd.CommandText = "delete from tblGenders where Id=@id";
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
