
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
            this.btCurve = new System.Windows.Forms.Button();
            this.btPolygone = new System.Windows.Forms.Button();
            this.btBezier = new System.Windows.Forms.Button();
            this.btFilledCurve = new System.Windows.Forms.Button();
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
            // btCurve
            // 
            this.btCurve.Location = new System.Drawing.Point(12, 84);
            this.btCurve.Name = "btCurve";
            this.btCurve.Size = new System.Drawing.Size(121, 30);
            this.btCurve.TabIndex = 2;
            this.btCurve.Text = "Кривая";
            this.btCurve.UseVisualStyleBackColor = true;
            this.btCurve.Click += new System.EventHandler(this.btCurve_Click);
            // 
            // btPolygone
            // 
            this.btPolygone.Location = new System.Drawing.Point(12, 120);
            this.btPolygone.Name = "btPolygone";
            this.btPolygone.Size = new System.Drawing.Size(121, 30);
            this.btPolygone.TabIndex = 3;
            this.btPolygone.Text = "Ломанная";
            this.btPolygone.UseVisualStyleBackColor = true;
            this.btPolygone.Click += new System.EventHandler(this.btPolygone_Click);
            // 
            // btBezier
            // 
            this.btBezier.Location = new System.Drawing.Point(12, 156);
            this.btBezier.Name = "btBezier";
            this.btBezier.Size = new System.Drawing.Size(121, 30);
            this.btBezier.TabIndex = 4;
            this.btBezier.Text = "Безье";
            this.btBezier.UseVisualStyleBackColor = true;
            this.btBezier.Click += new System.EventHandler(this.btBezier_Click);
            // 
            // btFilledCurve
            // 
            this.btFilledCurve.Location = new System.Drawing.Point(12, 192);
            this.btFilledCurve.Name = "btFilledCurve";
            this.btFilledCurve.Size = new System.Drawing.Size(121, 30);
            this.btFilledCurve.TabIndex = 5;
            this.btFilledCurve.Text = "Заполненная";
            this.btFilledCurve.UseVisualStyleBackColor = true;
            this.btFilledCurve.Click += new System.EventHandler(this.btFilledCurve_Click);
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
            this.Controls.Add(this.btFilledCurve);
            this.Controls.Add(this.btBezier);
            this.Controls.Add(this.btPolygone);
            this.Controls.Add(this.btCurve);
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
        private System.Windows.Forms.Button btCurve;
        private System.Windows.Forms.Button btPolygone;
        private System.Windows.Forms.Button btBezier;
        private System.Windows.Forms.Button btFilledCurve;
        private System.Windows.Forms.Button btMove;
        private System.Windows.Forms.Button btClear;
    }
}