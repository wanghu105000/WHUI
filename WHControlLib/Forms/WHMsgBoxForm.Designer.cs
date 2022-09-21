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
            this.whButtonPro1 = new WHControlLib.WHButtonPro();
            this.whTextBox1 = new WHControlLib.WHTextBox();
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
            // whButtonPro1
            // 
            this.whButtonPro1.BackColor = System.Drawing.Color.Transparent;
            this.whButtonPro1.BorderWidth = 2;
            this.whButtonPro1.BornColor = System.Drawing.Color.Blue;
            this.whButtonPro1.FirstFillcolor = System.Drawing.Color.Orange;
            this.whButtonPro1.FontColor = System.Drawing.Color.Black;
            this.whButtonPro1.IsDrawBoin = false;
            this.whButtonPro1.IsHightLight = true;
            this.whButtonPro1.IsUseTwoColor = false;
            this.whButtonPro1.Location = new System.Drawing.Point(46, 162);
            this.whButtonPro1.MyFillColorDec = WHControlLib.WHButtonPro.FillColorDec.Vertical;
            this.whButtonPro1.MyShape = WHControlLib.WHButtonPro.Shape.RoundRectange;
            this.whButtonPro1.MyText = "cfch";
            this.whButtonPro1.MyTextAlign = WHControlLib.WHButtonPro.TextAlign.Center;
            this.whButtonPro1.Name = "whButtonPro1";
            this.whButtonPro1.OnMouseColor = System.Drawing.Color.Lime;
            this.whButtonPro1.Radius = 5F;
            this.whButtonPro1.SecondFillcolor = System.Drawing.Color.Orange;
            this.whButtonPro1.Size = new System.Drawing.Size(310, 54);
            this.whButtonPro1.TabIndex = 5;
            this.whButtonPro1.Load += new System.EventHandler(this.whButtonPro1_Load);
            this.whButtonPro1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.whButtonPro1_MouseClick);
            // 
            // whTextBox1
            // 
            this.whTextBox1.BackColor = System.Drawing.Color.Transparent;
            this.whTextBox1.BorderColor = System.Drawing.Color.Black;
            this.whTextBox1.FillBackgroundColor = System.Drawing.Color.MediumPurple;
            this.whTextBox1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.whTextBox1.IsAllowMultieLine = true;
            this.whTextBox1.IsCanEditText = false;
            this.whTextBox1.IsDisplayWaterMark = false;
            this.whTextBox1.IsEnable = false;
            this.whTextBox1.Location = new System.Drawing.Point(46, 68);
            this.whTextBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.whTextBox1.MyFont = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.whTextBox1.MyFontColor = System.Drawing.SystemColors.HighlightText;
            this.whTextBox1.MyShape = WHControlLib.WHTextBox.Shapes.HalfCircle;
            this.whTextBox1.MyText = "nb bgh";
            this.whTextBox1.Name = "whTextBox1";
            this.whTextBox1.Radius = 3F;
            this.whTextBox1.Size = new System.Drawing.Size(290, 61);
            this.whTextBox1.TabIndex = 6;
            this.whTextBox1.WaterMarkColor = System.Drawing.Color.LightGray;
            this.whTextBox1.WatermarkDirection = WHControlLib.WHTextBox.WaterMarkDirection.left;
            this.whTextBox1.WaterMarkText = "";
            // 
            // WhMsgBoxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(384, 228);
            this.Controls.Add(this.whTextBox1);
            this.Controls.Add(this.whButtonPro1);
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
        private WHButtonPro whButtonPro1;
        private WHTextBox whTextBox1;
    }
}