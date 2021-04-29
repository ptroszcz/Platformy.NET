using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformy_NET
{
    /*
     * Klasa stworzona w celu napisania testów jednostkowych dla metod związanych z naciskaniem odpowiednich przycisków.
     * Zawiera metody związane z przyciskiem dodającym nowy utwór w oknie AddingWindow oraz z przyciskiem modyfikującym dany utwór na liście w oknie ModifyWindow
     */
    /// <summary>
    /// Klasa stworzona w celu napisania testów jednostkowych dla metod związanych z naciskaniem odpowiednich przycisków.
    /// Zawiera metody związane z przyciskiem dodającym nowy utwór w oknie AddingWindow oraz z przyciskiem modyfikującym dany utwór na liście w oknie ModifyWindow
    /// </summary>
    public class ButtonTestsClass
    {
        /// <summary>
        /// Konstruktor domyślny klasy ButtonTestsClass
        /// </summary>
        public ButtonTestsClass() { }

        /// <summary>
        /// Metoda symulująca naciśnięcie przycisku dodania nowego utworu do biblioteki
        /// Działa analogicznie do oryginalnego wywołania przycisku, jednak zamiast wyświetlać komunikat, zwraca stringa odpowiadającego komunikatowi, który został by wyświetlony albo obiektowi klasy Song po wywołaniu metody ToString()
        /// </summary>
        /// <param name="_artist">Nazwa artysty, który ma zostać dodany do list</param>
        /// <param name="_title">Tytuł utwotu, który ma zostać dodany do listy</param>
        /// <param name="_album">Nazwa albumu, która ma zostać dodana do listy</param>
        /// <returns>Komunikat o nieporpawnych danych lub w przypadku poprawności wprowadzonych danych wynik metody ToString() klasy Song na nowo utworzonym obiekcie tej klasy</returns>
        public string Add_Button_Click_Test(string _artist, string _title, string _album)
        {
            if (_artist == "" || _title == "")
                return "Pole artysta i tytuł są obowiązkowe";
            else if (_artist.Length > 30)
            {
                return "Pole artysta nie może być dłuższe niż 30 znaków";
            }
            else if (_title.Length > 30)
            {
                return "Pole tytuł nie może być dłuższe niż 30 znaków";
            }
            else if (_album.Length > 20)
            {
                return "Pole album nie może być dłuższe niż 20 znaków";
            }
            else
            {
                if (_album == "")
                {
                    Song addedSong = new Song(_artist, _title);
                    return addedSong.ToString();
                }
                else
                {
                    Song addedSong = new Song(_artist, _title, _album);
                    return addedSong.ToString();
                }

            }
        }
        /// <summary>
        /// Metoda symulująca naciśnięcie przycisku modyfikacji danego utworu do biblioteki
        /// Działa analogicznie do oryginalnego wywołania przycisku, jednak zamiast wyświetlać komunikat, zwraca stringa odpowiadającego komunikatowi, który został by wyświetlony albo obiektu klasy Song po wywołaniu metody ToString()
        /// </summary>
        /// <param name="_artist">Nowa nazwa artysty</param>
        /// <param name="_title">Nowy tytuł artysty</param>
        /// <param name="_album">Nowa nazwa albumu danego utworu</param>
        /// <param name="before_modify">Obiekt klasy Song, który będzie modyfikowany</param>
        /// <returns>Komunikat o nieporpawnych danych lub w przypadku poprawności wprowadzonych danych wynik metody ToString() klasy Song na nowo utworzonym obiekcie tej klasy</returns>
        public string Modify_Button_Click_Test(Song before_modify,string _artist, string _title, string _album)
        {
            if (_artist == "")
            {
                _artist = before_modify.Artist;
            }

            if (_title == "")
            {
                _title = before_modify.Title;
            }

            if (_album == "")
            {
                _album = before_modify.Album;
            }
            if (_artist.Length > 30)
            {
                return "Pole artysta nie może być dłuższe niż 30 znaków";
            }
            else if (_title.Length > 30)
            {
                return "Pole tytuł nie może być dłuższe niż 30 znaków";
            }
            else if (_album.Length > 20)
            {
                return "Pole album nie może być dłuższe niż 20 znaków";
            }
            else
            {
                Song song = new Song(_artist, _title, _album);
                return song.ToString();
            }
        }

    }

}
