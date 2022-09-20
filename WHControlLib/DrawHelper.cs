using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.IO;

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

        public static GraphicsPath GetFillRoundRectFPath(RectangleF Myrect, float borderWidth, float Radius)
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
        /// <summary>
        /// Icon格式转image
        /// </summary>
        /// <param name="icon"></param>
        /// <returns></returns>
        public static Image IconToImage(Icon icon)
        {
            MemoryStream mStream = new MemoryStream();
            icon.Save(mStream);
            Image image = Image.FromStream(mStream);
            return image;
        }

        public void SetBits(Bitmap bitmap, IntPtr FormHandle, bool haveHandle )
        {
            if (!haveHandle) return;

            if (!Bitmap.IsCanonicalPixelFormat(bitmap.PixelFormat) || !Bitmap.IsAlphaPixelFormat(bitmap.PixelFormat))
                throw new ApplicationException("The picture must be 32bit picture with alpha channel.");

            IntPtr oldBits = IntPtr.Zero;
            IntPtr screenDC = Win32.GetDC(IntPtr.Zero);
            IntPtr hBitmap = IntPtr.Zero;
            IntPtr memDc = Win32.CreateCompatibleDC(screenDC);

            try
            {
                //Win32.Point topLoc = new Win32.Point(Left, Top);
                Win32.Size bitMapSize = new Win32.Size(bitmap.Width, bitmap.Height);
                Win32.BLENDFUNCTION blendFunc = new Win32.BLENDFUNCTION();
                Win32.Point srcLoc = new Win32.Point(0, 0);

                hBitmap = bitmap.GetHbitmap(Color.FromArgb(0));
                oldBits = Win32.SelectObject(memDc, hBitmap);

                blendFunc.BlendOp = Win32.AC_SRC_OVER;
                blendFunc.SourceConstantAlpha = 255;//这里设置窗体绘制的透明度
                blendFunc.AlphaFormat = Win32.AC_SRC_ALPHA;
                blendFunc.BlendFlags = 0;

                //Win32.UpdateLayeredWindow(FormHandle, screenDC, ref topLoc, ref bitMapSize, memDc, ref srcLoc, 0, ref blendFunc, Win32.ULW_ALPHA);
            }
            finally
            {
                if (hBitmap != IntPtr.Zero)
                {
                    Win32.SelectObject(memDc, oldBits);
                    Win32.DeleteObject(hBitmap);
                }
                Win32.ReleaseDC(IntPtr.Zero, screenDC);
                Win32.DeleteDC(memDc);
            }
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

    //public  static class Win32
    //{
      

    //    public const int WM_KEYDOWN = 0x0100;
    //    public const int WM_KEYUP = 0x0101;
    //    public const int WM_CTLCOLOREDIT = 0x133;
    //    public const int WM_ERASEBKGND = 0x0014;
    //    public const int WM_LBUTTONDOWN = 0x0201;
    //    public const int WM_LBUTTONUP = 0x0202;
    //    public const int WM_LBUTTONDBLCLK = 0x0203;
    //    public const int WM_WINDOWPOSCHANGING = 0x46;
    //    public const int WM_PAINT = 0xF;
    //    public const int WM_CREATE = 0x0001;
    //    public const int WM_ACTIVATE = 0x0006;
    //    public const int WM_NCCREATE = 0x0081;
    //    public const int WM_NCCALCSIZE = 0x0083;
    //    public const int WM_NCPAINT = 0x0085;
    //    public const int WM_NCACTIVATE = 0x0086;
    //    public const int WM_NCLBUTTONDOWN = 0x00A1;
    //    public const int WM_NCLBUTTONUP = 0x00A2;
    //    public const int WM_NCLBUTTONDBLCLK = 0x00A3;
    //    public const int WM_NCMOUSEMOVE = 0x00A0;

    //    public const int WM_NCHITTEST = 0x0084;

    //    public const int HTLEFT = 10;
    //    public const int HTRIGHT = 11;
    //    public const int HTTOP = 12;
    //    public const int HTTOPLEFT = 13;
    //    public const int HTTOPRIGHT = 14;
    //    public const int HTBOTTOM = 15;
    //    public const int HTBOTTOMLEFT = 0x10;
    //    public const int HTBOTTOMRIGHT = 17;
    //    public const int HTCAPTION = 2;
    //    public const int HTCLIENT = 1;

    //    public const int WM_FALSE = 0;
    //    public const int WM_TRUE = 1;




   

    //    [DllImport("gdi32.dll")]
    //    public static extern int CreateRoundRectRgn(int x1, int y1, int x2, int y2, int x3, int y3);

    //    [DllImport("user32.dll")]
    //    public static extern int SetWindowRgn(IntPtr hwnd, int hRgn, Boolean bRedraw);

    //    [DllImport("gdi32.dll", EntryPoint = "DeleteObject", CharSet = CharSet.Ansi)]
    //    public static extern int DeleteObject(int hObject);

    //    [DllImport("user32.dll")]
    //    public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

    //    [DllImport("user32.dll")]
    //    public static extern bool ReleaseCapture();
    //}
    //////////////
    ///

  public static class Win32Msg
    {
        public const int WM_NULL = 0x0000;
       /// <summary>
       /// 应用程序创建一个窗口
       /// </summary>
        public const int WM_CREATE = 0x0001;
       /// <summary>
       /// 一个窗口被销毁
       /// </summary>
        public const int WM_DESTROY = 0x0002;
       /// <summary>
       /// 移动一个窗口
       /// </summary>
        public const int WM_MOVE = 0x0003;
        /// <summary>
        /// 改变一个窗口的大小
        /// </summary>
        public const int WM_SIZE = 0x0005;
        /// <summary>
        /// 一个窗口被激活或失去激活状态；
        /// </summary>
        public const int WM_ACTIVATE = 0x0006;
      /// <summary>
      ///  获得焦点后
      /// </summary>
        public const int WM_SETFOCUS = 0x0007;
       /// <summary>
       /// 失去焦点
       /// </summary>
        public const int WM_KILLFOCUS = 0x0008;
       /// <summary>
       /// 改变enable状态
       /// </summary>
        public const int WM_ENABLE = 0x000A;
       /// <summary>
       /// 设置窗口是否能重画
       /// </summary>
        public const int WM_SETREDRAW = 0x000B;
       /// <summary>
       /// 应用程序发送此消息来设置一个窗口的文本
       /// </summary>
        public const int WM_SETTEXT = 0x000C;
        /// <summary>
        /// 应用程序发送此消息来复制对应窗口的文本到缓冲区
        /// </summary>
        public const int WM_GETTEXT = 0x000D;
        /// <summary>
        /// 得到与一个窗口有关的文本的长度（不包含空字符）
        /// </summary>
        public const int WM_GETTEXTLENGTH = 0x000E;
        /// <summary>
        /// 要求一个窗口重画自己
        /// </summary>
        public const int WM_PAINT = 0x000F;
        /// <summary>
        /// 当一个窗口或应用程序要关闭时发送一个信号
        /// </summary>
        public const int WM_CLOSE = 0x0010;
       /// <summary>
       /// 当用户选择结束对话框或程序自己调用ExitWindows函数
       /// </summary>
        public const int WM_QUERYENDSESSION = 0x0011;
       /// <summary>
       /// 用来结束程序运行或当程序调用postquitmessage函数
       /// </summary>
        public const int WM_QUIT = 0x0012;
       /// <summary>
       /// 当用户窗口恢复以前的大小位置时，把此消息发送给某个图标
       /// </summary>
        public const int WM_QUERYOPEN = 0x0013;
        /// <summary>
        /// 当窗口背景必须被擦除时（例在窗口改变大小时）
        /// </summary>
        public const int WM_ERASEBKGND = 0x0014;
        
        public const int WM_SYSCOLORCHANGE = 0x0015;
       /// <summary>
       /// 当系统颜色改变时，发送此消息给所有顶级窗口
       /// </summary>
        public const int WM_ENDSESSION = 0x0016;
        public const int WM_SHOWWINDOW = 0x0018;
        public const int WM_WININICHANGE = 0x001A;
        public const int WM_SETTINGCHANGE = 0x001A;
        public const int WM_DEVMODECHANGE = 0x001B;
        public const int WM_ACTIVATEAPP = 0x001C;
        public const int WM_FONTCHANGE = 0x001D;
        public const int WM_TIMECHANGE = 0x001E;
        public const int WM_CANCELMODE = 0x001F;
        public const int WM_SETCURSOR = 0x0020;
        public const int WM_MOUSEACTIVATE = 0x0021;
        public const int WM_CHILDACTIVATE = 0x0022;
        public const int WM_QUEUESYNC = 0x0023;
        public const int WM_GETMINMAXINFO = 0x0024;
        public const int WM_PAINTICON = 0x0026;
        public const int WM_ICONERASEBKGND = 0x0027;
        public const int WM_NEXTDLGCTL = 0x0028;
        public const int WM_SPOOLERSTATUS = 0x002A;
        public const int WM_DRAWITEM = 0x002B;
        public const int WM_MEASUREITEM = 0x002C;
        public const int WM_DELETEITEM = 0x002D;
        public const int WM_VKEYTOITEM = 0x002E;
        public const int WM_CHARTOITEM = 0x002F;
        public const int WM_SETFONT = 0x0030;
        public const int WM_GETFONT = 0x0031;
        public const int WM_SETHOTKEY = 0x0032;
        public const int WM_GETHOTKEY = 0x0033;
        public const int WM_QUERYDRAGICON = 0x0037;
        public const int WM_COMPAREITEM = 0x0039;
        public const int WM_GETOBJECT = 0x003D;
        public const int WM_COMPACTING = 0x0041;
        public const int WM_COMMNOTIFY = 0x0044;
        public const int WM_WINDOWPOSCHANGING = 0x0046;
        public const int WM_WINDOWPOSCHANGED = 0x0047;
        public const int WM_POWER = 0x0048;
        public const int WM_COPYDATA = 0x004A;
        public const int WM_CANCELJOURNAL = 0x004B;
        public const int WM_NOTIFY = 0x004E;
        public const int WM_INPUTLANGCHANGEREQUEST = 0x0050;
        public const int WM_INPUTLANGCHANGE = 0x0051;
        public const int WM_TCARD = 0x0052;
        public const int WM_HELP = 0x0053;
        public const int WM_USERCHANGED = 0x0054;
        public const int WM_NOTIFYFORMAT = 0x0055;
        public const int WM_CONTEXTMENU = 0x007B;
        public const int WM_STYLECHANGING = 0x007C;
        public const int WM_STYLECHANGED = 0x007D;
        public const int WM_DISPLAYCHANGE = 0x007E;
        public const int WM_GETICON = 0x007F;
        public const int WM_SETICON = 0x0080;
        public const int WM_NCCREATE = 0x0081;
        public const int WM_NCDESTROY = 0x0082;
        public const int WM_NCCALCSIZE = 0x0083;
      /// <summary>
      /// 移动鼠标，按住或释放鼠标时发生
      /// </summary>
        public const int WM_NCHITTEST = 0x0084;
       /// <summary>
       /// 程序发送此消息给某个窗口当它（窗口）的框架必须被绘制时；
       /// </summary>
        public const int WM_NCPAINT = 0x0085;
       /// <summary>
       /// 此消息发送给某个窗口 仅当它的非客户区需要被改变来显示是激活还是非激活状态；
       /// </summary>
        public const int WM_NCACTIVATE = 0x0086;
       /// <summary>
       /// 发送此消息给某个与对话框程序关联的控件，widdows控制方位键和TAB键使输入进入此控件 通过响应WM_GETDLGCODE消息，应用程序可以把他当成一个特殊的输入控件并能处理它
       /// </summary>

        public const int WM_GETDLGCODE = 0x0087;
        public const int WM_SYNCPAINT = 0x0088;
        /// <summary>
        /// 当光标在一个窗口的非客户区内移动时发送此消息给这个窗口 //非客户区为：窗体的标题栏及窗  的边框体
        /// </summary>

        public const int WM_NCMOUSEMOVE = 0x00A0;
       /// <summary>
       /// 当光标在一个窗口的非客户区同时按下鼠标左键时提交此消息
       /// </summary>
        public const int WM_NCLBUTTONDOWN = 0x00A1;
        public const int WM_NCLBUTTONUP = 0x00A2;
        public const int WM_NCLBUTTONDBLCLK = 0x00A3;
        public const int WM_NCRBUTTONDOWN = 0x00A4;
        public const int WM_NCRBUTTONUP = 0x00A5;
        public const int WM_NCRBUTTONDBLCLK = 0x00A6;
        public const int WM_NCMBUTTONDOWN = 0x00A7;
        public const int WM_NCMBUTTONUP = 0x00A8;
        public const int WM_NCMBUTTONDBLCLK = 0x00A9;
       /// <summary>
       /// 按下一个键
       /// </summary>
        public const int WM_KEYDOWN = 0x0100;
       /// <summary>
       /// 释放一个键
       /// </summary>
        public const int WM_KEYUP = 0x0101;
        /// <summary>
        /// 按下某键，并已发出WM_KEYDOWN， WM_KEYUP消息
        /// </summary>
        public const int WM_CHAR = 0x0102;
        /// <summary>
        /// 当用translatemessage函数翻译WM_KEYUP消息时发送此消息给拥有焦点的窗口
        /// </summary>
        public const int WM_DEADCHAR = 0x0103;
        /// <summary>
        /// 当用户按住ALT键同时按下其它键时提交此消息给拥有焦点的窗口；
        /// </summary>
        public const int WM_SYSKEYDOWN = 0x0104;
        public const int WM_SYSKEYUP = 0x0105;
        public const int WM_SYSCHAR = 0x0106;
        public const int WM_SYSDEADCHAR = 0x0107;
        public const int WM_KEYLAST = 0x0108;
        public const int WM_IME_STARTCOMPOSITION = 0x010D;
        public const int WM_IME_ENDCOMPOSITION = 0x010E;
        public const int WM_IME_COMPOSITION = 0x010F;
        public const int WM_IME_KEYLAST = 0x010F;
       /// <summary>
       /// 在一个对话框程序被显示前发送此消息给它，通常用此消息初始化控件和执行其它任务
       /// </summary>
        public const int WM_INITDIALOG = 0x0110;
       /// <summary>
       /// 当用户选择一条菜单命令项或当某个控件发送一条消息给它的父窗口，一个快捷键被翻译
       /// </summary>
        public const int WM_COMMAND = 0x0111;
       /// <summary>
       /// 当用户选择窗口菜单的一条命令或当用户选择最大化或最小化时那个窗口会收到此消息
       /// </summary>
        public const int WM_SYSCOMMAND = 0x0112;
      /// <summary>
      /// 发生了定时器事件
      /// </summary>
        public const int WM_TIMER = 0x0113;
        /// <summary>
        /// 当一个窗口标准水平滚动条产生一个滚动事件时发送此消息给那个窗口，也发送给拥有它的控件
        /// </summary>
        public const int WM_HSCROLL = 0x0114;
       /// <summary>
       /// 当一个窗口标准垂直滚动条产生一个滚动事件时发送此消息给那个窗口也，发送给拥有它的控件
       /// </summary>
        public const int WM_VSCROLL = 0x0115;
       /// <summary>
       /// 当一个菜单将要被激活时发送此消息，它发生在用户菜单条中的某项或按下某个菜单键，它允许程序在显示前更改菜单
       /// </summary>
        public const int WM_INITMENU = 0x0116;
       /// <summary>
       /// 当一个下拉菜单或子菜单将要被激活时发送此消息，它允许程序在它显示前更改菜单，而不要改变全部
       /// </summary>
        public const int WM_INITMENUPOPUP = 0x0117;
        public const int WM_MENUSELECT = 0x011F;
        public const int WM_MENUCHAR = 0x0120;
        public const int WM_ENTERIDLE = 0x0121;
        public const int WM_MENURBUTTONUP = 0x0122;
        public const int WM_MENUDRAG = 0x0123;
        public const int WM_MENUGETOBJECT = 0x0124;
        public const int WM_UNINITMENUPOPUP = 0x0125;
        public const int WM_MENUCOMMAND = 0x0126;
        /// <summary>
        /// 在windows绘制消息框前发送此消息给消息框的所有者窗口，通过响应这条消息，所有者窗口可以通过使用给定的相关显示设备的句柄来设置消息框的文本和背景颜色
        /// </summary>
        public const int WM_CTLCOLORMSGBOX = 0x0132;
       /// <summary>
       /// 当一个编辑型控件将要被绘制时发送此消息给它的父窗口；通过响应这条消息，所有者窗口可以通过使用给定的相关显示设备的句柄来设置编辑框的文本和背景颜色
       /// </summary>
        public const int WM_CTLCOLOREDIT = 0x0133;
       /// <summary>
       /// 当一个列表框控件将要被绘制前发送此消息给它的父窗口；通过响应这条消息，所有者窗口可以通过使用给定的相关显示设备的句柄来设置列表框的文本和背景颜色
       /// </summary>
        public const int WM_CTLCOLORLISTBOX = 0x0134;
       /// <summary>
       /// 当一个按钮控件将要被绘制时发送此消息给它的父窗口；通过响应这条消息，所有者窗口可以通过使用给定的相关显示设备的句柄来设置按纽的文本和背景颜色
       /// </summary>
        public const int WM_CTLCOLORBTN = 0x0135;
        /// <summary>
        /// 当一个对话框控件将要被绘制前发送此消息给它的父窗口；通过响应这条消息，所有者窗口可以通过使用给定的相关显示设备的句柄来设置对话框的文本背景颜色
        /// </summary>
        public const int WM_CTLCOLORDLG = 0x0136;
        /// <summary>
        /// 当一个滚动条控件将要被绘制时发送此消息给它的父窗口；通过响应这条消息，所有者窗口可以通过使用给定的相关显示设备的句柄来设置滚动条的背景颜色
        /// </summary>
        public const int WM_CTLCOLORSCROLLBAR = 0x0137;
       /// <summary>
       /// 当一个静态控件将要被绘制时发送此消息给它的父窗口；通过响应这条消息，所有者窗口可以通过使用给定的相关显示设备的句柄来设置静态控件的文本和背景颜色
       /// </summary>
        public const int WM_CTLCOLORSTATIC = 0x0138;
        /// <summary>
        /// 移动鼠标
        /// </summary>
        public const int WM_MOUSEMOVE = 0x0200;
        /// <summary>
        /// 按下鼠标左键
        /// </summary>
        public const int WM_LBUTTONDOWN = 0x0201;
        /// <summary>
        /// 释放鼠标左键
        /// </summary>
        public const int WM_LBUTTONUP = 0x0202;
        /// <summary>
        /// 双击鼠标左键
        /// </summary>
        public const int WM_LBUTTONDBLCLK = 0x0203;
        /// <summary>
        /// 按下鼠标右键
        /// </summary>
        public const int WM_RBUTTONDOWN = 0x0204;
        /// <summary>
        /// 释放鼠标右键
        /// </summary>
        public const int WM_RBUTTONUP = 0x0205;
        /// <summary>
        /// 双击鼠标右键
        /// </summary>
        public const int WM_RBUTTONDBLCLK = 0x0206;
        /// <summary>
        /// 按下鼠标中键 
        /// </summary>
        public const int WM_MBUTTONDOWN = 0x0207;
       
        public const int WM_MBUTTONUP = 0x0208;
        public const int WM_MBUTTONDBLCLK = 0x0209;
        /// <summary>
        /// 当鼠标轮子转动时发送此消息个当前有焦点的控件
        /// </summary>
        public const int WM_MOUSEWHEEL = 0x020A;
        public const int WM_PARENTNOTIFY = 0x0210;
        public const int WM_ENTERMENULOOP = 0x0211;
        public const int WM_EXITMENULOOP = 0x0212;
        public const int WM_NEXTMENU = 0x0213;
        public const int WM_SIZING = 0x0214;
        public const int WM_CAPTURECHANGED = 0x0215;
        public const int WM_MOVING = 0x0216;
        public const int WM_DEVICECHANGE = 0x0219;
        public const int WM_MDICREATE = 0x0220;
        public const int WM_MDIDESTROY = 0x0221;
        public const int WM_MDIACTIVATE = 0x0222;
        public const int WM_MDIRESTORE = 0x0223;
        public const int WM_MDINEXT = 0x0224;
        public const int WM_MDIMAXIMIZE = 0x0225;
        public const int WM_MDITILE = 0x0226;
        public const int WM_MDICASCADE = 0x0227;
        public const int WM_MDIICONARRANGE = 0x0228;
        public const int WM_MDIGETACTIVE = 0x0229;
        public const int WM_MDISETMENU = 0x0230;
        public const int WM_ENTERSIZEMOVE = 0x0231;
        public const int WM_EXITSIZEMOVE = 0x0232;
        public const int WM_DROPFILES = 0x0233;
        public const int WM_MDIREFRESHMENU = 0x0234;
        public const int WM_IME_SETCONTEXT = 0x0281;
        public const int WM_IME_NOTIFY = 0x0282;
        public const int WM_IME_CONTROL = 0x0283;
        public const int WM_IME_COMPOSITIONFULL = 0x0284;
        public const int WM_IME_SELECT = 0x0285;
        public const int WM_IME_CHAR = 0x0286;
        public const int WM_IME_REQUEST = 0x0288;
        public const int WM_IME_KEYDOWN = 0x0290;
        public const int WM_IME_KEYUP = 0x0291;
        public const int WM_MOUSEHOVER = 0x02A1;
        public const int WM_MOUSELEAVE = 0x02A3;
        public const int WM_CUT = 0x0300;
        public const int WM_COPY = 0x0301;
        public const int WM_PASTE = 0x0302;
        public const int WM_CLEAR = 0x0303;
        public const int WM_UNDO = 0x0304;
        public const int WM_RENDERFORMAT = 0x0305;
        public const int WM_RENDERALLFORMATS = 0x0306;
        public const int WM_DESTROYCLIPBOARD = 0x0307;
        public const int WM_DRAWCLIPBOARD = 0x0308;
        public const int WM_PAINTCLIPBOARD = 0x0309;
        public const int WM_VSCROLLCLIPBOARD = 0x030A;
        public const int WM_SIZECLIPBOARD = 0x030B;
        public const int WM_ASKCBFORMATNAME = 0x030C;
        public const int WM_CHANGECBCHAIN = 0x030D;
        public const int WM_HSCROLLCLIPBOARD = 0x030E;
        public const int WM_QUERYNEWPALETTE = 0x030F;
        public const int WM_PALETTEISCHANGING = 0x0310;
        public const int WM_PALETTECHANGED = 0x0311;
        public const int WM_HOTKEY = 0x0312;
        public const int WM_PRINT = 0x0317;
        public const int WM_PRINTCLIENT = 0x0318;
        public const int WM_HANDHELDFIRST = 0x0358;
        public const int WM_HANDHELDLAST = 0x035F;
        public const int WM_AFXFIRST = 0x0360;
        public const int WM_AFXLAST = 0x037F;
        public const int WM_PENWINFIRST = 0x0380;
        public const int WM_PENWINLAST = 0x038F;

        public const int WM_APP = 0x8000;
        public const int WM_USER = 0x0400;

        //////////////////////
        ///


    }

    /// <summary>
    /// Wind32API
    /// </summary>
    public static class Win32
    {
        #region 消息
        public const int MF_REMOVE = 0x1000;

        public const int SC_RESTORE = 0xF120; //还原
        public const int SC_MOVE = 0xF010; //移动
        public const int SC_SIZE = 0xF000; //大小
        public const int SC_MINIMIZE = 0xF020; //最小化
        public const int SC_MAXIMIZE = 0xF030; //最大化
        public const int SC_CLOSE = 0xF060; //关闭 

        public const int WM_SYSCOMMAND = 0x0112;
        public const int WM_COMMAND = 0x0111;

        public const int GW_HWNDFIRST = 0;
        public const int GW_HWNDLAST = 1;
        public const int GW_HWNDNEXT = 2;
        public const int GW_HWNDPREV = 3;
        public const int GW_OWNER = 4;
        public const int GW_CHILD = 5;

        public const int WM_NCCALCSIZE = 0x83;
        public const int WM_WINDOWPOSCHANGING = 0x46;
        public const int WM_PAINT = 0xF;
        public const int WM_CREATE = 0x1;
        public const int WM_NCCREATE = 0x81;
        public const int WM_NCPAINT = 0x85;
        public const int WM_PRINT = 0x317;
        public const int WM_DESTROY = 0x2;
        public const int WM_SHOWWINDOW = 0x18;
        public const int WM_SHARED_MENU = 0x1E2;
        public const int HC_ACTION = 0;
        public const int WH_CALLWNDPROC = 4;
        public const int GWL_WNDPROC = -4;

        public const int WS_SYSMENU = 0x80000;
        public const int WS_SIZEBOX = 0x40000;

        public const int WS_MAXIMIZEBOX = 0x10000;

        public const int WS_MINIMIZEBOX = 0x20000;
        #endregion
        [StructLayout(LayoutKind.Sequential)]
        public struct Size
        {
            public Int32 cx;
            public Int32 cy;

            public Size(Int32 x, Int32 y)
            {
                cx = x;
                cy = y;
            }
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct BLENDFUNCTION
        {
            public byte BlendOp;
            public byte BlendFlags;
            public byte SourceConstantAlpha;
            public byte AlphaFormat;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Point
        {
            public Int32 x;
            public Int32 y;

            public Point(Int32 x, Int32 y)
            {
                this.x = x;
                this.y = y;
            }
        }

        public const byte AC_SRC_OVER = 0;
        public const Int32 ULW_ALPHA = 2;
        public const byte AC_SRC_ALPHA = 1;

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("gdi32.dll", ExactSpelling = true)]
        public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObj);

        [DllImport("user32.dll", ExactSpelling = true)]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern int DeleteDC(IntPtr hDC);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern int DeleteObject(IntPtr hObj);

        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern int UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref Point pptDst, ref Size psize, IntPtr hdcSrc, ref Point pptSrc, Int32 crKey, ref BLENDFUNCTION pblend, Int32 dwFlags);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr ExtCreateRegion(IntPtr lpXform, uint nCount, IntPtr rgnData);

        [DllImport("user32")]
        public static extern int SendMessage(IntPtr hwnd, int msg, int wp, int lp);
    }
    #region win32消息功能参考
    /**
     * 
     * 消息，就是指Windows发出的一个通知，告诉应用程序某个事情发生了。例如，单击鼠标、改变窗口尺寸、按下键盘上的一个键都会使Windows发送一个消息给应用程序。消息本身是作为一个记录传递给应用程序的，这个记录中包含了消息的类型以及其他信息。例如，对于单击鼠标所产生的消息来说，这个记录中包含了单击鼠标时的坐标。这个记录类型叫做TMsg，


它在Windows单元中是这样声明的：
type
TMsg = packed record
hwnd: HWND; / /窗口句柄
message: UINT; / /消息常量标识符
wParam: WPARAM ; // 32位消息的特定附加信息
lParam: LPARAM ; // 32位消息的特定附加信息
time: DWORD; / /消息创建时的时间
pt: TPoint; / /消息创建时的鼠标位置
end;

消息中有什么？
是否觉得一个消息记录中的信息像希腊语一样？如果是这样，那么看一看下面的解释：
hwnd 32位的窗口句柄。窗口可以是任何类型的屏幕对象，因为Win32能够维护大多数可视对象的句柄(窗口、对话框、按钮、编辑框等)。
message 用于区别其他消息的常量值，这些常量可以是Windows单元中预定义的常量，也可以是自定义的常量。
wParam 通常是一个与消息有关的常量值，也可能是窗口或控件的句柄。
lParam 通常是一个指向内存中数据的指针。由于W P a r a m、l P a r a m和P o i n t e r都是3 2位的，
因此，它们之间可以相互转换。

WM_NULL = $0000;
WM_CREATE = $0001;
应用程序创建一个窗口
WM_DESTROY = $0002;
一个窗口被销毁
WM_MOVE = $0003;
移动一个窗口
WM_SIZE = $0005;
改变一个窗口的大小
WM_ACTIVATE = $0006;
一个窗口被激活或失去激活状态；
WM_SETFOCUS = $0007;
获得焦点后
WM_KILLFOCUS = $0008;
失去焦点
WM_ENABLE = $000A;
改变enable状态
WM_SETREDRAW = $000B;
设置窗口是否能重画 
WM_SETTEXT = $000C;
应用程序发送此消息来设置一个窗口的文本
WM_GETTEXT = $000D;
应用程序发送此消息来复制对应窗口的文本到缓冲区
WM_GETTEXTLENGTH = $000E;
得到与一个窗口有关的文本的长度（不包含空字符）
WM_PAINT = $000F;
要求一个窗口重画自己
WM_CLOSE = $0010;
当一个窗口或应用程序要关闭时发送一个信号
WM_QUERYENDSESSION = $0011;
当用户选择结束对话框或程序自己调用ExitWindows函数
WM_QUIT = $0012;
用来结束程序运行或当程序调用postquitmessage函数 
WM_QUERYOPEN = $0013;
当用户窗口恢复以前的大小位置时，把此消息发送给某个图标
WM_ERASEBKGND = $0014;
当窗口背景必须被擦除时（例在窗口改变大小时）
WM_SYSCOLORCHANGE = $0015;
当系统颜色改变时，发送此消息给所有顶级窗口
WM_ENDSESSION = $0016;
当系统进程发出WM_QUERYENDSESSION消息后，此消息发送给应用程序，
通知它对话是否结束
WM_SYSTEMERROR = $0017;
WM_SHOWWINDOW = $0018;
当隐藏或显示窗口是发送此消息给这个窗口
WM_ACTIVATEAPP = $001C;
发此消息给应用程序哪个窗口是激活的，哪个是非激活的；
WM_FONTCHANGE = $001D;
当系统的字体资源库变化时发送此消息给所有顶级窗口
WM_TIMECHANGE = $001E;
当系统的时间变化时发送此消息给所有顶级窗口
WM_CANCELMODE = $001F;
发送此消息来取消某种正在进行的摸态（操作）
WM_SETCURSOR = $0020;
如果鼠标引起光标在某个窗口中移动且鼠标输入没有被捕获时，就发消息给某个窗口
WM_MOUSEACTIVATE = $0021;
当光标在某个非激活的窗口中而用户正按着鼠标的某个键发送此消息给当前窗口
WM_CHILDACTIVATE = $0022;
发送此消息给MDI子窗口当用户点击此窗口的标题栏，或当窗口被激活，移动，改变大小
WM_QUEUESYNC = $0023;
此消息由基于计算机的训练程序发送，通过WH_JOURNALPALYBACK的hook程序
分离出用户输入消息
WM_GETMINMAXINFO = $0024;
此消息发送给窗口当它将要改变大小或位置；
WM_PAINTICON = $0026;
发送给最小化窗口当它图标将要被重画
WM_ICONERASEBKGND = $0027;
此消息发送给某个最小化窗口，仅当它在画图标前它的背景必须被重画
WM_NEXTDLGCTL = $0028;
发送此消息给一个对话框程序去更改焦点位置
WM_SPOOLERSTATUS = $002A;
每当打印管理列队增加或减少一条作业时发出此消息 
WM_DRAWITEM = $002B;
当button，combobox，listbox，menu的可视外观改变时发送
此消息给这些空件的所有者
WM_MEASUREITEM = $002C;
当button, combo box, list box, list view control, or menu item 被创建时
发送此消息给控件的所有者
WM_DELETEITEM = $002D;
当the list box 或 combo box 被销毁 或 当 某些项被删除通过LB_DELETESTRING, LB_RESETCONTENT, CB_DELETESTRING, or CB_RESETCONTENT 消息
WM_VKEYTOITEM = $002E;
此消息有一个LBS_WANTKEYBOARDINPUT风格的发出给它的所有者来响应WM_KEYDOWN消息 
WM_CHARTOITEM = $002F;
此消息由一个LBS_WANTKEYBOARDINPUT风格的列表框发送给他的所有者来响应WM_CHAR消息 
WM_SETFONT = $0030;
当绘制文本时程序发送此消息得到控件要用的颜色 
WM_GETFONT = $0031;
应用程序发送此消息得到当前控件绘制文本的字体
WM_SETHOTKEY = $0032;
应用程序发送此消息让一个窗口与一个热键相关连
WM_GETHOTKEY = $0033;
应用程序发送此消息来判断热键与某个窗口是否有关联
WM_QUERYDRAGICON = $0037;
此消息发送给最小化窗口，当此窗口将要被拖放而它的类中没有定义图标，应用程序能返回一个图标或光标的句柄，当用户拖放图标时系统显示这个图标或光标
WM_COMPAREITEM = $0039;
发送此消息来判定combobox或listbox新增加的项的相对位置
WM_GETOBJECT = $003D;
WM_COMPACTING = $0041;
显示内存已经很少了
WM_WINDOWPOSCHANGING = $0046;
发送此消息给那个窗口的大小和位置将要被改变时，来调用setwindowpos函数或其它窗口管理函数
WM_WINDOWPOSCHANGED = $0047;
发送此消息给那个窗口的大小和位置已经被改变时，来调用setwindowpos函数或其它窗口管理函数
WM_POWER = $0048;（适用于16位的windows）
当系统将要进入暂停状态时发送此消息
WM_COPYDATA = $004A;
当一个应用程序传递数据给另一个应用程序时发送此消息
WM_CANCELJOURNAL = $004B;
当某个用户取消程序日志激活状态，提交此消息给程序
WM_NOTIFY = $004E;
当某个控件的某个事件已经发生或这个控件需要得到一些信息时，发送此消息给它的父窗口
WM_INPUTLANGCHANGEREQUEST = $0050;
当用户选择某种输入语言，或输入语言的热键改变
WM_INPUTLANGCHANGE = $0051;
当平台现场已经被改变后发送此消息给受影响的最顶级窗口
WM_TCARD = $0052;
当程序已经初始化windows帮助例程时发送此消息给应用程序
WM_HELP = $0053;
此消息显示用户按下了F1，如果某个菜单是激活的，就发送此消息个此窗口关联的菜单，否则就
发送给有焦点的窗口，如果当前都没有焦点，就把此消息发送给当前激活的窗口
WM_USERCHANGED = $0054;
当用户已经登入或退出后发送此消息给所有的窗口，当用户登入或退出时系统更新用户的具体
设置信息，在用户更新设置时系统马上发送此消息；
WM_NOTIFYFORMAT = $0055;
公用控件，自定义控件和他们的父窗口通过此消息来判断控件是使用ANSI还是UNICODE结构
在WM_NOTIFY消息，使用此控件能使某个控件与它的父控件之间进行相互通信
WM_CONTEXTMENU = $007B;
当用户某个窗口中点击了一下右键就发送此消息给这个窗口
WM_STYLECHANGING = $007C;
当调用SETWINDOWLONG函数将要改变一个或多个 窗口的风格时发送此消息给那个窗口
WM_STYLECHANGED = $007D;
当调用SETWINDOWLONG函数一个或多个 窗口的风格后发送此消息给那个窗口WM_DISPLAYCHANGE = $007E;
当显示器的分辨率改变后发送此消息给所有的窗口
WM_GETICON = $007F;
此消息发送给某个窗口来返回与某个窗口有关连的大图标或小图标的句柄；
WM_SETICON = $0080;
程序发送此消息让一个新的大图标或小图标与某个窗口关联；
WM_NCCREATE = $0081;
当某个窗口第一次被创建时，此消息在WM_CREATE消息发送前发送；
WM_NCDESTROY = $0082;
此消息通知某个窗口，非客户区正在销毁
WM_NCCALCSIZE = $0083;
当某个窗口的客户区域必须被核算时发送此消息
WM_NCHITTEST = $0084;//移动鼠标，按住或释放鼠标时发生
WM_NCPAINT = $0085;
程序发送此消息给某个窗口当它（窗口）的框架必须被绘制时；
WM_NCACTIVATE = $0086;
此消息发送给某个窗口 仅当它的非客户区需要被改变来显示是激活还是非激活状态；
WM_GETDLGCODE = $0087;
发送此消息给某个与对话框程序关联的控件，widdows控制方位键和TAB键使输入进入此控件
通过响应WM_GETDLGCODE消息，应用程序可以把他当成一个特殊的输入控件并能处理它
WM_NCMOUSEMOVE = $00A0;
当光标在一个窗口的非客户区内移动时发送此消息给这个窗口 //非客户区为：窗体的标题栏及窗 
的边框体
WM_NCLBUTTONDOWN = $00A1;
当光标在一个窗口的非客户区同时按下鼠标左键时提交此消息
WM_NCLBUTTONUP = $00A2;
当用户释放鼠标左键同时光标某个窗口在非客户区十发送此消息；
WM_NCLBUTTONDBLCLK = $00A3;
当用户双击鼠标左键同时光标某个窗口在非客户区十发送此消息
WM_NCRBUTTONDOWN = $00A4;
当用户按下鼠标右键同时光标又在窗口的非客户区时发送此消息
WM_NCRBUTTONUP = $00A5;
当用户释放鼠标右键同时光标又在窗口的非客户区时发送此消息
WM_NCRBUTTONDBLCLK = $00A6;
当用户双击鼠标右键同时光标某个窗口在非客户区十发送此消息
WM_NCMBUTTONDOWN = $00A7;
当用户按下鼠标中键同时光标又在窗口的非客户区时发送此消息
WM_NCMBUTTONUP = $00A8;
当用户释放鼠标中键同时光标又在窗口的非客户区时发送此消息
WM_NCMBUTTONDBLCLK = $00A9;
当用户双击鼠标中键同时光标又在窗口的非客户区时发送此消息
WM_KEYFIRST = $0100;
WM_KEYDOWN = $0100; 
//按下一个键
WM_KEYUP = $0101; 
//释放一个键
WM_CHAR = $0102; 
//按下某键，并已发出WM_KEYDOWN， WM_KEYUP消息
WM_DEADCHAR = $0103;
当用translatemessage函数翻译WM_KEYUP消息时发送此消息给拥有焦点的窗口
WM_SYSKEYDOWN = $0104;
当用户按住ALT键同时按下其它键时提交此消息给拥有焦点的窗口；
WM_SYSKEYUP = $0105;
当用户释放一个键同时ALT 键还按着时提交此消息给拥有焦点的窗口
WM_SYSCHAR = $0106;
当WM_SYSKEYDOWN消息被TRANSLATEMESSAGE函数翻译后提交此消息给拥有焦点的窗口
WM_SYSDEADCHAR = $0107;
当WM_SYSKEYDOWN消息被TRANSLATEMESSAGE函数翻译后发送此消息给拥有焦点的窗口
WM_KEYLAST = $0108;
WM_INITDIALOG = $0110;
在一个对话框程序被显示前发送此消息给它，通常用此消息初始化控件和执行其它任务
WM_COMMAND = $0111;
当用户选择一条菜单命令项或当某个控件发送一条消息给它的父窗口，一个快捷键被翻译
WM_SYSCOMMAND = $0112;
当用户选择窗口菜单的一条命令或当用户选择最大化或最小化时那个窗口会收到此消息
WM_TIMER = $0113; //发生了定时器事件
WM_HSCROLL = $0114;
当一个窗口标准水平滚动条产生一个滚动事件时发送此消息给那个窗口，也发送给拥有它的控件
WM_VSCROLL = $0115;
当一个窗口标准垂直滚动条产生一个滚动事件时发送此消息给那个窗口也，发送给拥有它的控件 WM_INITMENU = $0116;
当一个菜单将要被激活时发送此消息，它发生在用户菜单条中的某项或按下某个菜单键，它允许程序在显示前更改菜单
WM_INITMENUPOPUP = $0117;
当一个下拉菜单或子菜单将要被激活时发送此消息，它允许程序在它显示前更改菜单，而不要改变全部 
WM_MENUSELECT = $011F;
当用户选择一条菜单项时发送此消息给菜单的所有者（一般是窗口）
WM_MENUCHAR = $0120;
当菜单已被激活用户按下了某个键（不同于加速键），发送此消息给菜单的所有者；
WM_ENTERIDLE = $0121;
当一个模态对话框或菜单进入空载状态时发送此消息给它的所有者，一个模态对话框或菜单进入空载状态就是在处理完一条或几条先前的消息后没有消息它的列队中等待
WM_MENURBUTTONUP = $0122;
WM_MENUDRAG = $0123;
WM_MENUGETOBJECT = $0124;
WM_UNINITMENUPOPUP = $0125;
WM_MENUCOMMAND = $0126;
WM_CHANGEUISTATE = $0127;
WM_UPDATEUISTATE = $0128;
WM_QUERYUISTATE = $0129; 
WM_CTLCOLORMSGBOX = $0132;
在windows绘制消息框前发送此消息给消息框的所有者窗口，通过响应这条消息，所有者窗口可以通过使用给定的相关显示设备的句柄来设置消息框的文本和背景颜色
WM_CTLCOLOREDIT = $0133;
当一个编辑型控件将要被绘制时发送此消息给它的父窗口；通过响应这条消息，所有者窗口可以通过使用给定的相关显示设备的句柄来设置编辑框的文本和背景颜色
WM_CTLCOLORLISTBOX = $0134;
当一个列表框控件将要被绘制前发送此消息给它的父窗口；通过响应这条消息，所有者窗口可以通过使用给定的相关显示设备的句柄来设置列表框的文本和背景颜色 
WM_CTLCOLORBTN = $0135;
当一个按钮控件将要被绘制时发送此消息给它的父窗口；通过响应这条消息，所有者窗口可以通过使用给定的相关显示设备的句柄来设置按纽的文本和背景颜色
WM_CTLCOLORDLG = $0136;
当一个对话框控件将要被绘制前发送此消息给它的父窗口；通过响应这条消息，所有者窗口可以通过使用给定的相关显示设备的句柄来设置对话框的文本背景颜色
WM_CTLCOLORSCROLLBAR= $0137;
当一个滚动条控件将要被绘制时发送此消息给它的父窗口；通过响应这条消息，所有者窗口可以通过使用给定的相关显示设备的句柄来设置滚动条的背景颜色
WM_CTLCOLORSTATIC = $0138; 
当一个静态控件将要被绘制时发送此消息给它的父窗口；通过响应这条消息，所有者窗口可以通过使用给定的相关显示设备的句柄来设置静态控件的文本和背景颜色
WM_MOUSEFIRST = $0200;
WM_MOUSEMOVE = $0200; 
// 移动鼠标
WM_LBUTTONDOWN = $0201; 
//按下鼠标左键
WM_LBUTTONUP = $0202; 
//释放鼠标左键
WM_LBUTTONDBLCLK = $0203;
//双击鼠标左键
WM_RBUTTONDOWN = $0204;
//按下鼠标右键
WM_RBUTTONUP = $0205;
//释放鼠标右键
WM_RBUTTONDBLCLK = $0206; 
//双击鼠标右键
WM_MBUTTONDOWN = $0207; 
//按下鼠标中键 
WM_MBUTTONUP = $0208; 
//释放鼠标中键
WM_MBUTTONDBLCLK = $0209; 
//双击鼠标中键
WM_MOUSEWHEEL = $020A;
当鼠标轮子转动时发送此消息个当前有焦点的控件
WM_MOUSELAST = $020A;
WM_PARENTNOTIFY = $0210;
当MDI子窗口被创建或被销毁，或用户按了一下鼠标键而光标在子窗口上时发送此消息给它的父窗口
WM_ENTERMENULOOP = $0211;
发送此消息通知应用程序的主窗口that已经进入了菜单循环模式
WM_EXITMENULOOP = $0212;
发送此消息通知应用程序的主窗口that已退出了菜单循环模式
WM_NEXTMENU = $0213;
WM_SIZING = 532;
当用户正在调整窗口大小时发送此消息给窗口；通过此消息应用程序可以监视窗口大小和位置也可以修改他们
WM_CAPTURECHANGED = 533;
发送此消息 给窗口当它失去捕获的鼠标时；
WM_MOVING = 534;
当用户在移动窗口时发送此消息，通过此消息应用程序可以监视窗口大小和位置也可以修改他们；
WM_POWERBROADCAST = 536;
此消息发送给应用程序来通知它有关电源管理事件；
WM_DEVICECHANGE = 537;
当设备的硬件配置改变时发送此消息给应用程序或设备驱动程序
WM_IME_STARTCOMPOSITION = $010D;
WM_IME_ENDCOMPOSITION = $010E;
WM_IME_COMPOSITION = $010F;
WM_IME_KEYLAST = $010F;
WM_IME_SETCONTEXT = $0281;
WM_IME_NOTIFY = $0282;
WM_IME_CONTROL = $0283;
WM_IME_COMPOSITIONFULL = $0284;
WM_IME_SELECT = $0285;
WM_IME_CHAR = $0286;
WM_IME_REQUEST = $0288;
WM_IME_KEYDOWN = $0290;
WM_IME_KEYUP = $0291;
WM_MDICREATE = $0220;
应用程序发送此消息给多文档的客户窗口来创建一个MDI 子窗口
WM_MDIDESTROY = $0221;
应用程序发送此消息给多文档的客户窗口来关闭一个MDI 子窗口
WM_MDIACTIVATE = $0222;
应用程序发送此消息给多文档的客户窗口通知客户窗口激活另一个MDI子窗口，当客户窗口收到此消息后，它发出WM_MDIACTIVE消息给MDI子窗口（未激活）激活它；
WM_MDIRESTORE = $0223;
程序 发送此消息给MDI客户窗口让子窗口从最大最小化恢复到原来大小
WM_MDINEXT = $0224;
程序 发送此消息给MDI客户窗口激活下一个或前一个窗口
WM_MDIMAXIMIZE = $0225;
程序发送此消息给MDI客户窗口来最大化一个MDI子窗口；
WM_MDITILE = $0226;
程序 发送此消息给MDI客户窗口以平铺方式重新排列所有MDI子窗口
WM_MDICASCADE = $0227;
程序 发送此消息给MDI客户窗口以层叠方式重新排列所有MDI子窗口
WM_MDIICONARRANGE = $0228;
程序 发送此消息给MDI客户窗口重新排列所有最小化的MDI子窗口
WM_MDIGETACTIVE = $0229;
程序 发送此消息给MDI客户窗口来找到激活的子窗口的句柄
WM_MDISETMENU = $0230;
程序 发送此消息给MDI客户窗口用MDI菜单代替子窗口的菜单
WM_ENTERSIZEMOVE = $0231;
WM_EXITSIZEMOVE = $0232;
WM_DROPFILES = $0233;
WM_MDIREFRESHMENU = $0234;
WM_MOUSEHOVER = $02A1;
WM_MOUSELEAVE = $02A3;
WM_CUT = $0300;
程序发送此消息给一个编辑框或combobox来删除当前选择的文本
WM_COPY = $0301;
程序发送此消息给一个编辑框或combobox来复制当前选择的文本到剪贴板
WM_PASTE = $0302;
程序发送此消息给editcontrol或combobox从剪贴板中得到数据
WM_CLEAR = $0303;
程序发送此消息给editcontrol或combobox清除当前选择的内容；
WM_UNDO = $0304;
程序发送此消息给editcontrol或combobox撤消最后一次操作
WM_RENDERFORMAT = $0305；

WM_RENDERALLFORMATS = $0306;
WM_DESTROYCLIPBOARD = $0307;
当调用ENPTYCLIPBOARD函数时 发送此消息给剪贴板的所有者
WM_DRAWCLIPBOARD = $0308;
当剪贴板的内容变化时发送此消息给剪贴板观察链的第一个窗口；它允许用剪贴板观察窗口来
显示剪贴板的新内容；
WM_PAINTCLIPBOARD = $0309;
当剪贴板包含CF_OWNERDIPLAY格式的数据并且剪贴板观察窗口的客户区需要重画；
WM_VSCROLLCLIPBOARD = $030A;
WM_SIZECLIPBOARD = $030B;
当剪贴板包含CF_OWNERDIPLAY格式的数据并且剪贴板观察窗口的客户区域的大小已经改变是此消息通过剪贴板观察窗口发送给剪贴板的所有者；
WM_ASKCBFORMATNAME = $030C;
通过剪贴板观察窗口发送此消息给剪贴板的所有者来请求一个CF_OWNERDISPLAY格式的剪贴板的名字
WM_CHANGECBCHAIN = $030D;
当一个窗口从剪贴板观察链中移去时发送此消息给剪贴板观察链的第一个窗口；
WM_HSCROLLCLIPBOARD = $030E; 
此消息通过一个剪贴板观察窗口发送给剪贴板的所有者 ；它发生在当剪贴板包含CFOWNERDISPALY格式的数据并且有个事件在剪贴板观察窗的水平滚动条上；所有者应滚动剪贴板图象并更新滚动条的值；
WM_QUERYNEWPALETTE = $030F;
此消息发送给将要收到焦点的窗口，此消息能使窗口在收到焦点时同时有机会实现他的逻辑调色板
WM_PALETTEISCHANGING= $0310;
当一个应用程序正要实现它的逻辑调色板时发此消息通知所有的应用程序
WM_PALETTECHANGED = $0311;
此消息在一个拥有焦点的窗口实现它的逻辑调色板后发送此消息给所有顶级并重叠的窗口，以此来改变系统调色板 
WM_HOTKEY = $0312;
当用户按下由REGISTERHOTKEY函数注册的热键时提交此消息
WM_PRINT = 791;
应用程序发送此消息仅当WINDOWS或其它应用程序发出一个请求要求绘制一个应用程序的一部分；
WM_PRINTCLIENT = 792;
WM_HANDHELDFIRST = 856;
WM_HANDHELDLAST = 863;
WM_PENWINFIRST = $0380;
WM_PENWINLAST = $038F;
WM_COALESCE_FIRST = $0390;
WM_COALESCE_LAST = $039F;
WM_DDE_FIRST = $03E0;
WM_DDE_INITIATE = WM_DDE_FIRST + 0;
一个DDE客户程序提交此消息开始一个与服务器程序的会话来响应那个指定的程序和主题名；
WM_DDE_TERMINATE = WM_DDE_FIRST + 1;
一个DDE应用程序（无论是客户还是服务器）提交此消息来终止一个会话；
WM_DDE_ADVISE = WM_DDE_FIRST + 2;
一个DDE客户程序提交此消息给一个DDE服务程序来请求服务器每当数据项改变时更新它
WM_DDE_UNADVISE = WM_DDE_FIRST + 3;
一个DDE客户程序通过此消息通知一个DDE服务程序不更新指定的项或一个特殊的剪贴板格式的项
WM_DDE_ACK = WM_DDE_FIRST + 4;
此消息通知一个DDE（动态数据交换）程序已收到并正在处理WM_DDE_POKE, WM_DDE_EXECUTE, WM_DDE_DATA, WM_DDE_ADVISE, WM_DDE_UNADVISE, or WM_DDE_INITIAT消息
WM_DDE_DATA = WM_DDE_FIRST + 5;
一个DDE服务程序提交此消息给DDE客户程序来传递个一数据项给客户或通知客户的一条可用数据项
WM_DDE_REQUEST = WM_DDE_FIRST + 6;
一个DDE客户程序提交此消息给一个DDE服务程序来请求一个数据项的值；
WM_DDE_POKE = WM_DDE_FIRST + 7;
一个DDE客户程序提交此消息给一个DDE服务程序，客户使用此消息来请求服务器接收一个未经同意的数据项；服务器通过答复WM_DDE_ACK消息提示是否它接收这个数据项；
WM_DDE_EXECUTE = WM_DDE_FIRST + 8;
一个DDE客户程序提交此消息给一个DDE服务程序来发送一个字符串给服务器让它象串行命令一样被处理，服务器通过提交WM_DDE_ACK消息来作回应；
WM_DDE_LAST = WM_DDE_FIRST + 8;
WM_APP = $8000;
WM_USER = $0400;
此消息能帮助应用程序自定义私有消息；
/
通知消息(Notification message)是指这样一种消息，一个窗口内的子控件发生了一些事情，需要通知父窗口。通知消息只适用于标准的窗口控件如按钮、列表框、组合框、编辑框，以及Windows 95公共控件如树状视图、列表视图等。例如，单击或双击一个控件、在控件中选择部分文本、操作控件的滚动条都会产生通知消息。 
按扭
B N _ C L I C K E D //用户单击了按钮
B N _ D I S A B L E //按钮被禁止
B N _ D O U B L E C L I C K E D //用户双击了按钮
B N _ H I L I T E //用户加亮了按钮
B N _ PA I N T按钮应当重画
B N _ U N H I L I T E加亮应当去掉
组合框
C B N _ C L O S E U P组合框的列表框被关闭
C B N _ D B L C L K用户双击了一个字符串
C B N _ D R O P D O W N组合框的列表框被拉出
C B N _ E D I T C H A N G E用户修改了编辑框中的文本
C B N _ E D I T U P D AT E编辑框内的文本即将更新
C B N _ E R R S PA C E组合框内存不足
C B N _ K I L L F O C U S组合框失去输入焦点
C B N _ S E L C H A N G E在组合框中选择了一项
C B N _ S E L E N D C A N C E L用户的选择应当被取消
C B N _ S E L E N D O K用户的选择是合法的
C B N _ S E T F O C U S组合框获得输入焦点
编辑框
E N _ C H A N G E编辑框中的文本己更新
E N _ E R R S PA C E编辑框内存不足
E N _ H S C R O L L用户点击了水平滚动条
E N _ K I L L F O C U S编辑框正在失去输入焦点
E N _ M A X T E X T插入的内容被截断
E N _ S E T F O C U S编辑框获得输入焦点
E N _ U P D AT E编辑框中的文本将要更新
E N _ V S C R O L L用户点击了垂直滚动条消息含义
列表框
L B N _ D B L C L K用户双击了一项
L B N _ E R R S PA C E列表框内存不够
L B N _ K I L L F O C U S列表框正在失去输入焦点
L B N _ S E L C A N C E L选择被取消
L B N _ S E L C H A N G E选择了另一项
L B N _ S E T F O C U S列表框获得输入焦点  

 

 

 

wParam 是功能建组合有Shift,Alt,Ctrl键和鼠标的三个键，每个一位，
lParam 是 鼠标的位置信息。
     * 
     * 
     * 
     */
    #endregion
    ///////////////////////////




}
