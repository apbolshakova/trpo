
namespace trpo_lw4
{
    partial class ParamForms
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
            this.lineUpDown = new System.Windows.Forms.NumericUpDown();
            this.pointUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.lineUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pointUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // lineUpDown
            // 
            this.lineUpDown.Location = new System.Drawing.Point(197, 34);
            this.lineUpDown.Name = "lineUpDown";
            this.lineUpDown.Size = new System.Drawing.Size(150, 22);
            this.lineUpDown.TabIndex = 1;
            // 
            // pointUpDown
            // 
            this.pointUpDown.Location = new System.Drawing.Point(197, 94);
            this.pointUpDown.Name = "pointUpDown";
            this.pointUpDown.Size = new System.Drawing.Size(150, 22);
            this.pointUpDown.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Толщина кривой";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Радиус точки";
            // 
            // ParamForms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 149);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pointUpDown);
            this.Controls.Add(this.lineUpDown);
            this.Name = "ParamForms";
            this.Text = "ParamForms";
            ((System.ComponentModel.ISupportInitialize)(this.lineUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pointUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.NumericUpDown lineUpDown;
        private System.Windows.Forms.NumericUpDown pointUpDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}