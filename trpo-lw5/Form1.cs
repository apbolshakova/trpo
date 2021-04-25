using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace trpo_lw5
{
    public partial class Form1 : Form
    {
        BindingSource bs;
        public Form1()
        {
            InitializeComponent();

            const char c1 = 'b';
            const char c2 = 'a';
            const int variant = ((int)c1 + (int)c2) % 8;
            const int functionalVar = ((int)'B') % 3;
            const int diagramVar = ((int)'A') % 3;

            this.Text = $"V: {variant}, F: {functionalVar}, D: {diagramVar}"; // Вариант 3, F = 0, D = 2
            this.MinimumSize = new Size(400, 400);

            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.DoubleClick += PictureBox1_DoubleClick;

            BindAll();
        }

        private void BindAll()
        {
            bs = new BindingSource();
            bs.DataSource = GetInitData();

            bindingNavigator1.BindingSource = bs;
            
            dataGridView1.DataSource = bs;
            dataGridView1.Columns["ImageFile"].Visible = false;
            
            dataGridView1.Columns.Remove("City");

            var cityColumn = new DataGridViewComboBoxColumn();
            cityColumn.Name = "Город";
            cityColumn.HeaderText = "Город";
            cityColumn.DataPropertyName = "City";
            cityColumn.DataSource = Enum.GetValues(typeof(Cities));
            dataGridView1.Columns.Add(cityColumn);

            propertyGrid1.DataBindings.Add("SelectedObject", bs, "");
            pictureBox1.DataBindings.Add("ImageLocation", bs, "ImageFile");

            chart1.DataSource = (bs.DataSource as List<University>).GroupBy(university => university.City).Select(universitiesGroup => new
            {
                City = universitiesGroup.Key.ToString(),
                AvgFoundYear = universitiesGroup.Average(university => university.FoundYear),
            }); ;
            chart1.Series[0].XValueMember = "City";
            chart1.Series[0].YValueMembers = "AvgFoundYear";
            chart1.Legends.Clear();
            bs.CurrentChanged += (o, e) => chart1.DataBind();

            toolStripTextBox1.TextChanged += (o, e) =>
            {
                if (!string.IsNullOrEmpty(toolStripTextBox1.Text))
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        row.Selected = false;
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            if (cell.Value == null) continue;
                                if ((cell.ValueType.Name == "String") && (cell.Value.ToString().Contains(toolStripTextBox1.Text)))
                                row.Selected = true;
                        }
                    }
                }
                else
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        row.Selected = false;
                    }
                }
            };
        }

        private void dataGridView1_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            var name = dataGridView1["Name", e.RowIndex].Value?.ToString() ?? "";
            var foundYear = (int) (dataGridView1["FoundYear", e.RowIndex].Value ?? -1);
            var numOfStudents = (int) (dataGridView1["NumOfStudents", e.RowIndex].Value ?? -1);
            if (string.IsNullOrWhiteSpace(name))
            {
                e.Cancel = true;
                dataGridView1.CurrentCell = dataGridView1["Name", e.RowIndex];
                dataGridView1.BeginEdit(true);
                MessageBox.Show("Имя не может быть пустым!");
            }
            else if (foundYear < 1000 || 2021 < foundYear)
            {
                e.Cancel = true;
                dataGridView1.CurrentCell = dataGridView1["FoundYear", e.RowIndex];
                dataGridView1.BeginEdit(true);
                MessageBox.Show("Некорректный год основания!");
            }
            else if (numOfStudents < 0)
            {
                e.Cancel = true;
                dataGridView1.CurrentCell = dataGridView1["NumOfStudents", e.RowIndex];
                dataGridView1.BeginEdit(true);
                MessageBox.Show("Количество студентов не может быть отрицательным!");
            }
        }

        private List<University> GetInitData() =>
            new List<University>
            {
                new University {City = Cities.Спб, FoundYear = 1899, IsTechnical = true, Name = "СПбПУ", NumOfStudents = 1000, ImageFile = "C:\\Users\\julod\\source\\repos\\trpo-lw5\\un1.jpg"},
                new University {City = Cities.Москва, FoundYear = 1900, IsTechnical = false, Name = "ВШЭ", NumOfStudents = 1234, ImageFile = "C:\\Users\\julod\\source\\repos\\trpo-lw5\\un2.jpg"},
                new University {City = Cities.Казань, FoundYear = 1500, IsTechnical = false, Name = "КГМУ", NumOfStudents = 2345, ImageFile = "C:\\Users\\julod\\source\\repos\\trpo-lw5\\un3.jpg"},
                new University {City = Cities.Москва, FoundYear = 1925, IsTechnical = true, Name = "МИФИ", NumOfStudents = 3456, ImageFile = "C:\\Users\\julod\\source\\repos\\trpo-lw5\\un4.jpg"},
                new University {City = Cities.Казань, FoundYear = 2000, IsTechnical = true, Name = "КФУ", NumOfStudents = 4567, ImageFile = "C:\\Users\\julod\\source\\repos\\trpo-lw5\\un5.jpg"},
                new University {City = Cities.Спб, FoundYear = 1960, IsTechnical = true, Name = "ИТМО", NumOfStudents = 5678, ImageFile = "C:\\Users\\julod\\source\\repos\\trpo-lw5\\un6.jpg"},
            };

        private void PictureBox1_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.InitialDirectory = System.Environment.CurrentDirectory;
            opf.Filter = "jpg|*.jpg|png|*.png";
            if (opf.ShowDialog() == DialogResult.OK)
            {
                (bs.Current as University).ImageFile = opf.FileName;
                bs.ResetBindings(false);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;
                var name = dataGridView1["Name", row.Index].Value?.ToString() ?? "";
                var foundYear = (int)(dataGridView1["FoundYear", row.Index].Value ?? -1);
                var numOfStudents = (int)(dataGridView1["NumOfStudents", row.Index].Value ?? -1);

                if (string.IsNullOrWhiteSpace(name))
                {
                    MessageBox.Show("Имя не может быть пустым!");
                    return;
                }
                else if (foundYear < 1000 || 2021 < foundYear)
                {
                    MessageBox.Show("Некорректный год основания!");
                    return;
                }
                else if (numOfStudents < 0)
                {
                    MessageBox.Show("Количество студентов не может быть отрицательным!");
                    return;
                }
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = System.Environment.CurrentDirectory;
            sfd.Filter = "bin|*.bin|xml|*.xml";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                switch (sfd.FilterIndex)
                {
                    case 1:
                        BinarySerialize(sfd.FileName);
                        break;
                    case 2:
                        SaveXml(sfd.FileName);
                        break;
                }
            }
        }

        private void BinarySerialize(string file)
        {
            BinaryFormatter bin = new BinaryFormatter();
            Stream sw = new FileStream(file, FileMode.Create);
            {
                bin.Serialize(sw, bs.DataSource);
            }
            sw.Close();
        }

        private void SaveXml(string file)
        {
            XmlSerializer ser = new XmlSerializer(typeof(List<University>));
            using (Stream sw = new FileStream(file, FileMode.Create))
            {
                ser.Serialize(sw, bs.DataSource);
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            OpenFileDialog sfd = new OpenFileDialog();
            sfd.InitialDirectory = System.Environment.CurrentDirectory;
            sfd.Filter = "bin|*.bin|xml|*.xml";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                switch (sfd.FilterIndex)
                {
                    case 1:
                        BinaryDeserialize(sfd.FileName);
                        break;
                    case 2:
                        LoadXml(sfd.FileName);
                        break;
                }
            }
        }

        private void BinaryDeserialize(string file)
        {
            BinaryFormatter bin = new BinaryFormatter();
            Stream sw = new FileStream(file, FileMode.Open);
            {
                bs.DataSource = (List<University>) bin.Deserialize(sw);
            }
            sw.Close();
        }

        private void LoadXml(string file)
        {
            XmlSerializer ser = new XmlSerializer(typeof(List<University>));
            using (Stream sw = new FileStream(file, FileMode.Open))
            {
                bs.DataSource = ser.Deserialize(sw);
            }
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(pictureBox1.ImageLocation))
            {
                ToolTip tt = new ToolTip();
                tt.SetToolTip(this.pictureBox1, "Нажмите дважды для изменения!");
            }
        }
    }

    public enum Cities : byte
    {
        Спб, Москва, Казань
    }

    [Serializable]
    public class University
    {
        private string _name;
        [DisplayName("Название")]
        public string Name
        {
            get => _name;
            set => _name = value;
        }

        private int _foundYear;
        [DisplayName("Год основания")]
        public int FoundYear
        {
            get => _foundYear;
            set => _foundYear = value;
        }

        private int _numOfStudents;
        [DisplayName("Количество студентов")]
        public int NumOfStudents
        {
            get => _numOfStudents;
            set => _numOfStudents = value;
        }
        
        [DisplayName("Город")]
        public Cities City { get; set; }

        [DisplayName("Является техническим")]
        public bool IsTechnical { get; set; }
        public string ImageFile { get; set; }
    }
}
