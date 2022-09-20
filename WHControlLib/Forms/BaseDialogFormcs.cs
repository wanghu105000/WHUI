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

        Rectangle MyRect = new Rectangle();

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



        #endregion

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


            //SolidBrush FillFormBrush=new SolidBrush()

            RectangleF DrawRct = new RectangleF();
            DrawRct.X = Myrect.X-FormBorderWidth/2;
            DrawRct.Y = Myrect.Y-FormBorderWidth/2;
            DrawRct.Width = Myrect.Width-FormBorderWidth;
            DrawRct.Height = Myrect.Height-FormBorderWidth ;

            GraphicsPath RoundPath = new GraphicsPath();

            RoundPath = DrawHelper.GetFillRoundRectFPath(DrawRct, FormBorderWidth, Radius);
            Region Formreg = new Region(RoundPath);
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
                using (LinearGradientBrush LinerBrush = new LinearGradientBrush(Myrect, firstColor, SecondColor, LinearGradientMode.Vertical))
                {
                    Myg.FillRectangle(new SolidBrush( Color.Transparent), DrawRct);
                    Myg.FillRegion(LinerBrush, Formreg);
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
                    formbodypath= DrawHelper.GetFillRoundRectFPath(Myrect, FormBorderWidth, Radius);
                    Myg.DrawPath(formBodyPen, formbodypath);
                }
            }



            this.Region = Formreg;


            //释放资源

        }



        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams cParms = base.CreateParams;
        //        cParms.ExStyle |= 0x00080000; // WS_EX_LAYERED
        //        return cParms;
        //    }
        //}



    }
}
