using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_Models
{
    class Parents:Persons
    { 
        private static string Command;
        private static Database_Connection db = new Database_Connection();
        public int Id { get; set; }
        public int PersonsId { get; set; }
        public int LoginDetailsId { get; set; }
        public LoginDetails LoginDetails { get; set; }
        public override string ToString()
        {
            return "{ [ id: " + Id + ", PersonsId: " + PersonsId + ", LoginDetailsId: " + LoginDetailsId + ", personId: "
                        + PersonId + ", firstName: " + FirstName + "  ]" + LoginDetails + "}";
        }
        
        public static List<Parents> ListOfParents {
            get
            {
                return _GetListOfParents();
            }
            set
            {

            }
        }
        public Parents()
        {
            LoginDetails = new LoginDetails();
        }

        private static List<Parents> _GetListOfParents()
        {
            List<Parents> parents = new List<Parents>();
            try
            {
                Command = @"    select * from tblPersons prs
	                                join tblGenders gndr
		                             on prs.Gender_ID = gndr.id 
	                                 join tblParents prnt 
		                             on prs.Id=prnt.Person_ID
	                                 join tblLoginDetails lgn
		                             on prnt.LoginDetail_ID=lgn.id;";
                db.cmd.CommandText = Command;
                db.con.Open();

                SqlDataReader rdr = db.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Parents singleparent = new Parents();
                    singleparent.PersonId = (int)rdr[0];
                    singleparent.FirstName = rdr[1].ToString();
                    singleparent.LastName = rdr[2].ToString();
                    singleparent.DateOfBirth = rdr[3].ToString();
                    singleparent.CNIC = rdr[4].ToString();
                    singleparent.ImageUrlPath = rdr[5].ToString();
                    singleparent.Phone1 = rdr[6].ToString();
                    singleparent.Phone2 = rdr[7].ToString();
                    singleparent.GendersId = (int)rdr[8];

                    singleparent.Genders.Id = (int)rdr[9];
                    singleparent.Genders.GenderName = rdr[10].ToString();

                    singleparent.Id = (int)rdr[11];
                    singleparent.PersonId =(int)rdr[12];
                    singleparent.LoginDetailsId=(int)rdr[13];

                    //
                    var logindetail = new LoginDetails();
                    singleparent.LoginDetails = LoginDetails.ListOfLoginDetails.SingleOrDefault(lg => lg.Id == singleparent.LoginDetailsId);
                    //

                    //singleparent.LoginDetails.Id = (int)rdr[14];
                    //singleparent.LoginDetails.Username = rdr[15].ToString();
                    //singleparent.LoginDetails.Password = rdr[16].ToString();
                    //singleparent.LoginDetails.AllowAccess = (int)rdr[17];

                    parents.Add(singleparent);
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

            return parents;
            

        }

        public static int Add(Parents parents)
        {
            int retvalue = -1;
            try
            {
                Persons persons = new Persons();

                persons.FirstName = parents.FirstName;
                persons.LastName = parents.LastName;
                persons.DateOfBirth = parents.DateOfBirth;
                persons.CNIC = parents.CNIC;
                persons.ImageUrlPath = parents.ImageUrlPath;
                persons.Phone1 = parents.Phone1;
                persons.Phone2 = parents.Phone2;
                persons.GendersId = parents.GendersId;

                parents.PersonsId = Persons.PersonAdd(persons);

                parents.LoginDetailsId = LoginDetails.Add(parents.LoginDetails);


                db.cmd.CommandType = CommandType.StoredProcedure;
                db.cmd.CommandText = "procParents_AddParents";
                db.cmd.Parameters.AddWithValue("@Person_ID", parents.PersonsId);
                db.cmd.Parameters.AddWithValue("@LoginDetail_ID", parents.LoginDetailsId);
                db.cmd.Parameters.Add("@id", SqlDbType.Int);
                db.cmd.Parameters["@id"].Direction = ParameterDirection.Output;
                db.con.Open();
                db.cmd.ExecuteNonQuery();
                db.con.Close();
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

        public static void Update(int parentId, Parents parents)
        {
            try
            {

                Persons.PersonUpdate(parents.PersonId, parents);
                LoginDetails.Update(parents.LoginDetailsId, parents.LoginDetails);

                db.cmd.CommandText = @"UPDATE [dbo].[tblParents]
                            SET [Person_ID] = @pid
                             ,[LoginDetail_ID] = @lid
                                WHERE Id=id";
                db.cmd.Parameters.AddWithValue("@pid", parents.PersonId);
                db.cmd.Parameters.AddWithValue("@lid", parents.LoginDetailsId);


                db.cmd.Parameters.AddWithValue("@id", parents.Id);

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
            Parents parents = new Parents();
            Persons person = new Persons();
            var parent = Parents.ListOfParents.SingleOrDefault(p => p.Id == id);
            Persons.PersonDelete(parent.PersonsId);
            LoginDetails.Delete(parents.LoginDetailsId);
            try
            {
        db.cmd.CommandText = "delete from tblParents  where Id=@id";
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
