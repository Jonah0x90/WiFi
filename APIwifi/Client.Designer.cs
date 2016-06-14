namespace APIwifi
{
    partial class Client
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Client));
            this.lb_online = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_internet = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btn_colse = new System.Windows.Forms.Button();
            this.txt_pwd = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_ssid = new System.Windows.Forms.TextBox();
            this.btn_start = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.网站ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.cb_netkeeper = new System.Windows.Forms.CheckBox();
            this.tb_min = new System.Windows.Forms.TrackBar();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.cb_shutdown = new System.Windows.Forms.CheckBox();
            this.Timer_Count = new System.Windows.Forms.Timer(this.components);
            this.lb_count = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tb_min)).BeginInit();
            this.SuspendLayout();
            // 
            // lb_online
            // 
            this.lb_online.AutoSize = true;
            this.lb_online.BackColor = System.Drawing.SystemColors.Desktop;
            this.lb_online.ForeColor = System.Drawing.Color.Transparent;
            this.lb_online.Location = new System.Drawing.Point(104, 151);
            this.lb_online.Name = "lb_online";
            this.lb_online.Size = new System.Drawing.Size(11, 12);
            this.lb_online.TabIndex = 36;
            this.lb_online.Text = "0";
            this.lb_online.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Desktop;
            this.label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(63, 151);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 35;
            this.label1.Text = "在线数";
            this.label1.Visible = false;
            // 
            // cmb_internet
            // 
            this.cmb_internet.BackColor = System.Drawing.SystemColors.Window;
            this.cmb_internet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_internet.ForeColor = System.Drawing.Color.Black;
            this.cmb_internet.FormattingEnabled = true;
            this.cmb_internet.Location = new System.Drawing.Point(122, 91);
            this.cmb_internet.Name = "cmb_internet";
            this.cmb_internet.Size = new System.Drawing.Size(100, 20);
            this.cmb_internet.TabIndex = 34;
            this.toolTip1.SetToolTip(this.cmb_internet, "选择拥有访问Internet权限的连接~");
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.Desktop;
            this.label6.ForeColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(63, 94);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 33;
            this.label6.Text = "共享网络";
            // 
            // btn_colse
            // 
            this.btn_colse.BackColor = System.Drawing.Color.Black;
            this.btn_colse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_colse.ForeColor = System.Drawing.Color.Transparent;
            this.btn_colse.Location = new System.Drawing.Point(139, 146);
            this.btn_colse.Name = "btn_colse";
            this.btn_colse.Size = new System.Drawing.Size(83, 23);
            this.btn_colse.TabIndex = 32;
            this.btn_colse.Text = "关闭热点";
            this.btn_colse.UseVisualStyleBackColor = false;
            this.btn_colse.Visible = false;
            this.btn_colse.Click += new System.EventHandler(this.btn_colse_Click);
            // 
            // txt_pwd
            // 
            this.txt_pwd.BackColor = System.Drawing.Color.Black;
            this.txt_pwd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_pwd.ForeColor = System.Drawing.Color.Transparent;
            this.txt_pwd.Location = new System.Drawing.Point(122, 56);
            this.txt_pwd.Name = "txt_pwd";
            this.txt_pwd.Size = new System.Drawing.Size(100, 21);
            this.txt_pwd.TabIndex = 31;
            this.txt_pwd.Text = "输入热点密码...";
            this.txt_pwd.Click += new System.EventHandler(this.txt_pwd_Click);
            this.txt_pwd.Leave += new System.EventHandler(this.txt_pwd_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.Desktop;
            this.label3.ForeColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(63, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 30;
            this.label3.Text = "WiFi密码";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Desktop;
            this.label2.ForeColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(63, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 29;
            this.label2.Text = "WiFi名称";
            // 
            // txt_ssid
            // 
            this.txt_ssid.BackColor = System.Drawing.Color.Black;
            this.txt_ssid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_ssid.ForeColor = System.Drawing.Color.Transparent;
            this.txt_ssid.Location = new System.Drawing.Point(122, 19);
            this.txt_ssid.Name = "txt_ssid";
            this.txt_ssid.Size = new System.Drawing.Size(100, 21);
            this.txt_ssid.TabIndex = 28;
            this.txt_ssid.Text = "输入热点名称...";
            this.txt_ssid.Click += new System.EventHandler(this.txt_ssid_Click);
            this.txt_ssid.Leave += new System.EventHandler(this.txt_ssid_Leave);
            // 
            // btn_start
            // 
            this.btn_start.BackColor = System.Drawing.Color.Black;
            this.btn_start.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_start.ForeColor = System.Drawing.Color.Transparent;
            this.btn_start.Location = new System.Drawing.Point(139, 146);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(83, 23);
            this.btn_start.TabIndex = 27;
            this.btn_start.Text = "开启热点";
            this.btn_start.UseVisualStyleBackColor = false;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.关于ToolStripMenuItem,
            this.网站ToolStripMenuItem,
            this.退出ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 70);
            // 
            // 关于ToolStripMenuItem
            // 
            this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            this.关于ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.关于ToolStripMenuItem.Text = "关于";
            this.关于ToolStripMenuItem.Click += new System.EventHandler(this.关于ToolStripMenuItem_Click);
            // 
            // 网站ToolStripMenuItem
            // 
            this.网站ToolStripMenuItem.Name = "网站ToolStripMenuItem";
            this.网站ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.网站ToolStripMenuItem.Text = "网站";
            this.网站ToolStripMenuItem.Click += new System.EventHandler(this.网站ToolStripMenuItem_Click);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Wi-Fi热点 v3.0";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_DoubleClick);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // cb_netkeeper
            // 
            this.cb_netkeeper.AutoSize = true;
            this.cb_netkeeper.BackColor = System.Drawing.Color.Black;
            this.cb_netkeeper.ForeColor = System.Drawing.Color.Transparent;
            this.cb_netkeeper.Location = new System.Drawing.Point(73, 121);
            this.cb_netkeeper.Name = "cb_netkeeper";
            this.cb_netkeeper.Size = new System.Drawing.Size(138, 16);
            this.cb_netkeeper.TabIndex = 43;
            this.cb_netkeeper.Text = "NetKeeper心跳包屏蔽";
            this.toolTip1.SetToolTip(this.cb_netkeeper, "高校使用NetKeeper拨号端环境的童鞋请务必打勾~");
            this.cb_netkeeper.UseVisualStyleBackColor = false;
            this.cb_netkeeper.CheckedChanged += new System.EventHandler(this.cb_netkeeper_CheckedChanged);
            // 
            // tb_min
            // 
            this.tb_min.BackColor = System.Drawing.Color.Black;
            this.tb_min.Location = new System.Drawing.Point(65, 195);
            this.tb_min.Maximum = 6;
            this.tb_min.Name = "tb_min";
            this.tb_min.Size = new System.Drawing.Size(157, 45);
            this.tb_min.TabIndex = 44;
            this.toolTip1.SetToolTip(this.tb_min, "滑动滑块设置关机时间，单位为分~");
            this.tb_min.Scroll += new System.EventHandler(this.tb_min_Scroll);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.Desktop;
            this.label4.Cursor = System.Windows.Forms.Cursors.Default;
            this.label4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(71, 225);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 45;
            this.label4.Text = "10";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.Desktop;
            this.label5.Cursor = System.Windows.Forms.Cursors.Default;
            this.label5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(92, 225);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 46;
            this.label5.Text = "20";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.SystemColors.Desktop;
            this.label7.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.label7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(114, 225);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 12);
            this.label7.TabIndex = 47;
            this.label7.Text = "30";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.SystemColors.Desktop;
            this.label8.Cursor = System.Windows.Forms.Cursors.Default;
            this.label8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.ForeColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(136, 225);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 12);
            this.label8.TabIndex = 48;
            this.label8.Text = "60";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.SystemColors.Desktop;
            this.label9.Cursor = System.Windows.Forms.Cursors.Default;
            this.label9.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.ForeColor = System.Drawing.Color.Transparent;
            this.label9.Location = new System.Drawing.Point(158, 225);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 12);
            this.label9.TabIndex = 49;
            this.label9.Text = "90";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.SystemColors.Desktop;
            this.label10.Cursor = System.Windows.Forms.Cursors.Default;
            this.label10.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.ForeColor = System.Drawing.Color.Transparent;
            this.label10.Location = new System.Drawing.Point(176, 225);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(23, 12);
            this.label10.TabIndex = 50;
            this.label10.Text = "120";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.SystemColors.Desktop;
            this.label11.Cursor = System.Windows.Forms.Cursors.Default;
            this.label11.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.ForeColor = System.Drawing.Color.Transparent;
            this.label11.Location = new System.Drawing.Point(198, 225);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(23, 12);
            this.label11.TabIndex = 51;
            this.label11.Text = "180";
            // 
            // cb_shutdown
            // 
            this.cb_shutdown.AutoSize = true;
            this.cb_shutdown.BackColor = System.Drawing.Color.Black;
            this.cb_shutdown.ForeColor = System.Drawing.Color.Transparent;
            this.cb_shutdown.Location = new System.Drawing.Point(106, 177);
            this.cb_shutdown.Name = "cb_shutdown";
            this.cb_shutdown.Size = new System.Drawing.Size(72, 16);
            this.cb_shutdown.TabIndex = 52;
            this.cb_shutdown.Text = "定时关机";
            this.toolTip1.SetToolTip(this.cb_shutdown, "是时候躺在床上享受Wi-Fi了~");
            this.cb_shutdown.UseVisualStyleBackColor = false;
            this.cb_shutdown.CheckedChanged += new System.EventHandler(this.cb_shutdown_CheckedChanged);
            // 
            // Timer_Count
            // 
            this.Timer_Count.Interval = 1000;
            this.Timer_Count.Tick += new System.EventHandler(this.Timer_Count_Tick);
            // 
            // lb_count
            // 
            this.lb_count.BackColor = System.Drawing.SystemColors.Desktop;
            this.lb_count.ForeColor = System.Drawing.Color.Transparent;
            this.lb_count.Location = new System.Drawing.Point(54, 244);
            this.lb_count.Name = "lb_count";
            this.lb_count.Size = new System.Drawing.Size(179, 18);
            this.lb_count.TabIndex = 53;
            this.lb_count.Text = "关机时间";
            this.lb_count.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lb_count.Visible = false;
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.lb_count);
            this.Controls.Add(this.cb_shutdown);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tb_min);
            this.Controls.Add(this.cb_netkeeper);
            this.Controls.Add(this.lb_online);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmb_internet);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btn_colse);
            this.Controls.Add(this.txt_pwd);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_ssid);
            this.Controls.Add(this.btn_start);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Client";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Wi-Fi热点 v3.0";
            this.TopMost = true;
            this.Resize += new System.EventHandler(this.Main_Resize);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tb_min)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lb_online;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_internet;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btn_colse;
        private System.Windows.Forms.TextBox txt_pwd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_ssid;
        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 网站ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox cb_netkeeper;
        private System.Windows.Forms.TrackBar tb_min;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox cb_shutdown;
        private System.Windows.Forms.Timer Timer_Count;
        private System.Windows.Forms.Label lb_count;
        private System.Windows.Forms.ToolTip toolTip1;

    }
}

