using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoleBasedAccess.Models.DTO;
using RoleBasedAccess.Repositories.Abstract;

namespace RoleBasedAccess.Controllers
{
    public class UserAuthentication : Controller
    {
       
       
            private readonly IUserAuthenticationService _Service;
            public UserAuthentication(IUserAuthenticationService authService)
            {
                this._Service = authService;
            }
            public IActionResult Login()
            {
                return View();
            }
        //registration model
        public IActionResult Registration()
        {
            return View();
        }

            [HttpPost]
            public async Task<IActionResult> Login(LoginModel model)
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var result = await _Service.LoginAsync(model);


                if (result.StatusCode == 1)
                {
                    return RedirectToAction("Display", "Dashboard");
                }
                else
                {
                    TempData["msg"] = result.Message;
                    return RedirectToAction(nameof(Login));
                }
            }


            //logout model 

            [Authorize]
            public async Task<IActionResult> Logout()
            {
                await this._Service.LogoutAsync();
            return RedirectToAction(nameof(Login));

            }


           

            

            [HttpPost]
            public async Task<IActionResult> Registration(RegistrationModel model)
            {
                if (!ModelState.IsValid)
                    return View(model);

                model.Role = "user";
                var result = await this._Service.RegistrationAsync(model);
                TempData["msg"] = result.Message;
                return RedirectToAction(nameof(Registration));
            }


            //only 1 Default Admin , uncomment this and change credential and update migration and db to change existing credential

       //    [AllowAnonymous]
       //    public async Task<IActionResult> Reg()
        //   {
       //        RegistrationModel model = new RegistrationModel
       //        {
       //            Username = "admin",
       //            Name = "Vishal",                   
       //            Password = "Vishal@123",
       //             Email = "admin@gmail.com",
       //        };
        //      model.Role = "admin";
        //       var result = await this._Service.RegistrationAsync(model);
         //      return Ok(result);
        //   }


        }
    }


