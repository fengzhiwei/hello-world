namespace PipeLine.ChildWindow
{
    partial class PipePointQuery
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PipePointQuery));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.cancelbt_dev = new DevExpress.XtraEditors.SimpleButton();
            this.ensurebt_dev = new DevExpress.XtraEditors.SimpleButton();
            this.queryvaluecb_dev = new DevExpress.XtraEditors.ComboBoxEdit();
            this.queryconditoncb_dev = new DevExpress.XtraEditors.ComboBoxEdit();
            this.queryfiled_dev = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.PipePointcb_dev = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.resultdata_dev = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.queryvaluecb_dev.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.queryconditoncb_dev.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.queryfiled_dev.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PipePointcb_dev.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultdata_dev)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.cancelbt_dev);
            this.groupControl1.Controls.Add(this.ensurebt_dev);
            this.groupControl1.Controls.Add(this.queryvaluecb_dev);
            this.groupControl1.Controls.Add(this.queryconditoncb_dev);
            this.groupControl1.Controls.Add(this.queryfiled_dev);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.PipePointcb_dev);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Location = new System.Drawing.Point(12, 12);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(555, 163);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "参数设置：";
            // 
            // cancelbt_dev
            // 
            this.cancelbt_dev.Location = new System.Drawing.Point(447, 118);
            this.cancelbt_dev.Name = "cancelbt_dev";
            this.cancelbt_dev.Size = new System.Drawing.Size(75, 23);
            this.cancelbt_dev.TabIndex = 9;
            this.cancelbt_dev.Text = "取消";
            this.cancelbt_dev.Click += new System.EventHandler(this.cancelbt_dev_Click);
            // 
            // ensurebt_dev
            // 
            this.ensurebt_dev.Location = new System.Drawing.Point(447, 38);
            this.ensurebt_dev.Name = "ensurebt_dev";
            this.ensurebt_dev.Size = new System.Drawing.Size(75, 23);
            this.ensurebt_dev.TabIndex = 8;
            this.ensurebt_dev.Text = "查询";
            this.ensurebt_dev.Click += new System.EventHandler(this.ensurebt_dev_Click);
            // 
            // queryvaluecb_dev
            // 
            this.queryvaluecb_dev.Location = new System.Drawing.Point(326, 119);
            this.queryvaluecb_dev.Name = "queryvaluecb_dev";
            this.queryvaluecb_dev.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.queryvaluecb_dev.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.queryvaluecb_dev.Size = new System.Drawing.Size(71, 20);
            this.queryvaluecb_dev.TabIndex = 7;
            // 
            // queryconditoncb_dev
            // 
            this.queryconditoncb_dev.Location = new System.Drawing.Point(187, 119);
            this.queryconditoncb_dev.Name = "queryconditoncb_dev";
            this.queryconditoncb_dev.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.queryconditoncb_dev.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.queryconditoncb_dev.Size = new System.Drawing.Size(79, 20);
            this.queryconditoncb_dev.TabIndex = 6;
            // 
            // queryfiled_dev
            // 
            this.queryfiled_dev.Location = new System.Drawing.Point(35, 119);
            this.queryfiled_dev.Name = "queryfiled_dev";
            this.queryfiled_dev.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.queryfiled_dev.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.queryfiled_dev.Size = new System.Drawing.Size(92, 20);
            this.queryfiled_dev.TabIndex = 5;
            this.queryfiled_dev.SelectedIndexChanged += new System.EventHandler(this.queryfiled_dev_SelectedIndexChanged);
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(187, 89);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(48, 14);
            this.labelControl4.TabIndex = 4;
            this.labelControl4.Text = "查询条件";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(326, 89);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(36, 14);
            this.labelControl3.TabIndex = 3;
            this.labelControl3.Text = "查询值";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(35, 89);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 14);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "查询字段";
            // 
            // PipePointcb_dev
            // 
            this.PipePointcb_dev.Location = new System.Drawing.Point(119, 39);
            this.PipePointcb_dev.Name = "PipePointcb_dev";
            this.PipePointcb_dev.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.PipePointcb_dev.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.PipePointcb_dev.Size = new System.Drawing.Size(279, 20);
            this.PipePointcb_dev.TabIndex = 1;
            this.PipePointcb_dev.SelectedIndexChanged += new System.EventHandler(this.PipePointcb_dev_SelectedIndexChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(35, 42);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "管点图层：";
            // 
            // resultdata_dev
            // 
            this.resultdata_dev.AllowRestoreSelectionAndFocusedRow = DevExpress.Utils.DefaultBoolean.False;
            this.resultdata_dev.Location = new System.Drawing.Point(12, 181);
            this.resultdata_dev.MainView = this.gridView1;
            this.resultdata_dev.Name = "resultdata_dev";
            this.resultdata_dev.Size = new System.Drawing.Size(555, 222);
            this.resultdata_dev.TabIndex = 1;
            this.resultdata_dev.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.resultdata_dev;
            this.gridView1.GroupPanelText = "\"属性：\"";
            this.gridView1.IndicatorWidth = 30;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridView1_CustomDrawRowIndicator);
            // 
            // PipePointQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 415);
            this.Controls.Add(this.resultdata_dev);
            this.Controls.Add(this.groupControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PipePointQuery";
            this.Text = "管点查询";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.PipePointQuery_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.queryvaluecb_dev.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.queryconditoncb_dev.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.queryfiled_dev.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PipePointcb_dev.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultdata_dev)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ComboBoxEdit PipePointcb_dev;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.ComboBoxEdit queryvaluecb_dev;
        private DevExpress.XtraEditors.ComboBoxEdit queryconditoncb_dev;
        private DevExpress.XtraEditors.ComboBoxEdit queryfiled_dev;
        private DevExpress.XtraEditors.SimpleButton cancelbt_dev;
        private DevExpress.XtraEditors.SimpleButton ensurebt_dev;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        public DevExpress.XtraGrid.GridControl resultdata_dev;
    }
}