using AutoMapper;
using Company.S03.BLL.Interface;
using Company.S03.BLL.Repositories;
using Company.S03.DAL.Models;
using Company.S03.PL.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.Threading.Tasks;

namespace Company.S03.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DepartmentController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
         
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            return View(departments);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDepartmentDto model)
        {

            if(ModelState.IsValid) //Server Side Validation
            {
                var department = _mapper.Map<Department>(model);

                await _unitOfWork.DepartmentRepository.AddAsync(department);
                var count = await _unitOfWork.CompleteAsync();
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }           
            }
            
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id , string viewName = "Details")
        {
            if (id is null) return BadRequest("Invalid Id");

            var department = await _unitOfWork.DepartmentRepository.GetAsync(id.Value);

            if (department is null) return NotFound(new { StatusCode = 404, Message = $"Department with Id {id} is Not Found" });
             
            return View(viewName,department);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest("Invalid Id");

            var department = await _unitOfWork.DepartmentRepository.GetAsync(id.Value);

            if (department is null) return NotFound(new { StatusCode = 404, Message = $"Department with Id {id} is Not Found" });
            var departmentDto = new CreateDepartmentDto
            {
                Code = department.Code,
                Name = department.Name,
                CreateAt = department.CreateAt
            };
            return View (departmentDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id,CreateDepartmentDto model)
        {
            if (ModelState.IsValid)
            {
                var department = _mapper.Map<Department>(model);

                _unitOfWork.DepartmentRepository.Update(department);
                var count = await _unitOfWork.CompleteAsync();
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));//دا عشان يعمل تعديل و يغيرهفال view و ال db
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id,"Delete");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var department = await _unitOfWork.DepartmentRepository.GetAsync(id);
            if (department == null)
            {
                return NotFound();
            }
        
            _unitOfWork.DepartmentRepository.Delete(department);
            var count = await _unitOfWork.CompleteAsync();
            if (count > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            
            return View(department);
        }
    }
}
