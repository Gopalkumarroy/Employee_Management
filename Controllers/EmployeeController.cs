using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Employee_Management_Project.Controllers.LoginController;

namespace Employee_Management_Project.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Employee model)
        {
            EmpDbEntities db = new EmpDbEntities();
            Employee tbl = new Employee();
            tbl.Emp_Address = model.Emp_Address;
            tbl.Emp_Designation = model.Emp_Designation;
            tbl.Emp_Email = model.Emp_Email;
            tbl.Emp_MobileNo = model.Emp_MobileNo;
            tbl.Emp_Name = model.Emp_Name;
            tbl.Emp_Salary = model.Emp_Salary;
            tbl.Password = model.Password;

            db.Employees.Add(tbl);
            db.SaveChanges();

            TempData["MessageUPdate"] = "Item successfully Added!";

            // return View();
            return RedirectToAction("ViewPage");
           // return RedirectToAction("Index");

            //return View();
        }

        [SessionAuthorize]
        public ActionResult ViewPage()
        {
            Employee model = new Employee();
            EmpDbEntities db = new EmpDbEntities();

            //fetch data from database
          var data =  db.Employees.OrderByDescending(x => x.Id).ToList();


         foreach(var item in data)
            {
                // Create a new Student object for each record
                Employee xy = new Employee();
                xy.Id = item.Id;
                xy.Emp_Address = item.Emp_Address;
                xy.Emp_Designation = item.Emp_Designation;
                xy.Emp_Email = item.Emp_Email;
                xy.Emp_MobileNo = item.Emp_MobileNo;
                xy.Emp_Name = item.Emp_Name;
                xy.Emp_Salary = item.Emp_Salary;
                model.Employees.Add(xy);
            }
            return View(model);
        }
        //Edit the data 
        public ActionResult EditPage(int id)



        {
            Employee model = new Employee();
            EmpDbEntities db = new EmpDbEntities();
            var detail = db.Employees.FirstOrDefault(x => x.Id == id);
            if (detail != null)
            {
                model.Id = detail.Id;
                model.Emp_MobileNo = detail.Emp_MobileNo;
                model.Emp_Name = detail.Emp_Name;
                model.Emp_Designation = detail.Emp_Designation;
                model.Emp_Address = detail.Emp_Address;
                model.Emp_Salary = detail.Emp_Salary;
                model.Emp_Email = detail.Emp_Email;

            }

            return View(model);
        }

        //Update the Viewpage
        [HttpPost]

        public ActionResult Update(Employee obj)
        {
            EmpDbEntities db = new EmpDbEntities();
            //Tbl_Student t = new Tbl_Student();
            var existingEmployee = db.Employees.FirstOrDefault(s => s.Id == obj.Id);
            existingEmployee.Emp_Address = obj.Emp_Address;
            existingEmployee.Emp_Email= obj.Emp_Email;
            existingEmployee.Emp_Designation = obj.Emp_Designation;
            existingEmployee.Emp_MobileNo = obj.Emp_MobileNo;
            existingEmployee.Emp_Salary = obj.Emp_Salary;
            existingEmployee.Emp_Name = obj.Emp_Name;
            db.SaveChanges();

            TempData["MessageUPdate"] = "Item successfully updated!";


            // return RedirectToAction("EditPage");
            // return RedirectToAction("ViewPage");
            return RedirectToAction("EditPage", new { id = obj.Id });

        }

        //Delete from viewPage
        public ActionResult Delete(int id)
        {
            //Student model = new Student();
            EmpDbEntities db = new EmpDbEntities();
            var detail = db.Employees.FirstOrDefault(x => x.Id == id);
            if (detail != null)
            {
                db.Employees.Remove(detail);

                db.SaveChanges();

                TempData["Message"] = "Record deleted successfully.";
            }


            return RedirectToAction("ViewPage");
        }

        //View Details of the page
        public ActionResult DetailPage(int id)
        {
            Employee model = new Employee();
            EmpDbEntities db = new EmpDbEntities();
            // Tbl_Student t = new Tbl_Student();
            var detail = db.Employees.FirstOrDefault(x => x.Id == id);

            if (detail != null)
            {
                model.Id = detail.Id;
                model.Emp_Name = detail.Emp_Name;
                model.Emp_Address = detail.Emp_Address;
                model.Emp_Designation = detail.Emp_Designation;
                model.Emp_MobileNo = detail.Emp_MobileNo;
                model.Emp_Salary = detail.Emp_Salary;
                model.Emp_Email = detail.Emp_Email;
            }

            return View(model);


        }


    }
}