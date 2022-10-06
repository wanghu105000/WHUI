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

      


    }
}
