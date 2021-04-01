using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace trpo_lw4
{
    public partial class Form1 : Form
    {
        private Timer moveTimer = new Timer();

        private bool bPixel;
        private bool bMove;

        enum LineType
        {
            None,
            Curve,
            Bezier,
            Polygone,
            FilledCurve
        };
        
        private LineType LineTypeToShow = LineType.None;
        private List<Point> arPoints = new List<Point>();
        private int[] arOffsets = {1, 1};

        public float PointRadius { get; set; } = 15;
        private Color pointColor { get; set; } = Color.DarkMagenta;
        public Color lineColor { get; set; } = Color.PaleVioletRed;

        private int lineWidth = 3;
        private int curveTension = 3;
        private int pointsSpeed = 10;
        private int[] extraSpeed = {5, 0};

        private bool bDrag;
        private int iPointToDrag;

        public Form1()
        {
            InitializeComponent();

            const char c1 = 'O';
            const char c2 = 'N';
            const int variant = ((int)c1 + (int)c2) % 8;

            this.Text = $"Вариант {variant}"; // Вариант 5
            this.MinimumSize = new Size(400, 400);

            // КНОПКИ
            btPixel.Click += btPixel_Click;
            btParams.Click += btParams_Click;
            btMove.Click += btMove_Click;

            DoubleBuffered = true;

            // ОБРАБОТЧИКИ ФОРМЫ
            MouseClick += Form1_MouseClick;
            MouseDown += Form1_MouseDown;
            MouseMove += Form1_MouseMove;
            MouseUp += Form1_MouseUp;

            Paint += Form1_Paint;

            moveTimer.Interval = 30;
            moveTimer.Tick += TimerTickHandler;

            KeyPreview = true;
            KeyDown += Form1_KeyDown;

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (msg.WParam.ToInt32())
            {
                case (38):
                    extraSpeed[1] -= 5;
                    break;
                case (40):
                    extraSpeed[1] += 5;
                    break;
                case (39):
                    extraSpeed[0] += 5;
                    break;
                case (37):
                    extraSpeed[0] -= 5;
                    break;
                case (187):
                    pointsSpeed += 5;
                    break;
                case (189):
                    pointsSpeed -= 5;
                    break;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case (Keys.Space):
                    (Controls["btMove"] as Button)?.PerformClick();
                    break;
                case (Keys.Escape):
                    (Controls["btClear"] as Button)?.PerformClick();
                    break;
            }

            e.Handled = true;
        }
        void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            Point p = e.Location;
            if (bPixel)
            {
                arPoints.Add(p);
                LineTypeToShow = LineType.None;
                Refresh();
            }
        }
        void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            bDrag = false;
        }
        void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (bDrag)
            {
                arPoints[iPointToDrag] = new Point(e.Location.X, e.Location.Y);
                Refresh();
            }
        }
        void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            for (int i=0; i < arPoints.Count;i++)
                if (IsOnPoint(arPoints[i], e.Location))
                {
                    bDrag = true;
                    iPointToDrag = i;
                    break;
                }
        }
        bool IsOnPoint(Point pPixel, Point pMouse)
        {
            if ((pMouse.X >= pPixel.X - PointRadius / 2) &&
                (pMouse.X <= pPixel.X + PointRadius / 2) &&
                (pMouse.Y >= pPixel.Y - PointRadius / 2) &&
                (pMouse.Y <= pPixel.Y + PointRadius / 2))
                return true;

            return false;
        }


        void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (arPoints.Count > 0)
            {
                ShowPoints(g);
                if (LineTypeToShow != LineType.None)
                {
                    ShowLine(g, LineTypeToShow);
                }
            }
        }
        void ShowPoints(Graphics g)
        {
            SolidBrush br = new SolidBrush(pointColor);

            foreach (Point point in arPoints)
                g.FillEllipse(br, point.X - PointRadius / 2, point.Y - PointRadius / 2, PointRadius, PointRadius);
        }
        void ShowLine(Graphics g, LineType lt)
        {
            SolidBrush br = new SolidBrush(lineColor);
            Pen pen = new Pen(br, lineWidth);
            switch (lt)
            {
                case (LineType.Curve):
                    g.DrawClosedCurve(pen, arPoints.ToArray(), curveTension, FillMode.Alternate);
                    break;
                case (LineType.Polygone):
                    g.DrawPolygon(pen, arPoints.ToArray());
                    break;
                case (LineType.Bezier):
                    g.DrawBeziers(pen, arPoints.ToArray());
                    break;
                case (LineType.FilledCurve):
                    g.FillClosedCurve(br, arPoints.ToArray());
                    break;
            }
            br.Dispose();
            pen.Dispose();
        }
        void TimerTickHandler(object sender, EventArgs e)
        {
            MovePoints();
            Refresh();
        }

        void MovePoints()
        {
            int _x = 0, _y = 0;
            bool revertX = false;
            bool revertY = false;

            // Сбрасываем режим "Точки"
            if (bPixel)
                (Controls["btPixel"] as Button)?.PerformClick();

            for (int i = 0; i < arPoints.Count; i++)
            {
                _x = arPoints[i].X + arOffsets[0] * pointsSpeed + extraSpeed[0];
                if (_x >= this.ClientRectangle.Width || _x <= 0)
                {
                    revertX = true;
                }

                _y = arPoints[i].Y + arOffsets[1] * pointsSpeed + extraSpeed[1];
                if (_y >= this.ClientRectangle.Height || _y <= 0)
                {
                    revertY = true;
                }
            }

            if (revertX)
            {
                arOffsets[0] = -arOffsets[0];
            }
            if (revertY)
            {
                arOffsets[1] = -arOffsets[1];
            }

            for (int i = 0; i < arPoints.Count; i++)
            {
                if (revertX) 
                    _x = arPoints[i].X + arOffsets[0] * (pointsSpeed + extraSpeed[0]);
                else
                    _x = arPoints[i].X + arOffsets[0] * pointsSpeed + extraSpeed[0];

                if (revertY)
                    _y = arPoints[i].Y + arOffsets[1] * (pointsSpeed + extraSpeed[1]);
                else
                     _y = arPoints[i].Y + arOffsets[1] * pointsSpeed + extraSpeed[1];

                arPoints[i] = new Point(_x, _y);
            }
        }

        void btPixel_Click(object sender, EventArgs e)
        {
            bPixel = !bPixel;
            Button b = (Button) sender;
            if (bPixel)
            {
                LineTypeToShow = LineType.None;
                arPoints = new List<Point>();
                b.BackColor = Color.LightBlue;
            }
            else
            {
                b.BackColor = DefaultBackColor;
            }
        }
        void btCurve_Click(object sender, EventArgs e)
        {
            if (arPoints.Count > 0)
            {
                LineTypeToShow = LineType.Curve;
                if (bPixel)
                    this.Controls.OfType<Button>().First(b => b.Name == "btPixel").PerformClick();
                Refresh();
            }
        }
        void btPolygone_Click(object sender, EventArgs e)
        {
            if (arPoints.Count > 0)
            {
                LineTypeToShow = LineType.Polygone;
                if (bPixel)
                    this.Controls.OfType<Button>().First(b => b.Name == "btPixel").PerformClick();
                Refresh();
            }
        }
        void btBezier_Click(object sender, EventArgs e)
        {
            if (arPoints.Count > 0 && (arPoints.Count - 1) % 3 == 0)
            {
                LineTypeToShow = LineType.Bezier;
                if (bPixel)
                    this.Controls.OfType<Button>().First(b => b.Name == "btPixel").PerformClick();
                Refresh();
            }
        }
        void btFilledCurve_Click(object sender, EventArgs e)
        {
            if (arPoints.Count > 0)
            {
                LineTypeToShow = LineType.FilledCurve;
                if (bPixel)
                    this.Controls.OfType<Button>().First(b => b.Name == "btPixel").PerformClick();
                Refresh();
            }
        }
        void btParams_Click(object sender, EventArgs e)
        {
            ParamForms pForms = new ParamForms(this);
            pForms.ShowDialog();
        }
        void btMove_Click(object sender, EventArgs e)
        {
            if (arPoints.Count == 0)
                bMove = false;
            else
                bMove = !bMove;

            if (bMove)
            {
                extraSpeed = new[] {0, 0};
                moveTimer.Start();
            }
            else
            {
                moveTimer.Stop();
            }
        }
        void btClear_Click(object sender, EventArgs e)
        {
            arPoints = new List<Point>();
            LineTypeToShow = LineType.None;
            if (bMove)
                Controls.OfType<Button>().First(b => b.Name == "btMove").PerformClick();
            bPixel = false;
            bDrag = false;
            Refresh();
        }
    }
}
