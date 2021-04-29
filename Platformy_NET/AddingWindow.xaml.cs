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
     * Klasa okna AddingWindow związanego z dodawaniem nowych utworów do biblioteki.
     * Zawiera wszystkie metody związane z integracją z użytkownikiem na oknie dodawania  nowego utworu.
     */
    /// <summary>
    /// Klasa okna Adding Window związanego z dodawaniem nowych utworów do biblioteki.
    /// Zawiera wszystkie metody związane z integracją z użytkownikiem na oknie dodawania  nowego utworu.
    /// </summary>
    public partial class AddingWindow : Window
    {
        /// <summary>
        /// Obiekt używanej bazy danych klasy DataBaseUsage
        /// </summary>
        public DataBaseUsage data;



        /// <summary>
        /// Konstruktor okna odpowiedzialnego za dodawanie nowych utworów do wyświetlanej listy.
        /// Inicjalizuje obiekt klasy zwiazany z bazą danych.
        /// Wyświetla okno dodawania nowego utworu.
        /// </summary>
        public AddingWindow()
        {
            InitializeComponent();
            data = new DataBaseUsage();
        }

        /// <summary>
        /// Metoda odpowiedzialna za reakcję aplikacji na wciśnięcie przycisku "Add" w oknie AddingWindow
        /// Odczytuje wpisane przez użytkownika nazwy wykonawcy, utworu oraz albumu.
        /// Nazwy wykonawcy i tytułu muszą zostać podane. Nazwa utworu jest opcjonalna.
        /// Nazwa wykonawcy nie może być dłuższa niż 30 znaków.
        /// Nazwa tytułu nie możę być dłuższa niż 30 znaków.
        /// Nazwa albumu nie możę być dłuższa niż 20 znaków.
        /// Jeżeli wszystkie warunki są spełnione tworzy nowy obiekt klasy Song, dodaje go do bazy danych i wyłącza okno AddingWindow.
        /// </summary>
        /// <param name="sender">Odwołanie do przycisku, któy wywołał zdarzenie w tym przypadku przycisk z napisem "Add"</param>
        /// <param name="e">Obiekt specyficzny dla zdarzenia</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string _artist = AddArtist.Text;
            string _title = AddTitle.Text;
            string _album = AddAlbum.Text;
            if (_artist == "" || _title == "")
                MessageBox.Show("Pole artysta i tytuł są obowiązkowe");
            else if (_artist.Length > 30){
                MessageBox.Show("Pole artysta nie może być dłuższe niż 30 znaków");
            }
            else if(_title.Length > 30)
            {
                MessageBox.Show("Pole tytuł nie może być dłuższe niż 30 znaków");
            }
            else if (_album.Length > 20)
            {
                MessageBox.Show("Pole album nie może być dłuższe niż 20 znaków");
            }
            else
            {
                if (_album == "")
                { 
                    Song addedSong = new Song(_artist, _title);
                    data.Add(addedSong);
                }
                else 
                {
                    Song addedSong = new Song(_artist, _title, _album);
                    data.Add(addedSong);
                }
                this.Close();
                    
            }
            
        }
        /// <summary>
        /// Metoda odpowiedzialna za wyłączanie okna AddingWindow po naciśnięciu przycisku Cancel
        /// </summary>
        /// <param name="sender">Odwołanie do przycisku, któy wywołał zdarzenie w tym przypadku przycisk z napisenm "Cancel"</param>
        /// <param name="e">Obiekt specyficzny dla zdarzenia</param>
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }




    }
}
