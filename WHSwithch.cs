using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WHControlLib
{
    public partial class WHSwithch : UserControl
    {
        public WHSwithch()

        {
            //设置双缓冲

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.AllPaintingInWmPaint, true);

            InitializeComponent();
        }
        #region 属性，字段，定义部分
     
        private float _hWzoom=2.0f;
        [Category("我的"), Description("控件外边框长宽比例，默认，2.0f,不能小于2.0"), Browsable(true)]
        public float HWzoom
        {
            get { return _hWzoom; }
            set {
                if (value < 2) _hWzoom = 2;

               else _hWzoom = value; Invalidate(); }
        }


        private int _outSiderBorderWidth = 3;
        [Category("我的"), Description("控件外边框 宽度，默认，3,不能小于2"), Browsable(true)]
        public int OutSiderBorderWidth
        {
            get { return _outSiderBorderWidth; }
            set
            {
                if (value < 2) _outSiderBorderWidth = 2;
                else _outSiderBorderWidth = value; Invalidate();
            }
        }

        private Color _outSiderborderColor = Color.Orange;
        [Category("我的"), Description("外边框颜色，默认，橙色"), Browsable(true)]
        public Color OutSiderborderColor
        {
            get { return _outSiderborderColor; }
            set { _outSiderborderColor = value; Invalidate(); }
        }

        private bool _isDrawOutSiderBorder = false;
        [Category("我的"), Description("是否化控件外边框，默认，false"), Browsable(true)]
        public bool IsDrawOutSiderBorder
        {
            get { return _isDrawOutSiderBorder; }
            set
            { _isDrawOutSiderBorder = value; Invalidate(); }
        }
        private bool _isDisplayText=true;
        [Category("我的"), Description("是否显示 开 或 关  文字，默认，显示"), Browsable(true)]
        public bool IsDisplayText
        {
            get { return _isDisplayText; }
            set { _isDisplayText = value; Invalidate(); }
        }


        private int _inSiderBorderWidth = 2;
        [Category("我的"), Description("控件内部对号边框宽度，默认，2"), Browsable(true)]
        public int InSiderBorderWidth
        {
            get { return _inSiderBorderWidth; }
            set
            {
                if (value < 1) _inSiderBorderWidth = 1;
                else _inSiderBorderWidth = value; Invalidate();
            }
        }

        private Color _offSideColor=Color.Gray;
        [Category("我的"), Description("控件Off那边的颜色，默认，灰色"), Browsable(true)]
        public Color OffSideColor
        {
            get { return _offSideColor; }
            set { _offSideColor = value; Invalidate(); }
        }


        private Color _onSideColor = Color.Orange;
        [Category("我的"), Description("控件ON那边的的颜色，默认，橙色"), Browsable(true)]
        public Color OnSideColor
        {
            get { return _onSideColor; }
            set { _onSideColor = value; Invalidate(); }
        }

        private Color _inCircleColor = Color.White;
        [Category("我的"), Description("控件圆圈里的颜色，默认，白色"), Browsable(true)]
        public Color InCircleColor
        {
            get { return _inCircleColor; }
            set { _inCircleColor = value; Invalidate(); }
        }
       /// <summary>
        /// 定义可供 选择形状的枚举
        /// </summary>
        public enum Shapes
        {
            [Description("圆形")]
            Circle =0,
            [Description("方形")]
            Square =1
        }

        
        /// <summary>
        /// 目前所使用的形状，默认0 圆形
        /// </summary>
      
      
        [Category("我的"), Description("可以选择的形状，默认 圆形"), Browsable(true)]
        public Shapes MyShape { get; set; }


        private float _radius=5.0f;
        [Category("我的"), Description("如果选择方形时的 圆角弧度量，默认5.0f,不能小于3"), Browsable(true)]
        public float Radius
        {
            get { return _radius; }
            set  {
                if (value<2)
                {
                    _radius = 3.0f; Invalidate();
                }
               else _radius = value; Invalidate();
            }
        }


        private bool _myOn = false;
        [Category("我的"), Description("控件ON状态，默认，false 关闭状态"), Browsable(true)]
        public bool MyOn
        {
            get { return _myOn; }
            set
            { _myOn = value; Invalidate(); }
        }



        #endregion
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.Width =Convert.ToInt32(this.Height * HWzoom) ;

        }
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (MyOn)
            {
                MyOn = false;
            }
            else
            {
                MyOn=true;
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics Myg=e.Graphics;
            Myg.SmoothingMode = SmoothingMode.AntiAlias;
            Myg.CompositingQuality = CompositingQuality.HighQuality;
            Myg.InterpolationMode = InterpolationMode.HighQualityBicubic;


            Rectangle MyRect = this.ClientRectangle;

            MyRect.Width=Convert.ToInt32 (MyRect.Height*HWzoom) ;

            //**********选择圆形的时候  画边框 和填充    
            if ((int)MyShape==0)
            {
             DrawDisplayBorder(Myg, MyRect);
           
               //画文字
               if (IsDisplayText)
            {
             DrawText(Myg, MyRect, OutSiderBorderWidth);
            }
           
            }

            //**********选择方形的时候  画边框 和填充    

            else
            {
                       DrawSquareDisplayBorder(Myg, MyRect);
                //画文字
                if (IsDisplayText)
                {
                    DrawText(Myg, MyRect, OutSiderBorderWidth);
                }

            }






            //释放资源



        }
        /// <summary>
        ///画方形时 获得控件外的圆角方形框架路径 
        /// </summary>
        /// <param name="Myrect"></param>
        /// <param name="OutSiderBorderWidth"></param>
        /// <returns></returns>
        GraphicsPath GetSquareOutBorderPath(Rectangle Myrect,int OutSiderBorderWidth)
        {
            float PenWidht = OutSiderBorderWidth / 2;
            RectangleF Leftrectangle = new RectangleF();
            RectangleF Rightrectangle = new RectangleF();
            Leftrectangle.X = Myrect.X + PenWidht;
            Leftrectangle.Y = Myrect.Y + PenWidht;
            Leftrectangle.Width = Myrect.Height - PenWidht * 2;
            Leftrectangle.Height = Leftrectangle.Width - PenWidht;
            //Rightrectangle.X = Myrect.X + Myrect.Width - Myrect.Height - PenWidht;
            Rightrectangle.X = Myrect.X + Myrect.Width - Myrect.Height;
            Rightrectangle.Y = Myrect.Y + PenWidht;

            Rightrectangle.Width = Leftrectangle.Width;
            Rightrectangle.Height = Leftrectangle.Height;
      
            GraphicsPath path = new GraphicsPath();
            return DrawHelper.GetFillRoundRectFPath(Myrect, OutSiderBorderWidth, Radius);    

            //return path;

        }
        /// <summary>
        /// 画方形时 获得控件里的圆角方形路径
        /// </summary>
        /// <param name="Myrect"></param>
        /// <param name="OutSiderBorderWidth"></param>
        /// <param name="On"></param>
        /// <returns></returns>
        GraphicsPath GetSquareCirlePath(Rectangle Myrect,int OutSiderBorderWidth,bool On)

        {

            GraphicsPath SquarPath = new GraphicsPath();
           int  PenWidht = OutSiderBorderWidth / 2;
            //  内部圆圈 是 外框 的0.85 倍数 看着比较好看 暂定以后可以添加内部圆圈的大小比例属性
            const float CircleBl = 0.8f;
            float XYBl = (1 - CircleBl) / 2;
            //float XYBl = 0.2f;
            RectangleF Leftrectangle = new RectangleF();
            RectangleF Rightrectangle = new RectangleF();
            Leftrectangle.Width = (Myrect.Height - OutSiderBorderWidth) * CircleBl;
            //Leftrectangle.Height = (Leftrectangle.Width - PenWidht)*CircleBl;
            Leftrectangle.Height = Leftrectangle.Width;


            Leftrectangle.X = Myrect.X + PenWidht*2 + Leftrectangle.Width * XYBl;
            Leftrectangle.Y = Myrect.Y + PenWidht *2+ Myrect.Height * XYBl;

          
            Rightrectangle.Width = Leftrectangle.Width;
            Rightrectangle.Height = Leftrectangle.Height;
            Rightrectangle.X = Myrect.X + Myrect.Width - Myrect.Height + Rightrectangle.Width * XYBl*2;
            Rightrectangle.Y = Myrect.Y + PenWidht + Myrect.Height * XYBl ;


            if (On)
            {
                return DrawHelper.GetFillRoundRectFPath(Rightrectangle, PenWidht, Radius);

            }
            else 
                return DrawHelper.GetFillRoundRectFPath(Leftrectangle, PenWidht, Radius);

      




        }
        /// <summary>
        /// 画圆角方形
        /// </summary>
        /// <param name="Myg"></param>
        /// <param name="Myrect"></param>
        void DrawSquareDisplayBorder(Graphics Myg,Rectangle Myrect)
        {
            Pen BorderPen = new Pen(OutSiderborderColor, OutSiderBorderWidth);
            SolidBrush offFillBrush = new SolidBrush(OffSideColor);
            SolidBrush onFillBrush = new SolidBrush(OnSideColor);
            SolidBrush incircleBrush = new SolidBrush(InCircleColor);
            GraphicsPath OutPath = GetSquareOutBorderPath(Myrect, OutSiderBorderWidth);
            GraphicsPath InSquarePath = GetSquareCirlePath(Myrect, OutSiderBorderWidth, MyOn);
            //选择开 关 状态时 的内部填充
            if (MyOn)
            {
                Myg.FillPath(onFillBrush, OutPath);
            }
            else
            {
                Myg.FillPath(offFillBrush, OutPath);
            }

            //**********选择是否描绘控件的外部边框
            //画外部边框
            if (IsDrawOutSiderBorder)
            {
                Myg.DrawPath(BorderPen, OutPath);
            }
            //填充内部方块
            Myg.FillPath(incircleBrush, InSquarePath);
            //画内部方块
            if (IsDrawOutSiderBorder)
            {
                Myg.DrawPath(BorderPen, InSquarePath);
            }



            //释放资源
            BorderPen.Dispose();
            offFillBrush.Dispose();
            onFillBrush.Dispose();
            incircleBrush.Dispose();

        }

      /// <summary>
      /// 选择圆形时候 画圆形控件
      /// </summary>
      /// <param name="Myg"></param>
      /// <param name="Myrect"></param>
        void DrawDisplayBorder(Graphics Myg,Rectangle Myrect)
        {

       
            Pen BorderPen = new Pen(OutSiderborderColor, OutSiderBorderWidth);
            SolidBrush offFillBrush = new SolidBrush(OffSideColor);
            SolidBrush onFillBrush=new SolidBrush (OnSideColor);
            SolidBrush incircleBrush = new SolidBrush(InCircleColor);
            GraphicsPath OutPath = GetOutBorderPath(Myrect, OutSiderBorderWidth);
            GraphicsPath InCirclePath = GetInsideCirlePath(Myrect, OutSiderBorderWidth, MyOn);
            if (MyOn)
            {
            
             Myg.FillPath(onFillBrush, OutPath);
             
            }
            else
            {
             Myg.FillPath(offFillBrush, OutPath);

            }
                

            if (IsDrawOutSiderBorder)
            {
             Myg.DrawPath(BorderPen,OutPath );
            }
           
           Myg.FillPath(incircleBrush, InCirclePath);

            if (IsDrawOutSiderBorder)
            {
            Myg.DrawPath(BorderPen,InCirclePath );
            }
           



            //释放资源
            BorderPen.Dispose();
            offFillBrush.Dispose();
            onFillBrush.Dispose();
            incircleBrush.Dispose();
        }
