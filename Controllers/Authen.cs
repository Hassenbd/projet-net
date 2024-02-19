using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using projet_net.Data;
using projet_net.Dto.User;

namespace projet_net.Controllers
{
    [Route("[controller]")]
    public class Authen : Controller
    {
        private readonly IAuth auth;

        public Authen(IAuth _auth)
        {
            auth = _auth;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<int>>Register(userRegister user)
        {
            var rep=await auth.Register(new User{userName=user.userName},user.password);
            return Ok(rep);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<int>>Login(userLogin user)
        {
            return Ok(await auth.Login(user.userName,user.password));
        }
    }
}