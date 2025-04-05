using AutoMapper;
using Company.S03.BLL.Interface;
using Company.S03.BLL.Repositories;
using Company.S03.DAL.Models;
using Company.S03.PL.Dtos;
using Company.S03.PL.Helpers;
using Company.S03.PL.Mapping;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Company.S03.PL.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string? SearchInput)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchInput))
            {
                 employees = await _unitOfWork.EmployeeRepository.GetAllAsync();
            }
            else
            {
                 employees = await _unitOfWork.EmployeeRepository.GetByNameAsync(SearchInput);
            }
            return View(employees);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            ViewBag.departments = departments;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeDto model)
        {

            if (ModelState.IsValid) //Server Side Validation
            {
                try
                {
                    //Upload Image
                    if (model.Image is not null)
                        model.ImageName = DocumentSetting.UploadFile(model.Image, "Images");
                   
                    //AutoMapper
                    var employee = _mapper.Map<Employee>(model);

                    await _unitOfWork.EmployeeRepository.AddAsync(employee);
                    var count = await _unitOfWork.CompleteAsync();
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
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id, string viewName = "Details")
        {
            if (id is null) return BadRequest("Invalid Id");

            var employee = await _unitOfWork.EmployeeRepository.GetAsync(id.Value);

            if (employee is null) return NotFound(new { StatusCode = 404, Message = $"Employee with Id {id} is Not Found" });

            return View(viewName, employee);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            ViewBag.departments = departments;

            if (id is null) return BadRequest("Invalid Id");

            var employee = await _unitOfWork.EmployeeRepository.GetAsync(id.Value);

            if (employee is null) return NotFound(new { StatusCode = 404, Message = $"Employee with Id {id} is Not Found" });

            var employeeDto = _mapper.Map<CreateEmployeeDto>(employee);
            return View(employeeDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, CreateEmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                if (model.ImageName is not null && model.Image is not null)
                {
                    DocumentSetting.DeleteFile(model.ImageName, "Images");
                }

                if (model.Image is not null)
                {
                    model.ImageName = DocumentSetting.UploadFile(model.Image, "Images");
                }

                var employee = _mapper.Map<Employee>(model);
                
                _unitOfWork.EmployeeRepository.Update(employee);
                var count = await _unitOfWork.CompleteAsync();
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index)); 
                }
            }
  
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync(); 
            ViewData["departments"] = departments;

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id,CreateEmployeeDto model)
        {
            var employee = _mapper.Map<Employee>(model);
            employee.Id = id;
            _unitOfWork.EmployeeRepository.Delete(employee);
            var count = await _unitOfWork.CompleteAsync();
            if (count > 0)
            {
                if(model.ImageName is not null)
                {
                    DocumentSetting.DeleteFile(model.ImageName, "Images");
                }
                return RedirectToAction(nameof(Index));
            }

            return View(employee);
        }
    }
}
