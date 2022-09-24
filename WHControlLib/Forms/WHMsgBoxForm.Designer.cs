namespace WHControlLib.Forms
{
    partial class WhMsgBoxForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WhMsgBoxForm));
            this.label1 = new System.Windows.Forms.Label();
            this.whButton1 = new WHControlLib.Controls.WHButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(84, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "label1";
            // 
            // whButton1
            // 
            this.whButton1.BackColor = System.Drawing.Color.Transparent;
            this.whButton1.BorderWidth = 2;
            this.whButton1.BornColor = System.Drawing.Color.Blue;
            this.whButton1.ColorChangeint = 50;
            this.whButton1.FirstFillcolor = System.Drawing.Color.Orange;
            this.whButton1.FontColor = System.Drawing.Color.Black;
            this.whButton1.ImageTranparentColor = System.Drawing.Color.White;
            this.whButton1.IsDrawBoin = false;
            this.whButton1.IsOpenimageTranparentColor = false;
            this.whButton1.IsShowFouceLine = true;
            this.whButton1.IsShowMark = false;
            this.whButton1.IsShowMarkBorder = false;
            this.whButton1.IsShowMyImage = true;
            this.whButton1.IsShowText = true;
            this.whButton1.IsUseTwoColor = false;
            this.whButton1.Location = new System.Drawing.Point(50, 118);
            this.whButton1.MarkBackColor = System.Drawing.Color.Red;
            this.whButton1.MarkBorderColor = System.Drawing.Color.White;
            this.whButton1.MarkBorderWidth = 1;
            this.whButton1.MarkerTextColor = System.Drawing.Color.White;
            this.whButton1.MarkerTextSzie = 10;
            this.whButton1.MarkHeight = 2F;
            this.whButton1.MarkText = "";
            this.whButton1.MarkTextColor = System.Drawing.Color.White;
            this.whButton1.MyFillColorDec = WHControlLib.baseStaticCtrl.FillColorDec.Vertical;
            this.whButton1.MyFont = new System.Drawing.Font("微软雅黑", 12F);
            this.whButton1.MyImage = ((System.Drawing.Image)(resources.GetObject("whButton1.MyImage")));
            this.whButton1.MyImageDec = WHControlLib.baseStaticCtrl.ImageDec.left;
            this.whButton1.MyimageHeight = 0.9F;
            this.whButton1.MyimageWidth = 0.5F;
            this.whButton1.MyShape = WHControlLib.baseStaticCtrl.Shape.RoundRectange;
            this.whButton1.MyTextAlign = WHControlLib.baseStaticCtrl.TextAlign.Center;
            this.whButton1.Name = "whButton1";
            this.whButton1.OnMouseColor = System.Drawing.Color.BurlyWood;
            this.whButton1.Radius = 5F;
            this.whButton1.SecondFillcolor = System.Drawing.Color.Orange;
            this.whButton1.Size = new System.Drawing.Size(292, 77);
            this.whButton1.TabIndex = 4;
            this.whButton1.Text = "whButton1";
            // 
            // WhMsgBoxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(384, 228);
            this.Controls.Add(this.whButton1);
            this.Controls.Add(this.label1);
            this.firstColor = System.Drawing.Color.Indigo;
            this.IsUseTwoColor = true;
            this.Name = "WhMsgBoxForm";
            this.Radius = 9F;
            this.SecondColor = System.Drawing.Color.RoyalBlue;
            this.Text = "WhMsgBoxForm";
            this.Load += new System.EventHandler(this.WhMsgBoxForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private Controls.WHButton whButton1;
    }
}