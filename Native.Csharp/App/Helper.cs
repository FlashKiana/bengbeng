using Native.Csharp.App.Mihayou;
using Native.Csharp.Tool.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Native.Csharp.App
{
    /// <summary>
    /// 通用工具类
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// 获取最新的公主祈愿并保存
        /// </summary>
        public static void GetPrincess()
        {
            try
            {
                //创建获取公告连接
                string url = @"https://www.benghuai.com/news/getNotice";
                //创建对象
                HttpWebClient httpWebClient = new HttpWebClient();
                //将获获取的公告转换为新的对象
                NoticeRoot noticeRoot = JsonConvert.DeserializeObject<NoticeRoot>(Encoding.UTF8.GetString(httpWebClient.DownloadData(url)));
                //获取想要的公告id
                string princessid = "0";
                foreach (Gach item in noticeRoot.Data.Gach)
                {
                    //如果祈愿标题包含公主祈愿
                    if (item.title.Contains("公主祈愿"))
                    {
                        //获取公主祈愿的id
                        princessid = item.id;
                        break;
                    }
                }
                //创建祈愿公告的id
                string WishUrl = @"https://www.benghuai.com/news/getNoticeByID?id=" + princessid;
                //将获取的信息转成对象
                QiyuanRoot qiyuanRoot = JsonConvert.DeserializeObject<QiyuanRoot>(Encoding.UTF8.GetString(httpWebClient.DownloadData(WishUrl)));
                //创建获取祈愿时间正则
                string WishTime = @"[0-9月日 :]+~[0-9月日 :]+";
                //获取祈愿时间
                string PrincessTime = Regex.Match(qiyuanRoot.data.text, WishTime).Value;
                //保存祈愿图片
                httpWebClient.DownloadFile(qiyuanRoot.data.banner_src_url, System.Environment.CurrentDirectory + "\\data\\image\\公主祈愿.jpg");
                //创建获取公主装备正则
                string PrincessZB = @"[\u4e00-\u9fa5a-zA-Z·-]+</td>";
                //创建公主装备数组
                List<string> PrincessZBstr = new List<string>();
                //往数组添加装备
                foreach (var item in Regex.Matches(qiyuanRoot.data.text, PrincessZB))
                {
                    PrincessZBstr.Add(item.ToString().Replace("</td>", ""));
                }
                //创建配置保存路径
                string PrincessPath = Common.CqApi.GetAppDirectory() + "公主祈愿.txt";
                //创建保存的内容
                StringBuilder sb = new StringBuilder();
                //添加内容
                sb.Append("【活动标题】" + "\n");
                sb.Append(qiyuanRoot.data.title + "\n");
                sb.Append("【活动时间】" + "\n");
                sb.Append(PrincessTime);
                sb.Append("[CQ:image,file=公主祈愿.jpg]");
                sb.Append("【注目装备】" + "\n");
                for (int i = 0; i < PrincessZBstr.Count; i++)
                {
                    if (i == PrincessZBstr.Count - 1)
                    {
                        sb.Append(PrincessZBstr[i]);
                    }
                    else
                    {
                        sb.Append(PrincessZBstr[i] + "\n");
                    }
                }
                //保存内容
                SaveDate(PrincessPath, sb, false);
            }
            catch (Exception ex)
            {

                StringBuilder sb = new StringBuilder();
                sb.Append(DateTime.Now.ToString() + "获取公主祈愿信息失败:");
                sb.Append(ex.Message);
                //创建保存路径
                string FamiliarPath = Common.CqApi.GetAppDirectory() + "ErrorLog.txt";
                //保存数据
                SaveDate(FamiliarPath, sb, true);
            }


        }
        /// <summary>
        /// 读取保存的祈愿数据
        /// </summary>
        /// <param name="Name">配置名称</param>
        public static string SendPrincess(string Name)
        {
            //数据路径
            string Path = Common.CqApi.GetAppDirectory() + Name + ".txt";
            //读取对象
            StreamReader stream = new StreamReader(Path);
            //将所有内容一次读完
            string msg = stream.ReadToEnd();
            stream.Close();
            //返回读取的内容
            return msg;
        }
        /// <summary>
        /// 获取当前魔女祈愿信息并保存
        /// </summary>
        /// <param name="ia"></param>
        public static void GetWitch()
        {
            try
            {
                //创建获取公告连接
                string url = @"https://www.benghuai.com/news/getNotice";
                //创建对象
                HttpWebClient httpWebClient = new HttpWebClient();
                //将获获取的公告转换为新的对象
                NoticeRoot noticeRoot = JsonConvert.DeserializeObject<NoticeRoot>(Encoding.UTF8.GetString(httpWebClient.DownloadData(url)));
                //获取想要的公告id
                string princessid = "0";
                foreach (Gach item in noticeRoot.Data.Gach)
                {
                    //如果祈愿标题其中的
                    if (item.title.Contains("魔女祈愿") || item.title.Contains("使魔祈愿"))
                    {
                        //获取魔女祈愿的id
                        princessid = item.id;
                        break;
                    }
                }
                //创建祈愿公告的id
                string WishUrl = @"https://www.benghuai.com/news/getNoticeByID?id=" + princessid;
                //将获取的信息转成对象
                QiyuanRoot qiyuanRoot = JsonConvert.DeserializeObject<QiyuanRoot>(Encoding.UTF8.GetString(httpWebClient.DownloadData(WishUrl)));
                //创建获取祈愿时间正则
                string WishTime = @"[0-9月日 :]+~[0-9月日 :]+";
                //获取祈愿时间
                string PrincessTime = Regex.Match(qiyuanRoot.data.text, WishTime).Value;
                //如果当前是使魔祈愿
                if (qiyuanRoot.data.title.Contains("使魔祈愿"))
                {

                    //保存祈愿图片
                    httpWebClient.DownloadFile(qiyuanRoot.data.banner_src_url, System.Environment.CurrentDirectory + "\\data\\image\\使魔祈愿.jpg");
                    //创建获取使魔祈愿信息
                    List<string> Str = new List<string>();
                    foreach (var item in Regex.Matches(qiyuanRoot.data.text, @";[\u4e00-\u9fa5a-zA-Z:：·-]+&"))
                    {
                        Str.Add(item.ToString().Replace(";", "").Replace("&", ""));
                    }
                    //获取集齐(30|50)颗可合成
                    List<string> Strke = new List<string>();
                    foreach (var item in Regex.Matches(qiyuanRoot.data.text, @"集齐(30|50)颗可合成"))
                    {
                        Strke.Add(item.ToString());
                    }
                    //保存信息
                    StringBuilder sb = new StringBuilder();
                    try
                    {
                        sb.Append("【活动标题】" + "\n");
                        sb.Append(qiyuanRoot.data.title + "\n");
                        sb.Append("【活动时间】" + "\n");
                        sb.Append(PrincessTime);
                        sb.Append("[CQ:image,file=使魔祈愿.jpg]");
                        sb.Append("【第一注目素材:】" + "\n");
                        sb.Append(Str[0] + " >>>>" + Strke[0] + " >>>>" + Str[1] + "\n");
                        sb.Append("【第二注目素材:】" + "\n");
                        sb.Append(Str[2] + " >>>>" + Strke[1] + " >>>>" + Str[3] + "\n");
                        sb.Append("【其他可能获得】" + "\n");
                        sb.Append(Str[4] + "\n");
                        sb.Append(Str[5] + "\n");
                        sb.Append(Str[6] + "\n");
                        sb.Append(Str[7] + "\n");
                        sb.Append(Str[8] + "\n");
                        sb.Append(Str[9] + "\n");
                        sb.Append(Str[10] + "\n");
                        sb.Append(Str[11]);

                    }
                    catch (Exception)
                    {
                        sb.Append("保存使魔祈愿信息失败");

                    }
                    //创建保存路径
                    string FamiliarPath = Common.CqApi.GetAppDirectory() + "使魔祈愿.txt";
                    //保存数据
                    SaveDate(FamiliarPath, sb, false);

                }
                else
                {
                    //保存祈愿图片
                    httpWebClient.DownloadFile(qiyuanRoot.data.banner_src_url, System.Environment.CurrentDirectory + "\\data\\image\\魔女祈愿.jpg");
                    //获取魔女祈愿装备
                    List<string> Str = new List<string>();
                    //foreach (var item in Regex.Matches(qiyuanRoot.data.text, @";[\u4e00-\u9fa5a-zA-Z:：·-]{3,}(&|<)"))
                    //{
                    //    Str.Add(item.ToString().Replace(";", "").Replace("&", "").Replace("<", ""));
                    //}
                    foreach (var item in Regex.Matches(qiyuanRoot.data.text, @"&nbsp;[\u4e00-\u9fa5a-zA-Z:：·-]+&nbsp;"))
                    {
                        Str.Add(item.ToString().Replace("&nbsp;", ""));
                    }
                    //写入保存内容
                    StringBuilder sb = new StringBuilder();
                    try
                    {
                        sb.Append("【活动标题】" + "\n");
                        sb.Append(qiyuanRoot.data.title + "\n");
                        sb.Append("【活动时间】" + "\n");
                        sb.Append(PrincessTime);
                        sb.Append("[CQ:image,file=魔女祈愿.jpg]");
                        sb.Append("【注目装备】" + "\n");
                        for (int i = 0; i < Str.Count; i++)
                        {
                            if (i == Str.Count - 1)
                            {
                                sb.Append(Str[i]);
                            }
                            else
                            {
                                sb.Append(Str[i] + "\n");
                            }
                        }
                    }
                    catch (Exception)
                    {

                        sb.Append("写入魔女祈愿信息失败");
                    }
                    //创建保存路径
                    string FamiliarPath = Common.CqApi.GetAppDirectory() + "魔女祈愿.txt";
                    //保存数据
                    SaveDate(FamiliarPath, sb, false);

                }

            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(DateTime.Now.ToString() + "获取魔女祈愿信息失败:");
                sb.Append(ex.Message);
                //创建保存路径
                string FamiliarPath = Common.CqApi.GetAppDirectory() + "ErrorLog.txt";
                //保存数据
                SaveDate(FamiliarPath, sb, true);
            }
        }
        /// <summary>
        /// 保存配置文件
        /// </summary>
        /// <param name="Path">路径</param>
        /// <param name="sb">内容</param>
        /// <param name="tihuan">是否追加写入</param>
        public static void SaveDate(string Path, StringBuilder sb, bool tihuan)
        {
            if (!System.IO.File.Exists(Path))
            {
                //如果不存在则创建    FileMode.Create:创建文件   
                FileStream fileStream = new FileStream(Path, FileMode.Create, FileAccess.Write);
                //创建完成释放资源
                fileStream.Close();
                //设置文件隐藏 但时文件隐藏后 StreamWriter就获取不到了
                //System.IO.File.SetAttributes(Common.CqApi.GetAppDirectory() + "配置.txt", FileAttributes.Hidden);
                //获取整个文件     false 替换   true:追加
                StreamWriter streamWriter = new StreamWriter(Path, tihuan, Encoding.UTF8);
                //写入一行数据
                streamWriter.Write(sb);
                //释放该文件资源
                streamWriter.Close();

            }
            else
            {
                //如果存在则直接读取
                StreamWriter streamWriter = new StreamWriter(Path, tihuan, Encoding.UTF8);
                //写入祈愿信息
                streamWriter.Write(sb);
                //释放文件资源
                streamWriter.Close();

            }

        }

        /// <summary>
        /// 获取当前魔女祈愿类型
        /// </summary>
        /// <returns></returns>
        public static string GetNow()
        {
            //创建获取公告连接
            string url = @"https://www.benghuai.com/news/getNotice";
            //创建对象
            HttpWebClient httpWebClient = new HttpWebClient();
            //将获获取的公告转换为新的对象
            NoticeRoot noticeRoot = JsonConvert.DeserializeObject<NoticeRoot>(Encoding.UTF8.GetString(httpWebClient.DownloadData(url)));
            //获取想要的公告id
            string princessid = "0";
            foreach (Gach item in noticeRoot.Data.Gach)
            {
                //如果祈愿标题其中的
                if (item.title.Contains("魔女祈愿") || item.title.Contains("使魔祈愿"))
                {
                    //获取魔女祈愿的id
                    princessid = item.id;
                    break;
                }
            }
            //创建祈愿公告的id
            string WishUrl = @"https://www.benghuai.com/news/getNoticeByID?id=" + princessid;
            //将获取的信息转成对象
            QiyuanRoot qiyuanRoot = JsonConvert.DeserializeObject<QiyuanRoot>(Encoding.UTF8.GetString(httpWebClient.DownloadData(WishUrl)));

            //如果当前是使魔祈愿
            if (qiyuanRoot.data.title.Contains("使魔祈愿"))
            {
                return "使魔祈愿";
            }
            else
            {
                return "魔女祈愿";
            }
        }

        /// <summary>
        /// 获取全神器祈愿信息
        /// </summary>
        public static bool GetGod()
        {
            try
            {
                //创建获取公告连接
                string url = @"https://www.benghuai.com/news/getNotice";
                //创建对象
                HttpWebClient httpWebClient = new HttpWebClient();
                //将获获取的公告转换为新的对象
                NoticeRoot noticeRoot = JsonConvert.DeserializeObject<NoticeRoot>(Encoding.UTF8.GetString(httpWebClient.DownloadData(url)));
                //获取想要的公告id
                string princessid = "0";
                foreach (Gach item in noticeRoot.Data.Gach)
                {
                    //如果祈愿标题包含全神祈愿
                    if (item.title.Contains("全神器"))
                    {
                        //获取祈愿的id
                        princessid = item.id;
                        break;
                    }
                }
                //创建祈愿公告的id
                string WishUrl = @"https://www.benghuai.com/news/getNoticeByID?id=" + princessid;
                //将获取的信息转成对象
                QiyuanRoot qiyuanRoot = JsonConvert.DeserializeObject<QiyuanRoot>(Encoding.UTF8.GetString(httpWebClient.DownloadData(WishUrl)));
                //创建获取祈愿时间正则
                string WishTime = @"[0-9月日 :]+~[0-9月日 :]+";
                //获取祈愿时间
                string PrincessTime = Regex.Match(qiyuanRoot.data.text, WishTime).Value;
                //判断当前活动日期是否有效
                if (GetTi(PrincessTime)) { return false; }
                //保存祈愿图片
                httpWebClient.DownloadFile(qiyuanRoot.data.banner_src_url, System.Environment.CurrentDirectory + "\\data\\image\\全神祈愿.jpg");
                //创建获取公主装备正则
                string PrincessZB = @"[\u4e00-\u9fa5a-zA-Z·-]+</td>";
                //创建公主装备数组
                List<string> PrincessZBstr = new List<string>();
                //往数组添加装备
                foreach (var item in Regex.Matches(qiyuanRoot.data.text, PrincessZB))
                {
                    PrincessZBstr.Add(item.ToString().Replace("</td>", ""));
                }
                //创建配置保存路径
                string PrincessPath = Common.CqApi.GetAppDirectory() + "全神祈愿.txt";
                //创建保存的内容
                StringBuilder sb = new StringBuilder();
                //添加内容
                sb.Append("【活动标题】" + "\n");
                sb.Append(qiyuanRoot.data.title + "\n");
                sb.Append("【活动时间】" + "\n");
                sb.Append(PrincessTime);
                sb.Append("[CQ:image,file=全神祈愿.jpg]");
                sb.Append("【注目装备】" + "\n");
                for (int i = 0; i < PrincessZBstr.Count; i++)
                {
                    if (i == PrincessZBstr.Count - 1)
                    {
                        sb.Append(PrincessZBstr[i]);
                    }
                    else
                    {
                        sb.Append(PrincessZBstr[i] + "\n");
                    }
                }
                //保存内容
                SaveDate(PrincessPath, sb, false);
                return true;
            }
            catch (Exception ex)
            {

                StringBuilder sb = new StringBuilder();
                sb.Append(DateTime.Now.ToString() + "获取全神祈愿信息失败:");
                sb.Append(ex.Message);
                //创建保存路径
                string FamiliarPath = Common.CqApi.GetAppDirectory() + "ErrorLog.txt";
                //保存数据
                SaveDate(FamiliarPath, sb, true);
                return true;
            }
        }

        /// <summary>
        /// 获取祈愿是否还存在
        /// </summary>
        /// <param name="time">活动时间</param>
        /// <returns>bool</returns>
        public static bool GetTi(string time)
        {
            try
            {
                DateTime QiTime = Convert.ToDateTime(Regex.Match(time, @"~[ 1-9]+月[1-9 ]+日").ToString().Trim().Replace("~", DateTime.Now.Year.ToString() + "年"));
                DateTime NowTime = Convert.ToDateTime(DateTime.Now.ToLongDateString().ToString());

                return NowTime > QiTime ? true : false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 获取魔法少女祈愿信息
        /// </summary>
        /// <returns></returns>
        public static bool GetMagic()
        {
            try
            {
                //创建获取公告连接
                string url = @"https://www.benghuai.com/news/getNotice";
                //创建对象
                HttpWebClient httpWebClient = new HttpWebClient();
                //将获获取的公告转换为新的对象
                NoticeRoot noticeRoot = JsonConvert.DeserializeObject<NoticeRoot>(Encoding.UTF8.GetString(httpWebClient.DownloadData(url)));
                //获取想要的公告id
                string princessid = "0";
                foreach (Gach item in noticeRoot.Data.Gach)
                {
                    //如果祈愿标题包含全神祈愿
                    if (item.title.Contains("魔法少女"))
                    {
                        //获取祈愿的id
                        princessid = item.id;
                        break;
                    }
                }
                //创建祈愿公告的id
                string WishUrl = @"https://www.benghuai.com/news/getNoticeByID?id=" + princessid;
                //将获取的信息转成对象
                QiyuanRoot qiyuanRoot = JsonConvert.DeserializeObject<QiyuanRoot>(Encoding.UTF8.GetString(httpWebClient.DownloadData(WishUrl)));
                //创建获取祈愿时间正则
                string WishTime = @"[0-9月日 :]+~[0-9月日 :]+";
                //获取祈愿时间
                string PrincessTime = Regex.Match(qiyuanRoot.data.text, WishTime).Value;
                //判断当前活动日期是否有效
                if (GetTi(PrincessTime)) { return false; }

                //获取魔法少女祈愿图片
                string str = Regex.Match(qiyuanRoot.data.text, @"http://static-image.benghuai.com/hsod2_webview/images/broadcast_top/bannerv2.{3,25}(jpg|JPG|png)").Value;
                httpWebClient.DownloadFile(str, System.Environment.CurrentDirectory + "\\data\\image\\魔法少女祈愿.jpg");
                //写入魔法少女祈愿信息
                StringBuilder sb = new StringBuilder();
                sb.Append("【活动标题】" + "\n");
                sb.Append(qiyuanRoot.data.title + "\n");
                sb.Append("【活动时间】" + "\n");
                sb.Append(PrincessTime + "\n");
                sb.Append("活动期间，在魔法少女的祈愿中，每天都有不同的瞩目装备获得概率特别提升! 每日概率提升装备如下图：");
                sb.Append("[CQ:image,file=魔法少女祈愿.jpg]");

                //创建配置保存路径
                string PrincessPath = Common.CqApi.GetAppDirectory() + "魔法少女祈愿.txt";
                //保存内容
                SaveDate(PrincessPath, sb, false);
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }

        }
    }
}
