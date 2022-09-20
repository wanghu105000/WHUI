using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WHControlLib.Forms
{
    public partial class BaseDialogFormcs : Form
    {

        private void InitializeStyles()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.Selectable, false);
            UpdateStyles();
        }



        public BaseDialogFormcs()
        {//设置双缓冲

            //this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
            //         ControlStyles.ResizeRedraw |
            //         ControlStyles.AllPaintingInWmPaint, true);
            InitializeStyles();
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;

            //this.TopMost = true;
            this.StartPosition = FormStartPosition.CenterScreen;

        }
        //************全局参数定义***************
        Rectangle MyRect = new Rectangle();
        RectangleF DrawRct = new RectangleF();
        Rectangle CloseBoxRect = new Rectangle();
        //**************************************
        #region 无边框窗体拖动

        [DllImport("user32.dll")]//拖动无窗体的控件
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;
        public const int WM_VSCROLL = 0x0115;
        public const int WM_HSCROLL = 0x0114;
        #endregion


        #region 属性字段定义
        public new bool AutoScroll
        {
            get => base.AutoScroll;
            set => base.AutoScroll = false;
        }

        private bool _isDrawFormBorder = true;
        [Category("A我的"), Description("是否显示窗体边框，默认，true，显示"), Browsable(true)]
        public bool IsDrawFormBorder
        {
            get { return _isDrawFormBorder; }
            set { _isDrawFormBorder = value; Invalidate(); }
        }

        private Color _formBorderColor = Color.Black;
        [Category("A我的"), Description("窗体边框颜色，默认，true，显示"), Browsable(true)]
        public Color FormBorderColor
        {
            get { return _formBorderColor; }
            set { _formBorderColor = value; Invalidate(); }
        }
        private float _formBorderWidth = 2.0f  ;
          
        [Category("A我的"), Description("窗体边框宽度，默认，2.0f "), Browsable(true)]
        public float FormBorderWidth
        {
            get { return _formBorderWidth; }
            set
            {
                if (value < 1)
                {
                    _formBorderWidth = 1;
                }
                else
                    _formBorderWidth = value;
                Invalidate();
            }
        }

        private float _radius = 8.0f;
        [Category("A我的"), Description("圆角矩形窗体的四角圆弧的程度，默认，8.0f，数字越大圆弧越小不能小于1.0f"), Browsable(true)]
        public float Radius
        {
            get { return _radius; }
            set
            {
                if (value < 1.0f)
                {
                    _radius = 1.0f;
                }
                _radius = value; Invalidate();
            }

        }

        private bool _isUseTwoColor = false;
        [Category("A我的"), Description("窗体是否使用上下两种渐变颜色，默认，false "), Browsable(true)]
        public bool IsUseTwoColor
        {
            get { return _isUseTwoColor; }
            set { _isUseTwoColor = value; Invalidate(); }
        }

        private Color _firstColor = Color.SkyBlue;
        [Category("A我的"), Description("窗体第一种颜色主颜色不起用第二种颜色时为背景色，默认，蓝色 "), Browsable(true)]
        public Color firstColor
        {
            get { return _firstColor; }
            set { _firstColor = value; Invalidate(); }
        }

        private Color _secondColor = Color.DarkBlue;
        [Category("A我的"), Description("窗体第二种背景色，默认，深蓝色 "), Browsable(true)]
        public Color SecondColor
        {
            get { return _secondColor; }
            set { _secondColor = value; Invalidate(); }
        }
        //***********有关标题栏属性定义***开始***********************
        private int _boxJG=10;
        [Category("A我的标题栏"), Description("关闭按钮距离窗体左边的距离，默认，10 "), Browsable(true)]
        public int BoxJG
        {
            get { return _boxJG; }
            set { _boxJG= value; }
        }

        private Color _titleTextColor = Color.Black;
        [Category("A我的标题栏"), Description("标题栏文本的颜色，默认，黑色 "), Browsable(true)]
        public Color TitleTextColor
        {
            get { return _titleTextColor; }
            set { _titleTextColor = value; Invalidate(); }
        }

        private Font _titleTextFont;
        [Category("A我的标题栏"), Description("标题栏显示文字字体，默认，当前窗体默认字体 "), Browsable(true)]
        public Font TitleTextFont
        {
            get { return _titleTextFont; }
            set { _titleTextFont = value; Invalidate(); }
        }

        private int _titleHeight = 15;
        [Category("A我的标题栏"), Description("标题栏的高度像素，默认，窗体高度的15分之一 "), Browsable(true)]
        public int TitleHeight
        {
            get { return _titleHeight; }
            set
            {
                if (value < 0)
                {
                    _titleHeight = 40;
                }
                _titleHeight = value; Invalidate();
            }
        }

        private Color _closeBoxBackColor = Color.Transparent;
        [Category("A我的标题栏"), Description("关闭按钮背景色，默认，透明色 "), Browsable(true)]
        public Color CloseBoxBackColor
        {
            get { return _closeBoxBackColor; }
            set { _closeBoxBackColor = value; Invalidate(); }
        }

        private Color _closeBoxSelectColor = Color.Red;
        [Category("A我的标题栏"), Description("关闭按钮选中后的色，默认，红色 "), Browsable(true)]
        public Color CloseBoxSelectColor
        {
            get { return _closeBoxSelectColor; }
            set { _closeBoxSelectColor = value; }
        }
        private Color _closeBoxShapeColor = Color.White;
        [Category("A我的标题栏"), Description("标题栏按钮形状的颜色，默认，白色 "), Browsable(true)]
        public Color CloseBoxShapeColor
        {
            get { return _closeBoxShapeColor; }
            set { _closeBoxShapeColor = value; Invalidate(); }
        }

        #endregion

        protected override void WndProc(ref Message m)
        {
        
            switch (m.Msg)
            {
                case 0x0201: //截获鼠标左键按下的消息实现鼠标拖到窗体移动 
              
                    ReleaseCapture();
                    SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
                    base.WndProc(ref m);
              
                    break;

                default:
                    base.WndProc(ref m);
                    break;
            }


        }




        void beforePainIni()
        {
            this.Width = Screen.PrimaryScreen.WorkingArea.Width / 5;
            this.Height = this.Width / 3 * 2;
            MyRect = this.ClientRectangle;

        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            beforePainIni();
            Graphics Myg = e.Graphics;
            Myg.SmoothingMode = SmoothingMode.AntiAlias;
            Myg.CompositingQuality = CompositingQuality.HighQuality;
            Myg.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //**************资源申请********************




            DrawRoundRctForm(MyRect, Myg);

        }


        void DrawRoundRctForm(Rectangle Myrect, Graphics Myg)
        {

        
            DrawRct.X = Myrect.X+FormBorderWidth/2;
            DrawRct.Y = Myrect.Y+FormBorderWidth/2;
            DrawRct.Width = Myrect.Width-FormBorderWidth ;
            DrawRct.Height = Myrect.Height-FormBorderWidth ;
            using (SolidBrush backBrush=new SolidBrush(Color.Transparent))
            {
               Myg.FillRectangle(backBrush, Myrect);

            }

            GraphicsPath RoundPath = new GraphicsPath();

            RoundPath = DrawHelper.GetFillRoundRectFPath(DrawRct, FormBorderWidth, Radius);
          
            if (IsUseTwoColor)
            {
                //RectangleF topRct = new RectangleF();
                //RectangleF BottomRct = new RectangleF();
                //topRct.X = MyRect.X;
                //topRct.Y = MyRect.Y;
                //topRct.Width = MyRect.Width;
                //topRct.Height = MyRect.Height / 3;
                //BottomRct.X = MyRect.X;
                //BottomRct.Y = MyRect.Y + topRct.Height;
                //BottomRct.Width = MyRect.Width;
                //BottomRct.Height = MyRect.Height - topRct.Height;
                using (LinearGradientBrush LinerBrush = new LinearGradientBrush(DrawRct, firstColor, SecondColor, LinearGradientMode.Vertical))
                {
                    Myg.FillPath(LinerBrush, RoundPath);
                
                }

            }
            else
            {
                this.BackColor = firstColor;
            }
            if (IsDrawFormBorder)
            {
                using (Pen formBodyPen=new Pen(FormBorderColor,FormBorderWidth))
                {
                     GraphicsPath formbodypath=new GraphicsPath();
                    formbodypath= DrawHelper.GetFillRoundRectFPath(DrawRct, FormBorderWidth, Radius);
                    Myg.DrawPath(formBodyPen, formbodypath);

                }
            }

            GraphicsPath formPath = new GraphicsPath();
            formPath = DrawHelper.GetFillRoundRectFPath(Myrect, FormBorderWidth, Radius);
            Region Formreg = new Region(formPath);
            this.Region = Formreg;


            //释放资源



        }

        /// <summary>
        /// 画出关闭按钮
        /// </summary>
        /// <param name="TitleRect">要画在那个标题区域</param>
        /// <param name="Myg">在那个设备上下文上面</param>
        /// <param name="TitleBoxBackColor">按钮背景色</param>
        /// <param name="MaxMinSelectColor">按钮选中后的背景色</param>
        /// <param name="TitleBoxShapeColor">按钮内部图形的颜色</param>
        void CloseBoxDraw(Rectangle TitleRect, Graphics Myg, Color CloseBoxBackColor, Color ColseBoxSelectColor, Color TitleBoxShapeColor)
        {
            SolidBrush TitleBoxBrush = new SolidBrush(CloseBoxBackColor);
            SolidBrush TitleBoxSelectBrush = new SolidBrush(ColseBoxSelectColor);
            Pen TitleBoxPen = new Pen(TitleBoxShapeColor, 2);
            int Boxhight = TitleHeight;
            int wbl = TitleHeight / 2;
            int hbl = wbl;

            //定义 关闭按钮范围

            if (IsDrawFormBorder)
            {
                CloseBoxRect.X = TitleRect.X + TitleRect.Width - Boxhight - FormBorderWidth - BoxJG;

            }
            else
            {
                CloseBoxRect.X = TitleRect.X + TitleRect.Width - Boxhight - BoxJG;

            }


            CloseBoxRect.Y = TitleRect.Y;
            CloseBoxRect.Width = Boxhight;
            CloseBoxRect.Height = Boxhight;
            Point CloseBoxlefttopP = new Point(CloseBoxRect.X + wbl / 2, CloseBoxRect.Y + hbl / 2);
            Point CloseBoxRighttopP = new Point(CloseBoxlefttopP.X + wbl, CloseBoxlefttopP.Y);
            Point CloseBoxLeftbuttomP = new Point(CloseBoxlefttopP.X, CloseBoxlefttopP.Y + hbl);
            Point CloseBoxRightbuttomP = new Point(CloseBoxlefttopP.X + wbl, CloseBoxlefttopP.Y + hbl);
            if (InMouseCloseBoxRect)
            {
                Myg.FillRectangle(TitleBoxSelectBrush, CloseBoxRect);

            }
            else
                Myg.FillRectangle(TitleBoxBrush, CloseBoxRect);

            Myg.DrawLine(TitleBoxPen, CloseBoxlefttopP, CloseBoxRightbuttomP);
            Myg.DrawLine(TitleBoxPen, CloseBoxRighttopP, CloseBoxLeftbuttomP);

            ////////////释放资源
            ///
            TitleBoxPen.Dispose();
            TitleBoxBrush.Dispose();
            TitleBoxSelectBrush.Dispose();
        }





    }
}
