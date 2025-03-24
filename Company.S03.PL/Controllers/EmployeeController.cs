using AutoMapper;
using Company.S03.BLL.Interface;
using Company.S03.BLL.Repositories;
using Company.S03.DAL.Models;
using Company.S03.PL.Dtos;
using Company.S03.PL.Mapping;
using Microsoft.AspNetCore.Mvc;

namespace Company.S03.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public IDepartmentRepository _departmentRepository { get; }

        public EmployeeController(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository , IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Index(string? SearchInput)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchInput))
            {
                 employees = _employeeRepository.GetAll();
            }
            else
            {
                 employees = _employeeRepository.GetByName(SearchInput);
            }

                //ViewData["Message"] = "Welcome to Employee Page";

                //ViewBag.Message = "Welcome to Employee Page";
                return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var departments = _departmentRepository.GetAll();
            ViewBag.departments = departments;

            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateEmployeeDto model)
        {

            if (ModelState.IsValid) //Server Side Validation
            {
                try
                {
                    ////Manual Mapping
                    //var employee = new Employee
                    //{
                    //    Name = model.Name,
                    //    Age = model.Age,
                    //    Email = model.Email,
                    //    Phone = model.Phone,
                    //    Address = model.Address,
                    //    Salary = model.Salary,
                    //    IsActive = model.IsActive,
                    //    IsDeleted = model.IsDeleted,
                    //    HiringDate = model.HiringDate,
                    //    CreateAt = model.CreateAt,
                    //    DepartmentId = model.DepartmentId
                    //};

                    //AutoMapper
                    var employee = _mapper.Map<Employee>(model);

                    var count = _employeeRepository.Add(employee);
                    if (count > 0)
                    {
                        TempData["Message"] = "Employee Added Successfully";
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (id is null) return BadRequest("Invalid Id");

            var employee = _employeeRepository.Get(id.Value);

            if (employee is null) return NotFound(new { StatusCode = 404, Message = $"Employee with Id {id} is Not Found" });

            return View(viewName, employee);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var departments = _departmentRepository.GetAll();
            ViewBag.departments = departments;

            if (id is null) return BadRequest("Invalid Id");

            var employee = _employeeRepository.Get(id.Value);

            if (employee is null) return NotFound(new { StatusCode = 404, Message = $"Employee with Id {id} is Not Found" });
            var employeeDto = new CreateEmployeeDto
            {
                Name = employee.Name,
                Age = employee.Age,
                Email = employee.Email,
                Phone = employee.Phone,
                Address = employee.Address,
                Salary = employee.Salary,
                IsActive = employee.IsActive,
                IsDeleted = employee.IsDeleted,
                HiringDate = employee.HiringDate,
                CreateAt = employee.CreateAt,
                DepartmentId = employee.DepartmentId
            };
            return View(employeeDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Employee model)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee
                {
                    Id = id,
                    Name = model.Name,
                    Age = model.Age,
                    Email = model.Email,
                    Phone = model.Phone,
                    Address = model.Address,
                    Salary = model.Salary,
                    IsActive = model.IsActive,
                    IsDeleted = model.IsDeleted,
                    HiringDate = model.HiringDate,
                    CreateAt = model.CreateAt,
                    DepartmentId = model.DepartmentId
                };
                var count = _employeeRepository.Update(employee);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));//دا عشان يعمل تعديل ويغيره ف اviewوال db
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id)
        {
            var employee = _employeeRepository.Get(id);
            if (employee == null)
            {
                return NotFound();
            }

            var count = _employeeRepository.Delete(employee);
            if (count > 0)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(employee);
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Delete([FromRoute] int id, Employee employee)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (id == employee.Id)
        //        {
        //            var count = _employeeRepository.Delete(employee);
        //            if (count > 0)
        //            {
        //                return RedirectToAction(nameof(Index));//دا عشان يعمل تعديل ويغيره ف ال view و ال db
        //            }
        //        }
        //    }
        //    return View(employee);
        //}
    }
}
