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
    public partial class WHButtonPro : UserControl
    {
        public WHButtonPro()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                  ControlStyles.ResizeRedraw |
                  ControlStyles.AllPaintingInWmPaint, true);
            InitializeComponent();
        }

        //全局定义

        Rectangle MyRect = new Rectangle();
        Rectangle DrawRect = new Rectangle();
        bool IsMouseOnFlag=false;

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


        //private string _myText;

        //public  string MyText

        //{
        //    get { return _myText; }
        //    set { _myText = value; Invalidate(); }
        //}
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
        private Color _onMouseColor=Color.BurlyWood;
        [Category("A我的"), Description("当鼠标停留在控件上的颜色填充，默认 浅橙色 "), Browsable(true)]
        public Color OnMouseColor
        {
            get { return _onMouseColor; }
            set { _onMouseColor = value; }
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


            DrawRoungRectShape(Myg);
          
            
            
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
        void  DrawRoungRectShape(Graphics Myg)
        {
            const int ColorChangeint = 50;
            LinearGradientBrush FillBrush;
            SolidBrush SFillBrush;
            if (IsMouseOnFlag)
            {
                SFillBrush = new SolidBrush(OnMouseColor);

            }
            else SFillBrush = new SolidBrush(FirstFillcolor);


            Pen BorderPen = new Pen(BornColor, BorderWidth);
  
            switch (MyFillColorDec)
            {       
                case FillColorDec.Vertical:
                    if (IsMouseOnFlag)
                    {
                        FillBrush = new LinearGradientBrush(DrawRect,  DrawHelper.GetChangeColor(FirstFillcolor, ColorChangeint), DrawHelper.GetChangeColor(SecondFillcolor, ColorChangeint), LinearGradientMode.Vertical);
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
           
            GraphicsPath Borderpath = new GraphicsPath();
            //得到外形轮廓路径
            switch (MyShape)
            {       
                case Shape.RoundRectange:
                    Borderpath = DrawHelper.GetRoundRectangePath(MyRect, BorderWidth, Radius);
                    break;
                case Shape.HalfCircle:
                    Borderpath = DrawHelper.GetTwoHalfCircleRect(MyRect, BorderWidth);
                    break;
                case Shape.Rectange:
                    Borderpath = DrawHelper.GetRectangePath(MyRect, BorderWidth);
                    break;
                default:
                    Borderpath = DrawHelper.GetRoundRectangePath(MyRect, BorderWidth, Radius);
                    break;
            }

            //Borderpath = DrawHelper.GetRoundRectangePath(MyRect, BorderWidth, Radius);
            GraphicsPath FillPath = new GraphicsPath();
            //得到内部填充路径

            switch (MyShape)
            {
                case Shape.RoundRectange:
                    FillPath = DrawHelper.GetRoundRectangePath(DrawRect, BorderWidth, Radius);
                    break;
                case Shape.HalfCircle:
                    FillPath = DrawHelper.GetTwoHalfCircleRect(DrawRect, BorderWidth);
                    break;
                case Shape.Rectange:
                    FillPath = DrawHelper.GetRectangePath(DrawRect, BorderWidth);
                    break;
                default:
                    FillPath = DrawHelper.GetRoundRectangePath(DrawRect, BorderWidth, Radius);
                    break;
            }



            //FillPath = DrawHelper.GetRoundRectangePath(DrawRect, BorderWidth, Radius);
            if (IsUseTwoColor)
            {
                Myg.FillPath(FillBrush, FillPath);
                }
            else
            {
             
                Myg.FillPath(SFillBrush, FillPath);

            }

            if (IsDrawBoin)
            {
                Myg.DrawPath(BorderPen,Borderpath);
            }
    
            DrawText(Myg,DrawRect);
          
            
            //////释放资源
            FillBrush.Dispose();
            SFillBrush.Dispose();
            BorderPen.Dispose();
        }
        void DrawText(Graphics Myg,Rectangle TexRect)
        {
            SolidBrush FontBrush = new SolidBrush(FontColor);
            //Rectangle TexRect = DrawRect;
            StringFormat sf = new StringFormat();
            //格式化显示文本 指定在工作矩形的中心显示
            if (MyTextAlign== TextAlign.Center) sf.Alignment = StringAlignment.Center;
            if (MyTextAlign == TextAlign.Left) sf.Alignment = StringAlignment.Near;
            if (MyTextAlign == TextAlign.Right) sf.Alignment = StringAlignment.Far;

            sf.LineAlignment = StringAlignment.Center;
           Myg.DrawString(Text, MyFont, FontBrush, TexRect, sf);

            //也可以用效果不好
            //TextRenderer.DrawText(g, this.Text, this.Font, pevent.ClipRectangle, FontColor);

        }
    }
}
