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
        int time,sec,msec,min;
 
        void start()
        {
            if (!banTimer.Enabled)
            {
                appname = txtApp.Text;
                time = Convert.ToInt32(txtMin.Text);

                if (!string.IsNullOrEmpty(appname) && !string.IsNullOrEmpty(Convert.ToString(time)))
                {
                    sec = time * 60;
                    msec = sec * 1000;

                    banTimer.Interval = msec;

                    //Results gbox
                    lblStat.Text = "Status: ON";
                    lblApp.Text = "App name: " + appname;
                    lblApp.Show();
                    lblTime.Show();
                    onOff.BackColor = Color.Green;

                    banTimer.Start();
                    infoTimer.Start();
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
                    infoTimer.Stop();

                    //Results gbox
                    lblStat.Text = "Status: OFF";
                    lblApp.Hide();
                    lblTime.Hide();
                    onOff.BackColor = Color.Red;
                }
            }
        }

        void timeleft()
        {
            if (sec > 60)
            {
                min = sec / 60;
                sec = 1;
            }
            else if (sec == 0)
            {
                sec = 59;
                min--;
            }

            sec--;
            lblTime.Text = min + " Minute " + sec + " Second left.";
        }

        void kill()
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

        private void btnStart_Click(object sender, EventArgs e)
        {
            start();
        }

        private void banTimer_Tick(object sender, EventArgs e)
        {
            kill();
        }

        private void infoTimer_Tick(object sender, EventArgs e)
        {
            timeleft();
        }
    }
}
