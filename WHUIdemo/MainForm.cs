using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WHControlLib.Forms;
using WHUIdemo.Forms;

namespace WHUIdemo
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            testForm test = new testForm();
            test.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WHMsgDialog.Show("简单的很上课时间都是山东省克劳德萨里的DSAKL DDSDASL阿达SDSA DKSDKSADK 123");
            WHMsgDialog.ShowErroMsg("写错了","错误",true);
        }
    }
}
