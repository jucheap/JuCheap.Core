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

using System.Reflection;
using System.Text;

namespace JuCheap.Core.Infrastructure.Extentions
{
    /// <summary>
    ///     通用类型扩展方法类
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// 获取对象的属性和值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetKeyValue<T>(this T obj)
        {
            if (obj == null)
                return string.Empty;

            var sb = new StringBuilder();
            foreach (var prop in obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                var val = prop.GetValue(obj, null);
                var name = prop.Name;

                sb.AppendFormat("{0}={1}\r\n", name, val);
            }
            return sb.ToString();
        }
    }
}