using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GenUAFromBilds
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public Random rand = new Random();
        private void button1_Click(object sender, EventArgs e)
        {
            List<string> uas = new List<string>(File.ReadAllLines("ua.txt"));
            List<string> bilds = new List<string>(File.ReadAllLines("bilds.txt"));
            foreach (var ua in uas)
            {
                //Dalvik/1.6.0 (Linux; U; Android 4.1.2; C5303 Build/12.0.A.1.257) [FBAN/FB4A;FBAV/163.0.0.43.91;FBPN/com.facebook.katana;FBLC/en_US;FBBV/96845997;FBCR/null;FBMF/Sony;FBBD/sony;FBDV/C5303;FBSV/4.1.2;FBCA/armeabi-v7a:armeabi;FBDM/{density=3.0,width=1196,height=720};FB_FW/1;]
                string bild = bilds[rand.Next(0, bilds.Count)];
                string ver = bild.Split(';')[0];
                string variant = bild.Split(';')[1];
                string newua = ua.Replace("163.0.0.43.91", ver);
                newua = newua.Replace("96845997", variant);
                File.AppendAllText("newua.txt", newua + Environment.NewLine);
                richTextBox1.AppendText(newua + Environment.NewLine);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<string> infile = new List<string>(File.ReadAllLines("in.txt"));
            List<string> uas = new List<string>(File.ReadAllLines("newua.txt"));
            foreach (var acc in infile)
            {
                string acc_without_proxy = acc.Split('|')[0];
                int countDelimetr = acc_without_proxy.Split(';').Count();
               // richTextBox1.AppendText(countDelimetr.ToString() + Environment.NewLine);
                /*
7
8
25
24
                 */
                string newacc_without_proxy = "";
                switch (countDelimetr)
                {
                    case 7:
                        //richTextBox1.AppendText(acc_without_proxy);
                        newacc_without_proxy = acc_without_proxy + ";;" + uas[rand.Next(0, uas.Count)];
                        break;
                    case 8:
                        newacc_without_proxy = acc_without_proxy + ";" + uas[rand.Next(0, uas.Count)];
                        break;
                    case 24:
                        newacc_without_proxy = acc_without_proxy.Replace(";Dalvik", ";;Dalvik");
                        break;
                    case 25:
                        newacc_without_proxy = acc_without_proxy;
                        break;
                }
                richTextBox1.AppendText(newacc_without_proxy + Environment.NewLine);
                File.AppendAllText("out.txt", newacc_without_proxy + Environment.NewLine);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<string> infile = new List<string>(File.ReadAllLines("out.txt"));
            foreach (var acc in infile)
            {
                string acc_without_proxy = acc.Split('|')[0];
                int countDelimetr = acc_without_proxy.Split(';').Count();
                richTextBox1.AppendText(countDelimetr.ToString() + Environment.NewLine);
            }
        }
    }
}
