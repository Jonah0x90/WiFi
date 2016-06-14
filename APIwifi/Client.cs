using System;
using System.IO;
using System.Xml;
using System.Net;
using System.Data;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Globalization;
using System.ComponentModel;
using System.ServiceProcess;
using System.Security.Principal;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Net.NetworkInformation;
using Control;
using Win32API;
using NETCONLib;


namespace APIwifi
{
    public partial class Client : Form
    {
        private static string dirPath;
        private static IntPtr _ptr;
        private bool wifi;

        public Client()
        {
            InitializeComponent();
        }

        #region AeroDll
        // Aero 效果实现
        [StructLayout(LayoutKind.Sequential)]

        public struct MARGINS
        {
            public int Left;

            public int Right;

            public int Top;

            public int Bottom;
        }

        [DllImport("dwmapi.dll", PreserveSig = false)]

        static extern void DwmExtendFrameIntoClientArea(IntPtr hwnd, ref MARGINS margins);

        [DllImport("dwmapi.dll", PreserveSig = false)]

        static extern bool DwmIsCompositionEnabled();
        #endregion

        #region Dll
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "FindWindowEx")]
        private static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszCalss, string lpszWindow);

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(int hwnd, int wMsg, string wParam, string lParam);

        [DllImport("ntdll.dll")]
        private static extern int ZwSuspendProcess(IntPtr ProcessId);
        [DllImport("ntdll.dll")]
        private static extern int ZwResumeProcess(IntPtr ProcessId);

        [System.Runtime.InteropServices.DllImport("User32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]

        private static extern int GetWindowThreadProcessId(IntPtr hwnd, out int ID);

        [DllImport("kernel32.dll", EntryPoint = "OpenThread")]
        private static extern IntPtr OpenThread(int dwDesiredAccess, bool bInheritHandle, int dwThreadId);

        [DllImport("kernel32.dll", EntryPoint = "SuspendThread")]
        private static extern int SuspendThread(IntPtr hThread);

        [DllImport("kernel32.dll", EntryPoint = "ResumeThread")]
        private static extern int ResumeThread(IntPtr hThread); 
        #endregion

        #region Admin Checkin
        /// <summary>
        /// 判断程序是否是以管理员身份运行。
        /// </summary>
        public static bool IsRunAsAdmin()
        {
            bool result = false;
            try
            {
                WindowsIdentity id = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(id);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch (Exception Error)
            {
                MessageBox.Show(Error.Message);
            }
            return result;
        } 
        #endregion

        #region Exit Tips
        [DllImport("User32.dll", EntryPoint = "SendMessage")] //用于发送信息给窗体

        public static extern int SendMessage(int hwnd, int wMsg, int wParam, int lParam); //调用发送消息的API函数

        protected override void WndProc(ref Message m)    //重写消息泵
        {
            const int WM_CLOSE = 0x010;    //关闭窗体的消息值
            const int WM_DESTROY = 0x002;  //销毁窗体的消息值
            switch (m.Msg)
            {
                case WM_CLOSE:                             //截获关闭窗体的消息
                    if (wifi == true)
                    {
                        DialogResult r = MessageBox.Show("退出将会自动关闭WiFi,是否要真的退出？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (r == DialogResult.Yes)
                        {
                            Win32API.WlanManager.Instance.StopHostedNetwork();
                            resume();
                            SendMessage((int)this.Handle, (int)WM_DESTROY, (int)m.WParam, (int)m.LParam);      //向窗体发送消息
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        resume();
                        Application.Exit();
                    }
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        } 
        #endregion

        #region OS Checkin
        private void systemcheckin()
        {
            OperatingSystem os = Environment.OSVersion;
            switch (os.Platform)
            {
                case PlatformID.Win32NT:
                    switch (os.Version.Major)
                    {
                        case 3:
                            //label1.Text = "Windows   NT   3.51 ";
                            break;
                        case 4:
                            //label1.Text = "Windows   NT   4.0 ";
                            break;
                        case 5:
                            switch (os.Version.Minor)
                            {
                                case 0:
                                    //label1.Text = "Windows   200 ";
                                    Application.Exit();
                                    MessageBox.Show("(*当前系统:Windows 2000)Sorry,为了保证系统安全稳定，Windows7以下系统暂不支持服务。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    break;
                                case 1:
                                    //label1.Text = "Windows   XP ";
                                    Application.Exit();
                                    MessageBox.Show("(*当前系统:Windows XP)Sorry,为了保证系统安全稳定，Windows7以下系统暂不支持服务。。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    break;
                                case 2:
                                    //label1.Text = "Windows   2003 ";
                                    Application.Exit();
                                    MessageBox.Show("(*当前系统:Windows 2003)Sorry,为了保证系统安全稳定，Windows7以下系统暂不支持服务。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    break;
                            }
                            break;
                        case 6:
                            switch (os.Version.Minor)
                            {
                                case 0:
                                    //label1.Text = "Windows  Vista ";
                                    break;
                                case 1:
                                    //label1.Text = "Windows   7 ";
                                    break;
                            }
                            break;
                    }
                    break;
            }
        }     
        #endregion

        #region TxtCheckin
        private bool txtcheckin()
        {
            if (txt_ssid.Text.Trim() == "" || txt_pwd.Text.Length < 8)
            {
                MessageBox.Show("名称不能为空且密码不能少于8位！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (txt_ssid.Text.Trim() == "输入热点名称..." || txt_pwd.Text == "输入热点密码...")
            {
                MessageBox.Show("热点名称与密码不能使用中文\r\n只能使用英文(a~z / A~Z)数字(0~9)", "错误", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (cmb_internet.Text == "")
            {
                MessageBox.Show("请选择拥有访问Internet权限的网络连接！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else
            {
                return true;
            }
        } 
        #endregion

        #region NetkeeperCheckin
        private void netkeepercheckin()
        {
            int temp = 0x001F03FFF;
            IntPtr hwnd = FindWindow(null, "重庆热线校园频道");
            if (hwnd != IntPtr.Zero)
            {
                int calcID;
                int calcTD;
                //获取PID与TID   
                GetWindowThreadProcessId(hwnd, out calcID);
                calcTD = GetWindowThreadProcessId(hwnd, out calcID);
                //MessageBox.Show(calcID.ToString());  //PID
                //MessageBox.Show(calcTD.ToString());  //TID
                _ptr = OpenThread(temp, false, calcTD);
                int result = SuspendThread(_ptr);
                //MessageBox.Show("成功屏蔽心跳包验证。\r\n请不要关闭失去响应的NetKeeper以免屏蔽失效导致被服务器踢下线！ ", "注意！！！", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("无法检测到NetKeeper!");
            }
        } 
        #endregion

        #region ResumeNetkeeper
        private void resume()
        {
            int temp = 0x001F03FFF;
            IntPtr hwnd = FindWindow(null, "重庆热线校园频道");
            if (hwnd != IntPtr.Zero)
            {
                int calcID;
                int calcTD;
                //获取PID与TID   
                GetWindowThreadProcessId(hwnd, out calcID);
                calcTD = GetWindowThreadProcessId(hwnd, out calcID);
                //MessageBox.Show(calcID.ToString());  //PID
                //MessageBox.Show(calcTD.ToString());  //TID
                _ptr = OpenThread(temp, false, calcTD);
                int result = ResumeThread(_ptr);
                //MessageBox.Show("恢复屏蔽心跳包验证。 ", "注意！！！", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("无法检测到NetKeeper!");
            }            
        } 
        #endregion

        #region ICSCheckin
        private void icscheckin()
        {
            ServiceController sc2 = new ServiceController("SharedAccess");  //检查ICS服务项
            try
            {
                if (sc2.Status.Equals(System.ServiceProcess.ServiceControllerStatus.Stopped))
                {
                    sc2.Start();//打开服务
                }
            }
            catch (Exception c)
            {
                MessageBox.Show("启动Internet Connection Sharing (ICS)服务项失败！\r\n请到控制面板--管理工具--服务手动启动Internet Connection Sharing (ICS)服务项\r\nWiFi热点需要Internet Connection Sharing (ICS)服务项的支持", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                Application.Exit();
            }

            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces(); //获取网络列表
            foreach (NetworkInterface adapter in nics)
            {
                if (adapter.OperationalStatus.ToString() == "Up")
                {
                    cmb_internet.Items.Add(adapter.Name);
                }

            }
        } 
        #endregion

        #region InternetShare
        public static void EnableInternetConnectionSharing(string Share)
        {
            try
            {
                string connectionToShare = Share;
                string sharedForConnection = "无线网络连接 2";

                var manager = new NetSharingManager();
                var connections = manager.EnumEveryConnection;

                foreach (INetConnection c in connections)
                {
                    var props = manager.NetConnectionProps[c];
                    var sharingCfg = manager.INetSharingConfigurationForINetConnection[c];
                    if (props.Name == connectionToShare)
                    {
                        sharingCfg.EnableSharing(tagSHARINGCONNECTIONTYPE.ICSSHARINGTYPE_PUBLIC);
                    }
                    else if (props.Name == sharedForConnection)
                    {
                        sharingCfg.EnableSharing(tagSHARINGCONNECTIONTYPE.ICSSHARINGTYPE_PRIVATE);
                    }
                }
            }
            catch (Exception c)
            {
                MessageBox.Show(c.Message);
            }
        } 
        #endregion

        #region AeroUI + Load
        private void Aero()
        {
            MARGINS margins = new MARGINS();

            margins.Right = margins.Left = margins.Top = margins.Bottom = this.Width + this.Height;

            DwmExtendFrameIntoClientArea(this.Handle, ref margins);
        }

        protected override void OnLoad(EventArgs e)
        {
            if (DwmIsCompositionEnabled())
            {
                MARGINS margins = new MARGINS();
                margins.Right = margins.Left = margins.Top = margins.Bottom = this.Width + this.Height;
                DwmExtendFrameIntoClientArea(this.Handle, ref margins);
            }
            base.OnLoad(e);
            systemcheckin();
            bool result = IsRunAsAdmin();
            if (result == false)
            {
                MessageBox.Show("因程序需要调用系统API，请使用管理员身份运行", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Exit();
            }
            Process.EnterDebugMode();
            icscheckin();
            //checkupdata();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            if (DwmIsCompositionEnabled())
            {
                e.Graphics.Clear(Color.Black);
            }
        } 

        #endregion

        #region Minimized
        private void Main_Resize(object sender, EventArgs e)  //最小化到托盘
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                this.ShowInTaskbar = false;
                this.notifyIcon1.Visible = true;
                int tipShowMilliseconds = 1000;               //托盘气泡tips提醒~
                string tipTitle = "温馨提示";
                string tipContent = txt_ssid.Text + "热点后台运行ing~";
                ToolTipIcon tipType = ToolTipIcon.Info;
                notifyIcon1.ShowBalloonTip(tipShowMilliseconds, tipTitle, tipContent, tipType);
            }
        }

        private void notifyIcon1_DoubleClick(object sender, MouseEventArgs e) //从托盘返回
        {
            this.Visible = true;
            this.Show();
            this.ShowInTaskbar = true;
            Aero();
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            this.notifyIcon1.Visible = true;
        }
        #endregion

        #region ClientText
        private void ChangeMessage(string message)
        {
            this.Invoke((MethodInvoker)delegate
            {
                this.Text = message;
            });
        } 
        #endregion

        #region Menu
        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int tipShowMilliseconds = 1000;
            string tipTitle = "关于作者";
            string tipContent = "谭健~90后~男~爱生活~纯治愈系~";
            ToolTipIcon tipType = ToolTipIcon.Info;
            notifyIcon1.ShowBalloonTip(tipShowMilliseconds, tipTitle, tipContent, tipType);
        }

        private void 网站ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, "http://inj.me");
            int tipShowMilliseconds = 1000;
            string tipTitle = "关于网站";
            string tipContent = "欢迎光临兰釉设计工作室~";
            ToolTipIcon tipType = ToolTipIcon.Info;
            notifyIcon1.ShowBalloonTip(tipShowMilliseconds, tipTitle, tipContent, tipType);
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        #endregion

        #region InputTxt
        private void txt_ssid_Click(object sender, EventArgs e)
        {
            txt_ssid.Text = "";
        }

        private void txt_ssid_Leave(object sender, EventArgs e)
        {
            if (txt_ssid.Text == "")
            {
                txt_ssid.Text = "输入热点名称...";
            }
        }

        private void txt_pwd_Click(object sender, EventArgs e)
        {
            txt_pwd.Text = "";
        }

        private void txt_pwd_Leave(object sender, EventArgs e)
        {
            if (txt_pwd.Text == "")
            {
                txt_pwd.Text = "输入热点密码...";
            }
        }
        
        #endregion

        #region AutoUpData
        internal static string GetConfigValue(string path, string appKey)
        {
            XmlDocument xDoc = new XmlDocument();
            XmlNode xNode;
            XmlElement xElem = null;
            try
            {
                xDoc.Load(path);

                xNode = xDoc.SelectSingleNode("//appSettings");

                xElem = (XmlElement)xNode.SelectSingleNode("//add[@key=\"" + appKey + "\"]");

            }
            catch (XmlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            if (xElem != null)
                return xElem.GetAttribute("value");
            else
                return "";
        }

        private static string GetTheLastUpdateTime(string Dir)
        {
            string LastUpdateTime = "";
            string AutoUpdaterFileName = Dir + "AutoUpdater/AutoUpdater.xml";
            try
            {
                WebClient wc = new WebClient();
                Stream sm = wc.OpenRead(AutoUpdaterFileName);
                XmlTextReader xml = new XmlTextReader(sm);
                while (xml.Read())
                {
                    if (xml.Name == "UpdateTime")
                    {
                        LastUpdateTime = xml.GetAttribute("Date");
                        break;
                    }
                }
                xml.Close();
                sm.Close();
            }
            catch (WebException ex)
            {
                MessageBox.Show(ex.Message);
            }
            return LastUpdateTime;
        }

        private void checkupdata()
        {
            dirPath = GetConfigValue("conf.config", "Url");
            string thePreUpdateDate = GetTheLastUpdateTime(dirPath);
            string localUpDate = GetConfigValue("conf.config", "UpDate");
            if (!String.IsNullOrEmpty(thePreUpdateDate) && !String.IsNullOrEmpty(localUpDate))
            {
                if (DateTime.Compare(
                    Convert.ToDateTime(thePreUpdateDate, CultureInfo.InvariantCulture),
                    Convert.ToDateTime(localUpDate, CultureInfo.InvariantCulture)) > 0)
                {
                    MessageBox.Show("发现新版本,点击确定开始更新.", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AutoUpdata Sj = new AutoUpdata();
                    Sj.ShowDialog();
                }
                else
                {
                    //MessageBox.Show("恭喜!当前程序是最新版本.", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        
        #endregion

        #region ShutDown
        private void StartCount(int time)
        {
            Timer_Count.Enabled = true;
            int reckon = 1000 * time;
            Timer_Count.Interval = reckon;
            Timer_Count.Start();
        }

        private void shutdown()
        {
            switch (tb_min.Value)
            {
                case 0:
                    StartCount(600);
                    break;
                case 1:
                    StartCount(1200);
                    break;
                case 2:
                    StartCount(1800);
                    break;
                case 3:
                    StartCount(3600);
                    break;
                case 4:
                    StartCount(5400);
                    break;
                case 5:
                    StartCount(7200);
                    break;
                case 6:
                    StartCount(10800);
                    break;
            }
        }

        private void stopsd()
        {
            Timer_Count.Stop();
            Timer_Count.Enabled = false;
        }

        private void Timer_Count_Tick(object sender, EventArgs e)
        {
            WindowsController.ExitWindows(RestartOptions.PowerOff, true);
        }


        private void tb_min_Scroll(object sender, EventArgs e)
        {
            lb_count.Visible = true;
            switch (tb_min.Value)
            {
                case 0:
                    lb_count.Text = "预计"+ DateTime.Now.AddMinutes(10).ToString() + "关机";
                    break;
                case 1:
                    lb_count.Text = "预计" + DateTime.Now.AddMinutes(20).ToString() + "关机";
                    break;
                case 2:
                    lb_count.Text = "预计" + DateTime.Now.AddMinutes(30).ToString() + "关机";
                    break;
                case 3:
                    lb_count.Text = "预计" + DateTime.Now.AddMinutes(60).ToString() + "关机";
                    break;
                case 4:
                    lb_count.Text = "预计" + DateTime.Now.AddMinutes(90).ToString() + "关机";
                    break;
                case 5:
                    lb_count.Text = "预计" + DateTime.Now.AddMinutes(120).ToString() + "关机";
                    break;
                case 6:
                    lb_count.Text = "预计" + DateTime.Now.AddMinutes(180).ToString() + "关机";
                    break;
            }        
        }

        #endregion

        private void btn_start_Click(object sender, EventArgs e)
        {
            try
            {
                bool checkin = txtcheckin();
                if (checkin == true)
                {
                    Win32API.WlanManager.Instance.WlanManagers();
                    Win32API.WlanManager.Instance.SetConnectionSettings(txt_ssid.Text.Trim(), 128);
                    Win32API.WlanManager.Instance.SetSecondaryKey(txt_pwd.Text.Trim());
                    Win32API.WlanManager.Instance.QueryStatus();
                    Win32API.WlanManager.Instance.StartHostedNetwork();
                    EnableInternetConnectionSharing(cmb_internet.Text.Trim());
                    #region Tips
                    int tipShowMilliseconds = 1000;
                    string tipTitle = "温馨提示";
                    string tipContent = txt_ssid.Text.Trim() + " 运行中。 ";
                    ToolTipIcon tipType = ToolTipIcon.Info;
                    notifyIcon1.ShowBalloonTip(tipShowMilliseconds, tipTitle, tipContent, tipType);
                    txt_ssid.Enabled = false;
                    txt_pwd.Enabled = false;
                    label1.Visible = true;
                    lb_online.Visible = true;
                    wifi = true;
                    timer1.Enabled = true;
                    timer1.Start();
                    btn_start.Visible = false;
                    btn_colse.Visible = true;
                    #endregion
                }
                else
                {
                    return;
                }
            }
            catch (Exception Error)
            {
                MessageBox.Show(Error.Message);
            }

            
        }

        private void btn_colse_Click(object sender, EventArgs e)
        {
            try
            {
                Win32API.WlanManager.Instance.StopHostedNetwork();
                #region Tips
                int tipShowMilliseconds = 1000;
                string tipTitle = "温馨提示";
                string tipContent = txt_ssid.Text.Trim() + " 热点已关闭。 ";
                ToolTipIcon tipType = ToolTipIcon.Info;
                notifyIcon1.ShowBalloonTip(tipShowMilliseconds, tipTitle, tipContent, tipType);
                txt_ssid.Enabled = true;
                txt_pwd.Enabled = true;
                label1.Visible = false;
                lb_online.Visible = false;
                ChangeMessage("Wi-Fi热点 v3.0");
                timer1.Enabled = false;
                timer1.Stop();
                wifi = false;
                btn_colse.Visible = false;
                btn_start.Visible = true;
                #endregion
            }
            catch (Exception Error)
            {
                MessageBox.Show(Error.Message);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Win32API.WlanManager.Instance.QueryStatus();
            string num = Win32API.WlanManager.Instance.ststatus.dwNumberOfPeers.ToString();
            ChangeMessage(txt_ssid.Text + "运行中...在线数: " + num);
            lb_online.Text = num;
        }

        private void cb_netkeeper_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_netkeeper.Checked == true)
            {
                netkeepercheckin();
            }
            else
            {
                resume();
            }

        }

        private void cb_shutdown_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_shutdown.Checked == true)
            {
                shutdown();
            }
            else
            {
                stopsd();
            }
        }
    }
}
