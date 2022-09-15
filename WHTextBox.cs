using System;
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

namespace WHControlLib
{
    public partial class WHTextBox : UserControl
    {

      public  enum Shapes
        {
            HalfCircle,
            RoundRectange
        }
        
        public WHTextBox()
        {
            //设置双缓冲

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.AllPaintingInWmPaint, true);


            InitializeComponent();
            MyFont = this.textbox.Font;
        }
        #region 属性，字段，定义
 

        private int _borderWidth = 2;
        [Category("A我的"), DefaultValue(typeof(int), "2"), Description("控件外边框宽度，默认，2,不能小于1"), Browsable(true)]
        public int BorderWidth
        {
            get { return _borderWidth; }
            set { _borderWidth = value; Invalidate(); }
        }
        private Color _fillBackgroundColor=Color.White;
        [Category("A我的"), Description("内部填充颜色，默认，白色"), Browsable(true)]
        public Color FillBackgroundColor
        {
            get { return _fillBackgroundColor; }
            set { _fillBackgroundColor = value; 
                Invalidate();
            }
        }


        private Color _borderColor = Color.Black;
        [Category("A我的"), Description("边框颜色，默认，黑色"), Browsable(true)]
        public Color  BorderColor
        {
            get { return _borderColor; }
            set { _borderColor = value; Invalidate(); }
        }

        private Font _myFont ;
        [DefaultValue(typeof(Font), "宋体, 12pt")]
        [Category("A我的"), Description("字体及大小,单行模式下，控件大小随字体大小改变，其他字体设置失效，默认，"), Browsable(true)]
        public Font MyFont
        {
            get { return _myFont; }
            set
            {
                _myFont = value;
                textFontChange();

            }
        }

        private Shapes _myShape;
        [Category("A我的"), Description("边框形状外形，默认，两边半圆"), Browsable(true)]
        public Shapes MyShape
        {
            get { return _myShape; }
            set { _myShape = value; Invalidate(); }
        }

        private float _radius = 3.0f;
        [Category("A我的"), Description("如果选择方形时的 圆角弧度量，默认3.0f,不能小于1"), Browsable(true)]
        public float Radius
        {
            get { return _radius; }
            set
            {
                if (value < 1)
                {
                    _radius = 1.0f; Invalidate();
                }
                else _radius = value; Invalidate();
            }
        }

        //private  string _text = null;
        [Category("A我的"), Description("文本框显示的文字文本取代Text属性 ，只能用MyText不能用Text属性，默认 无文字，"), Browsable(true)]


        public  string MyText
        {
            get {  return textbox.Text; }

            set { textbox.Text = value; ; }

        }

        //private bool _isDisplayWaterMark=false;
        [Category("A我的"), Description("是否显示水印，默认 ，不显示水印文字，"), Browsable(true)]
        public bool IsDisplayWaterMark
        {
            get { return textbox.IsDisplayWaterMark; }
            set {textbox.IsDisplayWaterMark = value; Invalidate(); }
        }
        //private Color _waterMarkColor = Color.Gray;
        [Category("A我的"), Description("水印文字的颜色，默认 ，浅灰色，"), Browsable(true)]
        public Color WaterMarkColor
        {
            get { return textbox.WatermarkColor; }
            set { textbox.WatermarkColor = value; Invalidate(); }
        }


        //private string _waterMarkText="";
        [Category("A我的"), Description("水印要显示的文字，默认 ，没有文字，"), Browsable(true)]
        public string WaterMarkText
        {
            get { return textbox.WaterMarkstring; }
            set {textbox.WaterMarkstring= value; Invalidate(); }
        }
     public enum WaterMarkDirection
        {
           left=0,
           center=1,
           right=2,


        }


        [Category("A我的"), Description("水印文字的对齐方向，默认 ，左对齐，"), Browsable(true)]
        public  WaterMarkDirection WatermarkDirection
        {
            get { return (WaterMarkDirection)  textbox.WaterMarkDire; }
            set {  textbox.WaterMarkDire= (int)value; Invalidate(); }
        }


        #endregion



        void textFontChange()
        {
            this.textbox.Font = MyFont;
            this.Font = MyFont;

     
        }

  

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
          
            Graphics Myg = e.Graphics;
            Myg.SmoothingMode = SmoothingMode.AntiAlias;
            Myg.CompositingQuality = CompositingQuality.HighQuality;
            Myg.InterpolationMode = InterpolationMode.HighQualityBicubic;
           
            //**************资源申请********************
            SolidBrush backgroundBrush =new SolidBrush(Parent.BackColor);
            SolidBrush fillbackgroundBrush=new SolidBrush(FillBackgroundColor);
            //****************************************
      
              Rectangle Myrect = this.ClientRectangle;
              Myg.FillRectangle(backgroundBrush, Myrect);
        
            textbox.BackColor = FillBackgroundColor;
           
            //根据所选形状 **********   获得 外边框路径**********
            GraphicsPath OutsidePath;
            if (MyShape==Shapes.HalfCircle)
            {  
                //**********两边半圆的形状下 定义textbox的坐标**********
            int textboxX = Convert.ToInt32(Myrect.X - BorderWidth / 2 + Myrect.Height / 2);
            int textboxY = Convert.ToInt32(Myrect.Y + BorderWidth );
            int textboxwidth = Convert.ToInt32(Myrect.Width - Myrect.Height + BorderWidth );
            textbox.Left = textboxX;
            textbox.Top = textboxY+3;
            textbox.Width= textboxwidth;
                textbox.Height = Myrect.Height - BorderWidth * 3 - 6;
                //this.Height = textbox.Height + BorderWidth * 2+6;
                OutsidePath = DrawHelper.GetTwoHalfCircleRect(Myrect, BorderWidth);
            }
            else

            {
                float hw = Myrect.Height / Radius;
                int textboxX = Convert.ToInt32( BorderWidth+hw);
                int textboxY = Convert.ToInt32( BorderWidth);
                int textboxwidth = Convert.ToInt32(Myrect.Width - BorderWidth*2-hw*2);
               
                textbox.Left = textboxX;
                textbox.Top = textboxY+3;
                textbox.Width = textboxwidth;
                //this.Height = textbox.Height + BorderWidth * 2;
                textbox.Height = Myrect.Height - BorderWidth * 3 - 6;

                OutsidePath = DrawHelper.GetRoundRectangePath(Myrect, BorderWidth, Radius);
            }
            //内部填充同色
            Myg.FillPath(fillbackgroundBrush, OutsidePath);
            //画外边框

            DrawOUtsideBorder(Myg, OutsidePath);
 
           

            ////释放资源
            ///
            fillbackgroundBrush.Dispose();
            backgroundBrush.Dispose();


        }
        /// <summary>
        /// 画外边框
        /// </summary>
        /// <param name="g"></param>
        /// <param name="path"></param>
            void DrawOUtsideBorder(Graphics g,GraphicsPath path)
            {
            using (SolidBrush sb = new SolidBrush(BorderColor)       )
            {
                     using (   Pen pen = new Pen(sb, BorderWidth))
                   {
                              g.DrawPath(pen,path);
                    }
            }
            }

        protected override void OnLayout(LayoutEventArgs e)
        {
            base.OnLayout(e);
         
        }
    }
    }

