using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Employee_Management_Project.Models
{
    public class EmployeeModel
    {
        public EmployeeModel()
         {
            // Initialize the list in the constructor
            Employees = new List<Employee>();
        }

        public int Id { get; set; }

        public string Password { get; set; }
        public String Emp_Name { get; set; }

        public String Emp_Designation { get; set; }

        public int Emp_Salary { get; set; }

        public String Emp_Address { get; set; }

        public String Emp_MobileNo { get; set; }

        public String Emp_Email { get; set; }

        public List<Employee> Employees { get; set; }
    }
}