using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace trpo_lw6
{
    public class Route : INotifyPropertyChanged
    {
        private string _name;
        private string _description;
        private string _peak;
        private int _height;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        public string Peak
        {
            get => _peak;
            set
            {
                _peak = value;
                OnPropertyChanged("Peak");
            }
        }

        public int Height
        {
            get => _height;
            set
            {
                _height = value;
                OnPropertyChanged("Height");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}