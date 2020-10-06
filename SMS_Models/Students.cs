using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace SMS_Models
{
    class Students : Persons
    {
        private static string Command;
        private static Database_Connection db = new Database_Connection();

        public int Id { get; set; }
        public string RegistrationId { get; set; }
        public float DiscountPercentage { get; set; }
        public string IdentityCardUrl { get; set; }
        public string AdmissionCard { get; set; }
        public int PersonsId { get; private set; }
        public string PostalAddress { get; set; }
        public string ResidentialAddress { get; set; }
        public int LoginDetailsId { get; set; }
        public int ParentsId { get; set; }
        public int ClassesId { get; set; }
        public LoginDetails LoginDetails { get; set; }
        public Parents Parents { get; set; }
        public Classes Classes { get; set; }

        public override string ToString()
        {
            return "{ [ id: " + Id + ", PersonsId: " + PersonsId + ", LoginDetailsId: " + LoginDetailsId 
                        + ", personId: " + PersonId + ", ParentsId: " + ParentsId + ", classesId: " + ClassesId + "  ]" 
                        + LoginDetails + Parents + Classes + "}";
        }
        public static List<Students> ListOfStudents
        {
            get
            {
                return _GetListOfStudents();
            }
            set
            {

            }
        }

        public Students()
        {
            LoginDetails = new LoginDetails();
            Parents = new Parents();
            Classes = new Classes();
        }

        private static List<Students> _GetListOfStudents()
        {
            List<Students> students = new List<Students>();
            try
            {
                Command = @"select * from tblStudents std join tblLoginDetails lgn
                    on std.LoginDetail_ID=lgn.Id join tblParents pts
                    on std.Parent_ID=pts.Id join tblClasses cls
                    on std.Class_ID=cls.Id";
                db.cmd.CommandText = Command;
                db.con.Open();

                SqlDataReader rdr = db.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Students singleStudents = new Students();
                    singleStudents.Id = (int)rdr[0];
                    singleStudents.RegistrationId = rdr[1].ToString();
                    singleStudents.DiscountPercentage = Convert.ToSingle(rdr[2]);
                    singleStudents.IdentityCardUrl = rdr[3].ToString();
                    singleStudents.AdmissionCard = rdr[4].ToString();
                    singleStudents.PostalAddress = rdr[5].ToString();
                    singleStudents.ResidentialAddress = rdr[6].ToString();
                    singleStudents.PersonsId = (int)rdr[7];
                    singleStudents.LoginDetailsId = (int)rdr[8];
                    singleStudents.ParentsId = (int)rdr[9];
                    singleStudents.ClassesId = (int)rdr[10];

                    var logindetail = new LoginDetails();
                    singleStudents.LoginDetails = LoginDetails.ListOfLoginDetails.SingleOrDefault(lg => lg.Id == singleStudents.LoginDetailsId);

                    //singleStudents.LoginDetails.Id= (int)rdr[11];
                    //singleStudents.LoginDetails.Username = rdr[12].ToString();
                    //singleStudents.LoginDetails.Password = rdr[13].ToString();
                    //singleStudents.LoginDetails.AllowAccess = (int)rdr[14];

                    var parents = new Parents();
                    singleStudents.Parents = Parents.ListOfParents.SingleOrDefault(p => p.Id == singleStudents.ParentsId);
                    //singleStudents.Parents.Id = (int)rdr[15];
                    //singleStudents.Parents.PersonsId = (int)rdr[16];
                    //singleStudents.Parents.LoginDetailsId = (int)rdr[17];


                    var classes = new Classes();
                    singleStudents.Classes = Classes.ListOfClasses.SingleOrDefault(cls => cls.Id == singleStudents.ClassesId);

                    //singleStudents.Classes.Id = (int)rdr[18];
                    //singleStudents.Classes.ClassesName = rdr[19].ToString() ;
                    //singleStudents.Classes.GradesId = (int)rdr[20];

                    var person = Persons.ListOfPersons.SingleOrDefault(p => p.PersonId == singleStudents.PersonsId);
                    singleStudents.Id = person.PersonId;
                    singleStudents.FirstName = person.FirstName;
                    singleStudents.LastName = person.LastName;
                    singleStudents.DateOfBirth = person.DateOfBirth;
                    singleStudents.CNIC = person.CNIC;
                    singleStudents.ImageUrlPath = person.ImageUrlPath;
                    singleStudents.Phone1 = person.Phone1;
                    singleStudents.Phone2 = person.Phone2;
                    singleStudents.GendersId = person.GendersId;

                    singleStudents.Genders.Id = person.Genders.Id;
                    singleStudents.Genders.GenderName = person.Genders.GenderName;

                    students.Add(singleStudents);
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

            return students;
        }

        public static int Add(Students students)
        {
            int retvalue = -1;
            try
            {
                Persons persons = new Persons();
                persons.PersonId = students.PersonsId;
                persons.FirstName = students.FirstName;
                persons.LastName = students.LastName;
                persons.DateOfBirth = students.DateOfBirth;
                persons.CNIC = students.CNIC;
                persons.ImageUrlPath = students.ImageUrlPath;
                persons.Phone1 = students.Phone1;
                persons.Phone2 = students.Phone2;
                persons.GendersId = students.GendersId;

                students.PersonsId = Persons.PersonAdd(persons);

                students.LoginDetailsId = LoginDetails.Add(students.LoginDetails);


                db.cmd.CommandType = CommandType.StoredProcedure;
                db.cmd.CommandText = "procStudents_AddStudents";
                db.cmd.Parameters.AddWithValue("@RegistrationID", students.RegistrationId);
                db.cmd.Parameters.AddWithValue("@DiscountPercentage", students.DiscountPercentage);
                db.cmd.Parameters.AddWithValue("@IdentityCardUrl", students.IdentityCardUrl);
                db.cmd.Parameters.AddWithValue("@AdmissionCard", students.AdmissionCard);
                db.cmd.Parameters.AddWithValue("@PostalAddress", students.PostalAddress);
                db.cmd.Parameters.AddWithValue("@ResidentialAddress", students.ResidentialAddress);
                db.cmd.Parameters.AddWithValue("@Person_ID", students.PersonsId);
                db.cmd.Parameters.AddWithValue("@LoginDetail_ID", students.LoginDetailsId);
                db.cmd.Parameters.AddWithValue("@Parent_ID", students.ParentsId);
                db.cmd.Parameters.AddWithValue("@Class_ID", students.ClassesId);
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
        public static int Add(Students students, Parents parents)
        {
            int retvalue = -1;
            students.ParentsId = Parents.Add(parents);
            retvalue = Students.Add(students);
            return retvalue;
        }
        public static int Add(Students students, Classes classes)
        {
            int retvalue = -1;
            students.ClassesId = Classes.Add(classes);
            retvalue = Students.Add(students);

          

            return retvalue;

        }
        public static int Add(Students students, Parents parents, Classes classes)
        {
            int retvalue = -1;
            students.ParentsId = Parents.Add(parents);
            students.ClassesId = Classes.Add(classes);
            retvalue = Students.Add(students);
            return retvalue;

        }

        public static void Update(int id, Students std)
        {
            try
            {
                Parents.Update(std.ParentsId, std.Parents);
                PersonUpdate(std.PersonId, std);
                LoginDetails.Update(std.LoginDetailsId, std.LoginDetails);

                db.cmd.CommandText = @"UPDATE [dbo].[tblStudents]
   SET [RegistrationID] = @regid
      ,[DiscountPercentage] =  @disper
      ,[IdentityCardUrl] =  @idcard
      ,[AdmissionCard] =  @addmission
      ,[PostalAddress] =  @post
      ,[ResidentialAddress] =  @resi
      ,[Person_ID] =  @perid1
      ,[LoginDetail_ID] =  @lid1
      ,[Parent_ID] =  @pid1
      ,[Class_ID] =  @cid1
 WHERE Id=@id";
                db.cmd.Parameters.AddWithValue("@regid", std.RegistrationId);
                db.cmd.Parameters.AddWithValue("@disper", std.DiscountPercentage);
                db.cmd.Parameters.AddWithValue("@idcard", std.IdentityCardUrl);
                db.cmd.Parameters.AddWithValue("@addmission", std.AdmissionCard);
                db.cmd.Parameters.AddWithValue("@post", std.PostalAddress);
                db.cmd.Parameters.AddWithValue("@resi", std.ResidentialAddress);
                db.cmd.Parameters.AddWithValue("@perid1", std.PersonsId);
                db.cmd.Parameters.AddWithValue("@lid1", std.LoginDetailsId);
                db.cmd.Parameters.AddWithValue("@pid1 ", std.ParentsId);
                db.cmd.Parameters.AddWithValue("@cid1 ", std.ClassesId);


                db.cmd.Parameters.AddWithValue("@id", std.Id);

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

   db.cmd.CommandText = "delete from tblStudents  where Id=@id";
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
        public static void GenerateStudentCard(Students students)
        {
            Color backColor = Color.GhostWhite;
            Image img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);


            img.Dispose();
            drawing.Dispose();


            Bitmap A4 = new Bitmap(1240, 2000);
            img = A4;
            drawing = Graphics.FromImage(img);

            drawing.Clear(backColor);


            FontFamily fontFamily = new FontFamily("Arial");
            Font font = new Font(fontFamily, 16, FontStyle.Bold, GraphicsUnit.Pixel);

            Brush textBrush = new SolidBrush(Color.Brown);
            Image src = new Bitmap("logo.png");

            drawing.DrawImage(src, new Rectangle(20, 20, 350, 350));

            font = new Font(fontFamily, 52, FontStyle.Bold, GraphicsUnit.Pixel);
            textBrush = new SolidBrush(Color.Black);
            drawing.DrawString("Tapas Logistics Limited", font, textBrush, 390, 110);


            font = new Font(fontFamily, 42, FontStyle.Regular, GraphicsUnit.Pixel);
            textBrush = new SolidBrush(Color.Black);
            drawing.DrawString("5th Streat Korangi Two Number,Karachi", font, textBrush, 390, 170);

            src = new Bitmap("t1.jpg");
            drawing.DrawImage(src, new Rectangle(380, 380, 500, 500));


            textBrush = new SolidBrush(Color.Blue);
            font = new Font(fontFamily, 67, FontStyle.Bold, GraphicsUnit.Pixel);
            drawing.DrawString("____________________________________________________________________________________________________", font, textBrush, 0, 840);
            drawing.DrawString(students.FirstName + " " + students.LastName, font, textBrush, 360, 920);
            drawing.DrawString(students.RegistrationId, font, textBrush, 370, 980);
            drawing.DrawString("____________________________________________________________________________", font, textBrush, 0, 990);
            textBrush = new SolidBrush(Color.Black);
            font = new Font(fontFamily, 72, FontStyle.Regular, GraphicsUnit.Pixel);

            DateTime dob = Convert.ToDateTime(students.DateOfBirth);
            drawing.DrawString(dob.ToShortDateString(), font, textBrush, 50, 1070);
            drawing.DrawString(students.Genders.GenderName, font, textBrush, 50, 1170);
            drawing.DrawString(students.Classes.ClassesName, font, textBrush, 50, 1270);
            drawing.DrawString(students.ResidentialAddress, font, textBrush, 50, 1370);
            drawing.DrawString(students.PostalAddress, font, textBrush, 50, 1470);
            drawing.DrawString(students.Phone1, font, textBrush, 50, 1570);

            DateTime d = DateTime.Now;
            d = d.AddYears(1);
            string year = d.ToShortDateString();
            drawing.DrawString("Valid Till: " + year, font, textBrush, 50, 1670);
            drawing.DrawString("Principal:__________", font, textBrush, 600, 1880);
            drawing.DrawString(students.Parents.FirstName + " " + students.Parents.LastName, font, textBrush, 600, 1170);
            drawing.DrawString(students.Parents.Phone1, font, textBrush, 600, 1270);
            FontFamily fontFamily1 = new FontFamily("Arial Black");
            textBrush = new SolidBrush(Color.Black);
            font = new Font(fontFamily1, 72, FontStyle.Regular, GraphicsUnit.Pixel);
            drawing.DrawString("Parent Details", font, textBrush, 600, 1070);




            drawing.Save();

            textBrush.Dispose();
            drawing.Dispose();

            Image a = img;
            a.Save("IdentityCards/Students/" + students.RegistrationId.ToString() + ".jpg");


        }
        public static void GenerateStudentsCardByClass(Classes classes)
        {
            Students std = new Students();
            var stds = Students.ListOfStudents.FindAll(s => s.ClassesId == classes.Id);

            foreach (var item in stds)
            {
                GenerateStudentCard(item);
            }

        }
        public static void GenerateStudentsCard(List<Students> ListOfStudents)
        {


            foreach (var item in ListOfStudents)
            {
                GenerateStudentCard(item);
            }

        }

        public static void GenerateFeeVoucher(List<Students> students)
        {
            foreach (var item in students)
            {
                GenerateFeeVoucher(item);
            }
        }
        public static void GenerateFeeVoucher(Students students)
        {
            Color backColor = Color.GhostWhite;
            //first, create a dummy bitmap just to get a graphics object
            Image img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);

            //measure the string to see how big the image needs to be
            //   SizeF textSize = drawing.MeasureString(text, font);

            //free up the dummy image and old graphics object
            img.Dispose();
            drawing.Dispose();

            //create a new image of the right size
            //  img = new Bitmap((int)textSize.Width, (int)textSize.Height);

            Bitmap A4 = new Bitmap(1240, 1754);
            A4.SetResolution(150, 150);
            A4.RotateFlip(RotateFlipType.Rotate90FlipNone);
            float DivideSizeThree = 584.6666666666667f;
            //img = new Bitmap(250, 370);
            img = A4;


            drawing = Graphics.FromImage(img);

            //paint the background
            drawing.Clear(backColor);


            FontFamily fontFamily = new FontFamily("Arial");
            Font font = new Font(fontFamily, 16, FontStyle.Bold, GraphicsUnit.Pixel);
            //create a brush for the text
            Brush textBrush = new SolidBrush(Color.Black);

            //lines dividing in three
            for (int i = 0; i < 1240; i = i + 14)
            {
                drawing.DrawString("|", font, textBrush, DivideSizeThree, i);
            }
            for (int i = 0; i < 1240; i = i + 15)
            {
                drawing.DrawString("|", font, textBrush, DivideSizeThree + DivideSizeThree, i);
            }
            float settingall = 0;

            for (int j = 0; j < 3; j++)
            {
                if (j == 1)
                {
                    settingall = DivideSizeThree + 5;
                }
                if (j == 2)
                {
                    settingall = DivideSizeThree + 5 + DivideSizeThree;
                }

                fontFamily = new FontFamily("Arial");
                font = new Font(fontFamily, 25, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Pixel);
                textBrush = new SolidBrush(Color.Black);

                drawing.DrawString("Royal Falcon Grammar School", font, textBrush, 130 + settingall, 10);

                fontFamily = new FontFamily("Arial");
                font = new Font(fontFamily, 18, FontStyle.Regular, GraphicsUnit.Pixel);
                textBrush = new SolidBrush(Color.Black);
                drawing.DrawString("35-Larex Colony,Data Nagar,Badami Bagh,Karachi", font, textBrush, 130 + settingall, 40);
                drawing.DrawString("Contact: 0333-5541615,021-47848142", font, textBrush, 130 + settingall, 60);
                //drawing.DrawString("|", font, textBrush, DivideSizeThree, 20);
                //  drawing.DrawString("|", font, textBrush, 100, 16);
                Image src = new Bitmap("logo.png");

                drawing.DrawImage(src, new Rectangle(5 + Convert.ToInt32(settingall), 5, 110, 90));

                fontFamily = new FontFamily("Arial");
                font = new Font(fontFamily, 30, FontStyle.Bold, GraphicsUnit.Pixel);
                textBrush = new SolidBrush(Color.Black);

                drawing.DrawString("Fee Voucher", font, textBrush, 180 + settingall, 115);


                fontFamily = new FontFamily("Arial");
                font = new Font(fontFamily, 25, FontStyle.Bold, GraphicsUnit.Pixel);
                textBrush = new SolidBrush(Color.Black);
                drawing.DrawString("__________________________________________", font, textBrush, 0 + settingall, 80);
                drawing.DrawString("__________________________________________", font, textBrush, 0 + settingall, 130);

                for (int i = 150; i < 875; i = i + 15)
                {
                    drawing.DrawString("|", font, textBrush, 240 + settingall, i);
                }

                drawing.DrawString(students.Id.ToString(), font, textBrush, 180 + settingall, 165);

                drawing.DrawString("Serial No.", font, textBrush, 10 + settingall, 165);
                drawing.DrawString("|", font, textBrush, 140 + settingall, 150);
                drawing.DrawString("|", font, textBrush, 140 + settingall, 170);

                drawing.DrawString(DateTime.Now.ToShortDateString(), font, textBrush, 360 + settingall, 165);

                drawing.DrawString("Date", font, textBrush, 260 + settingall, 165);
                drawing.DrawString("|", font, textBrush, 330 + settingall, 150);
                drawing.DrawString("|", font, textBrush, 330 + settingall, 170);
                drawing.DrawString("__________________________________________", font, textBrush, 0 + settingall, 175);

                drawing.DrawString(students.FirstName + " " + students.LastName, font, textBrush, 260 + settingall, 210);
                drawing.DrawString(students.Parents.FirstName + " " + students.Parents.LastName, font, textBrush, 260 + settingall, 265);
                drawing.DrawString(students.Classes.ClassesName, font, textBrush, 260 + settingall, 315);
                drawing.DrawString(students.RegistrationId, font, textBrush, 260 + settingall, 360);
                drawing.DrawString("AMOUNT [PKR]", font, textBrush, 260 + settingall, 410);

               
                var feetot = FeeSchedules.ListOfFeeSchedules.SingleOrDefault(f => f.GradesId == students.Classes.Grades.Id).Fee;
                drawing.DrawString(feetot.ToString(), font, textBrush, 260 + settingall, 460);
                drawing.DrawString(students.DiscountPercentage.ToString() + "%", font, textBrush, 260 + settingall, 510);
                
                var finetot = (StudentFines.ListOfStudentFines.FindAll((f => f.StudentsId == students.Id)).Select(x => x.Fine)).Sum();
                drawing.DrawString(finetot.ToString(), font, textBrush, 260 + settingall, 560);

                var totaldis = ((students.DiscountPercentage / 100) * feetot);
                var totalfee = (feetot + finetot) - totaldis;
                drawing.DrawString(totalfee.ToString(), font, textBrush, 260 + settingall, 860);



                drawing.DrawString("Student Name ", font, textBrush, 25 + settingall, 210);
                drawing.DrawString("__________________________________________", font, textBrush, 0 + settingall, 225);
                drawing.DrawString("Father Name ", font, textBrush, 25 + settingall, 265);
                drawing.DrawString("__________________________________________", font, textBrush, 0 + settingall, 275);
                drawing.DrawString("Class Name", font, textBrush, 25 + settingall, 315);
                drawing.DrawString("__________________________________________", font, textBrush, 0 + settingall, 325);
                drawing.DrawString("Roll Number  ", font, textBrush, 25 + settingall, 360);
                drawing.DrawString("__________________________________________", font, textBrush, 0 + settingall, 375);
                drawing.DrawString("Particulars  ", font, textBrush, 25 + settingall, 410);
                drawing.DrawString("__________________________________________", font, textBrush, 0 + settingall, 425);
                drawing.DrawString("Tution Fee  ", font, textBrush, 25 + settingall, 460);
                drawing.DrawString("__________________________________________", font, textBrush, 0 + settingall, 475);
                drawing.DrawString("Discount  ", font, textBrush, 25 + settingall, 510);
                drawing.DrawString("__________________________________________", font, textBrush, 0 + settingall, 525);
                drawing.DrawString("Fine  ", font, textBrush, 25 + settingall, 560);
                drawing.DrawString("__________________________________________", font, textBrush, 0 + settingall, 575);
                drawing.DrawString("  ", font, textBrush, 25 + settingall, 610);
                drawing.DrawString("__________________________________________", font, textBrush, 0 + settingall, 625);
                drawing.DrawString("  ", font, textBrush, 25 + settingall, 660);
                drawing.DrawString("__________________________________________", font, textBrush, 0 + settingall, 675);
                drawing.DrawString("  ", font, textBrush, 25 + settingall, 710);
                drawing.DrawString("__________________________________________", font, textBrush, 0 + settingall, 725);
                drawing.DrawString("  ", font, textBrush, 25 + settingall, 760);
                drawing.DrawString("__________________________________________", font, textBrush, 0 + settingall, 775);
                drawing.DrawString("  ", font, textBrush, 25 + settingall, 810);
                drawing.DrawString("__________________________________________", font, textBrush, 0 + settingall, 825);
                drawing.DrawString("Total Amount  ", font, textBrush, 25 + settingall, 860);
                drawing.DrawString("__________________________________________", font, textBrush, 0 + settingall, 875);

                drawing.DrawString("___________________", font, textBrush, 0 + settingall, 980);
                drawing.DrawString("___________", font, textBrush, 380 + settingall, 980);
                drawing.DrawString("Principle Sign", font, textBrush, 20 + settingall, 1020);
                drawing.DrawString("Stamp", font, textBrush, 420 + settingall, 1020);
                drawing.DrawString("Note:", font, textBrush, 10 + settingall, 1060);
                font = new Font(fontFamily, 20, FontStyle.Bold, GraphicsUnit.Pixel);
                drawing.DrawString("the sad hsdh dh hdah dh dh dsh dhih hfjk gfhi", font, textBrush, 10 + settingall, 1090);
                drawing.DrawString("the sad hsdh dh hdah dh dh dsh dhih hfjk gfhi", font, textBrush, 10 + settingall, 1110);
                drawing.DrawString("the sad hsdh dh hdah dh dh dsh dhih hfjk gfhi", font, textBrush, 10 + settingall, 1130);

            }


            font = new Font(fontFamily, 25, FontStyle.Bold, GraphicsUnit.Pixel);
            drawing.DrawString("__________________________________________", font, textBrush, 0, 1135);
            drawing.DrawString("__________________________________________", font, textBrush, 0, 1205);

            font = new Font(fontFamily, 35, FontStyle.Bold, GraphicsUnit.Pixel);
            drawing.DrawString("Record Copy", font, textBrush, 180, 1177);

            font = new Font(fontFamily, 25, FontStyle.Bold, GraphicsUnit.Pixel);

            drawing.DrawString("__________________________________________", font, textBrush, 0 + DivideSizeThree, 1135);
            drawing.DrawString("__________________________________________", font, textBrush, 0 + DivideSizeThree, 1205);

            font = new Font(fontFamily, 35, FontStyle.Bold, GraphicsUnit.Pixel);
            drawing.DrawString("School Copy", font, textBrush, 180 + DivideSizeThree, 1177);

            font = new Font(fontFamily, 25, FontStyle.Bold, GraphicsUnit.Pixel);

            drawing.DrawString("__________________________________________", font, textBrush, 0 + DivideSizeThree + DivideSizeThree, 1135);
            drawing.DrawString("__________________________________________", font, textBrush, 0 + DivideSizeThree + DivideSizeThree, 1205);

            font = new Font(fontFamily, 35, FontStyle.Bold, GraphicsUnit.Pixel);
            drawing.DrawString("Parent Copy", font, textBrush, 180 + DivideSizeThree + DivideSizeThree, 1177);



            drawing.Save();

            textBrush.Dispose();
            drawing.Dispose();

            Image a = img;
            a.Save("Receipts/Students/" + students.Id.ToString() + ".jpg");

        }

    }
}
