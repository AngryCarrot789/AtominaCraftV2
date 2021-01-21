using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AtominaCraft.ZResources.Windows
{
    public partial class DebugWindow : Form
    {
        public DebugWindow()
        {
            InitializeComponent();
        }

        public void Write(string text)
        {
            label1.Text += text;
        }

        public void Clear()
        {
            label1.Text = "";
        }
    }
}
