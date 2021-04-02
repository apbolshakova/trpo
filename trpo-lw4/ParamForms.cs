using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace trpo_lw4
{
    public partial class ParamForms : Form
    {
        private Form1 parent;
        public ParamForms(Form1 parentForm)
        {
            InitializeComponent();

            parent = parentForm;

            lineUpDown.Value = parent.lineWidth;
            pointUpDown.Value = parent.PointRadius;

            lineUpDown.ValueChanged += LineUpDown_ValueChanged;
            pointUpDown.ValueChanged += PointUpDown_ValueChanged;
        }

        void LineUpDown_ValueChanged(object sender, EventArgs e)
        {
            parent.lineWidth = (int)lineUpDown.Value;
            parent.Refresh();
        }
        void PointUpDown_ValueChanged(object sender, EventArgs e)
        {
            parent.PointRadius = (int)pointUpDown.Value;
            parent.Refresh();
        }
    }
}
