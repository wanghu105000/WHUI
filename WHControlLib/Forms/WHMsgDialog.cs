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
     /// <summary>
     /// 信息提示框
     /// </summary>
     /// <param name="MsgText">信息内容</param>
     /// <returns></returns>
        public static DialogResult ShowMsgDialog(string MsgText)
        {
            MsgDialogFrm msgDialog = new MsgDialogFrm();

            msgDialog.MessageText = MsgText;
            msgDialog.IsShowMaskFrm = false;
            msgDialog.ShowDialog();
            return MyDialogResult;
        }
    /// <summary>
    /// 警告信息对话框
    /// </summary>
    /// <param name="MsgText">警告信息内容</param>
    /// <param name="TitleText">标题栏文字，可以不输入null 或“”</param>
    /// <returns></returns>
     public static DialogResult ShowWarningDlg(String MsgText,string TitleText)
        {
            MsgDialogFrm msgDialog = new MsgDialogFrm();
            if (TitleText == null || TitleText == "")
            {
                msgDialog.TitleText = "警告";
            }
            else msgDialog.TitleText = TitleText;
            msgDialog.TitleBackColor = Color.Transparent;
            msgDialog.IsUseTwoColor = true;
            msgDialog.firstColor = Color.GreenYellow;
            msgDialog.SecondColor = Color.DarkGoldenrod;
            msgDialog.OKbutton.FirstFillcolor = Color.LightGoldenrodYellow;
            msgDialog.OKbutton.SecondFillcolor = Color.YellowGreen;
            msgDialog.Cannelbutton.FirstFillcolor = msgDialog.OKbutton.FirstFillcolor;
            msgDialog.Cannelbutton.SecondFillcolor = msgDialog.OKbutton.SecondFillcolor;
            
            msgDialog.CloseBoxShapeColor = Color.Gray;
            msgDialog.CloseBoxSelectColor = Color.BurlyWood;
            msgDialog.MessageText = MsgText;
            msgDialog.IsShowMaskFrm = false;
            msgDialog.MsgTextLable.MyImage = Resource1.WaringYellow;
            msgDialog.ShowDialog();

            return MyDialogResult;
        }


 public static  DialogResult  ShowErrorDlg(string MsgText, string TitleText)
        {
            MsgDialogFrm msgDialog = new MsgDialogFrm();
           
            
            if (TitleText == null || TitleText == "")
            {
                msgDialog.TitleText = "错误";
            }
            else msgDialog.TitleText = TitleText;

            msgDialog.TitleTextColor = Color.Snow;
            msgDialog.TitleBackColor = Color.Transparent;
            msgDialog.IsUseTwoColor = true;
            msgDialog.firstColor = Color.LightPink;
            msgDialog.SecondColor = Color.Red;
            msgDialog.OKbutton.FirstFillcolor = Color.LemonChiffon;
            msgDialog.OKbutton.SecondFillcolor = Color.Tomato;
            msgDialog.Cannelbutton.FirstFillcolor = msgDialog.OKbutton.FirstFillcolor;
            msgDialog.Cannelbutton.SecondFillcolor = msgDialog.OKbutton.SecondFillcolor;

            msgDialog.CloseBoxShapeColor = Color.White;
            msgDialog.CloseBoxBorderColor = Color.Snow;
            msgDialog.CloseBoxSelectColor = Color.HotPink;


            msgDialog.MessageText = MsgText;
            msgDialog.IsShowMaskFrm = false;
            msgDialog.MsgTextLable.FontColor= Color.White;  
            msgDialog.MsgTextLable.MyImage = Resource1.ErrorRed;
            msgDialog.ShowDialog();

            return MyDialogResult;


        }

        //  、、、、、、、、、、、、、、、、、、、、
    }
}