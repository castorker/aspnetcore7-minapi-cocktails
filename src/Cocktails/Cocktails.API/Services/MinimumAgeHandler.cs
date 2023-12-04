using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Cocktails.API.Services
{
    public class MinimumAgeHandler : AuthorizationHandler<MinimumAgeRequirement>
    {
        private readonly IConfiguration _configuration;

        public MinimumAgeHandler(IConfiguration configuration)
        {
            _configuration = configuration 
                ?? throw new ArgumentNullException(nameof(configuration));
        }
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
        {
            var dateOfBirthClaim = context.User.FindFirst(
                c => c.Type == ClaimTypes.DateOfBirth 
                && c.Issuer == _configuration["Authentication:Schemes:Bearer:ValidIssuer"]);

            if (dateOfBirthClaim is null)
            {
                return Task.CompletedTask;
            }

            var dateOfBirth = Convert.ToDateTime(dateOfBirthClaim.Value);
            int calculatedAge = DateTime.Today.Year - dateOfBirth.Year;
            if (dateOfBirth > DateTime.Today.AddYears(-calculatedAge))
            {
                calculatedAge--;
            }

            if (calculatedAge >= requirement.MinimumAge)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
