//********************暂不支持图形的方向 默认 左方向，有必要后以后可以增加>*****************
//*************************************
//*************************************




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

namespace WHControlLib.Controls


{
  
    public partial class WHRadioButton : UserControl
    {
        public WHRadioButton()
        {
            //设置双缓冲

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.AllPaintingInWmPaint, true);

          ////背景定义为透明色   
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        this.BackColor = Color.Transparent;
            AutoSize = false;
          
            InitializeComponent();

         
    

        }
        //*************全局参数定义*************
        Rectangle MyRect=new Rectangle();
        Rectangle DrawRect=new Rectangle();
        //Rectangle TextRect = new Rectangle();
        Rectangle ShapeRect=new Rectangle();
        //Rectangle ShapeRect = new Rectangle();
        Rectangle inShapeRect = new Rectangle();

        //***************************************



        #region 属性，字段定义
        public enum Shape
        {
            Circle,Square
        }
        private Shape _myShape=Shape.Circle;
        [Category("A我的"), Description("本控件的形状，默认，圆形"), Browsable(true)]
        public Shape MyShape
        {
            get { return _myShape; }
            set { _myShape = value; Invalidate(); }
        }

        ////当用隐藏的Text属性时必须这么加特性才能使本控件使用
        [Category("A我的"), Description("文字在控件上显示文字，默认"), Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text { get; set; }


        private float _shapeBl=0.7f;
        [Category("A我的"), Description("本控件选择形状相对于整体高度的比例，默认，0.7f，不能大于1.0f"), Browsable(true)]
        public float ShapeBl
        {
            get { return _shapeBl; }
            set { _shapeBl = value; Invalidate(); }
        }
        private Color _unEnableColor=Color.Gray ;
        [Category("A我的"), Description("当不可用时候的颜色，默认，灰色"), Browsable(true)]
        public Color UnEnableColor
        {
            get { return _unEnableColor ; }
            set { _unEnableColor = value; }
        }

        private bool _isShowShapeBorder=true;
        [Category("A我的"), Description("是否显示本控件的选择图形的外边框，默认，true"), Browsable(true)]
        public bool IsShowShapeBorder
        {
            get { return _isShowShapeBorder; }
            set { _isShowShapeBorder = value; Invalidate(); }
        }

        private float _shapeBorderWidth=2.0f;
        [Category("A我的"), Description("本控件选择形状外框的线宽度，默认，2.0f，并不能小于0"), Browsable(true)]
        public float ShapeBorderWidth
        {
            get { return _shapeBorderWidth; }
            set
            {
                if (value<=0)
                {
                    value = 1.0f;

                }else
                
                _shapeBorderWidth = value;  Invalidate();
            }
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
        private Color _shapeBorderColor=Color.Orange;
        [Category("A我的"), Description("本控件的选择图形的外边框颜色，默认，橙色"), Browsable(true)]
        public Color ShapeBorderColor
        {
            get { return _shapeBorderColor; }
            set { _shapeBorderColor = value; Invalidate(); }
        }

        private Color _shapeFillColor=Color.White;
        [Category("A我的"), Description("本控件的选择图形的内部填充颜色，默认，白色"), Browsable(true)]
        public Color ShapeFillColor
        {
            get { return _shapeFillColor; }
            set { _shapeFillColor = value; Invalidate(); }
        }

        private Color _inShapeColor=Color.Orange;
        [Category("A我的"), Description("本控件选中后的小图形填充颜色，默认，橙色"), Browsable(true)]
        public Color InShapeColor
        {
            get { return _inShapeColor; }
            set { _inShapeColor = value; Invalidate(); }
        }
        private float _inshapeBl = 0.7f;
        [Category("A我的"), Description("本控件选中后的小图形相对于外框高度的比例，默认，0.7f，不能大于1.0f"), Browsable(true)]
        public float InShapeBl
        {
            get { return _inshapeBl; }
            set { _inshapeBl = value; Invalidate(); }
        }

        private bool _checked;
        [Category("A我的"), Description("本控件是否被选中，默认，falsef"), Browsable(true)]
        public bool Checked
        {
            get { return _checked; }
            set {
                     _checked = value;
                if (value)
                {
                  
                    try
                    {
                        if (Parent == null) return;
                     
                        foreach (Control item in this.Parent.Controls)
                        {
                            if (item==this)
                            {
                                continue;
                            }
                            if (item is WHRadioButton)
                            {
                                //WHRadioButton ra= (WHRadioButton)item;
                                //ra.Checked = false;
                                this.Invoke(new MethodInvoker(() =>
                                  {
                                      WHRadioButton Ra = (WHRadioButton)item;
                                      Ra.Checked = false;
                                  }));


                            }  }
                         
                    }
                    catch (Exception)
                    {

                        //throw;
                    }

                }


              
                Invalidate();

            }
        }

        private Color _myTextColor=Color.Black;
        [Category("A我的"), Description("本控件文字的颜色，默认，黑色"), Browsable(true)]
        public Color MyTextColor
        {
            get { return _myTextColor; }
            set { _myTextColor = value; }
        }
      
        private Font _myFont = new Font("微软雅黑", 12.0f, 0, GraphicsUnit.Point, 1);
        [Category("A我的"), Description(" 控件字体，默认 微软雅黑12t"), Browsable(true)]
        public Font MyFont
        {
            get { return _myFont; }
            set { _myFont = value; this.Invalidate(); }
        }
        #endregion





        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (!Checked)
            {
                Checked = true;
               

            }

         


        }

        public void BeginPainIni()
        {
            MyRect = this.ClientRectangle;
            //将所画的形状范围整体缩小10个像素为了在父控件上背景透明
            DrawRect.X = MyRect.X +5;
            DrawRect.Y = MyRect.Y + 5;
            DrawRect.Width=MyRect.Width-10;
            DrawRect.Height = MyRect.Height - 10;
           

        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);

            BeginPainIni();

            Graphics Myg=pevent.Graphics;
            Myg.SmoothingMode = SmoothingMode.AntiAlias;
            Myg.CompositingQuality = CompositingQuality.HighQuality;
            Myg.InterpolationMode = InterpolationMode.HighQualityBicubic;
         
  
          
            DrawShape(Myg, MyRect);

        }




