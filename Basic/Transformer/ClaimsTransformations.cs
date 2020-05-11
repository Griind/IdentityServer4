using Microsoft.AspNetCore.Authentication;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Basic.Transformer
{
    /// <summary>
    /// Wrapper around claims principal to modify claims.
    /// </summary>
    public class ClaimsTransformations : IClaimsTransformation
    {
        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var hasClaims = principal.Claims.Any(x => x.Type == "Friend");
            if (!hasClaims)
            {
                ((ClaimsIdentity)principal.Identity).AddClaim(new Claim("Friend", "Bad"));
            }
            return Task.FromResult(principal);
        }
    }
}
