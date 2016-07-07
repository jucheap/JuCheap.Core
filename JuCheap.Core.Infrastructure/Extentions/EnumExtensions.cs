/*******************************************************************************
* Copyright (C) AspxPet.Com
* 
* Author: dj.wong
* Create Date: 09/04/2015 11:47:14
* Description: Automated building by service@aspxpet.com 
* 
* Revision History:
* Date         Author               Description
*
*********************************************************************************/

using System;
using System.Reflection;


namespace JuCheap.Core.Infrastructure.Extentions
{
    /// <summary>
    ///     枚举扩展方法类
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// 获取描述
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescriptionForEnum(this object value)
        {
            try
            {
                if (value == null) return string.Empty;
                var type = value.GetType();
                var field = type.GetField(Enum.GetName(type, value));

                if (field == null) return value.ToString();

                var des = CustomAttributeData.GetCustomAttributes(type.GetMember(field.Name)[0]);

                return des.AnyOne() && des[0].ConstructorArguments.AnyOne()
                    ? des[0].ConstructorArguments[0].Value.ToString()
                    : value.ToString();
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
    }
}