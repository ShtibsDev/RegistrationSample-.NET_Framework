using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using RegistrationSample.DataAccess.Models;
using RegistrationSample.OdlDataAccess;

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
                return Ok(data.GetUserById(userId).First());
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
    }
}