// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Collections.Generic;
using JuCheap.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace JuCheap.Core.WebApi
{
    /// <summary>
    /// Extension methods for the IdentityServer builder
    /// </summary>
    public static class IdentityServerBuilderExtensions
    {
        /// <summary>
        /// Adds test users.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="users">The users.</param>
        /// <returns></returns>
        public static IIdentityServerBuilder AddUserService(this IIdentityServerBuilder builder, List<TestUser> users)
        {
            builder.Services.AddScoped<IUserService>();
            builder.Services.AddSingleton(new TestUserStore(users));
            builder.AddProfileService<TestUserProfileService>();
            builder.AddResourceOwnerValidator<TestUserResourceOwnerPasswordValidator>();

            return builder;
        }
    }
}