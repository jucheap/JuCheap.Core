/*******************************************************************************
* Copyright (C) JuCheap.Com
* 
* Author: dj.wong
* Create Date: 09/04/2015 11:47:14
* Description: Automated building by service@JuCheap.com 
* 
* Revision History:
* Date         Author               Description
*
*********************************************************************************/


using System;

namespace JuCheap.Core.Data.Entity
{
    /// <summary>
    /// 用户登录日志实体
    /// </summary>
    public class LoginLogEntity : BaseEntity
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 登录账号
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 登录IP地址
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// 登录结果信息
        /// </summary>
        public string Mac { get; set; }
    }
}
