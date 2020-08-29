using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AutoService.Models;
using AutoService.DAL.Entities;
using Microsoft.AspNetCore.Authorization;

namespace AutoService.Controllers
{
    [Authorize(Roles = "admin")]

    public class RoleAdminController : Controller
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<ApplicationUser> userManager;
        public RoleAdminController(RoleManager<IdentityRole> roleMgr,
                                    UserManager<ApplicationUser> userMgr)
        {
            roleManager = roleMgr;
            userManager = userMgr;
        }
        public ViewResult Index()
        {
            return View(roleManager.Roles);
        }
        public IActionResult CreateRole() => View();
        [HttpPost]
        public async Task<IActionResult> CreateRole([Required] string name)
        {
            if(ModelState.IsValid)
            {
                IdentityResult result = await roleManager.CreateAsync(new IdentityRole(name));
                if(result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            return View(name);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            if (role!=null)
            {
                IdentityResult result = await roleManager.DeleteAsync(role);
                if(result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            else
            {
                ModelState.AddModelError("", "No Role found");
            }
            return View("Index", roleManager.Roles);
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach(IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
        public async Task<IActionResult> EditRole(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            List<ApplicationUser> members = new List<ApplicationUser>();
            List<ApplicationUser> nonMembers = new List<ApplicationUser>();
            foreach(ApplicationUser user in userManager.Users)
            {
                var list = await userManager.IsInRoleAsync(user, role.Name)
                    ? members : nonMembers;
                list.Add(user);
            }
            return View(new RoleEditModel
            {
                Role=role,
                Members=members,
                NonMembers=nonMembers
            });
        }
        [HttpPost]
        public async Task<IActionResult> EditRole(RoleModificationModel model)
        {
            IdentityResult result;
            if(ModelState.IsValid)
            {
                foreach(string userId in model.IdsToAdd?? new string[] { })
                {
                    ApplicationUser user = await userManager.FindByIdAsync(userId);
                    if(user!=null)
                    {
                        result = await userManager.AddToRoleAsync(user, model.RoleName);
                        if(!result.Succeeded)
                        {
                            AddErrorsFromResult(result);
                        }
                    }
                }
                foreach(string userId in model.IdsToDelete?? new string[] { })
                {
                    ApplicationUser user = await userManager.FindByIdAsync(userId);
                    if(user!=null)
                    {
                        result = await userManager.RemoveFromRoleAsync(user, model.RoleName);
                        if(!result.Succeeded)
                        {
                            AddErrorsFromResult(result);
                        }
                    }
                }
            }
            if(ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return await EditRole(model.RoleId);
            }
        }
    }
}
