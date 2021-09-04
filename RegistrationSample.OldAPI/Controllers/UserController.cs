﻿using System.Collections.Generic;
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
        public List<UserModel> GetById()
        {
            var userId = RequestContext.Principal.Identity.GetUserId();
            UserData data = new UserData();

            return data.GetUserById(userId);
        }
    }
}