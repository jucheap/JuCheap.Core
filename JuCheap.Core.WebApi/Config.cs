// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4.Models;
using System.Collections.Generic;

namespace JuCheap.Core.WebApi
{
    public class Config
    {
        // scopes define the API resources in your system
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("jucheap", "jucheap web api")
            };
        }

        // client want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
	            // resource owner password grant client
	            new Client
	            {
		            ClientId = "api.client",
		            AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

		            ClientSecrets =
		            {
			            new Secret("28365BC74137474DA6986B86836B4468".Sha256())

                    },
                    AllowedScopes = { "jucheap","openid","email","profile","offline_access" }
                }
			};
        }
	}
}