using AutoReservation.Common.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using AutoReservation.Common.DataTransferObjects.Faults;

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



            //Polling
            DispatcherTimer Timer = new DispatcherTimer();

            // Send tick event each second:
            Timer.Interval = TimeSpan.FromSeconds(600);
            Timer.Tick += (sender, args) =>
            {
                //Update View
                listReservationen.ItemsSource = null;
                listReservationen.ItemsSource = Model.Reservation;

            };
            Timer.Start();


        }



        //////////////////////////////////////////////////////////////////////////////////////////////////////////

        // Reads from AutoForm and returns a new AutoDto
        private AutoDto loadFromAutoForm()
        {
            string marke = AutoMarke.Text;

            //Autoklasse, Standard by Default
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
                case "Standard":
                    klasse = AutoKlasse.Standard;
                    break;
                default:
                    klasse = AutoKlasse.Standard;
                    MessageBox.Show("Fehler beim Lesen der Autoklasse. Sie wurde auf einen Standardwert gesetzt", "Fehler",
                        MessageBoxButton.OK);
                    break;
            }

            //Tarife
            //Hinweis: Die Tariffelder akzeptieren nur Zahlen, es sind deshalb keine TryParse-Fehlschläge zu erwarten.
            bool success;
            int tagestarif = 0, basistarif = 0;

            success = int.TryParse(AutoTagestarif.Text, out tagestarif);
            if (!success)
            {
                throw new FormatException("Fehler beim lesen des Tagestarifs");

            }



            if (klasse == AutoKlasse.Luxusklasse)
            {
                success = int.TryParse(AutoBasistarif.Text, out basistarif);
                if (!success)
                {
                    throw new FormatException("Fehler beim lesen des Basistarifs");
                }
            }
            return new AutoDto
            {
                Marke = marke,
                AutoKlasse = klasse,
                Tagestarif = tagestarif,
                Basistarif = basistarif
            };

        }



        private void loadIntoAutoForm(AutoDto car)
        {
            AutoMarke.Text = car.Marke;
            AutoklasseTextbox.Text = car.AutoKlasse.ToString();
            AutoTagestarif.Text = car.Tagestarif.ToString();
            AutoBasistarif.Text = car.Basistarif.ToString();
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
            try
            {
                AutoDto autoToAdd = loadFromAutoForm();
                Model.service.InsertAuto(autoToAdd);
                Model.Autos.Add(autoToAdd);
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Fehler",  MessageBoxButton.OK);
            }
            
        }


        //Auto removen:
        private void AutoRemoveButton_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {

                AutoDto targetAutoToDelete = GetSelectedAuto();
                Model.service.DeleteAuto(targetAutoToDelete);
                Model.Autos.Remove(targetAutoToDelete);
            }

        }

        //Auto updaten:
        private void AutoSaveButton_OnClick(object sender, RoutedEventArgs e)
        {

            try
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
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Fehler", MessageBoxButton.OK);

            }
            
        }
    


        private void AutoSelectedListBox_OnMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            AutoDto selectedCar = GetSelectedAuto();
            loadIntoAutoForm(selectedCar);
        }




        /////////////////////////////////////////////Number and Dot only for number / date Fields


        private void OnKeyDown(object o, KeyEventArgs a)
        {

            Key pressedKey = a.Key;
            switch (pressedKey)
            {
                case Key.D1:
                case Key.D2:
                case Key.D3:
                case Key.D4:
                case Key.D5:
                case Key.D6:
                case Key.D7:
                case Key.D8:
                case Key.D9:
                case Key.D0:
                case Key.Back:
                case Key.OemPeriod:
                    break;

                default:
                    a.Handled = true;
                    break;
            }

        }



        ////////////////////////////////***KUNDE***//////////////////////////////////


        // Reads from From and returns a new Dto
        private KundeDto loadFromKundeForm()
        {
            string vorname = KundeVorname.Text;
            string nachname = KundeNachname.Text;
            string gebdatText = KundeGeburtsdatum.Text;
            DateTime gebdat;
            
            bool success = DateTime.TryParse(gebdatText, out gebdat);
            if (!success)
            {
                throw new FormatException("Konnte das Geburtsdatum nicht lesen");
            }
            

            return new KundeDto
            {
                Nachname = nachname,
                Vorname = vorname,
                Geburtsdatum = gebdat
            };

        }


        private void loadIntoKundeForm(KundeDto k)
        {
            KundeVorname.Text = k.Vorname;
            KundeNachname.Text = k.Nachname;
            KundeGeburtsdatum.Text = k.Geburtsdatum.ToShortDateString();
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
            try
            {
                KundeDto kundeToAdd = loadFromKundeForm();
                Model.service.InsertKunde(kundeToAdd);
                Model.Kunden.Add(kundeToAdd);
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Fehler", MessageBoxButton.OK);
            }
        }


        //Kunde removen:
        private void KundeRemoveButton_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                KundeDto targetKundeToDelete = GetSelectedKunde();
                Model.service.DeleteKunde(targetKundeToDelete);
                Model.Kunden.Remove(targetKundeToDelete);
            }

        }

        //Kunde updaten:
        private void KundeSaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
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
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Fehler", MessageBoxButton.OK);
            }


        }


        private void KundeSelectedListBox_OnMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            KundeDto selectedKunde = GetSelectedKunde();
            loadIntoKundeForm(selectedKunde);
        }







        ////////////////////////////*****RESER*****//////////////////////////////////////////////////////////////////////////////



        // Reads from From and returns a new Dto
        private ReservationDto loadFromReservationForm()
        {

            string vonDatText = ResVon.Text;
            string bisDatText = ResBis.Text;
            DateTime vonDat;
            DateTime bisDat;

            bool success = DateTime.TryParse(vonDatText, out vonDat);

            if (!success)
            {
                throw new FormatException("Meine Intelligenz reicht nicht aus, das VON-Datum zu lesen.");
            }

            success = DateTime.TryParse(bisDatText, out bisDat);
            if (!success)
            {
                throw new FormatException("BIS Datum.... isch en Chabis! Hmm, lecker Kabis.");
            }


            KundeDto k = ResKunde.SelectionBoxItem as KundeDto;
            AutoDto a = ResAuto.SelectionBoxItem as AutoDto;

            return new ReservationDto
            {
                Von = vonDat,
                Bis = bisDat,
                Kunde = k,
                Auto = a
            };

        }


        private void loadIntoReservationForm(ReservationDto r)
        {
            ResVon.Text = r.Von.ToShortDateString();
            ResBis.Text = r.Bis.ToShortDateString();
            ResKunde.SelectedValue = r.Kunde.Id;
            ResAuto.SelectedValue = r.Auto.Id;
        }

        //Checks which reservation is selected and returns the proper DTO
        private ReservationDto GetSelectedReservation()
        {
            int index = listReservationen.SelectedIndex;
            return Model.Reservation.ElementAt(index); //Die index von selected und Kunden ist gleich weil sie gebindet sind.

        }



        // adden:
        private void ReservationAddButton_OnClick(object sender, RoutedEventArgs e)
        {
            
            try
            {
                ReservationDto reservationToAdd = loadFromReservationForm();
                Model.service.InsertReservation(reservationToAdd);
                Model.Reservation.Add(reservationToAdd);
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Fehler", MessageBoxButton.OK);
            }
            catch (FaultException<AutoUnavailableFault> ex)
            {
                string msg = ex.Detail.Message;
                MessageBoxResult result = MessageBox.Show(msg,
                    "Fault!",
                    MessageBoxButton.OK);

            }
            catch (FaultException<InvalidDateRangeFault> ex)
            {
                string msg = ex.Detail.Message;
                MessageBoxResult result = MessageBox.Show(msg,
                    "Fault!",
                    MessageBoxButton.OK);

            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                MessageBoxResult result = MessageBox.Show(msg,
                    "Fault!",
                    MessageBoxButton.OK);
            }

        }


        // removen:
        private void ReservationRemoveButton_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                ReservationDto targetResToDelete = GetSelectedReservation();
                Model.service.DeleteReservation(targetResToDelete);
                Model.Reservation.Remove(targetResToDelete);
            }

        }

        // updaten:
        private void ReservationSaveButton_OnClick(object sender, RoutedEventArgs e)
        {

            //alte reservation löschen, und neue hinzufügen. ist simpler als update (Auto Unavailble Exception würde den Fall aber berücksichtigen!)
            try
            {
                ReservationDto targetReservationToUpdate = GetSelectedReservation();
                ReservationDto newReservation = loadFromReservationForm();
                Model.service.DeleteReservation(targetReservationToUpdate);
                Model.service.InsertReservation(newReservation);
                Model.Reservation.Remove(targetReservationToUpdate);
                Model.Reservation.Add(newReservation);
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Fehler", MessageBoxButton.OK);
            }
            catch (FaultException<AutoUnavailableFault> ex)
            {
                string msg = ex.Detail.Message;
                MessageBoxResult result = MessageBox.Show(msg,
                    "Fault!",
                    MessageBoxButton.OK);

            }
            catch (FaultException<InvalidDateRangeFault> ex)
            {
                string msg = ex.Detail.Message;
                MessageBoxResult result = MessageBox.Show(msg,
                    "Fault!",
                    MessageBoxButton.OK);

            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                MessageBoxResult result = MessageBox.Show(msg,
                    "Fault!",
                    MessageBoxButton.OK);
            }

            /*
            alter version mit update: For Reference. Please ignore.
            //totaler gurkencode again
            targetReservationToUpdate.Von = newReservation.Von;
            targetReservationToUpdate.Bis = newReservation.Bis;
            targetReservationToUpdate.Kunde = newReservation.Kunde;
            targetReservationToUpdate.Auto = newReservation.Auto;
            Model.service.UpdateReservation(targetReservationToUpdate)
             */


        }


        private void ReservationSelectedListBox_OnMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ReservationDto selectedRes = GetSelectedReservation();
            loadIntoReservationForm(selectedRes);
        }


        private void OnOnlyActiveReservationsCheckecd(object sender, RoutedEventArgs e)
        {
            DateTime now = DateTime.Now;
            IEnumerable<ReservationDto> targetEntries = Model.Reservation.Where(r => r.Von < now && r.Bis > now);
            Model.Reservation = new ObservableCollection<ReservationDto>(targetEntries);

            //Update View
            listReservationen.ItemsSource = null;
            listReservationen.ItemsSource = Model.Reservation;
            //Model.Reservation.CollectionChanged(this, new NotifyCollectionChangedEventArgs()...);
        }


        private void OnOnlyActiveReservationsUnCheckecd(object sender, RoutedEventArgs e)
        {
            Model.Reservation = new ObservableCollection<ReservationDto>(Model.service.GetAllReservationDtos());
            //Update View
            listReservationen.ItemsSource = null;
            listReservationen.ItemsSource = Model.Reservation;
        }






    }
}

