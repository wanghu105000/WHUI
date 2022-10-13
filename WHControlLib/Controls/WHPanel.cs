using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WHControlLib.Controls
{
    public partial class WHPanel : /*Panel*/ UserControl
    {
        public WHPanel()
        {
            //设置双缓冲

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.AllPaintingInWmPaint, true);

            ////背景定义为透明色   
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;


            InitializeComponent();
        }

        //*******************全局参数定义*******************
        Rectangle MyRect=new Rectangle();
        Rectangle DrawRect = new Rectangle();
        Rectangle TitleRect = new Rectangle();
        Rectangle TiteTxtRect=new Rectangle();
        Rectangle TitleCtrlBoxRect = new Rectangle();

        //********************************************************
        #region 属性参数定义
        public enum Shape
        {
            RoundRectange, Square
        }
        private Shape _myShape = Shape.RoundRectange;
        [Category("A我的"), Description("本控件的形状，默认，圆角矩形"), Browsable(true)]
        public Shape MyShape
        {
            get { return _myShape; }
            set { _myShape = value; Invalidate(); }
        }


        private bool _isShowBorder=true;
        [Category("A我的"), Description("是否显示控件外边框，默认，true"), Browsable(true)]
        public bool IsShowBorder
        {
            get { return _isShowBorder; }
            set { _isShowBorder=  value; }

        }
        private Color _unEnableColor = Color.Gray;
        [Category("A我的"), Description("当不可用时候的颜色，默认，灰色"), Browsable(true)]
        public Color UnEnableColor
        {
            get { return _unEnableColor; }
            set { _unEnableColor = value; }
        }
        private Color _borderColor = Color.Blue;
        /// <summary>
        /// 边框颜色
        /// </summary>
        [Category("A我的"), Description("边框颜色"), Browsable(true)]
        public Color BorderColor
        {
            get { return _borderColor; }
            set { _borderColor = value; this.Invalidate(); }
        }

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


        private float _radius = 5.0f;

        /// <summary>
        /// 圆角大小比例
        /// </summary>
        [Category("A我的"), Description(" 圆角大小比例默认5.0,值越大圆角比例越小，不能小于1.1"), Browsable(true)]
        public float Radius
        {
            get { return _radius; }
            set
            {
                if (value > 1.1f)
                {
                    _radius = value;
                    this.Invalidate();
                }
                else { _radius = 1.1f; this.Invalidate(); }

            }
        }

        //***********有关标题栏属性定义***开始***********************

        public enum TitleCtrlBoxShape { square, Circle }


        private TitleCtrlBoxShape _myTitleCtrlBoxShape = TitleCtrlBoxShape.square;
        [Category("A我的标题栏"), Description("关闭按钮距的形状，默认，方形 "), Browsable(true)]
        public TitleCtrlBoxShape MyTitleCtrlBoxShape
        {
            get { return _myTitleCtrlBoxShape; }
            set { _myTitleCtrlBoxShape = value; Invalidate(); }
        }

        private int _boxJG = 10;

        [Category("A我的标题栏"), Description("关闭按钮距离窗体左边的距离，默认，10 "), Browsable(true)]
        public int BoxJG
        {
            get { return _boxJG; }
            set { _boxJG = value; Invalidate(); }
        }

        private bool _isShowTitle=true;
        [Category("A我的标题栏"), Description("是否显示标题栏，默认，true "), Browsable(true)]
        public bool IsShowTitle
        {
            get { return _isShowTitle; }
            set { _isShowTitle = value; }
        }


        private Color _titleTextColor = Color.Black;
        [Category("A我的标题栏"), Description("标题栏文本的颜色，默认，黑色 "), Browsable(true)]
        public Color TitleTextColor
        {
            get { return _titleTextColor; }
            set { _titleTextColor = value; Invalidate(); }
        }

        private Font _titleTextFont = new Font("宋体", 12);
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

        private Color _TitleCtrlBoxBackColor = Color.Transparent;
        [Category("A我的标题栏"), Description("关闭按钮背景色，默认，透明色 "), Browsable(true)]
        public Color TitleCtrlBoxBackColor
        {
            get { return _TitleCtrlBoxBackColor; }
            set { _TitleCtrlBoxBackColor = value; Invalidate(); }
        }

        private Color _TitleCtrlBoxSelectColor = Color.Red;
        [Category("A我的标题栏"), Description("关闭按钮选中后的色，默认，红色 "), Browsable(true)]
        public Color TitleCtrlBoxSelectColor
        {
            get { return _TitleCtrlBoxSelectColor; }
            set { _TitleCtrlBoxSelectColor = value; }
        }
        private Color _TitleCtrlBoxShapeColor = Color.White;
        [Category("A我的标题栏"), Description("标题栏按钮形状的颜色，默认，白色 "), Browsable(true)]
        public Color TitleCtrlBoxShapeColor
        {
            get { return _TitleCtrlBoxShapeColor; }
            set { _TitleCtrlBoxShapeColor = value; Invalidate(); }
        }
        private Color _titleBackColor = Color.DeepSkyBlue;
        [Category("A我的标题栏"), Description("标题栏背景的颜色，默认，蓝色 "), Browsable(true)]
        public Color TitleBackColor
        {
            get { return _titleBackColor; }
            set { _titleBackColor = value; Invalidate(); }
        }

        private bool _isShowTitleCtrlBoxBorder = true;
        [Category("A我的标题栏"), Description("是否显示关闭按钮外边框，默认，是 "), Browsable(true)]
        public bool IsShowTitleCtrlBoxBorder
        {
            get { return _isShowTitleCtrlBoxBorder; }
            set { _isShowTitleCtrlBoxBorder = value; Invalidate(); }
        }

        private Color _TitleCtrlBoxBorderColor = Color.Black;
        [Category("A我的标题栏"), Description("关闭按钮外边框颜色，默认，黑色 "), Browsable(true)]
        public Color TitleCtrlBoxBorderColor
        {
            get { return _TitleCtrlBoxBorderColor; }
            set { _TitleCtrlBoxBorderColor = value; Invalidate(); }
        }

        private float _TitleCtrlBoxBorderWidth = 2.0f;
        [Category("A我的标题栏"), Description("关闭按钮边框和内部形状的宽度，默认,2.0f 不能小于0.5f "), Browsable(true)]
        public float TitleCtrlBoxBorderWidth
        {
            get { return _TitleCtrlBoxBorderWidth; }
            set
            {
                if (value < 0.5f)
                {
                    _TitleCtrlBoxBorderWidth = 0.5f;
                }

                _TitleCtrlBoxBorderWidth = value; Invalidate();
            }
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

        private float _TitleCtrlBoxSize = 0.8f;
        [Category("A我的标题栏"), Description("关闭按钮相对于标题栏的大小，默认 0.8f不能大于1.0f小于0.2f "), Browsable(true)]
        public float TitleCtrlBoxSize
        {
            get { return _TitleCtrlBoxSize; }
            set
            {
                if (value > 1 | value < 0.2f)
                {
                    value = 0.2f;
                }
                else _TitleCtrlBoxSize = value; Invalidate();
            }
        }

        private float _TitleCtrlBoxShapeBl = 0.7f;
        [Category("A我的标题栏"), Description("关闭按钮X形状的相对关闭按钮范围的大小，默认 0.8f不能大于1.0f小于0.2f "), Browsable(true)]
        public float TitleCtrlBoxShapeBl
        {
            get { return _TitleCtrlBoxShapeBl; }
            set
            {
                if (value > 1 | value < 0.2f)
                {
                    _TitleCtrlBoxShapeBl = 1.0f;

                }
                else
                    _TitleCtrlBoxShapeBl = value; Invalidate();
            }
        }



        #endregion
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);


        }

    }
}
