// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace DesignPatterns.IdentityServer.Common
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

        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
                {
                    new ApiResource("designpatterns", "Main API")
                    {
                        Scopes = { "designpatterns.api" }
                    },
                    // new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
                    // {
                    //     Scopes = { IdentityServerConstants.LocalApi.ScopeName}
                    // }
                };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("designpatterns.api", new string[] { JwtClaimTypes.Role, "name", "email" }),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
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

                    RedirectUris = { "http://localhost:50004/authentication/login-callback" },
                    FrontChannelLogoutUri = "http://localhost:50004/authentication/logout-callback",
                    PostLogoutRedirectUris = { "http://localhost:50004/authentication/logged-out" },
                    AllowedCorsOrigins =     { "http://localhost:50004" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.LocalApi.ScopeName,
                        "designpatterns.api",
                        "roles"
                    }
                },
                new Client
                {
                    ClientId = "webapp",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,

                    // where to redirect to after login
                    RedirectUris = { "https://localhost:50008/signin-oidc" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "https://localhost:50008/signout-callback-oidc" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.LocalApi.ScopeName,
                        "designpatterns.api",
                        "roles"
                    }
                }
            };
    }
}