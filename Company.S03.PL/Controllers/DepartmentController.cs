using Company.S03.BLL.Interface;
using Company.S03.BLL.Repositories;
using Company.S03.DAL.Models;
using Company.S03.PL.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateDepartmentDto model)
        {

            if(ModelState.IsValid) //Server Side Validation
            {
                var department = new Department
                {
                    Code = model.Code,
                    Name = model.Name,
                    CreateAt = model.CreateAt
                };

                var count = _departmentRepository.Add(department);

                if(count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }           
            }
            
            return View(model);
        }

        [HttpGet]
        public IActionResult Details(int? id , string viewName = "Details")
        {
            if (id is null) return BadRequest("Invalid Id");

            var department = _departmentRepository.Get(id.Value);

            if (department is null) return NotFound(new { StatusCode = 404, Message = $"Department with Id {id} is Not Found" });
             
            return View(viewName,department);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            //if (id is null) return BadRequest("Invalid Id");

            //var department = _departmentRepository.Get(id.Value);

            //if (department is null) return NotFound(new { StatusCode = 404, Message = $"Department with Id {id} is Not Found" });

            return Details(id,"Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id,Department department)
        {
            if (ModelState.IsValid)
            {
               if(id == department.Id)
                {
                    var count = _departmentRepository.Update(department);
                    if (count > 0)
                    {
                        return RedirectToAction(nameof(Index));//دا عشان يعمل تعديل ويغيره ف ال view و ال db
                    }
                }
            }

            return View(department);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            //if (id is null) return BadRequest("Invalid Id");

            //var department = _departmentRepository.Get(id.Value);

            //if (department is null) return NotFound(new { StatusCode = 404, Message = $"Department with Id {id} is Not Found" });

            return Details(id,"Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, Department department)
        {
            if (ModelState.IsValid)
            {
                if (id == department.Id)
                {
                    var count = _departmentRepository.Delete(department);
                    if (count > 0)
                    {
                        return RedirectToAction(nameof(Index));//دا عشان يعمل تعديل ويغيره ف ال view و ال db
                    }
                }
            }
            return View(department);
        }
    }
}
