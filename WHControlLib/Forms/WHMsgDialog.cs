using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WHControlLib.Forms
{
public static class WHMsgDialog
    {
        public static DialogResult MyDialogResult;
      
        public static DialogResult ShowMsgDialog(string MsgText)
        {
            MsgDialogFrm msgDialog = new MsgDialogFrm();

            msgDialog.MessageText = MsgText;
            msgDialog.IsShowMaskFrm = true;
            msgDialog.ShowDialog();
            return MyDialogResult;
        }
       


        //  、、、、、、、、、、、、、、、、、、、、
    }
}