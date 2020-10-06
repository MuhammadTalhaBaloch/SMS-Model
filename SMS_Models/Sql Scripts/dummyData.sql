SET IDENTITY_INSERT dbo.[tblDesignations] ON;
INSERT INTO [dbo].[tblDesignations]
           (Id,[DesignationName])
     VALUES
           (1,'Manager')
INSERT INTO [dbo].[tblDesignations]
           (Id,[DesignationName])
     VALUES
           (2,'Teacher')
INSERT INTO [dbo].[tblDesignations]
           (Id,[DesignationName])
     VALUES
           (3,'Helper')
INSERT INTO [dbo].[tblDesignations]
           (Id,[DesignationName])
     VALUES
           (4,'Principal')INSERT INTO [dbo].[tblDesignations]
           (Id,[DesignationName])
     VALUES
           (5,'Incharge')
SET IDENTITY_INSERT dbo.[tblDesignations] OFF;

SET IDENTITY_INSERT dbo.[tblGenders] ON;
INSERT INTO [dbo].[tblGenders]
           (Id,[GenderName])
     VALUES
           (1,'Male')
INSERT INTO [dbo].[tblGenders]
           (Id,[GenderName])
     VALUES
           (2,'Female')
SET IDENTITY_INSERT dbo.[tblGenders] OFF;

SET IDENTITY_INSERT dbo.[tblPersons] ON;
INSERT INTO [dbo].[tblPersons]
           (Id,[FirstName]
           ,[LastName]
           ,[DateOfBirth]
           ,[CNIC]
           ,[ImageUrlPath]
           ,[Phone1]
           ,[Phone2]
           ,[Gender_ID])
     VALUES
           (1,'Ali','Raza','1997-05-08','5401-000000000','D:/image1','030300000000','0304111111111',1),
		   (2,'Tehmina','khan','1980-08-03','5402-1111111111','D:/image2','030522222222','030699999999',2),
		   (3,'Shizra','Ahsan','1995-07-02','56403-77777777777','D:/image3','03045555555','0305888888888',2),
		   (4,'Waleed','Abbas','1996-09-03','5670-00000000000','D:/image4','030933333333','0307222222222',1),
		   (5,'Zehra','Ali','1998-09-06','56789-77777777','D:/image5','030366666666','030799999999',2),		   
		   (6,'Alizey','Arain','1998-05-09','5409-0000000000','D:/image6','030488888888','030900000000',2),
		   (7,'Kashif','Khan','1960-09-02','540133333333','D:/image7','0304222222222','0305111111111',1),
		   (8,'Danial','Sheikh','1977-09-02','5403-12344354356','D:/image8','039844444444','030545352345',1),
		   (9,'Muhammad','Ezan','1988-04-08','5404-111111111','D:/image9','039000000000','03243444333',1),
		   (10,'Muhammad','Saad','1987-09-12','54321-999999322','D:/image10','036665666666','043323243242',1)
SET IDENTITY_INSERT dbo.[tblPersons] OFF;

SET IDENTITY_INSERT dbo.[tblLoginDetails] ON;
INSERT INTO [dbo].[tblLoginDetails]
           (Id,[Username]
           ,[Password]
           ,[AllowAccess])
     VALUES
           (1,'Shizra','abc123',1),(2,'Alizey','xyz123',0),(3,'Muhammad','bwb123',1),(4,'Saad','hmm123',0),(5,'Amir','kpk123',1)

SET IDENTITY_INSERT dbo.[tblLoginDetails] OFF;

SET IDENTITY_INSERT dbo.[tblEmployeeTypes] ON;
INSERT INTO [dbo].[tblEmployeeTypes]
           (Id,[EmployeeTypeName])
     VALUES
           (1,'physical'),(2,'remotly')
SET IDENTITY_INSERT dbo.[tblEmployeeTypes] OFF;

