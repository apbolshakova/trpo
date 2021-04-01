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
        private Timer myTimer = new Timer();

        private bool bPixel;
        private bool bMove;
        private bool bSameDirectionToMove;

        enum LineType
        {
            None,
            Curve,
            Bezier,
            Polygone,
            FilledCurve
        };

        private bool bShowLine;
        private LineType LineTypeToShow;
        private List<Point> arPoints = new List<Point>();
        private List<Point> arOffsets = new List<Point>();

        public float PointRadius { get; set; } = 5;
        public Size PointSize { get; set; } = new Size(5, 5);
        private Color pointColor { get; set; } = Color.DarkMagenta;
        public Color lineColor { get; set; } = Color.PaleVioletRed;

        private int lineWidth;
        private int curveTension;

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

            pointColor = Color.DarkBlue;
            lineColor = Color.DarkBlue;
            lineWidth = 4;
            curveTension = 2;
            bSameDirectionToMove = true;

            // КНОПКИ
            btPixel.Click += new EventHandler(btPixel_Click);
            btParams.Click += new EventHandler(btParams_Click);
            btMove.Click += new EventHandler(btMove_Click);

            DoubleBuffered = true;

            // ОБРАБОТЧИКИ ФОРМЫ
            MouseClick += new MouseEventHandler(Form1_MouseClick);
            MouseDown += new MouseEventHandler(Form1_MouseDown);
            MouseMove += new MouseEventHandler(Form1_MouseMove);
            MouseUp += new MouseEventHandler(Form1_MouseUp);

            Paint += new PaintEventHandler(Form1_Paint);

            moveTimer.Interval = 30;
            moveTimer.Tick += new EventHandler(TimerTickHandler);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            return base.ProcessCmdKey(ref msg, keyData);
        }

        void Form1_KeyDown(object sender, KeyEventArgs e)
        {

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
            if ((pMouse.X >= pPixel.X - PointSize.Width / 2 &&
                 pMouse.X <= pPixel.X + PointSize.Width / 2) &&
                (pMouse.Y >= pPixel.Y - PointSize.Height / 2 &&
                 pMouse.Y <= pPixel.Y + PointSize.Height / 2))
                return true;
            else
                return false;
        }

        void ShowMousePosition(object sender, PaintEventArgs e)
        {

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
            SolidBrush br = new SolidBrush(lineColor);

            foreach (Point point in arPoints)
                g.FillEllipse(br, point.X, point.Y, PointRadius, PointRadius);
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
                default:
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
            int _x, _y;

            // Сбрасываем режим "Точки"
            if (bPixel)
                (this.Controls["btPixel"] as Button).PerformClick();

            for (int i = 0; i < arPoints.Count; i++)
            {
                _x = arPoints[i].X + arOffsets[i].X;
                if (_x >= this.ClientRectangle.Width || _x <= 0)
                {
                    arOffsets[i] = new Point(-arOffsets[i].X, arOffsets[i].Y);
                    _x = arPoints[i].X + arOffsets[i].X;
                }

                _y = arPoints[i].Y + arOffsets[i].Y;
                if (_y >= this.ClientRectangle.Height || _y <= 0)
                {
                    arOffsets[i] = new Point(arOffsets[i].X, -arOffsets[i].Y);
                    _y = arPoints[i].Y + arOffsets[i].Y;
                }

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
                b.BackColor = Button.DefaultBackColor;
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
                arOffsets = new List<Point>();
                int _x = 0, _y = 0;
                Random r = new Random((int) DateTime.Now.Ticks);
                if (bSameDirectionToMove)
                {
                    _x = r.Next(0, 10);
                    _y = r.Next(0, 10);
                }

                for (int i = 0; i < arPoints.Count; i++)
                {
                    if (!bSameDirectionToMove)
                    {
                        _x = r.Next(0, 10);
                        _y = r.Next(0, 10);
                    }
                    arOffsets.Add(new Point(_x, _y));
                }
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
