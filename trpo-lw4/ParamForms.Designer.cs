
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
            this.btUpdate = new System.Windows.Forms.Button();
            this.ptSizeUpDown = new System.Windows.Forms.NumericUpDown();
            this.cmbColors = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.ptSizeUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // btUpdate
            // 
            this.btUpdate.Location = new System.Drawing.Point(74, 340);
            this.btUpdate.Name = "btUpdate";
            this.btUpdate.Size = new System.Drawing.Size(75, 23);
            this.btUpdate.TabIndex = 0;
            this.btUpdate.Text = "btUpdate";
            this.btUpdate.UseVisualStyleBackColor = true;
            // 
            // ptSizeUpDown
            // 
            this.ptSizeUpDown.Location = new System.Drawing.Point(74, 261);
            this.ptSizeUpDown.Name = "ptSizeUpDown";
            this.ptSizeUpDown.Size = new System.Drawing.Size(120, 22);
            this.ptSizeUpDown.TabIndex = 1;
            // 
            // cmbColors
            // 
            this.cmbColors.FormattingEnabled = true;
            this.cmbColors.Location = new System.Drawing.Point(73, 189);
            this.cmbColors.Name = "cmbColors";
            this.cmbColors.Size = new System.Drawing.Size(121, 24);
            this.cmbColors.TabIndex = 2;
            // 
            // ParamForms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cmbColors);
            this.Controls.Add(this.ptSizeUpDown);
            this.Controls.Add(this.btUpdate);
            this.Name = "ParamForms";
            this.Text = "ParamForms";
            ((System.ComponentModel.ISupportInitialize)(this.ptSizeUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btUpdate;
        private System.Windows.Forms.NumericUpDown ptSizeUpDown;
        private System.Windows.Forms.ComboBox cmbColors;
    }
}