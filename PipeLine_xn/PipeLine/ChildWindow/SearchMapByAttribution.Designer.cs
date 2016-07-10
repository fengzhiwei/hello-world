namespace PipeLine.ChildWindow
{
    partial class SearchMapByAttribution
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchMapByAttribution));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.queryConditoncb_dev = new DevExpress.XtraEditors.ComboBoxEdit();
            this.queryFieldcb_dev = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layerNamecb_dev = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.querybt_dev = new DevExpress.XtraEditors.SimpleButton();
            this.cancelbt_dev = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.queryConditoncb_dev.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.queryFieldcb_dev.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layerNamecb_dev.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.queryConditoncb_dev);
            this.groupControl1.Controls.Add(this.queryFieldcb_dev);
            this.groupControl1.Controls.Add(this.layerNamecb_dev);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Location = new System.Drawing.Point(0, -3);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(350, 336);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "参数：";
            // 
            // queryConditoncb_dev
            // 
            this.queryConditoncb_dev.Location = new System.Drawing.Point(96, 253);
            this.queryConditoncb_dev.Name = "queryConditoncb_dev";
            this.queryConditoncb_dev.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.queryConditoncb_dev.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.queryConditoncb_dev.Size = new System.Drawing.Size(203, 20);
            this.queryConditoncb_dev.TabIndex = 5;
            // 
            // queryFieldcb_dev
            // 
            this.queryFieldcb_dev.Location = new System.Drawing.Point(96, 157);
            this.queryFieldcb_dev.Name = "queryFieldcb_dev";
            this.queryFieldcb_dev.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.queryFieldcb_dev.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.queryFieldcb_dev.Size = new System.Drawing.Size(203, 20);
            this.queryFieldcb_dev.TabIndex = 4;
            this.queryFieldcb_dev.SelectedIndexChanged += new System.EventHandler(this.queryFieldcb_dev_SelectedIndexChanged);
            // 
            // layerNamecb_dev
            // 
            this.layerNamecb_dev.Location = new System.Drawing.Point(96, 61);
            this.layerNamecb_dev.Name = "layerNamecb_dev";
            this.layerNamecb_dev.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.layerNamecb_dev.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.layerNamecb_dev.Size = new System.Drawing.Size(203, 20);
            this.layerNamecb_dev.TabIndex = 3;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(21, 256);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 14);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "查找条件：";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(21, 160);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 14);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "字段名称：";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(21, 64);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "选择图层：";
            // 
            // querybt_dev
            // 
            this.querybt_dev.Location = new System.Drawing.Point(33, 375);
            this.querybt_dev.Name = "querybt_dev";
            this.querybt_dev.Size = new System.Drawing.Size(75, 23);
            this.querybt_dev.TabIndex = 1;
            this.querybt_dev.Text = "查找";
            this.querybt_dev.Click += new System.EventHandler(this.querybt_dev_Click);
            // 
            // cancelbt_dev
            // 
            this.cancelbt_dev.Location = new System.Drawing.Point(236, 375);
            this.cancelbt_dev.Name = "cancelbt_dev";
            this.cancelbt_dev.Size = new System.Drawing.Size(75, 23);
            this.cancelbt_dev.TabIndex = 2;
            this.cancelbt_dev.Text = "取消";
            this.cancelbt_dev.Click += new System.EventHandler(this.cancelbt_dev_Click);
            // 
            // SearchMapByAttribution
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 434);
            this.Controls.Add(this.cancelbt_dev);
            this.Controls.Add(this.querybt_dev);
            this.Controls.Add(this.groupControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SearchMapByAttribution";
            this.Text = "属性查图";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SearchMapByAttribution_FormClosed);
            this.Load += new System.EventHandler(this.SearchMapByAttribution_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.queryConditoncb_dev.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.queryFieldcb_dev.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layerNamecb_dev.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ComboBoxEdit queryConditoncb_dev;
        private DevExpress.XtraEditors.ComboBoxEdit queryFieldcb_dev;
        private DevExpress.XtraEditors.ComboBoxEdit layerNamecb_dev;
        private DevExpress.XtraEditors.SimpleButton querybt_dev;
        private DevExpress.XtraEditors.SimpleButton cancelbt_dev;


    }
}