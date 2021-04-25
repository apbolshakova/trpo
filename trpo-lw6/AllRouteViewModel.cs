using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using Microsoft.Win32;
using Newtonsoft.Json;

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
                new Route
                {
                    Name = "Мачаме",
                    Description =
                        "Маршрут Мачаме предоставляет живописные виды на гору Меру в Национальном Парке Аруши.",
                    Peak = "Пик Шира",
                    Height = 3962

                },
                new Route
                {
                    Name = "Умбве",
                    Description = "Умбве имеет заслуженную репутацию самого трудного маршрута на горе Килиманджаро.",
                    Peak = "Пик Ухуру",
                    Height = 5895
                },
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
                           Route Route = new Route();
                           Routes.Insert(0, Route);
                           SelectedRoute = Route;
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
                               Route Route = obj as Route;
                               if (Route != null)
                               {
                                   Routes.Remove(Route);
                               }
                           },
                           (obj) => Routes.Count > 0));
            }
        }

        private RelayCommand saveCommand;

        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                       (saveCommand = new RelayCommand(obj =>
                           {
                               Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                               dlg.Filter = "json|*.json|xml|*.xml";

                               Nullable<bool> result = dlg.ShowDialog();

                               if (result == true)
                               {
                                   string filename = dlg.FileName;
                                   switch (dlg.FilterIndex)
                                   {
                                       case 1:
                                           SaveJson(dlg.FileName);
                                           break;
                                       case 2:
                                           SaveXml(dlg.FileName);
                                           break;
                                   }
                               }
                           },
                           (obj) => Routes.Count > 0));
            }
        }

        private RelayCommand loadCommand;

        public RelayCommand LoadCommand
        {
            get
            {
                return loadCommand ??
                       (loadCommand = new RelayCommand(obj =>
                       {
                           Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                           dlg.Filter = "json|*.json|xml|*.xml";

                           Nullable<bool> result = dlg.ShowDialog();

                           if (result == true)
                           {
                               string filename = dlg.FileName;
                               switch (dlg.FilterIndex)
                               {
                                   case 1:
                                       LoadJson(dlg.FileName);
                                       break;
                                   case 2:
                                       LoadXml(dlg.FileName);
                                       break;
                               }
                           }
                       }));
            }
        }

        public void SaveJson(string file)
        {
            string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(Routes, Formatting.Indented);
            File.WriteAllText(file, jsonString);
        }

        public void SaveXml(string file)
        {
            XElement x = new XElement("Routes",
                from route in Routes
                select new XElement("Route",
                    new XElement("Name", route.Name),
                    new XElement("Description", route.Description),
                    new XElement("Peak", route.Peak),
                    new XElement("Height", route.Height)));
            string s = x.ToString();
            File.WriteAllText(file, s);
        }

        public void LoadJson(string file)
        {
            string jsonString = File.ReadAllText(file);
            Routes = Newtonsoft.Json.JsonConvert.DeserializeObject<ObservableCollection<Route>>(jsonString);
            OnPropertyChanged("Routes");
        }

        public void LoadXml(string file)
        {
            string xmlString = File.ReadAllText(file);
            XElement x = XElement.Parse(xmlString);
            Routes = new ObservableCollection<Route>(from e in x.Elements()
                select new Route
                {
                    Name = e.Element("Name")?.Value,
                    Description = e.Element("Description")?.Value,
                    Peak = e.Element("Peak")?.Value,
                    Height = int.Parse(e.Element("Height")?.Value ?? string.Empty)
                });
            OnPropertyChanged("Routes");
        }
    }
}