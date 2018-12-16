using Flexlive.CQP.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace Flexlive.CQP.CSharpPlugins.Demo
{
    /// <summary>
    /// 酷Q C#版插件Demo
    /// </summary>
    public class MyPlugin : CQAppAbstract
    {
        /// <summary>
        /// 应用初始化，用来初始化应用的基本信息。
        /// </summary>
        public FormSettings settings = new FormSettings();
        
        public override void Initialize()
        {
            // 此方法用来初始化插件名称、版本、作者、描述等信息，
            // 不要在此添加其它初始化代码，插件初始化请写在Startup方法中。
            
            this.Name = "群Roll机器人";
            this.Version = new Version("1.0.0.0");
            this.Author = "Flexlive";
            this.Description = "基于Flexlive版 酷Q C#开方框架的酷Q插件示例。";
        }
        List<string> GroupMemverlist = new List<string>(100);
        List<long> Admingroup = new List<long>(10);

        List<int> ShopList = new List<int>(100);
        List<string> ShopListinfo = new List<string>(100);
        List<long> ShopSellPeople = new List<long>(100);
        List<int> SellInfo = new List<int>(100);
        /// <summary>
        /// 应用启动，完成插件线程、全局变量等自身运行所必须的初始化工作。
        /// </summary>
        public override void Startup()
        {
            //完成插件线程、全局变量等自身运行所必须的初始化工作。
            Admingroup.Add(1773805744);
            Admingroup.Add(921627738);
        }
        string rolllist="";
        /// <summary>
        /// 打开设置窗口。
        /// </summary>
        public override void OpenSettingForm()
        {
            // 打开设置窗口的相关代码。
            FormSettings frm = new FormSettings();
            frm.ShowDialog();
        }
        string AddgroupMsg = "新来的大佬们小老弟们加一下steam组https://steamcommunity.com/groups/669034/" +
                "\n外加使用!join指令加入抽奖列表哦" +
                "\n群内饰品交易推荐群主中介，在本群非群主交易被骗本群概不负责" +
                "\n抽奖时间为12月20日";
        /// <summary>
        /// Type=21 私聊消息。
        /// </summary>
        /// <param name="subType">子类型，11/来自好友 1/来自在线状态 2/来自群 3/来自讨论组。</param>
        /// <param name="sendTime">发送时间(时间戳)。</param>
        /// <param name="fromQQ">来源QQ。</param>
        /// <param name="msg">消息内容。</param>
        /// <param name="font">字体。</param>
        /// 
        public override void PrivateMessage(int subType, int sendTime, long fromQQ, string msg, int font)
        {
            string shopname, shopuse, shopsay, shoptiezhi1, shoptiezhi2, shoptiezhi3,shoptiezhi4,image;
                shopname = shopuse = shopsay = shoptiezhi1 = shoptiezhi2 = shoptiezhi3 = shoptiezhi4 =image= " ";
            int ShopPay=0;
            var command1 = msg.Split('!');
            if (command1.Length <= 1)
            {

            }
            else
            {
                var command2 = command1[1].Split(' ');
                switch (command2[0])
                {
                    case "sell":
                        if (this.SellPeopleCheck(fromQQ))
                        {
                            if (command2.Length!=10)
                            {
                                CQ.SendPrivateMessage(fromQQ,"缺少信息");
                            }
                            else
                            {
                                for (int i = 1; i <= command2.Length-1; i++)
                                {
                                    switch (i)
                                    {
                                        case 1:shopname = command2[i];
                                            break;
                                        case 2:shopuse = command2[i];
                                            break;
                                        case 3:shopsay = command2[i];
                                            break;
                                        case 4:shoptiezhi1 = command2[i];
                                            break;
                                        case 5:shoptiezhi2 = command2[i];
                                            break;
                                        case 6:shoptiezhi3 = command2[i];
                                            break;
                                        case 7:shoptiezhi4 = command2[i];
                                            break;
                                        case 8:
                                            try
                                            {
                                              int pay =   Convert.ToInt32(command2[i]);
                                                ShopPay = pay;
                                            }
                                            catch 
                                            {
                                                CQ.SendPrivateMessage(fromQQ,"价格设置错误");                                                
                                            }                                           
                                            break;
                                        case 9:
                                            image = command2[i].Remove(0,17).Replace(']',' ').Trim();
                                            break;
                                    }
                                }
                               string num =  (ShopListNumber() + 1).ToString();
                                FileStream fs1a = new FileStream("ShopList\\"+num +".txt", FileMode.Create, FileAccess.Write);//创建写入文件 
                                StreamWriter swa = new StreamWriter(fs1a);
                                swa.Write("商品名称:"+shopname+"\n" +
                                    "商品磨损度:"+shopuse+"\n" +
                                    "商品说明:"+shopsay+"\n" +
                                    "商品贴纸:"+shoptiezhi1+" "+shoptiezhi2+" "+shoptiezhi3+" "+shoptiezhi4 + " "+"\n" +
                                    "商品价格:"+ShopPay.ToString()+"\n" +
                                    "图片:"+image);//开始写入值
                                swa.Close();
                                fs1a.Close();
                                
                            }
                        }
                        
                        break;
                }
            }






















               
        }

        /// <summary>
        /// Type=2 群消息。
        /// </summary>
        /// <param name="subType">子类型，目前固定为1。</param>
        /// <param name="sendTime">发送时间(时间戳)。</param>
        /// <param name="fromGroup">来源群号。</param>
        /// <param name="fromQQ">来源QQ。</param>
        /// <param name="fromAnonymous">来源匿名者。</param>
        /// <param name="msg">消息内容。</param>
        /// <param name="font">字体。</param>
        /// 
        public int ShopListNumber()
        {
            int i = 0;
            DirectoryInfo folder = new DirectoryInfo("ShopList");

            foreach (FileInfo file in folder.GetFiles("*.txt"))
            {
                i++;
            }
            return i;
        }
        public bool SellPeopleCheck(long QQnumber)
        {
            bool Cansell = false;
            DirectoryInfo folder = new DirectoryInfo("SellPeople");
            
            foreach (FileInfo file in folder.GetFiles("*.txt"))
            {
                string Filename = file.Name.Replace('.',' ').Replace('t', ' ').Replace('x', ' ').Trim();
               
                if (QQnumber.ToString() == Filename)
                {
                    Cansell = true;
                    break;
                }
                else
                {
                    Cansell = false;
                }
            }
            return Cansell;
        }
        public bool admincheck(long QQnumber)
        {
            bool use = false;
            foreach (var item in Admingroup)
            {
                if (item == QQnumber)
                {
                    use = true;
                    break;
                }
                else
                {
                    use = false;
                }
            }
            return use;
        }
        public string txtread()
        {
            rolllist = " ";
            StreamReader sr = new StreamReader("roll.txt", Encoding.UTF8);
            String line;
            while ((line = sr.ReadLine()) != null)
            {
                rolllist += line;
            }

            sr.Close();
            return rolllist;
        }
        public  bool rollcheck(string  fromQQs)
        {bool canjoin = true;
            StreamReader sr = new StreamReader("roll.txt", Encoding.UTF8);
            String line;
            while ((line = sr.ReadLine()) != null)
            {
                rolllist += line;
            }
            sr.Close();
            var qqnumber = rolllist.Split(',');
            foreach (var item in qqnumber)
            {
                if (fromQQs != item)
                {
                    canjoin = true;
                }
                else
                {
                    canjoin = false;
                    break;
                }
            }
            return canjoin;
        }
        public override void GroupMessage(int subType, int sendTime, long fromGroup, long fromQQ, string fromAnonymous, string msg, int font)
        {
            // 处理群消息。
            //  var groupMember = CQ.GetGroupMemberInfo(fromGroup, fromQQ);
            //  var GroupList = CQE.GetGroupMemberList(fromGroup);
            if (fromGroup == 641713800)
            {
                var command1 = msg.Split('!');
                if (command1.Length <= 1)
                {

                }
                else
                {
                    var command2 = command1[1].Split(' ');
                    switch (command2[0])
                    {
                        case "reload":
                            if (admincheck(fromQQ))
                            {
                                FileStream fs1a = new FileStream("roll.txt", FileMode.Create, FileAccess.Write);//创建写入文件 
                                StreamWriter swa = new StreamWriter(fs1a);
                                swa.Write("");//开始写入值
                                swa.Close();
                                fs1a.Close();
                                CQ.SendGroupMessage(fromGroup,"抽奖名单文件已经重置!");
                            }
                            break;
                        case "AddgroupmsgSet":
                            if (admincheck(fromQQ))
                            {
                                try
                                {
                                    AddgroupMsg = command2[1];
                                }
                                catch 
                                {
                                    CQ.SendGroupMessage(fromGroup,"指令缺少参数，或者语法错误."); 
                                }
                              
                                CQ.SendGroupMessage(fromGroup,"加群公告已经设置为:"+AddgroupMsg);
                            }
                            else
                            {
                                CQ.SendGroupMessage(fromGroup,"你没有权限使用该命令.");
                            }
                                break;

                        case "addroll":
                            if (admincheck(fromQQ))
                            {
                                try
                                {
                                    string QQ = command2[1];
                                    QQ = QQ.Replace('[', '/').Replace(',', '/').Replace('C', '/').Replace('Q', '/').Replace('q', '/').Replace(']', '/').Replace(':', '/').Replace('a', '/').Replace('t', '/').Replace('=', '/');
                                    var GetQQ = QQ.Split('/');
                                    string lasta = this.txtread();
                                    if (!File.Exists("roll.txt"))
                                    {
                                        FileStream fs1 = new FileStream("roll.txt", FileMode.Create, FileAccess.Write);//创建写入文件 
                                        StreamWriter sw = new StreamWriter(fs1);
                                        sw.Write(lasta + GetQQ[10] + ",");//开始写入值
                                        sw.Close();
                                        fs1.Close();
                                    }
                                    else
                                    {
                                        FileStream fs = new FileStream("roll.txt", FileMode.Open, FileAccess.Write);
                                        StreamWriter srr = new StreamWriter(fs);
                                        srr.Write(lasta + GetQQ[10] + ",");//开始写入值
                                                                           //开始写入值
                                        srr.Close();
                                        fs.Close();
                                    }
                                    CQ.SendGroupMessage(fromGroup, "管理员已经把" + CQ.CQCode_At(Convert.ToInt64(GetQQ[10])) + "加入了抽奖名单");
                                }
                                catch 
                                {
                                    CQ.SendGroupMessage(fromGroup, "发生机器人崩溃错误，已经恢复.");

                                }

                            }
                            else
                            {
                                CQ.SendGroupMessage(fromGroup,"你没有权限。");
                            }
                            break;
                        case "addadmin":
                            bool canuse = this.admincheck(fromQQ);
                            if (canuse)
                            {
                                
                               string QQ = command2[1];
                                QQ = QQ.Replace('[', '/').Replace(',', '/').Replace('C', '/').Replace('Q', '/').Replace('q', '/').Replace(']', '/').Replace(':', '/').Replace('a', '/').Replace('t', '/').Replace('=', '/');
                                var GetQQ = QQ.Split('/');
                                if (command2.Length >= 2)
                                {
                                    try
                                    {
                                        Admingroup.Add(Convert.ToInt64(GetQQ[10]));
                                        CQ.SendGroupMessage(fromGroup, GetQQ[10] + "\n已经被设置为管理员");
                                    }
                                    catch
                                    {
                                        CQ.SendGroupMessage(fromGroup,"发生机器人崩溃错误，已经恢复."); 
                                    }
                                    
                                }
                                else
                                {
                                    CQ.SendGroupMessage(fromGroup, "缺少参数");
                                }
                            }
                            else
                            {
                                CQ.SendGroupMessage(fromGroup,"你没有权限");
                            }
                            break;
                        case "adminlist":
                            string output = "";
                            foreach (var item in Admingroup)
                            {
                                output += item + "\n";
                            }
                            CQ.SendGroupMessage(fromGroup,"管理员为\n"+output);
                            break;
                        case "random":
                            if (admincheck(fromQQ))
                            {
                              
                                        string rolllist = this.txtread();
                                        var roll = rolllist.Split(',');
                                            Random rollrandom = new Random();
                                            int rollnext = rollrandom.Next(1, roll.Length);
                                            CQ.SendGroupMessage(fromGroup, "恭喜中奖!" + CQ.CQCode_At(Convert.ToInt64(roll[rollnext - 1])));

                                
                                 
                                
                                
                            }
                            else
                            {
                                CQ.SendGroupMessage(fromGroup,"你没有权限");
                            }
                            
                            break;
                        case "rolllist":
                            if (admincheck(fromQQ))
                            {
                                int i=0;
                                string rolla = this.txtread();
                                var num = rolla.Split(',');
                                foreach (var item in num)
                                {
                                    i++;
                                }
                                CQ.SendGroupMessage(fromGroup, rolla+"\n一共有["+i+"]人加入了抽奖");
                            }
                            else
                            {
                                CQ.SendGroupMessage(fromGroup,"你没有权限");
                            }
                            
                            break;
                        case "join":
                            bool canjoin = false;
                            string fromQQs;
                            fromQQs = fromQQ.ToString();
                            if (!File.Exists("roll.txt"))
                            {
                                File.Create("roll.txt");
                            }
                          canjoin =  this.rollcheck(fromQQs);
                           string last = this.txtread();

                            if (canjoin == true)
                            {
                                if (!File.Exists("roll.txt"))
                                {
                                    FileStream fs1 = new FileStream("roll.txt", FileMode.Create, FileAccess.Write);//创建写入文件 
                                    StreamWriter sw = new StreamWriter(fs1);
                                    sw.Write(last +  fromQQ + ",");//开始写入值
                                    sw.Close();
                                    fs1.Close();
                                }
                                else
                                {
                                    FileStream fs = new FileStream("roll.txt", FileMode.Open, FileAccess.Write);
                                    StreamWriter srr = new StreamWriter(fs);
                                     srr.Write(last + fromQQ + ",");//开始写入值
                                                                              //开始写入值
                                    srr.Close();
                                    fs.Close();
                                }
                                CQ.SendGroupMessage(fromGroup, CQ.CQCode_At(fromQQ) +"恭喜你加入抽奖成功");
                            }
                            else
                            {
                                CQ.SendGroupMessage(fromGroup, "你已经加入了抽奖，你不能加入了");
                            }

                            break;
                        default:                          
                            break;
                        case "help":
                            CQ.SendGroupMessage(fromGroup, "帮助菜单\n !addadmin @<群成员> " +
                               "\n!adminlist 管理员列表" +
                               "\n!random 开始抽奖" +
                               "\n!join 加入抽奖" +
                               "\n!addroll @<群成员> 把群成员加入到抽奖名单 <奖励名额>" +
                               "\n!rolllist 抽奖人员名单" +
                               "\n!reload 重置抽奖名单" +
                               "\n!AddgroupmsgSet <公告信息> 设置加群公告信息");
                            break;
                        case "交易系统":
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Type=4 讨论组消息。
        /// </summary>
        /// <param name="subType">子类型，目前固定为1。</param>
        /// <param name="sendTime">发送时间(时间戳)。</param>
        /// <param name="fromDiscuss">来源讨论组。</param>
        /// <param name="fromQQ">来源QQ。</param>
        /// <param name="msg">消息内容。</param>
        /// <param name="font">字体。</param>
        public override void DiscussMessage(int subType, int sendTime, long fromDiscuss, long fromQQ, string msg, int font)
        {
            // 处理讨论组消息。
            //CQ.SendDiscussMessage(fromDiscuss, String.Format("[{0}]{1}你发的讨论组消息是：{2}", CQ.ProxyType, CQ.CQCode_At(fromQQ), msg));
        }

        /// <summary>
        /// Type=11 群文件上传事件。
        /// </summary>
        /// <param name="subType">子类型，目前固定为1。</param>
        /// <param name="sendTime">发送时间(时间戳)。</param>
        /// <param name="fromGroup">来源群号。</param>
        /// <param name="fromQQ">来源QQ。</param>
        /// <param name="file">上传文件信息。</param>
        public override void GroupUpload(int subType, int sendTime, long fromGroup, long fromQQ, string file)
        {
            // 处理群文件上传事件。
           // CQ.SendGroupMessage(fromGroup, String.Format("[{0}]{1}你上传了一个文件：{2}", CQ.ProxyType, CQ.CQCode_At(fromQQ), file));
        }

        /// <summary>
        /// Type=101 群事件-管理员变动。
        /// </summary>
        /// <param name="subType">子类型，1/被取消管理员 2/被设置管理员。</param>
        /// <param name="sendTime">发送时间(时间戳)。</param>
        /// <param name="fromGroup">来源群号。</param>
        /// <param name="beingOperateQQ">被操作QQ。</param>
        public override void GroupAdmin(int subType, int sendTime, long fromGroup, long beingOperateQQ)
        {
            // 处理群事件-管理员变动。
            //CQ.SendGroupMessage(fromGroup, String.Format("[{0}]{2}({1})被{3}管理员权限。", CQ.ProxyType, beingOperateQQ, CQE.GetQQName(beingOperateQQ), subType == 1 ? "取消了" : "设置为"));
        }

        /// <summary>
        /// Type=102 群事件-群成员减少。
        /// </summary>
        /// <param name="subType">子类型，1/群员离开 2/群员被踢 3/自己(即登录号)被踢。</param>
        /// <param name="sendTime">发送时间(时间戳)。</param>
        /// <param name="fromGroup">来源群。</param>
        /// <param name="fromQQ">来源QQ。</param>
        /// <param name="beingOperateQQ">被操作QQ。</param>
        public override void GroupMemberDecrease(int subType, int sendTime, long fromGroup, long fromQQ, long beingOperateQQ)
        {
            if (fromGroup == 528972847)
            {
                CQ.SendGroupMessage(fromGroup, beingOperateQQ + "精神失常，退群了!");
            }
        }

        /// <summary>
        /// Type=103 群事件-群成员增加。
        /// </summary>
        /// <param name="subType">子类型，1/管理员已同意 2/管理员邀请。</param>
        /// <param name="sendTime">发送时间(时间戳)。</param>
        /// <param name="fromGroup">来源群。</param>
        /// <param name="fromQQ">来源QQ。</param>
        /// <param name="beingOperateQQ">被操作QQ。</param>
        public override void GroupMemberIncrease(int subType, int sendTime, long fromGroup, long fromQQ, long beingOperateQQ)
        {
            // 处理群事件-群成员增加。
            if (fromGroup == 528972847)
            {
                CQ.SendGroupMessage(fromGroup, AddgroupMsg);
            }
        }

        /// <summary>
        /// Type=201 好友事件-好友已添加。
        /// </summary>
        /// <param name="subType">子类型，目前固定为1。</param>
        /// <param name="sendTime">发送时间(时间戳)。</param>
        /// <param name="fromQQ">来源QQ。</param>
        public override void FriendAdded(int subType, int sendTime, long fromQQ)
        {
            // 处理好友事件-好友已添加。
          //  CQ.SendPrivateMessage(fromQQ, String.Format("[{0}]你好，我的朋友！", CQ.ProxyType));
        }

        /// <summary>
        /// Type=301 请求-好友添加。
        /// </summary>
        /// <param name="subType">子类型，目前固定为1。</param>
        /// <param name="sendTime">发送时间(时间戳)。</param>
        /// <param name="fromQQ">来源QQ。</param>
        /// <param name="msg">附言。</param>
        /// <param name="responseFlag">反馈标识(处理请求用)。</param>
        public override void RequestAddFriend(int subType, int sendTime, long fromQQ, string msg, string responseFlag)
        {
            // 处理请求-好友添加。
          //  CQ.SetFriendAddRequest(responseFlag, CQReactType.Allow, "新来的朋友");
        }

        /// <summary>
        /// Type=302 请求-群添加。
        /// </summary>
        /// <param name="subType">子类型，目前固定为1。</param>
        /// <param name="sendTime">发送时间(时间戳)。</param>
        /// <param name="fromGroup">来源群号。</param>
        /// <param name="fromQQ">来源QQ。</param>
        /// <param name="msg">附言。</param>
        /// <param name="responseFlag">反馈标识(处理请求用)。</param>
        public override void RequestAddGroup(int subType, int sendTime, long fromGroup, long fromQQ, string msg, string responseFlag)
        {
            CQ.SetGroupAddRequest(responseFlag, CQRequestType.GroupAdd, CQReactType.Allow);
        }
    }
}
