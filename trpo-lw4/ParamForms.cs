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

            FormClosed += ParamForms_FormClosed;
            btUpdate.Click += BtUpdate_Click;
            ptSizeUpDown.ValueChanged += PtSizeUpDown_ValueChanged;
            cmbColors.Items.AddRange(Enum.GetNames(typeof(KnownColor)));
            cmbColors.SelectedValueChanged += CmbColors_SelectedValueChanged;
        }

        void CmbColors_SelectedValueChanged(object sender, EventArgs e)
        {
            parent.lineColor = Color.FromName(cmbColors.SelectedItem.ToString());
            parent.Refresh();
        }
        void PtSizeUpDown_ValueChanged(object sender, EventArgs e)
        {
            parent.PointRadius = (int)ptSizeUpDown.Value;
            parent.Refresh();
        }
        void BtUpdate_Click(object sender, EventArgs e)
        {
            parent.PointRadius = (int)ptSizeUpDown.Value;
            parent.lineColor = Color.FromName(cmbColors.SelectedItem.ToString());
            parent.Refresh();
        }
        void ParamForms_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
        void btPointColor_Click(object sender, EventArgs e)
        {

        }
    }
}
