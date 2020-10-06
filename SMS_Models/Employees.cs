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
    class Employees:Persons
    {
        private static string Command;
        private static Database_Connection db = new Database_Connection();
        public int Id { get; set; }
        public string RegistrationID { get; set; }
        public float Salary { get; set; }
        public string IdentityCardUrl { get; set; }
        public string PostalAddress { get; set; }
        public string ResidentialAddress { get; set; }
        public string AggrementUrl { get; set; }
        public int PersonsId { get; set; }
        public int DesignationsId { get; set; }
        public int EmployeeTypesId { get; set; }
        public int LoginDetailsId { get; set; }
        public EmployeeTypes EmployeeTypes { get; set; }
        public LoginDetails LoginDetails { get; set; }
        public Designations Designations { get; set; }

        public override string ToString()
        {
            return "{ [ id: " + Id + ", PersonsId: " + PersonsId + ", LoginDetailsId: " + LoginDetailsId
                        + ", personId: " + PersonId + ", EmployeeTypesId: " + EmployeeTypesId + ", Designations: " + DesignationsId + "  ]"
                        + LoginDetails + EmployeeTypes + Designations + "}";
        }

        public static List<Employees> ListOfEmployees
        {
            get
            {
                return _GetEmployees();
            }
            private set
            {
            }
        }
        public Employees()
        {
            EmployeeTypes = new EmployeeTypes();
            LoginDetails = new LoginDetails();
            Designations = new Designations();
        }
        private static List<Employees> _GetEmployees()
        {
            List<Employees> employees = new List<Employees>();
            

            try
            {
                Command = @"select * from tblEmployees";
                db.cmd.CommandText = Command;
                db.con.Open();

                SqlDataReader rdr = db.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Employees singleEmployees = new Employees();
                    singleEmployees.Id = (int)rdr[0];
                    singleEmployees.RegistrationID = rdr[1].ToString();
                    singleEmployees.Salary = Convert.ToSingle(rdr[2]);
                    singleEmployees.IdentityCardUrl = rdr[3].ToString();
                    singleEmployees.PostalAddress = rdr[4].ToString();
                    singleEmployees.ResidentialAddress = rdr[5].ToString();
                    singleEmployees.AggrementUrl = rdr[6].ToString();
                    singleEmployees.PersonsId = (int)rdr[7];
                    singleEmployees.DesignationsId = (int)rdr[8];
                    singleEmployees.EmployeeTypesId = (int)rdr[9];
                    singleEmployees.LoginDetailsId = (int)rdr[10];

                    var emptypes = new EmployeeTypes();
                    singleEmployees.EmployeeTypes = EmployeeTypes.ListOfEmployeeTypes.SingleOrDefault(et => et.Id == singleEmployees.EmployeeTypesId);
                    var logindetails = new LoginDetails();
                    singleEmployees.LoginDetails = LoginDetails.ListOfLoginDetails.SingleOrDefault(ld => ld.Id == singleEmployees.LoginDetailsId);
                    var desig = new Designations();
                    singleEmployees.Designations = Designations.ListOfDesignations.SingleOrDefault(d => d.Id == singleEmployees.DesignationsId);
                    var a = Persons.ListOfPersons.SingleOrDefault(p => p.PersonId == singleEmployees.PersonsId);
                    singleEmployees.FirstName = a.FirstName;
                    singleEmployees.LastName = a.LastName;
                    singleEmployees.PersonId = a.PersonId;
                    singleEmployees.DateOfBirth = a.DateOfBirth;
                    singleEmployees.CNIC = a.CNIC;
                    singleEmployees.ImageUrlPath = a.ImageUrlPath;
                    singleEmployees.Phone1 = a.Phone1;
                    singleEmployees.Phone2 = a.Phone2;
                    singleEmployees.GendersId = a.GendersId;

                    singleEmployees.Genders.Id = a.Genders.Id;
                    singleEmployees.Genders.GenderName = a.Genders.GenderName;

                    employees.Add(singleEmployees);
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
            return employees;
        }

        public static int Add(Employees employee)
        {
            Persons person = new Persons();
            
            int retvalue = -1;
            try
            {
                employee.PersonsId= Persons.PersonAdd(employee);
                employee.LoginDetailsId = LoginDetails.Add(employee.LoginDetails);
                db.cmd.CommandType = CommandType.StoredProcedure;
                db.cmd.CommandText = "procEmployees_AddEmployees";
                db.cmd.Parameters.AddWithValue("@RegistrationID", employee.RegistrationID);
                db.cmd.Parameters.AddWithValue("@Salary", employee.Salary);
                db.cmd.Parameters.AddWithValue("@IdentityCardUrl", employee.IdentityCardUrl);
                db.cmd.Parameters.AddWithValue("@PostalAddress", employee.PostalAddress);
                db.cmd.Parameters.AddWithValue("@ResidentialAddress", employee.RegistrationID);
                db.cmd.Parameters.AddWithValue("@AggrementUrl", employee.AggrementUrl);
                db.cmd.Parameters.AddWithValue("@Person_ID", employee.PersonsId);
                db.cmd.Parameters.AddWithValue("@Designation_ID", employee.DesignationsId);
                db.cmd.Parameters.AddWithValue("@EmployeeType_ID", employee.EmployeeTypesId);
                db.cmd.Parameters.AddWithValue("@LoginDetail_ID", employee.LoginDetailsId);
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
                db.CloseDb(db.con,db.cmd);
            }

            return retvalue;

        }
        public static int Add(Employees employees, Designations designations)
        {
            int retvalue = -1;
            employees.DesignationsId = Designations.Add(designations);
            retvalue = Employees.Add(employees);
            return retvalue;
        }
        public static int Add(Employees employees, EmployeeTypes employeeTypes)
        {
            int retvalue = -1;
            employees.EmployeeTypesId = EmployeeTypes.Add(employeeTypes);
            retvalue = Employees.Add(employees);
            return retvalue;
        }
        public static int Add(Employees employees, EmployeeTypes employeeTypes, Designations designations)
        {
            int retvalue = -1;
            employees.EmployeeTypesId = EmployeeTypes.Add(employeeTypes);
            employees.DesignationsId = Designations.Add(designations);
            retvalue = Employees.Add(employees);
            return retvalue;
        }

        public static void Delete(int id)
        {
            try
            {
            db.cmd.CommandText = "delete from tblEmployees  where Id=@id";
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
        public static void Update(int empId, Employees employees)
        {
            try
            {

                Persons.PersonUpdate(employees.PersonId, employees);
            LoginDetails.Update(employees.LoginDetailsId, employees.LoginDetails);

            db.cmd.CommandText = @" update tblEmployees set RegistrationID=@regid,
                                  Salary=@salary,IdentityCardUrl=@idcardurl,
                                  PostalAddress=@postaladd,ResidentialAddress=@residadd,
                                  AggrementUrl=@aggurl,Person_ID=@pid,
                                  Designation_ID=@did,
                                  EmployeeType_ID=@eid,
                                  LoginDetail_ID=@lid where Id=@id;";
            db.cmd.Parameters.AddWithValue("@regid", employees.RegistrationID);
            db.cmd.Parameters.AddWithValue("@salary", employees.Salary);
            db.cmd.Parameters.AddWithValue("@idcardurl", employees.IdentityCardUrl);
            db.cmd.Parameters.AddWithValue("@postaladd", employees.PostalAddress);
            db.cmd.Parameters.AddWithValue("@residadd", employees.ResidentialAddress);
            db.cmd.Parameters.AddWithValue("@aggurl", employees.AggrementUrl);
            db.cmd.Parameters.AddWithValue("@pid", employees.PersonsId);
            db.cmd.Parameters.AddWithValue("@did", employees.DesignationsId);
            db.cmd.Parameters.AddWithValue("@eid ", employees.EmployeeTypesId);
            db.cmd.Parameters.AddWithValue("@lid ", employees.LoginDetailsId);


            db.cmd.Parameters.AddWithValue("@id", employees.Id);

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
        public static void Update(int empId, Employees employees, EmployeeTypes employeeTypes)
        {
            employees.EmployeeTypesId = EmployeeTypes.Add(employeeTypes);
            Employees.Update(empId, employees);
        }
        public static void Update(int empId, Employees employees, Designations designations)
        {
            employees.DesignationsId = Designations.Add(designations);
            Employees.Update(empId, employees);
        }
        public static void Update(int empId, Employees employees, EmployeeTypes employeeTypes, Designations designations)
        {
            employees.DesignationsId = Designations.Add(designations);
            employees.EmployeeTypesId = EmployeeTypes.Add(employeeTypes);
            Employees.Update(empId, employees);
        }

        public static void GenerateEmployeeCard(Employees employees)
        {
            Color backColor = Color.GhostWhite;
            Image img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);


            img.Dispose();
            drawing.Dispose();

            Bitmap A4 = new Bitmap(1240, 1920);
            img = A4;



            drawing = Graphics.FromImage(img);

            drawing.Clear(backColor);


            FontFamily fontFamily = new FontFamily("Arial");
            Font font = new Font(fontFamily, 16, FontStyle.Bold, GraphicsUnit.Pixel);
            Brush textBrush = new SolidBrush(Color.Red);


            Image src = new Bitmap("logo.png");

            drawing.DrawImage(src, new Rectangle(20, 20, 350, 350));


            font = new Font(fontFamily, 52, FontStyle.Bold, GraphicsUnit.Pixel);
            textBrush = new SolidBrush(Color.Black);
            drawing.DrawString("Tapas Logistics Limited", font, textBrush, 390, 110);

            font = new Font(fontFamily, 42, FontStyle.Regular, GraphicsUnit.Pixel);
            textBrush = new SolidBrush(Color.Black);
            drawing.DrawString("5th Streat Korangi Two Number,Karachi", font, textBrush, 390, 170);

            src = new Bitmap("t1.jpg");
            drawing.DrawImage(src, new Rectangle(380, 410, 500, 500));

            font = new Font(fontFamily, 67, FontStyle.Bold, GraphicsUnit.Pixel);
            textBrush = new SolidBrush(Color.Black);

            drawing.DrawString(employees.FirstName + " " + employees.LastName, font, textBrush, 360, 920);

            font = new Font(fontFamily, 45, FontStyle.Regular, GraphicsUnit.Pixel);
            textBrush = new SolidBrush(Color.DimGray);
            drawing.DrawString(employees.Designations.DesignationName, font, textBrush, 380, 995);

            font = new Font(fontFamily, 45, FontStyle.Regular, GraphicsUnit.Pixel);
            textBrush = new SolidBrush(Color.Red);
            drawing.DrawString("__________________________________________", font, textBrush, 80, 1015);

            font = new Font(fontFamily, 52, FontStyle.Bold, GraphicsUnit.Pixel);
            textBrush = new SolidBrush(Color.DimGray);
            drawing.DrawString(employees.CNIC, font, textBrush, 125, 1100);

            DateTime dob = Convert.ToDateTime(employees.DateOfBirth);
            drawing.DrawString(dob.ToShortDateString(), font, textBrush, 125, 1190);
            drawing.DrawString(employees.Phone1 + "    " + employees.Phone2, font, textBrush, 125, 1280);
            drawing.DrawString(employees.ResidentialAddress, font, textBrush, 125, 1370);
            drawing.DrawString(employees.PostalAddress, font, textBrush, 125, 1460);
            DateTime d = DateTime.Now;
            d = d.AddYears(4);
            string year = d.ToShortDateString();
            drawing.DrawString("Valid Till: " + year, font, textBrush, 125, 1550);

            FontFamily fontFamily1 = new FontFamily("Arial Black");
            font = new Font(fontFamily1, 52, FontStyle.Bold, GraphicsUnit.Pixel);
            textBrush = new SolidBrush(Color.DimGray);
            drawing.DrawString(employees.Genders.GenderName, font, textBrush, 700, 1190);
            font = new Font(fontFamily, 45, FontStyle.Regular, GraphicsUnit.Pixel);
            textBrush = new SolidBrush(Color.Red);
            drawing.DrawString("__________________________________________", font, textBrush, 80, 340);

            font = new Font(fontFamily, 78, FontStyle.Bold, GraphicsUnit.Pixel);
            textBrush = new SolidBrush(Color.Red);
            drawing.DrawString("ID: " + employees.RegistrationID, font, textBrush, 370, 1750);

            font = new Font(fontFamily, 45, FontStyle.Regular, GraphicsUnit.Pixel);
            textBrush = new SolidBrush(Color.Red);
            drawing.DrawString("__________________________________________________", font, textBrush, 0, 1650);
            drawing.DrawString("__________________________________________________", font, textBrush, 0, 1850);





            drawing.Save();

            textBrush.Dispose();
            drawing.Dispose();

            Image a = img;
            a.Save("IdentityCards/Employees/" + employees.RegistrationID.ToString() + ".jpg");

        }
        public static void GenerateEmployeeCard(List<Employees> ListOfEmployees)
        {


            foreach (var item in ListOfEmployees)
            {
                GenerateEmployeeCard(item);
            }

        }

        public static void GenerateEmployeeSalayReceipt(List<Employees> employees)
        {
            foreach (var item in employees)
            {
                GenerateEmployeeSalayReceipt(item);
            }
        }
        public static void GenerateEmployeeSalayReceipt(Employees employees)
        {
            Color backColor = Color.GhostWhite;
            Image img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);


            img.Dispose();
            drawing.Dispose();


            Bitmap A4 = new Bitmap(1240, 1754); ;
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
            drawing.DrawString("Salary Receipt", font, textBrush, 390, 400);

            font = new Font(fontFamily, 42, FontStyle.Regular, GraphicsUnit.Pixel);
            textBrush = new SolidBrush(Color.Black);
            drawing.DrawString("5th Streat Korangi Two Number,Karachi", font, textBrush, 390, 170);

            drawing.DrawString(employees.FirstName + " " + employees.LastName, font, textBrush, 450, 520);
            drawing.DrawString(employees.Designations.DesignationName, font, textBrush, 450, 590);
            drawing.DrawString(DateTime.Now.ToShortDateString(), font, textBrush, 450, 660);
            drawing.DrawString("Employee Name: ___________________", font, textBrush, 80, 520);
            drawing.DrawString("Designation: _______________________", font, textBrush, 80, 590);
            drawing.DrawString("Date: _____________________________", font, textBrush, 80, 660);
            //  src = new Bitmap("t1.jpg");
            //  drawing.DrawImage(src, new Rectangle(380, 380, 500, 500));


            textBrush = new SolidBrush(Color.Black);
            font = new Font(fontFamily, 67, FontStyle.Bold, GraphicsUnit.Pixel);
            drawing.DrawString("____________________________________________________________________________________________________", font, textBrush, 0, 690);
            drawing.DrawString("____________________________________________________________________________________________________", font, textBrush, 0, 1000);

            font = new Font(fontFamily, 42, FontStyle.Regular, GraphicsUnit.Pixel);
            textBrush = new SolidBrush(Color.Black);
            drawing.DrawString(employees.Salary.ToString(), font, textBrush, 450, 820);


            var finetot = (EmployeeFines.ListOfEmployeeFines.FindAll((f => f.EmployeesId == employees.Id)).Select(x => x.Fine)).Sum();
            drawing.DrawString(finetot.ToString(), font, textBrush, 450, 890);
            drawing.DrawString((employees.Salary - finetot).ToString(), font, textBrush, 450, 960);

            drawing.DrawString("Salary: _____________________________", font, textBrush, 80, 820);
            drawing.DrawString("Fine: _______________________________", font, textBrush, 80, 890);
            drawing.DrawString("Actual Amount: __________________________", font, textBrush, 80, 960);

            textBrush = new SolidBrush(Color.Black);
            font = new Font(fontFamily, 42, FontStyle.Regular, GraphicsUnit.Pixel);
            drawing.DrawString("Principle Sign: ___________________", font, textBrush, 80, 1200);
            drawing.DrawString("Employee Sign: __________________", font, textBrush, 80, 1400);

            drawing.Save();

            textBrush.Dispose();
            drawing.Dispose();

            Image a = img;
            a.Save("Receipts/Employees/" + employees.Id.ToString() + ".jpg");


        }

    }
}
