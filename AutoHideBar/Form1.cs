using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace AutoHideBar
{
    public partial class Form1 : Form
    {
        bool ib_CanExit = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //timer1.Enabled = false;
            var barState = TaskBarHider.GetAppBarState();
            if(barState== TaskBarHider.AppBarStates.AlwaysOnTop)
            {
                if (!ib_Hide)
                {
                    return;
                }
            }
            else
            {
                if (ib_Hide)
                {
                    return;
                }
            }
            TaskBarHider.SetAppBarAutoDisplay(ib_Hide);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SynchronizationContext.Current.Post(d =>
            {
                this.Hide();
                if (ib_Hide)
                {

                    radioButton1.Checked = true;
                    radioButton2.Checked = false;
                }
                else
                {
                    radioButton1.Checked = false;
                    radioButton2.Checked = true;
                }
            }, null);
            timer1.Enabled = true;
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ib_CanExit = true;
            Application.Exit();
        }

        private void 设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            if (!ib_CanExit)
                e.Cancel = true;
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(e.Button== MouseButtons.Left)
            {
                if (this.Visible)
                {
                    this.Hide();
                }
                else
                {
                    this.Show();
                }
            }
        }
        bool ib_Hide = false;
        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            if (radioButton1.Checked)
            {
                ib_Hide = true;
            }
            else
            {
                ib_Hide = false;
            }
            timer1.Enabled = true;
        }
    }
}
