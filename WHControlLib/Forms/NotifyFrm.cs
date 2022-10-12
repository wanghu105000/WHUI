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
    public partial class NotifyFrm : Form
    {
        public NotifyFrm()
        {        //设置双缓冲

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.AllPaintingInWmPaint, true);

            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            this.FormBorderStyle = FormBorderStyle.None;

            InitializeComponent();
      
        }


        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        //发送消息//winuser.h 中有函数原型定义   
        [DllImportAttribute("user32.dll")]
     
        public static extern bool ReleaseCapture(); //释放鼠标捕捉winuser.h   
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
    



        int StartTop, StartLeft;
        int timer2tiem =3;
        int MoveX =10;
        int FrmCount;
        private void NotifyFrm_Load(object sender, EventArgs e)

        {
            ShowWindow(this.Handle, SW_SHOWNOACTIVATE);
            SetWindowPos(this.Handle, HWND_TOPMOST, Left, Top, Width, Height, SWP_NOACTIVATE);


            foreach (var Frm in Application.OpenForms)
            {
                if (Frm is NotifyFrm)
                {
                    FrmCount++;
                }
               
            }
            if (FrmCount>5)
            {
                FrmCount = 1;
            }

            this.Width = Screen.PrimaryScreen.WorkingArea.Width / 5;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height / 5;
           StartTop= Screen.PrimaryScreen.WorkingArea.Height - FrmCount *this.Height;
            
            
            StartLeft=Screen.PrimaryScreen.WorkingArea.Width;
            this.Location = new Point(StartLeft, StartTop);
            this.Opacity = 0;
            timer1.Interval = 15;
            timer2.Interval = 500;
            timer1.Start();
        }
        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        const int WS_EX_NOACTIVATE = 0x08000000;
        //        const int WS_CHILD = 0x40000000;
        //        CreateParams cp = base.CreateParams;
        //        cp.Style |= WS_CHILD;
        //        cp.ExStyle |= WS_EX_NOACTIVATE;
        //        return cp;
        //    }
        //}

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (timer2tiem>0)
            {
            timer2tiem--;
            }
            else
            {
                this.Close();
            }


        }

        private void NotifyFrm_Shown(object sender, EventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            MoveX += (int)(MoveX/5);
            this.Opacity += 0.02;
            int MYNowX = StartLeft - MoveX;
            if (MoveX<=this.Width)
            {
              this.Location=new Point(MYNowX, StartTop);
            }
            else
            {
                MoveX = 0;
                this.Opacity = 1;
                timer1.Stop();
                timer2.Start();
            }
            
        }

        //////////////////————————————————

    }
}
