using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RegistrationSample.DataAccess;

namespace RegistrationSample.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserData _data;

        public UserController(IUserData userData)
        {
            _data = userData;
        }

        [HttpGet]
        public IActionResult GetUserById()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                return Ok(_data.GetUserById(userId));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterUser([FromBody] NewUserModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var applicationUser = new ApplicationUser() { UserName = user.EmailAddress, Email = user.EmailAddress };
                    var userManager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
                    var result = await userManager.CreateAsync(applicationUser, user.Password);

                    if (!result.Succeeded)
                    {
                        return GetErrorResult(result);
                    }

                    var newUser = new UserModel
                    {
                        Id = applicationUser.Id,
                        EmailAddress = user.EmailAddress,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        BirthDate = user.BirthDate,
                    };
                    var data = new UserData();
                    try
                    {
                        _data.RegisterUser(newUser);
                        return Ok();
                    }
                    catch (Exception ex)
                    {
                        await userManager.DeleteAsync(applicationUser);
                        return InternalServerError(ex);
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        public IActionResult UpdateUser([FromBody] UserModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userId = RequestContext.Principal.Identity.GetUserId();
                    if (user.Id != userId)
                    {
                        return BadRequest("Id's does not match");
                    }
                    var data = new UserData();
                    _data.UpdateUser(user);
                    return Ok();
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        private IActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}
