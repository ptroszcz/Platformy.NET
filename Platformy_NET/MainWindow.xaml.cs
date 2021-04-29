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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace Platformy_NET
{

    /*
     * Główna klasa MainWindow.
     * Zawiera wszystkie metody związane z intereakcją z użytkownikiem w głównym oknie aplikacji.
     */
    /// <summary>
    /// Główna klasa MainWindow.
    /// Zawiera wszystkie metody związane z intereakcją z użytkownikiem w głównym oknie aplikacji.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Obiekt używanej bazy danych klasy DataBaseUsage
        /// </summary>
        public DataBaseUsage data;

        /// <summary>
        /// Struktura zbierająca dynamicznie dane o obiektach klasy Song
        /// </summary>
        private ObservableCollection<Song> songlibrary;

        /// <summary>
        /// Obiekt klasy YTApi odpowiedzialny za komunikację z Api YouTube'a
        /// </summary>
        public YTApi YouTube;
        /// <summary>
        /// Kontstruktor głównego okna aplikacji.
        /// Inicjalizuje obiekty klas zwiazane z bazami danych, YouTube Api.
        /// Wczytuje z bazy listę zapisanych utworow - obiektów klasy Song przy uruchamianiu się.
        /// Wyświetla główne okno aplikacji.
        /// </summary>
        public MainWindow()

        {
            
            InitializeComponent();
            //Linijka niżej służy do ponownego zainicjowania bazy danych
            //System.Data.Entity.Database.SetInitializer<DataBase>(new System.Data.Entity.DropCreateDatabaseIfModelChanges<DataBase>());

           
            data = new DataBaseUsage();
            YouTube = new YTApi();
            
            songlibrary = data.getSongList();
            YourListBox.ItemsSource = songlibrary;
            



        }
        /// <summary>
        /// Metoda odpowiedzialna za reakcję aplikacji na wciśnięcie przycisku "Add"
        /// Otwiera okno AddingWindow, a po jego zamknięciu odświeża wyświetlaną listę utworów.
        /// </summary>
        /// <param name="sender">Odwołanie do przycisku, któy wywołał zdarzenie w tym przypadku przycisk z napisenm "Add"</param>
        /// <param name="e">Obiekt specyficzny dla zdarzenia</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddingWindow win2 = new AddingWindow();
            win2.ShowDialog();
            songlibrary = data.getSongList();
            YourListBox.ItemsSource = songlibrary;

        }
        /// <summary>
        /// Metoda odpowiedzialna za reakcję aplikacji na wciśnięcie przycisku "Remove"
        /// Jeżeli wybrano utwór na liście to wyszukuje go i wywołuję metodę bazy danych do jego usunięcia.
        /// </summary>
        /// <param name="sender">Odwołanie do przycisku, któy wywołał zdarzenie w tym przypadku przycisk z napisenm "Remove"</param>
        /// <param name="e">Obiekt specyficzny dla zdarzenia</param>
        private void Remove_Click(object sender, RoutedEventArgs e)
        {

            
            if (YourListBox.SelectedItem != null)
            {
                
                data.Remove((Song)YourListBox.SelectedItem);
                songlibrary = data.getSongList();
                YourListBox.ItemsSource = songlibrary;
            }
            
        }
        /// <summary>
        /// Metoda odpowiedzialna za reakcję aplikacji na wciśnięcie przycisku "Modify"
        /// Jeżeli wybrano utwór na liście otwiera okno ModifyWindow i po jego zamknięcię aktualizuje
        /// wyświetlaną listę utworów(obiektów klasy Song)
        /// </summary>
        /// <param name="sender">Odwołanie do przycisku, któy wywołał zdarzenie w tym przypadku przycisk z napisenm "Modify"</param>
        /// <param name="e">Obiekt specyficzny dla zdarzenia</param>
        private void Modify_Click(object sender, RoutedEventArgs e)
        {
            if (YourListBox.SelectedItem != null)
            {
                ModifyWindow win3 = new ModifyWindow();
                win3.ShowDialog();
                songlibrary = data.getSongList();
                YourListBox.ItemsSource = songlibrary;
            }
                

        }


        /// <summary>
        /// Metoda odpowiedzialna za reakcję aplikacji na wciśnięcie przycisku "Find On Youtube"
        /// Jeżeli wybrano utwór na liście wyświetla, dla którego obiektu poszukuje filmu na Youtube'ie.
        /// Wywołuje metodę Find_Video, która będzie się wykonywać asynchronicznie względem reszty kodu.
        /// </summary>
        /// <param name="sender">Odwołanie do przycisku, któy wywołał zdarzenie w tym przypadku przycisk z napisenm "Find on Youtube"</param>
        /// <param name="e">Obiekt specyficzny dla zdarzenia</param>
        private void Find_Click(object sender, RoutedEventArgs e)
        {
            if (YourListBox.SelectedItem != null)
            {
                SearchPhrase.Text = "Wynik wyszukiwania dla " + ((Song)YourListBox.SelectedItem).ToString();
                _ = Find_Video(((Song)YourListBox.SelectedItem).ToString());
            }
        }
        /// <summary>
        /// Metoda asynchroniczna wywołująca metodę klasy YTApi, która łączy się z Api Youtube'a w celu wyszukania podanej frazy z argumentu <paramref name="songName"/>.
        /// Przechwytuje błędy związane z niepoprawnym połączeniem się z zewnętrzym Api Youtube i wyświetla komunikat błędu.
        /// Po wyszukiwaniu sprawdza, czy dla podanego <paramref name="songName"/> uzyskano wynik. W przeciwnym wypadku wyświetli komunikat o nieudanym wyszukiwaniu.
        /// Sprawdza czy wyszukany link jest dłuższy niż 100 i jeśli tak wyświetla komunikat o podejrzanym linku, nie wyświetlając go.
        /// Jeżeli wyszukany link będzie prawidłowy zostanie on wyświetlony.
        /// </summary>
        /// <param name="songName">Nazwa frazy, która będzie wyszukiwana na Youtube'ie - domyślnie nazwa autora, utworu i albumu</param>
        /// <returns>Obiekt klasy Task - czyli pojedyncza operacja, która nie zwraca wartości i wykonuje sie asynchronicznie</returns>
        private async Task Find_Video(string songName)
        {
            try
            {
                await YouTube.Search(songName);
            }
            catch (AggregateException ex)
            {
                foreach (var e in ex.InnerExceptions)
                {
                    MessageBox.Show("Error: " + e.Message);
                }
            }
            if (YouTube.FoundVideos.Count <= 0)
            {
                Youtube_Links.Text = "Brak wyników dla wybranego utworu";
            }
            else if (YouTube.FoundVideos[0].Length > 100)
            {
                Youtube_Links.Text = "Dla wybranego utworu otrzymano podejrzanie długi link, dlatego nie został wyświetlony";
            }
            else
            {
                Youtube_Links.Text = YouTube.FoundVideos[0];
            }
            
        }
        /// <summary>
        /// Metoda odpowiedzialna za wyłączanie aplikacji po naciśnięciu przycisku Exit.
        /// </summary>
        /// <param name="sender">Odwołanie do przycisku, któy wywołał zdarzenie w tym przypadku przycisk z napisenm "Exit"</param>
        /// <param name="e">Obiekt specyficzny dla zdarzenia</param>
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
