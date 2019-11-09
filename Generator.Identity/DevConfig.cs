using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace Generator.Identity
{
    public static class DevConfig
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            var discordResource = new IdentityResource("discord", "Discord User Information",
                new List<string> { ClaimTypes.NameIdentifier, "urn:discord:avatar", "urn:discord:discriminator" });
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                discordResource
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            var makise = new ApiResource("makise", "MakiseSharp")
            {
                UserClaims = new List<string>
                {
                    "discord"
                }
            };
            return new List<ApiResource>
            {
                makise
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "pkce_client",
                    ClientName = "Test Client",
                    
                    AllowedGrantTypes = GrantTypes.Code,
                    //ClientSecrets =
                    //{
                    //    new Secret("chuj".Sha256())
                    //},
                    AllowAccessTokensViaBrowser = true,
                    RequireClientSecret = false,
                    EnableLocalLogin = false,
                    RequireConsent = true,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    RequirePkce = true,


                    IdentityProviderRestrictions = { "Google"},
                    PostLogoutRedirectUris = { "https://makise.club" },
                    RedirectUris =           { "https://localhost:44362/external/callback" },
                    //AllowedCorsOrigins =     { "https://localhost:5000" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        "discord",
                        "makise"
                    }
                }
            };
        }
    }
}
