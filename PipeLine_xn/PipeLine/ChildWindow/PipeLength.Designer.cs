namespace PipeLine.ChildWindow
{
    partial class PipeLength
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PipeLength));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.DisplayChart = new DevExpress.XtraEditors.SimpleButton();
            this.minTe = new DevExpress.XtraEditors.TextEdit();
            this.maxTe = new DevExpress.XtraEditors.TextEdit();
            this.lessThan = new DevExpress.XtraEditors.ComboBoxEdit();
            this.moreThan = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.statisticsbt = new DevExpress.XtraEditors.SimpleButton();
            this.clearselect = new DevExpress.XtraEditors.SimpleButton();
            this.allselect = new DevExpress.XtraEditors.SimpleButton();
            this.pipe_clbc = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.minTe.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxTe.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lessThan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.moreThan.Properties)).BeginInit();
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
            this.groupControl1.Controls.Add(this.minTe);
            this.groupControl1.Controls.Add(this.maxTe);
            this.groupControl1.Controls.Add(this.lessThan);
            this.groupControl1.Controls.Add(this.moreThan);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.statisticsbt);
            this.groupControl1.Controls.Add(this.clearselect);
            this.groupControl1.Controls.Add(this.allselect);
            this.groupControl1.Controls.Add(this.pipe_clbc);
            this.groupControl1.Location = new System.Drawing.Point(12, 12);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(407, 264);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "操作选项";
            // 
            // DisplayChart
            // 
            this.DisplayChart.Location = new System.Drawing.Point(292, 225);
            this.DisplayChart.Name = "DisplayChart";
            this.DisplayChart.Size = new System.Drawing.Size(75, 23);
            this.DisplayChart.TabIndex = 10;
            this.DisplayChart.Text = "显示统计图";
            this.DisplayChart.Click += new System.EventHandler(this.DisplayChart_Click);
            // 
            // minTe
            // 
            this.minTe.Location = new System.Drawing.Point(292, 148);
            this.minTe.Name = "minTe";
            this.minTe.Size = new System.Drawing.Size(75, 20);
            this.minTe.TabIndex = 9;
            this.minTe.Validated += new System.EventHandler(this.minTe_Validated);
            // 
            // maxTe
            // 
            this.maxTe.Location = new System.Drawing.Point(292, 104);
            this.maxTe.Name = "maxTe";
            this.maxTe.Size = new System.Drawing.Size(75, 20);
            this.maxTe.TabIndex = 8;
            this.maxTe.Validated += new System.EventHandler(this.maxTe_Validated);
            // 
            // lessThan
            // 
            this.lessThan.Location = new System.Drawing.Point(221, 148);
            this.lessThan.Name = "lessThan";
            this.lessThan.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lessThan.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.lessThan.Size = new System.Drawing.Size(58, 20);
            this.lessThan.TabIndex = 7;
            // 
            // moreThan
            // 
            this.moreThan.Location = new System.Drawing.Point(221, 104);
            this.moreThan.Name = "moreThan";
            this.moreThan.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.moreThan.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.moreThan.Size = new System.Drawing.Size(58, 20);
            this.moreThan.TabIndex = 6;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(179, 151);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(36, 14);
            this.labelControl2.TabIndex = 5;
            this.labelControl2.Text = "长度：";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(179, 107);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(36, 14);
            this.labelControl1.TabIndex = 4;
            this.labelControl1.Text = "长度：";
            // 
            // statisticsbt
            // 
            this.statisticsbt.Location = new System.Drawing.Point(179, 225);
            this.statisticsbt.Name = "statisticsbt";
            this.statisticsbt.Size = new System.Drawing.Size(75, 23);
            this.statisticsbt.TabIndex = 3;
            this.statisticsbt.Text = "统计";
            this.statisticsbt.Click += new System.EventHandler(this.statisticsbt_Click);
            // 
            // clearselect
            // 
            this.clearselect.Location = new System.Drawing.Point(292, 38);
            this.clearselect.Name = "clearselect";
            this.clearselect.Size = new System.Drawing.Size(75, 23);
            this.clearselect.TabIndex = 2;
            this.clearselect.Text = "清空";
            this.clearselect.Click += new System.EventHandler(this.clearselect_Click);
            // 
            // allselect
            // 
            this.allselect.Location = new System.Drawing.Point(179, 38);
            this.allselect.Name = "allselect";
            this.allselect.Size = new System.Drawing.Size(75, 23);
            this.allselect.TabIndex = 1;
            this.allselect.Text = "全选";
            this.allselect.Click += new System.EventHandler(this.allselect_Click);
            // 
            // pipe_clbc
            // 
            this.pipe_clbc.Location = new System.Drawing.Point(5, 25);
            this.pipe_clbc.Name = "pipe_clbc";
            this.pipe_clbc.Size = new System.Drawing.Size(156, 234);
            this.pipe_clbc.TabIndex = 0;
            // 
            // groupControl2
            // 
            this.groupControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl2.Controls.Add(this.gridControl1);
            this.groupControl2.Location = new System.Drawing.Point(12, 282);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(407, 226);
            this.groupControl2.TabIndex = 1;
            this.groupControl2.Text = "结果：";
            // 
            // gridControl1
            // 
            this.gridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl1.Location = new System.Drawing.Point(5, 26);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(397, 195);
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
            // PipeLength
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 508);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PipeLength";
            this.Text = "长度统计";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.PipeLength_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.minTe.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxTe.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lessThan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.moreThan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pipe_clbc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.TextEdit minTe;
        private DevExpress.XtraEditors.TextEdit maxTe;
        private DevExpress.XtraEditors.ComboBoxEdit lessThan;
        private DevExpress.XtraEditors.ComboBoxEdit moreThan;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton statisticsbt;
        private DevExpress.XtraEditors.SimpleButton clearselect;
        private DevExpress.XtraEditors.SimpleButton allselect;
        private DevExpress.XtraEditors.CheckedListBoxControl pipe_clbc;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SimpleButton DisplayChart;
    }
}