SET IDENTITY_INSERT dbo.[tblEmployees] ON;
INSERT INTO [dbo].[tblEmployees]
           (Id,[RegistrationID]
           ,[Salary]
           ,[IdentityCardUrl]
           ,[PostalAddress]
           ,[ResidentialAddress]
           ,[AggrementUrl]
           ,[Person_ID]           
		   ,[Designation_ID]
           ,[EmployeeType_ID]
           ,[LoginDetail_ID])
     VALUES
           (1,1,457.9,'C:/url1','karachi','Lahore','C:/ffff',1,1,1,1),
		   (2,2,2346.87,'C:/abc','Multan','Karchi','C:/sdfsd',2,2,2,2),
		   (3,3,435643,'C:/def','Sindh','lahore','C:/gggfg',3,3,1,3),
		   (4,4,3435,'C:/xyz','Punjab','multan','D:/tyrtytu',4,4,2,4),
		   (5,5,234423,'C:/kpk','Khyber','North Nazimabad','C:/dssda',5,5,1,5)
SET IDENTITY_INSERT dbo.[tblEmployees] OFF;


SET IDENTITY_INSERT dbo.[tblGrades] ON;
INSERT INTO [dbo].[tblGrades]
           (Id,[GradeName])
     VALUES
           (1,'one'),(2,'two'),(3,'three'),(4,'four'),(5,'five')
SET IDENTITY_INSERT dbo.[tblGrades] OFF;

SET IDENTITY_INSERT dbo.[tblClasses] ON;
INSERT INTO [dbo].[tblClasses]
           (Id,[ClassName]
           ,[Grade_ID])
     VALUES
           (1,'One A',1),
		   (2,'One B',1),
		   (3,'Two A',2),
		   (4,'Two B',2),
		   (5,'Three A',3),
		   (6,'Three B',3),
		   (7,'Four A',4),
		   (8,'Four B',4),
		   (9,'Five A',5),
		   (10,'Five B',5)
		   

SET IDENTITY_INSERT dbo.[tblClasses] OFF;

SET IDENTITY_INSERT dbo.[tblParents] ON;
INSERT INTO [dbo].[tblParents]
           (Id,[Person_ID]           
		   ,[LoginDetail_ID])
     VALUES
           (1,1,1),(2,2,2),(3,3,3),(4,4,4),(5,5,5)
SET IDENTITY_INSERT dbo.[tblParents] OFF;

SET IDENTITY_INSERT dbo.[tblStudents] ON;
INSERT INTO [dbo].[tblStudents]
           (Id,[RegistrationID]
           ,[DiscountPercentage]
           ,[IdentityCardUrl]
           ,[AdmissionCard]           
		   ,[PostalAddress]
           ,[ResidentialAddress]
           ,[Person_ID]
           ,[LoginDetail_ID]
           ,[Parent_ID]
		   ,[Class_ID])
     VALUES
           (1,'1',10,'C:/DummyUrl','yes','karachi','sindh',1,1,1,1),
		   (2,'2',20,'D:/DummyUrl','yes','lahore','punjab',2,2,2,2),
		   (3,'3',20,'D:/dummyUrl','yes','faislabad','punjab',3,3,3,3),
		   (4,'4',30.5,'D:/dummyUrl','No','karachi','kpk',4,4,4,4),
		   (5,'5',15,'C:/dummyurl','No','queeta','balochistan',5,5,5,5)
SET IDENTITY_INSERT dbo.[tblStudents] OFF;




SET IDENTITY_INSERT dbo.[tblDocumentUrls] ON;
INSERT INTO [dbo].[tblDocumentUrls]
           (Id,[DocumentLink]
           ,[DateTime]
           ,[AddedByEmployee_ID])
     VALUES
           (1,'C:/dummyUrl','1995-09-07 12:00',1),
		   (2,'D:/dummyUrl','1996-06-02 10:00',2),
		   (3,'E:/dummyUrl','1994-02-01 11:00',3),
		   (4,'C:/dummyUrl','1991-09-07 10:00',4),
		   (5,'E:/dummyUrl','1954-07-07 12:00',5)
SET IDENTITY_INSERT dbo.[tblDocumentUrls] OFF;

SET IDENTITY_INSERT dbo.[tblStudentDocuments] ON;
INSERT INTO [dbo].[tblStudentDocuments]
           (Id,
		   [Student_ID]
           ,[DocumentUrl_ID])
     VALUES
           (1,1,1),(2,2,2),(3,3,3),(4,4,4),(5,5,5)
