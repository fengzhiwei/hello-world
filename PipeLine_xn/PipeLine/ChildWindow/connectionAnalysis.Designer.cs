namespace PipeLine.ChildWindow
{
    partial class connectionAnalysis
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.reset_bt = new DevExpress.XtraEditors.SimpleButton();
            this.result_tb = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.result_tb.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "结果概述：";
            // 
            // reset_bt
            // 
            this.reset_bt.Location = new System.Drawing.Point(379, 8);
            this.reset_bt.Name = "reset_bt";
            this.reset_bt.Size = new System.Drawing.Size(57, 23);
            this.reset_bt.TabIndex = 2;
            this.reset_bt.Text = "重新取点";
            this.reset_bt.Click += new System.EventHandler(this.reset_bt_Click);
            // 
            // result_tb
            // 
            this.result_tb.Location = new System.Drawing.Point(78, 9);
            this.result_tb.Name = "result_tb";
            this.result_tb.Size = new System.Drawing.Size(295, 20);
            this.result_tb.TabIndex = 3;
            // 
            // connectionAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 214);
            this.Controls.Add(this.result_tb);
            this.Controls.Add(this.reset_bt);
            this.Controls.Add(this.labelControl1);
            this.Name = "connectionAnalysis";
            this.Text = "连通性分析";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.connectionAnalysis_Load);
            ((System.ComponentModel.ISupportInitialize)(this.result_tb.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton reset_bt;
        private DevExpress.XtraEditors.TextEdit result_tb;
    }
}