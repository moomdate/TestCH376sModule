using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Diagnostics;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        int[] br = new int[] { 9600, 14400, 19200, 38400, 57600, 115200, 128000 };
        Boolean mountStatus = false;
        
        public Form1()
        {
            InitializeComponent();
          
            foreach (string s in SerialPort.GetPortNames())
            {
                comboBox1.Items.Add(s);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Text = Properties.Settings.Default.decom;
            brd.Text = br[Properties.Settings.Default.buadRate].ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (comboBox1.Text!="")
            {
                MessageBox.Show("Open Port: " + comboBox1.Text);
                listBox1.Items.Add("FTDI : OK");
                try {
                    
                    serialPort1.PortName = comboBox1.Text;
                    serialPort1.DataBits = 8;
                    serialPort1.Parity = Parity.None;
                    serialPort1.StopBits = StopBits.One;
                    serialPort1.BaudRate = br[Properties.Settings.Default.buadRate];
                   // Debug.Print(br[Properties.Settings.Default.buadRate].ToString());
                }catch(System.InvalidOperationException)
                {
                    MessageBox.Show("Port has Open");
                }
                try
                {
                    serialPort1.Open();
                    serialPort1.DiscardOutBuffer();
                    serialPort1.DiscardInBuffer();
                    //Debug.Print("OG");
                }
                catch (Exception exc)
                {
                }
            }
        }
        private void responseHandler(object sender, SerialDataReceivedEventArgs args)
        {
            string x = serialPort1.ReadExisting();
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] asciiBytes = Encoding.ASCII.GetBytes(serialPort1.ReadExisting());
            foreach (int c in asciiBytes)
            {
                
                Console.WriteLine("{0:x}", c);
                if (c == 0x51) {
                    BeginInvoke(new EventHandler(delegate
                    {
                        listBox1.Items.Add("Set SD : OK");
                        int visibleItems = listBox1.ClientSize.Height / listBox1.ItemHeight;
                        listBox1.TopIndex = Math.Max(listBox1.Items.Count - visibleItems + 1, 0);
                    }));
                } else if (c == 0x17)
                {
                    //mountStatus = true;
                    BeginInvoke(new EventHandler(delegate
                    {
                        listBox1.Items.Add("Mount : Error");
                        listBox1.Items.Add("Insert SD Card and Mount again!!");
                        int visibleItems = listBox1.ClientSize.Height / listBox1.ItemHeight;
                        listBox1.TopIndex = Math.Max(listBox1.Items.Count - visibleItems + 1, 0);
                    }));
                } 
                else if (c == 0x14)
                {
                    BeginInvoke(new EventHandler(delegate
                    {
                        listBox1.Items.Add("Mount : OK");
                        int visibleItems = listBox1.ClientSize.Height / listBox1.ItemHeight;
                        listBox1.TopIndex = Math.Max(listBox1.Items.Count - visibleItems + 1, 0);
                    }));
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            foreach (string s in SerialPort.GetPortNames())
            {
                comboBox1.Items.Add(s);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try {
                serialPort1.Write(new byte[] { 0x57, 0xab, 0x15, 0x03 }, 0, 4);
            }
            catch (Exception) {

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.Write(new byte[] { 0x57, 0xab, 0x06 }, 0, 3);
            }
            catch (Exception)
            {

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.Write(new byte[] { 0x57, 0xab, 0x31 }, 0, 3);
            }
            catch (Exception)
            {

            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //brd.Text = br[Properties.Settings.Default.buadRate].ToString();
            settings f2 = new settings();
            f2.ShowDialog();
            brd.Text = br[Properties.Settings.Default.buadRate].ToString();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void versionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            about a = new about();
            a.ShowDialog();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
           
        }
    }
}
