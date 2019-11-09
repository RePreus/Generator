using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Generator.Domain.Entities;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authorization;

namespace Generator.Identity.Controllers
{
    [AllowAnonymous]
    public class ExternalController : Controller
    {
        private readonly IIdentityServerInteractionService interaction;
        private readonly IClientStore clientStore;
        //private readonly dbcontext

        public ExternalController(IIdentityServerInteractionService interaction, IClientStore clientStore)
        {
            this.clientStore = clientStore;
            this.interaction = interaction;
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
                    {"returnUrl", returnUrl},
                    {"scheme", provider},
                }
            };
            //HttpContext.RequestServices.GetService<IAuthenticationService>().ChallengeAsync()
            return Challenge(props, provider);
        }

        [HttpGet]
        public async Task<string> Callback([FromQuery(Name = "code")]string code, [FromQuery(Name = "state")]string state)
        {
            //var authorizationCode = Request.QueryString["code"];
            //var state = this.Request.QueryString["state"];
            // read external identity from the temporary cookie
            //var result = await HttpContext.AuthenticateAsync(IdentityServer4.IdentityServerConstants.ExternalCookieAuthenticationScheme);
            //if (result?.Succeeded != true)
            //{
            //    throw new Exception("External authentication error");
            //}

            return code + state;
            //HttpContext.sign

        }


        private async Task<(User user, string provider, string providerUserId, ICollection<Claim> claims)> FindUserFromExternalProvider(AuthenticateResult result)
        {
            var externalUser = result.Principal;

            var providerUserIdClaim = externalUser.FindFirst(ClaimTypes.NameIdentifier) ??
                                      throw new Exception("Unknown userid");

            var claims = externalUser.Claims.ToList();

            var provider = result.Properties.Items["scheme"];
            var providerUserId = providerUserIdClaim.Value;

            // find external user
            var user = new User(123);//await userRepository.GetByDiscordId(ulong.Parse(providerUserId));

            return (user, provider, providerUserId, claims);
        }
    }
}