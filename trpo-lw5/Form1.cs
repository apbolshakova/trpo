using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace trpo_lw5
{
    public partial class Form1 : Form
    {
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
        }
    }

    enum Cities
    {
        Spb, Msk, Yo
    }

    class University
    {
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("Ошибка: название университета не может быть пустым!");
                }

                _name = value;
            }
        }

        private int _foundYear;
        public int FoundYear
        {
            get => _foundYear;
            set
            {
                if (value < 1000 || 2021 < value)
                {
                    throw new Exception("Ошибка: некорректный год основания!");
                }

                _foundYear = value;
            }
        }

        private int _numOfStudents;
        public int NumOfStudents
        {
            get => _numOfStudents;
            set
            {
                if (value < 0)
                {
                    throw new Exception("Ошибка: некорректное количество студентов!");
                }

                _numOfStudents = value;
            }
        }

        public Cities City;
        public bool IsTechnical;
        public Image Image;
    }
}
