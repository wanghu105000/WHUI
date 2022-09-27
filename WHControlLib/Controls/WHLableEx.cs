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
    [ToolboxItem(true)]
    [DefaultEvent("Click")]
    [DefaultProperty("Text")]
    public partial class WHLableEx :baseStaticCtrl
    {
      
        
        public WHLableEx()
        {
            InitializeComponent();
        }
       
        
        #region 属性 字段 定义

      private TextAlign _myTextAlign = TextAlign.TopLeft;
        [Category("A我的"), Description("文字在控件上显示的对齐方式，默认，左上"), Browsable(true)]
        public override TextAlign MyTextAlign
        {
            get { return _myTextAlign; }
            set { _myTextAlign = value; Invalidate(); }
     

 
       

        #endregion
  
        }

        private bool _isAutoSize;
        [Category("A我的"), Description("文字在控件上是否自动大小（只有纯文本状态下支持，有图标不支持），默认，否"), Browsable(true)]
        public bool IsAutoSize
        {
            get { return _isAutoSize; }
            set { _isAutoSize = value; }
        }



        public override void DrawText(Graphics Myg)
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
 
          
          
            if (IsAutoSize)
              {
                 if (IsShowMyImage == false || MyImage == null/*|| MyImageOnMouse==null  || MyImageUnEnable==null*/)
                 {    int textwidth = (int)Myg.MeasureString(Text, MyFont).Width;
                     int textheight = (int)Myg.MeasureString(Text, MyFont).Height;
                
                    Width = textwidth + 10;
                    this.Height = textheight + 10;
                
                     TextRect.X = DrawRect.X+  2;
                       TextRect.Y =DrawRect.Y+ 2;
                      TextRect.Width = textwidth + 5;
                    TextRect.Height = textheight + 5;
                    Myg.DrawString(Text, MyFont, FontBrush, TextRect, sf); return;  }

 
            }

          
                TextRect = DrawRect;
                Myg.DrawString(Text, MyFont, FontBrush, TextRect, sf); return;

    

       ////////////自动对齐为实现

           




            ////也可以用效果不好
            ////TextRenderer.DrawText(g, this.Text, this.Font, pevent.ClipRectangle, FontColor);

        }


    }
}
