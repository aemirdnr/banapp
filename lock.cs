using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BanApp
{
    public partial class @lock : Form
    {
        public @lock()
        {
            InitializeComponent();
        }

        private void unlock_Tick(object sender, EventArgs e)
        {
            main mn = new main();            
            mn.Show();
            this.Hide();
            unlock.Stop();
            MessageBox.Show("Time is up!");
        }

        private void lock_Load(object sender, EventArgs e)
        {
            unlock.Start();
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
