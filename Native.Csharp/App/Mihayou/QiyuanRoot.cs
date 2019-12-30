namespace Native.Csharp.App.Mihayou
{
    public class QiyuanRoot
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public string retcode { get; set; }
        /// <summary>
        /// 获取状态
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 祈愿数据
        /// </summary>
        public QiYuanDate data { get; set; }

    }
}
