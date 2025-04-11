using Company.S03.DAL.Models;
using Company.S03.PL.Dtos;
using Company.S03.PL.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Company.S03.PL.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public UserController(UserManager<AppUser> userManager)
        {
           _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string? SearchInput)
        {
            var userList = await _userManager.Users.ToListAsync(); // افصل الـ IQueryable عن EF
            var filteredUsers = string.IsNullOrEmpty(SearchInput)
                ? userList
                : userList.Where(u => u.UserName.ToLower().Contains(SearchInput.ToLower())).ToList();

            var users = new List<UserToReturnDto>();

            foreach (var user in filteredUsers)
            {
                var roles = await _userManager.GetRolesAsync(user); // async بشكل آمن
                users.Add(new UserToReturnDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    FristName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Roles = roles
                });
            }

            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string? id, string viewName = "Details")
        {
            if (id is null) return BadRequest("Invalid Id");

            var user = await _userManager.FindByIdAsync(id);

            if (user is null) return NotFound(new { StatusCode = 404, Message = $"User with Id {id} is Not Found" });
            var dto = new UserToReturnDto()
            {
                Id=user.Id,
                UserName = user.UserName,
                FristName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Roles = _userManager.GetRolesAsync(user).Result
            };
            return View(viewName, dto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            return await Details(id,"Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, UserToReturnDto model)
        {
            if (ModelState.IsValid)
            {
                if(id!=model.Id)
                    return BadRequest("Invalid Operations !!");
                var user = await _userManager.FindByIdAsync(id);
                if(user is null) 
                    return BadRequest("Invalid Operations !!");
                user.UserName = model.UserName;
                user.FirstName = model.FristName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string id, UserToReturnDto model)
        {
            if (ModelState.IsValid)
            {
                if (id != model.Id)
                    return BadRequest("Invalid Operations !!");
                var user = await _userManager.FindByIdAsync(id);
                if (user is null)
                    return BadRequest("Invalid Operations !!");
               
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }

    }
}
