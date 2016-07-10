using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PipeLine.ChildWindow
{
    public partial class waiting : Form
    {
        public static waiting _Instance = null;
        public waiting()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }
        public static void Show(Form owner)
        {
            owner.UseWaitCursor = true;
            if (_Instance == null) _Instance = new waiting();
            _Instance.Owner = owner;
            _Instance.Show();

            Application.DoEvents();
        }
        public static void Show(Form owner, bool disableOwner)
        {
            owner.UseWaitCursor = true;
            owner.Enabled = !disableOwner;

            if (_Instance == null) _Instance = new waiting();
            _Instance.Owner = owner;
            _Instance.Show();

            Application.DoEvents();
        }
        public static void Hide(Form owner)
        {
            owner.UseWaitCursor = false;
            owner.Enabled = true;

            if (_Instance != null) _Instance.Hide();
            Application.DoEvents();
        }
    }
}
