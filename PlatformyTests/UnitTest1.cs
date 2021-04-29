using NUnit.Framework;
using Platformy_NET;
namespace PlatformyTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SongcontructortestwithAlbum()
        {
            string expected_artist = "Dead by April";
            string expected_title = "Last Goodbye";
            string expected_album = "Incomparable";
            Song song = new Song("Dead by April", "Last Goodbye", "Incomparable");
            Assert.AreEqual(song.Artist, expected_artist, "Artist must be the same");
            Assert.AreEqual(song.Title, expected_title, "Title must be the same");
            Assert.AreEqual(song.Album, expected_album, "Album must be the same");
        }
        [Test]
        public void SongcontructortestwithoutAlbum()
        {
            string expected_artist = "Dead by April";
            string expected_title = "Last Goodbye";
            Song song = new Song("Dead by April", "Last Goodbye");
            Assert.AreEqual(song.Artist, expected_artist, "Artist must be the same");
            Assert.AreEqual(song.Title, expected_title, "Title must be the same");
            Assert.AreEqual(song.Album, "None", "Album must be the same");
        }
        [Test]
        public void SongtostringwithAlbum()
        {
            string expected_string = "Skillet - Rise from Album: Rise";
            Song song = new Song("Skillet", "Rise", "Rise");
            Assert.AreEqual(song.ToString(), expected_string, "Returned strings must be the same");
        }
        [Test]
        public void SongtostringwithoutAlbum()
        {
            string expected_string = "Skillet - Rise";
            Song song = new Song("Skillet", "Rise");
            Assert.AreEqual(song.ToString(), expected_string, "Returned strings must be the same");
        }
        [Test]
        public void YTApicorrectcontructor()
        {
            YTApi Youtube = new YTApi();
            Assert.AreEqual(Youtube.Search_Word, "", "Search_Word should be empty after create YTApi object");
            Assert.AreEqual(Youtube.FoundVideos.Count, 0, "FoundVideos should be empty after create YTApi object");
        }
        [Test]
        public void YTApicorrectSearchWordchange()
        {
            YTApi Youtube = new YTApi();
            string expected_search_string = "Skillet - Rise";
            Youtube.Search("Skillet - Rise").Wait();
            Assert.AreEqual(Youtube.Search_Word, expected_search_string, "Correct change searchword is required");
        }
        [Test]
        public void YTApicorrectFoundVideo()
        {
            YTApi Youtube = new YTApi();
            int expected_length_of_found_videos = 3;
            string expected_link = "https://www.youtube.com/watch?v=b3jQ0tFqG_0";
            Youtube.Search("Skillet - Rise").Wait();
            Assert.AreEqual(Youtube.FoundVideos.Count, expected_length_of_found_videos, "For chosen string should find 3 videos");
            Assert.AreEqual(Youtube.FoundVideos[0], expected_link, "Chosen string should give expected string");
        }
        [Test]
        public void YTApinotcorrectFoundVideo()
        {
            YTApi Youtube = new YTApi();
            int expected_length_of_found_videos = 0;
            Youtube.Search("kacperro - arkoj").Wait();
            Assert.AreEqual(Youtube.FoundVideos.Count, expected_length_of_found_videos, "For chosen string should find 0 videos");
        }
        [Test]
        public void AddButtonCorrectAddwithAlbum()
        {
            ButtonTestsClass addbutton = new ButtonTestsClass();
            string result=addbutton.Add_Button_Click_Test("Skillet", "Rise", "Rise");
            string expected_string = "Skillet - Rise from Album: Rise";
            Assert.AreEqual(result, expected_string, "New Song object must be created with chosen artist,title and album ");

        }
        [Test]
        public void AddButtonCorrectAddwithoutAlbum()
        {
            ButtonTestsClass addbutton = new ButtonTestsClass();
            string result = addbutton.Add_Button_Click_Test("Skillet", "Rise","");
            string expected_string = "Skillet - Rise";
            Assert.AreEqual(result, expected_string, "New Song object must be created with chosen artist,title and album ");

        }
        [Test]
        public void AddButtonTooLongArtist()
        {
            ButtonTestsClass addbutton = new ButtonTestsClass();
            string result = addbutton.Add_Button_Click_Test("Skilletfromrisetonationtesttesttest", "Rise", "Rise");
            string expected_string = "Pole artysta nie mo¿e byæ d³u¿sze ni¿ 30 znaków";
            Assert.AreEqual(result, expected_string, "Max length of artist is 30");

        }
        [Test]
        public void AddButtonTooLongTitle()
        {
            ButtonTestsClass addbutton = new ButtonTestsClass();
            string result = addbutton.Add_Button_Click_Test("Skillet", "Risetesttesttesttesttesttesttesttesttest", "Rise");
            string expected_string = "Pole tytu³ nie mo¿e byæ d³u¿sze ni¿ 30 znaków";
            Assert.AreEqual(result, expected_string, "Max length of title is 30");

        }
        [Test]
        public void AddButtonTooLongAlbum()
        {
            ButtonTestsClass addbutton = new ButtonTestsClass();
            string result = addbutton.Add_Button_Click_Test("Skillet", "Rise", "Risetesttesttesttesttesttest");
            string expected_string = "Pole album nie mo¿e byæ d³u¿sze ni¿ 20 znaków";
            Assert.AreEqual(result, expected_string, "Max length of album is 20");

        }
        [Test]
        public void AddButtonArtistisEmpty()
        {
            ButtonTestsClass addbutton = new ButtonTestsClass();
            string result = addbutton.Add_Button_Click_Test("", "Rise", "Rise");
            string expected_string = "Pole artysta i tytu³ s¹ obowi¹zkowe";
            Assert.AreEqual(result, expected_string, "Artist can't be empty");

        }
        [Test]
        public void AddButtonTitletisEmpty()
        {
            ButtonTestsClass addbutton = new ButtonTestsClass();
            string result = addbutton.Add_Button_Click_Test("Skillet", "", "Rise");
            string expected_string = "Pole artysta i tytu³ s¹ obowi¹zkowe";
            Assert.AreEqual(result, expected_string, "Title can't be empty");

        }
        [Test]
        public void ModifyButtonCorrectChangeEverything()
        {
            ButtonTestsClass modifybutton = new ButtonTestsClass();
            Song song = new Song("Skillet", "Rise", "Rise");
            string result = modifybutton.Modify_Button_Click_Test(song,"TFK","War of Change","None");
            string expected_string = "TFK - War of Change";
            Assert.AreEqual(result, expected_string, "Artist, Title and Album should be modified correctly");

        }
        [Test]
        public void ModifyButtonCorrectChangeArist()
        {
            ButtonTestsClass modifybutton = new ButtonTestsClass();
            Song song = new Song("Skillet", "Rise", "Rise");
            string result = modifybutton.Modify_Button_Click_Test(song, "TFK","","");
            string expected_string = "TFK - Rise from Album: Rise";
            Assert.AreEqual(result, expected_string, "Artist should be modified correctly");

        }
        [Test]
        public void ModifyButtonCorrectChangeTitle()
        {
            ButtonTestsClass modifybutton = new ButtonTestsClass();
            Song song = new Song("Skillet", "Rise", "Rise");
            string result = modifybutton.Modify_Button_Click_Test(song, "", "Salvation", "");
            string expected_string = "Skillet - Salvation from Album: Rise";
            Assert.AreEqual(result, expected_string, "Title should be modified correctly");

        }
        [Test]
        public void ModifyButtonCorrectChangeAlbum()
        {
            ButtonTestsClass modifybutton = new ButtonTestsClass();
            Song song = new Song("Skillet", "Rise", "Rise");
            string result = modifybutton.Modify_Button_Click_Test(song, "", "", "NewOne");
            string expected_string = "Skillet - Rise from Album: NewOne";
            Assert.AreEqual(result, expected_string, "Album should be modified correctly");

        }
        [Test]
        public void ModifyButtonNothingChanged()
        {
            ButtonTestsClass modifybutton = new ButtonTestsClass();
            Song song = new Song("Skillet", "Rise", "Rise");
            string result = modifybutton.Modify_Button_Click_Test(song, "", "", "");
            string expected_string = "Skillet - Rise from Album: Rise";
            Assert.AreEqual(result, expected_string, "Nothing should be changed");

        }


    }
}