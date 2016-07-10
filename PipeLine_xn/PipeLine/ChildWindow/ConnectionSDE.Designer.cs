namespace PipeLine.ChildWindow
{
    partial class ConnectionSDE
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.version = new DevExpress.XtraEditors.TextEdit();
            this.database = new DevExpress.XtraEditors.TextEdit();
            this.instance = new DevExpress.XtraEditors.TextEdit();
            this.server = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.password = new DevExpress.XtraEditors.TextEdit();
            this.user = new DevExpress.XtraEditors.TextEdit();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.Loggin = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.version.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.database.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.instance.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.server.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.password.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.user.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.version);
            this.groupControl1.Controls.Add(this.database);
            this.groupControl1.Controls.Add(this.instance);
            this.groupControl1.Controls.Add(this.server);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Location = new System.Drawing.Point(4, 5);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(211, 181);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "参数设置";
            // 
            // version
            // 
            this.version.EditValue = "sde.DEFAULT";
            this.version.Location = new System.Drawing.Point(85, 130);
            this.version.Name = "version";
            this.version.Size = new System.Drawing.Size(100, 20);
            this.version.TabIndex = 7;
            // 
            // database
            // 
            this.database.EditValue = "sde";
            this.database.Location = new System.Drawing.Point(85, 95);
            this.database.Name = "database";
            this.database.Size = new System.Drawing.Size(100, 20);
            this.database.TabIndex = 6;
            // 
            // instance
            // 
            this.instance.EditValue = "esri_sde";
            this.instance.Location = new System.Drawing.Point(85, 64);
            this.instance.Name = "instance";
            this.instance.Size = new System.Drawing.Size(100, 20);
            this.instance.TabIndex = 5;
            // 
            // server
            // 
            this.server.EditValue = "192.168.72.183";
            this.server.Location = new System.Drawing.Point(85, 35);
            this.server.Name = "server";
            this.server.Size = new System.Drawing.Size(100, 20);
            this.server.TabIndex = 4;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(20, 133);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(36, 14);
            this.labelControl4.TabIndex = 3;
            this.labelControl4.Text = "版本：";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(20, 98);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(48, 14);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "数据库：";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(20, 67);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(36, 14);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "实例：";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(20, 38);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "服务器：";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.password);
            this.groupControl2.Controls.Add(this.user);
            this.groupControl2.Controls.Add(this.simpleButton2);
            this.groupControl2.Controls.Add(this.Loggin);
            this.groupControl2.Controls.Add(this.labelControl6);
            this.groupControl2.Controls.Add(this.labelControl5);
            this.groupControl2.Location = new System.Drawing.Point(4, 192);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(211, 160);
            this.groupControl2.TabIndex = 1;
            this.groupControl2.Text = "用户登录";
            // 
            // password
            // 
            this.password.EditValue = "2615022";
            this.password.Location = new System.Drawing.Point(85, 82);
            this.password.Name = "password";
            this.password.Properties.PasswordChar = '*';
            this.password.Size = new System.Drawing.Size(100, 20);
            this.password.TabIndex = 5;
            // 
            // user
            // 
            this.user.EditValue = "sde";
            this.user.Location = new System.Drawing.Point(85, 43);
            this.user.Name = "user";
            this.user.Size = new System.Drawing.Size(100, 20);
            this.user.TabIndex = 4;
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(110, 123);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(75, 23);
            this.simpleButton2.TabIndex = 3;
            this.simpleButton2.Text = "取消";
            // 
            // Loggin
            // 
            this.Loggin.Location = new System.Drawing.Point(20, 123);
            this.Loggin.Name = "Loggin";
            this.Loggin.Size = new System.Drawing.Size(75, 23);
            this.Loggin.TabIndex = 2;
            this.Loggin.Text = "登录";
            this.Loggin.Click += new System.EventHandler(this.Loggin_Click);
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(20, 88);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(24, 14);
            this.labelControl6.TabIndex = 1;
            this.labelControl6.Text = "密码";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(20, 46);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(36, 14);
            this.labelControl5.TabIndex = 0;
            this.labelControl5.Text = "用户：";
            // 
            // ConnectionSDE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(222, 367);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConnectionSDE";
            this.Text = "连接空间数据库";
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.version.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.database.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.instance.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.server.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.password.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.user.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.TextEdit version;
        private DevExpress.XtraEditors.TextEdit database;
        private DevExpress.XtraEditors.TextEdit instance;
        private DevExpress.XtraEditors.TextEdit server;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.TextEdit password;
        private DevExpress.XtraEditors.TextEdit user;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton Loggin;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl5;
    }
}