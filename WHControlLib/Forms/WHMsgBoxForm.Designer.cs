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
            this.label1 = new System.Windows.Forms.Label();
            this.whButton1 = new WHControlLib.Controls.WHButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(138, 20);
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
            this.whButton1.FirstFillcolor = System.Drawing.Color.Sienna;
            this.whButton1.FontColor = System.Drawing.Color.Black;
            this.whButton1.IsDrawBoin = false;
            this.whButton1.IsShowText = true;
            this.whButton1.IsUseTwoColor = true;
            this.whButton1.Location = new System.Drawing.Point(72, 141);
            this.whButton1.MyFillColorDec = WHControlLib.baseStaticCtrl.FillColorDec.Vertical;
            this.whButton1.MyFont = new System.Drawing.Font("微软雅黑", 12F);
            this.whButton1.MyShape = WHControlLib.baseStaticCtrl.Shape.HalfCircle;
            this.whButton1.MyTextAlign = WHControlLib.baseStaticCtrl.TextAlign.Center;
            this.whButton1.Name = "whButton1";
            this.whButton1.OnMouseColor = System.Drawing.Color.Linen;
            this.whButton1.Radius = 5F;
            this.whButton1.SecondFillcolor = System.Drawing.Color.Orange;
            this.whButton1.Size = new System.Drawing.Size(246, 58);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private Controls.WHButton whButton1;
    }
}