        public virtual void DrawShape(Graphics Myg,Rectangle Rect)

        {
        
            float H ;
            float W ;
            if (IsShowShapeBorder)
            {
                 H = Rect.Height * ShapeBl-ShapeBorderWidth;
                W = H;
                 ShapeRect.Width = (int) W;
                ShapeRect.Height = (int)H;
                ShapeRect.X =Rect.X+ (int)(ShapeBorderWidth / 2);
                ShapeRect.Y =Rect.Y+ (int)(ShapeBorderWidth / 2+Height/2-ShapeRect.Height/2);
              
              
            }
            else
            {
                 H = Rect.Height * ShapeBl;
                 W = H;
                 ShapeRect.Width = (int)W;
                ShapeRect.Height = (int)H;
                ShapeRect.X = Rect.X ;
                ShapeRect.Y = Rect.Y+(int)( Height / 2 - ShapeRect.Height / 2); 
               
              
            }

       ///////////确定内部选中时候的矩形大小

                inShapeRect.Height = (int)(ShapeRect.Height * InShapeBl);
                inShapeRect.Width = inShapeRect.Height;
                inShapeRect.X = ShapeRect.X + ShapeRect.Width / 2 - inShapeRect.Width / 2;
                inShapeRect.Y = ShapeRect.Y + ShapeRect.Height / 2 - inShapeRect.Height / 2;
            ///开始画具体图形
            ///
            switch (MyShape)
            {
                case Shape.Circle:
                    
                     DrawCircleShape(Myg, ShapeRect, inShapeRect);   
                    break;

                case Shape.Square:
                    DrawSquareShape(Myg, ShapeRect, inShapeRect);
                    break;
                default:
                    break;
            }

           DrawText(Myg,MyRect,ShapeRect );



        }
 
        /// <summary>
        /// 画圆形选择
        /// </summary>
        /// <param name="Myg"></param>
        /// <param name="ShapeRect">原外形大小</param>
        /// <param name="inShapeRect">内部选中点的大小</param>
        public virtual void DrawCircleShape(Graphics Myg,Rectangle ShapeRect, Rectangle inShapeRect)
            
