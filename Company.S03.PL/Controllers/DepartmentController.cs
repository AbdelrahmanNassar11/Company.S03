using Company.S03.BLL.Interface;
using Company.S03.BLL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Company.S03.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentController(IDepartmentRepository departmentRepository )
        {
            _departmentRepository = departmentRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {
         
            var departments = _departmentRepository.GetAll();
            return View(departments);
        }
    }
}
