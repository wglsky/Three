using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Three.Models;
using Three.Services;

namespace Three.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IDepartmentService departmentService, IEmployeeService employeeService)
        {
            _departmentService = departmentService;
            _employeeService = employeeService;
        }

        // 显示某部门的全部员工
        public async Task<IActionResult> Index(int departmentId)
        {
            var department = await this._departmentService.GetById(departmentId);
            ViewBag.Title = $"Employee of {department.Name}";
            ViewBag.DepartmentId = departmentId;
            var employees = await _employeeService.GetByDepartmentId(departmentId);
            return View(employees);
        }

        // 向某部门添加员工
        public IActionResult Add(int departmentId)
        {
            ViewBag.Title = "Add Employee";
            // 返回一个 model 到页面
            return View(new Employee
            {
                DepartmentId = departmentId
            });
        }

        [HttpPost]
        public async Task<IActionResult> Add(Employee model)
        {
            if (ModelState.IsValid)
            {
                await _employeeService.Add(model);
            }
            return RedirectToAction(nameof(Index), new { departmentId = model.DepartmentId });
        }


        public async Task<IActionResult> Fire(int employeeId)
        {
            var employee = await _employeeService.Fire(employeeId);
            return RedirectToAction(nameof(Index), new { departmentId = employee.DepartmentId });
        }

    }
}
