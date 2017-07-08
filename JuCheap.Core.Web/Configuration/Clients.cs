// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace JuCheap.Core.Web.Configuration
{
    public class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new List<Client>
            {
                ///////////////////////////////////////////
                // MVC Hybrid Flow Samples
                //////////////////////////////////////////
                new Client
                {
                    ClientId = "JuCheap-SSO-Demo",
                    ClientName = "JuCheap-SSO-Demo",
                    ClientUri = "http://localhost:63919",

                    ClientSecrets = 
                    {
                        new Secret("JuCheapSecret".Sha256())
                    },
                    
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    AllowAccessTokensViaBrowser = false,

                    RedirectUris = { "http://localhost:63919/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:63919/signout-callback-oidc" },

                    AllowOfflineAccess = true,

                    RequireConsent = false,//禁用权限信息确认页面，直接跳转到登录之前的页面

                    AllowedScopes = 
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email
                    }
                }
            };
        }
    }
}