SET IDENTITY_INSERT dbo.[tblStudentDocuments] OFF;

SET IDENTITY_INSERT dbo.[tblEmployeeDocuments] ON;
INSERT INTO [dbo].[tblEmployeeDocuments]
           (Id,[Employee_ID]
           ,[DocumentUrl_ID])
     VALUES
           (1,1,1),(2,2,2),(3,3,3),(4,4,4),(5,5,5)
SET IDENTITY_INSERT dbo.[tblEmployeeDocuments] OFF;
SET IDENTITY_INSERT dbo.[tblSkills] ON;
INSERT INTO [dbo].[tblSkills]
           (Id,[SkillName])
     VALUES
           (1,'Drawing'),(2,'Writing'),(3,'Reading'),(4,'Listening'),(5,'singing')

SET IDENTITY_INSERT dbo.[tblSkills] OFF;

SET IDENTITY_INSERT dbo.[tblEmployeeSkills] ON;
INSERT INTO [dbo].[tblEmployeeSkills]
           (Id,
		   [Employee_ID]
           ,[Skill_ID])
     VALUES
           (1,1,1),(2,2,2),(3,3,3),(4,4,4),(5,5,5)

SET IDENTITY_INSERT dbo.[tblEmployeeSkills] OFF;
SET IDENTITY_INSERT dbo.[tblQualifications] ON;
INSERT INTO [dbo].[tblQualifications]
           (Id,[QualificationName])
     VALUES
           (1,'Matric'),(2,'Inter'),(3,'Bachelor in science'),(4,'Masters'),(5,'Phd')
SET IDENTITY_INSERT dbo.[tblQualifications] OFF;

SET IDENTITY_INSERT dbo.[tblEmployeeQualifications] ON;
INSERT INTO [dbo].[tblEmployeeQualifications]
           (Id,
		   [Employee_ID]
           ,[Qualification_ID])
     VALUES
           (1,1,1),(2,2,2),(3,3,3),(4,4,4),(5,5,5)
SET IDENTITY_INSERT dbo.[tblEmployeeQualifications] OFF;

SET IDENTITY_INSERT dbo.[tblTeachers] ON;INSERT INTO [dbo].[tblTeachers]
           (Id,[Employee_ID])
     VALUES
           (1,1),(2,2),(3,3),(4,4),(5,5)
SET IDENTITY_INSERT dbo.[tblTeachers] OFF;

SET IDENTITY_INSERT dbo.[tblSubjects] ON;
INSERT INTO [dbo].[tblSubjects]
           (Id,[SubjectName]
           ,[Teacher_ID]
           ,[Class_ID])
     VALUES
           (1,'English',1,1),(2,'Mathematics',2,2),(3,'Science',3,3),(4,'Drawing',4,4),(5,'Islamiat',5,5)
SET IDENTITY_INSERT dbo.[tblSubjects] OFF;

SET IDENTITY_INSERT dbo.[tblFeeTypes] ON;
INSERT INTO [dbo].[tblFeeTypes]           (Id,[FeeTypeName])
     VALUES
           (1,'Examination fee'),(2,'Tution fee'),(3,'Transport Fee'),(4,'ID card fee'),(5,'Library Fee')
SET IDENTITY_INSERT dbo.[tblFeeTypes] OFF;

SET IDENTITY_INSERT dbo.[tblFeeSchedules] ON;
INSERT INTO [dbo].[tblFeeSchedules]
           (Id,[Fee]
           ,[Grade_ID]
           ,[FeeType_ID])
     VALUES
           (1,1500.00,1,1),(2,1500.00,2,2),(3,1500.00,3,3),(4,500,4,4),(5,100,5,5)
SET IDENTITY_INSERT dbo.[tblFeeSchedules] OFF;


SET IDENTITY_INSERT dbo.[tblExamTypes] ON;
INSERT INTO [dbo].[tblExamTypes]           (Id,[ExamTypeName])
     VALUES
           (1,'First Term'),(2,'Mid Term'),(3,'Final Term')
