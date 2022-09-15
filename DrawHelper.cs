using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace WHControlLib
{

//    2.避免闪烁
//a.自定义控件类，构造函数中加入

//public void XXXClass()
//    {
//        this.DoubleBuffered = true; // 双缓冲
//        SetStyle(ControlStyles.UserPaint, true);
//        SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
//        SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲
//    }
//    b.父窗体类，重载CreateParams

////加载usercontrl控件不闪烁

//protected override CreateParams CreateParams
//    {
//        get
//        {
//            CreateParams cp = base.CreateParams;
//            cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
//            return cp;
//        }
//    }



    public class DrawHelper
    {
        /// <summary>
        /// 获得要填充的圆角矩形的路径
        /// </summary>
        /// <param name="Myrect">要填充圆角矩形的矩形</param>
        /// <param name="borderWidth">该矩形外框的线宽</param>
        /// <param name="Radius">圆角度，越大圆角越小不能小于2.0f</param>
        /// <returns></returns>
        //[Obsolete("此方法是旧方法; ")]
        public static GraphicsPath GetFillRoundRectFPath(RectangleF Myrect, int borderWidth, float Radius)
        {
            float RadiusWidth = Myrect.Width / Radius;
            //int RadiusHeight = Myrect.Height / Radius;
            float RadiusHeight = RadiusWidth;
            //定义画弧线的四个角的矩形

            RectangleF LeftTopRect = new RectangleF(Myrect.X + borderWidth, Myrect.Y + borderWidth, RadiusWidth, RadiusHeight);
            RectangleF RightTopRect = new RectangleF(Myrect.X + Myrect.Width - RadiusWidth - borderWidth, Myrect.Y + borderWidth, RadiusWidth, RadiusHeight);
            RectangleF RightButtonRect = new RectangleF(Myrect.X + Myrect.Width - RadiusWidth - borderWidth, Myrect.Y + Myrect.Height - RadiusHeight - borderWidth, RadiusWidth, RadiusHeight);
            RectangleF LeftButtomRect = new RectangleF(Myrect.X + borderWidth, Myrect.Y + Myrect.Height - RadiusHeight - borderWidth, RadiusWidth, RadiusHeight);
            //定义需要连接的8给点

            PointF LeftTopPointH = new PointF((float)Myrect.X + LeftTopRect.Width / 2, (float)Myrect.Y + borderWidth);
            PointF LeftTopPointV = new PointF((float)Myrect.X + borderWidth, (float)Myrect.Y + LeftTopRect.Height / 2);

            PointF RightTopPointH = new PointF((float)Myrect.X + Myrect.Width - RightTopRect.Width / 2, (float)Myrect.Y + borderWidth);
            PointF RightTopPointV = new PointF((float)Myrect.X + Myrect.Width - borderWidth, (float)Myrect.Y + RightTopRect.Height / 2);

            PointF RightButtonPointV = new PointF((float)Myrect.X + Myrect.Width - borderWidth, (float)Myrect.Y + Myrect.Height - RightButtonRect.Height / 2);
            PointF RightButtonPointH = new PointF((float)Myrect.X + Myrect.Width - RightButtonRect.Width / 2, (float)Myrect.Y + Myrect.Height - borderWidth);

            PointF LeftButtonPointH = new PointF((float)Myrect.X + LeftButtomRect.Width / 2, (float)Myrect.Y + Myrect.Height - borderWidth);
            PointF LeftButtonPointV = new PointF((float)Myrect.X + borderWidth, (float)Myrect.Y + Myrect.Height - LeftButtomRect.Height / 2);
            //构造填充路径
            GraphicsPath myfillPath = new GraphicsPath();
            myfillPath.AddArc(LeftTopRect, 180, 90);
            myfillPath.AddLine(LeftTopPointH, RightTopPointH);
            myfillPath.AddArc(RightTopRect, 270, 90);
            myfillPath.AddLine(RightTopPointV, RightButtonPointV);
            myfillPath.AddArc(RightButtonRect, 0, 90);
            myfillPath.AddLine(RightButtonPointH, LeftButtonPointH);
            myfillPath.AddArc(LeftButtomRect, 90, 90);
            myfillPath.AddLine(LeftButtonPointV, LeftTopPointV);
            return myfillPath;

        }

        //public static GraphicsPath GetBorderRoundRectFPath(RectangleF Myrect, int borderWidth, float Radius)
        //{


        //    float RadiusWidth = Myrect.Width / Radius;
        //    //int RadiusHeight = Myrect.Height / Radius;
        //    float RadiusHeight = RadiusWidth;
        //    //定义画弧线的四个角的矩形

        //    RectangleF LeftTopRect = new RectangleF(Myrect.X + borderWidth, Myrect.Y + borderWidth, RadiusWidth, RadiusHeight);
        //    RectangleF RightTopRect = new RectangleF(Myrect.X + Myrect.Width - RadiusWidth - borderWidth, Myrect.Y + borderWidth, RadiusWidth, RadiusHeight);
        //    RectangleF RightButtonRect = new RectangleF(Myrect.X + Myrect.Width - RadiusWidth - borderWidth, Myrect.Y + Myrect.Height - RadiusHeight - borderWidth, RadiusWidth, RadiusHeight);
        //    RectangleF LeftButtomRect = new RectangleF(Myrect.X + borderWidth, Myrect.Y + Myrect.Height - RadiusHeight - borderWidth, RadiusWidth, RadiusHeight);
        //    //定义需要连接的8给点

        //    PointF LeftTopPointH = new PointF((float)Myrect.X + LeftTopRect.Width / 2, (float)Myrect.Y + borderWidth);
        //    PointF LeftTopPointV = new PointF((float)Myrect.X + borderWidth, (float)Myrect.Y + LeftTopRect.Height / 2);

        //    PointF RightTopPointH = new PointF((float)Myrect.X + Myrect.Width - RightTopRect.Width / 2, (float)Myrect.Y + borderWidth);
        //    PointF RightTopPointV = new PointF((float)Myrect.X + Myrect.Width - borderWidth, (float)Myrect.Y + RightTopRect.Height / 2);

        //    PointF RightButtonPointV = new PointF((float)Myrect.X + Myrect.Width - borderWidth, (float)Myrect.Y + Myrect.Height - RightButtonRect.Height / 2);
        //    PointF RightButtonPointH = new PointF((float)Myrect.X + Myrect.Width - RightButtonRect.Width / 2, (float)Myrect.Y + Myrect.Height - borderWidth);

        //    PointF LeftButtonPointH = new PointF((float)Myrect.X + LeftButtomRect.Width / 2, (float)Myrect.Y + Myrect.Height - borderWidth);
        //    PointF LeftButtonPointV = new PointF((float)Myrect.X + borderWidth, (float)Myrect.Y + Myrect.Height - LeftButtomRect.Height / 2);
        //    //画边框
        //    GraphicsPath path = new GraphicsPath();
        //    Myg.DrawArc(BorderPen, LeftTopRect, 180, 90);
        //    path.AddArc
        //    Myg.DrawLine(BorderPen, LeftTopPointH, RightTopPointH);
        //    Myg.DrawArc(BorderPen, RightTopRect, 270, 90);
        //    Myg.DrawLine(BorderPen, RightTopPointV, RightButtonPointV);
        //    Myg.DrawArc(BorderPen, RightButtonRect, 0, 90);
        //    Myg.DrawLine(BorderPen, RightButtonPointH, LeftButtonPointH);
        //    Myg.DrawArc(BorderPen, LeftButtomRect, 90, 90);
        //    Myg.DrawLine(BorderPen, LeftButtonPointV, LeftTopPointV);



        //}
       public static GraphicsPath GetRoundRectangePath(Rectangle MYRect,int BorderWidth,float Radius)

        {
            const float textboxlinewidth = 2.0f;

            GraphicsPath path = new GraphicsPath();
            float  PenWidth=BorderWidth/2;
           float HWheight = MYRect.Height / Radius;
            //float w = HWheight- textboxlinewidth;
            float w = HWheight ;
            /*     */
            //float h = HWheight ;
            RectangleF topLeftRect=new RectangleF(MYRect.X+PenWidth, MYRect.Y+PenWidth, w, w);
            RectangleF topRightRect = new RectangleF(MYRect.X + MYRect.Width  - HWheight-textboxlinewidth, topLeftRect.Y, w, w);
            RectangleF bottomLeftRact=new RectangleF(topLeftRect.X, MYRect.Y+MYRect.Height - HWheight-textboxlinewidth, w, w);
            RectangleF bottomRightRact=new RectangleF(topRightRect.X, bottomLeftRact.Y, w, w);
            path.AddArc(topLeftRect, 180, 90);
            path.AddArc(topRightRect, 270, 90);

            path.AddArc(bottomRightRact, 0, 90);

            path.AddArc(bottomLeftRact, 90, 90);
            path.CloseAllFigures();
            return path;
      

        }

        /// <summary>
        /// 获得 两头是半圆的矩形路径
        /// </summary>
        /// <param name="MyRect">在那个矩形里绘制</param>
        /// <param name="BorderWidth">线宽</param>
        /// <returns></returns>
        public static GraphicsPath GetTwoHalfCircleRect(Rectangle MyRect, int BorderWidth)
        {
            int workTop = Convert.ToInt32(MyRect.Top + BorderWidth / 2);
            int workLeft = Convert.ToInt32(MyRect.Left + BorderWidth / 2);
            int workwidth = Convert.ToInt32(MyRect.Width - BorderWidth);
            int workheight = Convert.ToInt32(MyRect.Height - BorderWidth);

            Rectangle WorkRct = new Rectangle(workTop, workLeft, workwidth, workheight);


            Rectangle leftRct = new Rectangle(WorkRct.Left, WorkRct.Top, workheight, workheight);
            Rectangle rightRct = new Rectangle(WorkRct.Left + WorkRct.Width - WorkRct.Height, WorkRct.Top, workheight, workheight);



            GraphicsPath path = new GraphicsPath();
            path.AddArc(leftRct, 90, 180);
            path.AddArc(rightRct, 270, 180);
            path.CloseAllFigures();
            return path;

        }
        #region 暂时为使用的方法

        /// <summary>
        /// 得到两种颜色的过渡色（1代表开始色，100表示结束色）
        /// </summary>
        /// <param name="c">开始色</param>
        /// <param name="c2">结束色</param>
        /// <param name="value">需要获得的度</param>
        /// <returns></returns>
        public static Color GetIntermediateColor(Color c, Color c2, int value)
        {
            float pc = value * 1.0F / 100;

            int ca = c.A, cr = c.R, cg = c.G, cb = c.B;
            int c2a = c2.A, c2r = c2.R, c2g = c2.G, c2b = c2.B;

            int a = (int)Math.Abs(ca + (ca - c2a) * pc);
            int r = (int)Math.Abs(cr - ((cr - c2r) * pc));
            int g = (int)Math.Abs(cg - ((cg - c2g) * pc));
            int b = (int)Math.Abs(cb - ((cb - c2b) * pc));

            if (a > 255) { a = 255; }
            if (r > 255) { r = 255; }
            if (g > 255) { g = 255; }
            if (b > 255) { b = 255; }

            return (Color.FromArgb(a, r, g, b));
        }

        public static StringFormat StringFormatAlignment(ContentAlignment textalign)
        {
            StringFormat sf = new StringFormat();
            switch (textalign)
            {
                case ContentAlignment.TopLeft:
                case ContentAlignment.TopCenter:
                case ContentAlignment.TopRight:
                    sf.LineAlignment = StringAlignment.Near;
                    break;
                case ContentAlignment.MiddleLeft:
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.MiddleRight:
                    sf.LineAlignment = StringAlignment.Center;
                    break;
                case ContentAlignment.BottomLeft:
                case ContentAlignment.BottomCenter:
                case ContentAlignment.BottomRight:
                    sf.LineAlignment = StringAlignment.Far;
                    break;
            }
            return sf;
        }
        public static Font GetFontForTextBoxHeight(int TextBoxHeight, Font OriginalFont)
        {

            float dsheight = (float)TextBoxHeight;
            Font fnt = new Font(OriginalFont.FontFamily, OriginalFont.Size, OriginalFont.Style, GraphicsUnit.Pixel);
            if (dsheight < 8) dsheight = 8;

            float FontEmSize = fnt.FontFamily.GetEmHeight(fnt.Style);
            float FontLineSpacing = fnt.FontFamily.GetLineSpacing(fnt.Style);
            float emSize = (dsheight - 7) * FontEmSize / FontLineSpacing;
            fnt = new Font(fnt.FontFamily, emSize, fnt.Style, GraphicsUnit.Pixel);
            return fnt;

        }


        #endregion




    }

    public  static class Win32
    {
      

        public const int WM_KEYDOWN = 0x0100;
        public const int WM_KEYUP = 0x0101;
        public const int WM_CTLCOLOREDIT = 0x133;
        public const int WM_ERASEBKGND = 0x0014;
        public const int WM_LBUTTONDOWN = 0x0201;
        public const int WM_LBUTTONUP = 0x0202;
        public const int WM_LBUTTONDBLCLK = 0x0203;
        public const int WM_WINDOWPOSCHANGING = 0x46;
        public const int WM_PAINT = 0xF;
        public const int WM_CREATE = 0x0001;
        public const int WM_ACTIVATE = 0x0006;
        public const int WM_NCCREATE = 0x0081;
        public const int WM_NCCALCSIZE = 0x0083;
        public const int WM_NCPAINT = 0x0085;
        public const int WM_NCACTIVATE = 0x0086;
        public const int WM_NCLBUTTONDOWN = 0x00A1;
        public const int WM_NCLBUTTONUP = 0x00A2;
        public const int WM_NCLBUTTONDBLCLK = 0x00A3;
        public const int WM_NCMOUSEMOVE = 0x00A0;

        public const int WM_NCHITTEST = 0x0084;

        public const int HTLEFT = 10;
        public const int HTRIGHT = 11;
        public const int HTTOP = 12;
        public const int HTTOPLEFT = 13;
        public const int HTTOPRIGHT = 14;
        public const int HTBOTTOM = 15;
        public const int HTBOTTOMLEFT = 0x10;
        public const int HTBOTTOMRIGHT = 17;
        public const int HTCAPTION = 2;
        public const int HTCLIENT = 1;

        public const int WM_FALSE = 0;
        public const int WM_TRUE = 1;




   

        [DllImport("gdi32.dll")]
        public static extern int CreateRoundRectRgn(int x1, int y1, int x2, int y2, int x3, int y3);

        [DllImport("user32.dll")]
        public static extern int SetWindowRgn(IntPtr hwnd, int hRgn, Boolean bRedraw);

        [DllImport("gdi32.dll", EntryPoint = "DeleteObject", CharSet = CharSet.Ansi)]
        public static extern int DeleteObject(int hObject);

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
    }


}
