// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace DesignPatterns.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource
                {
                    Name = "roles",
                    DisplayName = "Roles",
                    UserClaims = { JwtClaimTypes.Role }
                }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("designpatterns.api", new string[] { JwtClaimTypes.Role, "name", "email" }),
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                // interactive client using code flow + pkce
                new Client
                {
                    ClientId = "designpatterns.angular",
                    ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,

                    RedirectUris = { "https://localhost:5005/authentication/login-callback" },
                    FrontChannelLogoutUri = "https://localhost:5005/authentication/logout-callback",
                    PostLogoutRedirectUris = { "https://localhost:5005/authentication/logged-out" },
                    AllowedCorsOrigins =     { "https://localhost:5005" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "designpatterns.api",
                        "roles"
                    }
                },
            };
    }
}