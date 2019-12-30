using System.Collections.Generic;

namespace Native.Csharp.App.Mihayou
{

    /// <summary>
    /// 所有公告数据
    /// </summary>
    public class Data
    {
        /// <summary>
        /// 未知分类
        /// </summary>
        public List<Event> Event { get; set; }
        /// <summary>
        /// 祈愿类公告
        /// </summary>
        public List<Gach> Gach { get; set; }
        /// <summary>
        /// 活动类公告
        /// </summary>
        public List<_New> _New { get; set; }
        /// <summary>
        /// 版本类公告
        /// </summary>
        public List<Version> Version { get; set; }
    }
}
