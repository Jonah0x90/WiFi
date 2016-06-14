using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Xml;
using System.Globalization;
using System.Windows.Forms;

namespace APIwifi
{
    public partial class AutoUpdata : Form
    {
        public AutoUpdata()
        {
            InitializeComponent();
        }

        private WebClient downWebClient = new WebClient();
        private static string dirPath;
        private static long size;//所有文件大小 
        private static int count;//文件总数 
        private static string[] fileNames;
        private static int num;//已更新文件数 
        private static long upsize;//已更新文件大小 
        private static string fileName;//当前文件名 
        private static long filesize;//当前文件大小 
        private void ComCirUpdate_Load(object sender, EventArgs e)
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
                    //MeBox("发现新版本,点击确定开始更新.");
                    UpdaterStart();
                }
                else
                {
                    //MeBox("恭喜!当前程序是最新版本.");
                    this.Close();
                }
            }
            else
            {
                UpdaterClose();
            }
            //UpdaterStart(); 
        }

        /// <summary> 
        /// 开始更新 
        /// </summary> 
        private void UpdaterStart()
        {
            float tempf;
            //委托下载数据时事件 
            this.downWebClient.DownloadProgressChanged += delegate(object wcsender, DownloadProgressChangedEventArgs ex)
            {
                this.label2.Text = String.Format(
                    CultureInfo.InvariantCulture,
                    "正在下载:{0}  [ {1}/{2} ]",
                    fileName,
                    ConvertSize(ex.BytesReceived),
                    ConvertSize(ex.TotalBytesToReceive));

                filesize = ex.TotalBytesToReceive;
                tempf = ((float)(upsize + ex.BytesReceived) / size);
                this.progressBar1.Value = Convert.ToInt32(tempf * 100);
                this.progressBar2.Value = ex.ProgressPercentage;
            };
            //委托下载完成时事件 
            this.downWebClient.DownloadFileCompleted += delegate(object wcsender, AsyncCompletedEventArgs ex)
            {
                if (ex.Error != null)
                {
                    MeBox(ex.Error.Message);
                }
                else
                {
                    if (File.Exists(Application.StartupPath + "\\" + fileName))
                    {
                        File.Delete(Application.StartupPath + "\\" + fileName);
                    }
                    File.Move(Application.StartupPath + "\\AutoUpdater\\" + fileName, Application.StartupPath + "\\" + fileName);
                    upsize += filesize;
                    if (fileNames.Length > num)
                    {
                        DownloadFile(num);
                    }
                    else
                    {
                        SetConfigValue("conf.config", "UpDate", GetTheLastUpdateTime(dirPath));
                        UpdaterClose();
                    }
                }
            };

            size = GetUpdateSize(dirPath + "UpdateSize.ashx");
            if (size == 0)
                UpdaterClose();
            num = 0;
            upsize = 0;
            UpdateList();
            if (fileNames != null)
                DownloadFile(0);
        }

        /// <summary> 
        /// 获取更新文件大小统计 
        /// </summary> 
        /// <param name="filePath">更新文件数据XML</param> 
        /// <returns>返回值</returns> 
        private static long GetUpdateSize(string filePath)
        {
            long len;
            len = 0;
            try
            {
                WebClient wc = new WebClient();
                Stream sm = wc.OpenRead(filePath);
                XmlTextReader xr = new XmlTextReader(sm);
                while (xr.Read())
                {
                    if (xr.Name == "UpdateSize")
                    {
                        len = Convert.ToInt64(xr.GetAttribute("Size"), CultureInfo.InvariantCulture);
                        break;
                    }
                }
                xr.Close();
                sm.Close();
            }
            catch (WebException ex)
            {
                MeBox(ex.Message);
            }
            return len;
        }

        /// <summary> 
        /// 获取文件列表并下载 
        /// </summary> 
        private static void UpdateList()
        {
            string xmlPath = dirPath + "AutoUpdater/AutoUpdater.xml";
            WebClient wc = new WebClient();
            DataSet ds = new DataSet();
            ds.Locale = CultureInfo.InvariantCulture;

            try
            {
                Stream sm = wc.OpenRead(xmlPath);
                ds.ReadXml(sm);
                DataTable dt = ds.Tables["UpdateFileList"];
                StringBuilder sb = new StringBuilder();
                count = dt.Rows.Count;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        sb.Append(dt.Rows[i]["UpdateFile"].ToString());
                    }
                    else
                    {
                        sb.Append("," + dt.Rows[i]["UpdateFile"].ToString());
                    }
                }
                fileNames = sb.ToString().Split(',');
                sm.Close();
            }
            catch (WebException ex)
            {
                MeBox(ex.Message);
            }
        }

        /// <summary> 
        /// 下载文件 
        /// </summary> 
        /// <param name="arry">下载序号</param> 
        private void DownloadFile(int arry)
        {
            try
            {
                num++;
                fileName = fileNames[arry];
                this.label1.Text = String.Format(
                    CultureInfo.InvariantCulture,
                    "更新进度 {0}/{1}  [ {2} ]",
                    num,
                    count,
                    ConvertSize(size));

                this.progressBar2.Value = 0;
                this.downWebClient.DownloadFileAsync(
                    new Uri(dirPath + "AutoUpdater/" + fileName),
                    Application.StartupPath + "\\AutoUpdater\\" + fileName);
            }
            catch (WebException ex)
            {
                MeBox(ex.Message);
            }
        }

        /// <summary> 
        /// 转换字节大小 
        /// </summary> 
        /// <param name="byteSize">输入字节数</param> 
        /// <returns>返回值</returns> 
        private static string ConvertSize(long byteSize)
        {
            string str = "";
            float tempf = (float)byteSize;
            if (tempf / 1024 > 1)
            {
                if ((tempf / 1024) / 1024 > 1)
                {
                    str = ((tempf / 1024) / 1024).ToString("##0.00", CultureInfo.InvariantCulture) + "MB";
                }
                else
                {
                    str = (tempf / 1024).ToString("##0.00", CultureInfo.InvariantCulture) + "KB";
                }
            }
            else
            {
                str = tempf.ToString(CultureInfo.InvariantCulture) + "B";
            }
            return str;
        }

        /// <summary> 
        /// 弹出提示框 
        /// </summary> 
        /// <param name="txt">输入提示信息</param> 
        private static void MeBox(string txt)
        {
            MessageBox.Show(
                txt,
                "提示信息",
                MessageBoxButtons.OK,
                MessageBoxIcon.Asterisk,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
        }

        /// <summary> 
        /// 更新成功关闭程序 
        /// </summary> 
        private static void UpdaterClose()
        {
            try
            {
                System.Diagnostics.Process.Start(Application.StartupPath + "\\Wi-Fi热点.exe");
            }
            catch (Win32Exception ex)
            {
                MeBox(ex.Message);
            }
            Application.Exit();
        }

        /// <summary> 
        /// 读取.exe.config的值 
        /// </summary> 
        /// <param name="path">.exe.config文件的路径</param> 
        /// <param name="appKey">"key"的值</param> 
        /// <returns>返回"value"的值</returns> 
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
                MeBox(ex.Message);
            }
            if (xElem != null)
                return xElem.GetAttribute("value");
            else
                return "";
        }

        /// <summary> 
        /// 设置.exe.config的值 
        /// </summary> 
        /// <param name="path">.exe.config文件的路径</param> 
        /// <param name="appKey">"key"的值</param> 
        /// <param name="appValue">"value"的值</param> 
        internal static void SetConfigValue(string path, string appKey, string appValue)
        {
            XmlDocument xDoc = new XmlDocument();
            try
            {
                xDoc.Load(path);

                XmlNode xNode;
                XmlElement xElem1;
                XmlElement xElem2;

                xNode = xDoc.SelectSingleNode("//appSettings");

                xElem1 = (XmlElement)xNode.SelectSingleNode("//add[@key=\"" + appKey + "\"]");
                if (xElem1 != null) xElem1.SetAttribute("value", appValue);
                else
                {
                    xElem2 = xDoc.CreateElement("add");
                    xElem2.SetAttribute("key", appKey);
                    xElem2.SetAttribute("value", appValue);
                    xNode.AppendChild(xElem2);
                }
                xDoc.Save(Application.StartupPath + "\\" + path);
            }
            catch (XmlException ex)
            {
                MeBox(ex.Message);
            }
        }

        /// <summary> 
        /// 判断软件的更新日期 
        /// </summary> 
        /// <param name="Dir">服务器地址</param> 
        /// <returns>返回日期</returns> 
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
                MeBox(ex.Message);
            }
            return LastUpdateTime;
        }
    }
}

