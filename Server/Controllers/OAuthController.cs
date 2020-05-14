using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Token(
            string grant_type,// flow of access_token request
            string code,//confirmation of the authentication
            string redirect_url,
            string client_id)
        {
            //some mechanism for validating the code
            return View();
        }
    }
}
