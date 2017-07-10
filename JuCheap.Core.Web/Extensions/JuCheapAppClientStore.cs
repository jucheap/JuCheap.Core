using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Stores;
using JuCheap.Core.Interfaces;
using IdentityServer4;
using JuCheap.Core.Web.Configuration;

namespace JuCheap.Core.Web.Extensions
{
    /// <summary>
    /// In-memory client store
    /// </summary>
    public class JuCheapAppClientStore : IClientStore
    {
        private readonly IAppService _appService;

        /// <summary>
        /// Initializes a new instance of the <see cref="JuCheapAppClientStore"/> class.
        /// </summary>
        /// <param name="clients">The clients.</param>
        public JuCheapAppClientStore(IAppService appService)
        {
            _appService = appService;
        }

        /// <summary>
        /// Finds a client by id
        /// </summary>
        /// <param name="clientId">The client id</param>
        /// <returns>
        /// The client
        /// </returns>
        public async Task<Client> FindClientByIdAsync(string clientId)
        {
            //此处是fake的client数据，正式环境应该取消掉
            if (Clients.Get().Any(x => x.ClientId == clientId))
            {
                return Clients.Get().FirstOrDefault(x => x.ClientId == clientId);
            }

            //正式环境，保留下面的代码，已确保client是在系统中注册过的
            var app = await _appService.GetByClientId(clientId);            
            var client = new Client
            {
                ClientId = app.ClientId,
                ClientName = app.ClientName,
                ClientUri = app.ClientUri,

                ClientSecrets =
                {
                    new Secret("JuCheapSecret".Sha256())
                },

                AllowedGrantTypes = GrantTypes.Hybrid,
                AllowAccessTokensViaBrowser = false,

                RedirectUris = { $"{app.ClientUri}/signin-oidc" },
                PostLogoutRedirectUris = { $"http://{app.ClientUri}/signout-callback-oidc" },

                AllowOfflineAccess = true,

                RequireConsent = false,//禁用权限信息确认页面，直接跳转到登录之前的页面

                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email
                }
            };
            return client;
        }
    }
}