/// <summary>
/// 获得 整个 外部框架 路径
/// </summary>
/// <param name="Myrect"></param>
/// <param name="OutSideBorderWidht"></param>
/// <returns></returns>

        GraphicsPath GetOutBorderPath (Rectangle Myrect,int OutSideBorderWidht)

        {
            float PenWidht = OutSideBorderWidht / 2;
            RectangleF Leftrectangle = new RectangleF(); 
               RectangleF Rightrectangle = new RectangleF();
            Leftrectangle.X = Myrect.X+PenWidht ;
            Leftrectangle.Y = Myrect.Y + PenWidht;
            Leftrectangle.Width = Myrect.Height - PenWidht*2;
            Leftrectangle.Height = Leftrectangle.Width-PenWidht ;
            //Rightrectangle.X = Myrect.X + Myrect.Width - Myrect.Height - PenWidht;
            Rightrectangle.X = Myrect.X + Myrect.Width - Myrect.Height;
            Rightrectangle.Y = Myrect.Y + PenWidht;

            Rightrectangle.Width = Leftrectangle.Width;
            Rightrectangle.Height = Leftrectangle.Height;
            PointF leftTopPoint=new PointF(Leftrectangle.X+Leftrectangle.Width/2,Leftrectangle.Y);
            PointF rightTopPoint=new PointF(Rightrectangle.X+Rightrectangle.Width/2,Rightrectangle.Y);
            PointF leftBottomPoint = new PointF(Leftrectangle.X + Leftrectangle.Width / 2, Leftrectangle.Y + Leftrectangle.Height );
            PointF rightBottomPoint = new PointF(Rightrectangle.X + Rightrectangle.Width / 2, Rightrectangle.Y + Rightrectangle.Height ) ;
           
            GraphicsPath path = new GraphicsPath();
            path.AddArc(Leftrectangle, 90, 180);

            path.AddLine(leftTopPoint, rightTopPoint);
            path.AddArc(Rightrectangle, 270, 180);
            path.AddLine(rightBottomPoint, leftBottomPoint);
            //path.CloseFigure();

            return path;

        }
       /// <summary>
       /// 得到内部 圆圈 的大小路径
       /// </summary>
       /// <param name="Myrect">本控件的区域</param>
       /// <param name="OutSideBorderWidht">外框线宽</param>
       /// <param name="On">是否是开状态</param>
       /// <returns></returns>
        GraphicsPath GetInsideCirlePath(Rectangle Myrect, int OutSideBorderWidht, bool On)
        {
            GraphicsPath CirlePath = new GraphicsPath();
            float PenWidht = OutSideBorderWidht / 2;
           //  内部圆圈 是 外框 的0.85 倍数 看着比较好看 暂定以后可以添加内部圆圈的大小比例属性
           const float CircleBl = 0.85f;
            float XYBl = (1 - CircleBl) / 2;
            //float XYBl = 0.2f;
            RectangleF Leftrectangle = new RectangleF();
            RectangleF Rightrectangle = new RectangleF();
            Leftrectangle.Width = (Myrect.Height - PenWidht * 2)*CircleBl;
            //Leftrectangle.Height = (Leftrectangle.Width - PenWidht)*CircleBl;
            Leftrectangle.Height = Leftrectangle.Width;


            Leftrectangle.X = Myrect.X + PenWidht+Leftrectangle.Width*XYBl;
            Leftrectangle.Y = Myrect.Y + PenWidht+Leftrectangle.Height*XYBl;
    
            //Rightrectangle.X = Myrect.X + Myrect.Width - Myrect.Height - PenWidht;
              Rightrectangle.Width = Leftrectangle.Width;
            Rightrectangle.Height = Leftrectangle.Height;
            Rightrectangle.X = Myrect.X + Myrect.Width - Myrect.Height +Rightrectangle.Width * XYBl;
            Rightrectangle.Y = Myrect.Y + PenWidht+Rightrectangle.Height*XYBl;

         
            if (On)
            {
                CirlePath.AddEllipse(Rightrectangle);

            }
            else CirlePath.AddEllipse(Leftrectangle);

            return CirlePath;

        }
        /// <summary>
        /// 画显示文字
        /// </summary>
        /// <param name="Myg"></param>
        /// <param name="Myrect"></param>
        /// <param name="OutSiderBorderWidth"></param>
        void DrawText(Graphics Myg,Rectangle Myrect,int OutSiderBorderWidth)

        {
            SolidBrush TextBrush=new SolidBrush(InCircleColor);
            //Font textFont = new Font(this.Font.Name, 12);
      
             PointF L=new PointF(Myrect.X+Myrect.Width/3,Myrect.Y+Myrect.Height/2-this.Font.Height/2);
            PointF R=new PointF(Myrect.X+Myrect.Width-Myrect.Width/3,Myrect.Y + Myrect.Height / 2 - this.Font.Height / 2);

            StringFormat sf =new StringFormat ();
            sf.Alignment= StringAlignment.Center;   

            if (MyOn)
            {
                
                Myg.DrawString("开", this.Font, TextBrush, L, sf);
            }
            else
            {
                Myg.DrawString("关",this.Font, TextBrush, R, sf);
            }
            TextBrush.Dispose();

        }


        protected override void OnLayout(LayoutEventArgs e)
        {
            base.OnLayout(e);
           // 控件添加到容器后 设置字体为12号
            this.Font = new Font(Font.FontFamily, 12);
        }
        private void WHSwithch_Load(object sender, EventArgs e)
        {

        }
    }
}
