using System.Data.Entity;


namespace Platformy_NET
{
	/*
	* Klasa związana z listą wszystkich utworów, które będą przechowywane w bazie danych
	*/
	/// <summary>
	/// Klasa związana z listą wszystkich utworów, które będą przechowywane w bazie danych
	/// </summary>
	public class DataBase : DbContext
	{
		/// <summary>
		/// Reprezentacja wszystkich obiektów klasy Song, które będą przechowywane w bazie danych
		/// </summary>
		public DbSet<Song> Library { get; set; }
	}
}