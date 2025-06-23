using KykMotorApp.WebIU.Areas.Admin.Models;
using KykMotorApp.WebIU.Controllers;
using KykShopApp.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace KykMotorApp.WebIU.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        public RoleController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
     
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.Select(x => new RoleViewModel()
            {
                Id = x.Id,
                Name = x.Name,
            }).ToListAsync();

            return View(roles);
        }
     
        [HttpGet]
        public IActionResult RoleCreate()
        {
            return View(new RoleCreateViewModel());
        }
     
        [HttpPost]
        public async Task<IActionResult> RoleCreate(RoleCreateViewModel request)
        {
            var result = await _roleManager.CreateAsync(new ApplicationRole()
            {
                Name = request.Name,
            });
            if (!result.Succeeded)
            {
                return NotFound();
            }
            if (result == null)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(RoleController.Index));
        }
         [HttpGet]
        public async Task<IActionResult> AssignRoleToUser(string Id)
        {

            var currenUser = await _userManager.FindByIdAsync(Id);

            ViewBag.userId = Id;
            var roles = await _roleManager.Roles.ToListAsync();
            var userRoles = await _userManager.GetRolesAsync(currenUser);
            var roleViewModelList = new List<AssignRoleToViewModel>();


            foreach (var role in roles)
            {
                var assignRoleToViewModel = new AssignRoleToViewModel()
                {
                    Id = role.Id!,
                    Name = role.Name!,
                };

                if (userRoles.Contains(role.Name))
                {
                    assignRoleToViewModel.Exist = true;
                }
                roleViewModelList.Add(assignRoleToViewModel);
            }

            return View(roleViewModelList);
        }

       
        [HttpPost] 
        public async Task<IActionResult> AssignRoleToUser(string userId, List<AssignRoleToViewModel> requestList)
        {

            var userToAssignRoles = await _userManager.FindByIdAsync(userId);
            foreach (var role in requestList)
            {
                if (role.Exist)
                {
                    await _userManager.AddToRoleAsync(userToAssignRoles, role.Name);
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(userToAssignRoles, role.Name);
                }
            }
            return RedirectToAction(nameof(UserController.UserList), "User");
        }


    }
}
