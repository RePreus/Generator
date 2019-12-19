using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Generator.Domain.Entities;
using Generator.Identity.Persistence;
using IdentityServer4.Events;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Generator.Identity.Controllers
{
    [AllowAnonymous]
    public class ExternalController : Controller
    {
        private readonly IIdentityServerInteractionService interaction;
        private readonly IEventService events;
        private readonly UserContext context;

        // private readonly dbcontext
        public ExternalController(IIdentityServerInteractionService interaction, IEventService events, UserContext context)
        {
            this.interaction = interaction;
            this.events = events;
            this.context = context;
        }

        public IActionResult Challenge(string provider, string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl))
            {
                returnUrl = "~/";
            }

            // validate returnUrl - either it is a valid OIDC URL or back to a local page
            if (Url.IsLocalUrl(returnUrl) == false && interaction.IsValidReturnUrl(returnUrl) == false)
            {
                // user might have clicked on a malicious link - should be logged
                throw new Exception("Invalid return URL");
            }

            // start challenge and roundtrip the return URL and scheme
            var props = new AuthenticationProperties
            {
                RedirectUri = Url.Action(nameof(Callback)),
                Items =
                {
                    { "returnUrl", returnUrl },
                    { "scheme", provider },
                },
            };
            return Challenge(props, provider);
        }

        /// <summary>
        /// Post processing of external authentication.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Callback()
        {
            // read external identity from the temporary cookie
            var result = await HttpContext.AuthenticateAsync(IdentityServer4.IdentityServerConstants.ExternalCookieAuthenticationScheme);
            if (result?.Succeeded != true)
            {
                throw new Exception("External authentication error");
            }

            // lookup our user and external provider info
            var (user, provider, providerUserId, email, profile, claims) = await FindUserFromExternalProvider(result);
            if (user == null)
            {
                user = await CreateUser(provider, providerUserId, email, profile);
            }

            var username = claims.First(c => c.Type == ClaimTypes.Name).Value;

            // issue authentication cookie for user
            await events.RaiseAsync(new UserLoginSuccessEvent(provider, providerUserId, user.Id.ToString(), username));
            await HttpContext.SignInAsync(user.Id.ToString(), username, provider, claims.ToArray());

            // delete temporary cookie used during external authentication
            await HttpContext.SignOutAsync(IdentityServer4.IdentityServerConstants.ExternalCookieAuthenticationScheme);

            // retrieve return URL
            var returnUrl = result.Properties.Items["returnUrl"] ?? "~/";

            return Redirect(returnUrl);
        }

        private async Task<User> CreateUser(string provider, string providerUserId, string email, string profile)
        {
            if (provider != "Google")
            {
                throw new NotSupportedException("Providers other than Google are not supported.");
            }

            var user = new User(providerUserId, email, profile);
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            return user;
        }

        private async Task<(User user, string provider, string providerUserId, string email, string profile, ICollection<Claim> claims)> FindUserFromExternalProvider(AuthenticateResult result)
        {
            var externalUser = result.Principal;

            var providerUserIdClaim = externalUser.FindFirst(ClaimTypes.NameIdentifier) ??
                              throw new Exception("Unknown userid");

            var profile = externalUser.FindFirst("urn:google:profile")?.Value ?? string.Empty;
            var email = externalUser.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty;
            var claims = externalUser.Claims.ToList();

            var provider = result.Properties.Items["scheme"];
            var providerUserId = providerUserIdClaim.Value;

            // find external user
            var user = await context.Users.SingleOrDefaultAsync(o => o.GoogleId == providerUserId);

            return (user, provider, providerUserId, email, profile, claims);
        }
    }
}