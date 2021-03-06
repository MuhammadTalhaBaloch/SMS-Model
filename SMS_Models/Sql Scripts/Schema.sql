USE [master]
GO
/****** Object:  Database [SMS]    Script Date: 9/15/2020 2:17:57 PM ******/
CREATE DATABASE [SMS]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SMS', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\SMS.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'SMS_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\SMS_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [SMS] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SMS].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SMS] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SMS] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SMS] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SMS] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SMS] SET ARITHABORT OFF 
GO
ALTER DATABASE [SMS] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SMS] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SMS] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SMS] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SMS] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SMS] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SMS] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SMS] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SMS] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SMS] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SMS] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SMS] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SMS] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SMS] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SMS] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SMS] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SMS] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SMS] SET RECOVERY FULL 
GO
ALTER DATABASE [SMS] SET  MULTI_USER 
GO
ALTER DATABASE [SMS] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SMS] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SMS] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SMS] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [SMS] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'SMS', N'ON'
GO
USE [SMS]
GO
/****** Object:  Table [dbo].[tblClasses]    Script Date: 9/15/2020 2:17:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblClasses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClassName] [nvarchar](50) NOT NULL,
	[Grade_ID] [int] NOT NULL,
 CONSTRAINT [PK_Classes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblClassSchedules]    Script Date: 9/15/2020 2:17:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblClassSchedules](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[WeekDay] [nvarchar](50) NOT NULL,
	[Subject_ID] [int] NULL,
	[Class_ID] [int] NULL,
	[ClassTiming_ID] [int] NULL,
 CONSTRAINT [PK_ClassSchedules] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblClassTimings]    Script Date: 9/15/2020 2:17:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblClassTimings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StartTIme] [time](7) NOT NULL,
	[Endtime] [time](7) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_tblClassTimings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblDesignations]    Script Date: 9/15/2020 2:17:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblDesignations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DesignationName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Designation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblDocumentUrls]    Script Date: 9/15/2020 2:17:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblDocumentUrls](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DocumentLink] [nvarchar](50) NOT NULL,
	[DateTime] [datetime] NOT NULL,
	[AddedByEmployee_ID] [int] NULL,
 CONSTRAINT [PK_tblDocumentUrl\] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblEmployeeAttendanceReords]    Script Date: 9/15/2020 2:17:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblEmployeeAttendanceReords](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [date] NOT NULL,
	[Employee_ID] [int] NULL,
	[EmployeeAttendanceSatus_ID] [int] NULL,
 CONSTRAINT [PK_EmployeeAttendanceReords] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblEmployeeAttendanceStatuses]    Script Date: 9/15/2020 2:17:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblEmployeeAttendanceStatuses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StatusName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_EmployeeAttendanceStatuses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblEmployeeDocuments]    Script Date: 9/15/2020 2:17:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblEmployeeDocuments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Employee_ID] [int] NOT NULL,
	[DocumentUrl_ID] [int] NOT NULL,
 CONSTRAINT [PK_tblEmployeeDocuments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblEmployeeFines]    Script Date: 9/15/2020 2:17:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblEmployeeFines](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Fine] [float] NOT NULL,
	[DateTime] [datetime] NOT NULL,
	[Note] [nvarchar](50) NOT NULL,
	[AddedByEmployee_ID] [int] NOT NULL,
	[Employee_ID] [int] NOT NULL,
	[FineStatus_ID] [int] NOT NULL,
 CONSTRAINT [PK_tblEmployeeFines] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblEmployeeFinesDeleted]    Script Date: 9/15/2020 2:17:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblEmployeeFinesDeleted](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Fine] [float] NOT NULL,
	[DateTime] [datetime] NOT NULL,
	[Note] [nvarchar](50) NOT NULL,
	[AddedByEmployee_ID] [int] NOT NULL,
	[Employee_ID] [int] NOT NULL,
	[FineStatus_ID] [int] NOT NULL,
	[DeletedByEmployee_ID] [int] NOT NULL,
 CONSTRAINT [PK_tblEmployeeFinesDeleted] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblEmployeeQualifications]    Script Date: 9/15/2020 2:17:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblEmployeeQualifications](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Employee_ID] [int] NULL,
	[Qualification_ID] [int] NULL,
 CONSTRAINT [PK_tblEmployeeQualifications] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblEmployees]    Script Date: 9/15/2020 2:17:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblEmployees](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RegistrationID] [nvarchar](50) NOT NULL,
	[Salary] [float] NOT NULL,
	[IdentityCardUrl] [nvarchar](50) NULL,
	[PostalAddress] [nvarchar](50) NULL,
	[ResidentialAddress] [nvarchar](50) NULL,
	[AggrementUrl] [nvarchar](50) NULL,
	[Person_ID] [int] NULL,
	[Designation_ID] [int] NULL,
	[EmployeeType_ID] [int] NULL,
	[LoginDetail_ID] [int] NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblEmployeeSkills]    Script Date: 9/15/2020 2:17:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblEmployeeSkills](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Employee_ID] [int] NULL,
	[Skill_ID] [int] NULL,
 CONSTRAINT [PK_EmployeeSkills] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblEmployeeTypes]    Script Date: 9/15/2020 2:17:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblEmployeeTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeTypeName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_EmployeeTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblExams]    Script Date: 9/15/2020 2:17:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblExams](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TotalMarks] [float] NOT NULL,
	[ObtainedMarks] [float] NOT NULL,
	[DateTime] [datetime] NOT NULL,
	[Student_ID] [int] NOT NULL,
	[Subject_ID] [int] NOT NULL,
	[ExamType_ID] [int] NOT NULL,
 CONSTRAINT [PK_Exams] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblExamTypes]    Script Date: 9/15/2020 2:17:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblExamTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ExamTypeName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_tblExamTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblExpenseRecords]    Script Date: 9/15/2020 2:17:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblExpenseRecords](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DateTime] [datetime] NOT NULL,
	[Paid] [bit] NOT NULL,
	[AddedByEmployee_ID] [int] NULL,
	[ExpenseType_ID] [int] NULL,
 CONSTRAINT [PK_tblExpenseRecords] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblExpenseRecordsDeleted]    Script Date: 9/15/2020 2:17:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblExpenseRecordsDeleted](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DateTime] [datetime] NOT NULL,
	[Paid] [bit] NOT NULL,
	[AddedByEmployee_ID] [int] NOT NULL,
	[ExpenseType_ID] [int] NOT NULL,
	[DeletedByEmployee_ID] [int] NOT NULL,
 CONSTRAINT [PK_tblExpenseRecordsDeleted] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblExpenseTypes]    Script Date: 9/15/2020 2:17:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblExpenseTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ExpenseTypeName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ExpenseTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblFeeRecords]    Script Date: 9/15/2020 2:17:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblFeeRecords](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DateTime] [date] NOT NULL,
	[Paid] [bit] NOT NULL,
	[AddedByEmployee_ID] [int] NOT NULL,
	[Student_ID] [int] NOT NULL,
	[FeeSchedule_ID] [int] NOT NULL,
 CONSTRAINT [PK_tblFeeRecords] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblFeeRecordsDeleted]    Script Date: 9/15/2020 2:17:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblFeeRecordsDeleted](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DateTime] [datetime] NOT NULL,
	[Paid] [bit] NOT NULL,
	[AddedByEmployee_ID] [int] NOT NULL,
	[Student_ID] [int] NOT NULL,
	[FeeSchedule_ID] [int] NOT NULL,
	[DeletedByEmployee_ID] [int] NOT NULL,
 CONSTRAINT [PK_Table_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblFeeSchedules]    Script Date: 9/15/2020 2:17:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblFeeSchedules](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Fee] [float] NOT NULL,
	[Grade_ID] [int] NULL,
	[FeeType_ID] [int] NULL,
 CONSTRAINT [PK_FeeSchedules] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblFeeTypes]    Script Date: 9/15/2020 2:17:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblFeeTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FeeTypeName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_tblFeeTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblFineStatuses]    Script Date: 9/15/2020 2:17:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblFineStatuses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FineStatusName] [nchar](10) NULL,
 CONSTRAINT [PK_tblFineStatuses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblGenders]    Script Date: 9/15/2020 2:17:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblGenders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GenderName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Genders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblGrades]    Script Date: 9/15/2020 2:17:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblGrades](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GradeName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_tblGrades] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblLoginDetails]    Script Date: 9/15/2020 2:17:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblLoginDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[AllowAccess] [bit] NOT NULL,
 CONSTRAINT [PK_LoginDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblParents]    Script Date: 9/15/2020 2:17:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblParents](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Person_ID] [int] NULL,
	[LoginDetail_ID] [int] NULL,
 CONSTRAINT [PK_Parents] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblPersons]    Script Date: 9/15/2020 2:17:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblPersons](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[DateOfBirth] [date] NOT NULL,
	[CNIC] [nvarchar](50) NOT NULL,
	[ImageUrlPath] [nvarchar](50) NULL,
	[Phone1] [nvarchar](50) NULL,
	[Phone2] [nvarchar](50) NULL,
	[Gender_ID] [int] NULL,
 CONSTRAINT [PK_Persons] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblQualifications]    Script Date: 9/15/2020 2:17:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblQualifications](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[QualificationName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Qualifications] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblSalaryRecords]    Script Date: 9/15/2020 2:17:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblSalaryRecords](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DateTime] [datetime] NOT NULL,
	[Paid] [bit] NOT NULL,
	[PayableAmount] [decimal](18, 2) NOT NULL,
	[Employee_ID] [int] NULL,
	[AddedByEmployee_ID] [int] NULL,
 CONSTRAINT [PK_SalaryRecords] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblSalaryRecordsDeleted]    Script Date: 9/15/2020 2:17:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblSalaryRecordsDeleted](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DateTime] [datetime] NOT NULL,
	[Paid] [bit] NOT NULL,
	[PayableAmount] [decimal](18, 2) NOT NULL,
	[Employee_ID] [int] NOT NULL,
	[AddedByEmployee_ID] [int] NOT NULL,
	[DeletedByEmployee_ID] [int] NOT NULL,
 CONSTRAINT [PK_tblSalaryRecordsDeleted] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblSkills]    Script Date: 9/15/2020 2:17:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblSkills](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SkillName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Skills] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblStudentAttendanceRecords]    Script Date: 9/15/2020 2:17:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblStudentAttendanceRecords](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [date] NOT NULL,
	[Student_ID] [int] NOT NULL,
	[StudentAttendanceStatus_ID] [int] NOT NULL,
 CONSTRAINT [PK_StudentAttendanceRecords] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblStudentAttendanceStatuses]    Script Date: 9/15/2020 2:17:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblStudentAttendanceStatuses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StatusName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_tblStudentAttendanceStatuses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblStudentDocuments]    Script Date: 9/15/2020 2:17:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblStudentDocuments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Student_ID] [int] NOT NULL,
	[DocumentUrl_ID] [int] NOT NULL,
 CONSTRAINT [PK_tblStudentDocuments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblStudentFines]    Script Date: 9/15/2020 2:17:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblStudentFines](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Fine] [float] NOT NULL,
	[DateTime] [datetime] NOT NULL,
	[Note] [nvarchar](50) NOT NULL,
	[Student_ID] [int] NOT NULL,
	[AddedByEmployee_ID] [int] NOT NULL,
	[FineStatus_ID] [int] NOT NULL,
 CONSTRAINT [PK_tblStudentFines] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblStudentFinesDeleted]    Script Date: 9/15/2020 2:17:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblStudentFinesDeleted](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Fine] [float] NOT NULL,
	[DateTime] [datetime] NOT NULL,
	[Note] [nvarchar](50) NOT NULL,
	[Student_ID] [int] NOT NULL,
	[AddedByEmployee_ID] [int] NOT NULL,
	[FineStatus_ID] [int] NOT NULL,
	[DeletedByEmployee_ID] [int] NOT NULL,
 CONSTRAINT [PK_tblStudentFinesDeleted] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblStudents]    Script Date: 9/15/2020 2:17:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblStudents](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RegistrationID] [nvarchar](50) NOT NULL,
	[DiscountPercentage] [float] NOT NULL,
	[IdentityCardUrl] [nvarchar](50) NULL,
	[AdmissionCard] [nvarchar](50) NULL,
	[PostalAddress] [nvarchar](50) NULL,
	[ResidentialAddress] [nvarchar](50) NULL,
	[Person_ID] [int] NOT NULL,
	[LoginDetail_ID] [int] NOT NULL,
	[Parent_ID] [int] NOT NULL,
	[Class_ID] [int] NOT NULL,
 CONSTRAINT [PK_Students] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblSubjects]    Script Date: 9/15/2020 2:17:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblSubjects](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SubjectName] [nvarchar](50) NOT NULL,
	[Teacher_ID] [int] NULL,
	[Class_ID] [int] NULL,
 CONSTRAINT [PK_Subjects] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblTeachers]    Script Date: 9/15/2020 2:17:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblTeachers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Employee_ID] [int] NULL,
 CONSTRAINT [PK_tblTeachers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[tblClasses]  WITH CHECK ADD  CONSTRAINT [FK_Classes_tblGrades] FOREIGN KEY([Grade_ID])
REFERENCES [dbo].[tblGrades] ([Id])
GO
ALTER TABLE [dbo].[tblClasses] CHECK CONSTRAINT [FK_Classes_tblGrades]
GO
ALTER TABLE [dbo].[tblClassSchedules]  WITH CHECK ADD  CONSTRAINT [FK_ClassSchedules_tblClasses] FOREIGN KEY([Class_ID])
REFERENCES [dbo].[tblClasses] ([Id])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[tblClassSchedules] CHECK CONSTRAINT [FK_ClassSchedules_tblClasses]
GO
ALTER TABLE [dbo].[tblClassSchedules]  WITH CHECK ADD  CONSTRAINT [FK_ClassSchedules_tblClassTimings] FOREIGN KEY([ClassTiming_ID])
REFERENCES [dbo].[tblClassTimings] ([Id])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[tblClassSchedules] CHECK CONSTRAINT [FK_ClassSchedules_tblClassTimings]
GO
ALTER TABLE [dbo].[tblEmployeeAttendanceReords]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeAttendanceReords_EmployeeAttendanceStatuses] FOREIGN KEY([EmployeeAttendanceSatus_ID])
REFERENCES [dbo].[tblEmployeeAttendanceStatuses] ([Id])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[tblEmployeeAttendanceReords] CHECK CONSTRAINT [FK_EmployeeAttendanceReords_EmployeeAttendanceStatuses]
GO
ALTER TABLE [dbo].[tblEmployeeFinesDeleted]  WITH CHECK ADD  CONSTRAINT [FK_tblEmployeeFinesDeleted_tblEmployees] FOREIGN KEY([AddedByEmployee_ID])
REFERENCES [dbo].[tblEmployees] ([Id])
GO
ALTER TABLE [dbo].[tblEmployeeFinesDeleted] CHECK CONSTRAINT [FK_tblEmployeeFinesDeleted_tblEmployees]
GO
ALTER TABLE [dbo].[tblEmployeeFinesDeleted]  WITH CHECK ADD  CONSTRAINT [FK_tblEmployeeFinesDeleted_tblEmployees1] FOREIGN KEY([Employee_ID])
REFERENCES [dbo].[tblEmployees] ([Id])
GO
ALTER TABLE [dbo].[tblEmployeeFinesDeleted] CHECK CONSTRAINT [FK_tblEmployeeFinesDeleted_tblEmployees1]
GO
ALTER TABLE [dbo].[tblEmployeeFinesDeleted]  WITH CHECK ADD  CONSTRAINT [FK_tblEmployeeFinesDeleted_tblEmployees2] FOREIGN KEY([DeletedByEmployee_ID])
REFERENCES [dbo].[tblEmployees] ([Id])
GO
ALTER TABLE [dbo].[tblEmployeeFinesDeleted] CHECK CONSTRAINT [FK_tblEmployeeFinesDeleted_tblEmployees2]
GO
ALTER TABLE [dbo].[tblEmployeeFinesDeleted]  WITH CHECK ADD  CONSTRAINT [FK_tblEmployeeFinesDeleted_tblFineStatuses] FOREIGN KEY([FineStatus_ID])
REFERENCES [dbo].[tblFineStatuses] ([Id])
GO
ALTER TABLE [dbo].[tblEmployeeFinesDeleted] CHECK CONSTRAINT [FK_tblEmployeeFinesDeleted_tblFineStatuses]
GO
ALTER TABLE [dbo].[tblEmployeeQualifications]  WITH CHECK ADD  CONSTRAINT [FK_tblEmployeeQualifications_Qualifications] FOREIGN KEY([Qualification_ID])
REFERENCES [dbo].[tblQualifications] ([Id])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[tblEmployeeQualifications] CHECK CONSTRAINT [FK_tblEmployeeQualifications_Qualifications]
GO
ALTER TABLE [dbo].[tblEmployeeQualifications]  WITH CHECK ADD  CONSTRAINT [FK_tblEmployeeQualifications_tblEmployees] FOREIGN KEY([Employee_ID])
REFERENCES [dbo].[tblEmployees] ([Id])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[tblEmployeeQualifications] CHECK CONSTRAINT [FK_tblEmployeeQualifications_tblEmployees]
GO
ALTER TABLE [dbo].[tblEmployees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Designation] FOREIGN KEY([Designation_ID])
REFERENCES [dbo].[tblDesignations] ([Id])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[tblEmployees] CHECK CONSTRAINT [FK_Employees_Designation]
GO
ALTER TABLE [dbo].[tblEmployees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_EmployeeTypes] FOREIGN KEY([EmployeeType_ID])
REFERENCES [dbo].[tblEmployeeTypes] ([Id])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[tblEmployees] CHECK CONSTRAINT [FK_Employees_EmployeeTypes]
GO
ALTER TABLE [dbo].[tblEmployees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_LoginDetails] FOREIGN KEY([LoginDetail_ID])
REFERENCES [dbo].[tblLoginDetails] ([Id])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[tblEmployees] CHECK CONSTRAINT [FK_Employees_LoginDetails]
GO
ALTER TABLE [dbo].[tblEmployees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Persons] FOREIGN KEY([Person_ID])
REFERENCES [dbo].[tblPersons] ([Id])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[tblEmployees] CHECK CONSTRAINT [FK_Employees_Persons]
GO
ALTER TABLE [dbo].[tblEmployeeSkills]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeSkills_Employees] FOREIGN KEY([Employee_ID])
REFERENCES [dbo].[tblEmployees] ([Id])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[tblEmployeeSkills] CHECK CONSTRAINT [FK_EmployeeSkills_Employees]
GO
ALTER TABLE [dbo].[tblEmployeeSkills]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeSkills_Skills] FOREIGN KEY([Skill_ID])
REFERENCES [dbo].[tblSkills] ([Id])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[tblEmployeeSkills] CHECK CONSTRAINT [FK_EmployeeSkills_Skills]
GO
ALTER TABLE [dbo].[tblExpenseRecords]  WITH CHECK ADD  CONSTRAINT [FK_tblExpenseRecords_ExpenseTypes] FOREIGN KEY([ExpenseType_ID])
REFERENCES [dbo].[tblExpenseTypes] ([Id])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[tblExpenseRecords] CHECK CONSTRAINT [FK_tblExpenseRecords_ExpenseTypes]
GO
ALTER TABLE [dbo].[tblExpenseRecords]  WITH CHECK ADD  CONSTRAINT [FK_tblExpenseRecords_tblEmployees] FOREIGN KEY([AddedByEmployee_ID])
REFERENCES [dbo].[tblEmployees] ([Id])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[tblExpenseRecords] CHECK CONSTRAINT [FK_tblExpenseRecords_tblEmployees]
GO
ALTER TABLE [dbo].[tblExpenseRecordsDeleted]  WITH CHECK ADD  CONSTRAINT [FK_tblExpenseRecordsDeleted_tblEmployees] FOREIGN KEY([AddedByEmployee_ID])
REFERENCES [dbo].[tblEmployees] ([Id])
GO
ALTER TABLE [dbo].[tblExpenseRecordsDeleted] CHECK CONSTRAINT [FK_tblExpenseRecordsDeleted_tblEmployees]
GO
ALTER TABLE [dbo].[tblExpenseRecordsDeleted]  WITH CHECK ADD  CONSTRAINT [FK_tblExpenseRecordsDeleted_tblEmployees1] FOREIGN KEY([DeletedByEmployee_ID])
REFERENCES [dbo].[tblEmployees] ([Id])
GO
ALTER TABLE [dbo].[tblExpenseRecordsDeleted] CHECK CONSTRAINT [FK_tblExpenseRecordsDeleted_tblEmployees1]
GO
ALTER TABLE [dbo].[tblExpenseRecordsDeleted]  WITH CHECK ADD  CONSTRAINT [FK_tblExpenseRecordsDeleted_tblExpenseTypes] FOREIGN KEY([ExpenseType_ID])
REFERENCES [dbo].[tblExpenseTypes] ([Id])
GO
ALTER TABLE [dbo].[tblExpenseRecordsDeleted] CHECK CONSTRAINT [FK_tblExpenseRecordsDeleted_tblExpenseTypes]
GO
ALTER TABLE [dbo].[tblFeeRecords]  WITH CHECK ADD  CONSTRAINT [FK_tblFeeRecords_tblEmployees] FOREIGN KEY([AddedByEmployee_ID])
REFERENCES [dbo].[tblEmployees] ([Id])
GO
ALTER TABLE [dbo].[tblFeeRecords] CHECK CONSTRAINT [FK_tblFeeRecords_tblEmployees]
GO
ALTER TABLE [dbo].[tblFeeRecords]  WITH CHECK ADD  CONSTRAINT [FK_tblFeeRecords_tblFeeSchedules] FOREIGN KEY([FeeSchedule_ID])
REFERENCES [dbo].[tblFeeSchedules] ([Id])
GO
ALTER TABLE [dbo].[tblFeeRecords] CHECK CONSTRAINT [FK_tblFeeRecords_tblFeeSchedules]
GO
ALTER TABLE [dbo].[tblFeeRecords]  WITH CHECK ADD  CONSTRAINT [FK_tblFeeRecords_tblStudents] FOREIGN KEY([Student_ID])
REFERENCES [dbo].[tblStudents] ([Id])
GO
ALTER TABLE [dbo].[tblFeeRecords] CHECK CONSTRAINT [FK_tblFeeRecords_tblStudents]
GO
ALTER TABLE [dbo].[tblFeeRecordsDeleted]  WITH CHECK ADD  CONSTRAINT [FK_Table_1_tblEmployees] FOREIGN KEY([AddedByEmployee_ID])
REFERENCES [dbo].[tblEmployees] ([Id])
GO
ALTER TABLE [dbo].[tblFeeRecordsDeleted] CHECK CONSTRAINT [FK_Table_1_tblEmployees]
GO
ALTER TABLE [dbo].[tblFeeRecordsDeleted]  WITH CHECK ADD  CONSTRAINT [FK_Table_1_tblEmployees1] FOREIGN KEY([DeletedByEmployee_ID])
REFERENCES [dbo].[tblEmployees] ([Id])
GO
ALTER TABLE [dbo].[tblFeeRecordsDeleted] CHECK CONSTRAINT [FK_Table_1_tblEmployees1]
GO
ALTER TABLE [dbo].[tblFeeRecordsDeleted]  WITH CHECK ADD  CONSTRAINT [FK_Table_1_tblFeeSchedules] FOREIGN KEY([FeeSchedule_ID])
REFERENCES [dbo].[tblFeeSchedules] ([Id])
GO
ALTER TABLE [dbo].[tblFeeRecordsDeleted] CHECK CONSTRAINT [FK_Table_1_tblFeeSchedules]
GO
ALTER TABLE [dbo].[tblFeeRecordsDeleted]  WITH CHECK ADD  CONSTRAINT [FK_Table_1_tblStudents] FOREIGN KEY([Student_ID])
REFERENCES [dbo].[tblStudents] ([Id])
GO
ALTER TABLE [dbo].[tblFeeRecordsDeleted] CHECK CONSTRAINT [FK_Table_1_tblStudents]
GO
ALTER TABLE [dbo].[tblFeeSchedules]  WITH CHECK ADD  CONSTRAINT [FK_FeeSchedules_tblFeeTypes] FOREIGN KEY([FeeType_ID])
REFERENCES [dbo].[tblFeeTypes] ([Id])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[tblFeeSchedules] CHECK CONSTRAINT [FK_FeeSchedules_tblFeeTypes]
GO
ALTER TABLE [dbo].[tblFeeSchedules]  WITH CHECK ADD  CONSTRAINT [FK_FeeSchedules_tblGrades] FOREIGN KEY([Grade_ID])
REFERENCES [dbo].[tblGrades] ([Id])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[tblFeeSchedules] CHECK CONSTRAINT [FK_FeeSchedules_tblGrades]
GO
ALTER TABLE [dbo].[tblParents]  WITH CHECK ADD  CONSTRAINT [FK_Parents_Persons] FOREIGN KEY([LoginDetail_ID])
REFERENCES [dbo].[tblLoginDetails] ([Id])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[tblParents] CHECK CONSTRAINT [FK_Parents_Persons]
GO
ALTER TABLE [dbo].[tblPersons]  WITH CHECK ADD  CONSTRAINT [FK_Persons_Genders] FOREIGN KEY([Gender_ID])
REFERENCES [dbo].[tblGenders] ([Id])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[tblPersons] CHECK CONSTRAINT [FK_Persons_Genders]
GO
ALTER TABLE [dbo].[tblSalaryRecordsDeleted]  WITH CHECK ADD  CONSTRAINT [FK_tblSalaryRecordsDeleted_tblEmployees] FOREIGN KEY([Employee_ID])
REFERENCES [dbo].[tblEmployees] ([Id])
GO
ALTER TABLE [dbo].[tblSalaryRecordsDeleted] CHECK CONSTRAINT [FK_tblSalaryRecordsDeleted_tblEmployees]
GO
ALTER TABLE [dbo].[tblSalaryRecordsDeleted]  WITH CHECK ADD  CONSTRAINT [FK_tblSalaryRecordsDeleted_tblEmployees1] FOREIGN KEY([AddedByEmployee_ID])
REFERENCES [dbo].[tblEmployees] ([Id])
GO
ALTER TABLE [dbo].[tblSalaryRecordsDeleted] CHECK CONSTRAINT [FK_tblSalaryRecordsDeleted_tblEmployees1]
GO
ALTER TABLE [dbo].[tblSalaryRecordsDeleted]  WITH CHECK ADD  CONSTRAINT [FK_tblSalaryRecordsDeleted_tblEmployees2] FOREIGN KEY([DeletedByEmployee_ID])
REFERENCES [dbo].[tblEmployees] ([Id])
GO
ALTER TABLE [dbo].[tblSalaryRecordsDeleted] CHECK CONSTRAINT [FK_tblSalaryRecordsDeleted_tblEmployees2]
GO
ALTER TABLE [dbo].[tblStudentAttendanceRecords]  WITH CHECK ADD  CONSTRAINT [FK_StudentAttendanceRecords_tblStudents] FOREIGN KEY([Student_ID])
REFERENCES [dbo].[tblStudents] ([Id])
GO
ALTER TABLE [dbo].[tblStudentAttendanceRecords] CHECK CONSTRAINT [FK_StudentAttendanceRecords_tblStudents]
GO
ALTER TABLE [dbo].[tblStudentDocuments]  WITH CHECK ADD  CONSTRAINT [FK_tblStudentDocuments_tblStudents] FOREIGN KEY([Student_ID])
REFERENCES [dbo].[tblStudents] ([Id])
GO
ALTER TABLE [dbo].[tblStudentDocuments] CHECK CONSTRAINT [FK_tblStudentDocuments_tblStudents]
GO
ALTER TABLE [dbo].[tblStudentFines]  WITH CHECK ADD  CONSTRAINT [FK_tblStudentFines_tblStudents] FOREIGN KEY([Student_ID])
REFERENCES [dbo].[tblStudents] ([Id])
GO
ALTER TABLE [dbo].[tblStudentFines] CHECK CONSTRAINT [FK_tblStudentFines_tblStudents]
GO
ALTER TABLE [dbo].[tblStudentFinesDeleted]  WITH CHECK ADD  CONSTRAINT [FK_tblStudentFinesDeleted_tblEmployees] FOREIGN KEY([AddedByEmployee_ID])
REFERENCES [dbo].[tblEmployees] ([Id])
GO
ALTER TABLE [dbo].[tblStudentFinesDeleted] CHECK CONSTRAINT [FK_tblStudentFinesDeleted_tblEmployees]
GO
ALTER TABLE [dbo].[tblStudentFinesDeleted]  WITH CHECK ADD  CONSTRAINT [FK_tblStudentFinesDeleted_tblEmployees1] FOREIGN KEY([DeletedByEmployee_ID])
REFERENCES [dbo].[tblEmployees] ([Id])
GO
ALTER TABLE [dbo].[tblStudentFinesDeleted] CHECK CONSTRAINT [FK_tblStudentFinesDeleted_tblEmployees1]
GO
ALTER TABLE [dbo].[tblStudentFinesDeleted]  WITH CHECK ADD  CONSTRAINT [FK_tblStudentFinesDeleted_tblFineStatuses] FOREIGN KEY([FineStatus_ID])
REFERENCES [dbo].[tblFineStatuses] ([Id])
GO
ALTER TABLE [dbo].[tblStudentFinesDeleted] CHECK CONSTRAINT [FK_tblStudentFinesDeleted_tblFineStatuses]
GO
ALTER TABLE [dbo].[tblStudentFinesDeleted]  WITH CHECK ADD  CONSTRAINT [FK_tblStudentFinesDeleted_tblStudents] FOREIGN KEY([Student_ID])
REFERENCES [dbo].[tblStudents] ([Id])
GO
ALTER TABLE [dbo].[tblStudentFinesDeleted] CHECK CONSTRAINT [FK_tblStudentFinesDeleted_tblStudents]
GO
ALTER TABLE [dbo].[tblSubjects]  WITH CHECK ADD  CONSTRAINT [FK_Subjects_Classes] FOREIGN KEY([Class_ID])
REFERENCES [dbo].[tblClasses] ([Id])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[tblSubjects] CHECK CONSTRAINT [FK_Subjects_Classes]
GO
ALTER TABLE [dbo].[tblSubjects]  WITH CHECK ADD  CONSTRAINT [FK_Subjects_tblTeachers] FOREIGN KEY([Teacher_ID])
REFERENCES [dbo].[tblTeachers] ([Id])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[tblSubjects] CHECK CONSTRAINT [FK_Subjects_tblTeachers]
GO
ALTER TABLE [dbo].[tblTeachers]  WITH CHECK ADD  CONSTRAINT [FK_tblTeachers_tblEmployees] FOREIGN KEY([Employee_ID])
REFERENCES [dbo].[tblEmployees] ([Id])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[tblTeachers] CHECK CONSTRAINT [FK_tblTeachers_tblEmployees]
GO
/****** Object:  StoredProcedure [dbo].[procClasses_AddClasses]    Script Date: 9/15/2020 2:17:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[procClasses_AddClasses]
(
   @ClassName nvarchar(50),
   @Grade_ID int,
   @Id INT OUTPUT
)  
AS  
BEGIN  
   insert into tblClasses values(@ClassName,@Grade_ID)
   select @Id= SCOPE_IDENTITY()
END


GO
/****** Object:  StoredProcedure [dbo].[procClassSchedules_AddClassSchedules]    Script Date: 9/15/2020 2:17:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[procClassSchedules_AddClassSchedules]
(
   @WeekDay nvarchar(50),
   @Subject_ID int,
   @Class_ID int,
   @ClassTiming_ID int,
   @Id INT OUTPUT
)  
AS  
BEGIN  
   insert into tblClassSchedules values(@WeekDay,@Subject_ID,@Class_ID,@ClassTiming_ID)
   select @Id= SCOPE_IDENTITY()
END


GO
/****** Object:  StoredProcedure [dbo].[procClassTimings_ClassTimings]    Script Date: 9/15/2020 2:17:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[procClassTimings_ClassTimings]
(
   @StartTime time(7),  
   @Endtime time(7),  
   @Name nvarchar(50), 
   @Id INT OUTPUT
)  
AS  
BEGIN  
   insert into tblClassTimings values(@StartTime,@Endtime,@Name)
   select @Id= SCOPE_IDENTITY()
END


GO
/****** Object:  StoredProcedure [dbo].[procDesignations_Designations]    Script Date: 9/15/2020 2:17:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[procDesignations_Designations]
(
   @DesignationName nvarchar(50),  
   @Id INT OUTPUT
)  
AS  
BEGIN  
   insert into tblDesignations values(@DesignationName)
   select @Id= SCOPE_IDENTITY()
END



GO
/****** Object:  StoredProcedure [dbo].[procDocumentUrls_AddDucumentUrls]    Script Date: 9/15/2020 2:17:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[procDocumentUrls_AddDucumentUrls]
(
   @DocumentLink nvarchar(50),  
   @DateTime datetime,  
   @AddedByEmployee_ID int, 
   @Id INT OUTPUT
)  
AS  
BEGIN  
   insert into tblDocumentUrls values(@DocumentLink,@DateTime,@AddedByEmployee_ID)
   select @Id= SCOPE_IDENTITY()
END



GO
/****** Object:  StoredProcedure [dbo].[procEmployeeAttendanceRecords_AddEmployeeAttendanceRecords]    Script Date: 9/15/2020 2:17:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[procEmployeeAttendanceRecords_AddEmployeeAttendanceRecords]
( 
   @Date date, 
   @Employee_ID int,
   @EmployeeAttendanceSatus_ID int,
   @Id INT OUTPUT
)  
AS  
BEGIN  
   insert into tblEmployeeAttendanceReords values(@Date,@Employee_ID,@EmployeeAttendanceSatus_ID)
   select @Id= SCOPE_IDENTITY()
END


GO
/****** Object:  StoredProcedure [dbo].[procEmployeeAttendanceStatuses_AddEmployeeAttendanceStatuses]    Script Date: 9/15/2020 2:17:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[procEmployeeAttendanceStatuses_AddEmployeeAttendanceStatuses]
(
   @StatusName nvarchar(50),  
   @Id INT OUTPUT
)  
AS  
BEGIN  
   insert into tblEmployeeAttendanceStatuses values(@StatusName)
   select @Id= SCOPE_IDENTITY()
END


GO
/****** Object:  StoredProcedure [dbo].[procEmployeeDocuments_AddEmployeeDocuments]    Script Date: 9/15/2020 2:17:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[procEmployeeDocuments_AddEmployeeDocuments]
(
   @Employee_ID int,  
   @DocumentUrl_ID int,  
   @Id INT OUTPUT
)  
AS  
BEGIN  
   insert into tblEmployeeDocuments values(@Employee_ID,@DocumentUrl_ID)
   select @Id= SCOPE_IDENTITY()
END


GO
/****** Object:  StoredProcedure [dbo].[procEmployeeFines_AddEmployeeFines]    Script Date: 9/15/2020 2:17:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[procEmployeeFines_AddEmployeeFines]
(
   @Fine float,  
   @DateTime datetime,  
   @Note nvarchar(50),
   @AddedByEmployee_ID int,
   @Employee_ID int,
   @FineStatus_ID int,
   @Id INT OUTPUT
)  
AS  
BEGIN  
   insert into tblEmployeeFines values(@Fine,@DateTime,@Note,@AddedByEmployee_ID,@Employee_ID,@FineStatus_ID)
   select @Id= SCOPE_IDENTITY()
END


GO
/****** Object:  StoredProcedure [dbo].[procEmployeeQualifications_AddEmployeeQualifications]    Script Date: 9/15/2020 2:17:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[procEmployeeQualifications_AddEmployeeQualifications]
(
   @Employee_ID int,
   @Qualification_ID int,
   @Id INT OUTPUT
)  
AS  
BEGIN  
   insert into tblEmployeeQualifications values(@Employee_ID,@Qualification_ID)
   select @Id= SCOPE_IDENTITY()
END


GO
/****** Object:  StoredProcedure [dbo].[procEmployees_AddEmployees]    Script Date: 9/15/2020 2:17:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[procEmployees_AddEmployees]
(
   @RegistrationID nvarchar(50),
   @Salary float,
   @IdentityCardUrl nvarchar(50),
   @PostalAddress nvarchar(50),
   @ResidentialAddress nvarchar(50),
   @AggrementUrl  nvarchar(50),
   @Person_ID int,
   @Designation_ID int,
   @EmployeeType_ID int,
   @LoginDetail_ID int,
   @Id INT OUTPUT
)  
AS  
BEGIN  
   insert into tblEmployees values(@RegistrationID,@Salary,@IdentityCardUrl,@PostalAddress,@ResidentialAddress,@AggrementUrl,@Person_ID,@Designation_ID,@EmployeeType_ID,@LoginDetail_ID)
   select @Id= SCOPE_IDENTITY()
END


GO
/****** Object:  StoredProcedure [dbo].[procEmployeeSkills_AddEmployeeSkills]    Script Date: 9/15/2020 2:17:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[procEmployeeSkills_AddEmployeeSkills]
(
   @Employee_ID int,  
   @Skill_ID int,  
   @Id INT OUTPUT
)  
AS  
BEGIN  
   insert into tblEmployeeSkills values(@Employee_ID,@Skill_ID)
   select @Id= SCOPE_IDENTITY()
END


GO
/****** Object:  StoredProcedure [dbo].[procEmployeeTypes_EmployeeTypes]    Script Date: 9/15/2020 2:17:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[procEmployeeTypes_EmployeeTypes]
(
   @EmployeeTypeName nvarchar(50),  
   @Id INT OUTPUT
)  
AS  
BEGIN  
   insert into tblEmployeeTypes values(@EmployeeTypeName)
   select @Id= SCOPE_IDENTITY()
END


GO
/****** Object:  StoredProcedure [dbo].[procExams_AddExams]    Script Date: 9/15/2020 2:17:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[procExams_AddExams]
(
   @TotalMarks float,
   @ObtainedMarks float,
   @DateTime datetime,
   @Student_ID int,
   @Subject_ID int,
   @ExamType_ID int,
   @Id INT OUTPUT
)  
AS  
BEGIN  
   insert into tblExams values(@TotalMarks,@ObtainedMarks,@DateTime,@Student_ID,@Subject_ID,@ExamType_ID)
   select @Id= SCOPE_IDENTITY()
END


GO
/****** Object:  StoredProcedure [dbo].[procExamTypes_ExamTypes]    Script Date: 9/15/2020 2:17:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[procExamTypes_ExamTypes]
(
   @ExamTypeName nvarchar(50),  
   @Id INT OUTPUT
)  
AS  
BEGIN  
   insert into tblExamTypes values(@ExamTypeName)
   select @Id= SCOPE_IDENTITY()
END



GO
/****** Object:  StoredProcedure [dbo].[procExpenseRecords_AddExpenseRecords]    Script Date: 9/15/2020 2:17:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[procExpenseRecords_AddExpenseRecords]
(
   @DateTime datetime,
   @Paid bit,
   @AddedByEmployee_ID int,
   @ExpenseType_ID int,
   @Id INT OUTPUT
)  
AS  
BEGIN  
   insert into tblExpenseRecords values(@DateTime,@Paid,@AddedByEmployee_ID,@ExpenseType_ID)
   select @Id= SCOPE_IDENTITY()
END


GO
/****** Object:  StoredProcedure [dbo].[procExpenseTypes_ExpenseTypes]    Script Date: 9/15/2020 2:17:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[procExpenseTypes_ExpenseTypes]
(
   @ExpenseTypeName nvarchar(50),  
   @Id INT OUTPUT
)  
AS  
BEGIN  
   insert into tblExpenseTypes values(@ExpenseTypeName)
   select @Id= SCOPE_IDENTITY()
END


GO
/****** Object:  StoredProcedure [dbo].[procFeeRecords_AddFeeRecords]    Script Date: 9/15/2020 2:17:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[procFeeRecords_AddFeeRecords]
(
   @DateTime date,
   @Paid bit,
   @AddedByEmployee_ID int,
   @Student_ID int,
   @FeeSchedule_ID int,
   @Id INT OUTPUT
)  
AS  
BEGIN  
   insert into tblFeeRecords values(@DateTime,@Paid,@AddedByEmployee_ID,@Student_ID,@FeeSchedule_ID)
   select @Id= SCOPE_IDENTITY()
END


GO
/****** Object:  StoredProcedure [dbo].[procFeeSchedules_AddFeeSchedules]    Script Date: 9/15/2020 2:17:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[procFeeSchedules_AddFeeSchedules]
(
   @Fee float,
   @Grade_ID int,
   @FeeType_ID int,
   @Id INT OUTPUT
)  
AS  
BEGIN  
   insert into tblFeeSchedules values(@Fee,@Grade_ID,@FeeType_ID)
   select @Id= SCOPE_IDENTITY()
END


GO
/****** Object:  StoredProcedure [dbo].[procFeeTypes_FeeTypes]    Script Date: 9/15/2020 2:17:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[procFeeTypes_FeeTypes]
(
   @FeeTypeName nvarchar(50),  
   @Id INT OUTPUT
)  
AS  
BEGIN  
   insert into tblFeeTypes values(@FeeTypeName)
   select @Id= SCOPE_IDENTITY()
END



GO
/****** Object:  StoredProcedure [dbo].[procFineStatuses_FineStatuses]    Script Date: 9/15/2020 2:17:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[procFineStatuses_FineStatuses]
(
   @FineStatusName nvarchar(50),  
   @Id INT OUTPUT
)  
AS  
BEGIN  
   insert into tblFineStatuses values(@FineStatusName)
   select @Id= SCOPE_IDENTITY()
END



GO
/****** Object:  StoredProcedure [dbo].[procGenders_AddGender]    Script Date: 9/15/2020 2:17:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[procGenders_AddGender] 
(
   @GenderName nvarchar(20),  
   @Genderid INT OUTPUT
)  
AS  
BEGIN  
   insert into tblGenders values(@GenderName)
   select @Genderid= SCOPE_IDENTITY()
END



GO
/****** Object:  StoredProcedure [dbo].[procGrades_Grades]    Script Date: 9/15/2020 2:17:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[procGrades_Grades]
(
   @GradeName nvarchar(50),  
   @Id INT OUTPUT
)  
AS  
BEGIN  
   insert into tblGrades values(@GradeName)
   select @Id= SCOPE_IDENTITY()
END


GO
/****** Object:  StoredProcedure [dbo].[procLoginDetails_LoginDetails]    Script Date: 9/15/2020 2:17:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[procLoginDetails_LoginDetails]
(
   @Username nvarchar(50),  
   @Password nvarchar(50),  
   @AllowAccess bit, 
   @Id INT OUTPUT
)  
AS  
BEGIN  
   insert into tblLoginDetails values(@Username,@Password,@AllowAccess)
   select @Id= SCOPE_IDENTITY()
END



GO
/****** Object:  StoredProcedure [dbo].[procParents_AddParents]    Script Date: 9/15/2020 2:17:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[procParents_AddParents]
( 
   @Person_ID int,
   @LoginDetail_ID int, 
   @Id INT OUTPUT
)  
AS  
BEGIN  
   insert into tblParents values(@Person_ID,@LoginDetail_ID)
   select @Id= SCOPE_IDENTITY()
END


GO
/****** Object:  StoredProcedure [dbo].[procPersons_Persons]    Script Date: 9/15/2020 2:17:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[procPersons_Persons]
(
   @FirstName nvarchar(50),  
   @LastName nvarchar(50), 
   @DateOfBirth date,
   @CNIC nvarchar(50),
   @ImageUrlPath nvarchar(50),
   @Phone1 nvarchar(50),
   @Phone2 nvarchar(50),
   @Gender_ID int, 
   @Id INT OUTPUT
)  
AS  
BEGIN  
   insert into tblPersons values(@FirstName,@LastName,@DateOfBirth,@CNIC,@ImageUrlPath,@Phone1,@Phone2,@Gender_ID)
   select @Id= SCOPE_IDENTITY()
END


GO
/****** Object:  StoredProcedure [dbo].[procQualifications_AddQualifications]    Script Date: 9/15/2020 2:17:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[procQualifications_AddQualifications]
(
   @QualificationName nvarchar(50),  
   @Id INT OUTPUT
)  
AS  
BEGIN  
   insert into tblQualifications values(@QualificationName)
   select @Id= SCOPE_IDENTITY()
END


GO
/****** Object:  StoredProcedure [dbo].[procSalaryRecords_AddSalaryRecords]    Script Date: 9/15/2020 2:17:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[procSalaryRecords_AddSalaryRecords]
( 
   @DateTime DateTime, 
   @Paid bit,
   @PayableAmount decimal(18, 2),
   @Employee_ID int,
   @AddedByEmployee_ID int,
   @Id INT OUTPUT
)  
AS  
BEGIN  
   insert into tblSalaryRecords values(@DateTime,@Paid,@PayableAmount,@Employee_ID,@AddedByEmployee_ID)
   select @Id= SCOPE_IDENTITY()
END


GO
/****** Object:  StoredProcedure [dbo].[procSkills_AddSkills]    Script Date: 9/15/2020 2:17:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[procSkills_AddSkills]
(
   @SkillName nvarchar(50),  
   @Id INT OUTPUT
)  
AS  
BEGIN  
   insert into tblSkills values(@SkillName)
   select @Id= SCOPE_IDENTITY()
END



GO
/****** Object:  StoredProcedure [dbo].[procStudentAttendanceRecords_AddStudentAttendanceRecords]    Script Date: 9/15/2020 2:17:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[procStudentAttendanceRecords_AddStudentAttendanceRecords]
( 
   @Date date, 
   @Student_ID int,
   @StudentAttendanceStatus_ID int,
   @Id INT OUTPUT
)  
AS  
BEGIN  
   insert into tblStudentAttendanceRecords values(@Date,@Student_ID,@StudentAttendanceStatus_ID)
   select @Id= SCOPE_IDENTITY()
END


GO
/****** Object:  StoredProcedure [dbo].[procStudentAttendanceStatuses_AddStudentAttendanceStatuses]    Script Date: 9/15/2020 2:17:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[procStudentAttendanceStatuses_AddStudentAttendanceStatuses]
(
   @StatusName nvarchar(50),  
   @Id INT OUTPUT
)  
AS  
BEGIN  
   insert into tblStudentAttendanceStatuses values(@StatusName)
   select @Id= SCOPE_IDENTITY()
END



GO
/****** Object:  StoredProcedure [dbo].[procStudentDocuments_AddStudentDocuments]    Script Date: 9/15/2020 2:17:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[procStudentDocuments_AddStudentDocuments]
(
   @Student_ID int,  
   @DocumentUrl_ID int,  
   @Id INT OUTPUT
)  
AS  
BEGIN  
   insert into tblStudentDocuments values(@Student_ID,@DocumentUrl_ID)
   select @Id= SCOPE_IDENTITY()
END


GO
/****** Object:  StoredProcedure [dbo].[procStudentFines_AddStudentFines]    Script Date: 9/15/2020 2:17:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[procStudentFines_AddStudentFines]
(
   @Fine float,  
   @DateTime datetime,  
   @Note nvarchar(50),
   @Student_ID int,
   @AddedByEmployee_ID int,
   @FineStatus_ID int,
   @Id INT OUTPUT
)  
AS  
BEGIN  
   insert into tblEmployeeFines values(@Fine,@DateTime,@Note,@Student_ID,@AddedByEmployee_ID,@FineStatus_ID)
   select @Id= SCOPE_IDENTITY()
END


GO
/****** Object:  StoredProcedure [dbo].[procStudents_AddStudents]    Script Date: 9/15/2020 2:17:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[procStudents_AddStudents]
( 
   @RegistrationID nvarchar(50),
   @DiscountPercentage float,
   @IdentityCardUrl nvarchar(50),
   @AdmissionCard nvarchar(50),
   @PostalAddress nvarchar(50),
   @ResidentialAddress nvarchar(50),
   @Person_ID int,
   @LoginDetail_ID int,
   @Parent_ID int,
   @Class_ID int,
   @Id INT OUTPUT
)  
AS  
BEGIN  
   insert into tblStudents values(@RegistrationID,@DiscountPercentage,@IdentityCardUrl,@AdmissionCard,@PostalAddress,@ResidentialAddress,@Person_ID,@LoginDetail_ID,@Parent_ID,@Class_ID)
   select @Id= SCOPE_IDENTITY()
END


GO
/****** Object:  StoredProcedure [dbo].[procSubjects_AddSubjects]    Script Date: 9/15/2020 2:17:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[procSubjects_AddSubjects]
(
   @SubjectName nvarchar(50),
   @Teacher_ID int,
   @Class_ID int,
   @Id INT OUTPUT
)  
AS  
BEGIN  
   insert into tblSubjects values(@SubjectName,@Teacher_ID,@Class_ID)
   select @Id= SCOPE_IDENTITY()
END


GO
/****** Object:  StoredProcedure [dbo].[procTeachers_AddTeachers]    Script Date: 9/15/2020 2:17:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[procTeachers_AddTeachers]
(
   
   @Employee_ID int,
   @Id INT OUTPUT
)  
AS  
BEGIN  
   insert into tblTeachers values(@Employee_ID)
   select @Id= SCOPE_IDENTITY()
END


GO
USE [master]
GO
ALTER DATABASE [SMS] SET  READ_WRITE 
GO
