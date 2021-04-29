using System.Collections.ObjectModel;
using System.Data.Entity;


namespace Platformy_NET
{
/*
 * Klasa związana z komunikacją z bazą danych
 * Zawiera wszystkie metody związane z operacjami na bazie dancyh
 */
/// <summary>
/// Klasa związana z komunikacją z bazą danych
/// Zawiera wszystkie metody związane z operacjami na bazie dancyh
/// </summary>
	public class DataBaseUsage
	{
		/// <summary>
		/// Obiekt klasy DataBase przechowujący listę obiektów klasy Song, czyli listę utworów, które będą sie znajdować w bazie danych
		/// </summary>
		private DataBase _dataBase;
		/// <summary>
		/// Konstruktor domyślny klasy DataBaseUsage.
		/// Inicjuje obiekt klasy DataBase
		/// </summary>
		public DataBaseUsage()
		{
			_dataBase = new DataBase();
		}

		/// <summary>
		/// Metoda dodająca do bazy danych nowy utwór
		/// </summary>
		/// <param name="new_song">Obiekt klasy Song, który ma zostać dodany do bazy danych</param>
		public void Add(Song new_song)
		{
			_dataBase.Library.Add(new_song);
			_dataBase.SaveChanges();
		}

		/// <summary>
		/// Metoda usuwająca wybrany utwór(obiekt klasy Song) z bazy danych
		/// </summary>
		/// <param name="song_to_delete">Obiekt klasy Song, który ma zostać usunięty z bazy danych</param>
		public void Remove(Song song_to_delete)
		{
			_dataBase.Library.Remove(song_to_delete);
			_dataBase.SaveChanges();
		}
		/// <summary>
		/// Metoda modyfikująca istniejący już utwór w bazie danych
		/// </summary>
		/// <param name="song_to_modify">Obiekt klasy Song, który ma zostać zmodyfikowany w bazie danych</param>
		/// <param name="modified_song">Obiekt klasy Song, którego parametry Artist, Title oraz Album będą nowymi parametrami wskazanego <paramref name="song_to_modify"/></param>
		public void Modify(Song song_to_modify, Song modified_song)
		{
			_dataBase.Set<Song>().Attach(song_to_modify);
			song_to_modify.Artist = modified_song.Artist;
			song_to_modify.Title = modified_song.Title;
			song_to_modify.Album = modified_song.Album;
			_dataBase.SaveChanges();

		}

		/// <summary>
		/// Metoda czyszcząca cała zawartość bazy danych związaną z utworami
		/// </summary>
		public void clear()
		{
			_dataBase.Library.RemoveRange(_dataBase.Library);
		}
		/// <summary>
		/// Metoda zwracająca listę wszystkich utworów(obiektów klasy Song) znajdujących się w bazie danych.
		/// </summary>
		/// <returns>Lista obiektów klasy Song, znajdująca się w bazie danych</returns>
		public ObservableCollection<Song> getSongList()
		{
			ObservableCollection<Song> songlist = new ObservableCollection<Song>();
			foreach (var song in _dataBase.Library)
			{
				songlist.Add(song);
			}

			return songlist;
		}
	}
}
