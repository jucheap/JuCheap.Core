namespace JuCheap.Core.Models
{
    /// <summary>
    /// 模型验证错误消息模版
    /// </summary>
    public static class Message
    {
        /// <summary>
        /// 必填信息
        /// </summary>
        public const string Required = "{0}不能为空";

        /// <summary>
        /// 最小长度信息
        /// </summary>
        public const string MinLength = "{0}长度不能少于{1}个字符";

        /// <summary>
        /// 最大长度信息
        /// </summary>
        public const string MaxLength = "{0}长度不能超过{1}个字符";

        /// <summary>
        /// 信息已存在
        /// </summary>
        public const string Exists = "{0}已存在";

        /// <summary>
        /// 数字信息
        /// </summary>
        public const string Number = "{0}必须是数字";

        /// <summary>
        /// 两次输入不一致
        /// </summary>
        public const string Compare = "两次输入不一致";

        /// <summary>
        /// 格式错误信息
        /// </summary>
        public const string RuleError = "请输入正确的{0}";

        /// <summary>
        /// 登录帐号格式错误
        /// </summary>
        public const string LoginName = "登录账号必须是字母、数字或者下划线的组合";
    }
}
