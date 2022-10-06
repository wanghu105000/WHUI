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
    public partial class WHCheckBox : UserControl
    {
        public WHCheckBox()
        {
            //设置双缓冲
         
                this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                         ControlStyles.ResizeRedraw |
                         ControlStyles.AllPaintingInWmPaint, true);
            AutoSize = false;
                InitializeComponent();
        }

        #region 属性，字段  定义
        const float InandOutBL=0.8f;

        private Color _outSiderborderColor=Color.Orange;
        [Category("我的"), Description("外边框颜色，默认，橙色"), Browsable(true)]
        public Color OutSiderborderColor
        {
            get { return _outSiderborderColor; }
            set { _outSiderborderColor = value; Invalidate(); }
        }

        private bool _isDrawOutSiderBorder=false;
        [Category("我的"), Description("是否化控件外边框，默认，false"), Browsable(true)]
        public bool IsDrawOutSiderBorder
        {
            get { return _isDrawOutSiderBorder; }
            set
            {   _isDrawOutSiderBorder = value; Invalidate(); }
        }

        private int _outSiderBorderWidth=2;
        [Category("我的"), Description("控件外边框 宽度，默认，2"), Browsable(true)]
        public int OutSiderBorderWidth
        {
            get { return _outSiderBorderWidth; }
            set
            {
                if (value < 1) _outSiderBorderWidth = 1;
                 else _outSiderBorderWidth = value; Invalidate();  }
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


        private Color _selectSquartFillColor=Color.Orange;
        [Category("我的"), Description("选择对号框的填充颜色，默认，橙色"), Browsable(true)]
        public Color SelectSquartFillColor
        {
            get { return _selectSquartFillColor; }
            set { _selectSquartFillColor = value; Invalidate(); }
        }

        private float _radius =5.0f;
        [Category("我的"), Description("选择对号框的圆角弧度，默认，5.0f"), Browsable(true)]
        public float  Radius
        {
            get { return _radius; }
            set
            {
                if (_radius<1.0f)_radius = 1.0f;
               else  _radius = value; Invalidate();
            }
        }

        private bool _isInsideRoundRct=true ;
        [Category("我的"), Description("对号的填充方块是否是圆角矩形，默认，true"), Browsable(true)]
        public bool IsInsideRoundRct
        {
            get { return _isInsideRoundRct ; }
            set { _isInsideRoundRct  = value; }
        }

        private Color _theRightShapeFillColor = Color.White;
        [Category("我的"), Description("选择对号的填充颜色，默认，白色"), Browsable(true)]
        public Color TheRightShapeFillColor
        {
            get { return _theRightShapeFillColor; }
            set { _theRightShapeFillColor = value; Invalidate(); }
        }


        private bool _checked=false;
        [Category("我的"), Description("被选中状态，默认，false"), Browsable(true)]
        public bool Checked
        {
            get { return _checked; }
            set { _checked = value; Invalidate(); }
        }

        private string _mytext="" ;
        [Category("我的"), Description("要显示的文字，默认， 无"), Browsable(true)]
        public string MyText
        {
            get { return _mytext; }
            set { _mytext = value; Invalidate(); }
        }

        #endregion
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (this.Checked)
            {
                this.Checked = false;
            }
            else this.Checked = true;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            Rectangle MyRect = this.ClientRectangle;
            Graphics Myg=pe.Graphics;
            Myg.SmoothingMode = SmoothingMode.AntiAlias;
            Myg.CompositingQuality = CompositingQuality.HighQuality;
            Myg.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //此处后期改进这样 不能支持 混合背景色 透明 采取 控件背景色透明的方式
            Myg.Clear(this.Parent.BackColor);
            
              DrawFillSelectSquart(Myg, MyRect);
            


            //画控件外边框
            if (IsDrawOutSiderBorder)
            {
                DrawOutSideBorder(Myg, MyRect);
            }

            //画文字
            DrawText(Myg, MyRect);


        }



        /// <summary>
        /// 得到对号的形状
        /// </summary>
        /// <param name="SelecSquartRect"></param>
        /// <returns></returns>
        GraphicsPath GetTheRightShape(RectangleF SelecSquartRect)
        {
            float W = SelecSquartRect.Width;
            float x = SelecSquartRect.X;
            float y = SelecSquartRect.Y;
            //画对号、、
            PointF R1 = new PointF(x + W / 10 * 1, y + W / 10 * 4.5f);
            PointF R11 = new PointF(x + W / 10 * 2.0f, y + W / 10 * 3.3f);
            PointF R2 = new PointF(x + W / 10 * 4, y + W / 10 * 9);
            PointF R22 = new PointF(x + W / 10 * 4, y + W / 10 * 6.4f);
            PointF R3 = new PointF(x + W / 10 * 9, y + W / 10 * 2);
            PointF R33 = new PointF(x + W / 10 * 8, y + W / 10 * 1);

            GraphicsPath Rpath = new GraphicsPath();
            Rpath.AddLine(R1, R2);
            Rpath.AddLine(R2, R3);
            Rpath.AddLine(R3, R33);
            Rpath.AddLine(R33, R22);
            Rpath.AddLine(R22, R11);
            Rpath.AddLine(R11, R1);
            return Rpath;
        }

        [Obsolete]
        void DrawFillSelectSquart(Graphics Myg,Rectangle MyRect)
        {
           SolidBrush Fillbrush=new SolidBrush(SelectSquartFillColor);
            SolidBrush TheRightShapebrush = new SolidBrush(TheRightShapeFillColor);
            Pen InsideborderPen=new Pen(Fillbrush);

            RectangleF SelecSquartRect = new Rectangle();
          //内部正方形是宽高是 当前大小 高的 十分之八；
            SelecSquartRect.Width = MyRect.Height* InandOutBL;
            SelecSquartRect.Height = SelecSquartRect.Width;
            
            SelecSquartRect.X = MyRect.X+OutSiderBorderWidth;
            SelecSquartRect.Y = MyRect.Y+(MyRect.Height-SelecSquartRect.Height)/2;



            //选中画对号并填充内框
            if (Checked)
            {
                //是圆角矩形的时候
            if (IsInsideRoundRct)
            {
                GraphicsPath FillroundPath = DrawHelper.GetFillRoundRectFPath(SelecSquartRect, InSiderBorderWidth, Radius);
                Myg.FillPath(Fillbrush, FillroundPath);
               GraphicsPath Rpath = GetTheRightShape(SelecSquartRect);
                Myg.FillPath(TheRightShapebrush, Rpath);
            
            }
            else
            //是普通矩形的时候
                {
                 Myg.FillRectangle(Fillbrush, SelecSquartRect);
                    GraphicsPath Rpath = GetTheRightShape(SelecSquartRect);
                    Myg.FillPath(TheRightShapebrush, Rpath);

                }
                  


       
            }
            //checked 没有选中不画对号只画出 内边框
            else
            {
                if (IsInsideRoundRct)
                {
                    GraphicsPath FillroundPath = DrawHelper.GetFillRoundRectFPath(SelecSquartRect, InSiderBorderWidth, Radius);
                    Myg.FillPath(Fillbrush, FillroundPath);
                    FillroundPath.ClearMarkers();
                    RectangleF NullRct = new RectangleF();
                    NullRct.X=SelecSquartRect.X+SelecSquartRect.Width/8;
                    NullRct.Y = SelecSquartRect.Y + SelecSquartRect.Height / 8;
                    NullRct.Width = SelecSquartRect.Width / 8*6;
                    NullRct.Height = NullRct.Width;
                    GraphicsPath FillroundPath2 = DrawHelper.GetFillRoundRectFPath(NullRct, InSiderBorderWidth, Radius);
                    Myg.FillPath(new SolidBrush(Color.White), FillroundPath2);

                }
                else
                {
                    Myg.FillRectangle(Fillbrush, SelecSquartRect);
                    GraphicsPath Rpath = GetTheRightShape(SelecSquartRect);
                    RectangleF NullRct = new RectangleF();
                    NullRct.X = SelecSquartRect.X + SelecSquartRect.Width / 8;
                    NullRct.Y = SelecSquartRect.Y + SelecSquartRect.Height / 8;
                    NullRct.Width = SelecSquartRect.Width / 8 * 6;
                    NullRct.Height = NullRct.Width;
                    Myg.FillRectangle(new SolidBrush(Color.White),NullRct);
                }

            }





            //释放资源

            Fillbrush.Dispose();
            TheRightShapebrush.Dispose();
            InsideborderPen.Dispose();
        }



       /// <summary>
       /// 画控件外边框
       /// </summary>
       /// <param name="Myg"></param>
       /// <param name="MyRect"></param>
        void DrawOutSideBorder(Graphics Myg,Rectangle MyRect)
        {

            Pen OutSiderBorderPen=new Pen(OutSiderborderColor, OutSiderBorderWidth);
         
            Rectangle OutSideRect = new Rectangle();
             OutSideRect.X=MyRect.X+ OutSiderBorderWidth / 2;
            OutSideRect.Y = MyRect.Y+ OutSiderBorderWidth / 2;
            OutSideRect.Width = MyRect.Width - OutSiderBorderWidth;
            OutSideRect.Height = MyRect.Height - OutSiderBorderWidth;
            
          
            Myg.DrawRectangle(OutSiderBorderPen, OutSideRect);

            //释放资源
            OutSiderBorderPen.Dispose();


        }
        void  DrawText(Graphics Myg,Rectangle MyRect)
        {
            SolidBrush FontBrush = new SolidBrush(this.ForeColor);
            //计算文字的显示区域
            Rectangle TexRect =new Rectangle();
            int insiderWidth =Convert.ToInt32(MyRect.Height * InandOutBL) ;
           TexRect.X = MyRect.X + insiderWidth + InSiderBorderWidth * 2 + InSiderBorderWidth+5; 
          //当选择自动大小时候
            if (AutoSize)
            {
               
              int NowFontWidth =  (int) Myg.MeasureString(MyText, this.Font).Width;
                    
                this.Width= insiderWidth + InSiderBorderWidth * 2 + NowFontWidth+5;

            }
          //不自动大小的时候
            else
            {

            TexRect.Width=MyRect.Width - insiderWidth- InSiderBorderWidth * 2;

            }
         TexRect.Height = MyRect.Height;


            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Near ;
               
         
            sf.LineAlignment = StringAlignment.Center;
            Myg.DrawString(MyText, this.Font, FontBrush, TexRect, sf);

            //释放资源
            FontBrush.Dispose();


        }
 

    }
}
