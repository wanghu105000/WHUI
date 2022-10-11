using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHControlLib.Forms
{
  public static class WhMessageBox
    {
        /// <summary>
        /// 信息提示框
        /// </summary>
        /// <param name="MsgText">提示内容</param>
        public static void Show(string MsgText)
        {
            MsgBoxForm whMsgBoxForm = new MsgBoxForm();
            whMsgBoxForm.MessageText = MsgText;
            whMsgBoxForm.IsShowMaskFrm = true;
            whMsgBoxForm.ShowDialog();
        }

        public static void Show(string MsgText, string TitleText)
        {
            MsgBoxForm whMsgBoxForm = new MsgBoxForm();
            whMsgBoxForm.MessageText = MsgText;
            whMsgBoxForm.TitleText = TitleText;
            whMsgBoxForm.IsShowMaskFrm = true;
            whMsgBoxForm.ShowDialog();

        }

        public static void Show(string MsgText, string TitleText, bool IsShowMask)
        {
            MsgBoxForm whMsgBoxForm = new MsgBoxForm();
            whMsgBoxForm.MessageText = MsgText;
            whMsgBoxForm.TitleText = TitleText;
            whMsgBoxForm.IsShowMaskFrm = IsShowMask;
            whMsgBoxForm.ShowDialog();
        }
        
       /// <summary>
       /// 警告信息信息框
       /// </summary>
       /// <param name="MsgText">警告具体信息</param>
        public static void ShowWarningMsg(string MsgText)
        {
            MsgBoxForm whMsgBoxForm = new MsgBoxForm();
            whMsgBoxForm.MessageText = MsgText;
            whMsgBoxForm.TitleText = "警告";
            whMsgBoxForm.TitleBackColor = Color.Transparent;
            whMsgBoxForm.IsUseTwoColor = true;
            whMsgBoxForm.firstColor = Color.GreenYellow;
            whMsgBoxForm.SecondColor = Color.DarkGoldenrod;
            whMsgBoxForm.OKbutton.IsUseTwoColor = true;
            whMsgBoxForm.OKbutton.FirstFillcolor = Color.LightGoldenrodYellow;
            whMsgBoxForm.OKbutton.SecondFillcolor = Color.YellowGreen;
            whMsgBoxForm.CloseBoxShapeColor = Color.DarkGreen;
            whMsgBoxForm.CloseBoxBorderColor = Color.DarkGreen;
            whMsgBoxForm.CloseBoxSelectColor = Color.PaleVioletRed;
            whMsgBoxForm.CloseBoxShapeColor = Color.Gray;
            whMsgBoxForm.CloseBoxSelectColor = Color.BurlyWood;
           
            whMsgBoxForm.IsShowMaskFrm =true;
       
            whMsgBoxForm.ShowDialog();



        }
      /// <summary>
      /// 错误信息提示框
      /// </summary>
      /// <param name="MsgText">具体错误信息</param>
        public static void  ShowErrorMsg(String MsgText)
        {

            MsgBoxForm whMsgBoxForm = new MsgBoxForm();
            whMsgBoxForm.MessageText = MsgText;
            whMsgBoxForm.TitleText = "错误";
            whMsgBoxForm.TitleTextColor = Color.White;
            whMsgBoxForm.MsgTextLable.FontColor=Color.White;
            whMsgBoxForm.TitleBackColor = Color.Transparent;
            whMsgBoxForm.IsUseTwoColor = true;
            whMsgBoxForm.firstColor = Color.IndianRed;
            whMsgBoxForm.SecondColor = Color.Red;
            whMsgBoxForm.OKbutton.IsUseTwoColor = true;
         
            whMsgBoxForm.CloseBoxShapeColor = Color.White;
            whMsgBoxForm.CloseBoxBorderColor = Color.Snow;
            whMsgBoxForm.CloseBoxSelectColor = Color.HotPink;
  
            whMsgBoxForm.OKbutton.IsUseTwoColor= true;
            whMsgBoxForm.OKbutton.FontColor=Color.White;    
            whMsgBoxForm.OKbutton.FirstFillcolor = Color.LavenderBlush;
            whMsgBoxForm.OKbutton.SecondFillcolor = Color.Red;
            whMsgBoxForm.IsShowMaskFrm = true;

            whMsgBoxForm.ShowDialog();






        }
        /// <summary>
        /// 错误信息提示框
        /// </summary>
        /// <param name="MsgText">具体错误信息</param>
        /// <param name="TitleText">标题栏文字</param>
        /// <param name="IsShowMask">是否显示遮罩</param>
        public static void ShowErrorMsg(string MsgText, string TitleText, bool IsShowMask)
        {
            MsgBoxForm whMsgBoxForm = new MsgBoxForm();
            whMsgBoxForm.MessageText = MsgText;
            if (TitleText==null||TitleText=="")
            {
            whMsgBoxForm.TitleText = "错误";
            }
            else  whMsgBoxForm.TitleText = TitleText;

            whMsgBoxForm.TitleTextColor = Color.White;
            whMsgBoxForm.MsgTextLable.FontColor = Color.White;
            whMsgBoxForm.TitleBackColor = Color.Transparent;
            whMsgBoxForm.IsUseTwoColor = true;
            whMsgBoxForm.firstColor = Color.IndianRed;
            whMsgBoxForm.SecondColor = Color.Red;
            whMsgBoxForm.OKbutton.IsUseTwoColor = true;

            whMsgBoxForm.CloseBoxShapeColor = Color.White;
            whMsgBoxForm.CloseBoxBorderColor = Color.Snow;
            whMsgBoxForm.CloseBoxSelectColor = Color.HotPink;

            whMsgBoxForm.OKbutton.IsUseTwoColor = true;
            whMsgBoxForm.OKbutton.FontColor = Color.White;
            whMsgBoxForm.OKbutton.FirstFillcolor = Color.LavenderBlush;
            whMsgBoxForm.OKbutton.SecondFillcolor = Color.Red;
            whMsgBoxForm.IsShowMaskFrm = IsShowMask;

            whMsgBoxForm.ShowDialog();



        }

    }
}
