using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;

namespace Basic.AuthorizationRequirements
{
    public class CustomRequiredClaim : IAuthorizationRequirement
    {
        public CustomRequiredClaim(string claimType)
        {
            ClaimType = claimType;
        }

        public string ClaimType { get; }
    }
    public class CustomRequiredClaimHandler : AuthorizationHandler<CustomRequiredClaim>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            CustomRequiredClaim requirement)
        {
            var hasClaim = context.User.Claims.Any(x => x.Type == requirement.ClaimType);
            if (hasClaim)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
    public static class AuthorizationPolicyBuilderExtensions
    {
        public static AuthorizationPolicyBuilder RequireCustomClaim(
            this AuthorizationPolicyBuilder builder,
            string claimType)
        {
            builder.AddRequirements(new CustomRequiredClaim(claimType));
            return builder;
        }
    }
}
