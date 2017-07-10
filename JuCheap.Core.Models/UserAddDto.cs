using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using System;

namespace JuCheap.Core.Models
{
    /// <summary>
    /// 用户DTO
    /// </summary>
    public class UserAddDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 登录账号
        /// </summary>
        [Display(Name = "登录账号"), Required(ErrorMessage = "{0}不能为空"), MinLength(4, ErrorMessage = "{0}长度不能少于4个字符"), MaxLength(20, ErrorMessage = "{0}长度不能大于4个字符")]
        [RegularExpression("^[^_][a-zA-Z0-9_]*$",ErrorMessage = "登录账号必须是字母、数字或者下划线的组合")]
        [Remote("VerifyLoginName", "User",ErrorMessage = "登录帐号已经存在")]
        public string LoginName { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        [Display(Name = "真实姓名"), Required(ErrorMessage = "{0}不能为空"), MinLength(2, ErrorMessage = "{0}长度不能少于4个字符"), MaxLength(20, ErrorMessage = "{0}长度不能大于4个字符")]
        public string RealName { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [Display(Name = "邮箱"), Required(ErrorMessage = "{0}不能为空"), MinLength(5, ErrorMessage = "{0}长度不能少于4个字符"), MaxLength(36, ErrorMessage = "{0}长度不能大于4个字符")]
        [RegularExpression(@"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$",ErrorMessage = "请输入正确的邮箱地址")]
        public string Email { get; set; }

        ///<summary>
        /// 密码
        ///</summary>
        [Display(Name = "登录密码")]
        [Required(ErrorMessage = "{0}不能为空"), MinLength(6, ErrorMessage = "{0}长度不能少于4个字符"), MaxLength(12, ErrorMessage = "{0}长度不能大于4个字符")]
        public string Password { get; set; }
    }
}
