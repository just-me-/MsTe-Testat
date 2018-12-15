using AutoReservation.Common.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutoReservation.UI
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public AutoDto SelectedAuto { get; set; }

        public MainWindow()
        {
            InitFormData();
            this.DataContext = this;
            InitializeComponent();
        }

        private void InitFormData()
        {
            SelectedAuto = new AutoDto();
        }

        // Events - Auto Tab 
        private void AutoSaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            // Reset Form
            SelectedAuto = null;
        }
        private void AutoSelectedListBox_OnMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Console.WriteLine("Entered"); 
            var auto = listAutos.SelectedItem as AutoDto;
            SelectedAuto = auto;
        }


    }
        /*
        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (AvailableListBox.SelectedIndex < 0)
            {
                MessageBox.Show("Bitte etwas auswählen...");
                return;
            }
            var user = AvailableListBox.SelectedItem as UserInfo;
            if (!AddUserToSelection(user))
                return;
            AvailableListBox.SelectedIndex = -1;
        } */
        // helper

}
