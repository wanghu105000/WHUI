﻿using System;
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
    //********************** ******      说明   ******  ******************************************
    //***********本对话框窗体基类，基本上都是按比例显示窗体，目的为了自适应屏幕分辨率*****
    //***********      继承本类的窗体上的控件，尽量用比例计算出外形*******************
    //***************************************************************************

      delegate  void MaskFrmClose();
   
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
            ////////设置成无边框窗体
            this.FormBorderStyle = FormBorderStyle.None;

            //this.TopMost = true;
            this.StartPosition = FormStartPosition.CenterScreen;

        }
        //************全局参数定义***************
       public Rectangle MyRect = new Rectangle();
      public  RectangleF DrawRct = new RectangleF();
       public Rectangle TitleRect = new Rectangle();
       public Rectangle CloseBoxRect = new Rectangle();


      public bool OnMouseCloseBoxFlag;
        Form maskFrm;
        MaskFrmClose maskFrmClose;
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

        private string _messageText="";
        [Category("A我的"), Description("要显示的信息消息文本，默认，无"), Browsable(true)]
        public string MessageText
        {
            get { return _messageText; }
            set { _messageText = value; Invalidate(); }
        }

        private bool _isShowMaskFrm;
        [Category("A我的"), Description("要否要显示遮罩窗体，默认，无"), Browsable(true)]
        public bool IsShowMaskFrm
        {
            get { return _isShowMaskFrm; }
            set { _isShowMaskFrm = value; }
        }

        private float _myDialogWidthBl=4.5f;
        [Category("A我的"), Description("窗体相对屏幕分辨率的宽度的几分之一，默认，屏幕宽度的4.5分之一，不能小于1f"), Browsable(true)]
        public float MyDialogWidthBl
        {
            get { return _myDialogWidthBl; }
            set {
                if (value<1.1f)
                {
                    _myDialogWidthBl = 2.0f;
                }
                _myDialogWidthBl = value; Invalidate();
            }
        }


        private float _myDialogHeightBl = 4.5f;
        [Category("A我的"), Description("窗体相对屏幕分辨率的宽度的几分之一，默认，屏幕宽度的4.5分之一，不能小于1f"), Browsable(true)]
        public float MyDialogHeightBl
        {
            get { return _myDialogHeightBl; }
            set
            {
                if (value < 1.1f)
                {
                    _myDialogHeightBl = 2.0f;
                }
                _myDialogHeightBl = value; Invalidate();
            }
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
        private bool _isShowTitle=true;
        [Category("A我的"), Description("是否显示标题栏，默认，显示 "), Browsable(true)]
        public bool IsShowTitle
        {
            get { return _isShowTitle; }
            set { _isShowTitle = value; Invalidate(); }
        }



        //***********有关标题栏属性定义***开始***********************
 
       public enum CloseBoxShape { square,Circle}

       
        private CloseBoxShape _myCloseBoxShape=CloseBoxShape.square;
        [Category("A我的标题栏"), Description("关闭按钮距的形状，默认，方形 "), Browsable(true)]
        public CloseBoxShape MyCloseBoxShape
        {
            get { return _myCloseBoxShape; }
            set { _myCloseBoxShape = value; Invalidate(); }
        }

       private int _boxJG=10;

        [Category("A我的标题栏"), Description("关闭按钮距离窗体左边的距离，默认，10 "), Browsable(true)]
        public int BoxJG
        {
            get { return _boxJG; }
            set { _boxJG= value; Invalidate(); }
        }

        private Color _titleTextColor = Color.Black;
        [Category("A我的标题栏"), Description("标题栏文本的颜色，默认，黑色 "), Browsable(true)]
        public Color TitleTextColor
        {
            get { return _titleTextColor; }
            set { _titleTextColor = value; Invalidate(); }
        }

        private Font _titleTextFont= new Font("宋体", 12);
        [Category("A我的标题栏"), Description("标题栏显示文字字体，默认，当前窗体默认字体 "), Browsable(true)]
        public Font TitleTextFont 
        {
            get { return _titleTextFont; }
            set { _titleTextFont = value; Invalidate(); }
        }

        private int _titleHeight = 5;
        [Category("A我的标题栏"), Description("标题栏的高度像素，默认，窗体高度的4分之一,不能小于2分之一不然不美观 "), Browsable(true)]
        public int TitleHeight
        {
            get { return _titleHeight; }
            set
            {
                if (value < 2)
                {
                    _titleHeight = 2;
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
            set { _closeBoxSelectColor = value;  }
        }
        private Color _closeBoxShapeColor = Color.White;
        [Category("A我的标题栏"), Description("标题栏按钮形状的颜色，默认，白色 "), Browsable(true)]
        public Color CloseBoxShapeColor
        {
            get { return _closeBoxShapeColor; }
            set { _closeBoxShapeColor = value; Invalidate(); }
        }
        private Color _titleBackColor=Color.DeepSkyBlue;
        [Category("A我的标题栏"), Description("标题栏背景的颜色，默认，蓝色 "), Browsable(true)]
        public Color TitleBackColor
        {
            get { return _titleBackColor; }
            set { _titleBackColor = value; Invalidate(); }
        }

        private bool _isShowCloseBoxBorder=true;
        [Category("A我的标题栏"), Description("是否显示关闭按钮外边框，默认，是 "), Browsable(true)]
        public bool IsShowCloseBoxBorder
        {
            get { return _isShowCloseBoxBorder; }
            set { _isShowCloseBoxBorder = value; Invalidate(); }
        }

        private Color _closeBoxBorderColor=Color.Black;
        [Category("A我的标题栏"), Description("关闭按钮外边框颜色，默认，黑色 "), Browsable(true)]
        public Color CloseBoxBorderColor
        {
            get { return _closeBoxBorderColor; }
            set { _closeBoxBorderColor= value; Invalidate(); }
        }

        private float _closeBoxBorderWidth = 2.0f;
        [Category("A我的标题栏"), Description("关闭按钮边框和内部形状的宽度，默认,2.0f 不能小于0.5f "), Browsable(true)]
        public float CloseBoxBorderWidth
        {
            get { return _closeBoxBorderWidth; }
            set
            {
                if (value<0.5f)
                {
                    _closeBoxBorderWidth = 0.5f;
                }
                
                _closeBoxBorderWidth = value; Invalidate(); }
        }


        private bool _isShowTitleBorder = true;
        [Category("A我的标题栏"), Description("是否显示标题栏的外边框，默认，是 "), Browsable(true)]
        public bool IsShowTitleBorder
        {
            get { return _isShowTitleBorder; }
            set { _isShowTitleBorder = value; Invalidate(); }
        }


        private Color _titleBorderColor = Color.Black;
        [Category("A我的标题栏"), Description("标题栏的外边框颜色，默认，黑色 "), Browsable(true)]
        public Color TitleBorderColor
        {
            get { return _titleBorderColor; }
            set { _titleBorderColor = value; Invalidate(); }
        }

        private float _titleBorderWidth = 2.0f;
        [Category("A我的标题栏"), Description("标题栏的外边框的宽度，默认,2.0f 不能小于0.5f "), Browsable(true)]
        public float TitleBorderWidth
        {
            get { return _titleBorderWidth; }
            set
            {
                if (value < 0.5f)
                {
                    _titleBorderWidth = 0.5f;
                }

                _titleBorderWidth = value; Invalidate();
            }
        }

        public enum TitleTextAglin { Center, Left }
        private TitleTextAglin _myTitleTextAglin = TitleTextAglin.Center;
        [Category("A我的标题栏"), Description("标题栏 文字的对齐方向，默认，中间 "), Browsable(true)]
        public TitleTextAglin MmyTitleTextAglin
        {
            get { return _myTitleTextAglin; }
            set { _myTitleTextAglin = value; Invalidate(); }
        }

        private string _titleText = "信息";
        [Category("A我的标题栏"), Description("标题栏 的 文字，默认，信息 "), Browsable(true)]
        public string TitleText
        {
            get { return _titleText; }
            set { _titleText = value; Invalidate(); }
        }

        private float _closeBoxSize=0.8f;
        [Category("A我的标题栏"), Description("关闭按钮相对于标题栏的大小，默认 0.8f不能大于1.0f小于0.2f "), Browsable(true)]
        public float CloseBoxSize
        {
            get { return _closeBoxSize; }
            set {
                if (value>1|value<0.2f)
                {
                    value= 0.2f;
                }
              else  _closeBoxSize = value; Invalidate();
            }
        }


        #endregion

        protected override void WndProc(ref Message m)
        {
          
            switch (m.Msg)
            {
                case 0x0201: //截获鼠标左键按下的消息实现鼠标拖到窗体移动 
                  base.WndProc(ref m); 
                    if (this.RectangleToScreen(TitleRect).Contains(MousePosition)&& !this.RectangleToScreen(CloseBoxRect).Contains(MousePosition))
                    {
                    ReleaseCapture();
                    SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
                
                    }
                    
              
                    break;

                default:
                    base.WndProc(ref m);
                    break;
            }


        }




        void beforePainIni()
        {
            this.Width = (int)(Screen.PrimaryScreen.WorkingArea.Width / MyDialogWidthBl);
            this.Height = (int)(Screen.PrimaryScreen.WorkingArea.Height/ MyDialogHeightBl);
          
            MyRect = this.ClientRectangle;

            DrawRct.X = MyRect.X + FormBorderWidth / 2;
            DrawRct.Y = MyRect.Y + FormBorderWidth / 2;
            DrawRct.Width = MyRect.Width - FormBorderWidth;
            DrawRct.Height = MyRect.Height - FormBorderWidth;

            if (IsDrawFormBorder)
            {
               
            TitleRect.Y = (int)(DrawRct.Y + FormBorderWidth);
                TitleRect.X = (int)(DrawRct.X + FormBorderWidth);
                if (IsShowCloseBoxBorder)
                {
                    TitleRect.Height = (int)(DrawRct.Height / TitleHeight + FormBorderWidth+CloseBoxBorderWidth);
                }
                else
                {
               TitleRect.Height = (int)(DrawRct.Height / TitleHeight + FormBorderWidth );
               
                }
          TitleRect.Width =(int)( DrawRct.Width- FormBorderWidth*2);
               
            }
            else
            {
                TitleRect.Width = MyRect.Width;
                TitleRect.Height = Height / TitleHeight;
                TitleRect.X = MyRect.X;
                TitleRect.Y = MyRect.Y;
            }



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

        
        
            using (SolidBrush backBrush=new SolidBrush(Color.Transparent))
            {
               Myg.FillRectangle(backBrush, Myrect);

            }

            GraphicsPath RoundPath = new GraphicsPath();

            RoundPath = DrawHelper.GetRoundRectangePath(DrawRct, FormBorderWidth, Radius);
          
            if (IsUseTwoColor)
            {
               
                using (LinearGradientBrush LinerBrush = new LinearGradientBrush(DrawRct, firstColor, SecondColor, LinearGradientMode.Vertical))
                {
                    Myg.FillPath(LinerBrush, RoundPath);
                
                }

            }
            else
            {
                this.BackColor = firstColor;
            }
           
            
            //画标题栏
            DrawTitle(Myg, MyRect);
            DrawTitleText(Myg, TitleRect);



            //画边框
            if (IsDrawFormBorder)
            {
                using (Pen formBodyPen=new Pen(FormBorderColor,FormBorderWidth))
                {
                     GraphicsPath formbodypath=new GraphicsPath();
                    formbodypath= DrawHelper.GetRoundRectangePath(DrawRct, FormBorderWidth, Radius);
                    Myg.DrawPath(formBodyPen, formbodypath);

                }
            }

            GraphicsPath formPath = new GraphicsPath();
            formPath = DrawHelper.GetRoundRectangePath(Myrect, FormBorderWidth, Radius);
            Region Formreg = new Region(formPath);
            this.Region = Formreg;


        


            //释放资源



        }
        void DrawCloseBox(Graphics Myg,RectangleF TitleRect)
        {

            SolidBrush TitleBoxBrush = new SolidBrush(CloseBoxBackColor);
            SolidBrush TitleBoxSelectBrush = new SolidBrush(CloseBoxSelectColor);
            Pen TitleBoxPen = new Pen(CloseBoxShapeColor, CloseBoxBorderWidth);
            Pen CloseBoxBorderPen = new Pen(CloseBoxBorderColor, CloseBoxBorderWidth);
            //关闭按钮的相对高度
            float Boxhight = TitleRect.Height* CloseBoxSize - BoxJG ;
          //叉形状在关闭按钮区域中的占比
            float wbl =Boxhight/3 ;
            float hbl = wbl;

            //定义 关闭按钮范围

            if (IsDrawFormBorder)
            {
                CloseBoxRect.X = (int)(TitleRect.X + TitleRect.Width - Boxhight - FormBorderWidth - BoxJG);

            }
            else
            {
                CloseBoxRect.X = (int)(TitleRect.X + TitleRect.Width - Boxhight - BoxJG);

            }
            CloseBoxRect.Y = (int)(TitleRect.Y + FormBorderWidth/2  + BoxJG / 4);
            CloseBoxRect.Width = (int)Boxhight ;
            CloseBoxRect.Height = (int)Boxhight;
            PointF CloseBoxlefttopP = new PointF(CloseBoxRect.X +CloseBoxRect.Width/2-wbl/2 , CloseBoxRect.Y+ CloseBoxRect.Height/2-hbl/2) ;
            PointF CloseBoxRighttopP = new PointF(CloseBoxlefttopP.X + wbl, CloseBoxlefttopP.Y);
            PointF CloseBoxLeftbuttomP = new PointF(CloseBoxlefttopP.X, CloseBoxlefttopP.Y + hbl );
            PointF CloseBoxRightbuttomP = new PointF(CloseBoxlefttopP.X + wbl , CloseBoxlefttopP.Y + hbl );
            if (OnMouseCloseBoxFlag)
            {
                switch (MyCloseBoxShape)
                {
                    case CloseBoxShape.square:
                       Myg.FillRectangle(TitleBoxSelectBrush, CloseBoxRect); 
                        
                        if (IsShowCloseBoxBorder)
                        {
                            Myg.DrawRectangle(CloseBoxBorderPen, CloseBoxRect);

                        }
                         
                        break;
                    case CloseBoxShape.Circle:
                       Myg.FillEllipse(TitleBoxSelectBrush, CloseBoxRect);
                        if (IsShowCloseBoxBorder)
                        {
                            Myg.DrawEllipse(CloseBoxBorderPen, CloseBoxRect);

                        }

                       
                        break;
                    default:
                        break;
                }
         

            }
            else
                switch (MyCloseBoxShape)
                {
                    case CloseBoxShape.square:
                       Myg.FillRectangle(TitleBoxBrush, CloseBoxRect);
                        if (IsShowCloseBoxBorder)
                        {
                            Myg.DrawRectangle(CloseBoxBorderPen, CloseBoxRect);

                        }
                       
                        break;
                    case CloseBoxShape.Circle:
                        Myg.FillEllipse (TitleBoxBrush, CloseBoxRect);
                        if (IsShowCloseBoxBorder)
                        {
                            Myg.DrawEllipse(CloseBoxBorderPen, CloseBoxRect);

                        }
                      
                        break;
                    default:
                        break;
                }
        

  
        

            Myg.DrawLine(TitleBoxPen, CloseBoxlefttopP, CloseBoxRightbuttomP);
            Myg.DrawLine(TitleBoxPen, CloseBoxRighttopP, CloseBoxLeftbuttomP);

            ////////////释放资源
            ///
            TitleBoxPen.Dispose();
            TitleBoxBrush.Dispose();
            TitleBoxSelectBrush.Dispose();
            CloseBoxBorderPen.Dispose();


        }





  

      public virtual  void DrawTitle(Graphics Myg, Rectangle MyRect)
        {
            using (SolidBrush titleBrush = new SolidBrush(TitleBackColor))
            {
                GraphicsPath titlePath = new GraphicsPath();
                titlePath = DrawHelper.GetRoundRectangePath(TitleRect, 0, Radius);

                Myg.FillPath(titleBrush, titlePath);
                if (IsShowTitleBorder)
                {
                    using (Pen titleBorderPen=new Pen(TitleBorderColor,TitleBorderWidth))
                    {
                          Myg.DrawPath(Pens.Brown,titlePath);  }
                  
                }

            }
            //////////////////画关闭按钮

            DrawCloseBox(Myg, TitleRect);
          

        }

    protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (this.RectangleToScreen(CloseBoxRect).Contains(MousePosition) )
            {
             OnMouseCloseBoxFlag=true;
             Invalidate(CloseBoxRect);
            }
            else
            {
                OnMouseCloseBoxFlag = false;
                Invalidate(CloseBoxRect);
            }
            
         
           
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            OnMouseCloseBoxFlag = false;
            Invalidate(CloseBoxRect);
        }
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (this.RectangleToScreen(CloseBoxRect).Contains(MousePosition))
            {
                Close();
            }

        }

        public virtual void DrawTitleText(Graphics Myg, Rectangle TitleRct)
        {
            Rectangle titleTextRect = new Rectangle();
            titleTextRect.X = TitleRct.X - 10;
            titleTextRect.Y = TitleRct.Y;

            titleTextRect.Width = TitleRct.Width - 10;
            titleTextRect.Height = TitleRct.Height;
            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            switch (_myTitleTextAglin)
            {
                case TitleTextAglin.Center:
                    sf.Alignment = StringAlignment.Center;
                    break;
                case TitleTextAglin.Left:
                    sf.Alignment = StringAlignment.Near;
                    break;
                default:
                    sf.Alignment = StringAlignment.Center;
                    break;
            }
            if (TitleText != null)
            {
                using (SolidBrush titleTextbrush = new SolidBrush(TitleTextColor))
                {
                    Myg.DrawString(TitleText, TitleTextFont, titleTextbrush, titleTextRect, sf);
                }



            }



        }
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            if (maskFrm!=null)
            {maskFrm.Close();

            }

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (IsShowMaskFrm)
            {
                maskFrm = new Form();
                maskFrm.FormBorderStyle = FormBorderStyle.None;
                maskFrm.ShowInTaskbar = false;
                maskFrm.BackColor = Color.Black ;
                maskFrm.Opacity = 0.7;
                maskFrm.WindowState = FormWindowState.Maximized;
                maskFrm.Owner = this;
                //maskFrm.TopLevel = false;
                maskFrm.Show();
               
                //this.TopMost=true;

            }
        }
        //////////////////////////////////////////////////////////////////////
    }
}
