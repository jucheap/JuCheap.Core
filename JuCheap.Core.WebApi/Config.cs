using IdentityServer4.Models;
using System.Collections.Generic;
using IdentityServer4;

namespace JuCheap.Core.WebApi
{
    /// <summary>
    /// 配置文件
    /// </summary>
    public class Config
    {
        /// <summary>
        /// IdentityServer地址(IdentityServer配置在本项目，所以此地址就是本项目运行时的根地址)
        /// </summary>
        public const string IdentityUrl = "http://localhost:63230";
        /// <summary>
        /// Api名称
        /// </summary>
        public const string ApiName = "jucheap";
        /// <summary>
        /// Api描述
        /// </summary>
        public const string ApiDescription = "jucheap web api";
        /// <summary>
        /// Api唯一标识(不能重复)
        /// </summary>
        public const string ApiClientId = "api.client";
        /// <summary>
        /// Api密钥
        /// </summary>
        public const string ApiClientSecret = "28365BC74137474DA6986B86836B4468";
        /// <summary>
        /// claim中用来存放用户唯一标识的
        /// </summary>
        public const string UserId = "userid";

        public static IEnumerable<IdentityResource> GetIdentityResourceResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(), //必须要添加，否则报无效的scope错误
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };
        }

        // scopes define the API resources in your system
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource(ApiName, ApiDescription)
            };
        }

        // client want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
	            //密码授权模式的Api配置
	            new Client
	            {
		            ClientId = ApiClientId,
		            AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

		            ClientSecrets =
		            {
			            new Secret(ApiClientSecret.Sha256())
                    },
                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        ApiName
                    }
                }
			};
        }
	}
}