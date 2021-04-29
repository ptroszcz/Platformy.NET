using System;



namespace Platformy_NET
{
	/*
	* Klasa przechowująca informację o jednym utworze.
	* Zawiera metodę związaną z przetwarzaniem obiektu tej Klasy na string.
	*/
	/// <summary>
	/// Klasa przechowująca informację o jednym utworze.
	/// Zawiera metodę związaną z przetwarzaniem obiektu tej Klasy na string.
	/// </summary>
	public class Song
	{
		/// <summary>
		/// ID danego utworu.
		/// Wykorzystywane przez bazę danych.
		/// </summary>
		public int ID { get; set; }
		/// <summary>
		/// Nazwa wykonawcy utworu.
		/// </summary>
		public string Artist { get; set; }
		/// <summary>
		/// Tytuł utworu.
		/// </summary>
		public string Title { get; set; }
		/// <summary>
		/// Nazwa albumu, z którego jest album
		/// </summary>
		public string Album { get; set; }
		/// <summary>
		/// Konstruktor domyślny bezparametryczny klasy Song.
		/// </summary>
		public Song() { }
		/// <summary>
		/// Konstruktor z parametrami klasy Song.
		/// </summary>
		/// <param name="Artist">Nazwa wykonwacy utworu</param>
		/// <param name="Title">Tytuł utworu</param>
		/// <param name="Album">Nazwa albumu - domyślnie "None", jeżeli nie zostanie podane</param>
		public Song(string Artist, string Title, string Album = "None")
		{
			this.Artist = Artist;
			this.Title = Title;
			this.Album = Album;

		}



		/// <summary>
		/// Wyświetla napis związany z poszczególnymi polami klasy.
		/// Postać: "Wykonawca" - "Tytuł" (from Album: "Album")
		/// Zawartość nawiasu wyświetla sie tylko jeżeli pole Album jest różne od "None"
		/// </summary>
		/// <returns></returns>
		override
		public string ToString()
		{
			string result = string.Empty;
			result += Artist + " - " + Title;
			if (Album != "None")
			{
				result += " from Album: " + Album;
			}
			return result;



		}
	}

}