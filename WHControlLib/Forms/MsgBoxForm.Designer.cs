namespace WHControlLib.Forms
{
    partial class MsgBoxForm
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
            this.SuspendLayout();
            // 
            // MsgBoxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SkyBlue;
            this.ClientSize = new System.Drawing.Size(384, 228);
            this.CloseBoxBorderColor = System.Drawing.Color.White;
            this.FormBorderColor = System.Drawing.Color.Firebrick;
            this.FormBorderWidth = 4F;
            this.IsUseTwoColor = true;
            this.MyCloseBoxShape = WHControlLib.Forms.BaseDialogFormcs.CloseBoxShape.Circle;
            this.Name = "MsgBoxForm";
            this.Text = "MsgBoxForm";
            this.TitleBackColor = System.Drawing.Color.Transparent;
            this.TitleHeight = 6;
            this.ResumeLayout(false);

        }

        #endregion
    }
}