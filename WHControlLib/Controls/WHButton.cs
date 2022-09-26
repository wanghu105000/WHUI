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
    public partial class WHButton : baseStaticCtrl
    {
        public WHButton()
        {
            InitializeComponent();
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);

            Invalidate();

        }
        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            Invalidate();
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
                    if (this.Focused&& e.KeyCode== Keys.Enter&& this.Enabled)
            {
                OnClick(EventArgs.Empty);
            }
            
            base.OnKeyDown(e);
        }


        protected override void OnMouseEnter(EventArgs e)
        {

            IsMouseOnFlag = true;
            Invalidate();

            base.OnMouseEnter(e);

        }


        protected override void OnMouseLeave(EventArgs e)
        {    
              base.OnMouseLeave(e);
            IsMouseOnFlag = false;
            Invalidate();
         
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            IsMouseOnFlag = false;
            Invalidate();

          
           

        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            
            base.OnMouseUp(e);
            
            if (this.RectangleToScreen(this.DrawRect).Contains(MousePosition))
            {
              IsMouseOnFlag = true;
              Invalidate();
            } 
            
           

        }

    }
}
