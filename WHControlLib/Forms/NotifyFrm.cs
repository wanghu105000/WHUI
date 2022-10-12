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


        //全局 参数定义
        int StartTop, StartLeft;
        int timer2tiem = 13;
        int MoveX = 10;
        int FrmCount=0;
        int FrmStartX, FrmStartY;
        int OffsetX, OffsetY;
        int OldMouseX,
            OldMouseY;

        bool BeginMove;


        //********************************************


        #region 无焦点窗体


        const Int32 HWND_TOPMOST = -1;
        const Int32 SWP_NOACTIVATE = 0x0010;
        const Int32 SW_SHOWNOACTIVATE = 4;

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
                FrmStartX = this.Left;
                FrmStartY = this.Top;
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
        public void BeginLoadIni()
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

            //titlepicturebox.Width = this.TitleRect.Width;
            //titlepicturebox.Height = this.TitleRect.Height;
            //titlepicturebox.Top= this.TitleRect.Top;
            //titlepicturebox.Left = this.TitleRect.Left;



        }
        private void NotifyFrm_Load(object sender, EventArgs e)
        {
        
            SetWindowPos(this.Handle, HWND_TOPMOST, Left, Top, Width, Height, SWP_NOACTIVATE);

            foreach (var Frm in Application.OpenForms)
            {
                if (Frm is NotifyFrm)
                {
                    FrmCount++;
                }

            }
            if (FrmCount > 5)
            {
                FrmCount = 1;
            }

            StartTop = Screen.PrimaryScreen.WorkingArea.Height - FrmCount * this.Height;


            StartLeft = Screen.PrimaryScreen.WorkingArea.Width;
            this.Location = new Point(StartLeft, StartTop);


            this.Opacity = 0;
            timer1.Interval = 15;
            timer2.Interval = 500;

            timer1.Start();
            nowtime.Start();



        }
    }
}
