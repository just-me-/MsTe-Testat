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

        private MainViewModel Model { get; }
        //Model hat:
        //Lioste mit Auto, Reservation und kunde
        //SWelected Auto und SelectedKunde



        public MainWindow()
        {
            Model = new MainViewModel();
            DataContext = Model;
            InitializeComponent();
        }



        //////////////////////////////////////////////////////////////////////////////////////////////////////////

        // Reads from AutoForm and returns a new AutoDto
        private AutoDto loadFromAutoForm()
        {
            string marke = AutoMarke.Text;

            string klasseText = AutoklasseTextbox.Text;

            AutoKlasse klasse;
            switch (klasseText)
            {
                case "Luxusklasse":
                    klasse = AutoKlasse.Luxusklasse;
                    break;
                case "Mittelklasse":
                    klasse = AutoKlasse.Mittelklasse;
                    break;
                default:
                    klasse = AutoKlasse.Standard;
                    break;
            }


            int tagestarif = int.Parse(AutoTagestarif.Text);
            int basistarif = int.Parse(AutoBasistarif.Text);

            return new AutoDto
            {
                Marke = marke,
                AutoKlasse = klasse,
                Tagestarif = tagestarif,
                Basistarif = basistarif
            };

        }

        //Checks which car is selected and returns the proper DTO
        private AutoDto GetSelectedAuto()
        {
            int index = listAutos.SelectedIndex;
            return Model.Autos.ElementAt(index); //Die index von selected und Autos ist gleich weil sie gebindet sind.

        }



        //Auto adden:
        private void AutoAddButton_OnClick(object sender, RoutedEventArgs e)
        {
            AutoDto autoToAdd = loadFromAutoForm();
            Model.service.InsertAuto(autoToAdd);
            Model.Autos.Add(autoToAdd);
        }


        //Auto removen:
        private void AutoRemoveButton_OnClick(object sender, RoutedEventArgs e)
        {
            AutoDto targetAutoToDelete = GetSelectedAuto();
            Model.service.DeleteAuto(targetAutoToDelete);
            Model.Autos.Remove(targetAutoToDelete);

        }

        //Auto updaten:
        private void AutoSaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            AutoDto targetAutoToUpdate = GetSelectedAuto();
            AutoDto newAuto = loadFromAutoForm();

            //totaler gurkencode, ist mir aber egal
            //Die Idee mit einem Member "selectedCar" war schon nicht schlecht, aber eig müsste man eh ein Binding machen...
            targetAutoToUpdate.AutoKlasse = newAuto.AutoKlasse;
            targetAutoToUpdate.Marke = newAuto.Marke;
            targetAutoToUpdate.Basistarif = newAuto.Basistarif;
            targetAutoToUpdate.Tagestarif = newAuto.Tagestarif;
            Model.service.UpdateAuto(targetAutoToUpdate);
            //Property Changed Dings... DTO müsste INotifyPropertyChanged implementieren oder sowas
            //Mache es hier the simple way. Wie gesagt, sehr gurkig.
            Model.Autos.Remove(targetAutoToUpdate);
            Model.Autos.Add(newAuto);
        }


        private void AutoSelectedListBox_OnMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            AutoDto selectedCar = GetSelectedAuto();
            loadIntoAutoForm(selectedCar);
        }






        private void loadIntoAutoForm(AutoDto car)
        {
            AutoMarke.Text = car.Marke;
            AutoklasseTextbox.Text = car.AutoKlasse.ToString();
            AutoTagestarif.Text = car.Tagestarif.ToString();
            AutoBasistarif.Text = car.Basistarif.ToString();
        }







        ////////////////////////////*****KUNDE*****//////////////////////////////////////////////////////////////////////////////

        // Reads from From and returns a new Dto
        private KundeDto loadFromKundeForm()
        {
            string vorname = KundeVorname.Text;
            string nachname = KundeNachname.Text;
            string gebdatText = KundeGeburtsdatum.Text;
            DateTime gebdat = DateTime.Parse(gebdatText);
            
            return new KundeDto
            {
                Nachname = nachname,
                Vorname =  vorname,
                Geburtsdatum = gebdat
            };

        }

        //Checks which car is selected and returns the proper DTO
        private KundeDto GetSelectedKunde()
        {
            int index = listKunden.SelectedIndex;
            return Model.Kunden.ElementAt(index); //Die index von selected und Kunden ist gleich weil sie gebindet sind.

        }



        //Kunde adden:
        private void KundeAddButton_OnClick(object sender, RoutedEventArgs e)
        {
            KundeDto kundeToAdd = loadFromKundeForm();
            Model.service.InsertKunde(kundeToAdd);
            Model.Kunden.Add(kundeToAdd);
        }


        //Kunde removen:
        private void KundeRemoveButton_OnClick(object sender, RoutedEventArgs e)
        {
            KundeDto targetKundeToDelete = GetSelectedKunde();
            Model.service.DeleteKunde(targetKundeToDelete);
            Model.Kunden.Remove(targetKundeToDelete);

        }

        //Kunde updaten:
        private void KundeSaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            KundeDto targetKundeToUpdate = GetSelectedKunde();
            KundeDto newKunde = loadFromKundeForm();

            //totaler gurkencode again
            targetKundeToUpdate.Nachname = newKunde.Nachname;
            targetKundeToUpdate.Vorname = newKunde.Vorname;
            targetKundeToUpdate.Geburtsdatum = newKunde.Geburtsdatum;
            Model.service.UpdateKunde(targetKundeToUpdate);

            //Property Changed Dings... DTO müsste INotifyPropertyChanged implementieren oder sowas
            //Mache es hier the simple way. Wie gesagt, sehr gurkig.
            Model.Kunden.Remove(targetKundeToUpdate);
            Model.Kunden.Add(newKunde);
        }


        private void KundeSelectedListBox_OnMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            KundeDto selectedKunde = GetSelectedKunde();
            loadIntoKundeForm(selectedKunde);
        }






        private void loadIntoKundeForm(KundeDto k)
        {
            KundeVorname.Text = k.Vorname;
            KundeNachname.Text = k.Nachname;
            KundeGeburtsdatum.Text = k.Geburtsdatum.ToShortDateString();
        }
    }
}

