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
    public partial class NotifyForm :BaseDialogFormcs
    {
        public NotifyForm()
        {
            InitializeComponent();
        }
        //全局 参数定义
        int StartTop, StartLeft;
        int timer2tiem = 13;
        int MoveX = 10;
        int FrmCount;
        int FrmStartX,FrmStartY;
        int OffsetX,OffsetY;
        int OldMouseX,
            OldMouseY;

        bool BeginMove;
     

        //********************************************

        //private string _msgText;

        public string MsgText
        {
            get { return MsgTxtLable.Text; }
            set
            {
                if (value!=null)
                {
                 MsgTxtLable.Text=value;
                }
                }
            
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private extern static IntPtr SetActiveWindow(IntPtr handle);
        private const int WM_ACTIVATE = 0x006;
        private const int WM_ACTIVATEAPP = 0x01C;
        private const int WM_NCACTIVATE = 0x086;
        private const int WA_INACTIVE = 0;
        private const int WM_MOUSEACTIVATE = 0x21;
        private const int MA_NOACTIVATE = 3;





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
                this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width-this.Width , StartTop);
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
        public void BeginLoadIni()
        {
            timeLable.AutoSize = false;
            timeLable.Height = this.Height / 6;
            timeLable.Width = this.Width ;
            timeLable.Top = this.Height - timeLable.Height;
            timeLable.Left =(int)this.DrawRct.X+15;
           
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

        private void NotifyForm_MouseLeave(object sender, EventArgs e)
        {
           
        }

        private void titlepicturebox_MouseMove(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                if (BeginMove == false)
                {
                    OldMouseX = e.X;
                    OldMouseY = e.Y;
                    return;
                }
                else
                {
                    OffsetX = e.X - OldMouseX;
                    OffsetY = e.Y - OldMouseY;
                    this.Top += OldMouseY;
                    this.Left += OldMouseX;
                    OldMouseX = e.X;
                    OldMouseY = e.Y;

                }




            }
            else
            {
                BeginMove = false;
            }




        }

        private void titlepicturebox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button==MouseButtons.Left)
            {
                BeginMove = true;
            }
        }

        private void whButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timeLable_Click(object sender, EventArgs e)
        {

        }

        private void nowtime_Tick(object sender, EventArgs e)
        {
            timeLable.Text = DateTime.Now.ToString("f");
        }

        private void NotifyForm_MouseDown(object sender, MouseEventArgs e)
        {
       
           
            //if ( e.Button==MouseButtons.Left )
            //{
            //    if (BeginMove==false)
            //    {
            //      OldMouseX = e.X;
            //       OldMouseY= e.Y;
            //        return;
            //    }
            //    else
            //    {
            //        OffsetX = e.X - OldMouseX;
            //        OffsetY = e.Y - OldMouseY;
            //        this.Top += OldMouseY;
            //        this.Left += OldMouseX;
            //        OldMouseX = e.X;
            //        OldMouseY = e.Y;

            //    }
                
               


            //}
            //else
            //{
            //    BeginMove=false;
            //}       
                    
      }

        private void NotifyForm_Load(object sender, EventArgs e)
        {
            BeginLoadIni();


            //设置窗口为无焦点的顶层窗口
            //ShowWindow(this.Handle, SW_SHOWNOACTIVATE);
            SetWindowPos(this.Handle, HWND_TOPMOST, Left, Top, Width, Height, SWP_NOACTIVATE);


            foreach (var Frm in Application.OpenForms)
            {
                if (Frm is NotifyForm)
                {
                    FrmCount++;
                }

            }
            if (FrmCount > 5)
            {
                FrmCount = 1;
            }

            //this.Width = Screen.PrimaryScreen.WorkingArea.Width / 5;
            //this.Height = Screen.PrimaryScreen.WorkingArea.Height / 5;
            StartTop = Screen.PrimaryScreen.WorkingArea.Height - FrmCount * this.Height;


            StartLeft = Screen.PrimaryScreen.WorkingArea.Width;
            this.Location = new Point(StartLeft, StartTop);
         
            
            this.Opacity = 0;
            timer1.Interval = 15;
            timer2.Interval = 500;
         
            timer1.Start();
            nowtime.Start();
        }




     


        //____________________________________________ 

    }
}
