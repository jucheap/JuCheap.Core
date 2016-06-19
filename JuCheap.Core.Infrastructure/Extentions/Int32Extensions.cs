using System;

namespace JuCheap.Core.Infrastructure.Extentions
{
    /// <summary>
    /// 扩展Int32功能
    /// </summary>
    public static class Int32Extensions
    {
        /// <summary>
        /// 向上整除
        /// 1.当num能被divideBy整除时,结果即为num/divideBy;
        /// 2.当num不能被divideBy整除时,结果为num/divideBy + 1;
        /// </summary>
        /// <param name="num">被除数,大于或者等于0</param>
        /// <param name="divideBy">除数,大于0</param>
        /// <returns>向上整除结果</returns>
        public static int CeilingDivide(this int num, int divideBy)
        {
            if (num < 0) throw new ArgumentException("num");
            if (divideBy <= 0) throw new ArgumentException("divideBy");

            return (num + divideBy - 1) / divideBy;
        }
    }
}
