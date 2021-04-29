


using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;


namespace Platformy_NET
{
    /*
    * Klasa odpowiedzialna za łaczenie się z zewnętrznym serwerem Api YouTube.
    * Zawiera wszystkie metody związane z połączeniem z Api Youtube.
    */
    /// <summary>
    /// Klasa odpowiedzialna za łaczenie się z zewnętrznym serwerem Api YouTube.
    /// Zawiera wszystkie metody związane z połączeniem z Api Youtube.
    /// </summary>
    public class YTApi
    {
        /// <summary>
        /// Nazwa wyszukiwanego utworu.
        /// </summary>
        public string Search_Word { get; set; }
        /// <summary>
        /// Lista linków związanych z wyszukanym utworem.
        /// </summary>
        public List<string> FoundVideos { get; set; }

        /// <summary>
        /// Konstruktor domyślny klasy YTApi inicjujący listę na linki do znalezionych utworów oraz wyszukiwane słowo.
        /// </summary>
        public YTApi()
        {
            this.Search_Word = "";
            this.FoundVideos = new List<string>();
        }
        /// <summary>
        /// Asynchroniczna metoda ustawiająca wyszukiwany utwór na <paramref name="searchsong"/> oraz czyszcząca listę odnalezionych linków do utworów.
        /// Wywołuje metodą Run tej samej klasy, łączącej się z Api Youtube'a.
        /// </summary>
        /// <param name="searchsong">Nazwa wyszukiwanego utworu</param>
        /// <returns>Obiekt klasy Task - czyli pojedyncza operacja, która nie zwraca wartości i wykonuje sie asynchronicznie</returns>
        ///<exception cref="AggregateException">Błąd rzucony, gdy nie powiedzie się połączenie z Api Youtube'a</exception>
        [STAThread]
        public async Task Search(string searchsong)
        {
            this.Search_Word=searchsong;
            this.FoundVideos.Clear();
            try
            {
                await Run();
            }
            catch (AggregateException ex)
            {
                foreach (var e in ex.InnerExceptions)
                {
                    throw new AggregateException("Error: " + e.Message);
                }
            }

        }


        /// <summary>
        /// Asynchroniczna metoda łącząca się z Api Youtuba w celu wyszukania linku do wskazanej w polu Search_Word klasy YTApi, dla której zostanie wywołany, nazwy utworu.
        /// Wyszukane linki do utworów dodaje na listę FoundVideos klasy YTApi, dla której została wywołana
        /// Metoda wyszukuje 3 pierwsze wyszukiwania wskazanego słowa na Youtube, którym mogą być zarówno kanały, playlisty jak i filmy, ale przetwarza tylko filmy 
        /// </summary>
        /// <returns>Obiekt klasy Task - czyli pojedyncza operacja, która nie zwraca wartości i wykonuje sie asynchronicznie</returns>
        ///<exception cref="AggregateException">Błąd rzucony, gdy nie powiedzie się połączenie z Api Youtube'a</exception>
        private async Task Run()
        {

            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "Podany w mailu",
                ApplicationName = this.GetType().ToString()
            });

            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = Search_Word;
            searchListRequest.MaxResults = 3;

            var searchListResponse = await searchListRequest.ExecuteAsync();

            List<string> videos = new List<string>();

            foreach (var searchResult in searchListResponse.Items)
            {
                switch (searchResult.Id.Kind)
                {
                    case "youtube#video":
                        videos.Add(String.Format("{0}{1}", "https://www.youtube.com/watch?v=", searchResult.Id.VideoId));
                        break;
                }
            }
            this.FoundVideos=videos;
        }
    }
}
