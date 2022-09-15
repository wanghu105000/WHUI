using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WHControlLib
{

  
    public partial class WHbaseTextBoxEx :TextBox
    {
        /// <summary>
        /// 自绘消息
        /// </summary>
        const int WM_paint=0xf;
       /// <summary>
       /// 控件颜色改变或编辑时消息
       /// </summary>
        const int WM_CtlColOrEdit = 0x133;

        #region 属性定义

        private string  _waterMarkstring="";
        [Category("A我的"), Description("水印文字内容，默认 ，无，"), Browsable(true)]
        public string  WaterMarkstring
        {
            get { return _waterMarkstring; }
            set { _waterMarkstring= value; }
        }
        private bool _isDisplayWaterMark=false;
        [Category("A我的"), Description("是否开启水印文字，默认 ，否，"), Browsable(true)]
        public bool IsDisplayWaterMark
        {
            get { return _isDisplayWaterMark; }
            set { _isDisplayWaterMark = value; Invalidate(); }
        }

        private Color _watermarkColor=Color.Silver;
        [Category("A我的"), Description("水印文字的颜色，默认 ，灰色，"), Browsable(true)]
        public Color WatermarkColor
        {
            get { return _watermarkColor; }
            set { _watermarkColor = value;Invalidate(); }
        }



        private int _waterMarkDire ;
        [Category("A我的"), Description("水印文字对齐方式，默认 ，左对齐，"), Browsable(true)]
        public int WaterMarkDire
        {
            get { return _waterMarkDire; }
            set { _waterMarkDire = value; }
        }

        #endregion



        public WHbaseTextBoxEx()
        {
           
            InitializeComponent();
            this.AutoSize = false;
             
        }
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == WM_paint || m.Msg == WM_CtlColOrEdit)
            {
            
                if (this.Text==""&& !this.Focused && IsDisplayWaterMark)
                {
                    try
                    {
                        DrawMarkText();
                    }
                    catch (Exception)
                    { }

                }

            }

        }
        void DrawMarkText( )
        {


            using (SolidBrush WaterFontBrush = new SolidBrush(WatermarkColor))
            {

                StringFormat sf = new StringFormat();
                switch (WaterMarkDire)
                {
                    case 0:
                        sf.Alignment = StringAlignment.Near; 
                        break;
                    case 1:
                        sf.Alignment =StringAlignment.Center;
                        break;
                    case 2:
                        sf.Alignment = StringAlignment.Far;
                        break;
                    default:
                        sf.Alignment = StringAlignment.Center;
                        break;
                }

               
              
                
                sf.LineAlignment = StringAlignment.Center;
                using (Graphics g=this.CreateGraphics())
                {
                   
                    g.DrawString(WaterMarkstring,this.Font,WaterFontBrush,this.ClientRectangle,sf);
                }

            }

        }

        private void WHbaseTextBoxEx_Layout(object sender, LayoutEventArgs e)
        {
            
        }
        //   win32 发窗口消息方法 画水印

        //if (IsDisplayWaterMark&& WaterMarkString!=""&& !textbox.Focused)
        //{
        //   SendMessage(this.textbox.Handle, EM_SETCUEBANNER, 0, WaterMarkString);
        //}


    }
}
