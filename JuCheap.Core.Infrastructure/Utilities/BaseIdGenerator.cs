using System;

namespace JuCheap.Core.Infrastructure.Utilities
{
    /// <summary>
    /// 订单编号生成器
    /// </summary>
    public class BaseIdGenerator
    {
        private readonly IdGenerator _numberGenerator = new IdGenerator(5);

        /// <summary>
        /// ctor
        /// </summary>
        public BaseIdGenerator()
        {
        }

        /// <summary>
        /// 生成11+seedWidth长度的ID
        /// </summary>
        /// <returns></returns>
        public string GetId()
        {
            return _numberGenerator.GetId(DateTime.Now);
        }

        /// <summary>
        /// Instance
        /// </summary>
        public static readonly BaseIdGenerator Instance = new BaseIdGenerator();
    }
}
