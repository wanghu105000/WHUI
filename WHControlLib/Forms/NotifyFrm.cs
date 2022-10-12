using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WHControlLib.Forms
{
    public partial class NotifyFrm : BaseDialogFormcs
    {
        public NotifyFrm()
        {
            InitializeComponent();
            this.ShowInTaskbar=false;
        }

        public string MsgText
        {
            get { return MsgTxtLable.Text; }
            set
            {
                if (value != null)
                {
                    MsgTxtLable.Text = value;
                }
            }

        }
        public int StartTop { get; set; }
        public int StartLeft { get; set; }

        //全局 参数定义
        //int StartTop, StartLeft;
      /// <summary>
      /// 窗体保持时间6秒
      /// </summary>
        int timer2tiem = 6;
        int MoveX = 10;
        
        public static int NotifyFrmCount;
    


        //********************************************


        #region 无焦点窗体


        const Int32 HWND_TOPMOST = -1;
        const Int32 SWP_NOACTIVATE = 0x0010;
        const Int32 SW_SHOWNOACTIVATE = 4;
        [DllImport("user32.dll")]
        protected static extern bool ShowWindow(IntPtr hWnd, Int32 flags);
        [DllImport("user32.dll")]
        protected static extern bool SetWindowPos(IntPtr hWnd,
          Int32 hWndInsertAfter, Int32 X, Int32 Y, Int32 cx, Int32 cy, uint uFlags);
        //// Show the window without activating it.
        //ShowWindow(this.Handle, SW_SHOWNOACTIVATE);

        //// Equivalent to setting TopMost = true, except don't activate the window.
        //SetWindowPos(this.Handle, HWND_TOPMOST, Left, Top, Width, Height, SWP_NOACTIVATE);



        #endregion
        private void timer1_Tick(object sender, EventArgs e)
        {
            MoveX += (int)(MoveX / 5);
            this.Opacity += 0.02;
            int MYNowX = StartLeft - MoveX;
            if (MoveX <= this.Width)
            {
                this.Location = new Point(MYNowX, StartTop);
            }
            else
            {
                MoveX = 0;
                this.Opacity = 1;
                this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width, StartTop);
   
                timer1.Stop();
                timer2.Start();
            }

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (timer2tiem > 0)
            {
                timer2tiem--;
            }
            else
            {   
                this.Close();
            }
        }

        private void nowtime_Tick(object sender, EventArgs e)
        {
            timeLable.Text = DateTime.Now.ToString("f");
        }

        private void NotifyFrm_Paint(object sender, PaintEventArgs e)
        {
            BeginLoadIniss();

        }

        private void NotifyFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            WHNotify.NotifyStartY[(int)this.Tag] = false;
        }

        public void BeginLoadIniss()
        {
            timeLable.AutoSize = false;
            timeLable.Height = this.Height / 6;
            timeLable.Width = this.Width;
            timeLable.Top = this.Height - timeLable.Height;
            timeLable.Left = (int)this.DrawRct.X + 15;
            timeLable.Text = DateTime.Now.ToString("f");
           
            MsgTxtLable.Height = this.Height - this.TitleRect.Height - timeLable.Height - MsgTxtLable.BorderWidth * 2;
            MsgTxtLable.Width = this.Width - 10;
            MsgTxtLable.Top = this.TitleRect.Height + 2;
            MsgTxtLable.Left = (int)this.DrawRct.X;

     



        }
        private void NotifyFrm_Load(object sender, EventArgs e)
        {
              //设置窗口为置顶，无焦点窗口
            SetWindowPos(this.Handle, HWND_TOPMOST, Left, Top, Width, Height, SWP_NOACTIVATE);
            // //查找所有通知窗口
            // foreach (var Frm in Application.OpenForms)
            // {
            //     if (Frm is NotifyFrm)
            //     {
            //         FrmCount++;
            //     }

            // }
            //////通知窗口数量最多5个
            // if (FrmCount > 5)
            // {
            //     FrmCount = 1;
            // }

            //StartTop = Screen.PrimaryScreen.WorkingArea.Height - FrmCount * this.Height;
            StartLeft = Screen.PrimaryScreen.WorkingArea.Width;
            this.Location = new Point(StartLeft, StartTop);
            //this.Tag = this.Location.Y;
            this.Opacity = 0;
            ////移动间隔时间
            timer1.Interval = 15;
           //保持时间1秒的倍数
            timer2.Interval = 1000;

            timer1.Start();
            nowtime.Start();



        }
    }
}
