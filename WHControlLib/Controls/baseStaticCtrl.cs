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
        [DefaultEvent("Click")]
        [DefaultProperty("Text")]
    [ToolboxItem(false)]
    public partial class baseStaticCtrl : UserControl
    {
        public baseStaticCtrl()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                  ControlStyles.ResizeRedraw |
                  ControlStyles.AllPaintingInWmPaint, true);
            InitializeComponent();
        }

        //全局定义

        /// <summary>
        /// 控件整体的矩形大小
        /// </summary>
        Rectangle MyRect = new Rectangle();
      /// <summary>
      /// 需要画出控件的矩形大小因为控件本身是透明色，为了消除锯齿现象所以画出的控件 小于 本控件的实际大小。
      /// </summary>
        Rectangle DrawRect = new Rectangle();
       /// <summary>
       /// 鼠标是否停留在控件上的标志
       /// </summary>
        bool IsMouseOnFlag=false;
        bool IsMouseOverFlag=false;
        bool    IsMouseLeaveFlag=false;
        bool IsMouseClickFlag=false;
        //********************
        #region 属性字段定义
      public  enum FillColorDec
        {
            Vertical,
            Horizontal,
            LeftVH,
            RightVH

        }
       public enum Shape
        {
                RoundRectange,
                HalfCircle,
                Rectange
        }

       public enum TextAlign
        {
            Center,Left,Right
        }


         ////当用隐藏的Text属性时必须这么加特性才能使本控件使用
        [Category("A我的"), Description("文字在控件上显示文字，默认"), Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text { get; set; }

        private TextAlign _myTextAlign=TextAlign.Center;
        [Category("A我的"), Description("文字在控件上显示的对齐方式，默认，中间对齐"), Browsable(true)]
        public TextAlign MyTextAlign
        {
            get { return _myTextAlign; }
            set { _myTextAlign = value; Invalidate(); }
        }



        private Shape  _myShape=Shape.RoundRectange;
        [Category("A我的"), Description("要显示的控件的形状，默认，圆角矩形"), Browsable(true)]
        public Shape MyShape
        {
            get { return _myShape; }
            set { _myShape = value; Invalidate(); }
        }
        private FillColorDec _myFillColorDec=FillColorDec.Vertical;
        [Category("A我的"), Description("控件填充渐变色的方向，默认，上下方向"), Browsable(true)]
        public FillColorDec MyFillColorDec
        {
            get { return _myFillColorDec; }
            set { _myFillColorDec = value; Invalidate(); }
        }


        private Color bornColor = Color.Blue;

        /// <summary>
        /// 边框颜色
        /// </summary>
        [Category("A我的"), Description("边框颜色"), Browsable(true)]
        public Color BornColor
        {
            get { return bornColor; }
            set { bornColor = value; this.Invalidate(); }
        }
        private float radius = 5.0f;

        /// <summary>
        /// 圆角大小比例
        /// </summary>
        [Category("A我的"), Description(" 圆角大小比例默认5.0,值越大圆角比例越小，不能小于1.1"), Browsable(true)]
        public float Radius
        {
            get { return radius; }
            set
            {
                if (value >1.1f)
                {
                    radius = value;
                    this.Invalidate();
                }
                else { radius = 1.1f; this.Invalidate(); }

            }
        }
        /// <summary>
        /// 第一种背景填充色
        /// </summary>
        private Color _firstFillcolor = Color.Orange;
        [Category("A我的"), Description(" 第一种背景填充色"), Browsable(true)]
        public Color FirstFillcolor
        {
            get { return _firstFillcolor; }
            set
            {
                _firstFillcolor = value;
             
                this.Invalidate();
            }
        }
        private Color _secondFillcolor = Color.Orange;
        [Category("A我的"), Description(" 第二种背景填充色"), Browsable(true)]
        public Color SecondFillcolor
        {
            get { return _secondFillcolor; }
            set
            {
                _secondFillcolor = value;

                this.Invalidate();
            }
        }

        private Color fontColor = Color.Black;
        [Category("A我的"), Description(" 字体颜色，默认黑色"), Browsable(true)]
        public Color FontColor
        {
            get { return fontColor; }
            set { fontColor = value; this.Invalidate(); }
        }

        private Font _myFont=new Font("微软雅黑", 12.0f, 0, GraphicsUnit.Point, 1);
        [Category("A我的"), Description(" 控件字体，默认 微软雅黑12t"), Browsable(true)]
        public Font MyFont
        {
            get { return _myFont; }
            set { _myFont = value; this.Invalidate(); }
        }
  

        [Category("A我的"), Description("是否有边框"), Browsable(true)]
        public bool IsDrawBoin { get; set; }


        private int _borderWidth = 2;
        [Category("A我的"), Description("边框的宽度，默认 2"), Browsable(true)]
        public int BorderWidth
        {
            get { return _borderWidth; }
            set { _borderWidth = value; }
        }
        private bool _isUseTwoColor;
        [Category("A我的"), Description("是否启用两种颜色填充，默认 false"), Browsable(true)]
        public bool IsUseTwoColor
        {
            get { return _isUseTwoColor; }
            set { _isUseTwoColor = value; Invalidate(); }
        }
        //const int ColorChangeint = 50;

        private int _colorChangeint=50;
        [Category("A我的"), Description("当选择两种颜色填空控件时，鼠标移动上控件时的控件颜色变化值，正值变亮，负值变暗，默认 50"), Browsable(true)]
        public int ColorChangeint
        {
            get { return _colorChangeint; }
            set { _colorChangeint = value; }
        }

        private Color _onMouseColor=Color.BurlyWood;
        [Category("A我的"), Description("当鼠标停留在控件上的颜色填充,只有启用一种颜色填充时候才起作用，默认 浅橙色 "), Browsable(true)]
        public Color OnMouseColor
        {
            get { return _onMouseColor; }
            set { _onMouseColor = value; }
        }

        private bool _isShowText=true;
        [Category("A我的"), Description("控件上是否显示文字，默认 true 显示 "), Browsable(true)]
        public bool IsShowText
        {
            get { return _isShowText; }
            set { _isShowText = value; Invalidate(); }
        }

        private bool _isShowFouceLine;
        [Category("A我的"), Description("控件上是否显示获得焦点后的虚线框，默认 false 不显示 "), Browsable(true)]
        public bool IsShowFouceLine
        {
            get { return _isShowFouceLine; }
            set { _isShowFouceLine = value; }
        }

        #endregion


        void BeginPainIni()
        {
            MyRect = this.ClientRectangle;
            DrawRect.X = MyRect.X + BorderWidth / 2;
            DrawRect.Y = MyRect.Y + BorderWidth / 2;
            DrawRect.Width = MyRect.Width - BorderWidth;
            DrawRect.Height = MyRect.Height - BorderWidth;



        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            BeginPainIni();
            Graphics Myg = e.Graphics;
            Myg.SmoothingMode = SmoothingMode.AntiAlias;
            Myg.CompositingQuality = CompositingQuality.HighQuality;
            Myg.InterpolationMode = InterpolationMode.HighQualityBicubic;


            DrawShape(Myg);
            if (IsShowFouceLine)
            {
                DrawFouceLine(MyShape, Myg, DrawRect);
            }
            
            
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            IsMouseOnFlag = true;
            Invalidate();
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            IsMouseOnFlag = false;
            Invalidate();
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            IsMouseOnFlag = true;
            Invalidate();
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            IsMouseOnFlag = false;
            Invalidate();
        }
       /// <summary>
       /// 得到所选形状的外框路径
       /// </summary>
       /// <param name="MyShape">枚举中的一个形状</param>
       /// <param name="MyRect">控件本身的全尺寸</param>
       /// <param name="BorderWidth">控件的外框线宽</param>
       /// <param name="Radius">如果是圆角矩形的圆角站该矩形的高的比例</param>
       /// <returns></returns>
        public virtual GraphicsPath GetShapeBorderPath(Shape MyShape,Rectangle MyRect,int BorderWidth,float Radius )
        {
            GraphicsPath borderpath = new GraphicsPath();
            switch (MyShape)
            {
                case Shape.RoundRectange:
                    borderpath = DrawHelper.GetRoundRectangePath(MyRect, BorderWidth, Radius);
                    break;
                case Shape.HalfCircle:
                    borderpath = DrawHelper.GetTwoHalfCircleRect(MyRect, BorderWidth);
                    break;
                case Shape.Rectange:
                    borderpath = DrawHelper.GetRectangePath(MyRect, BorderWidth);
                    break;
                default:
                    borderpath = DrawHelper.GetRoundRectangePath(MyRect, BorderWidth, Radius);
                    break;
            }
            return borderpath;

        }

        /// <summary>
        /// 得到所选形状的内部填充色的路径，要小于外框路径
        /// </summary>
        /// <param name="MyShape"></param>
        /// <param name="DrawRect"></param>
        /// <param name="BorderWidth"></param>
        /// <param name="Radius"></param>
        /// <returns></returns>

        public virtual GraphicsPath GetShapeFillPath(Shape MyShape, Rectangle DrawRect, int BorderWidth, float Radius)

        {
            GraphicsPath fillPath = new GraphicsPath();


            switch (MyShape)
            {
                case Shape.RoundRectange:
                    fillPath = DrawHelper.GetRoundRectangePath(DrawRect, BorderWidth, Radius);
                    break;
                case Shape.HalfCircle:
                    fillPath = DrawHelper.GetTwoHalfCircleRect(DrawRect, BorderWidth);
                    break;
                case Shape.Rectange:
                    fillPath = DrawHelper.GetRectangePath(DrawRect, BorderWidth);
                    break;
                default:
                    fillPath = DrawHelper.GetRoundRectangePath(DrawRect, BorderWidth, Radius);
                    break;
            }
            return fillPath;    

        }



        void  DrawShape(Graphics Myg)
        {
       

            GraphicsPath Borderpath = new GraphicsPath();
            //得到外形轮廓路径
            Borderpath = GetShapeBorderPath(MyShape, MyRect, BorderWidth, Radius);
            GraphicsPath FillPath = new GraphicsPath();
            //得到内部填充路径
            FillPath = GetShapeFillPath(MyShape, DrawRect, BorderWidth, Radius);

            //当填充两种颜色时后，鼠标经过控件的填充色，使控件原始色变亮或变暗变化
            if (IsUseTwoColor)
            {
                LinearGradientBrush FillBrush;

                switch (MyFillColorDec)
                {
                    case FillColorDec.Vertical:
                        if (IsMouseOnFlag)
                        {
                            FillBrush = new LinearGradientBrush(DrawRect, DrawHelper.GetChangeColor(FirstFillcolor, ColorChangeint), DrawHelper.GetChangeColor(SecondFillcolor, ColorChangeint), LinearGradientMode.Vertical);
                        }
                        else

                            FillBrush = new LinearGradientBrush(DrawRect, FirstFillcolor, SecondFillcolor, LinearGradientMode.Vertical);
                        break;
                    case FillColorDec.Horizontal:
                        if (IsMouseOnFlag)
                        {
                            FillBrush = new LinearGradientBrush(DrawRect, DrawHelper.GetChangeColor(FirstFillcolor, ColorChangeint), DrawHelper.GetChangeColor(SecondFillcolor, ColorChangeint), LinearGradientMode.Horizontal);
                        }
                        else

                            FillBrush = new LinearGradientBrush(DrawRect, FirstFillcolor, SecondFillcolor, LinearGradientMode.Horizontal);
                        break;
                    case FillColorDec.LeftVH:
                        if (IsMouseOnFlag)
                        {
                            FillBrush = new LinearGradientBrush(DrawRect, DrawHelper.GetChangeColor(FirstFillcolor, ColorChangeint), DrawHelper.GetChangeColor(SecondFillcolor, ColorChangeint), LinearGradientMode.ForwardDiagonal);
                        }
                        else

                            FillBrush = new LinearGradientBrush(DrawRect, FirstFillcolor, SecondFillcolor, LinearGradientMode.ForwardDiagonal);
                        break;

                    case FillColorDec.RightVH:
                        if (IsMouseOnFlag)
                        {
                            FillBrush = new LinearGradientBrush(DrawRect, DrawHelper.GetChangeColor(FirstFillcolor, ColorChangeint), DrawHelper.GetChangeColor(SecondFillcolor, ColorChangeint), LinearGradientMode.BackwardDiagonal);
                        }
                        else

                            FillBrush = new LinearGradientBrush(DrawRect, FirstFillcolor, SecondFillcolor, LinearGradientMode.BackwardDiagonal);
                        break;
                    default:
                        if (IsMouseOnFlag)
                        {
                            FillBrush = new LinearGradientBrush(DrawRect, DrawHelper.GetChangeColor(FirstFillcolor, ColorChangeint), DrawHelper.GetChangeColor(SecondFillcolor, ColorChangeint), LinearGradientMode.Vertical);
                        }
                        else


                            FillBrush = FillBrush = new LinearGradientBrush(DrawRect, FirstFillcolor, SecondFillcolor, LinearGradientMode.Vertical);
                        break;
                }

                Myg.FillPath(FillBrush, FillPath);
                   FillBrush.Dispose();
                }
            // 当填充色是单色时候 的鼠标经过颜色和 原填充色变化
            else
            {
                SolidBrush SFillBrush;

                if (IsMouseOnFlag)
                {
                    SFillBrush = new SolidBrush(OnMouseColor);

                }
                else SFillBrush = new SolidBrush(FirstFillcolor);

                Myg.FillPath(SFillBrush, FillPath);

              SFillBrush.Dispose();
            }
            //如果显示边框画边框
            if (IsDrawBoin)
            {

                Pen BorderPen = new Pen(BornColor, BorderWidth);

                Myg.DrawPath(BorderPen,Borderpath);
               BorderPen.Dispose();
            }
            //如果显示文字画文字
            if (IsShowText)
            {
                 DrawText(Myg,DrawRect);
            }
    
       
          
            
            //////释放资源
         
          
           
        }
        /// <summary>
        /// 画该控件要显示的文本要与 MyTextAlign 连用决定文字对齐方式
        /// </summary>
        /// <param name="Myg">那个DC上画</param>
        /// <param name="TexRect">文本所在的矩形区域</param>
        public virtual void DrawText(Graphics Myg,Rectangle TexRect)
        {
            SolidBrush FontBrush = new SolidBrush(FontColor);
            StringFormat sf = new StringFormat();
            //格式化显示文本 指定在工作矩形的中心显示
            if (MyTextAlign== TextAlign.Center) sf.Alignment = StringAlignment.Center;
            if (MyTextAlign == TextAlign.Left) sf.Alignment = StringAlignment.Near;
            if (MyTextAlign == TextAlign.Right) sf.Alignment = StringAlignment.Far;

            sf.LineAlignment = StringAlignment.Center;
           Myg.DrawString(Text, MyFont, FontBrush, TexRect, sf);

            ////也可以用效果不好
            ////TextRenderer.DrawText(g, this.Text, this.Font, pevent.ClipRectangle, FontColor);

        }


        public virtual void DrawFouceLine( Shape MyShape, Graphics Myg,Rectangle DrawRect)
        {
            if (this.Focused)
            {
                Rectangle rct = new Rectangle( DrawRect.X+ 4,DrawRect.Y+ 4,DrawRect.Width - 8, DrawRect.Height - 8);
                GraphicsPath path = new GraphicsPath();
                switch (MyShape)
                {   
                    case Shape.RoundRectange:
                        path = DrawHelper.GetRoundRectangePath(rct, 1, Radius);


                        break;
                    case Shape.HalfCircle:
                        path = DrawHelper.GetTwoHalfCircleRect(rct, 1);
                        
                        break;
                    case Shape.Rectange:
                              path = DrawHelper.GetRectangePath(rct, 1);
                        
                        break;
                    default:
                        path = DrawHelper.GetRoundRectangePath(rct, 1, Radius);
                        break;
               
                
                }

                using (Pen pn = new Pen(FontColor))
                {
                    pn.DashStyle = DashStyle.Dot;
                    Myg.DrawPath(pn, path);
                }

                path.Dispose();
            }

        }
        public virtual void DrawMark(Graphics Myg, Rectangle MyRect)
        {


        }
    }
}
