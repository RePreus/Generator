using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer4;
using IdentityServer4.Models;

namespace Generator.Identity
{
    public static class DevConfig
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            var googleResource = new IdentityResource("google", "Google User Information",
                new List<string> { ClaimTypes.NameIdentifier, ClaimTypes.Name, ClaimTypes.Email, "urn:google:profile" });
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                googleResource,
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource("wyro","Wyro API"),
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "dashboard",
                    ClientName = "Wyro dashboard",
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256()),
                    },
                    AllowedGrantTypes = GrantTypes.Code,
                    EnableLocalLogin = false,
                    RequireConsent = false,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    RequirePkce = true,


                    IdentityProviderRestrictions = { "Google" },
                    PostLogoutRedirectUris = { "https://localhost:44362 " },
                    RedirectUris = { "https://localhost:44362/dashboard/callback" },
                    AllowedCorsOrigins =     { "https://localhost:44362" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        "google",
                        "wyro",
                    },
                },
            };
        }
    }
}
