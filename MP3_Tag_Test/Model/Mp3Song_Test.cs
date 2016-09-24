// ///////////////////////////////////
// File: Mp3Song_Test.cs
// Last Change: 16.09.2016  19:07
// Author: Andre Multerer
// ///////////////////////////////////



namespace MP3_Tag_Test.Model
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MP3_Tag.Model;
    using Resources;
    using TagLib;



    [TestClass]
    public class Mp3Song_Test
    {
        #region Fields

        private Mp3Song mp3Song;

        #endregion



        #region Constructors

        public Mp3Song_Test()
        {
            this.mp3Song = new Mp3Song(MediaStrings.Get_FilePath_Anna_Naklab__Supergirl);
        }

        #endregion



        #region Test Cleanup

        [TestCleanup]
        public void TestCleanup()
        {
            this.mp3Song.Title = MediaStrings.Get_Tags_Anna_Naklab__Supergirl.Title;
            this.mp3Song.Artist = MediaStrings.Get_Tags_Anna_Naklab__Supergirl.Artist;
            this.mp3Song.Album = MediaStrings.Get_Tags_Anna_Naklab__Supergirl.Album;
            this.mp3Song.Save();
        }

        #endregion



        #region Test Methods

        [TestMethod]
        public void LoadFileIfPathExists()
        {
            // Assert
            Assert.IsInstanceOfType(this.mp3Song, typeof(Mp3Song));
        }

        [TestMethod]
        [ExpectedException(typeof(UnsupportedFormatException))]
        public void ThrowExceptionIfPathNotExists()
        {
            // Arrange
            this.mp3Song = new Mp3Song(string.Empty);

            // Assert
            // --> throw exception
        }

        [TestMethod]
        public void NotInEditModeAfterInit()
        {
            // Assert
            Assert.IsFalse(this.mp3Song.IsEdited, "Should detect that file was not changed.");
        }

        [TestMethod]
        public void FileInEditModeAfterTitleEdited()
        {
            // Act
            this.mp3Song.Title = "TestTitle";

            // Assert
            Assert.IsTrue(this.mp3Song.IsEdited, "Changed property should be detected.");
        }

        [TestMethod]
        public void FileInEditModeAfterArtistEdited()
        {
            // Act
            this.mp3Song.Artist = "TestArtist";

            // Assert
            Assert.IsTrue(this.mp3Song.IsEdited, "Changed property should be detected.");
        }

        [TestMethod]
        public void FileInEditModeAfterAlbumEdited()
        {
            // Act
            this.mp3Song.Album = "TestAlbum";

            // Assert
            Assert.IsTrue(this.mp3Song.IsEdited, "Changed property should be detected.");
        }

        [TestMethod]
        public void FileInEditModeAfterDoubleEditting()
        {
            // Act
            this.mp3Song.Album = "T";
            this.mp3Song.Album += "e";

            // Assert
            Assert.IsTrue(this.mp3Song.IsEdited, "Changed property should be detected.");
        }

        [TestMethod]
        public void CancelEdittingShouldSetFileToInitValues()
        {
            // Arrange
            this.mp3Song.Title = "TestTitle";

            // Act
            this.mp3Song.CancelEdit();

            // Assert
            Assert.AreEqual(MediaStrings.Get_Tags_Anna_Naklab__Supergirl.Title, this.mp3Song.Title);
        }

        [TestMethod]
        public void FileInNotEditedStateWhenPropertyHasInitValue()
        {
            // Act
            this.mp3Song.Title = "TestTitle";
            this.mp3Song.Title = MediaStrings.Get_Tags_Anna_Naklab__Supergirl.Title;

            // Asssert
            Assert.IsFalse(this.mp3Song.IsEdited);
        }

        [TestMethod]
        public void ValidMp3AfterInit()
        {
            // Assert
            Assert.AreEqual(this.mp3Song.IsValid, true, "Mp3 file should be valid.");
        }

        [TestMethod]
        public void CheckIfMp3SongIsInvalidWhenTitleEmpty()
        {
            // Act
            this.mp3Song.Title = string.Empty;

            // Assert
            Assert.AreEqual(this.mp3Song.IsValid, false, "Mp3 file should be invalid.");
        }

        [TestMethod]
        public void CheckIfMp3SongIsInvalidWhenArtistEmpty()
        {
            // Act
            this.mp3Song.Artist = string.Empty;

            // Assert
            Assert.AreEqual(this.mp3Song.IsValid, false, "Mp3 file should be invalid.");
        }

        [TestMethod]
        public void CheckIfMp3SongIsInvalidWhenTitleHasWrongStringFormat()
        {
            // Act
            this.mp3Song.Title = "The Title# ";

            // Assert
            Assert.AreEqual(this.mp3Song.IsValid, false, "Mp3 file should be invalid.");
        }

        [TestMethod]
        public void WishedFileNameEqualsOwnFileNameShouldNotBeSeenAsExisting()
        {
            // Assert
            Assert.IsFalse(this.mp3Song.FileExistsAlready);
        }

        [TestMethod]
        public void DetectThatWishedFileNameExistsAlready()
        {
            // Act
            this.mp3Song.Title = MediaStrings.Get_Tags_AronChupa__Im_An_Albatraoz.Title;
            this.mp3Song.Artist = MediaStrings.Get_Tags_AronChupa__Im_An_Albatraoz.Artist;

            // Assert
            Assert.IsTrue(this.mp3Song.FileExistsAlready, "Should detect that wished file name already exists in folder.");
        }

        [TestMethod]
        public void DetectNotExistingFile()
        {
            // Arrange
            string ExpectedTitle = MediaStrings.Get_Tags_AronChupa__Im_An_Albatraoz.Title;
            const string ExpectedArtist = "TestArtist";

            // Act
            this.mp3Song.Title = ExpectedTitle;
            this.mp3Song.Artist = ExpectedArtist;

            // Assert
            Assert.IsFalse(this.mp3Song.FileExistsAlready, "Should detect that wished file name not exists in folder.");
        }

        [TestMethod]
        public void GetNoValidationErrorForValidTitle()
        {
            // Act
            this.mp3Song.Title = "Ach ja der Titel";

            // Assert
            Assert.IsNull(this.mp3Song[nameof(this.mp3Song.Title)]);
        }

        [TestMethod]
        public void GetValidationErrorForEmptyTitle()
        {
            // Act
            this.mp3Song.Title = string.Empty;

            // Assert
            Assert.AreEqual(this.mp3Song[nameof(this.mp3Song.Title)], ErrorStrings.Mp3Song_Error_TitleValueMissing);
        }

        [TestMethod]
        public void GetValidationErrorForInvalidTitleValue()
        {
            // Act
            this.mp3Song.Title = "titleWith+#";

            // Assert
            Assert.AreEqual(this.mp3Song[nameof(this.mp3Song.Title)], ErrorStrings.Mp3Song_Error_TitleValueFaulty);
        }

        [TestMethod]
        public void GetNoValidationErrorForValidArtist()
        {
            // Act
            this.mp3Song.Artist = "Interpretenname";

            // Assert
            Assert.IsNull(this.mp3Song[nameof(this.mp3Song.Artist)]);
        }

        [TestMethod]
        public void GetValidationErrorForEmptyArtist()
        {
            // Act
            this.mp3Song.Artist = string.Empty;

            // Assert
            Assert.AreEqual(this.mp3Song[nameof(this.mp3Song.Artist)], ErrorStrings.Mp3Song_Error_ArtistValueMissing);
        }

        [TestMethod]
        public void GetValidationErrorForInvalidArtistValue()
        {
            // Act
            this.mp3Song.Artist = "lalauschi123 ?´";

            // Assert
            Assert.AreEqual(this.mp3Song[nameof(this.mp3Song.Artist)], ErrorStrings.Mp3Song_Error_ArtistValueFaulty);
        }

        [TestMethod]
        public void GetNoValidationErrorForEmptyAlbum()
        {
            // Act
            this.mp3Song.Album = null;

            // Assert
            Assert.IsNull(this.mp3Song[nameof(this.mp3Song.Album)]);
        }

        [TestMethod]
        public void GetValidationErrorForInvalidAlbumValue()
        {
            // Act
            this.mp3Song.Album = "Kein Komma , erlaubt";

            // Assert
            Assert.AreEqual(this.mp3Song[nameof(this.mp3Song.Album)], ErrorStrings.Mp3Song_Error_AlbumValueFaulty);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), ErrorStrings.Mp3Song_Exception_PropertyNotExisting)]
        public void GetExceptionForNotExistingProperty()
        {
            // Act
            string s = this.mp3Song["test"];
        }

        [TestMethod]
        public void CheckIfSameFilePathWillBeDeteced()
        {
            // Arrange
            Mp3Song tempMp3Song = new Mp3Song(MediaStrings.Get_FilePath_Anna_Naklab__Supergirl);

            // Act
            tempMp3Song.Title = "TestTitle";
            tempMp3Song.Artist = "TestArtist";

            // Assert
            Assert.IsTrue(this.mp3Song.HasSameFilePath(tempMp3Song));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ThrowExceptionIfFileShouldBeSavedWhenFileHasInvalidProperties()
        {
            // Act
            this.mp3Song.Title = "+The title";
            this.mp3Song.Save();

            // Assert
            // Throw exception
        }

        [TestMethod]
        public void ChangeFilePathIfValid()
        {
            // Arrange
            const string ExpectedTitle = "Test Titleäü";
            string ExpectedFilePath = MediaStrings.Get_FolderPath_Mp3_Songs + MediaStrings.Get_Tags_Anna_Naklab__Supergirl.Artist + " - " + ExpectedTitle + ".mp3";

            // Act
            this.mp3Song.Title = ExpectedTitle;
            this.mp3Song.Save();

            // Assert
            Assert.AreEqual(this.mp3Song.FilePath, ExpectedFilePath, "The mp3 file should have been renamed.");
        }

        #endregion
    }
}