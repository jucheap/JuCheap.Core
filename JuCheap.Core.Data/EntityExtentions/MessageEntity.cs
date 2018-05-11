using System;
using System.Linq;

namespace JuCheap.Core.Data.Entity
{
    public partial class MessageEntity
    {
        /// <summary>
        /// 设置总量
        /// </summary>
        public void SetNumber()
        {
            Total = MessageReceivers.Count();
        }

        /// <summary>
        /// 查阅
        /// </summary>
        public void Read()
        {
            Total -= 1;
            ReadedNumber += 1;
        }
    }

    public partial class MessageReceiverEntity
    {
        public void Read()
        {
            if (IsReaded == false)
            {
                IsReaded = true;
                ReadDate = DateTime.Now;
                Message.Read();
            }
        }
    }
}
