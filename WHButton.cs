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
    public partial class WHButton : Button
    {
        //设置双缓冲
        public WHButton()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.AllPaintingInWmPaint, true);
           
            InitializeComponent();
        }
        #region 属性字段定义
        private Color bornColor = Color.Blue;

        /// <summary>
        /// 边框颜色
        /// </summary>
        [Category("我的"), Description("边框颜色"), Browsable(true)]
        public Color BornColor
        {
            get { return bornColor; }
            set { bornColor = value; this.Invalidate(); }
        }
        private float radius = 5.0f;

        /// <summary>
        /// 圆角大小比例
        /// </summary>
        [Category("我的"), Description(" 圆角大小比例默认5.0,值越大圆角比例越小，不能小于2"), Browsable(true)]
        public float Radius
        {
            get { return radius; }
            set
            {
                if (value >= 1.0f)
                {
                    radius = value;
                    this.Invalidate();
                }
                else {    radius = 1.0f; this.Invalidate(); } 
                
            }
        }
        /// <summary>
        /// 背景填充色
        /// </summary>
        private Color _fillcolor = Color.Orange;
        [Category("我的"), Description(" 背景填充色"), Browsable(true)]
        public Color Fillcolor
        {
            get { return _fillcolor; }
            set { _fillcolor = value;
                //控件设置填充颜色时候，就将变亮色和原色分开存储
                ThisChangeLight(); 
                this.Invalidate(); }
        }
        private Color fontColor = Color.Black;
        [Category("我的"), Description(" 字体颜色，默认黑色"), Browsable(true)]
        public Color FontColor
        {
            get { return fontColor; }
            set { fontColor = value; this.Invalidate(); }
        }

        private bool isHightLight=true;
        [Category("我的"), Description("是否添加高光，默认True"), Browsable(true)]
        public bool IsHightLight
        {
            get { return isHightLight; }
            set { isHightLight= value; this.Invalidate(); }
        }

        [Category("我的"), Description("是否有边框"), Browsable(true)]
        public bool IsDrawBoin { get; set; }
        /// <summary>
        /// 储存控件变亮后的填充颜色
        /// </summary>
        private Color TempFillcolor;
        /// <summary>
        /// 储存控件原来的填充色
        /// </summary>
        private Color OrgoinFillcolor;

        #endregion



        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);



            Graphics g = pevent.Graphics;

            SolidBrush backgroundBrush = new SolidBrush(this.Parent.BackColor);
            SolidBrush FillBrush = new SolidBrush(Fillcolor);
            SolidBrush FontBrush = new SolidBrush(FontColor);

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            
            Pen BornPen = new Pen(BornColor, 2);
            Rectangle MyRect = this.ClientRectangle;

            //画控件

            GraphicsPath FillPath = GetMyRegion(MyRect);
            g.FillRectangle(backgroundBrush, MyRect);

            g.FillPath(FillBrush, FillPath);
            //画边框
            if (IsDrawBoin)
            {

                DrawBoin(g, BornPen, MyRect);
            }
            //添加高光
            if (IsHightLight) SetMyLightOn(g, MyRect);



            //添加阴影
            //SetMyShadow(g, MyRect);

            //画文字

            DrawText(g, FontBrush, MyRect);



            //添加阴影



            //手工释放资源没有用 using
            backgroundBrush.Dispose();
            FillBrush.Dispose();
            FontBrush.Dispose();
            BornPen.Dispose();
          

        }
       /// <summary>
       /// 画文字
       /// </summary>
       /// <param name="g"></param>
       /// <param name="FontBrush"></param>
       /// <param name="MyRect"></param>
        private void DrawText(Graphics g, SolidBrush FontBrush, Rectangle MyRect)
        {
            Rectangle TexRect = MyRect;
            StringFormat sf = new StringFormat();
            //格式化显示文本 指定在工作矩形的中心显示
            if (this.TextAlign == ContentAlignment.MiddleCenter) sf.Alignment = StringAlignment.Center;
            if (this.TextAlign == ContentAlignment.MiddleLeft) sf.Alignment = StringAlignment.Near;
            if (this.TextAlign == ContentAlignment.MiddleRight) sf.Alignment = StringAlignment.Far;

            sf.LineAlignment = StringAlignment.Center;
            g.DrawString(this.Text, this.Font, FontBrush, TexRect, sf);
           
            //也可以用效果不好
            //TextRenderer.DrawText(g, this.Text, this.Font, pevent.ClipRectangle, FontColor);
        }
        /// <summary>
        /// 画边框
        /// </summary>
        /// <param name="g"></param>
        /// <param name="BornPen"></param>
        /// <param name="Myrect"></param>
        private void DrawBoin(Graphics g, Pen BornPen, Rectangle Myrect)

        {

            float RadiusWidth = Myrect.Width / Radius;
            //int RadiusHeight = Myrect.Height / Radius;
            float RadiusHeight = RadiusWidth;
            //定义画弧线的四个角的矩形

            RectangleF LeftTopRect = new RectangleF(Myrect.X, Myrect.Y, RadiusWidth, RadiusHeight);
            RectangleF RightTopRect = new RectangleF(Myrect.X + Myrect.Width - RadiusWidth - 1, Myrect.Y, RadiusWidth, RadiusHeight);
            RectangleF RightButtonRect = new RectangleF(Myrect.X + Myrect.Width - RadiusWidth - 1, Myrect.Y + Myrect.Height - RadiusHeight - 1, RadiusWidth, RadiusHeight);
            RectangleF LeftButtomRect = new RectangleF(Myrect.X, Myrect.Y + Myrect.Height - RadiusHeight - 1, RadiusWidth, RadiusHeight);
            //定义需要连接的8给点

            PointF LeftTopPointH = new PointF((float)Myrect.X + LeftTopRect.Width / 2, (float)Myrect.Y);
            PointF LeftTopPointV = new PointF((float)Myrect.X, (float)Myrect.Y + LeftTopRect.Height / 2);

            PointF RightTopPointH = new PointF((float)Myrect.X + Myrect.Width - RightTopRect.Width / 2, (float)Myrect.Y);
            PointF RightTopPointV = new PointF((float)Myrect.X + Myrect.Width - 1, (float)Myrect.Y + RightTopRect.Height / 2);

            PointF RightButtonPointV = new PointF((float)Myrect.X + Myrect.Width - 1, (float)Myrect.Y + Myrect.Height - RightButtonRect.Height / 2);
            PointF RightButtonPointH = new PointF((float)Myrect.X + Myrect.Width - RightButtonRect.Width / 2, (float)Myrect.Y + Myrect.Height - 1);

            PointF LeftButtonPointH = new PointF((float)Myrect.X + LeftButtomRect.Width / 2, (float)Myrect.Y + Myrect.Height - 1);
            PointF LeftButtonPointV = new PointF((float)Myrect.X, (float)Myrect.Y + Myrect.Height - LeftButtomRect.Height / 2);
            //画边框

            g.DrawArc(BornPen, LeftTopRect, 180, 90);
            g.DrawLine(BornPen, LeftTopPointH, RightTopPointH);
            g.DrawArc(BornPen, RightTopRect, 270, 90);
            g.DrawLine(BornPen, RightTopPointV, RightButtonPointV);
            g.DrawArc(BornPen, RightButtonRect, 0, 90);
            g.DrawLine(BornPen, RightButtonPointH, LeftButtonPointH);
            g.DrawArc(BornPen, LeftButtomRect, 90, 90);
            g.DrawLine(BornPen, LeftButtonPointV, LeftTopPointV);



        }

        GraphicsPath GetMyRegion(Rectangle Myrect)
        {
            GraphicsPath MyPath = new GraphicsPath();
            float RadiusWidth = Myrect.Width / Radius;
            //int RadiusHeight = Myrect.Height / Radius;
            float RadiusHeight = RadiusWidth;
            //定义画弧线的四个角的矩形
            RectangleF LeftTopRect = new RectangleF(Myrect.X, Myrect.Y, RadiusWidth, RadiusHeight);

            RectangleF RightTopRect = new RectangleF(Myrect.X + Myrect.Width - RadiusWidth - 1, Myrect.Y, RadiusWidth, RadiusHeight);
            RectangleF RightButtonRect = new RectangleF(Myrect.X + Myrect.Width - RadiusWidth - 1, Myrect.Y + Myrect.Height - RadiusHeight - 1, RadiusWidth, RadiusHeight);
            RectangleF LeftButtomRect = new RectangleF(Myrect.X, Myrect.Y + Myrect.Height - RadiusHeight - 1, RadiusWidth, RadiusHeight);
            //定义需要连接的8给点
            PointF LeftTopPointH = new PointF((float)Myrect.X + LeftTopRect.Width / 2, (float)Myrect.Y);
            PointF LeftTopPointV = new PointF((float)Myrect.X, (float)Myrect.Y + LeftTopRect.Height / 2);

            PointF RightTopPointH = new PointF((float)Myrect.X + Myrect.Width - RightTopRect.Width / 2, (float)Myrect.Y);
            PointF RightTopPointV = new PointF((float)Myrect.X + Myrect.Width - 1, (float)Myrect.Y + RightTopRect.Height / 2);

            PointF RightButtonPointV = new PointF((float)Myrect.X + Myrect.Width - 1, (float)Myrect.Y + Myrect.Height - RightButtonRect.Height / 2);
            PointF RightButtonPointH = new PointF((float)Myrect.X + Myrect.Width - RightButtonRect.Width / 2, (float)Myrect.Y + Myrect.Height - 1);

            PointF LeftButtonPointH = new PointF((float)Myrect.X + LeftButtomRect.Width / 2, (float)Myrect.Y + Myrect.Height - 1);
            PointF LeftButtonPointV = new PointF((float)Myrect.X, (float)Myrect.Y + Myrect.Height - LeftButtomRect.Height / 2);
            //添加到 自定义图形
            MyPath.AddArc(LeftTopRect, 180, 90);
            MyPath.AddLine(LeftTopPointH, RightTopPointH);
            MyPath.AddArc(RightTopRect, 270, 90);
            MyPath.AddLine(RightTopPointV, RightButtonPointV);
            MyPath.AddArc(RightButtonRect, 0, 90);
            MyPath.AddLine(RightButtonPointH, LeftButtonPointH);
            MyPath.AddArc(LeftButtomRect, 90, 90);
            MyPath.AddLine(LeftButtonPointV, LeftTopPointV);



            return MyPath;
        }
        void SetMyLightOn(Graphics g, Rectangle Myrect)

        {
            //// (实心刷)
            //Rectangle myrect1 = new Rectangle(20, 80, 250, 100);
            //SolidBrush mysbrush1 = new SolidBrush(Color.DarkOrchid);
            //SolidBrush mysbrush2 = new SolidBrush(Color.Aquamarine);
            //SolidBrush mysbrush3 = new SolidBrush(Color.DarkOrange);

            ////(梯度刷)
            //LinearGradientBrush mylbrush5 = new LinearGradientBrush(rect1,
            //Color.DarkOrange, Color.Aquamarine,
            //LinearGradientMode.BackwardDiagonal);

            ////(阴影刷)
            //HatchBrush myhbrush5 = new HatchBrush(HatchStyle.DiagonalCross,
            // Color.DarkOrange, Color.Aquamarine);
            //HatchBrush myhbrush2 = new HatchBrush(HatchStyle.DarkVertical,
            //Color.DarkOrange, Color.Aquamarine);
            //HatchBrush myhbrush3 = new HatchBrush(HatchStyle.LargeConfetti,
            //Color.DarkOrange, Color.Aquamarine);

            const int bl = 6;
            Color lightColor = new Color();
            int r = _fillcolor.R + 150;
            if (r > 255)r = 255;
            int gg=_fillcolor.G + 150;
            if (gg > 255) gg = 255;
            int b=_fillcolor.B + 150;
            if (b > 255) b = 255;
            lightColor=Color.FromArgb(r,gg,b);
        

            float RadiusWidth = Myrect.Width / Radius;
            float RadiusHeight = RadiusWidth;
            //顶部高亮区域坐标
            float TopLightX = Myrect.X + RadiusWidth / bl;
            float TopLightY = Myrect.Y + RadiusWidth / bl;
            float TopRightLightX = Myrect.X + Myrect.Width - RadiusWidth - RadiusWidth / bl;
            float TopRightLightY = Myrect.Y + RadiusHeight / bl;
            //底部高亮区域坐标
            float buttomLightX = Myrect.X + RadiusWidth / bl;
            float ButtomLightY = Myrect.Y + Myrect.Height - RadiusHeight - RadiusHeight / bl;
            //************顶部左边高亮***************************
            GraphicsPath gLightPath = new GraphicsPath();
            if (TopLightX > Myrect.Width)
            {
                TopLightX = Myrect.X;
            }
            if (TopLightY > Myrect.Height) TopLightX = Myrect.Y;
            RectangleF LeftTopRect = new RectangleF(TopLightX, TopLightY, RadiusWidth, RadiusHeight);
            gLightPath.AddArc(LeftTopRect, 180, 90);

            LinearGradientBrush Topbrush = new LinearGradientBrush(LeftTopRect, lightColor, this._fillcolor, 90);
            g.FillPath(Topbrush, gLightPath);
            //***********顶部右边高亮***********
            GraphicsPath gTopRightLightPath = new GraphicsPath();
            if (TopRightLightX < 0)
            {
                TopRightLightX = 0;
            }
            if (TopRightLightY < 0) TopRightLightY = 0;

            RectangleF RightTopRect = new RectangleF(TopRightLightX, TopRightLightY, RadiusWidth, RadiusHeight);
            gTopRightLightPath.AddArc(RightTopRect, 270, 90);


            g.FillPath(Topbrush, gTopRightLightPath);

            //***********底部右边高亮***********
            GraphicsPath gbuttompath = new GraphicsPath();
            LinearGradientBrush Bouttombrush = new LinearGradientBrush(LeftTopRect, this._fillcolor, lightColor, 270);
            RectangleF LeftButtomRect = new RectangleF(buttomLightX, ButtomLightY, RadiusWidth, RadiusHeight);
            gbuttompath.AddArc(LeftButtomRect, 90, 90);
            g.FillPath(Bouttombrush, gbuttompath);

            //***********************
          
            Topbrush.Dispose();
            Bouttombrush.Dispose();
        }
        void SetMyShadow(Graphics g, Rectangle Myrect)
          {
            float BL = radius;
            const int s = 20;
            Rectangle rct = new Rectangle();
            rct.X = Myrect.X-s;
            rct.Y = Myrect.Y-s;
            if (rct.Y < 0) rct.Y = 0;
            if (rct.X < 0) rct.X = 0;
            rct.Width = Myrect.Width + s;
            rct.Height = Myrect.Height + s;
            g.DrawRectangle(Pens.Black, rct);
           

           }

      /// <summary>
      /// 使控件颜色变亮
      /// </summary>
     private   void ThisChangeLight()
        {
         //将控件颜色变亮
            TempFillcolor = _fillcolor;
            OrgoinFillcolor = _fillcolor;

            Color c = this.TempFillcolor;
            int r = c.R + 50;
            if (r > 255) r = 255;
         
            int g =  c.G+40;
            if (g > 255) g = 255;
       
            int b=  c.B+30;
            if (b > 255) b = 255;


            TempFillcolor = Color.FromArgb(r, g, b);
            //this.Invalidate();

        }
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            //ThisChangeLight();
            //ThisChangeLight();

            this._fillcolor = TempFillcolor;
            this.Invalidate();
        }
        void IfUseAutoSize()
        {
            if (AutoSize)
            {
           int height = this.FontHeight;
            this.Height = height + 10;

            Graphics gf=this.CreateGraphics();
            SizeF Fontwidth=gf.MeasureString(this.Text,this.Font);
            this.Width = Convert.ToInt32(Fontwidth.Width)+10;
                gf.Dispose();
            this.Invalidate();
            }
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            IfUseAutoSize();
        }
        protected override void OnAutoSizeChanged(EventArgs e)
        {
            base.OnAutoSizeChanged(e);
            IfUseAutoSize();


        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            //恢复控件原颜色
            this._fillcolor = OrgoinFillcolor;
            this.Invalidate();
            //this._fillcolor = TempFillcolor;
           
        }


        protected override void OnMouseDown(MouseEventArgs mevent)
        {
          
             base.OnMouseDown(mevent);
            //恢复控件原颜色
            _fillcolor = OrgoinFillcolor;
            this.Invalidate();
            
           
           
        }
        protected override void OnMouseUp(MouseEventArgs mevent)
        {
          
            base.OnMouseUp(mevent);
       
            //判断鼠标是否在当前控件上 如果在就变亮不在就变回原色
         
            if (  this.RectangleToScreen(this.ClientRectangle).Contains(MousePosition))
            {
           this._fillcolor = TempFillcolor;
            }
            else
            {
                _fillcolor = OrgoinFillcolor;
            }
           
            this.Invalidate();
        }
       
    }
}
