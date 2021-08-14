using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Three.Models;
using Three.Services;

namespace Three.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly IOptions<ThreeOptions> _threeOptions;

        public IOptions<ThreeOptions> ThreeOptions { get; }

        // 在下面的构造器中进行了 Service 的注入
        public DepartmentController(IDepartmentService departmentService, IOptions<ThreeOptions> threeOptions)
        {
            _departmentService = departmentService;
            _threeOptions = threeOptions;
        }

        // 建立一个默认的 Action
        public async Task<IActionResult> Index()
        {
            ViewBag.Title = "Department Index";
            var departments = await _departmentService.GetAll();
            return View(departments); // View 中可以传入参数
        }

        //[HTTPGet]
        // 该 Action 负责跳转到添加页面，不是异步方法。 设一个空的model 返回回去 等用户添加数据
        public IActionResult Add()
        {
            ViewBag.Title = "Add Department";
            return View(new Department());
        }
        
        // 提交
        [HttpPost]
        public async Task<IActionResult> Add(Department model)
        {
            if (ModelState.IsValid)
            {
                await _departmentService.Add(model);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
