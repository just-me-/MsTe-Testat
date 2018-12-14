using AutoReservation.Common.Interfaces;
using AutoReservation.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows;

namespace AutoReservation.UI
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        public MainViewModel MvModel { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MvModel = new MainViewModel();

            MainWindow = new MainWindow();
            MainWindow.DataContext = MvModel;
            MainWindow.Show();
        }
    }
}
