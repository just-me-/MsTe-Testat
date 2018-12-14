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

            // 1st version; einfach 3 Fenster öffnen :P
            MainWindow = new MainWindow();
            MainWindow.DataContext = new KundeViewModel();
            MainWindow.Show();

            MainWindow = new MainWindow();
            MainWindow.DataContext = new AutoViewModel();
            MainWindow.Show();

            MainWindow = new MainWindow();
            MainWindow.DataContext = new ReservationViewModel();
            MainWindow.Show();
        }
    }
}
