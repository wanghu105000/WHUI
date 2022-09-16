using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
namespace WHControlLib.Forms
{
    public partial class WHMsgBoxForm :Form
    {
        public WHMsgBoxForm()
        {
            InitializeComponent();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Rectangle MyRect = this.ClientRectangle;
            RectangleF TopRect = new Rectangle();
            RectangleF BomRect = new Rectangle();
         float topRctheight = MyRect.Height / 1.5f;
         float bouttomRctHeight = MyRect.Height / 3*2;
            TopRect.Height = topRctheight;
            TopRect.X = MyRect.Width/2-topRctheight/2;
            TopRect.Y = 0;
            TopRect.Width = TopRect.Height;



            GraphicsPath Toppath = new GraphicsPath();
            Toppath.AddEllipse(TopRect);

            BomRect.X = 0;
            BomRect.Y = MyRect.Height-bouttomRctHeight;
            BomRect.Width = MyRect.Width;
            BomRect.Height = bouttomRctHeight;
            GraphicsPath Bompath = new GraphicsPath();
            Bompath.AddRectangle(BomRect);
            Region reg= new Region(Bompath);
            //reg.Xor(Toppath);
            reg.Union(Toppath);
            //reg.Union(TopRect);
            //reg.Union(BomRect);

            //e.Graphics.FillRegion(new SolidBrush(Color.Yellow), reg);
            this.Region = reg;

            //path.AddEllipse(this.ClientRectangle);
            //this.Region = new Region();
        }

    }
}
