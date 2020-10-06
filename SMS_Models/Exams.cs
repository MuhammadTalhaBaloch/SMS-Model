using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_Models
{
    class Exams
    {
        private static string Command;
        private static Database_Connection db = new Database_Connection();
        public int Id { get; set; }
        public float TotalMarks { get; set; }
        public float ObtainedMarks { get; set; }
        public string DateTime { get; set; }
        public int StudentsId { get; set; }
        public int SubjectsId { get; set; }
        public int ExamTypesId { get; set; }
        public Students Students { get; set; }
        public Subjects Subjects { get; set; }
        public ExamTypes ExamTypes { get; set; }
        public override string ToString()
        {
            return "{ [ id: " + Id + ", totalmarks: " + TotalMarks + ", obtainedmarks: " + ObtainedMarks
                        + ", datetime: " + DateTime + ", studentsid: " + StudentsId + ", subjectsid: " + SubjectsId +
                        ", examstypesid: " + ExamTypesId + "  ]"
                        + Students + Subjects + ExamTypes + "}";
        }
        public static List<Exams> ListOfExams
        {
            get
            {
                return _GetExams();
            }
            private set
            {
            }
        }

        public Exams()
        {
            Students = new Students();
            Subjects = new Subjects();
            ExamTypes = new ExamTypes();
        }
        private static List<Exams> _GetExams()
        {
            List<Exams> Exams = new List<Exams>();


            try
            {
                Command = "select * from tblExams";
                db.cmd.CommandText = Command;
                db.con.Open();

                SqlDataReader rdr = db.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Exams singleExams = new Exams();
                    singleExams.Id = (int)rdr[0];
                    singleExams.TotalMarks=Convert.ToSingle( rdr[1]);
                    singleExams.ObtainedMarks= Convert.ToSingle(rdr[2]);
                    singleExams.DateTime = rdr[3].ToString();
                    singleExams.StudentsId= (int)rdr[4];
                    singleExams.SubjectsId= (int)rdr[5];
                    singleExams.ExamTypesId= (int)rdr[6];

                    var students = new Students();
                    singleExams.Students = Students.ListOfStudents.SingleOrDefault(s => s.Id == singleExams.StudentsId);

                    var subject = new Subjects();
                    singleExams.Subjects = Subjects.ListOfSubjects.SingleOrDefault(s => s.Id == singleExams.SubjectsId);

                    var examtype = new ExamTypes();
                    singleExams.ExamTypes = ExamTypes.ListOfExamTypes.SingleOrDefault(e => e.Id == singleExams.ExamTypesId);

                    Exams.Add(singleExams);
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
            return Exams;
        }

        public static int Add(Exams exams)
        {
            int retvalue = -1;
            try
            {

                db.cmd.CommandType = CommandType.StoredProcedure;
                db.cmd.CommandText = "procExams_AddExams";
                db.cmd.Parameters.AddWithValue("@TotalMarks", exams.TotalMarks);
                db.cmd.Parameters.AddWithValue("@ObtainedMarks", exams.ObtainedMarks);
                db.cmd.Parameters.AddWithValue("@DateTime ", exams.DateTime);
                db.cmd.Parameters.AddWithValue("@Student_ID", exams.StudentsId);
                db.cmd.Parameters.AddWithValue("@Subject_ID", exams.SubjectsId);
                db.cmd.Parameters.AddWithValue("@ExamType_ID", exams.ExamTypesId);
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
        public static int Add(Exams exams, ExamTypes examTypes)
        {
            int retvalue = -1;
            exams.ExamTypesId = ExamTypes.Add(examTypes);
            retvalue = Exams.Add(exams);

            return retvalue;
        }

        public static void Delete(int id)
        {
            try
            {
  db.cmd.CommandText = "delete from tblExams  where Id=@id";
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
        public static void Update(int id, Exams exams)
        {
            try
            {
                db.cmd.CommandText = @"  update tblExams set TotalMarks = @totalmarks, 
                   ObtainedMarks=@obtmarks,
					DateTime=@datetime,
					Student_ID=@stuid,
					Subject_ID=@subid,
					ExamType_ID=@examtid
                    where Id=@id;";

                db.cmd.Parameters.AddWithValue("@totalmarks", exams.TotalMarks);
                db.cmd.Parameters.AddWithValue("@obtmarks", exams.ObtainedMarks);
                db.cmd.Parameters.AddWithValue("@datetime", exams.DateTime);
                db.cmd.Parameters.AddWithValue("@stuid", exams.StudentsId);
                db.cmd.Parameters.AddWithValue("@subid", exams.SubjectsId);
                db.cmd.Parameters.AddWithValue("@examtid", exams.ExamTypesId);

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
        public static void Update(int Id, Exams exams, ExamTypes examtype)
        {
            exams.ExamTypesId = ExamTypes.Add(examtype);
            Exams.Update(Id, exams);
        }
        public static void GenerateMarkSheetBySubject(int Subjectid, DateTime startdate, DateTime enddate)
        {
          
            //    var d = ex.ListOfExams;
            List<Exams> aa = Exams.ListOfExams.FindAll(su => su.SubjectsId == Subjectid && Convert.ToDateTime(su.DateTime) >= startdate || Convert.ToDateTime(su.DateTime) <= enddate);

            using (var workbook = new XLWorkbook())
            {

                var worksheet = workbook.Worksheets.Add("Sample Sheet");
                worksheet.Cell("A6").Value = "Student Name";
                worksheet.Cell("B6").Value = "Student Id";
                worksheet.Cell("C6").Value = "Obtained Marks";
                worksheet.Cell("D6").Value = "Total Marks";
                worksheet.Cell("A6").Style.Font.Bold = true;
                worksheet.Cell("B6").Style.Font.Bold = true;
                worksheet.Cell("C6").Style.Font.Bold = true;
                worksheet.Cell("D6").Style.Font.Bold = true;
                worksheet.Cell("E6").Style.Font.Bold = true;

                worksheet.Cell("A1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Cell("A2").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Cell("A3").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Cell("A4").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Cell("B1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Cell("B2").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Cell("B3").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Cell("B4").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                worksheet.Cell("A1").Style.Fill.BackgroundColor = XLColor.Yellow;
                worksheet.Cell("A2").Style.Fill.BackgroundColor = XLColor.Yellow;
                worksheet.Cell("A3").Style.Fill.BackgroundColor = XLColor.Yellow;
                worksheet.Cell("A4").Style.Fill.BackgroundColor = XLColor.Yellow;
                worksheet.Cell("B1").Style.Fill.BackgroundColor = XLColor.Yellow;
                worksheet.Cell("B2").Style.Fill.BackgroundColor = XLColor.Yellow;
                worksheet.Cell("B3").Style.Fill.BackgroundColor = XLColor.Yellow;
                worksheet.Cell("B4").Style.Fill.BackgroundColor = XLColor.Yellow;

                worksheet.Cell("A1").Value = "Teacher Name ";
                worksheet.Cell("B1").Value = aa[0].Subjects.Teachers.Employees.FirstName + " " + aa[0].Subjects.Teachers.Employees.LastName;

                worksheet.Cell("A2").Value = "Subject Name ";
                worksheet.Cell("B2").Value = aa[0].Subjects.SubjectName;

                worksheet.Cell("A3").Value = "Exam Type ";
                worksheet.Cell("B3").Value = aa[0].ExamTypes.ExamTypeName;

                worksheet.Cell("A4").Value = "Date ";
                worksheet.Cell("B4").Value = Convert.ToDateTime(aa[0].DateTime.ToString()).ToString("yyyy-MM-dd");
                int line = 7;
                foreach (var item in aa)
                {

                    worksheet.Cell("A" + line.ToString()).Value = item.Students.FirstName;
                    worksheet.Cell("B" + line.ToString()).Value = item.Students.Id;
                    worksheet.Cell("C" + line.ToString()).Value = item.ObtainedMarks;
                    worksheet.Cell("D" + line.ToString()).Value = item.TotalMarks;

                    line++;
                }

                worksheet.Rows().AdjustToContents();
                worksheet.Columns().AdjustToContents();
                workbook.SaveAs(aa[0].Subjects.SubjectName + "_" + startdate.ToString("yyyy-MM-dd") + ".xlsx");
            }

        }

        public static void GenerateMarkSheetByStudent(List<int> Studentsid, DateTime startdate, DateTime enddate)
        {
            foreach (var item in Studentsid)
            {
                GenerateMarkSheetByStudent(item, startdate, enddate);
            }
        }

        public static void GenerateMarkSheetByStudent(int Studentid, DateTime startdate, DateTime enddate)
        {
            
            //    var d = ex.ListOfExams;
            List<Exams> aa = Exams.ListOfExams.FindAll(su => su.StudentsId == Studentid && Convert.ToDateTime(su.DateTime) >= startdate || Convert.ToDateTime(su.DateTime) <= enddate);

            using (var workbook = new XLWorkbook())
            {

                var worksheet = workbook.Worksheets.Add("Sample Sheet");
                worksheet.Cell("A6").Value = "Subject Name";
                worksheet.Cell("B6").Value = "Date";
                worksheet.Cell("C6").Value = "Exam Type";
                worksheet.Cell("D6").Value = "Obtained Marks";
                worksheet.Cell("E6").Value = "Total Marks";

                worksheet.Cell("A6").Style.Font.Bold = true;
                worksheet.Cell("B6").Style.Font.Bold = true;
                worksheet.Cell("C6").Style.Font.Bold = true;
                worksheet.Cell("D6").Style.Font.Bold = true;
                worksheet.Cell("E6").Style.Font.Bold = true;

                worksheet.Cell("A1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Cell("A2").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Cell("A3").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Cell("B1").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Cell("B2").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Cell("B3").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                worksheet.Cell("A1").Style.Fill.BackgroundColor = XLColor.Yellow;
                worksheet.Cell("A2").Style.Fill.BackgroundColor = XLColor.Yellow;
                worksheet.Cell("A3").Style.Fill.BackgroundColor = XLColor.Yellow;
                worksheet.Cell("B1").Style.Fill.BackgroundColor = XLColor.Yellow;
                worksheet.Cell("B2").Style.Fill.BackgroundColor = XLColor.Yellow;
                worksheet.Cell("B3").Style.Fill.BackgroundColor = XLColor.Yellow;

                worksheet.Cell("A1").Value = "Std ID ";
                worksheet.Cell("B1").Value = aa[0].StudentsId;
                worksheet.Cell("A2").Value = "Std Name ";
                worksheet.Cell("B2").Value = aa[0].Students.FirstName + " " + aa[0].Students.LastName;
                worksheet.Cell("A3").Value = "Class Name";
                worksheet.Cell("B3").Value = aa[0].Subjects.Classes.ClassesName;
                int line = 7;
                foreach (var item in aa)
                {

                    worksheet.Cell("A" + line.ToString()).Value = item.Subjects.SubjectName;
                    worksheet.Cell("B" + line.ToString()).Value = Convert.ToDateTime(item.DateTime.ToString()).ToString("yyyy-MM-dd");
                    worksheet.Cell("C" + line.ToString()).Value = item.ExamTypes.ExamTypeName;
                    worksheet.Cell("D" + line.ToString()).Value = item.ObtainedMarks;
                    worksheet.Cell("E" + line.ToString()).Value = item.TotalMarks;




                    line++;
                }

                worksheet.Rows().AdjustToContents();
                worksheet.Columns().AdjustToContents();
                workbook.SaveAs(aa[0].StudentsId.ToString() + "_" + startdate.Year.ToString() + ".xlsx");
            }

        }

    }
}
