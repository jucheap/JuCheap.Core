using System;
using System.ComponentModel.DataAnnotations;

namespace JuCheap.Core.Data.Entity
{
    public class AppEntity : BaseEntity
    {
        /// <summary>
        /// App应用的唯一身份识别ID
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// 应用名称
        /// </summary>
        [Required(ErrorMessage = "请输入网站名称"), MinLength(1), MaxLength(20)]
        public string ClientName { get; set; }


        /// <summary>
        /// URL
        /// </summary>
        [Required(ErrorMessage = "请输入网站地址"), MinLength(1), MaxLength(256)]
        [RegularExpression(@"http(s)?://([\w-]+\.)+[\w-]+(/[\w-.\?%&=]*)?",ErrorMessage = "请输入正确的网址")]
        public string ClientUri { get; set; }
        
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserId { get; set; }
    }
}