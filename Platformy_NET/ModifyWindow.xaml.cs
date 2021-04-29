using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.ComponentModel;

namespace Platformy_NET
{
    /*
     * Klasa okna ModifyWindow związanego z dodawaniem nowych utworów do biblioteki.
     * Zawiera wszystkie metody związane z integracją z użytkownikiem na oknie modyfikacji wybranego utworu.
     */
    /// <summary>
    /// Klasa okna ModifyWindow związanego z dodawaniem nowych utworów do biblioteki.
    /// Zawiera wszystkie metody związane z integracją z użytkownikiem na oknie modyfikacji wybranego utworu.
    /// </summary>
    public partial class ModifyWindow : Window
    {
        /// <summary>
        /// Obiekt używanej bazy danych klasy DataBaseUsage
        /// </summary>
        public DataBaseUsage data;


        /// <summary>
        /// Konstruktor okna odpowiedzialnego za modyfikację utworów na wyświetlanej liscie.
        /// Inicjalizuje obiekt klasy zwiazany z bazą danych.
        /// Wyświetla okno modyfikacji nowego utworu.
        /// W polach przeznaczonych na wpisanie nowych napisów ustawia domyślnie te odpowiadające wybranemu utworowi z listy.
        /// </summary>
        public ModifyWindow()
        {
            InitializeComponent();
            ModifyArtist.Text = ((Song)((MainWindow)Application.Current.MainWindow).YourListBox.SelectedItem).Artist;
            ModifyTitle.Text = ((Song)((MainWindow)Application.Current.MainWindow).YourListBox.SelectedItem).Title;
            if (((Song)((MainWindow)Application.Current.MainWindow).YourListBox.SelectedItem).Album != "None")
                ModifyAlbum.Text = ((Song)((MainWindow)Application.Current.MainWindow).YourListBox.SelectedItem).Album;
            data = new DataBaseUsage();
        }

        /// <summary>
        /// Metoda odpowiedzialna za reakcję aplikacji na wciśnięcie przycisku "Modify" w oknie ModifyWindow
        /// Odczytuje wpisane przez użytkownika nazwy wykonawcy, utworu oraz albumu.
        /// Nazwy wykonawcy i tytułu muszą zostać podane. Nazwa utworu jest opcjonalna.
        /// Nazwa wykonawcy nie może być dłuższa niż 30 znaków.
        /// Nazwa tytułu nie możę być dłuższa niż 30 znaków.
        /// Nazwa albumu nie możę być dłuższa niż 20 znaków.
        /// Jeżeli któraś z nazw jest pusta to odpowiednie dla tej nazwy pole jest niemodyfikowane.
        /// Jeżeli wszystkie warunki są spełnione wywołuje metodę klasy DataBaseUsage modyfikującą wybrany utwór w bazie danych i wyłącza okno ModifyWindow.
        /// </summary>
        /// <param name="sender">Odwołanie do przycisku, któy wywołał zdarzenie w tym przypadku przycisk z napisem "Modify"</param>
        /// <param name="e">Obiekt specyficzny dla zdarzenia</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //((MainWindow)Application.Current.MainWindow).YourListBox.SelectedItem
            string _artist = ModifyArtist.Text;
            string _title = ModifyTitle.Text;
            string _album = ModifyAlbum.Text;
            if (_artist == "")
            {
                _artist = ((Song)((MainWindow)Application.Current.MainWindow).YourListBox.SelectedItem).Artist;
            }

            if (_title == "")
            {
                _title = ((Song)((MainWindow)Application.Current.MainWindow).YourListBox.SelectedItem).Title;
            }

            if (_album == "")
            {
                _album = ((Song)((MainWindow)Application.Current.MainWindow).YourListBox.SelectedItem).Album;
            }
            if (_artist.Length > 30)
            {
                MessageBox.Show("Pole artysta nie może być dłuższe niż 30 znaków");
            }
            else if (_title.Length > 30)
            {
                MessageBox.Show("Pole tytuł nie może być dłuższe niż 30 znaków");
            }
            else if (_album.Length > 20)
            {
                MessageBox.Show("Pole album nie może być dłuższe niż 20 znaków");
            }
            else
            {
                Song song = new Song(_artist, _title, _album);
                data.Modify((Song)((MainWindow)Application.Current.MainWindow).YourListBox.SelectedItem, song);
                this.Close();
            }
        }
        /// <summary>
        /// Metoda odpowiedzialna za wyłączanie okna ModifyWindow po naciśnięciu przycisku Cancel
        /// </summary>
        /// <param name="sender">Odwołanie do przycisku, któy wywołał zdarzenie w tym przypadku przycisk z napisenm "Cancel"</param>
        /// <param name="e">Obiekt specyficzny dla zdarzenia</param>
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }




    }
}
