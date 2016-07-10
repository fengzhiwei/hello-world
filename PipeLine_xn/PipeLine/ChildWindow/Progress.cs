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
    public partial class Progress : DevExpress.XtraEditors.XtraForm
    {
        public int m_count = 0;
        public Progress(int count)
        {
            InitializeComponent();
            m_count = count;
        }
    }
}