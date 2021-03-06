﻿using empPayrollDemo.Models;
using empPayrollDemo.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace empPayrollDemo.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        public ApplicationDbContext db = new ApplicationDbContext();
        
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterEmployee(RegisterEmpRequestModel employee)
        {
            bool result = false;
            if (ModelState.IsValid)
            {
                result = this.RegisterEmployeeService(employee);
            }
            ModelState.Clear();
            if (result == true)
            {
                return RedirectToAction("EmployeeList");
            }
            return View("Register", employee);
        }

        public bool RegisterEmployeeService(RegisterEmpRequestModel employee)
        {
            try
            {
                Employee validEmployee = db.Employees.Where(x => x.Name == employee.Name && x.Gender == employee.Gender).FirstOrDefault();
                if(validEmployee == null)
                {
                    int departmentId = db.Departments.Where(x => x.DepartmentName == employee.Department).Select(x => x.DepartmentId).FirstOrDefault();
                    Employee newEmployee = new Employee()
                    {
                        Name = employee.Name,
                        Gender = employee.Gender,
                        DepartmentId = departmentId,
                        SalaryId = Convert.ToInt32(employee.SalaryId),
                        StartDate = employee.StartDate,
                        Description = employee.Description
                    };
                    Employee returnData = db.Employees.Add(newEmployee);
                }
                int result = db.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public ActionResult EmployeeList()
        {
            List<EmployeeViewModel> list = GetAllEmployee();
            return View(list);
        }

        public List<EmployeeViewModel> GetAllEmployee()
        {
            try
            {
                List<EmployeeViewModel> list = (from e in db.Employees
                                                join d in db.Departments on e.DepartmentId equals d.DepartmentId
                                                join s in db.Salaries on e.SalaryId equals s.SalaryId
                                                select new EmployeeViewModel
                                                {
                                                    EmpId = e.EmpId,
                                                    Name = e.Name,
                                                    Gender = e.Gender,
                                                    DepartmentId = d.DepartmentId,
                                                    Department = d.DepartmentName,
                                                    SalaryId = s.SalaryId,
                                                    Amount = s.Amount,
                                                    StartDate = e.StartDate,
                                                    Description = e.Description
                                                }).ToList<EmployeeViewModel>();
                return list;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ActionResult Edit(EmployeeViewModel data)
        {
            RegisterEmpRequestModel employee = new RegisterEmpRequestModel
            {
                EmpId = data.EmpId,
                Name = data.Name,
                Gender = data.Gender,
                Department = data.Department,
                //used tostring method to convert int to string
                SalaryId = data.SalaryId.ToString(),
                StartDate = data.StartDate,
                Description = data.Description
            };
            return View(employee);
        }

        public ActionResult EditEmployee(RegisterEmpRequestModel employee)
        {
            bool result = EditEmployeeService(employee);
            if (result == true)
            {
                List<EmployeeViewModel> list = GetAllEmployee();
                return View("EmployeeList", list);
            }
            else
            {
                return View("Error");
            }
        }
        public bool EditEmployeeService(RegisterEmpRequestModel employee)
        {
            try
            {
                int departmentId = db.Departments.Where(x => x.DepartmentName == employee.Department).Select(x => x.DepartmentId).FirstOrDefault();

                Employee emp = db.Employees.Find(employee.EmpId); //searching employee using employee id
                emp.Name = employee.Name;
                emp.Gender = employee.Gender;
                emp.SalaryId = Convert.ToInt32(employee.SalaryId);
                emp.StartDate = employee.StartDate;
                emp.Description = employee.Description;
                emp.DepartmentId = departmentId;

                int result = db.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ActionResult Delete(int id)
        {
            bool result = DeleteEmployee(id);
            if (result == true)
            {
                List<EmployeeViewModel> list = GetAllEmployee();
                return View("EmployeeList", list);
            }
            else
            {
                return View("Error");
            }
        }
        public bool DeleteEmployee(int id)
        {
            try
            {
                Employee employee = db.Employees.Find(id);
                if (employee == null)
                {
                    return false;
                }
                db.Employees.Remove(employee);
                int result = db.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}

