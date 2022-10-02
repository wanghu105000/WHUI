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
          MsgBoxForm whMsgBoxForm = new MsgBoxForm();
            whMsgBoxForm.MessageText = "你做的很好";
            whMsgBoxForm.IsShowMaskFrm = true;
            whMsgBoxForm.ShowDialog();   
        }
    }
}
