using ApiAuth1.Models;
using ApiAuth1.Repositories;
using ApiAuth1.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAuth1.Controllers
{
    [ApiController]
    [Route("v1")]
    public class LoginController : ControllerBase 
    {
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> AuthenticateAsync([FromBody] User model)
        {
            var user = UserRepository.Get(model.Username, model.Password);
            if (user == null)
            {
                return NotFound(new { message = "Usuário ou senha inválidos"});
            }

            var token = TokenService.GenerateToken(user);

            user.Password = "";

            return new
            {
                user = user,
                token = token
            };
        }
    }
}
