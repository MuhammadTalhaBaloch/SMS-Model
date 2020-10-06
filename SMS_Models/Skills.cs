using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_Models
{
    class Skills
    {
        private static string Command;
        private static Database_Connection db = new Database_Connection();
        public int Id { get; set; }
        public string SkillName { get; set; }
        public override string ToString()
        {
            return "[ id: " + Id + ", SkillName: " + SkillName + " ]";
        }
        public static List<Skills> ListOfSkills
        {
            get
            {
                return _GetSkills();
            }
            private set
            {

            }
        }

        private static List<Skills> _GetSkills()
        {
            List<Skills> Skills = new List<Skills>();


            try
            {
                Command = "select * from tblSkills";
                db.cmd.CommandText = Command;
                db.con.Open();

                SqlDataReader rdr = db.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Skills singleSkills = new Skills();
                    singleSkills.Id = (int)rdr[0];
                    singleSkills.SkillName = rdr[1].ToString(); 

                

                    Skills.Add(singleSkills);
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
            return Skills;
        }
        public static int Add(Skills skills)
        {
            int retvalue = -1;
            try
            {
        db.cmd.CommandType = CommandType.StoredProcedure;
            db.cmd.CommandText = "procSkills_AddSkills ";
            db.cmd.Parameters.AddWithValue("@SkillName", skills.SkillName);
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

        public static void Update(int id, Skills skills)
        {
            try
            {

        db.cmd.CommandText = "update tblSkills set SkillName = @SkillName where Id=@id";
            db.cmd.Parameters.AddWithValue("@Skillname", skills.SkillName);
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
      db.cmd.CommandText = "delete from tblSkills where Id=@id";
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
