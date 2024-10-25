using DEMO.DAL.Entity;
using DEMO.PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DEMO.PL.Controllers
{
    [Authorize(Roles ="Admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<AplicationUser> _userManager;
        private readonly ILogger _logger;

        public UserManager<ApplicationRole> _UserManager { get; }

        public RolesController(
            RoleManager<ApplicationRole> roleManager,
            UserManager<AplicationUser> userManager,
            ILogger<RolesController> logger)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _logger = logger;
        }


        public async Task<IActionResult> Index()
        {
            var Role = await _roleManager.Roles.ToListAsync();
            return View(Role);
        }

        public IActionResult Create()
        {
            return View(new ApplicationRole());
        }


        [HttpPost]
        public async Task<IActionResult> Create(ApplicationRole role)
        {
            if (ModelState.IsValid)
            {
                var res = await _roleManager.CreateAsync(role);
                if (res.Succeeded)
                    return RedirectToAction(nameof(Index));

                foreach (var error in res.Errors)
                {
                    _logger.LogError(error.Description);
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(role);
        }

        public async Task<IActionResult> Details(string id, string viewName = "Details")
        {
            if (id is null)
                return NotFound();
            var user = await _roleManager.FindByIdAsync(id);

            if (user is null)
                return NotFound();

            return View(viewName, user);
        }

        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            return await Details(id, "Update");
        }
        [HttpPost]
        public async Task<IActionResult> Update(string id, ApplicationRole applicationRole)
        {
            try
            {
                if (id != applicationRole.Id)
                    return NotFound();
                if (ModelState.IsValid)
                {

                    var role = await _roleManager.FindByIdAsync(id);
                    role.Name = applicationRole.Name;
                    if (role.Name is null)
                        return NotFound();

                    role.NormalizedName = applicationRole.Name.ToUpper();
                    var result = await _roleManager.UpdateAsync(role);

                    if (result.Succeeded)
                        return RedirectToAction(nameof(Index));

                    foreach (var error in result.Errors)
                    {
                        _logger.LogError(error.Description);
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);


            }
            return View(applicationRole);
        }

        public async Task<IActionResult> Delete(string id)
        {

            try
            {
                var user = await _roleManager.FindByIdAsync(id);
                if (user is null)
                    return NotFound();

                var result = await _roleManager.DeleteAsync(user);

                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));

                foreach (var error in result.Errors)
                {
                    _logger.LogError(error.Description);
                    ModelState.AddModelError("", error.Description);
                }
            }

            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

            }


            return RedirectToAction(nameof(Index));
        }





        [HttpGet]
        public async Task<IActionResult> AddOrRemoveUser(string roleId)
        {

            var role = await _roleManager.FindByIdAsync(roleId);
            if (role is null)
                return NotFound();
            
            ViewBag.RoleId = roleId;    
            var usersinRole = new List<UserInRoleModel>();

            var users = await _userManager.Users.ToListAsync();


            foreach (var uesr in users)
            {

                var userinRole = new UserInRoleModel()
                {
                    UserId = uesr.Id,
                    UserName = uesr.UserName,


                };
                if (await _userManager.IsInRoleAsync(uesr, role.Name))
                {
                    userinRole.IsSelected = true;
                }
                else
                {
                    userinRole.IsSelected = false;
                }
                usersinRole.Add(userinRole);

            }
            return View(usersinRole);
        }

     
        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUser(string roleId, List<UserInRoleModel> users)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role is null)
                return NotFound();

            if (ModelState.IsValid)
            {
                foreach (var user in users)
                {
                    var appUser = await _userManager.FindByIdAsync(user.UserId);
                    if (appUser != null)
                    {
                        var isInRole = await _userManager.IsInRoleAsync(appUser, role.Name);
                        if (user.IsSelected && !isInRole)
                            await _userManager.AddToRoleAsync(appUser, role.Name);
                        else if (!user.IsSelected && isInRole)
                            await _userManager.RemoveFromRoleAsync(appUser, role.Name);
                    }
                }
                return RedirectToAction(nameof(Update), new { id = roleId });
            }

            return View(users);
        }
    }
}
