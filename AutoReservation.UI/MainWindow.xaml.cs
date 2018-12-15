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
        public KundeDto SelectedKunde { get; set; }
        private MainViewModel mv { get; set; }

        public MainWindow()
        {
            mv = new MainViewModel(); 
            InitFormData();
            InitializeComponent();
        }

        private void InitFormData()
        {
            SelectedAuto = new AutoDto();
            SelectedKunde = new KundeDto();
        }

        // Events - Auto Tab 
        private void AutoSaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            // Reset Form
            loadFromAutoForm();
            mv.service.UpdateAuto(SelectedAuto);
        }
        private void AutoAddButton_OnClick(object sender, RoutedEventArgs e)
        {
            //SelectedAuto = null;
            SelectedAuto.Id = -1; // null
            loadFromAutoForm();
            mv.service.InsertAuto(SelectedAuto);
        }
        private void AutoDeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
        }
        private void AutoSelectedListBox_OnMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            SelectedAuto = listAutos.SelectedItem as AutoDto;
            clearAutoForm();
            loadIntoAutoForm();
        }
        // Helpers Auto
        private void loadFromAutoForm()
        {
            String a = AutoMarke.Text; 
            SelectedAuto.Marke = AutoMarke.Text;
            // 2do.. pasring to 
            //SelectedAuto.AutoKlasse = AutoKlasse.Text;
            SelectedAuto.Tagestarif = 12;// AutoTagestarif.Text;
            SelectedAuto.Basistarif = 22;// AutoBasistarif.Text;
        }
        private void loadIntoAutoForm()
        {
            AutoMarke.Text = SelectedAuto.Marke;
            AutoKlasse.Text = SelectedAuto.AutoKlasse.ToString();
            AutoTagestarif.Text = SelectedAuto.Tagestarif.ToString();
            AutoBasistarif.Text = SelectedAuto.Basistarif.ToString();
        }
        private void clearAutoForm()
        {
            AutoMarke.Text = "";
            AutoKlasse.Text = "";
            AutoTagestarif.Text = "";
            AutoBasistarif.Text = "";
        }

        // Kunden stuff
        private void KundeSelectedListBox_OnMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            SelectedKunde = listKunden.SelectedItem as KundeDto;
            clearKundeForm();
            loadIntoKundeForm();
        }
        private void loadIntoKundeForm()
        {
            KundeVorname.Text = SelectedKunde.Vorname;
            KundeNachname.Text = SelectedKunde.Nachname;
            KundeGeburtsdatum.Text = SelectedKunde.Geburtsdatum.ToString();
        }
        private void clearKundeForm()
        {
            KundeVorname.Text = "";
            KundeNachname.Text = "";
            KundeGeburtsdatum.Text = "";
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


}
