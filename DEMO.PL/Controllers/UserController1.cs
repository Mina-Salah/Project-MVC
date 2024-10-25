using DEMO.DAL.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DEMO.PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController1 : Controller
    {
        private readonly UserManager<AplicationUser> _userManager;

        public ILogger<UserController1> Logger { get; }

        public UserController1(
            UserManager<AplicationUser> userManager,
            ILogger<UserController1> logger
            )
        {
            _userManager = userManager;
            Logger = logger;
        }

        public async Task<IActionResult> Index(string SearchValue="")
        {
            List<AplicationUser> users;
            if (string.IsNullOrEmpty(SearchValue))
                users = await _userManager.Users.ToListAsync(); 
            else
            users = await _userManager.Users
                    .Where(user => user.Email.Trim().ToLower().Contains(SearchValue.Trim().ToLower())).ToListAsync();
            
              return View(users);   
        }

        public async Task<IActionResult> Details(string id,string viewName = "Details")
        {
            if (id is null)
                return NotFound();  
            var user = await _userManager.FindByIdAsync(id);

            if (user is null)
                return NotFound();

            return View (viewName, user); 
        }

        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            return await Details( id, "Update");  
        }
        [HttpPost]
        public async Task<IActionResult> Update(string id,AplicationUser aplicationUser)
        {
            try
            {
                if (id != aplicationUser.Id)
                    return NotFound();
                if (ModelState.IsValid)
                {

                    var user = await _userManager.FindByIdAsync(id);
                    user.UserName = aplicationUser.UserName;
                    if (user.UserName is null)
                        return NotFound();

                    user.NormalizedUserName = aplicationUser.UserName.ToUpper();
                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                        return RedirectToAction(nameof(Index));

                    foreach (var error in result.Errors)
                    {
                        Logger.LogError(error.Description);
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);


            }
            return View(aplicationUser);  
        }

        public async Task<IActionResult> Delete(string id)
        {

            try
            {


                var user = await _userManager.FindByIdAsync(id);
                if (user is null) 
                    return NotFound();  

                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));

                foreach (var error in result.Errors)
                {
                    Logger.LogError(error.Description);
                    ModelState.AddModelError("", error.Description);
                }
            }

            catch (Exception ex)
            {
                Logger.LogError(ex.Message);

            }
           
               
        return RedirectToAction(nameof(Index));
        }

    }
}
