using Company.S03.DAL.Models;
using Company.S03.PL.Dtos;
using Company.S03.PL.Helpers;
using Company.S03.PL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using System.Threading.Tasks;

namespace Company.S03.PL.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager,UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string? SearchInput)
        {
            IEnumerable<RolesToReturnDto> roles;
            if (string.IsNullOrEmpty(SearchInput))
            {
                roles = _roleManager.Roles.Select(R => new RolesToReturnDto()
                {
                    Id = R.Id,
                    Name = R.Name
                });

            }
            else
            {
                roles = _roleManager.Roles.Select(R => new RolesToReturnDto()
                {
                    Id = R.Id,
                    Name = R.Name
                }).Where(R => R.Name.ToLower().Contains(SearchInput.ToLower()));
            }
            return View(roles);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RolesToReturnDto model)
        {

            if (ModelState.IsValid) //Server Side Validation
            {
                var role = await _roleManager.FindByNameAsync(model.Name);
                if (role is null)
                {
                    role = new IdentityRole()
                    {
                        Name = model.Name
                    };
                    var result = await _roleManager.CreateAsync(role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Details(string? id, string viewName = "Details")
        {
            if (id is null) return BadRequest("Invalid Id");

            var role = await _roleManager.FindByIdAsync(id);

            if (role is null) return NotFound(new { StatusCode = 404, Message = $"Role with Id {id} is Not Found" });
            var dto = new RolesToReturnDto()
            {
                Id = role.Id,
                Name = role.Name
            };
            return View(viewName, dto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            return await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, RolesToReturnDto model)
        {
            if (ModelState.IsValid)
            {
                if (id != model.Id)
                    return BadRequest("Invalid Operations !!");
                var role = await _roleManager.FindByIdAsync(id);
                if (role is null)
                    return BadRequest("Invalid Operations !!");
                var roleResult = await _roleManager.FindByNameAsync(model.Name);
                if (roleResult is null)
                {
                    role.Name = model.Name;
                    var result = await _roleManager.UpdateAsync(role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                ModelState.AddModelError("", "Invalid Operation !!");
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
        public async Task<IActionResult> Delete([FromRoute] string id, RolesToReturnDto model)
        {
            if (ModelState.IsValid)
            {
                if (id != model.Id)
                    return BadRequest("Invalid Operations !!");
                var role = await _roleManager.FindByIdAsync(id);
                if (role is null)
                    return BadRequest("Invalid Operations !!");

                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddOrRemoveUser(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role is null)
                return NotFound(new { StatusCode = 404, Message = $"Role with Id {roleId} is Not Found" });
            ViewData["RoleId"] = roleId;
            var usersInRole = new List<UserInRoleViewModel>();
            var users = await _userManager.Users.ToListAsync();
            foreach (var user in users)
            {
                var userInRole = new UserInRoleViewModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                };
                if(await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userInRole.IsSelected = true;
                }
                else
                {
                    userInRole.IsSelected = false;
                }
                usersInRole.Add(userInRole);
            }
            return View(usersInRole);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUser(string roleId,List<UserInRoleViewModel> users)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role is null)
                return NotFound(new { StatusCode = 404, Message = $"Role with Id {roleId} is Not Found" });
            if (ModelState.IsValid)
            {
                foreach (var user in users)
                {
                    var appUser = await _userManager.FindByIdAsync(user.UserId);
                    if (appUser is not null)
                    {
                        if (user.IsSelected && !await _userManager.IsInRoleAsync(appUser,role.Name))
                        {
                            await _userManager.AddToRoleAsync(appUser,role.Name);
                        }
                        else if (!user.IsSelected && await _userManager.IsInRoleAsync(appUser, role.Name))
                        {
                            await _userManager.RemoveFromRoleAsync(appUser, role.Name);
                        }
                    }
                }
                return RedirectToAction(nameof(Edit),new {id = roleId});
            }
            return View(users);
        }
    }
}
