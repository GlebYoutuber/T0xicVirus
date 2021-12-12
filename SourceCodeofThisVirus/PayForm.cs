using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace T0xicVirus
{
    public partial class PayForm : Form
    {
        public PayForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process.Start("chrome.exe", "https://ko-fi.com/glebyoutuber");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process.Start("chrome.exe", "https://www.donationalerts.com/r/glebyoutuber");
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