        {
            GraphicsPath path=new GraphicsPath();
            path.AddEllipse(ShapeRect);
        //填充整体内部颜色始终填充
          using (SolidBrush FillBrush=new SolidBrush (ShapeFillColor))
            {
                Myg.FillPath(FillBrush, path);
            }
    
            ////是否可以用时分别填充颜色 
            if (IsShowShapeBorder)
            {
                if (Enabled)
                {
               using (Pen shapeboredrPen=new Pen (ShapeBorderColor,ShapeBorderWidth))
                {
                    Myg.DrawPath(shapeboredrPen, path);
                }
                }
                else
                {
                    using (Pen shapeboredrPen = new Pen(UnEnableColor, ShapeBorderWidth))
                    {
                        Myg.DrawPath(shapeboredrPen, path);
                    }
                } 
            }
            //当选中时候里面画个小圆
            if (Checked)
            {
                GraphicsPath inpath = new GraphicsPath();
                inpath.AddEllipse(inShapeRect);
                if (Enabled)
                {
                using (SolidBrush inshapeBrush=new SolidBrush(InShapeColor))
                {
                    Myg.FillPath(inshapeBrush, inpath);

                }
                }
                else
                {
                    using (SolidBrush inshapeBrush = new SolidBrush(UnEnableColor))
                    {
                        Myg.FillPath(inshapeBrush, inpath);

                    }
                }
            }




        }

        public virtual void DrawSquareShape(Graphics Myg, Rectangle ShapeRect, Rectangle inShapeRect)

        {
            GraphicsPath path = new GraphicsPath();
            path = DrawHelper.GetRoundRectangePath(ShapeRect, ShapeBorderWidth, Radius);
            //填充整体内部颜色始终填充
            using (SolidBrush FillBrush = new SolidBrush(ShapeFillColor))
            {
                Myg.FillPath(FillBrush, path);
            }

            ////是否可以用时分别填充颜色 
            if (IsShowShapeBorder)
            {
                if (Enabled)
                {
                    using (Pen shapeboredrPen = new Pen(ShapeBorderColor, ShapeBorderWidth))
                    {
                        Myg.DrawPath(shapeboredrPen, path);
                    }
                }
                else
                {
                    using (Pen shapeboredrPen = new Pen(UnEnableColor, ShapeBorderWidth))
                    {
                        Myg.DrawPath(shapeboredrPen, path);
                    }
                }
            }
            //当选中时候里面画个小圆
            if (Checked)
            {
                GraphicsPath inpath = new GraphicsPath();
                inpath = DrawHelper.GetRoundRectangePath(inShapeRect, 0, Radius);
                if (Enabled)
                {
                    using (SolidBrush inshapeBrush = new SolidBrush(InShapeColor))
                    {
                        Myg.FillPath(inshapeBrush, inpath);

                    }
                }
                else
                {
                    using (SolidBrush inshapeBrush = new SolidBrush(UnEnableColor))
                    {
                        Myg.FillPath(inshapeBrush, inpath);

                    }
                }
            }






        }

        public virtual void DrawText(Graphics Myg,Rectangle DrawRect,Rectangle ShapeRect)

        { Rectangle TextRect = new Rectangle();
                 StringFormat sf = new StringFormat();
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment= StringAlignment.Near;

            if (!AutoSize)
            {
                TextRect.X = ShapeRect.X + ShapeRect.Width + 2;
                TextRect.Y = DrawRect.Y ;
                TextRect.Width = DrawRect.Width - ShapeRect.Width -5;
                TextRect.Height = DrawRect.Height;
                using (SolidBrush textbush=new SolidBrush(MyTextColor))
                {
              
                    Myg.DrawString(Text, MyFont, textbush, TextRect,sf);
                }
             

            }
            else
            {
                int textheight =(int) Myg.MeasureString(Text, MyFont).Height; 
                int textwidth= (int)Myg.MeasureString(Text, MyFont).Width;
                int height = Math.Max(ShapeRect.Height, textheight);
                int width = ShapeRect.Width + textwidth + 5;
                this.Height = height;
                this.Width = width;

                TextRect.X = ShapeRect.X + ShapeRect.Width + 2;
                TextRect.Y = DrawRect.Y;
                TextRect.Width = DrawRect.Width - ShapeRect.Width ;
                TextRect.Height =height;
                using (SolidBrush textbush = new SolidBrush(MyTextColor))
                {
              
                    Myg.DrawString(Text, MyFont, textbush, TextRect, sf);
                }

            }

        }

        //////////////////////////////////////////////////////

    }
}