SET IDENTITY_INSERT dbo.[tblExamTypes] OFF;


SET IDENTITY_INSERT dbo.[tblExams] ON;
INSERT INTO [dbo].[tblExams]
           (Id,[TotalMarks]
           ,[ObtainedMarks]
           ,[DateTime]
           ,[Student_ID]
           ,[Subject_ID]
           ,[ExamType_ID])
     VALUES
           (1,100,83,'2020-09-03 12:00',1,1,1),
		   (2,100,85,'2020-09-03 12:00',2,2,2),		   
           (3,100,65,'2020-09-03 12:00',3,3,3),
		   (4,100,74,'2020-09-03 12:00',4,4,1),
		   (5,100,91,'2020-09-03 10:00',5,5,2)
SET IDENTITY_INSERT dbo.[tblExams] OFF;

SET IDENTITY_INSERT dbo.[tblFineStatuses] ON;
INSERT INTO [dbo].[tblFineStatuses]
           (Id,[FineStatusName])
     VALUES
           (1,'Paid'),(2,'unpaid'),(3,'exemption')
SET IDENTITY_INSERT dbo.[tblFineStatuses] OFF;

SET IDENTITY_INSERT dbo.[tblExpenseTypes] ON;
INSERT INTO [dbo].[tblExpenseTypes]
           (Id,[ExpenseTypeName])
     VALUES
           (1,'Hostel Expense'),(2,'Cleaning Expense'),(3,'Electricity Expense'),(4,'Water Bill'),(5,'Gas Bill')
SET IDENTITY_INSERT dbo.[tblExpenseTypes] OFF;

SET IDENTITY_INSERT dbo.[tblFeeRecords] ON;
INSERT INTO [dbo].[tblFeeRecords]
           (Id,[DateTime]
           ,[Paid]
           ,[AddedByEmployee_ID]
           ,[Student_ID]
           ,[FeeSchedule_ID])
     VALUES
           (1,'2020-09-03 10:00',1,1,1,1),
		   (2,'2020-09-03 10:00',0,2,2,2),
		   (3,'2020-09-03 10:08',1,3,3,3),
		   (4,'2020-09-03 10:00',0,4,4,4),
		   (5,'2020-09-03 10:00',1,5,5,5)
SET IDENTITY_INSERT dbo.[tblFeeRecords] OFF;

SET IDENTITY_INSERT dbo.[tblSalaryRecords] ON;
INSERT INTO [dbo].[tblSalaryRecords]
           (Id,[DateTime]
           ,[Paid],
		   [PayableAmount]
           ,[Employee_ID]
           ,[AddedByEmployee_ID])
     VALUES
           (1,'2020-09-03 10:00',1,342.3,1,1),
           (2,'2020-09-03 10:08',0,7676.55,2,2),
		   (3,'2020-09-03 10:07',1,94340.44,3,3),
		   (4,'2020-09-03 10:09',0,10012.33,5,4),
		   (5,'2020-09-03 10:00',1,10423.43,4,5)
SET IDENTITY_INSERT dbo.[tblSalaryRecords] OFF;

SET IDENTITY_INSERT dbo.[tblStudentAttendanceStatuses] ON;INSERT INTO [dbo].[tblStudentAttendanceStatuses]
           (Id,[StatusName])
     VALUES
           (1,'Present'),(2,'Absent'),(3,'Leave')

SET IDENTITY_INSERT dbo.[tblStudentAttendanceStatuses] OFF;

SET IDENTITY_INSERT dbo.[tblStudentAttendanceRecords] ON;
INSERT INTO [dbo].[tblStudentAttendanceRecords]
           (Id,[Date]
           ,[Student_ID]
           ,[StudentAttendanceStatus_ID])
     VALUES
           (1,'2020-09-03',1,1),(2,'2020-09-03',2,1),(3,'2020-09-03',3,2),(4,'2020-09-03',4,1),(5,'2020-09-03',5,3)
SET IDENTITY_INSERT dbo.[tblStudentAttendanceRecords] OFF;

