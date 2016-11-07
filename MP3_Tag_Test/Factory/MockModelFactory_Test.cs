// ///////////////////////////////////
// File: MockModelFactory_Test.cs
// Last Change: 03.11.2016  20:50
// Author: Andre Multerer
// ///////////////////////////////////



namespace MP3_Tag_Test.Factory
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MP3_Tag.Factory;
    using MP3_Tag.Model;



    [TestClass]
    public class MockModelFactory_Test
    {
        #region Fields

        private MockModelFactory mockModelFactory;

        #endregion



        #region Test Initialize

        [TestInitialize]
        public void TestInit()
        {
            this.mockModelFactory = new MockModelFactory();
        }

        #endregion



        #region Test Methods

        [TestMethod]
        public void GetFileModifierWithFileAlreadyExistsIsFalse()
        {
            // Act
            IFileModifier mp3File = this.mockModelFactory.CreateFileModifier();

            // Assert
            Assert.IsFalse(mp3File.FileExists(null));
        }

        [TestMethod]
        public void GetFileModifierWithFileAlreadyExistsIsTrue()
        {
            // Act
            IFileModifier mp3File = this.mockModelFactory.CreateFileModifier(true);

            // Assert
            Assert.IsTrue(mp3File.FileExists(null));
        }

        [TestMethod]
        public void GetMp3FileWithDesiredTags()
        {
            // Arrange
            const string ExpectedArtist = "McCoffee";
            const string ExpectedTitle = "Title value";
            string ExpectedFilepath = MockMp3File.FolderPath + ExpectedArtist + " - " + ExpectedTitle + MockMp3File.Extension;

            // Act
            IMp3File mp3File = this.mockModelFactory.CreateMp3File(ExpectedFilepath);

            // Assert
            Assert.AreEqual(ExpectedArtist, mp3File.Artist);
            Assert.AreEqual(ExpectedTitle, mp3File.Title);
        }

        #endregion
    }
}