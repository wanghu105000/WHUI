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
    public partial class WHRadioButton : RadioButton
    {
        public WHRadioButton()
        {
            //设置双缓冲

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.AllPaintingInWmPaint, true);
            AutoSize = false;
            InitializeComponent();

            ////背景定义为透明色
            //this.BackColor = Color.Transparent;
          
        }
        //*************全局参数定义*************
        Rectangle MyRect=new Rectangle();
        Rectangle DrawRect=new Rectangle();
        Rectangle TextRect = new Rectangle();
        Rectangle ShapeRect=new Rectangle();


        //***************************************



        #region 属性，字段定义
        public enum Shape
        {
            Circle,Square
        }
        private Shape _myShape=Shape.Circle;
        [Category("我的"), Description("本控件的形状，默认，圆形"), Browsable(true)]
        public Shape MyShape
        {
            get { return _myShape; }
            set { _myShape = value; Invalidate(); }
        }

        private float _shapeBl=0.7f;
        [Category("我的"), Description("本控件选择形状相对于整体高度的比例，默认，0.7f，不能大于1.0f"), Browsable(true)]
        public float ShapeBl
        {
            get { return _shapeBl; }
            set { _shapeBl = value; Invalidate(); }
        }
        private Color _unEnableColor=Color.Gray ;
        [Category("我的"), Description("当不可用时候的颜色，默认，灰色"), Browsable(true)]
        public Color UnEnableColor
        {
            get { return _unEnableColor ; }
            set { _unEnableColor = value; }
        }

        private bool _isShowShapeBorder=true;
        [Category("我的"), Description("是否显示本控件的选择图形的外边框，默认，true"), Browsable(true)]
        public bool IsShowShapeBorder
        {
            get { return _isShowShapeBorder; }
            set { _isShowShapeBorder = value; Invalidate(); }
        }

        private float _shapeBorderWidth=1.0f;
        [Category("我的"), Description("本控件选择形状外框的线宽度，默认，1.0f，并不能小于0"), Browsable(true)]
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

        private Color _shapeBorderColor=Color.Orange;
        [Category("我的"), Description("本控件的选择图形的外边框颜色，默认，橙色"), Browsable(true)]
        public Color ShapeBorderColor
        {
            get { return _shapeBorderColor; }
            set { _shapeBorderColor = value; Invalidate(); }
        }

        private Color _shapeFillColor=Color.White;
        [Category("我的"), Description("本控件的选择图形的内部填充颜色，默认，白色"), Browsable(true)]
        public Color ShapeFillColor
        {
            get { return _shapeFillColor; }
            set { _shapeFillColor = value; Invalidate(); }
        }

        private Color _inShapeColor=Color.Orange;
        [Category("我的"), Description("本控件选中后的小图形填充颜色，默认，橙色"), Browsable(true)]
        public Color InShapeColor
        {
            get { return _inShapeColor; }
            set { _inShapeColor = value; }
        }
        private float _inshapeBl = 0.7f;
        [Category("我的"), Description("本控件选中后的小图形相对于外框高度的比例，默认，0.7f，不能大于1.0f"), Browsable(true)]
        public float InShapeBl
        {
            get { return _shapeBl; }
            set { _shapeBl = value; Invalidate(); }
        }
        #endregion

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            AutoSize = false;
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
           Myg.Clear(this.Parent.BackColor);
            DrawShape(Myg, MyRect);

        }

        public virtual void DrawShape(Graphics Myg,Rectangle Rect)

        {
            Rectangle ShapeRect = new Rectangle();
            Rectangle inShapeRect = new Rectangle();
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
            DrawCircleShape(Myg, ShapeRect, inShapeRect);



        }


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


//////////////////////////////////////////////////////

    }
}
