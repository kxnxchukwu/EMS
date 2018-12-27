using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMS.Models;
using System.Data.Entity;
using System.Data;
using ClosedXML.Excel;
using System.IO;

namespace EMS.Controllers
{
    public class EmployeeController : Controller
    {
        private EmployeeEntity context = new EmployeeEntity();

        // GET: Employee
        public ActionResult Index(string option, string search)
        {
            var employees = context.Employees.ToList();

            if (option == "Gender")
            {
                return View(context.Employees.Where(x => x.gender == search || search == null).ToList());
            }
            if (option == "Name")
            {
                return View(context.Employees.Where(x => x.fullName.StartsWith(search)).ToList());
            }
            if (option == "Department")
            {
                return View(context.Employees.Where(x => x.department.StartsWith(search)).ToList());
            }
            return View(employees);
        }

        [HttpPost]
        public FileResult Export()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[8] {
                new DataColumn ("EmployeeID"),
                new DataColumn("Full Name"),
                new DataColumn("Email"),
                new DataColumn("Phone Number"),
                new DataColumn("Age"),
                new DataColumn("Salary"),
                new DataColumn("Gender"),
                new DataColumn("Department")
            });

            var employees = from employee in context.Employees.Take(10) select employee;

            foreach (var employee in employees)
            {
                dt.Rows.Add(employee.ID, employee.fullName, employee.email, employee.phoneNumber, employee.age, employee.salary, employee.gender, employee.department);
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt); 
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Summary.xlsx");
                }
            }              
        }

        // Create Employee
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Employees employee)
        {
            if (ModelState.IsValid)
            {
                context.Employees.Add(employee);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return Content("Bad Request");
            }

            var employee = context.Employees.SingleOrDefault(e => e.ID == id);

            if (employee == null)
            {
                return Content("Bad Request");
            }
            return View(employee);
        }

        [HttpPost]
        public ActionResult Edit(Employees employee)
        {
            if (ModelState.IsValid)
            {
                context.Entry(employee).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return Content("Bad Request");
            }

            var employee = context.Employees.SingleOrDefault(e => e.ID == id);

            if (employee == null)
            {
                return Content("Bad Request");
            }
            return View(employee);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return Content("Bad Request");
            }

            var employee = context.Employees.SingleOrDefault(e => e.ID == id);

            if (employee == null)
            {
                return Content("Bad Request");
            }
            return View(employee);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var employee = context.Employees.Find(id);
            var employeeDetails = context.Employees.Find(id);
            //var employee = context.Employees.SingleOrDefault(e => e.ID == id);
            var demployee = new deletedEmployees();

            demployee.fullName = employeeDetails.fullName;
            demployee.email = employeeDetails.email;
            demployee.phoneNumber = employeeDetails.phoneNumber;
            demployee.age = employeeDetails.age;
            demployee.salary = employeeDetails.salary;
            demployee.gender = employeeDetails.gender;
            demployee.department = employeeDetails.department;

            context.deletedEmployees.Add(demployee);
            context.SaveChanges();

            context.Employees.Remove(employee);
            context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}