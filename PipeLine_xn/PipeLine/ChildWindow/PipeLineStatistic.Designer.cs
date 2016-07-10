namespace PipeLine.ChildWindow
{
    partial class PipeLineStatistic
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PipeLineStatistic));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.addusedatebt = new DevExpress.XtraEditors.SimpleButton();
            this.addmaterialbt = new DevExpress.XtraEditors.SimpleButton();
            this.export = new DevExpress.XtraEditors.SimpleButton();
            this.statistic = new DevExpress.XtraEditors.SimpleButton();
            this.allclear = new DevExpress.XtraEditors.SimpleButton();
            this.allselect = new DevExpress.XtraEditors.SimpleButton();
            this.usedateTime = new DevExpress.XtraEditors.DateEdit();
            this.addmaterialtb = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.usedateclb = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.materialclb = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.pipe_clbc = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.DisplayChart = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.usedateTime.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.usedateTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.addmaterialtb.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.usedateclb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.materialclb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pipe_clbc)).BeginInit();
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
            this.groupControl1.Controls.Add(this.DisplayChart);
            this.groupControl1.Controls.Add(this.addusedatebt);
            this.groupControl1.Controls.Add(this.addmaterialbt);
            this.groupControl1.Controls.Add(this.export);
            this.groupControl1.Controls.Add(this.statistic);
            this.groupControl1.Controls.Add(this.allclear);
            this.groupControl1.Controls.Add(this.allselect);
            this.groupControl1.Controls.Add(this.usedateTime);
            this.groupControl1.Controls.Add(this.addmaterialtb);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.usedateclb);
            this.groupControl1.Controls.Add(this.materialclb);
            this.groupControl1.Controls.Add(this.pipe_clbc);
            this.groupControl1.Location = new System.Drawing.Point(3, 12);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(440, 325);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "操作选项";
            // 
            // addusedatebt
            // 
            this.addusedatebt.Location = new System.Drawing.Point(339, 299);
            this.addusedatebt.Name = "addusedatebt";
            this.addusedatebt.Size = new System.Drawing.Size(75, 23);
            this.addusedatebt.TabIndex = 22;
            this.addusedatebt.Text = "使用日期";
            this.addusedatebt.Click += new System.EventHandler(this.addusedatebt_Click);
            // 
            // addmaterialbt
            // 
            this.addmaterialbt.Location = new System.Drawing.Point(339, 266);
            this.addmaterialbt.Name = "addmaterialbt";
            this.addmaterialbt.Size = new System.Drawing.Size(75, 23);
            this.addmaterialbt.TabIndex = 21;
            this.addmaterialbt.Text = "添加材质";
            this.addmaterialbt.Click += new System.EventHandler(this.addmaterialbt_Click);
            // 
            // export
            // 
            this.export.Location = new System.Drawing.Point(339, 176);
            this.export.Name = "export";
            this.export.Size = new System.Drawing.Size(75, 23);
            this.export.TabIndex = 20;
            this.export.Text = "导出";
            // 
            // statistic
            // 
            this.statistic.Location = new System.Drawing.Point(339, 137);
            this.statistic.Name = "statistic";
            this.statistic.Size = new System.Drawing.Size(75, 23);
            this.statistic.TabIndex = 19;
            this.statistic.Text = "统 计";
            this.statistic.Click += new System.EventHandler(this.statistic_Click);
            // 
            // allclear
            // 
            this.allclear.Location = new System.Drawing.Point(339, 98);
            this.allclear.Name = "allclear";
            this.allclear.Size = new System.Drawing.Size(75, 23);
            this.allclear.TabIndex = 18;
            this.allclear.Text = "清 空";
            this.allclear.Click += new System.EventHandler(this.allclear_Click);
            // 
            // allselect
            // 
            this.allselect.Location = new System.Drawing.Point(339, 59);
            this.allselect.Name = "allselect";
            this.allselect.Size = new System.Drawing.Size(75, 23);
            this.allselect.TabIndex = 17;
            this.allselect.Text = "全 选";
            this.allselect.Click += new System.EventHandler(this.allselect_Click);
            // 
            // usedateTime
            // 
            this.usedateTime.EditValue = null;
            this.usedateTime.Location = new System.Drawing.Point(147, 300);
            this.usedateTime.Name = "usedateTime";
            this.usedateTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.usedateTime.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.usedateTime.Size = new System.Drawing.Size(162, 20);
            this.usedateTime.TabIndex = 16;
            this.usedateTime.EditValueChanged += new System.EventHandler(this.usedateTime_EditValueChanged);
            // 
            // addmaterialtb
            // 
            this.addmaterialtb.Location = new System.Drawing.Point(147, 267);
            this.addmaterialtb.Name = "addmaterialtb";
            this.addmaterialtb.Size = new System.Drawing.Size(162, 20);
            this.addmaterialtb.TabIndex = 15;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(147, 247);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(96, 14);
            this.labelControl3.TabIndex = 14;
            this.labelControl3.Text = "添加材质和日期：";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(147, 39);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(72, 14);
            this.labelControl2.TabIndex = 13;
            this.labelControl2.Text = "材质和日期：";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(5, 39);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 14);
            this.labelControl1.TabIndex = 12;
            this.labelControl1.Text = "管线名称：";
            // 
            // usedateclb
            // 
            this.usedateclb.Location = new System.Drawing.Point(147, 141);
            this.usedateclb.Name = "usedateclb";
            this.usedateclb.Size = new System.Drawing.Size(162, 100);
            this.usedateclb.TabIndex = 3;
            // 
            // materialclb
            // 
            this.materialclb.Location = new System.Drawing.Point(147, 59);
            this.materialclb.Name = "materialclb";
            this.materialclb.Size = new System.Drawing.Size(162, 76);
            this.materialclb.TabIndex = 2;
            // 
            // pipe_clbc
            // 
            this.pipe_clbc.Location = new System.Drawing.Point(5, 59);
            this.pipe_clbc.Name = "pipe_clbc";
            this.pipe_clbc.Size = new System.Drawing.Size(136, 261);
            this.pipe_clbc.TabIndex = 1;
            // 
            // groupControl2
            // 
            this.groupControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl2.Controls.Add(this.gridControl1);
            this.groupControl2.Location = new System.Drawing.Point(3, 343);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(440, 188);
            this.groupControl2.TabIndex = 1;
            // 
            // gridControl1
            // 
            this.gridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl1.Location = new System.Drawing.Point(5, 25);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(429, 158);
            this.gridControl1.TabIndex = 1;
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
            // DisplayChart
            // 
            this.DisplayChart.Location = new System.Drawing.Point(339, 215);
            this.DisplayChart.Name = "DisplayChart";
            this.DisplayChart.Size = new System.Drawing.Size(75, 23);
            this.DisplayChart.TabIndex = 23;
            this.DisplayChart.Text = "显示统计图";
            this.DisplayChart.Click += new System.EventHandler(this.DisplayChart_Click);
            // 
            // PipeLineStatistic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 546);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PipeLineStatistic";
            this.Text = "管线统计";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.PipeLineStatistic_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.usedateTime.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.usedateTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.addmaterialtb.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.usedateclb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.materialclb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pipe_clbc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton addusedatebt;
        private DevExpress.XtraEditors.SimpleButton addmaterialbt;
        private DevExpress.XtraEditors.SimpleButton export;
        private DevExpress.XtraEditors.SimpleButton statistic;
        private DevExpress.XtraEditors.SimpleButton allclear;
        private DevExpress.XtraEditors.SimpleButton allselect;
        private DevExpress.XtraEditors.DateEdit usedateTime;
        private DevExpress.XtraEditors.TextEdit addmaterialtb;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.CheckedListBoxControl usedateclb;
        private DevExpress.XtraEditors.CheckedListBoxControl materialclb;
        private DevExpress.XtraEditors.CheckedListBoxControl pipe_clbc;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SimpleButton DisplayChart;
    }
}