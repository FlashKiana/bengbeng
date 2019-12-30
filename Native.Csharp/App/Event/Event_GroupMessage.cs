using Native.Csharp.App.EventArgs;
using Native.Csharp.App.Interface;
using System;

namespace Native.Csharp.App.Event
{
    class Event_GroupMessage : IReceiveGroupMessage
    {
        /// <summary>
        /// 处理收到的群消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ReceiveGroupMessage(object sender, CqGroupMessageEventArgs e)
        {
            try
            {
                switch (e.Message)
                {
                    case "公主祈愿":
                        Common.CqApi.SendGroupMessage(e.FromGroup, Helper.SendPrincess("公主祈愿"));
                        break;
                    case "魔女祈愿":
                        Common.CqApi.SendGroupMessage(e.FromGroup, Helper.SendPrincess(Helper.GetNow()));
                        break;
                    case "全神祈愿":
                        if (Helper.GetGod())
                        {
                            Common.CqApi.SendGroupMessage(e.FromGroup, Helper.SendPrincess("全神祈愿"));
                        }
                        else
                        {
                            Common.CqApi.SendGroupMessage(e.FromGroup, "还没有全神祈愿哦");
                        }
                        break;
                    case "魔法少女祈愿":
                        if (Helper.GetMagic())
                        {
                            Common.CqApi.SendGroupMessage(e.FromGroup, Helper.SendPrincess("魔法少女祈愿"));
                        }
                        else
                        {
                            Common.CqApi.SendGroupMessage(e.FromGroup, "还没有魔法少女祈愿哦");
                        }
                        break;
                    case "更新祈愿信息":
                        Helper.GetPrincess();
                        Helper.GetWitch();
                        Common.CqApi.SendGroupMessage(e.FromGroup, "更新祈愿信息完成");
                        break;
                }
            }
            catch (Exception)
            {
                Common.CqApi.SendGroupMessage(e.FromGroup, "Error");
                e.Handler = true;
            }

        }
    }
}
