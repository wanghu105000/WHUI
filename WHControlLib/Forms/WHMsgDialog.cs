using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHControlLib.Forms
{
public static class WHMsgDialog
    {
        public static void Show(string MsgText)
        {
            MsgBoxForm whMsgBoxForm = new MsgBoxForm();
            whMsgBoxForm.MessageText = MsgText;
            whMsgBoxForm.IsShowMaskFrm = true;
            whMsgBoxForm.ShowDialog();
        } 
        public static void Show(string MsgText,string TitleText)
        {
            MsgBoxForm whMsgBoxForm = new MsgBoxForm();
            whMsgBoxForm.MessageText = MsgText;
            whMsgBoxForm.TitleText = TitleText;
            whMsgBoxForm.IsShowMaskFrm = true;
            whMsgBoxForm.ShowDialog();
        }
        public static void Show(string MsgText, string TitleText,bool IsShowMask)
        {
            MsgBoxForm whMsgBoxForm = new MsgBoxForm();
            whMsgBoxForm.MessageText = MsgText;
            whMsgBoxForm.TitleText = TitleText;
            whMsgBoxForm.IsShowMaskFrm = IsShowMask;
            whMsgBoxForm.ShowDialog();
        }
        public static void ShowErroMsg(string MsgText, string TitleText, bool IsShowMask)
        {
            MsgBoxForm whMsgBoxForm = new MsgBoxForm();
            whMsgBoxForm.MessageText = MsgText;
            whMsgBoxForm.TitleText = TitleText;
            whMsgBoxForm.IsShowMaskFrm = false;
            whMsgBoxForm.FormBorderColor=Color.Red;
            whMsgBoxForm.firstColor = Color.Wheat;
            whMsgBoxForm.SecondColor = Color.OrangeRed;
            
            whMsgBoxForm.ShowDialog();
        }



        //  、、、、、、、、、、、、、、、、、、、、
    }
}