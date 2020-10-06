using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_Models
{
    class DocumentUrls
    {
        private static string Command;
        private static Database_Connection db = new Database_Connection();

        public int Id { get; set; }
        public string DocumentLink { get; set; }
        public DateTime DateTime { get; set; }
        public int AddedByEmployeesId { get; set; }
        public Employees AddedByEmployees { get; set; }

        public override string ToString()
        {
            return "{[ id: " + Id + ", DocumentLink: " + DocumentLink + ", DateTime: " 
                + DateTime + ", AddedByEmployeesId: " + AddedByEmployeesId + " ]" + AddedByEmployees + " }";
        }
        public static List<DocumentUrls> ListOfDocumentUrls
        {
            get
            {
                return _GetDocumentUrls();
            }
            private set
            {
            }
        }

        private static List<DocumentUrls> _GetDocumentUrls()
        {
            List<DocumentUrls> documentUrls = new List<DocumentUrls>();


            try
            {
                Command = "select * from tblDocumentUrls";
                db.cmd.CommandText = Command;
                db.con.Open();

                SqlDataReader rdr = db.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    DocumentUrls singledocumentUrl = new DocumentUrls();
                    
                    singledocumentUrl.Id = (int)rdr[0];
                    singledocumentUrl.DocumentLink = rdr[1].ToString();
                    singledocumentUrl.DateTime = Convert.ToDateTime( rdr[2]);
                    singledocumentUrl.AddedByEmployeesId = (int)rdr[3];
                    singledocumentUrl.AddedByEmployees = Employees
                                                        .ListOfEmployees
                                                        .SingleOrDefault(e => e.Id == singledocumentUrl.AddedByEmployeesId);
                    documentUrls.Add(singledocumentUrl);
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
            return documentUrls;
        }
        public static int Add(DocumentUrls documenturls)
        {
            int retvalue = -1;
            try
            {

                db.cmd.CommandType = CommandType.StoredProcedure;
                            db.cmd.CommandText = "procDocumentUrls_AddDucumentUrls";
                            db.cmd.Parameters.AddWithValue("@DocumentLink", documenturls.DocumentLink);
                            db.cmd.Parameters.AddWithValue("@DateTime", documenturls.DateTime);
                            db.cmd.Parameters.AddWithValue("@AddedByEmployee_ID ", documenturls.AddedByEmployeesId);
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
        public static void Update(int id, DocumentUrls documenturls)
        {
            try
            {
        db.cmd.CommandText = "update tblDocumentUrls set DocumentLink = @DocumentLink,DateTime=@DateTime,AddedByEmployee_ID=@AddedByEmployee_ID where Id=@id";
            db.cmd.Parameters.AddWithValue("@DocumentLink", documenturls.DocumentLink);
            db.cmd.Parameters.AddWithValue("@DateTime", documenturls.DateTime);
            db.cmd.Parameters.AddWithValue("@AddedByEmployee_ID", documenturls.AddedByEmployeesId);
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
            db.cmd.CommandText = "delete from tblDocumentUrls where Id=@id";
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
