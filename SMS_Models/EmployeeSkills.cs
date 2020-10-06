using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_Models
{
    class EmployeeSkills
    {
        private static string Command;
        private static Database_Connection db = new Database_Connection();
        public int Id { get; set; }
        public int EmployeesId { get; set; }
        public int SkillsId { get; set; }
        public Employees Employees { get; set; }
        public Skills Skills { get; set; }
        public override string ToString()
        {
            return "{ [ id: " + Id + ", employeeid: " + EmployeesId + ", skillsid " + SkillsId + "  ]"
                        + Employees + Skills + "}";
        }
        public static List<EmployeeSkills> ListOfEmployeeSkills
        {
            get
            {
                return _GetEmployeeSkills();
            }
            private set
            {

            }
        }
        public EmployeeSkills()
        {
            Employees = new Employees();
            Skills = new Skills();
        }
        private static List<EmployeeSkills> _GetEmployeeSkills()
        {
            List<EmployeeSkills> EmployeeSkills = new List<EmployeeSkills>();


            try
            {
                Command = "select * from tblEmployeeSkills";
                db.cmd.CommandText = Command;
                db.con.Open();

                SqlDataReader rdr = db.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    EmployeeSkills singleEmployeeSkills = new EmployeeSkills();
                    singleEmployeeSkills.Id = (int)rdr[0];
                    singleEmployeeSkills.EmployeesId = (int)rdr[1];
                    singleEmployeeSkills.SkillsId = (int)rdr[2];

                    var emp = new Employees();
                    singleEmployeeSkills.Employees = Employees.ListOfEmployees.SingleOrDefault(e => e.Id == singleEmployeeSkills.EmployeesId);

                    var skills = new Skills();
                    singleEmployeeSkills.Skills = Skills.ListOfSkills.SingleOrDefault(s => s.Id == singleEmployeeSkills.SkillsId);


                    EmployeeSkills.Add(singleEmployeeSkills);
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
            return EmployeeSkills;
        }

        public static int Add(EmployeeSkills employeeSkills)
        {
            int retvalue = -1;
            try
            {
                
                db.cmd.CommandType = CommandType.StoredProcedure;
                db.cmd.CommandText = "procEmployeeSkills_AddEmployeeSkills";
                db.cmd.Parameters.AddWithValue("@Employee_ID", employeeSkills.EmployeesId);
                db.cmd.Parameters.AddWithValue("@Skill_ID", employeeSkills.SkillsId);
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
        public static int Add(EmployeeSkills employeeSkills,Skills skills)
        {
            int retvalue = -1;
            employeeSkills.SkillsId = Skills.Add(skills);
            retvalue = EmployeeSkills.Add(employeeSkills);
            return retvalue; 
        }
        public static int Add(EmployeeSkills employeeSkills, Employees employees)
        {
            int retvalue = -1;
            employeeSkills.EmployeesId = Employees.Add(employees);
            retvalue = EmployeeSkills.Add(employeeSkills);
            return retvalue;
        }

        public static int Add(EmployeeSkills employeeSkills, Employees employees, Skills skills)
        {
            int retvalue = -1;
            employeeSkills.EmployeesId = Employees.Add(employees);
            employeeSkills.SkillsId = Skills.Add(skills);
            retvalue = EmployeeSkills.Add(employeeSkills);
            return retvalue;
        }

        public static void Delete(int id)
        {
            try
            {
            db.cmd.CommandText = "delete from tblEmployeeSkills  where Id=@id";
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
