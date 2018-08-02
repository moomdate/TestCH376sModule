using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class settings : Form
    {
        public settings()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            int cc = Properties.Settings.Default.buadRate;
            Comde.Text = Properties.Settings.Default.decom;
            baudRate.SelectedIndex = cc;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.buadRate = baudRate.SelectedIndex;
            Properties.Settings.Default.decom = Comde.Text;
            Properties.Settings.Default.Save();
            // MessageBox.Show(baudRate.SelectedIndex.ToString());
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Comde_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
