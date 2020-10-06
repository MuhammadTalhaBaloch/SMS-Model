using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_Models
{
    class Program
    {
        static void Main(string[] args)
        {
            //SalaryRecords s = new SalaryRecords();
            //s.DateTime = DateTime.Now;
            //s.Paid = true;
            //s.PayableAmount = 1000;
            //s.EmployeesId = 1;
            //s.AddedByEmployesId = 2;
            //s.Employees = Employees.ListOfEmployees[0];
            //s.AddedByEmployees = Employees.ListOfEmployees[1];
            //SalaryRecords.Add(s);


            #region OBJECTS
            //Classes classes = new Classes();
            //ClassSchedules classschedules = new ClassSchedules();
            //ClassTimings classtimings = new ClassTimings();
            //Designations designations = new Designations();
            //DocumentUrls documenturls = new DocumentUrls();
            //EmployeeAttendanceRecords employeeattandancerecord = new EmployeeAttendanceRecords();
            //EmployeeAttendanceStatuses employeeattendancestatuses = new EmployeeAttendanceStatuses();
            //EmployeeDocuments employeedocuments = new EmployeeDocuments();
            //EmployeeFines employeefines = new EmployeeFines();
            //EmployeeQualifications employeequalification = new EmployeeQualifications();
            //Employees empoyees = new Employees();
            //EmployeeSkills employeeskills = new EmployeeSkills();
            //EmployeeTypes employeetypes = new EmployeeTypes();
            //Exams exams = new Exams();
            //ExamTypes examtypes = new ExamTypes();
            //ExpenseRecords expenserecords = new ExpenseRecords();
            //ExpenseTypes expensetypes = new ExpenseTypes();
            //FeeRecords feerecords = new FeeRecords();
            //FeeSchedules feeschedules = new FeeSchedules();
            //FeeTypes feetypes = new FeeTypes();
            //FineStatuses finestatuses = new FineStatuses();
            //Genders genders = new Genders();
            //Grades grades = new Grades();
            //LoginDetails logindetails = new LoginDetails();
            //Parents parents = new Parents();
            //Persons persons = new Persons();
            //Qualifications qualifications = new Qualifications();
            //SalaryRecords salaryrecords = new SalaryRecords();
            //Skills skills = new Skills();
            //StudentAttendanceRecords studentattendancerecords = new StudentAttendanceRecords();
            //StudentAttendanceStatuses studentattendancestatuses = new StudentAttendanceStatuses();
            //StudentDocuments studentdocuments = new StudentDocuments();
            //StudentFines studentfines = new StudentFines();
            //Students students = new Students();
            //Subjects subjects = new Subjects();
            //Teachers teacher = new Teachers();
            #endregion

            #region DELTED
            //classes.Delete(1);
            //classschedules.Delete(1);
            //classtimings.Delete(1);
            //designations.Delete(1);
            //documenturls.Delete(1);
            //employeeattandancerecord.Delete(1);
            //employeeattendancestatuses.Delete(1);
            //employeedocuments.Delete(1);
            //employeefines.Delete(1);
            //employeequalification.Delete(1);
            //empoyees.Delete(1);
            //employeeskills.Delete(1);
            //employeetypes.Delete(1); 
            //exams.Delete(1);
            //examtypes.Delete(1); 
            //expenserecords.Delete(1);
            //expensetypes.Delete(1);
            //feerecords.Delete(1);
            //feeschedules.Delete(1); 
            //feetypes.Delete(1); 
            //finestatuses.Delete(1);
            //genders.Delete(1);
            //grades.Delete(1);
            ////logindetails.Delete(1);
            //parents.Delete(1);
            ////persons.PersonDelete(1);
            //qualifications.Delete(1);
            //salaryrecords.Delete(1);
            //skills.Delete(1);
            //studentattendancerecords.Delete(1);
            //studentattendancestatuses.Delete(1);
            //studentdocuments.Delete(1);
            //studentfines.Delete(1);
            //students.Delete(1);
            //subjects.Delete(1);
            //teacher.Delete(1);
            #endregion

            #region GENDER
            //Genders addgender = new Genders { GenderName = "Maleee" };
            //Genders.Add(addgender);
            //Genders.ListOfGenders.ForEach(Console.WriteLine);
            //addgender = Genders.ListOfGenders[2];
            //addgender.GenderName = "testtt";
            //Genders.Update(addgender.Id, addgender);
            //Genders.ListOfGenders.ForEach(Console.WriteLine);
            #endregion

            #region FineStatuses

            //FineStatuses testfinestatuses = new FineStatuses();
            //testfinestatuses.FineStatusName = "testfine";
            //FineStatuses.Add(testfinestatuses);
            //FineStatuses.ListOfFineStatuses.ForEach(Console.WriteLine);
            // testfinestatuses = FineStatuses.ListOfFineStatuses[0];
            //testfinestatuses.FineStatusName = "testttt";
            //FineStatuses.Update(testfinestatuses.Id,testfinestatuses);
            //FineStatuses.ListOfFineStatuses.ForEach(Console.WriteLine);

            #endregion

            #region SKILLS
            //var skills = new Skills();
            //skills.SkillName = "C#";
            //Skills.Add(skills);
            //Skills.ListOfSkills.ForEach(Console.WriteLine);
            //skills = Skills.ListOfSkills[0];
            //skills.SkillName = "C##";
            //Skills.Update(skills.Id, skills);
            //Skills.ListOfSkills.ForEach(Console.WriteLine);
            #endregion

            #region QUALIFICATIONS
            //var qualif = new Qualifications();
            //qualif.QualificationName = "postergra";
            //Qualifications.Add(qualif);
            //Qualifications.ListOfQualifications.ForEach(Console.WriteLine);
            //qualif = Qualifications.ListOfQualifications[0];
            //qualif.QualificationName = "posterrrr";
            //Qualifications.Update(qualif.Id,qualif);
            //Qualifications.ListOfQualifications.ForEach(Console.WriteLine);
            #endregion

            #region CLASSTIMINGS
            //var clstime = new ClassTimings ();
            //clstime.Name = "testtimee";
            //clstime.EndTime = DateTime.Now;
            //clstime.StartTime = DateTime.Now;
            //ClassTimings.Add(clstime);
            //ClassTimings.ListOfClassTimings.ForEach(Console.WriteLine);
            //clstime = ClassTimings.ListOfClassTimings[0];
            //clstime.Name = "testtimexxx";
            //clstime.EndTime = DateTime.Now;
            //clstime.StartTime = DateTime.Now;
            //ClassTimings.Update(clstime.Id,clstime);
            //ClassTimings.ListOfClassTimings.ForEach(Console.WriteLine);
            #endregion

            #region CLASSSCHEDULES

            #endregion

            var at = StudentAttendanceRecords.ListOfStudentAttendanceRecords[0];
            Console.WriteLine(at.ToString());
            at.StudentAttendanceStatusesId = 2;
            StudentAttendanceRecords.Update(at.Id, at);
            at = StudentAttendanceRecords.ListOfStudentAttendanceRecords[0];
            Console.WriteLine(at.ToString());

            //var st = Students.ListOfStudents[0];
            //Console.WriteLine(st.ToString());
            //st.LoginDetails.Username = "zzzzzz";
            //Students.Update(st.Id,st);
            //st = Students.ListOfStudents[0];
            //Console.WriteLine(st.ToString());
            #region PARENTS
            //var prt = Parents.ListOfParents[0];
            //Console.WriteLine(prt.ToString());
            //prt.FirstName = "talha";
            //Parents.Update(prt.Id, prt);
            //Console.WriteLine(prt.ToString());
            //Console.Read();

            //Parents addparent = new Parents
            //{
            //    FirstName = "first",
            //    LastName = "last",
            //    DateOfBirth = "1998-12-03",
            //    CNIC = "cnic123",
            //    ImageUrlPath = "path/image",
            //    Phone1 = "phone1",
            //    Phone2 = "phone2",
            //    GendersId = 2
            //};
            //addparent.LoginDetails.Username = "usernameparent1";
            //addparent.LoginDetails.Password = "passwordparent1";
            //addparent.LoginDetails.AllowAccess = true;

            //Parents.Update(3, addparent);
            //addparent.Add(addparent);

         //   var updateparents = parents.ListOfParents.SingleOrDefault(p => p.Id == 2);
         //   Console.WriteLine($@"{updateparents.Id},
         //                       {updateparents.LoginDetailsId}, {updateparents.LoginDetails.Id},
         //                       {updateparents.PersonsId}, {updateparents.PersonId}");
          //  updateparents.FirstName = "update first name";
          //  updateparents.Update(updateparents.Id, updateparents);

            #endregion

            #region CLASSES

            //Grades addclassgrade = new Grades();
            //Classes addclasses = new Classes();

            //addclassgrade.GradeName = "One1";
            //addclasses.ClassesName = "One-A1";

            //Classes.Add(addclasses, addclassgrade);
            //Classes.ListOfClasses.ForEach(Console.WriteLine);

            #endregion

            #region FeeTypes

            //var testfeetypes = new FeeTypes();
            //testfeetypes.FeeTypeName = "testttttfeetype";
            //FeeTypes.Add(testfeetypes);
            //FeeTypes.ListOfFeeTypes.ForEach(Console.WriteLine);
            //testfeetypes = FeeTypes.ListOfFeeTypes[0];
            //testfeetypes.FeeTypeName = "testnamechagedddd";
            //FeeTypes.Update(testfeetypes.Id, testfeetypes);
            //FeeTypes.ListOfFeeTypes.ForEach(Console.WriteLine);

            #endregion

            #region EMPLOYEES

            //EmployeeTypes addemployeEmployeetypes = new EmployeeTypes();
            //Designations addemployeeDesignation = new Designations();
            //Employees addemployees = new Employees
            //{
            //    FirstName = "empFirst",
            //    LastName = "empLast",
            //    DateOfBirth = "1998-2-12",
            //    CNIC = "empcinc",
            //    ImageUrlPath = "emp/img/path",
            //    Phone1 = "ph1",
            //    Phone2 = "ph2",
            //    GendersId = 2,

            //    RegistrationID = "regId",
            //    Salary = 10000,
            //    IdentityCardUrl = "emp/identity/url",
            //    PostalAddress = "postal",
            //    ResidentialAddress = "residential",
            //    AggrementUrl = "agrrement/url",

            //};
            //addemployees.LoginDetails.Username = "empusername";
            //addemployees.LoginDetails.Password = "empPass";
            //addemployees.LoginDetails.AllowAccess = true;

            //addemployeEmployeetypes.EmployeeTypeName = "empTestType";
            //addemployeeDesignation.DesignationName = "empTestDesignation";

            //addemployees.Add(addemployees, addemployeEmployeetypes, addemployeeDesignation);

            #endregion

            #region STUDENTS

            //Students addstudents = new Students
            //{
            //    FirstName="stdFirst",
            //    LastName="stdlast",
            //    DateOfBirth="2000-3-3",
            //    CNIC="stdBayform",
            //    ImageUrlPath="std/img/path",
            //    Phone1="ph1",
            //    Phone2="ph2",
            //    GendersId=2,

            //    RegistrationId="stdRegId",
            //    DiscountPercentage=10,
            //    IdentityCardUrl="std/idcar/url",
            //    PostalAddress="stdpostal",
            //    ResidentialAddress="stdRes",
            //    AdmissionCard="std/admcard/url",
            //};
            //addstudents.LoginDetails.Username = "stdUsername";
            //addstudents.LoginDetails.Password = "stdpassword";
            //addstudents.LoginDetails.AllowAccess = true;

            //Classes addstudentsClasses = new Classes
            //{
            //    ClassesName = "addstdClassname",
            //    GradesId = 1
            //};
            //Parents addstudentsParesnts = new Parents
            //{
            //    FirstName = "stdFirst",
            //    LastName = "stdlast",
            //    DateOfBirth = "2000-3-3",
            //    CNIC = "stdBayform",
            //    ImageUrlPath = "std/img/path",
            //    Phone1 = "ph1",
            //    Phone2 = "ph2",
            //    GendersId = 2,
            //};
            //addstudentsParesnts.LoginDetails.Username = "stdUsername";
            //addstudentsParesnts.LoginDetails.Password = "stdpassword";
            //addstudentsParesnts.LoginDetails.AllowAccess = true;

            //addstudents.Add(addstudents, addstudentsParesnts, addstudentsClasses);

            #endregion

            #region Grades

            //var testgrades = new Grades();
            //testgrades.GradeName = "testStaticGrade";
            //Grades.Add(testgrades);
            //Grades.ListOfGrades.ForEach(Console.WriteLine);

            #endregion



            //var a = feerecords.ListOfFeeRecords;
            //a.ForEach(Console.WriteLine);

            //(st.ListOfStudents).ForEach(Console.WriteLine);
            //    Console.Read();
        }
    }
}
