using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//**** 实现了消息通知存储当有5个通知窗口后就会存储 待显示窗体并待机显示出来自动处理是否有Bug需要自己测试***
//*****如果不想 使用 内存 存储功能，自己修改源代码 ，目前不做修改。******



namespace WHControlLib.Forms
{
  public static class WHNotify
    {
       //静态类开始初始化
        static WHNotify()
        {     
            for (int i = 1; i <= LimtNotifyFrmCount; i++)
            {
            NotifyStartY.Add(i,false);
              
            }

                tm1.Enabled = false;
                tm1.Interval = 200;
                tm1.Tick += Tm1_Tick;

        }
        /// <summary>
        /// 允许最多显示多少个通知窗口
        /// </summary>
        public  const int LimtNotifyFrmCount=6;
     
      /// <summary>
      /// 存储待显示的通知窗体的集合
      /// </summary>
        public static List<NotifyFrm> NotifyFrmList=new List<NotifyFrm>();
        /// <summary>
        /// 是否有可供显示位置的字典
        /// </summary>
        public static Dictionary<int, bool> NotifyStartY = new Dictionary<int, bool>();
        /// <summary>
        /// 自动取出待显示窗体的定时器
        /// </summary>
        public static Timer tm1=new Timer();
       /// <summary>
       /// 取出带显示 通知窗体的过程
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private static void Tm1_Tick(object sender, EventArgs e)
        {
            if (NotifyFrmList.Count==0)
            {
                tm1.Stop();
                return;

            }
            else
            {     foreach (var NotFrm in NotifyFrmList)
            {
                    for (int i = 1; i <= LimtNotifyFrmCount; i++)
                    {
                        if (NotifyStartY[i] == false)
                        {
                            NotifyStartY[i] = true;
                            //FindStart = true;
                            //return Screen.PrimaryScreen.WorkingArea.Height - i * H;
                            NotFrm.StartTop= Screen.PrimaryScreen.WorkingArea.Height - i *NotFrm.Height;
                            NotifyFrmList.Remove(NotFrm);
                            NotFrm.Tag = i;
                            NotFrm.Show();
                            return;

                        }
                    }
            }

            }
       

            //throw new NotImplementedException();
        }

   /// <summary>
   /// 得到显示的Y坐标，如果不能显示就储存进内存
   /// </summary>
   /// <param name="frm">要显示的通知窗体</param>
   /// <returns></returns>
        public static int GetStartTop(NotifyFrm frm)
        {
          
            int H = frm.Height;
             int NotifyFrmCount=0;
            //查找所有通知窗口
            foreach (var Frm in Application.OpenForms)
            {
               
                if (Frm is NotifyFrm)
                {
                    NotifyFrmCount++;
                }

            }
            ////通知窗口数量最多LimtNotifyFrmCount个
            if (NotifyFrmCount >= LimtNotifyFrmCount)
            {
                //NotifyFrmCount = 1;
                NotifyFrmList.Add(frm);
                return -1;
            }

                for (int i = 1; i <= LimtNotifyFrmCount; i++)
                {
                    if (NotifyStartY[i]==false)
                    {
                     NotifyStartY[i] = true;
                  //窗体 储存 自己显示的位置，以后关闭窗体是要 设置为本位置未使用
                    frm.Tag = i;
                    return Screen.PrimaryScreen.WorkingArea.Height - i  * H;
                   
                    }   }

            
                    return -1;




        }
        private static void ShowNotifyWindow(string MsgTxt, NotifyFrm notifyfrm)
        {
              notifyfrm.MsgText = MsgTxt;
            int starttop = GetStartTop(notifyfrm);
            if (starttop < 0)
            {
                tm1.Start();
                return;
            }
            else
            {
                notifyfrm.StartTop = starttop;
             
                notifyfrm.Show();
            }
        }

        /// <summary>
        /// 显示普通消息通知
        /// </summary>
        /// <param name="MsgTxt"></param>
        public static void ShowMsgNotify(string MsgTxt)
        {

            NotifyFrm notifyfrm = new NotifyFrm();
            ShowNotifyWindow(MsgTxt, notifyfrm);

        }
        public static void ShowMsgNotify(string MsgTxt,string titleTxt)
        {

            NotifyFrm notifyfrm = new NotifyFrm();
            if (titleTxt==null||titleTxt=="")
            {
                notifyfrm.TitleText = "消息通知";
            }
            else
                notifyfrm.TitleText=titleTxt;
            notifyfrm.TitleTextColor = Color.Black;
            notifyfrm.firstColor = Color.LightBlue;
            notifyfrm.SecondColor = Color.DarkBlue;

            ShowNotifyWindow(MsgTxt, notifyfrm);

        }

        public static void ShowErrorNotify(string MsgTxt, string titleTxt)
        {

            NotifyFrm notifyfrm = new NotifyFrm();
            if (titleTxt == null || titleTxt == "")
            {
             notifyfrm.TitleText = "错误通知";
            }
            else
                notifyfrm.TitleText = titleTxt;

          
            notifyfrm.TitleTextColor = Color.Black;
            notifyfrm.firstColor = Color.MistyRose;
            notifyfrm.SecondColor = Color.Red;
            ShowNotifyWindow(MsgTxt, notifyfrm);

        }
        public static void ShowWarningNotify(string MsgTxt, string titleTxt)

        {

            NotifyFrm notifyfrm = new NotifyFrm();

            if (titleTxt == null || titleTxt == "")
            {
              notifyfrm.TitleText = "警告通知";
            }  else
            notifyfrm.TitleText = titleTxt;

        
            notifyfrm.TitleTextColor= Color.Black;
            notifyfrm.timeLable.ForeColor= Color.Black;
            notifyfrm.MsgTxtLable.FontColor = Color.Black;
            notifyfrm.firstColor = Color.LightYellow;
            notifyfrm.SecondColor = Color.Yellow;
            ShowNotifyWindow(MsgTxt, notifyfrm);

        }
        public static void ShowSucessNotify(string MsgTxt, string titleTxt)

        {

            NotifyFrm notifyfrm = new NotifyFrm();
            if (titleTxt == null || titleTxt == "")
            {
                notifyfrm.TitleText = "成功通知";
            }
            else
                notifyfrm.TitleText = titleTxt;

          
            notifyfrm.TitleTextColor = Color.Black;
            notifyfrm.timeLable.ForeColor = Color.Yellow;
            notifyfrm.MsgTxtLable.FontColor = Color.Black;
            notifyfrm.firstColor = Color.PaleGreen;
            notifyfrm.SecondColor = Color.Green;
            ShowNotifyWindow(MsgTxt, notifyfrm);

        }

        //_____________________________________
    }
}
