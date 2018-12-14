using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AutoReservation.UI
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            // clue app view model ... 2DO  AppViewModel = ... 

            MainWindow = new MainWindow();
            MainWindow.DataContext = new KundeViewModel();
            MainWindow.Show();
        }
    }
}
