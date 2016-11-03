// ///////////////////////////////////
// File: Mp3Song_Test.cs
// Last Change: 03.11.2016  20:50
// Author: Andre Multerer
// ///////////////////////////////////



namespace MP3_Tag_Test.Model
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MP3_Tag.Model;



    [TestClass]
    public class Mp3Song_Test
    {
        #region  Static Fields and Constants

        private const string InitTitle = "TestTitle";
        private const string InitArtist = "TestArtist";
        private const string InitAlbum = "TestAlbum";

        #endregion



        #region Fields

        private IMp3File mp3File;
        private IFileModifier fileModifier;
        private Mp3Song mp3Song;

        #endregion



        #region Test Initialize

        [TestInitialize]
        public void TestInit()
        {
            this.CreateMp3Song(false);
        }

        #endregion



        #region Test Methods

        [TestMethod]
        public void IsWishedFilePathCorrect()
        {
            // Arrange
            this.mp3Song.Title = "Andre";
            MockMp3File mockMp3File = (MockMp3File)this.mp3File;
            string expectedValue = mockMp3File.WishedFilePath;

            // Act
            string actualValue = this.mp3Song.WishedFilePath;

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void WishedFilePathEqualsFilePathReturnsFalse()
        {
            // Assert
            Assert.IsFalse(this.mp3Song.FileExistsAlready);
        }

        [TestMethod]
        public void ReturnFalseWhenWishedFilePathNotFilePathAndFileDoesNotExist()
        {
            // Arrange
            this.mp3Song.Title = "Name";

            // Assert
            Assert.IsFalse(this.mp3Song.FileExistsAlready);
        }

        [TestMethod]
        public void ReturnTrueWhenWishedFilePathNotFilePathAndFileDoesExist()
        {
            // Arrange
            this.CreateMp3Song(true);
            this.mp3Song.Title = "Name";

            // Assert
            Assert.IsTrue(this.mp3Song.FileExistsAlready);
        }

        [TestMethod]
        public void ChangeFilePathWhenSaved()
        {
            // Arrange
            const string ExpectedTitle = "Test Titleäü";
            this.mp3Song.Title = ExpectedTitle;

            MockMp3File mockMp3File = (MockMp3File)this.mp3File;
            string expectedValue = mockMp3File.WishedFilePath;

            // Act
            this.mp3Song.SaveAndRename();

            // Assert
            Assert.AreEqual(expectedValue, this.mp3Song.FilePath);
        }

        [TestMethod]
        public void ResetToInitValues()
        {
            // Arrange
            this.mp3Song.Title = "Awwer";
            this.mp3Song.Artist = "asdf";
            this.mp3Song.Album = "moanser";

            // Act
            this.mp3Song.Undo();

            // Assert
            Assert.AreEqual(InitTitle, this.mp3Song.Title);
            Assert.AreEqual(InitArtist, this.mp3Song.Artist);
            Assert.AreEqual(InitAlbum, this.mp3Song.Album);
        }

        #endregion



        #region Methods

        private void CreateMp3Song(bool paramFileExists)
        {
            this.mp3File = new MockMp3File(InitTitle, InitArtist, InitAlbum);
            this.fileModifier = new MockFileModifier(paramFileExists);

            this.mp3Song = new Mp3Song(this.mp3File, this.fileModifier);
        }

        #endregion
    }
}