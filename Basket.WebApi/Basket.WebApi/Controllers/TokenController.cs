using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Basket.DAL.Models.Requests;
using Basket.WebApi.Helpers;
using Basket.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Basket.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Token")]
    public class TokenController : Controller
    {
        [HttpPost]
        public IActionResult Create([FromBody]TokenRequest request)
        {
            if (request != null)
                if (IsValidUserAndPasswordCombination(request.Username, request.Password))
                    return Ok(GenerateToken(request.Username));
            return BadRequest();
        }

        private bool IsValidUserAndPasswordCombination(string username, string password)
        {
            return !string.IsNullOrEmpty(username) && username == password;
        }

        private TokenResponse GenerateToken(string username)
        {
            DateTimeOffset dtExpired = new DateTimeOffset(DateTime.Now.AddDays(1));

            var claims = new Claim[]
            {
            new Claim(ClaimTypes.Name, username),
            new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
            new Claim(JwtRegisteredClaimNames.Exp, dtExpired.ToUnixTimeSeconds().ToString()),
            };

            var token = new JwtSecurityToken(
                new JwtHeader(new SigningCredentials(new SymmetricSecurityKey(SecurityHelpers.GetSharedKey()), SecurityAlgorithms.HmacSha256)),
                new JwtPayload(claims));

            TokenResponse response = new TokenResponse();
            response.ExpiredDate = dtExpired;
            response.Token = new JwtSecurityTokenHandler().WriteToken(token);
            return response;
        }
    }
}