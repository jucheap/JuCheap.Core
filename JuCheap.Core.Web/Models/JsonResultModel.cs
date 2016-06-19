namespace JuCheap.Core.Web.Models
{
    /// <summary>
    /// 通用Json结果对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JsonResultModel<T>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public JsonResultModel()
        {
            flag = false;
            msg = string.Empty;
            data = default(T);
        }

        /// <summary>
        /// ctor with params
        /// </summary>
        /// <param name="flagResult">操作结果</param>
        /// <param name="message">消息</param>
        /// <param name="returnData">返回数据</param>
        public JsonResultModel(bool flagResult, string message, T returnData)
        {
            flag = flagResult;
            msg = message;
            data = returnData;
        }

        /// <summary>
        /// 是否操作成功
        /// </summary>
        public bool flag { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public T data { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string msg { get; set; }
    }
}