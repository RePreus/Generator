using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IdentityServer4;

namespace Generator.Identity.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IAuthenticationSchemeProvider schemeProvider;
        public AccountController(IAuthenticationSchemeProvider schemeProvider)
        {
            this.schemeProvider = schemeProvider;
        }

        [HttpGet]
        public async Task<IActionResult> LogIn(/*string returnUrl*/)
        {
            string  returnUrl = "/Home/Chuj";
            
            
            var schemes = await schemeProvider.GetAllSchemesAsync();
            var providers = schemes
                .Where(x => x.DisplayName != null)
                .Select(x => x.Name).ToList();
            if (providers.Count > 1)
            {
                throw new NotSupportedException("Only single identity provider is supported");
            }

            return RedirectToAction("Challenge", "External", new {provider = providers.First(), returnUrl});
        }

    }
}