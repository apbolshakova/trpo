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

//using System.ComponentModel;
//using System.Runtime.CompilerServices;

//namespace MVVM
//{
//    public class Phone : INotifyPropertyChanged
//    {
//        private string title;
//        private string company;
//        private int price;

//        public string Title
//        {
//            get { return title; }
//            set
//            {
//                title = value;
//                OnPropertyChanged("Title");
//            }
//        }
//        public string Company
//        {
//            get { return company; }
//            set
//            {
//                company = value;
//                OnPropertyChanged("Company");
//            }
//        }
//        public int Price
//        {
//            get { return price; }
//            set
//            {
//                price = value;
//                OnPropertyChanged("Price");
//            }
//        }

//        public event PropertyChangedEventHandler PropertyChanged;
//        public void OnPropertyChanged([CallerMemberName] string prop = "")
//        {
//            if (PropertyChanged != null)
//                PropertyChanged(this, new PropertyChangedEventArgs(prop));
//        }
//    }
//}