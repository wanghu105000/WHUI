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
    public partial class WHLable : Label
    {
        public WHLable()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                             ControlStyles.ResizeRedraw |
                             ControlStyles.AllPaintingInWmPaint, true);
            
            InitializeComponent();
           
        }
        #region 属性，字段 定义
        //bool _autoSize=false;
        //[Category("我的"), Description("是否自动大小，默认，否"), Browsable(true)]
        //public override bool AutoSize
        //{ get { return _autoSize; }
        //    set { _autoSize = value; } }
        //{ get => _autoSize; set =>  value; }



        Rectangle MyRect;


        Rectangle MyBorderRect = new Rectangle();

        private int borderWidth = 3;
        [Category("我的"), Description("边框宽度，默认为1不能小于0,边框越大，控件填充区越小，与角标连用可调节角标高度"), Browsable(true)]
        public int BorderWidth
        {
            get { return borderWidth; }
            set
            {
                if (value < 0)
                {
                    borderWidth = 0;
                }
                else
                    borderWidth = value;
            }
        }

        private Color borderColor = Color.Orange;
        [Category("我的"), Description("边框颜色，默认橙色"), Browsable(true)]
        public Color BorderColor
        {
            get { return borderColor; }
            set { borderColor = value; }
        }

        private bool isDrawBorder = true;
        [Category("我的"), Description("是否有边框，默认有边框"), Browsable(true)]
        public bool IsDrawBorder
        {
            get { return isDrawBorder; }
            set { isDrawBorder = value;
                Invalidate();
            }
          
        }

        private bool isDrawRadiusBorder = true;
        [Category("我的"), Description("是否画圆角边框，默认是圆角边框"), Browsable(true)]
        public bool IsDrawRadiusBorder
        {
            get { return isDrawRadiusBorder; }
            set { isDrawRadiusBorder = value;
                Invalidate();
            }
        }

        private float radius = 5.0f;
        [Category("我的"), Description("圆角大小比例默认5.0f,值越大圆角比例越小，不能小于2"), Browsable(true)]
        public float Radius
        {
            get { return radius; }
            set
            {
                if (value > 2.0f)radius = value;
             
                else
              
                    radius = 2.0f;
                Invalidate();




            }
        }
        private bool isFillRadiusColor=false;
        [Category("我的"), Description("是否允许内部圆角矩形填充色，默认为否,为true后不会以矩形方式填充"), Browsable(true)]
        public bool IsFillRadiusColor
        {
            get { return isFillRadiusColor; }
            set { isFillRadiusColor = value;
                Invalidate();
            }
            
        }

        private Color fillColor=Color.Orange;
        [Category("我的"), Description("内部如果允许填充后的填充色，默认为橙色"), Browsable(true)]
        public Color FillColor
        {
            get { return fillColor; }
            set { fillColor = value;
                Invalidate();
            }
            
        }

        private bool isFillRectangeColor = false;
        [Category("我的"), Description("内部以矩形填充色，与允许以圆角矩形填充方式互斥，默认为橙色"), Browsable(true)]
        public bool IsFillRectangeColor
        {
            get { return isFillRectangeColor ; }
            set { isFillRectangeColor  = value;
                Invalidate();
            }
        }

        private bool isDrawMarker=false;
        [Category("我的"), Description("是否开启右上角的角标，默认 否"), Browsable(true)]
        public bool IsDrawMarker
        {
            get { return isDrawMarker; }
            set { isDrawMarker= value; Invalidate(); }
        }


        private Color markerColor=Color.Red;
        [Category("我的"), Description("角标的颜色，默认 红色"), Browsable(true)]
        public Color MarkerColor
        {
            get { return markerColor; }
            set { markerColor = value; Invalidate(); }
        }

        private string markerText= "";
        [Category("我的"), Description("角标显示的文字，默认 无 提示：最好汉字一个，数字最多2个，不要太多"), Browsable(true)]
        public string MarkerText 
        {
            get { return markerText; }
            set { markerText = value; Invalidate(); }
        }

        private Color markerTextColor=Color.White;
        [Category("我的"), Description("角标文字的颜色，默认  白色"), Browsable(true)]
        public Color MarkerTextColor
        {
            get { return markerTextColor; }
            set { markerTextColor = value; Invalidate(); }
        }

        private float  markerSize=3.0f;
        [Category("我的"), Description("角标大小，默认  3.0 ,数值越大角标越小，与borderWidth连用可调节角标高度"), Browsable(true)]
        public float MarkerSize
        {
            get { return markerSize; }
            set { markerSize = value; Invalidate(); }
        }
       
        private int markerTextSzie=10;
 [Category("我的"), Description("角标文字的大小，默认  10 ,数值越大文字越大"), Browsable(true)]
        public int MarkerTextSzie
        {
            get { return markerTextSzie; }
            set
            {
                if (value <= 0) markerTextSzie = 10; 
           
                else  markerTextSzie = value;

                Invalidate();

            }
        }

        #endregion

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            AutoSize = false;
        }
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            SolidBrush backgroundBrush = new SolidBrush(this.Parent.BackColor);


            Graphics Myg = pe.Graphics;
            Myg.SmoothingMode = SmoothingMode.AntiAlias;
            Myg.CompositingQuality = CompositingQuality.HighQuality;
            Myg.InterpolationMode = InterpolationMode.HighQualityBicubic;

            MyRect = this.ClientRectangle;

           //*********初始背景填充为父容器************
            Myg.FillRectangle(backgroundBrush, MyRect);
            //******是否填充内部色***********
            
            if (IsFillRectangeColor)
            {
               
                Myg.Clear(this.Parent.BackColor);
                FillRectageInsideColor(Myg, MyRect);
            }
                if (IsFillRadiusColor)
            {
                //IsFillRectangeColor = false;
                Myg.Clear(this.Parent.BackColor);
                FillRadiusInsideColor(Myg, MyRect);

            }

            //************画边框************
            if (IsDrawBorder)
            {
                if (IsDrawRadiusBorder)
                {
                    DrawRadiusBorder(Myg, MyRect);
                }
                else
                {
                    DrawGeneralBorder(Myg, MyRect);
                }
            }
            //**********画文字**********
            DrawText(Myg, MyRect);

            //*******画角标*******
            if (IsDrawMarker)
            {
             DrawConverMarker(Myg, MyRect);  
            DrawMarkerText(Myg, MyRect);
            }
            //*******画角标的文字*******
           

            ///////**************释放资源***************
            backgroundBrush.Dispose();
        }

        /// <summary>
        /// 画普通直角边框
        /// </summary>
        /// <param name="Myg"></param>
        /// <param name="MyRect"></param>
        void DrawGeneralBorder(Graphics Myg, Rectangle MyRect)

        {
            SolidBrush BorderBrush = new SolidBrush(BorderColor);
            Pen BorderPen = new Pen(BorderBrush, borderWidth);
            
            MyBorderRect.X = MyRect.X;
            MyBorderRect.Y = MyRect.Y;
            
            
            Myg.DrawRectangle(BorderPen, MyRect);


            BorderBrush.Dispose();
            BorderPen.Dispose();

        }
        /// <summary>
        /// 画圆角边框
        /// </summary>
        /// <param name="Myg"></param>
        /// <param name="Myrect"></param>
        void DrawRadiusBorder(Graphics Myg, Rectangle Myrect)
        {
            SolidBrush BorderBrush = new SolidBrush(BorderColor);
            Pen BorderPen = new Pen(BorderBrush, borderWidth);

            float RadiusWidth = Myrect.Width / Radius;
            //int RadiusHeight = Myrect.Height / Radius;
            float RadiusHeight = RadiusWidth;
            //定义画弧线的四个角的矩形

            RectangleF LeftTopRect = new RectangleF(Myrect.X + borderWidth, Myrect.Y + borderWidth, RadiusWidth, RadiusHeight);
            RectangleF RightTopRect = new RectangleF(Myrect.X + Myrect.Width - RadiusWidth - borderWidth, Myrect.Y + borderWidth, RadiusWidth, RadiusHeight);
            RectangleF RightButtonRect = new RectangleF(Myrect.X + Myrect.Width - RadiusWidth - borderWidth, Myrect.Y + Myrect.Height - RadiusHeight - borderWidth, RadiusWidth, RadiusHeight);
            RectangleF LeftButtomRect = new RectangleF(Myrect.X + borderWidth, Myrect.Y + Myrect.Height - RadiusHeight - borderWidth, RadiusWidth, RadiusHeight);
            //定义需要连接的8给点

            PointF LeftTopPointH = new PointF((float)Myrect.X + LeftTopRect.Width / 2, (float)Myrect.Y+ borderWidth);
            PointF LeftTopPointV = new PointF((float)Myrect.X + borderWidth, (float)Myrect.Y + LeftTopRect.Height / 2);

            PointF RightTopPointH = new PointF((float)Myrect.X + Myrect.Width - RightTopRect.Width / 2, (float)Myrect.Y + borderWidth);
            PointF RightTopPointV = new PointF((float)Myrect.X + Myrect.Width - borderWidth, (float)Myrect.Y + RightTopRect.Height / 2);

            PointF RightButtonPointV = new PointF((float)Myrect.X + Myrect.Width - borderWidth, (float)Myrect.Y + Myrect.Height - RightButtonRect.Height / 2);
            PointF RightButtonPointH = new PointF((float)Myrect.X + Myrect.Width - RightButtonRect.Width / 2, (float)Myrect.Y + Myrect.Height - borderWidth);

            PointF LeftButtonPointH = new PointF((float)Myrect.X + LeftButtomRect.Width / 2, (float)Myrect.Y + Myrect.Height - borderWidth);
            PointF LeftButtonPointV = new PointF((float)Myrect.X + borderWidth, (float)Myrect.Y + Myrect.Height - LeftButtomRect.Height / 2);
            //画边框

            Myg.DrawArc(BorderPen, LeftTopRect, 180, 90);
            Myg.DrawLine(BorderPen, LeftTopPointH, RightTopPointH);
            Myg.DrawArc(BorderPen, RightTopRect, 270, 90);
            Myg.DrawLine(BorderPen, RightTopPointV, RightButtonPointV);
            Myg.DrawArc(BorderPen, RightButtonRect, 0, 90);
            Myg.DrawLine(BorderPen, RightButtonPointH, LeftButtonPointH);
            Myg.DrawArc(BorderPen, LeftButtomRect, 90, 90);
            Myg.DrawLine(BorderPen, LeftButtonPointV, LeftTopPointV);
            BorderBrush.Dispose();
            BorderPen.Dispose();

        }

       /// <summary>
       /// 画文字
       /// </summary>
       /// <param name="Myg"></param>
       /// <param name="MyRect"></param>
        void DrawText(Graphics Myg,Rectangle MyRect)
        {
            SolidBrush FontBrush=new SolidBrush (this.ForeColor);
            Rectangle TexRect = MyRect;
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            //格式化显示文本 指定在工作矩形的中心显示
            if (this.TextAlign == ContentAlignment.MiddleCenter) sf.Alignment = StringAlignment.Center;
            if (this.TextAlign == ContentAlignment.MiddleLeft) sf.Alignment = StringAlignment.Near;
            if (this.TextAlign == ContentAlignment.MiddleRight) sf.Alignment = StringAlignment.Far;

            sf.LineAlignment = StringAlignment.Center;
            Myg.DrawString(this.Text, this.Font, FontBrush, TexRect, sf);

        }
        /// <summary>
        /// 以圆角矩形的方式填充颜色
        /// </summary>
        /// <param name="Myg"></param>
        /// <param name="Myrect"></param>
        void FillRadiusInsideColor(Graphics Myg ,Rectangle Myrect)
        {
            SolidBrush backgroundBrush = new SolidBrush(this.Parent.BackColor);
            Myg.FillRectangle(backgroundBrush, MyRect);


            SolidBrush FillBrush = new SolidBrush(FillColor);
            //Pen BorderPen = new Pen(BorderBrush, borderWidth);

            float RadiusWidth = Myrect.Width / Radius;
            //int RadiusHeight = Myrect.Height / Radius;
            float RadiusHeight = RadiusWidth;
            //定义画弧线的四个角的矩形

            RectangleF LeftTopRect = new RectangleF(Myrect.X + borderWidth, Myrect.Y + borderWidth, RadiusWidth, RadiusHeight);
            RectangleF RightTopRect = new RectangleF(Myrect.X + Myrect.Width - RadiusWidth - borderWidth, Myrect.Y + borderWidth, RadiusWidth, RadiusHeight);
            RectangleF RightButtonRect = new RectangleF(Myrect.X + Myrect.Width - RadiusWidth - borderWidth, Myrect.Y + Myrect.Height - RadiusHeight - borderWidth, RadiusWidth, RadiusHeight);
            RectangleF LeftButtomRect = new RectangleF(Myrect.X + borderWidth, Myrect.Y + Myrect.Height - RadiusHeight - borderWidth, RadiusWidth, RadiusHeight);
            //定义需要连接的8给点

            PointF LeftTopPointH = new PointF((float)Myrect.X + LeftTopRect.Width / 2, (float)Myrect.Y + borderWidth);
            PointF LeftTopPointV = new PointF((float)Myrect.X + borderWidth, (float)Myrect.Y + LeftTopRect.Height / 2);

            PointF RightTopPointH = new PointF((float)Myrect.X + Myrect.Width - RightTopRect.Width / 2, (float)Myrect.Y + borderWidth);
            PointF RightTopPointV = new PointF((float)Myrect.X + Myrect.Width - borderWidth, (float)Myrect.Y + RightTopRect.Height / 2);

            PointF RightButtonPointV = new PointF((float)Myrect.X + Myrect.Width - borderWidth, (float)Myrect.Y + Myrect.Height - RightButtonRect.Height / 2);
            PointF RightButtonPointH = new PointF((float)Myrect.X + Myrect.Width - RightButtonRect.Width / 2, (float)Myrect.Y + Myrect.Height - borderWidth);

            PointF LeftButtonPointH = new PointF((float)Myrect.X + LeftButtomRect.Width / 2, (float)Myrect.Y + Myrect.Height - borderWidth);
            PointF LeftButtonPointV = new PointF((float)Myrect.X + borderWidth, (float)Myrect.Y + Myrect.Height - LeftButtomRect.Height / 2);
            //构造填充路径
            GraphicsPath myfillPath = new GraphicsPath();
            myfillPath.AddArc(LeftTopRect, 180, 90);
            myfillPath.AddLine(LeftTopPointH, RightTopPointH);
            myfillPath.AddArc(RightTopRect, 270, 90);
            myfillPath.AddLine(RightTopPointV, RightButtonPointV);
            myfillPath.AddArc(RightButtonRect, 0, 90);
            myfillPath.AddLine(RightButtonPointH, LeftButtonPointH);
            myfillPath.AddArc(LeftButtomRect, 90, 90);
            myfillPath.AddLine(LeftButtonPointV, LeftTopPointV);

            
            Myg.FillPath(FillBrush, myfillPath);
            backgroundBrush.Dispose();
            FillBrush.Dispose();


        }
        /// <summary>
        /// 以矩形的方式填充颜色
        /// </summary>
        /// <param name="Myg"></param>
        /// <param name="Myrect"></param>
        void FillRectageInsideColor(Graphics Myg,Rectangle Myrect)
        {
            SolidBrush backgroundBrush = new SolidBrush(this.Parent.BackColor);
            Myg.FillRectangle(backgroundBrush, MyRect);

            SolidBrush FillBrush=new SolidBrush (FillColor);
            Myg.FillRectangle(FillBrush, Myrect);
            backgroundBrush.Dispose();
            FillBrush.Dispose();
        }
       /// <summary>
       /// 画圆形角标并填充
       /// </summary>
       /// <param name="Myg"></param>
       /// <param name="MyRect"></param>
        void DrawConverMarker(Graphics Myg,Rectangle MyRect)
        {
             SolidBrush MarierBrush=new SolidBrush(MarkerColor);
           //得到圆形角标的半径
            float R = MyRect.Height / MarkerSize;
          
            RectangleF rct = new RectangleF(MyRect.X + MyRect.Width - R, MyRect.Y, R, R);
           
            Myg.FillEllipse(MarierBrush, rct);
            MarierBrush.Dispose();

        }
        

        void DrawMarkerText(Graphics Myg,Rectangle MyRect )
        {
            SolidBrush MarkerTextBrush=new SolidBrush(MarkerTextColor);
            float R = MyRect.Height / MarkerSize;
            //int texsize = Convert.ToInt32(MyRect.Height / 10);

            Font f = new Font("粗体", MarkerTextSzie);

            RectangleF TexRect = new RectangleF(MyRect.X + MyRect.Width - R, MyRect.Y+borderWidth, R, R);
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
           
            Myg.DrawString(markerText, f, MarkerTextBrush, TexRect, sf);

            f.Dispose();
            MarkerTextBrush.Dispose();

        }
           
    }
}
