using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_Models
{
    class Persons
    {
        private static string Command;
        private static Database_Connection db = new Database_Connection();
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string CNIC { get; set; }
        public string ImageUrlPath { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public int GendersId { get; set; }
        public Genders Genders { get; set; }
        public static List<Persons> ListOfPersons {
            get
            {
                return _GetPersons();
            }
           

        }
        public  override string ToString()
        {
            return "{ [ personid: " + PersonId + ", firstname: " + FirstName + ", lastname: " + LastName + ",  datofbirth: "
                        + DateOfBirth + ", cnic: " + CNIC + "+ , imageurlpath: " + ImageUrlPath + " + , phone1: " 
                        + Phone1 + "  , phone2: " + Phone2 + " "
                        + "  , gendersid: " + GendersId + " ]" + Genders + "}";
        }
        private static List<Persons> _GetPersons()
        {
            List<Persons> Persons = new List<Persons>();
            try
            {
                Command = @"select * from tblPersons prs join tblGenders gdr
                            on prs.Gender_ID=gdr.id";
                db.cmd.CommandText = Command;
                db.con.Open();

                SqlDataReader rdr = db.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Persons singlePersons = new Persons();

                    singlePersons.PersonId = (int)rdr[0];
                    singlePersons.FirstName = rdr[1].ToString();
                    singlePersons.LastName = rdr[2].ToString();
                    singlePersons.DateOfBirth = rdr[3].ToString();
                    singlePersons.CNIC = rdr[4].ToString();
                    singlePersons.ImageUrlPath = rdr[5].ToString();
                    singlePersons.Phone1 = rdr[6].ToString();
                    singlePersons.Phone2 = rdr[7].ToString();
                    singlePersons.GendersId = (int)rdr[8];

                    singlePersons.Genders.Id = (int)rdr[9];
                    singlePersons.Genders.GenderName = rdr[10].ToString();
                    Persons.Add(singlePersons);
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
            return Persons;

        }

        public Persons()
        {
            Genders = new Genders();
        }

        public static int PersonAdd(Persons persons)
        {
            int retvalue = -1;
            try
            {
            db.cmd.CommandType = CommandType.StoredProcedure;
            db.cmd.CommandText = "procPersons_Persons";
            db.cmd.Parameters.AddWithValue("@FirstName", persons.FirstName);
            db.cmd.Parameters.AddWithValue("@LastName", persons.LastName);
            db.cmd.Parameters.AddWithValue("@DateOfBirth", persons.DateOfBirth);
            db.cmd.Parameters.AddWithValue("@CNIC", persons.CNIC);
            db.cmd.Parameters.AddWithValue("@ImageUrlPath", persons.ImageUrlPath);
            db.cmd.Parameters.AddWithValue("@Phone1", persons.Phone1);
            db.cmd.Parameters.AddWithValue("@Phone2", persons.Phone2);
            db.cmd.Parameters.AddWithValue("@Gender_ID", persons.GendersId);
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
        public static void PersonUpdate(int id, Persons person)
        {
          
            try
            {
                db.cmd.CommandText = @"  update tblPersons 

                                    set FirstName = @firstname,
                                        LastName = @lastname,
                                        DateOfBirth = @dateofbirth,
                                        CNIC = @cnic,
                                        ImageUrlPath = @imageurlpath,
                                        Phone1 = @phone1,
                                        phone2 = @phone2,
                                        Gender_ID = @genderid

                                    where Id = @personid";
            db.cmd.Parameters.AddWithValue("@firstname", person.FirstName);
            db.cmd.Parameters.AddWithValue("@lastname", person.LastName);
            db.cmd.Parameters.AddWithValue("@dateofbirth", person.DateOfBirth);
            db.cmd.Parameters.AddWithValue("@cnic", person.CNIC);
            db.cmd.Parameters.AddWithValue("@imageurlpath", person.ImageUrlPath);
            db.cmd.Parameters.AddWithValue("@phone1", person.Phone1);
            db.cmd.Parameters.AddWithValue("@phone2", person.Phone2);
            db.cmd.Parameters.AddWithValue("@genderid", person.GendersId);
            db.cmd.Parameters.AddWithValue("@personid", person.PersonId);

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

        public static void PersonDelete(int id)
        {
            try
            {
 db.cmd.CommandText = "delete from tblPersons  where Id=@id";
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
