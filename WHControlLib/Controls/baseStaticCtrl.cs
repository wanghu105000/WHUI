using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
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
        public Rectangle MyRect = new Rectangle();
        /// <summary>
        /// 需要画出控件的矩形大小因为控件本身是透明色，为了消除锯齿现象所以画出的控件 小于 本控件的实际大小。
        /// </summary>
        public Rectangle DrawRect = new Rectangle();

        RectangleF imageRect = new RectangleF();


        /// <summary>
        /// 鼠标是否停留在控件上的标志
        /// </summary>
        public bool IsMouseOnFlag = false;
        //bool IsMouseOverFlag=false;
        //bool    IsMouseLeaveFlag=false;
        //bool IsMouseClickFlag=false;
        //********************
        #region 属性字段定义
        public enum FillColorDec
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
            CenterMiddle, CenterLeft, CenterRight, CenterButtom, CenterTop,TopLeft,TopRight,BottomLeft,BottomRight
        }


        ////当用隐藏的Text属性时必须这么加特性才能使本控件使用
        [Category("A我的"), Description("文字在控件上显示文字，默认"), Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text { get; set; }

        private TextAlign _myTextAlign = TextAlign.CenterMiddle;
        [Category("A我的"), Description("文字在控件上显示的对齐方式，默认，中间对齐"), Browsable(true)]
        public virtual TextAlign MyTextAlign
        {
            get { return _myTextAlign; }
            set { _myTextAlign = value; Invalidate(); }
        }



        private Shape _myShape = Shape.RoundRectange;
        [Category("A我的"), Description("要显示的控件的形状，默认，圆角矩形"), Browsable(true)]
        public Shape MyShape
        {
            get { return _myShape; }
            set { _myShape = value; Invalidate(); }
        }
        private FillColorDec _myFillColorDec = FillColorDec.Vertical;
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
                if (value > 1.1f)
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

        private Font _myFont = new Font("微软雅黑", 12.0f, 0, GraphicsUnit.Point, 1);
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

        private int _colorChangeint = 50;
        [Category("A我的"), Description("当选择两种颜色填空控件时，鼠标移动上控件时的控件颜色变化值，正值变亮，负值变暗，默认 50"), Browsable(true)]
        public int ColorChangeint
        {
            get { return _colorChangeint; }
            set { _colorChangeint = value; }
        }

        private Color _unEnableColor = Color.Gray;
        [Category("A我的"), Description("当控件处于不可用状态时候的颜色，默认 灰色 "), Browsable(true)]
        public Color UnEnableColor
        {
            get { return _unEnableColor; }
            set { _unEnableColor = value; }
        }

        private Color _onMouseColor = Color.BurlyWood;
        [Category("A我的"), Description("当鼠标停留在控件上的颜色填充,只有启用一种颜色填充时候才起作用，默认 浅橙色 "), Browsable(true)]
        public Color OnMouseColor
        {
            get { return _onMouseColor; }
            set { _onMouseColor = value; }
        }

        private bool _isShowText = true;
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

        //角标属性设置
        private bool _isShowMark;
        [Category("A我的角标"), Description("控件上是否显示角标，默认 false 不显示 "), Browsable(true)]
        public bool IsShowMark
        {
            get { return _isShowMark; }
            set { _isShowMark = value; Invalidate(); }
        }
        private Color _markBackColor = Color.Red;
        [Category("A我的角标"), Description("控件上角标的背景色，默认 红色 "), Browsable(true)]
        public Color MarkBackColor
        {
            get { return _markBackColor; }
            set { _markBackColor = value; Invalidate(); }
        }
        private Color _markTextColor = Color.White;
        [Category("A我的角标"), Description("控件上角标上文字的颜色，默认 白色 "), Browsable(true)]
        public Color MarkTextColor
        {
            get { return _markTextColor; }
            set { _markTextColor = value; Invalidate(); }
        }

        private string _markText = "";
        [Category("A我的角标"), Description("控件上角标上文字 文本，默认 无 "), Browsable(true)]
        public string MarkText
        {
            get { return _markText; }
            set { _markText = value; Invalidate(); }
        }

        private float _markHeight = 2.0f;
        [Category("A我的角标"), Description("控件上角标的高度，采用比例形式 为控件高度的几份之一，默认 2.0f 分之一,不能小于1.0f "), Browsable(true)]
        public float MarkHeight
        {
            get { return _markHeight; }
            set
            {
                if (value < 1)
                {
                    _markHeight = 1.0f;

                }
                else
                    _markHeight = value; Invalidate(); }
        }
        private bool _isShowMarkBorder;
        [Category("A我的角标"), Description("是否显示角标外形边框 ，默认 无 "), Browsable(true)]
        public bool IsShowMarkBorder
        {
            get { return _isShowMarkBorder; }
            set { _isShowMarkBorder = value; Invalidate(); }
        }
        private int _markBorderWidth = 1;
        [Category("A我的角标"), Description("角标外形边框的宽度 ，默认 1 "), Browsable(true)]
        public int MarkBorderWidth
        {
            get { return _markBorderWidth; }
            set { _markBorderWidth = value; Invalidate(); }
        }
        private Color _markBorderColor = Color.White;
        [Category("A我的角标"), Description("角标外形边框的颜色 ，默认 白色 "), Browsable(true)]
        public Color MarkBorderColor
        {
            get { return _markBorderColor; }
            set { _markBorderColor = value; Invalidate(); }
        }
        private Color _markerTextColor = Color.White;
        [Category("A我的角标"), Description("角标文本的颜色 ，默认 白色 "), Browsable(true)]
        public Color MarkerTextColor
        {
            get { return _markerTextColor; }
            set { _markerTextColor = value; Invalidate(); }
        }

        private int _markerTextSzie = 10;
        [Category("A我的角标"), Description("角标文字的大小，默认  10 ,数值越大文字越大"), Browsable(true)]
        public int MarkerTextSzie
        {
            get { return _markerTextSzie; }
            set
            {
                if (value <= 0) _markerTextSzie = 10;

                else _markerTextSzie = value;

                Invalidate();
            }
        }
        // 图标定义\\
        private bool _isShowMyImage;
        [Category("A我的图标"), Description("控件是否显示图像，默认 否"), Browsable(true)]
        public bool IsShowMyImage
        {
            get { return _isShowMyImage; }
            set
            {

                _isShowMyImage = value;
                Invalidate();

            }
        }

        private Image _myImage = null;
        [Category("A我的图标"), Description("控件的图标图像，默认 null"), Browsable(true)]
        public Image MyImage
        {
            get { return _myImage; }
            set { _myImage = value; Invalidate(); }
        }
        private Image _myImageOnMouse = null;
        [Category("A我的图标"), Description("控件的鼠标停留在控件上的图标图像，默认 null"), Browsable(true)]
        public Image MyImageOnMouse
        {
            get { return _myImageOnMouse; }
            set { _myImageOnMouse = value; Invalidate(); }
        }
        private Image _myImageUnEnable = null;
        [Category("A我的图标"), Description("控件不可用状态时候的图标图像，默认 null"), Browsable(true)]
        public Image MyImageUnEnable
        {
            get { return _myImageUnEnable; }
            set { _myImageUnEnable = value; Invalidate(); }
        }
        public enum ImageDec
        {
            left,
            top,
            right,
            bottom,
        }
        private ImageDec _myImageDec = ImageDec.left;
        [Category("A我的图标"), Description("控件的图标图像的方向，默认 左边"), Browsable(true)]
        public ImageDec MyImageDec
        {
            get { return _myImageDec; }
            set { _myImageDec = value; Invalidate(); }
        }
        //private float _myimageWidth=0.5f;
        //[Category("A我的图标"), Description("控件的图标图像的宽度，用控件的比例表示不能大于1，默认0.5"), Browsable(true)]
        //public float MyimageWidth
        //{
        //    get { return _myimageWidth; }
        //    set { _myimageWidth= value; Invalidate(); }
        //}
        private float _myimageHeight = 0.5f
            ;
        [Category("A我的图标"), Description("控件的图标图像的宽度，用控件的比例表示不能大于1，默认0.5"), Browsable(true)]
        public float MyimageHeight
        {
            get { return _myimageHeight; }
            set
            {
                if (value > 1)
                {
                    _myimageHeight = 1;
                }
                else _myimageHeight = value; Invalidate(); }
        }
        private bool _isOpenimageTranparentColor;
        [Category("A我的图标"), Description("是否开启 导入的控件图标 的背景色透明过滤，注：windows支持png，ico格式透明色不用开启 默认 false 不开启"), Browsable(true)]
        public bool IsOpenimageTranparentColor
        {
            get { return _isOpenimageTranparentColor; }
            set { _isOpenimageTranparentColor = value; }
        }


        private Color _imageTranparentColo = Color.White;
        [Category("A我的图标"), Description("选取的 导入的控件图标透明过滤色，默认 用 白色 做 透明色"), Browsable(true)]
        public Color ImageTranparentColor
        {
            get { return _imageTranparentColo; }
            set { _imageTranparentColo = value; }
        }
        private Size _imageOffSet = new Size(0, 0);
        [Category("A我的图标"), Description("图片在当前位置的偏移量，默认 0,0"), Browsable(true)]
        public Size ImageOffSet
        {
            get { return _imageOffSet; }
            set { _imageOffSet = value; Invalidate(); }
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
            //如果选择自动大小
            ISAutoSize(Myg);

            Myg.SmoothingMode = SmoothingMode.AntiAlias;
            Myg.CompositingQuality = CompositingQuality.HighQuality;
            Myg.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //////////画形状并填充

            DrawShape(Myg);

            ////画获得焦点时候的虚线
            if (IsShowFouceLine)
            {
                DrawFouceLine(MyShape, Myg, DrawRect);
            }
            ////话角标
            if (IsShowMark)
            {
                DrawMark(Myg, MyRect, BorderWidth);
            }



        }

        /// <summary>
        /// 得到所选形状的外框路径
        /// </summary>
        /// <param name="MyShape">枚举中的一个形状</param>
        /// <param name="MyRect">控件本身的全尺寸</param>
        /// <param name="BorderWidth">控件的外框线宽</param>
        /// <param name="Radius">如果是圆角矩形的圆角站该矩形的高的比例</param>
        /// <returns></returns>
        public virtual GraphicsPath GetShapeBorderPath(Shape MyShape, Rectangle MyRect, int BorderWidth, float Radius)
        {
            GraphicsPath borderpath;
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
            GraphicsPath fillPath;


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



        void DrawShape(Graphics Myg)
        {


            GraphicsPath Borderpath;
            //得到外形轮廓路径
            Borderpath = GetShapeBorderPath(MyShape, MyRect, BorderWidth, Radius);
            GraphicsPath FillPath;
            //得到内部填充路径
            FillPath = GetShapeFillPath(MyShape, DrawRect, BorderWidth, Radius);
            //当控件处于可用状态时候 的颜色填充
            if (this.Enabled)
            {


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


                                FillBrush = new LinearGradientBrush(DrawRect, FirstFillcolor, SecondFillcolor, LinearGradientMode.Vertical);
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
            }
            //当控件处于不可用状态时候的颜色填充
            else
            {
                SolidBrush UnEnableFillBrush;
                UnEnableFillBrush = new SolidBrush(UnEnableColor);
                Myg.FillPath(UnEnableFillBrush, FillPath);
                UnEnableFillBrush.Dispose();

            }

            //如果显示边框画边框
            if (IsDrawBoin)
            {

                Pen BorderPen = new Pen(BornColor, BorderWidth);

                Myg.DrawPath(BorderPen, Borderpath);
                BorderPen.Dispose();
            }

            //画图标
            if (IsShowMyImage)
            {
                if (MyImage != null || MyImageOnMouse != null || MyImageUnEnable != null)
                    DrawNeedDrawImage(Myg, DrawRect);

            }


            //如果显示文字画文字
            if (IsShowText)
            {

                DrawText(Myg);

            }




            //////释放资源



        }
        /// <summary>
        /// 画该控件要显示的文本要与 MyTextAlign 连用决定文字对齐方式
        /// </summary>
        /// <param name="Myg">那个DC上画</param>
        /// <param name="TexRect">文本所在的矩形区域</param>
        public virtual void DrawText(Graphics Myg)
        {
            SolidBrush FontBrush = new SolidBrush(FontColor);
            StringFormat sf = new StringFormat();
            //格式化显示文本 指定在工作矩形的中心显示

            switch (MyTextAlign)
            {
                case TextAlign.CenterMiddle:
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;
                    break;
                case TextAlign.CenterLeft:
                    sf.Alignment = StringAlignment.Near;
                    sf.LineAlignment = StringAlignment.Center;
                    break;
                case TextAlign.CenterRight:
                    sf.Alignment = StringAlignment.Far;
                    sf.LineAlignment = StringAlignment.Center;
                    break;
                case TextAlign.CenterButtom:
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Far;
                    break;
                case TextAlign.CenterTop:
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Near;
                    break;
                case TextAlign.TopLeft:
                    sf.Alignment = StringAlignment.Near;
                    sf.LineAlignment = StringAlignment.Near;
                    break;
                case TextAlign.TopRight:
                    sf.Alignment = StringAlignment.Far;
                    sf.LineAlignment = StringAlignment.Near;
                    break;
                case TextAlign.BottomLeft:
                    sf.Alignment = StringAlignment.Near;
                    sf.LineAlignment = StringAlignment.Far;
                    break;
                case TextAlign.BottomRight:
                    sf.Alignment = StringAlignment.Far;
                    sf.LineAlignment = StringAlignment.Far;

                    break;
                default:
                    break;
            }

            Rectangle TextRect = new Rectangle();
            TextRect = DrawRect;
                Myg.DrawString(Text, MyFont, FontBrush, TextRect, sf); return;
            ////
            ///自动对齐为实现

            //if (  IsShowMyImage==false || MyImage == null&&AutoSize)
            //{
            //    Rectangle TextRect = new Rectangle();
            //    int textwidth = (int)Myg.MeasureString(Text, MyFont).Width;
            //    int textheight = (int)Myg.MeasureString(Text, MyFont).Height;
            //    this.Width = textwidth + 10;
            //    this.Height = textheight + 10;
            //    TextRect.X = 2;
            //    TextRect.Y = 2;
            //    TextRect.Width = textwidth + 5;
            //    TextRect.Height = textheight + 5;

            //    Myg.DrawString(Text, MyFont, FontBrush, TextRect, sf); return;


            //}




            ////也可以用效果不好
            ////TextRenderer.DrawText(g, this.Text, this.Font, pevent.ClipRectangle, FontColor);

        }


        public virtual void DrawFouceLine( Shape MyShape, Graphics Myg,Rectangle DrawRect)
        {
            if (this.Focused)
            {
                Rectangle rct = new Rectangle( DrawRect.X+ 4,DrawRect.Y+ 4,DrawRect.Width - 8, DrawRect.Height - 8);
                GraphicsPath path ;
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
        public virtual void DrawMark(Graphics Myg, Rectangle MyRect, int BorderWidth)
        { 
            float penwidth = BorderWidth / 2;
            RectangleF MarkRect = new RectangleF();
            float R = MyRect.Height / MarkHeight;
            MarkRect.Width = R-MarkBorderWidth;
            MarkRect.Height = R-MarkBorderWidth;
            MarkRect.X = MarkRect.X + MyRect.Width - R - BorderWidth- MarkBorderWidth / 2;
            MarkRect.Y = MarkRect.Y +penwidth+MarkBorderWidth/2;
            using (SolidBrush markfillbrush=new SolidBrush(MarkBackColor))
            {
                Myg.FillEllipse(markfillbrush, MarkRect);
            }
            if (IsShowMarkBorder)
            {
             using (Pen markborderPen=new Pen(MarkBorderColor,MarkBorderWidth))
            {
                Myg.DrawEllipse(markborderPen, MarkRect);
            }
            }
            if (MarkText!="")
            {
                DrawMarkText(Myg, MarkRect);
            }

        }
    
    
    public virtual void DrawMarkText(Graphics Myg,RectangleF MarkRect)
        {
            SolidBrush MarkerTextBrush = new SolidBrush(MarkerTextColor);
            Rectangle MarktextRct = new Rectangle();

            Font f = new Font("微软雅黑", MarkerTextSzie);
                 //////文字区域定义在了圆的内接正方形
            int NowFontWidth = (int)(Math.Sqrt(2.0) * MarkRect.Width / 2);
            int NowFontHeight = NowFontWidth;
            MarktextRct.Width = NowFontWidth;
            MarktextRct.Height = NowFontHeight;
            MarktextRct.X = (int)(MarkRect.X + MarkRect.Width / 2 - MarktextRct.Width / 2);
            MarktextRct.Y = (int)(MarkRect.Y + MarkRect.Height / 2 - MarktextRct.Height / 2);
            ////文字的对齐方式
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
      
            sf.FormatFlags = StringFormatFlags.NoWrap;
            Myg.DrawString(MarkText, f, MarkerTextBrush, MarktextRct, sf);
        
            f.Dispose();
            MarkerTextBrush.Dispose();

        }
        public virtual void DrawNeedDrawImage(Graphics Myg,Rectangle DrawRct)
        {
            ////是否开启颜色过滤画图片
            if (IsOpenimageTranparentColor)
            {

                if (IsMouseOnFlag && MyImageOnMouse != null)
                {

                    Bitmap bt = new Bitmap(MyImageOnMouse);
                    bt.MakeTransparent(ImageTranparentColor);

                   DrawMyImage(Myg,DrawRect,bt);
                    bt.Dispose();
                    return;
                }
                else if (IsMouseOnFlag && MyImageOnMouse == null && MyImage != null)
                {
                    Bitmap bt = new Bitmap(MyImage);
                    bt.MakeTransparent(ImageTranparentColor);

                    DrawMyImage(Myg, DrawRect, bt);
                    bt.Dispose();
                    return;
                }




                if (this.Enabled == false && MyImageUnEnable != null)
                {
                    Bitmap bt = new Bitmap(MyImageUnEnable);
                    bt.MakeTransparent(ImageTranparentColor);

                    DrawMyImage(Myg, DrawRect, bt);
                    bt.Dispose();
                    return;
                }
                else if (this.Enabled == false && MyImageUnEnable == null && MyImage != null)
                {
                    Bitmap bt = new Bitmap(MyImage);
                    bt.MakeTransparent(ImageTranparentColor);

                    DrawMyImage(Myg, DrawRect, bt);
                    bt.Dispose();
                    return;
                }


                if (MyImage != null && this.Enabled && IsMouseOnFlag == false)
                {
                    Bitmap bt = new Bitmap(MyImage);
                    bt.MakeTransparent(ImageTranparentColor);

                    DrawMyImage(Myg, DrawRect, bt);
                    bt.Dispose();
                    return;
                }





            }
            ////////不开启颜色过滤画图片
            else
            {
                if (IsMouseOnFlag && MyImageOnMouse != null)
                {
                    DrawMyImage(Myg, DrawRect, MyImageOnMouse);

             
                    return;
                }
                else if (IsMouseOnFlag && MyImageOnMouse == null && MyImage != null)
                {
                    DrawMyImage(Myg, DrawRect, MyImage);

           
                    return;
                }



                if (this.Enabled == false && MyImageUnEnable != null)
                {
                    DrawMyImage(Myg, DrawRect, MyImageUnEnable);

                 return;
                }
                else if (this.Enabled == false && MyImageUnEnable == null && MyImage != null)
                {
                    DrawMyImage(Myg, DrawRect, MyImage);
                     return; }


                if (MyImage != null && this.Enabled && IsMouseOnFlag == false)
                {

                    DrawMyImage(Myg, DrawRect, MyImage);
                 return;

                }
            }




        }

        public virtual void DrawMyImage(Graphics Myg,Rectangle DrawRect,Image Drawimage)
        {

          
            int offsetX = ImageOffSet.Width;
             int offsetY = ImageOffSet.Height;
            
            float imageBl = Drawimage.Width / Drawimage.Height;
       
            float imageheight =DrawRect.Height * MyimageHeight;
               float imagewidth = imageheight*imageBl;
               imageRect.Width = imagewidth;
                imageRect.Height = imageheight;
            //判断需要画图片的方向
                switch (MyImageDec)
                {
                    case ImageDec.left:
                        imageRect.X = DrawRect.X+offsetX+5;
                        imageRect.Y= DrawRect.Y+DrawRect.Height/2-imageRect.Height/2+offsetY;
                        break;
                    case ImageDec.top:
                        imageRect.X=DrawRect.X+DrawRect.Width/2-imageRect.Width/2+offsetX;
                        imageRect.Y = DrawRect.Y + offsetY+5;
                        break;
                    case ImageDec.right:
                        imageRect.X = DrawRect.X + DrawRect.Width - imageRect.Width - offsetX-5;
                        imageRect.Y=DrawRect.Y+ DrawRect.Height / 2 - imageRect.Height / 2+offsetY;
                        break;
                    case ImageDec.bottom:
                        imageRect.X = DrawRect.X + DrawRect.Width / 2 - imageRect.Width / 2+offsetX;
                        imageRect.Y = DrawRect.Y + DrawRect.Height - imageRect.Height - offsetY-5;
                        break;
                    default:
                        break;
                }

            Myg.DrawImage(Drawimage, imageRect);
     

            }
            
    //Size FindBigerImage()
    //    {
    //        Size imagesize = new Size();
    //        if (MyImage!=null)
                
    //        {

    //        }



        //}


        public virtual void ISAutoSize(Graphics Myg)
        {
            

        }



        ////////////////////////////
    }

}
