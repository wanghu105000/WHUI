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
            WhMessageBox.ShowWarningMsg("警告提示信息");

            WhMessageBox.ShowErrorMsg("错误 提示信息");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CtrlTestFrm ctrlfrm = new CtrlTestFrm();
            ctrlfrm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            WHMsgDialog.ShowMsgDialog("姑姑又故意故意䧜预估一个预估一个 这是信息对话框");
            MessageBox.Show(WHMsgDialog.MyDialogResult.ToString());
            WHMsgDialog.ShowWarningDlg("这是警告信息",null);
            WHMsgDialog.ShowErrorDlg("这是错误对话框",null);
            MessageBox.Show(WHMsgDialog.MyDialogResult.ToString());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //NotifyFrm nofrm = new NotifyFrm();
            //nofrm.Show();
            WHNotify.ShowMsgNotify("这是消息通知");
            WHNotify.ShowErrorNotify("这是错误消息的通知信息","");
            WHNotify.ShowWarningNotify("这是警告通知", null);
            WHNotify.ShowSucessNotify("现在是成功消息通知",null);
            WHNotify.ShowMsgNotify( "好消息通知你","好消息");
        }
    }
}
