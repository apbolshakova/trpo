//using System;
//using System.Collections.ObjectModel;
//using System.ComponentModel;
//using System.Windows.Input;
//using System.Runtime.CompilerServices;

//namespace trpo_lw6
//{ 
//    public class AllRouteViewModel : INotifyPropertyChanged
//    {
//        private Route _selectedRoute;

//        public ObservableCollection<Route> Routes { get; set; }
//        public Route SelectedRoute
//        {
//            get => _selectedRoute;
//            set
//            {
//                _selectedRoute = value;
//                OnPropertyChanged("SelectedRoute");
//            }
//        }

//        public AllRouteViewModel()
//        {
//            Routes = new ObservableCollection<Route>
//            {
//                new Route {
//                    Name = "Мачаме",
//                    Description = "Маршрут Мачаме предоставляет живописные виды на гору Меру в Национальном Парке Аруши.",
//                    Peak = "Пик Шира",
//                    Height = 3962

//                },
//                new Route {
//                    Name = "Умбве",
//                    Description = "Умбве имеет заслуженную репутацию самого трудного маршрута на горе Килиманджаро.",
//                    Peak = "Пик Ухуру",
//                    Height = 5895},
//                new Route
//                {
//                    Name = "Марангу",
//                    Description = "Маршрут Марангу является самым популярным на Килиманджаро.",
//                    Peak = "Пик Мавензи",
//                    Height = 5149
//                },
//            };
//        }

//        public event PropertyChangedEventHandler PropertyChanged;
//        public void OnPropertyChanged(string prop = "")
//        {
//            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
//        }

//        private BaseCommand _addCommand;
//        public BaseCommand AddCommand
//        {
//            get
//            {
//                return _addCommand ??
//                       (_addCommand = new BaseCommand(obj =>
//                       {
//                           Route route = new Route();
//                           Routes.Insert(0, route);
//                           SelectedRoute = route;
//                       }));
//            }
//        }

//        private BaseCommand _delCommand;
//        public BaseCommand DelCommand
//        {
//            get
//            {
//                if (_delCommand != null)
//                    return _delCommand;

//                Action<object> Execute = o =>
//                {
//                    Route b = (Route)o;
//                    Routes.Remove(b);
//                };
//                Func<object, bool> CanExecute = o => Routes.Count > 0;
//                _delCommand = new BaseCommand(Execute, CanExecute);
//                return _delCommand;
//            }
//        }

//    }

//    public class BaseCommand : ICommand
//    {
//        private readonly Action<object> _execute;
//        private readonly Func<object, bool> _canExecute;

//        public event EventHandler CanExecuteChanged
//        {
//            add => CommandManager.RequerySuggested += value;
//            remove => CommandManager.RequerySuggested -= value;
//        }
//        public BaseCommand(Action<object> execute, Func<object, bool> canExecute = null)
//        {
//            _execute = execute;
//            _canExecute = canExecute;
//        }
//        public bool CanExecute(object parameter)
//        {
//            return _canExecute == null || _canExecute(parameter);
//        }
//        public void Execute(object parameter)
//        {
//            _execute(parameter);
//        }
//    }

//}

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace trpo_lw6
{
    public class AllRouteViewModel : INotifyPropertyChanged
    {
        private Route selectedRoute;

        public ObservableCollection<Route> Routes { get; set; }
        public Route SelectedRoute
        {
            get { return selectedRoute; }
            set
            {
                selectedRoute = value;
                OnPropertyChanged("SelectedRoute");
            }
        }

        public AllRouteViewModel()
        {
            Routes = new ObservableCollection<Route>
            {
                new Route {
                    Name = "Мачаме",
                    Description = "Маршрут Мачаме предоставляет живописные виды на гору Меру в Национальном Парке Аруши.",
                    Peak = "Пик Шира",
                    Height = 3962

                },
                new Route {
                    Name = "Умбве",
                    Description = "Умбве имеет заслуженную репутацию самого трудного маршрута на горе Килиманджаро.",
                    Peak = "Пик Ухуру",
                    Height = 5895},
                new Route
                {
                    Name = "Марангу",
                    Description = "Маршрут Марангу является самым популярным на Килиманджаро.",
                    Peak = "Пик Мавензи",
                    Height = 5149
                },
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        
        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                       (addCommand = new RelayCommand(obj =>
                       {
                           Route phone = new Route();
                           Routes.Insert(0, phone);
                           SelectedRoute = phone;
                       }));
            }
        }

        private RelayCommand deleteCommand;
        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                       (deleteCommand = new RelayCommand(obj =>
                           {
                               Route phone = obj as Route;
                               if (phone != null)
                               {
                                   Routes.Remove(phone);
                               }
                           },
                           (obj) => Routes.Count > 0));
            }
        }
    }
}