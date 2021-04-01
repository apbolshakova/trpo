
namespace trpo_lw4
{
    partial class Form1
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
            this.btPixel = new System.Windows.Forms.Button();
            this.btParams = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.btMove = new System.Windows.Forms.Button();
            this.btClear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btPixel
            // 
            this.btPixel.Location = new System.Drawing.Point(12, 12);
            this.btPixel.Name = "btPixel";
            this.btPixel.Size = new System.Drawing.Size(121, 30);
            this.btPixel.TabIndex = 0;
            this.btPixel.Text = "Точки";
            this.btPixel.UseVisualStyleBackColor = true;
            // 
            // btParams
            // 
            this.btParams.Location = new System.Drawing.Point(12, 48);
            this.btParams.Name = "btParams";
            this.btParams.Size = new System.Drawing.Size(121, 30);
            this.btParams.TabIndex = 1;
            this.btParams.Text = "Параметры";
            this.btParams.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 84);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(121, 30);
            this.button3.TabIndex = 2;
            this.button3.Text = "Кривая";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.btCurve_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(12, 120);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(121, 30);
            this.button4.TabIndex = 3;
            this.button4.Text = "Ломанная";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.btPolygone_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(12, 156);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(121, 30);
            this.button5.TabIndex = 4;
            this.button5.Text = "Безье";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.btBezier_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(12, 192);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(121, 30);
            this.button6.TabIndex = 5;
            this.button6.Text = "Заполненная";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.btFilledCurve_Click);
            // 
            // btMove
            // 
            this.btMove.Location = new System.Drawing.Point(12, 228);
            this.btMove.Name = "btMove";
            this.btMove.Size = new System.Drawing.Size(121, 30);
            this.btMove.TabIndex = 6;
            this.btMove.Text = "Движение";
            this.btMove.UseVisualStyleBackColor = true;
            // 
            // btClear
            // 
            this.btClear.Location = new System.Drawing.Point(12, 264);
            this.btClear.Name = "btClear";
            this.btClear.Size = new System.Drawing.Size(121, 30);
            this.btClear.TabIndex = 7;
            this.btClear.Text = "Очистить";
            this.btClear.UseVisualStyleBackColor = true;
            this.btClear.Click += new System.EventHandler(this.btClear_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(968, 503);
            this.Controls.Add(this.btClear);
            this.Controls.Add(this.btMove);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btParams);
            this.Controls.Add(this.btPixel);
            this.Name = "Form1";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btPixel;
        private System.Windows.Forms.Button btParams;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button btMove;
        private System.Windows.Forms.Button btClear;
    }
}