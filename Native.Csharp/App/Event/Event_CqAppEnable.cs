using Native.Csharp.App.EventArgs;
using Native.Csharp.App.Interface;
using System;
using System.IO;
using System.Text;
using System.Timers;

namespace Native.Csharp.App.Event
{
    /// <summary>
	/// Type=1003 应用被启用, 事件实现
	/// </summary>
    public class Event_CqAppEnable : ICqAppEnable
    {
        //创建定时器对象
        public static System.Timers.Timer timer;
        /// <summary>
		/// 处理 酷Q 的插件启动事件回调
		/// </summary>
		/// <param name="sender">事件的触发对象</param>
		/// <param name="e">事件的附加参数</param>
        public void CqAppEnable(object sender, CqAppEnableEventArgs e)
        {
            // 当应用被启用后，本方法将被调用。
            // 如果酷Q载入时应用已被启用，则在 ICqStartup 接口的实现方法被调用后，本方法也将被调用一次。
            // 如非必要，不建议在这里加载窗口。（可以添加菜单，让用户手动打开窗口）
            //应用启用的时候就获取祈愿信息
            Helper.GetPrincess();
            Helper.GetWitch();
            timer = new System.Timers.Timer();
            //启动定时器
            timer.Enabled = true;
            timer.Interval = 60000;//执行间隔时间,单位为毫秒;此时时间间隔为1分钟 
            //绑定执行方法
            timer.Elapsed += new ElapsedEventHandler(CheckUpdatetimer_Elapsed);
            //设置是否循环执行
            timer.AutoReset = true;

            Common.IsRunning = true;
        }
        /// <summary>
        /// 定时获取最新的祈愿信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void CheckUpdatetimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //官网更新数据可能延迟所以在00：01的时候自动更新
            if (DateTime.Now.ToShortTimeString().ToString() == "00:01" || DateTime.Now.ToShortTimeString().ToString() == "0:01")
            {
                //取未更新前的标题
                string gongzhu = GetOneLine("公主祈愿");
                string Monv = GetOneLine("魔女祈愿") + GetOneLine("使魔祈愿");
                //更新数据
                Helper.GetPrincess();
                Helper.GetWitch();
                //取更新后的数据
                string Monvsss = GetOneLine("魔女祈愿") + GetOneLine("使魔祈愿");
                //对比有没有变化      
                //if (!GetOneLine("公主祈愿").Contains(gongzhu))
                //{
                //    Common.CqApi.SendGroupMessage(136667270, "公主祈愿更新啦");
                //    Common.CqApi.SendGroupMessage(136667270, Helper.SendPrincess("公主祈愿"));
                //}
                //if (!Monvsss.Contains(Monv))
                //{
                //    Common.CqApi.SendGroupMessage(136667270, "魔女祈愿更新啦");
                //    Common.CqApi.SendGroupMessage(136667270, Helper.SendPrincess(Helper.GetNow()));
                //}
                //写日志
                StringBuilder sb = new StringBuilder();
                sb.Append(DateTime.Now.ToString() + "定时更新成功" + "\n");
                string FamiliarPath = Common.CqApi.GetAppDirectory() + "ErrorLog.txt";
                Helper.SaveDate(FamiliarPath, sb, true);
            }
        }
        public static string GetOneLine(string Name)
        {
            try
            {
                string Path = Common.CqApi.GetAppDirectory() + Name + ".txt";
                if (!System.IO.File.Exists(Path))
                {
                    //如果不存在则创建    FileMode.Create:创建文件   
                    FileStream fileStream = new FileStream(Path, FileMode.Create, FileAccess.Write);
                    //创建完成释放资源
                    fileStream.Close();
                    //按行读取所有数据
                    string[] line = File.ReadAllLines(Path, Encoding.UTF8);
                    //取第二行
                    string str = line[1];
                    //返回
                    return str;
                }
                else
                {
                    string[] line = File.ReadAllLines(Path, Encoding.UTF8);
                    string str = line[1];
                    return str;
                }
            }
            catch (Exception)
            {

                return "错误";
            }


        }
    }
}
