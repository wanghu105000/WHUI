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
    public partial class WHForm : Form
    {
        /*****关于系统采用高dpi缩放问题，屏幕是高分屏为了看清放大字体系统设置里 缩放了布局与字体 *********
         * 此时系统接管 程序界面绘制，会使软件字体与图标变得模糊，
         * 解决1，启动程序工程中 添加 程序清单 即 工程->添加类->添加程序清单文件->看到开启dpi自动感知的部分将两头的注释去掉
         * 解决2，不想，添加程序清单，请将屏幕缩放改为100%
         * 解决3，网上搜代码 加在 主程序启动前，略！
         * 解决4，换WPF程序有自动感知功能

        ******* ********* ********* ***** ************* ***********************/


        public WHForm()
        {
            //设置双缓冲

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.AllPaintingInWmPaint, true);


            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            TitleHeight = Height / 20;
            TitleTextFont = Font;

            
        }
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





        #region 公共字段全局定义
        /// <summary>
        /// 标题栏按钮之间的间隔
        /// </summary>
        const int BoxJG = 6;
        const int Boxsub = 3;
        Rectangle TitleRect = new Rectangle();
        Rectangle IconRct = new Rectangle();
        Rectangle CloseBoxRect =new Rectangle();
        Rectangle MinBoxRect = new Rectangle();
        Rectangle MaxBoxRect = new Rectangle();
        bool InMouseCloseBoxRect;
        bool InMouseMinBoxRect;
        bool InMouseMaxBoxRect;

        #endregion

        #region 属性，字段定义

        public new bool AutoScroll
        {
            get => base.AutoScroll;
            set => base.AutoScroll = false;
        }

        private bool  _isDrawFormBorder=true;
        [Category("A我的"), Description("是否显示窗体边框，默认，true，显示"), Browsable(true)]
        public bool IsDrawFormBorder
        {
            get { return _isDrawFormBorder; }
            set { _isDrawFormBorder = value; Invalidate(); }
        }

        private Color _formBorderColor=Color.Black;
        [Category("A我的"), Description("窗体边框颜色，默认，true，显示"), Browsable(true)]
        public Color FormBorderColor
        {
            get { return _formBorderColor; }
            set { _formBorderColor = value; Invalidate(); }
        }
        private int _formBorderWidth=1;
        [Category("A我的"), Description("窗体边框宽度，默认，1 "), Browsable(true)]
        public int FormBorderWidth
        {
            get { return _formBorderWidth; }
            set {
                if (value<1)
                {
                    _formBorderWidth = 1;
                } else
                _formBorderWidth = value;
                Invalidate();
            }
        }
        private bool _isUserChangeSize=false;
        [Category("A我的"), Description("用户是否可以手动调节窗体大小，默认，false ，不可以 "), Browsable(true)]
        public bool IsUserChangeSize
        {
            get
            {
                if (this.WindowState==FormWindowState.Maximized)
                {
                    return false;
                }
                else
                return _isUserChangeSize; }
            set { _isUserChangeSize= value; }
        }
        private bool _isMaxAllScreen;
        [Category("A我的"), Description("窗体最大化后是否是全屏，默认，false ，不可以 "), Browsable(true)]
        public bool IsMaxAllScreen
        {
            get { return _isMaxAllScreen; }
            set { _isMaxAllScreen = value; }
        }

        private bool _isCanMoveBody;
        [Category("A我的"), Description("是否可以拖动窗体上的任意位置移动,当位true时IsCanMoveTitle为false，默认，false ，不可以 "), Browsable(true)]
        public bool IsCanMoveBody
        {
            get { return 
                    
                    _isCanMoveBody; }
            set
            {
                if (value)
                {
                    IsCanMoveTitle = false;
                }
                _isCanMoveBody= value; }
        }
        private bool _isDrawIcon=true;
        [Category("A我的"), Description("是否画图标，默认，true ，画 "), Browsable(true)]
        public bool IsDrawIcon
        {
            get { return _isDrawIcon; }
            set { _isDrawIcon = value; Invalidate(); }
        }


        //***********有关标题栏属性定义***开始***********************
        public enum TitleTextAligment
        {
             left=0,
             center=1,
            right=2,
        }
       private bool _isCanMoveTitle=true;
        [Category("A我的标题栏"), Description("拖动标题栏是否可移动窗体的位置，默认，true，可以"), Browsable(true)]
        public bool IsCanMoveTitle
        {
            get {
                if (IsDrawTitle)
                {
                    return _isCanMoveTitle;
                }
                else return false;
                     }
            set {
                if (value)
                {
                    IsCanMoveBody = false;
                }
                _isCanMoveTitle = value; }
        }

        private bool  _isDrawTitle=true;
        [Category("A我的标题栏"), Description("是否显示标题栏，默认，true，显示"), Browsable(true)]
        public bool  IsDrawTitle
        {
            get { return _isDrawTitle; }
            set { _isDrawTitle = value; Invalidate(); }
        }

        private Color _titleFirstColor=Color.DeepSkyBlue;
        [Category("A我的标题栏"), Description("标题栏的第一种颜色，默认，天蓝色 "), Browsable(true)]
        public Color TitleFirstColor
        {
            get { return _titleFirstColor; }
            set { _titleFirstColor = value; Invalidate(); }
        }

        private Color _titleSecondColor = Color.Orange;
        [Category("A我的标题栏"), Description("标题栏的第二种颜色，默认，浅蓝色 "), Browsable(true)]
        public Color TitleSecondColor
        {
            get { return _titleSecondColor; }
            set { _titleSecondColor = value; Invalidate(); }
        }

        private bool _isTitleTwoColor=false;
        [Category("A我的标题栏"), Description("标题栏是否开启渐变色颜色，默认，false，不开启 "), Browsable(true)]
        public bool IsTitleTwoColor
        {
            get { return _isTitleTwoColor; }
            set { _isTitleTwoColor = value; Invalidate(); }
        }
        private int _titleHeight=15;
        [Category("A我的标题栏"), Description("标题栏的高度像素，默认，窗体高度的15分之一 "), Browsable(true)]
        public int TitleHeight
        {
            get { return _titleHeight; }
            set {
                if (value<0)
                {
                    _titleHeight = 40;
                }
                _titleHeight = value; Invalidate(); }
        }
        private Color _titleTextColor=Color.Black;
        [Category("A我的标题栏"), Description("标题栏文本的颜色，默认，黑色 "), Browsable(true)]
        public Color TitleTextColor
        {
            get { return _titleTextColor; }
            set { _titleTextColor = value; Invalidate(); }
        }
        private bool _isShowTitleText=true;
        [Category("A我的标题栏"), Description("标题栏是否显示文字，默认，true 显示 "), Browsable(true)]
        public bool IsShowTitleText
        {
            get { return _isShowTitleText; }
            set { _isShowTitleText = value; Invalidate(); }
        }
        private TitleTextAligment _titletextAligment=0;
        [Category("A我的标题栏"), Description("标题栏显示文字的方向，默认，左边显示 "), Browsable(true)]
        public TitleTextAligment TitletextAligment
        {
            get { return _titletextAligment; }
            set { _titletextAligment = value; Invalidate(); }
        }
        private Font _titleTextFont;
        [Category("A我的标题栏"), Description("标题栏显示文字字体，默认，当前窗体默认字体 "), Browsable(true)]
        public Font TitleTextFont
        {
            get { return _titleTextFont; }
            set { _titleTextFont = value; Invalidate(); }
        }
        private bool _isShowMaxMinBox=true;
        [Category("A我的标题栏"), Description("是否显示标题栏最大化最小化按钮，默认，true显示 "), Browsable(true)]
        public bool IsShowMaxMinBox
        {
            get { return _isShowMaxMinBox; }
            set { _isShowMaxMinBox = value; Invalidate(); }
        }
        private bool _isShowTitleBox=true;
        [Category("A我的标题栏"), Description("是否显示标题栏按钮按钮，默认，true显示 "), Browsable(true)]
        public bool IsShowTitleBox
        {
            get { return _isShowTitleBox; }
            set { _isShowTitleBox = value; Invalidate(); }
        }
        private bool _isMaxedFlag=false;
        [Category("A我的"), Description("是否窗体已经最大化，默认，false "), Browsable(false)]
  
        public bool IsMaxedFlag
        {
            get
            {
                if (this.WindowState == FormWindowState.Maximized)
                {
                    return true;
                }
                else return false;
            }
            set {
                _isMaxedFlag = value;
            
                    ; Invalidate(); }
        }
        private Color _closeBoxBackColor= Color.Transparent;
        [Category("A我的标题栏"), Description("关闭按钮背景色，默认，透明色 "), Browsable(true)]
        public Color CloseBoxBackColor
        {
            get { return _closeBoxBackColor; }
            set { _closeBoxBackColor = value; Invalidate(); }
        }

        private Color _closeBoxSelectColor=Color.Red;
        [Category("A我的标题栏"), Description("关闭按钮选中后的色，默认，红色 "), Browsable(true)]
        public Color CloseBoxSelectColor
        {
            get { return _closeBoxSelectColor; }
            set { _closeBoxSelectColor = value; }
        }

        private Color _maxMinBoxBackColor = Color.Transparent;
        [Category("A我的标题栏"), Description("最大化最小化按钮背景的色，默认，透明色 "), Browsable(true)]
        public Color MaxMinBoxBackColor
        {
            get { return _maxMinBoxBackColor; }
            set { _maxMinBoxBackColor = value; Invalidate(); }
        }

        private Color _maxMinSelectColor=Color.LightBlue;
        [Category("A我的标题栏"), Description("最大化最小化选中后的色，默认，亮蓝色 "), Browsable(true)]
        public Color MaxMinSelectColor
        {
            get { return _maxMinSelectColor; }
            set { _maxMinSelectColor= value; }
        }
        private Color _titleBoxShapeColor=Color.White;
        [Category("A我的标题栏"), Description("标题栏按钮形状的颜色，默认，白色 "), Browsable(true)]
        public Color TitleBoxShapeColor
        {
            get { return _titleBoxShapeColor; }
            set { _titleBoxShapeColor = value; Invalidate(); }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!IsMaxAllScreen)
            {
                //窗体最大化后不是全屏
                this.MaximizedBounds = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea;
            }

        }

        //***********有关标题栏属性定义结束***********************

        #endregion

        /// <summary>
        /// 重绘前初始化
        /// </summary>
        void BeforePaintIni()
        {


        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            BeforePaintIni();
            Graphics Myg = e.Graphics;
            Myg.SmoothingMode = SmoothingMode.AntiAlias;
            Myg.CompositingQuality = CompositingQuality.HighQuality;
            Myg.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //**************资源申请********************



            Rectangle MyRect =this.ClientRectangle;
          //画窗体
            DrawShowBody(Myg, MyRect);
            // 画标题栏
            if (IsDrawTitle)
            {
             DrawTitle(Myg, MyRect);
            }
          
            //画边框
            DrawBodyBorder(Myg, MyRect);

       

        }
        void DrawShowBody( Graphics Myg, Rectangle MyRect)
        {
             
            //填充窗体区域
            using (SolidBrush BodyFillBrush =new SolidBrush (BackColor))
            {
                Myg.FillRectangle(BodyFillBrush, MyRect);   
            }
            //画标题栏




      


        }
        /// <summary>
        /// 画窗体边框
        /// </summary>
        /// <param name="Myg"></param>
        /// <param name="MyRect"></param>
        void DrawBodyBorder(Graphics Myg,Rectangle MyRect)
        {

            //如果画窗体边框就画窗体边框
             if (IsDrawFormBorder)
            {
                int penWidht = FormBorderWidth / 2;
                Rectangle borderRect = new Rectangle();
                borderRect.X = penWidht;
                borderRect.Y = penWidht;
                borderRect.Width = MyRect.Width - FormBorderWidth;
                borderRect.Height = MyRect.Height - FormBorderWidth;


                using (SolidBrush BodyBorderBrush=new SolidBrush(FormBorderColor))
                { using (Pen BodyBorderPen = new Pen(BodyBorderBrush,FormBorderWidth))
                    {
                       Myg.DrawRectangle(BodyBorderPen, borderRect);
                    }  }   }


        }
        /// <summary>
        /// 画标题栏文字
        /// </summary>
        /// <param name="Myg"></param>
        /// <param name="textRect"></param>
        void DrawTitleText(Graphics Myg,Rectangle textRect)
        {
            using (SolidBrush TitleFontBrush = new SolidBrush(TitleTextColor))
            {

                StringFormat sf = new StringFormat();
                switch (TitletextAligment)
                {
                    case TitleTextAligment.left:
                        sf.Alignment = StringAlignment.Near;
                        break;
                    case TitleTextAligment.center:
                        sf.Alignment = StringAlignment.Center;
                        break;
                    case TitleTextAligment.right:
                        sf.Alignment = StringAlignment.Far;
                        break;
                    default:
                        sf.Alignment = StringAlignment.Center;
                        break;
                }

                sf.LineAlignment = StringAlignment.Center;
            

                   Myg.DrawString(Text, TitleTextFont,TitleFontBrush, textRect, sf);
             }


        }


        void DrawIcon(Graphics Myg,Rectangle TitleRect)
        {
            
              
            if (IsDrawTitle && IsDrawIcon && this.Icon !=null)
            {
                //Graphics Icog = Graphics.FromHdc(Myg.GetHdc());
               

              
                int h = TitleRect.Height / 10;
                IconRct.X = TitleRect.X + h+5;
                IconRct.Y = TitleRect.Y + h;
                  IconRct.Height =TitleHeight / 5*4;
                IconRct.Width = IconRct.Height;


                //Myg.SmoothingMode = SmoothingMode.Default;
       
                Myg.DrawIcon(this.Icon, IconRct);
               

             }


        }





        /// <summary>
        /// 画出关闭按钮
        /// </summary>
        /// <param name="TitleRect">要画在那个标题区域</param>
        /// <param name="Myg">在那个设备上下文上面</param>
        /// <param name="TitleBoxBackColor">按钮背景色</param>
        /// <param name="MaxMinSelectColor">按钮选中后的背景色</param>
        /// <param name="TitleBoxShapeColor">按钮内部图形的颜色</param>
        void CloseBoxDraw(Rectangle TitleRect,Graphics Myg,Color CloseBoxBackColor, Color ColseBoxSelectColor, Color TitleBoxShapeColor)
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

            }  else
            Myg.FillRectangle(TitleBoxBrush, CloseBoxRect);
            
            Myg.DrawLine(TitleBoxPen, CloseBoxlefttopP, CloseBoxRightbuttomP);
                Myg.DrawLine(TitleBoxPen, CloseBoxRighttopP, CloseBoxLeftbuttomP);

                ////////////释放资源
        ///
        TitleBoxPen.Dispose();
                TitleBoxBrush.Dispose();
            TitleBoxSelectBrush.Dispose();
        }
        /// <summary>
        /// 画出最大化按钮
        /// </summary>
        /// <param name="TitleRect">要画在那个标题区域</param>
        /// <param name="Myg">在那个设备上下文上面</param>
        /// <param name="TitleBoxBackColor">按钮背景色</param>
        /// <param name="MaxMinSelectColor">按钮选中后的背景色</param>
        /// <param name="TitleBoxShapeColor">按钮内部图形的颜色</param>
        void MaxBoxDraw(Rectangle TitleRect, Graphics Myg, Color TitleBoxBackColor, Color MaxMinSelectColor, Color TitleBoxShapeColor)
        {
            SolidBrush TitleBoxBrush = new SolidBrush(TitleBoxBackColor);
            SolidBrush TitleBoxSelectBrush = new SolidBrush(MaxMinSelectColor);
            Pen TitleBoxPen = new Pen(TitleBoxShapeColor, 2);
            int Boxhight = TitleHeight;
            int wbl = TitleHeight / 2;
            int hbl = wbl;
            //定义最大化按钮范围
            MaxBoxRect.X = CloseBoxRect.X - CloseBoxRect.Width - BoxJG;
            MaxBoxRect.Y = CloseBoxRect.Y;
            MaxBoxRect.Width = CloseBoxRect.Width;
            MaxBoxRect.Height = CloseBoxRect.Height;
            //填充最大化按钮并画出对应的图形
            if (InMouseMaxBoxRect)
            {
                Myg.FillRectangle(TitleBoxSelectBrush, MaxBoxRect);
            }
            else
           Myg.FillRectangle(TitleBoxBrush, MaxBoxRect);
            
            if (IsMaxedFlag)
            {
                //已经最大化时对应  品 型
                PointF[] BigRectP = new PointF[4];
                PointF[] SmallRectP = new PointF[5];


                float H = MaxBoxRect.Height / 5;
                //PointF FistPoint = new PointF(MaxBoxRect.X + H, MaxBoxRect.Y + 2);
                BigRectP[0].X = MaxBoxRect.X + H;
                BigRectP[0].Y = MaxBoxRect.Y + H * 2;
                BigRectP[1].X = MaxBoxRect.X + H * 3;
                BigRectP[1].Y = MaxBoxRect.Y + H * 2;
                BigRectP[2].X = MaxBoxRect.X + H * 3;
                BigRectP[2].Y = MaxBoxRect.Y + H * 4;
                BigRectP[3].X = MaxBoxRect.X + H;
                BigRectP[3].Y = MaxBoxRect.Y + H * 4;
                ////////////////////////////////
                SmallRectP[0].X = MaxBoxRect.X + H * 2;
                SmallRectP[0].Y = MaxBoxRect.Y + H * 2;
                SmallRectP[1].X = MaxBoxRect.X + H * 2;
                SmallRectP[1].Y = MaxBoxRect.Y + H;
                SmallRectP[2].X = MaxBoxRect.X + H * 4;
                SmallRectP[2].Y = MaxBoxRect.Y + H;
                SmallRectP[3].X = MaxBoxRect.X + H * 4;
                SmallRectP[3].Y = MaxBoxRect.Y + H * 3;
                SmallRectP[4].X = MaxBoxRect.X + H * 3;
                SmallRectP[4].Y = MaxBoxRect.Y + H * 3;

                /////////////
                Myg.DrawLine(TitleBoxPen, BigRectP[0], BigRectP[1]);
                Myg.DrawLine(TitleBoxPen, BigRectP[1], BigRectP[2]);
                Myg.DrawLine(TitleBoxPen, BigRectP[2], BigRectP[3]);
                Myg.DrawLine(TitleBoxPen, BigRectP[3], BigRectP[0]);
                Myg.DrawLine(TitleBoxPen, SmallRectP[0], SmallRectP[1]);
                Myg.DrawLine(TitleBoxPen, SmallRectP[1], SmallRectP[2]);
                Myg.DrawLine(TitleBoxPen, SmallRectP[2], SmallRectP[3]);
                Myg.DrawLine(TitleBoxPen, SmallRectP[3], SmallRectP[4]);

            }
            else
            {
                //没有最大化时对应 口 型
                Point MaxTopLeftP = new Point(MaxBoxRect.X + wbl / 2, MaxBoxRect.Y + hbl / 2);
                Point MaxTopRightP = new Point(MaxTopLeftP.X + wbl, MaxTopLeftP.Y);
                Point MaxButtomLeftP = new Point(MaxTopLeftP.X, MaxTopLeftP.Y + hbl);
                Point MaxButtomRightP = new Point(MaxTopLeftP.X + wbl, MaxTopLeftP.Y + hbl);
                Myg.DrawLine(TitleBoxPen, MaxTopLeftP, MaxTopRightP);
                Myg.DrawLine(TitleBoxPen, MaxTopRightP, MaxButtomRightP);
                Myg.DrawLine(TitleBoxPen, MaxButtomRightP, MaxButtomLeftP);
                Myg.DrawLine(TitleBoxPen, MaxButtomLeftP, MaxTopLeftP);

            }
          ////////////释放资源
            ///
            TitleBoxPen.Dispose();
            TitleBoxBrush.Dispose();
            TitleBoxSelectBrush.Dispose();

        }
        /// <summary>
        /// 画出最小化按钮
        /// </summary>
        /// <param name="TitleRect">要画在那个标题区域</param>
        /// <param name="Myg">在那个设备上下文上面</param>
        /// <param name="TitleBoxBackColor">按钮背景色</param>
        /// <param name="MaxMinSelectColor">按钮选中后的背景色</param>
        /// <param name="TitleBoxShapeColor">按钮内部图形的颜色</param> 
        void MinBoxDraw(Rectangle TitleRect, Graphics Myg, Color TitleBoxBackColor, Color MaxMinSelectColor, Color TitleBoxShapeColor)
        {
            SolidBrush TitleBoxBrush = new SolidBrush(CloseBoxBackColor);
            SolidBrush TitleBoxSelectBrush = new SolidBrush(MaxMinSelectColor);
            Pen TitleBoxPen = new Pen(TitleBoxShapeColor, 2);
            int Boxhight = TitleHeight;
            int wbl = TitleHeight / 2;
            int hbl = wbl;

            //定义最小化按钮范围

            MinBoxRect.X = MaxBoxRect.X - MaxBoxRect.Width - BoxJG;
            MinBoxRect.Y = MaxBoxRect.Y;
            MinBoxRect.Width = MaxBoxRect.Width;
            MinBoxRect.Height = MaxBoxRect.Height;
            ///  填充最小化化按钮
            if (InMouseMinBoxRect)
            {
                Myg.FillRectangle(TitleBoxSelectBrush, MinBoxRect);
            }
            else
            Myg.FillRectangle(TitleBoxBrush, MinBoxRect);

            Point MinTopLeftP = new Point(MinBoxRect.X + hbl / 2, MinBoxRect.Y + hbl);
            Point MinTopRightP = new Point(MinTopLeftP.X + wbl, MinTopLeftP.Y);
            Myg.DrawLine(TitleBoxPen, MinTopLeftP, MinTopRightP);
            ////////////释放资源
            ///
            TitleBoxPen.Dispose();
            TitleBoxBrush.Dispose();
            TitleBoxSelectBrush.Dispose();
        }



           







        /// <summary>
        /// 绘制三个标题栏的按钮将来可以 再这里多加按钮
        /// </summary>
        /// <param name="Myg"></param>
        /// <param name="TitleRect"></param>
        void DrawTitleMinMaxCloseBox(Graphics Myg,Rectangle TitleRect)
        {
        //SolidBrush TitleBoxBrush = new SolidBrush(Color.Red);
        if (IsShowTitleBox)
        {
                CloseBoxDraw(TitleRect, Myg, CloseBoxBackColor,CloseBoxSelectColor ,TitleBoxShapeColor);
                
                if (IsShowMaxMinBox)
            {
               MaxBoxDraw(TitleRect, Myg, CloseBoxBackColor,MaxMinSelectColor,TitleBoxShapeColor);
               MinBoxDraw(TitleRect, Myg, CloseBoxBackColor,MaxMinSelectColor ,TitleBoxShapeColor);



                }

            }
        }
     /// <summary>
     /// 画整个标题栏
     /// </summary>
     /// <param name="Myg"></param>
     /// <param name="MyRect"></param>
        void DrawTitle( Graphics Myg,Rectangle MyRect)
        {
            
            if (IsDrawFormBorder)
            { 
                int penWidth=FormBorderWidth/2;
                TitleRect.Width = MyRect.Width- FormBorderWidth;
                TitleRect.Height = TitleHeight;
                TitleRect.X = MyRect.X+ FormBorderWidth;
                TitleRect.Y = MyRect.Y + FormBorderWidth;

            }
            else
            {
            TitleRect.Width = MyRect.Width ;
            TitleRect.Height = TitleHeight;
            TitleRect.X = MyRect.X  ;
            TitleRect.Y = MyRect.Y;
            }


            if (IsTitleTwoColor)
            {
             using (LinearGradientBrush titlebrush = new LinearGradientBrush(TitleRect, TitleFirstColor, TitleSecondColor, LinearGradientMode.Horizontal)) 
               
                {
                    Myg.FillRectangle(titlebrush, TitleRect);
                }

            }
            else
            {
                using (SolidBrush titleBrush=new SolidBrush(TitleFirstColor))
                {
                    Myg.FillRectangle(titleBrush, TitleRect);

                }
            }
            DrawTitleMinMaxCloseBox(Myg, TitleRect);
            if (IsDrawIcon)
            {
                DrawIcon(Myg, TitleRect);
            }
            if (IsShowTitleText)
            {
                Rectangle TitleTextRect = new Rectangle();
                if (IsDrawIcon)
                {
                    if (_titletextAligment!= TitleTextAligment.center)
                    {
                    TitleTextRect.X = IconRct.Width+15;
                    TitleTextRect.Y = TitleRect.Y;
                    TitleTextRect.Width = TitleRect.Width-IconRct.Width-4;
                    TitleTextRect.Height = TitleRect.Height;
                    }
                    else
                    {
                        TitleTextRect = TitleRect;
                    } }

                else
                {
                    TitleTextRect = TitleRect;
                }

               


                DrawTitleText(Myg, TitleTextRect);
            }

         

   
   


    }
       /// <summary>
       /// 挡鼠板在标题栏上移动时 顶部三大按键 感应动作
       /// </summary>
        void OnMouseMoveTitleBox()
        {
            if (this.RectangleToScreen(CloseBoxRect).Contains(MousePosition))
            {
                InMouseCloseBoxRect = true;
                Invalidate(CloseBoxRect);
                return;
            }
            else
            {
                InMouseCloseBoxRect = false;
                Invalidate(CloseBoxRect);


            }
            if (this.RectangleToScreen(MaxBoxRect).Contains(MousePosition))
            {
                InMouseMaxBoxRect = true;
                Invalidate(MaxBoxRect);
                return;
            }
            else
            {
                InMouseMaxBoxRect = false;
                Invalidate(MaxBoxRect);

            }
            if (this.RectangleToScreen(MinBoxRect).Contains(MousePosition))
            {
                InMouseMinBoxRect = true;
                Invalidate(MinBoxRect);
                return;
            }
            else
            {
                InMouseMinBoxRect = false;
                Invalidate(MinBoxRect);

            }


        }
        /// <summary>
        /// 挡鼠标在标题栏上移动时 顶部三大按键 感应点击动作
        /// </summary>
        void OnMouseClickTitleBox()
        {
           
            if (this.RectangleToScreen(CloseBoxRect).Contains(MousePosition))
            {
                this.Close();
                return;
            }

            if (this.RectangleToScreen(MaxBoxRect).Contains(MousePosition))
            {
                if (this.WindowState == FormWindowState.Maximized)
                {
                        Invalidate(MaxBoxRect);
                    this.WindowState = FormWindowState.Normal;
                    return;
                }

         
            else if (this.WindowState == FormWindowState.Normal)
            {
                 Invalidate(MaxBoxRect);
                this.WindowState = FormWindowState.Maximized;
                return;

                }
            }

            if (this.RectangleToScreen(MinBoxRect).Contains(MousePosition))
            {
                this.WindowState = FormWindowState.Minimized;
                return;
            }


        }





        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            OnMouseMoveTitleBox();
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (IsDrawTitle)
            {   
                InMouseMinBoxRect = false;
            InMouseMaxBoxRect = false;
            InMouseCloseBoxRect = false;
            Invalidate(TitleRect);

            }
        
        }



        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (IsDrawTitle)
            {
               OnMouseClickTitleBox();
            }
         
        }

        #region 拖动改变窗体的尺寸大小
   //拖动改变窗体的尺寸大小
        //        鼠标动作常见参数：

        //鼠标移动：512

        //鼠标左键：

        //down:513 up:514

        //double click:515

        //鼠标右键：

        //down:516 up:517

        //鼠标滚轮：522

        //定义9个方向
        const int HTLEFT = 10;
        const int HTRIGHT = 11;
        const int HTTOP = 12;
        const int HTTOPLEFT = 13;
        const int HTTOPRIGHT = 14;
        const int HTBOTTOM = 15;
        const int HTBOTTOMLEFT = 0x10;
        const int HTBOTTOMRIGHT = 17;

      protected override void WndProc(ref Message m)
        {
            //base.WndProc(ref m);
       
            
                switch (m.Msg)
                {
                      
                    case 0x0084:
                    base.WndProc(ref m);
        //当允许改变窗体大小时候并不能是最大窗体  截获的消息用来改变窗体大小
                    if (IsUserChangeSize)
                      {  
                        Point vPoint = new Point((int)m.LParam & 0xFFFF,
                            (int)m.LParam >> 16 & 0xFFFF);
                vPoint = PointToClient(vPoint);
                if (vPoint.X <= 5)
                    if (vPoint.Y <= 5)
                        m.Result = (IntPtr)HTTOPLEFT;
                    else if (vPoint.Y >= ClientSize.Height - 5)
                        m.Result = (IntPtr)HTBOTTOMLEFT;
                    else m.Result = (IntPtr)HTLEFT;
                else if (vPoint.X >= ClientSize.Width - 5)
                    if (vPoint.Y <= 5)
                        m.Result = (IntPtr)HTTOPRIGHT;
                    else if (vPoint.Y >= ClientSize.Height - 5)
                        m.Result = (IntPtr)HTBOTTOMRIGHT;
                    else m.Result = (IntPtr)HTRIGHT;
                else if (vPoint.Y <= 5)
                    m.Result = (IntPtr)HTTOP;
                else if (vPoint.Y >= ClientSize.Height - 5)
                    m.Result = (IntPtr)HTBOTTOM;
            }
                        break;

                case 0x0201://鼠标左键按下的消息 
                 base.WndProc(ref m);
                    if (IsCanMoveTitle&&IsDrawTitle)
                    {
  
                        //判断鼠标是否在标题栏区域内如果在就 拖动窗体
                        if (this.RectangleToScreen(TitleRect).Contains(MousePosition) &&
                          !this.RectangleToScreen(CloseBoxRect).Contains(MousePosition)
                         && !this.RectangleToScreen(MaxBoxRect).Contains(MousePosition)
                         && !this.RectangleToScreen(MinBoxRect).Contains(MousePosition)
                         && this.WindowState != FormWindowState.Maximized)
                        {
                            ReleaseCapture();
                            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
                         
                            return;
                        }

                    }
                    //当允许鼠标在任意位置拖动窗体移动 不包括三大按钮 但是包括标题栏，也可以修改不包括标题栏 目前包括标题栏
                    if (IsCanMoveBody && this.WindowState != FormWindowState.Maximized
                        && !this.RectangleToScreen(CloseBoxRect).Contains(MousePosition)
                        && !this.RectangleToScreen(MinBoxRect).Contains(MousePosition)
                        && !this.RectangleToScreen(MaxBoxRect).Contains(MousePosition))
                    {

                        ReleaseCapture();
                        SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
                        base.WndProc(ref m);
                        return;

                    }

                    break;
                case WM_HSCROLL:
                    base.WndProc(ref m);
          
                   
             


                    break;
        

                default:
                    base.WndProc(ref m);
                    break;

                }
       


        }



        #endregion




        }



    }

