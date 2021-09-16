using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using RegistrationSample.DataAccess.Models;
using RegistrationSample.OdlDataAccess;
using RegistrationSample.OldAPI.Models;

namespace RegistrationSample.OldAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetUserById()
        {
            try
            {
                var userId = RequestContext.Principal.Identity.GetUserId();
                var data = new UserData();
                return Ok(data.GetUserById(userId));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IHttpActionResult> RegisterUser([FromBody] NewUserModel user)
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
                        data.RegisterUser(newUser);
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
        public IHttpActionResult UpdateUser([FromBody] UserModel user)
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
                    data.UpdateUser(user);
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

        private IHttpActionResult GetErrorResult(IdentityResult result)
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