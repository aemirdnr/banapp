using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BanApp
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
        }

        string appname;
        int time;

        private void btnStart_Click(object sender, EventArgs e)
        {
            start();
        }

        void start()
        {
            if (banTimer.Enabled == false)
            {
                appname = txtApp.Text;
                time = Convert.ToInt32(txtMin.Text);

                if (!string.IsNullOrEmpty(appname) && !string.IsNullOrEmpty(Convert.ToString(time)))
                {
                    int min = time;
                    int hr = min / 60;
                    int sec = time * 60;
                    int msec = sec * 1000;

                    if (time >= 60)
                    {
                        min -= hr * 60;

                        banTimer.Interval = msec;

                    }
                    else
                    {
                        banTimer.Interval = msec;
                    }

                    //Results gbox
                    lblStat.Text = "Status: ON";
                    lblApp.Text = "App name: " + appname;
                    lblApp.Show();
                    lblTime.Text = "Will be closed in " + time + " minute.";
                    lblTime.Show();
                    onOff.BackColor = Color.Green;

                    banTimer.Start();
                }
                else
                {
                    MessageBox.Show("App name or minute is empty!");
                }
            }
            else
            {
                DialogResult ctrl = new DialogResult();
                ctrl = MessageBox.Show("Cancel Process", "There is have already process, Do you want cancel process?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (ctrl == DialogResult.Yes)
                {
                    //cancel process
                    banTimer.Stop();

                    //Results gbox
                    lblStat.Text = "Status: OFF";
                    lblApp.Hide();
                    lblTime.Hide();
                    onOff.BackColor = Color.Red;
                }
            }
        }

        private void banTimer_Tick(object sender, EventArgs e)
        {
            appname = txtApp.Text;
            Process[] processes = Process.GetProcesses();

            //find appname in process and kill.
            foreach (Process appList in processes)
            {
                if (appList.ProcessName == appname)
                {
                    appList.Kill();
                    
                    //Results gbox
                    lblStat.Text = "Status: OFF";
                    lblApp.Hide();
                    lblTime.Hide();
                    onOff.BackColor = Color.Red;

                    banTimer.Stop();
                }
            }
        }
    }
}
