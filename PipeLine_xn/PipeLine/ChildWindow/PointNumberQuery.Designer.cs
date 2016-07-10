namespace PipeLine.ChildWindow
{
    partial class PointNumberQuery
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PointNumberQuery));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.DiaplayChart = new DevExpress.XtraEditors.SimpleButton();
            this.Statistic = new DevExpress.XtraEditors.SimpleButton();
            this.AllClear = new DevExpress.XtraEditors.SimpleButton();
            this.AllSelect = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl4 = new DevExpress.XtraEditors.GroupControl();
            this.TypeListBoxControl2 = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.LineListBoxControl1 = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).BeginInit();
            this.groupControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TypeListBoxControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LineListBoxControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl1.Controls.Add(this.DiaplayChart);
            this.groupControl1.Controls.Add(this.Statistic);
            this.groupControl1.Controls.Add(this.AllClear);
            this.groupControl1.Controls.Add(this.AllSelect);
            this.groupControl1.Controls.Add(this.groupControl4);
            this.groupControl1.Controls.Add(this.groupControl3);
            this.groupControl1.Location = new System.Drawing.Point(12, 12);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(450, 247);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "统计条件：";
            // 
            // DiaplayChart
            // 
            this.DiaplayChart.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.DiaplayChart.Location = new System.Drawing.Point(341, 213);
            this.DiaplayChart.Name = "DiaplayChart";
            this.DiaplayChart.Size = new System.Drawing.Size(75, 29);
            this.DiaplayChart.TabIndex = 8;
            this.DiaplayChart.Text = "显示统计图";
            this.DiaplayChart.Click += new System.EventHandler(this.DiaplayChart_Click);
            // 
            // Statistic
            // 
            this.Statistic.Location = new System.Drawing.Point(341, 153);
            this.Statistic.Name = "Statistic";
            this.Statistic.Size = new System.Drawing.Size(75, 23);
            this.Statistic.TabIndex = 6;
            this.Statistic.Text = "统计";
            this.Statistic.Click += new System.EventHandler(this.Statistic_Click);
            // 
            // AllClear
            // 
            this.AllClear.Location = new System.Drawing.Point(341, 89);
            this.AllClear.Name = "AllClear";
            this.AllClear.Size = new System.Drawing.Size(75, 23);
            this.AllClear.TabIndex = 5;
            this.AllClear.Text = "清空";
            this.AllClear.Click += new System.EventHandler(this.AllClear_Click);
            // 
            // AllSelect
            // 
            this.AllSelect.Location = new System.Drawing.Point(341, 25);
            this.AllSelect.Name = "AllSelect";
            this.AllSelect.Size = new System.Drawing.Size(75, 23);
            this.AllSelect.TabIndex = 4;
            this.AllSelect.Text = "全选";
            this.AllSelect.Click += new System.EventHandler(this.AllSelect_Click);
            // 
            // groupControl4
            // 
            this.groupControl4.Controls.Add(this.TypeListBoxControl2);
            this.groupControl4.Location = new System.Drawing.Point(164, 25);
            this.groupControl4.Name = "groupControl4";
            this.groupControl4.Size = new System.Drawing.Size(138, 217);
            this.groupControl4.TabIndex = 3;
            this.groupControl4.Text = "选择类型：";
            // 
            // TypeListBoxControl2
            // 
            this.TypeListBoxControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TypeListBoxControl2.Items.AddRange(new DevExpress.XtraEditors.Controls.CheckedListBoxItem[] {
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("二通管点"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("三通管点"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("四通管点")});
            this.TypeListBoxControl2.Location = new System.Drawing.Point(0, 25);
            this.TypeListBoxControl2.Name = "TypeListBoxControl2";
            this.TypeListBoxControl2.Size = new System.Drawing.Size(128, 187);
            this.TypeListBoxControl2.TabIndex = 1;
            // 
            // groupControl3
            // 
            this.groupControl3.Controls.Add(this.LineListBoxControl1);
            this.groupControl3.Location = new System.Drawing.Point(6, 25);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(138, 217);
            this.groupControl3.TabIndex = 2;
            this.groupControl3.Text = "选择管线：";
            // 
            // LineListBoxControl1
            // 
            this.LineListBoxControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LineListBoxControl1.Location = new System.Drawing.Point(5, 25);
            this.LineListBoxControl1.Name = "LineListBoxControl1";
            this.LineListBoxControl1.Size = new System.Drawing.Size(128, 187);
            this.LineListBoxControl1.TabIndex = 0;
            // 
            // groupControl2
            // 
            this.groupControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl2.Controls.Add(this.gridControl1);
            this.groupControl2.Location = new System.Drawing.Point(12, 274);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(450, 170);
            this.groupControl2.TabIndex = 1;
            this.groupControl2.Text = "结果：";
            // 
            // gridControl1
            // 
            this.gridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl1.Location = new System.Drawing.Point(6, 25);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(439, 140);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // PointNumberQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 456);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PointNumberQuery";
            this.Text = "管点统计";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.PointNumberQuery_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).EndInit();
            this.groupControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TypeListBoxControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LineListBoxControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton Statistic;
        private DevExpress.XtraEditors.SimpleButton AllClear;
        private DevExpress.XtraEditors.SimpleButton AllSelect;
        private DevExpress.XtraEditors.GroupControl groupControl4;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.CheckedListBoxControl TypeListBoxControl2;
        private DevExpress.XtraEditors.CheckedListBoxControl LineListBoxControl1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SimpleButton DiaplayChart;
    }
}