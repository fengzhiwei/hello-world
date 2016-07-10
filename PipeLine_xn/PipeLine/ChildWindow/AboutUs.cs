using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace PipeLine.ChildWindow
{
    public partial class AboutUs : DevExpress.XtraEditors.XtraForm
    {
        public AboutUs()
        {
            InitializeComponent();
        }

        private void AboutUs_Load(object sender, EventArgs e)
        {
            this.textBox1.Select(0, 0);
        }
    }
}