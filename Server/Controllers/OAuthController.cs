﻿using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Server.Controllers
{
    public class OAuthController : Controller
    {
        [HttpGet]
        public IActionResult Authorize(
            string response_type, // authorization flow type
            string client_id, // client id
            string redirect_uri,
            string scope, // what information i wang => email,tel...
            string state) // random string generated to confirm that we are going back to the same client
        {
            var query = new QueryBuilder();
            query.Add("redirectUri", redirect_uri);
            query.Add("state", state);
            return View(model: query.ToString());
        }
        [HttpPost]
        public IActionResult Authorize(
            string userName,
            string redirectUri,
            string state)
        {

            const string code = "SomeCode";
            var query = new QueryBuilder();
            query.Add("code", code);
            query.Add("state", state);
            return Redirect($"{redirectUri}{query.ToString()}");
        }
        public async Task<IActionResult> Token(
            string grant_type,// flow of access_token request
            string code,//confirmation of the authentication
            string redirect_uri,
            string client_id)
        {

            var claims = new[]
           {
                new Claim(JwtRegisteredClaimNames.Sub, "some_id"),
                new Claim("customClaim", "CustClaim")
            };
            var bytes = Encoding.UTF8.GetBytes(Constants.Secret);
            var key = new SymmetricSecurityKey(bytes);
            var algorithm = SecurityAlgorithms.HmacSha256Signature;

            var signingCredentials = new SigningCredentials(key, algorithm);

            var token = new JwtSecurityToken(
                Constants.Issuer,
                Constants.Audience,
                claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddDays(1),
                signingCredentials
                );
            var access_token = new JwtSecurityTokenHandler().WriteToken(token);
            var responseObject = new
            {
                access_token,
                token_type = "Bearer",
                raw_claim = "oauthClaim"
            };
            var serializedObj = JsonConvert.SerializeObject(responseObject);
            var responseBytes = Encoding.UTF8.GetBytes(serializedObj);
            await Response.Body.WriteAsync(responseBytes, 0, responseBytes.Length);

            return Redirect(redirect_uri);
        }
    }
}
