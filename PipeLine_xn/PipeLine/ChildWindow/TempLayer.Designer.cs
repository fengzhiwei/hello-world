namespace PipeLine.ChildWindow
{
    partial class TempLayer
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.ToUp_bt = new System.Windows.Forms.Button();
            this.ToDown_bt = new System.Windows.Forms.Button();
            this.OK = new System.Windows.Forms.Button();
            this.ToTop_bt = new System.Windows.Forms.Button();
            this.ToBottom_bt = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 14;
            this.listBox1.Location = new System.Drawing.Point(12, 25);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(190, 368);
            this.listBox1.TabIndex = 0;
            // 
            // ToUp_bt
            // 
            this.ToUp_bt.Location = new System.Drawing.Point(302, 36);
            this.ToUp_bt.Name = "ToUp_bt";
            this.ToUp_bt.Size = new System.Drawing.Size(75, 23);
            this.ToUp_bt.TabIndex = 1;
            this.ToUp_bt.Text = "上移";
            this.ToUp_bt.UseVisualStyleBackColor = true;
            this.ToUp_bt.Click += new System.EventHandler(this.ToUp_bt_Click);
            // 
            // ToDown_bt
            // 
            this.ToDown_bt.Location = new System.Drawing.Point(302, 110);
            this.ToDown_bt.Name = "ToDown_bt";
            this.ToDown_bt.Size = new System.Drawing.Size(75, 23);
            this.ToDown_bt.TabIndex = 2;
            this.ToDown_bt.Text = "下移";
            this.ToDown_bt.UseVisualStyleBackColor = true;
            this.ToDown_bt.Click += new System.EventHandler(this.ToDown_bt_Click);
            // 
            // OK
            // 
            this.OK.Location = new System.Drawing.Point(302, 319);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(75, 23);
            this.OK.TabIndex = 3;
            this.OK.Text = "恢复";
            this.OK.UseVisualStyleBackColor = true;
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // ToTop_bt
            // 
            this.ToTop_bt.Location = new System.Drawing.Point(302, 186);
            this.ToTop_bt.Name = "ToTop_bt";
            this.ToTop_bt.Size = new System.Drawing.Size(75, 23);
            this.ToTop_bt.TabIndex = 4;
            this.ToTop_bt.Text = "置顶";
            this.ToTop_bt.UseVisualStyleBackColor = true;
            this.ToTop_bt.Click += new System.EventHandler(this.ToTop_bt_Click);
            // 
            // ToBottom_bt
            // 
            this.ToBottom_bt.Location = new System.Drawing.Point(302, 254);
            this.ToBottom_bt.Name = "ToBottom_bt";
            this.ToBottom_bt.Size = new System.Drawing.Size(75, 23);
            this.ToBottom_bt.TabIndex = 5;
            this.ToBottom_bt.Text = "置底";
            this.ToBottom_bt.UseVisualStyleBackColor = true;
            this.ToBottom_bt.Click += new System.EventHandler(this.ToBottom_bt_Click);
            // 
            // TempLayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 417);
            this.Controls.Add(this.ToBottom_bt);
            this.Controls.Add(this.ToTop_bt);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.ToDown_bt);
            this.Controls.Add(this.ToUp_bt);
            this.Controls.Add(this.listBox1);
            this.Name = "TempLayer";
            this.Text = "TempLayer";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.TempLayer_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ToUp_bt;
        private System.Windows.Forms.Button ToDown_bt;
        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.Button ToTop_bt;
        private System.Windows.Forms.Button ToBottom_bt;
        public System.Windows.Forms.ListBox listBox1;
    }
}