using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_Models
{
    class FeeSchedules
    {
        private static string Command;
        private static Database_Connection db = new Database_Connection();
        public int Id { get; set; }
        public float Fee { get; set; }
        public int GradesId { get; set; }
        public int FeeTypesId { get; set; }
        public Grades Grades { get; set; }
        public FeeTypes FeeType { get; set; }
        public override string ToString()
        {
            return "{ [ id: " + Id + ", fee: " + Fee + ", gradesid: " + GradesId
                        + ", feetypesid: " + FeeTypesId + "  ]"
                        + Grades + FeeType + "}";
        }

        public static List<FeeSchedules> ListOfFeeSchedules
        {
            get
            {
                return _GetListOfFeeSchedules();
            }
            set
            {

            }
        }
        public FeeSchedules()
        {
            Grades = new Grades();
            FeeType = new FeeTypes();
        }

        private static List<FeeSchedules> _GetListOfFeeSchedules()
        {
            List<FeeSchedules> FeeSchedules = new List<FeeSchedules>();
            try
            {
                Command = @"select * from tblFeeSchedules fees join tblGrades gds
on fees.Grade_ID=gds.Id join tblFeeTypes fts
on fees.FeeType_ID=fts.Id";
                db.cmd.CommandText = Command;
                db.con.Open();

                SqlDataReader rdr = db.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    FeeSchedules singleFeeSchedules = new FeeSchedules();

                    singleFeeSchedules.Id = (int)rdr[0];
                    singleFeeSchedules.Fee = Convert.ToSingle(rdr[1]);
                    singleFeeSchedules.GradesId= (int)rdr[2];
                    singleFeeSchedules.FeeTypesId= (int)rdr[3];

                    //
                    var grades = new Grades();
                    singleFeeSchedules.Grades = Grades.ListOfGrades.SingleOrDefault(gr => gr.Id == singleFeeSchedules.GradesId);
                    //

                    //singleFeeSchedules.Grades.Id = (int)rdr[4];
                    //singleFeeSchedules.Grades.GradeName = rdr[5].ToString();

                    //
                    var feetype = new FeeTypes();
                    singleFeeSchedules.FeeType = FeeTypes.ListOfFeeTypes.SingleOrDefault(ft => ft.Id == singleFeeSchedules.FeeTypesId);
                    //

                    //singleFeeSchedules.FeeType.Id = (int)rdr[6];
                    //singleFeeSchedules.FeeType.FeeTypeName = rdr[7].ToString();

                    FeeSchedules.Add(singleFeeSchedules);
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

            return FeeSchedules;
        }
        public static int Add(FeeSchedules feeSchedules)
        {
            int retvalue = -1;
            try
            {

                db.cmd.CommandType = CommandType.StoredProcedure;
                db.cmd.CommandText = "procFeeSchedules_AddFeeSchedules";
                db.cmd.Parameters.AddWithValue("@Fee", feeSchedules.Fee);
                db.cmd.Parameters.AddWithValue("@Grade_ID",feeSchedules.GradesId);
                db.cmd.Parameters.AddWithValue("@FeeType_ID", feeSchedules.FeeTypesId);
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
        public static int Add(FeeSchedules feeSchedules, FeeTypes feeTypes)
        {
            int retvalue = -1;
            feeSchedules.FeeTypesId = FeeTypes.Add(feeTypes);
            retvalue = FeeSchedules.Add(feeSchedules);

            return retvalue;
        }

        public static void Delete(int id)
        {
            try
            {
         db.cmd.CommandText = "delete from tblFeeSchedules  where Id=@id";
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
        public static void Update(int id, FeeSchedules feeschedules)
        {
            try
            {
                db.cmd.CommandText = @"update tblFeeSchedules set FEE = @fee, 
                    Grade_ID=@gradeid,
                FeeType_ID=@feetypeid
                 where Id=@id; ";

                db.cmd.Parameters.AddWithValue("@fee", feeschedules.Fee);
                db.cmd.Parameters.AddWithValue("@gradeid", feeschedules.GradesId);
                db.cmd.Parameters.AddWithValue("@feetypeid", feeschedules.FeeTypesId);
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
        public static void Update(int Id, FeeSchedules feeschedules, Grades grades)
        {
            feeschedules.GradesId = Grades.Add(grades);
            FeeSchedules.Update(Id, feeschedules);
        }

    }
}
