namespace Native.Csharp.App.Mihayou
{
    /// <summary>
    /// 公告类
    /// </summary>
    public class NoticeRoot
    {
        /// <summary>
        /// 所有公告数据
        /// </summary>
        public Data Data { get; set; }
        /// <summary>
        /// 获取状态
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 未知属性
        /// </summary>
        public int retcode { get; set; }
    }
}