SET IDENTITY_INSERT dbo.[tblStudentFines] ON;INSERT INTO [dbo].[tblStudentFines]
           (Id,[Fine]
           ,[Note]
            ,[DateTime]
          ,[Student_ID]
           ,[AddedByEmployee_ID]
           ,[FineStatus_ID])
     VALUES
           (1,100,'Late Fine','2020-09-01 11:00',1,1,1),
		   (2,200,'Absent Fine','2020-04-01 08:00',2,2,2),
		   (3,200,'Absent Fine','2020-04-06 11:00',3,3,3),
		   (4,100,'Late Fine','2020-09-02 12:00',4,4,1),
		   (5,200,'Late Fine','2020-09-04 10:00',5,5,2)

SET IDENTITY_INSERT dbo.[tblStudentFines] OFF;

SET IDENTITY_INSERT dbo.[tblExpenseRecords] ON;
INSERT INTO [dbo].[tblExpenseRecords]
           (Id,[DateTime]
           ,[Paid]
           ,[AddedByEmployee_ID]
           ,[ExpenseType_ID])
     VALUES
           (1,'2020-09-02 10:00',1,1,1),
		   (2,'2020-09-03 11:00',0,2,2),
		   (3,'2020-09-01 12:00',1,3,3),
		   (4,'2020-09-07 09:00',0,4,4),
		   (5,'2020-09-08 07:00',1,5,5)
SET IDENTITY_INSERT dbo.[tblExpenseRecords] OFF;

SET IDENTITY_INSERT dbo.[tblEmployeeAttendanceStatuses] ON;
INSERT INTO [dbo].[tblEmployeeAttendanceStatuses]
           (Id,[StatusName])
     VALUES
           (1,'Present'),(2,'Absent'),(3,'Leave')
SET IDENTITY_INSERT dbo.[tblEmployeeAttendanceStatuses] OFF;

SET IDENTITY_INSERT dbo.[tblEmployeeAttendanceReords] ON;
INSERT INTO [dbo].[tblEmployeeAttendanceReords]
           ([Id]
           ,[Date]
           ,[Employee_ID]
           ,[EmployeeAttendanceSatus_ID])
     VALUES
           (1,'2020-09-03',1,1),(2,'2020-09-02',2,2),(3,'2020-09-01',3,3),(4,'2020-03-09',4,1),(5,'2020-09-05',5,2)
SET IDENTITY_INSERT dbo.[tblEmployeeAttendanceReords] OFF;

SET IDENTITY_INSERT dbo.[tblEmployeeFines] ON;
INSERT INTO [dbo].[tblEmployeeFines]
           (Id,[Fine]
           ,[DateTime]
           ,[Note]
           ,[AddedByEmployee_ID]
           ,[Employee_ID]
           ,[FineStatus_ID])
     VALUES
           (1,100,'2020-09-03 10:00','Late Fine',1,1,1),
		   (2,200,'2020-09-02 10:00','Absent Fine',2,2,2),
		   (3,100,'2020-01-01 10:00','Late Fine',3,3,3),
		   (4,200,'2020-02-04 10:00','Absent Fine',4,4,1),
		   (5,100,'2020-03-01 10:00','Late Fine',5,5,2)
SET IDENTITY_INSERT dbo.[tblEmployeeFines] OFF;

SET IDENTITY_INSERT dbo.[tblClassTimings] ON;
INSERT INTO [dbo].[tblClassTimings]
           (Id,[StartTIme]
           ,[Endtime]
           ,[Name])
     VALUES
           (1,'08:00','01:20','Morning'),
		   (2,'02:00','7:00','Evening')
SET IDENTITY_INSERT dbo.[tblClassTimings] OFF;

SET IDENTITY_INSERT dbo.[tblClassSchedules] ON;
INSERT INTO [dbo].[tblClassSchedules]
           (Id,[WeekDay]
           ,[Subject_ID]
           ,[Class_ID]
           ,[ClassTiming_ID])
     VALUES
           (1,'Monday',1,1,1),(2,'Tuesday',2,2,2),(3,'Wednesday',3,3,1),(4,'Thursday',4,4,2),(5,'Friday',5,5,1)
SET IDENTITY_INSERT dbo.[tblClassSchedules] OFF;

