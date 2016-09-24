// ///////////////////////////////////
// File: Mp3SongRepository_Test.cs
// Last Change: 16.09.2016  20:23
// Author: Andre Multerer
// ///////////////////////////////////



namespace MP3_Tag_Test.DataAccess
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MP3_Tag.DataAccess;
    using MP3_Tag.Model;
    using Resources;
    using TagLib;



    [TestClass]
    public class Mp3SongRepository_Test
    {
        #region Fields

        private readonly Mp3SongRepository mp3SongRepository;

        #endregion



        #region Constructors

        public Mp3SongRepository_Test()
        {
            this.mp3SongRepository = new Mp3SongRepository();
        }

        #endregion



        #region Test Methods

        [TestMethod]
        public void CheckIfMp3SongWasAdded()
        {
            // Act
            this.mp3SongRepository.AddMp3Song(MediaStrings.Get_FilePath_Anna_Naklab__Supergirl);

            // Assert
            Assert.AreEqual(1, this.mp3SongRepository.Mp3Songs.Count, "Adding the mp3 file didn't work.");
        }

        [TestMethod]
        public void CheckIfSecondMp3SongWasAdded()
        {
            // Act
            this.mp3SongRepository.AddMp3Song(MediaStrings.Get_FilePath_Anna_Naklab__Supergirl);
            this.mp3SongRepository.AddMp3Song(MediaStrings.Get_FilePath_AronChupa__Im_An_Albatraoz);

            // Assert
            Assert.AreEqual(2, this.mp3SongRepository.Mp3Songs.Count, "The second mp3 file was not added.");
        }

        [TestMethod]
        public void CheckIfContainedMp3FileWillNotBeAdded()
        {
            // Act
            this.mp3SongRepository.AddMp3Song(MediaStrings.Get_FilePath_Anna_Naklab__Supergirl);
            this.mp3SongRepository.AddMp3Song(MediaStrings.Get_FilePath_Anna_Naklab__Supergirl);

            // Assert
            Assert.AreEqual(1, this.mp3SongRepository.Mp3Songs.Count, "Added mp3 file even though it is already in list.");
        }

        [TestMethod]
        [ExpectedException(typeof(UnsupportedFormatException))]
        public void ThrowExceptionIfFilePathDoesNotExist()
        {
            // Act
            this.mp3SongRepository.AddMp3Song(string.Empty);

            // Assert
            // Throw exception
        }

        [TestMethod]
        public void CheckIfEventWillBeRaisedWhenFileAdded()
        {
            // Arrange
            Mp3Song tempMp3Song = null;

            this.mp3SongRepository.Mp3SongAdded += delegate(object sender, Mp3SongAddedEventArgs paramMp3EventArgs)
            {
                tempMp3Song = new Mp3Song(paramMp3EventArgs.FilePath);
            };

            // Act
            this.mp3SongRepository.AddMp3Song(MediaStrings.Get_FilePath_Anna_Naklab__Supergirl);

            // Assert
            Assert.AreEqual(MediaStrings.Get_FilePath_Anna_Naklab__Supergirl, tempMp3Song.FilePath);
        }

        [TestMethod]
        public void CheckIfMp3SongWillBeRemoved()
        {
            // Arrange
            this.mp3SongRepository.AddMp3Song(MediaStrings.Get_FilePath_Anna_Naklab__Supergirl);

            // Act
            this.mp3SongRepository.RemoveMp3Song(MediaStrings.Get_FilePath_Anna_Naklab__Supergirl);

            // Assert
            Assert.AreEqual(0, this.mp3SongRepository.Mp3Songs.Count, "Removing the mp3 file didn't work.");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ThrowExceptionIfDesiredRemovingMp3NotInList()
        {
            // Arrange
            this.mp3SongRepository.RemoveMp3Song("test");

            // Assert
            // Throw exception
        }

        [TestMethod]
        public void CheckIfEventWillBeRaisedWhenFileRemoved()
        {
            // Arrange
            this.mp3SongRepository.AddMp3Song(MediaStrings.Get_FilePath_Anna_Naklab__Supergirl);
            Mp3Song tempMp3Song = null;

            this.mp3SongRepository.Mp3SongRemoved += delegate(object sender, Mp3SongRemovedEventArgs paramMp3EventArgs)
            {
                tempMp3Song = new Mp3Song(paramMp3EventArgs.FilePath);
            };

            // Act
            this.mp3SongRepository.RemoveMp3Song(MediaStrings.Get_FilePath_Anna_Naklab__Supergirl);

            // Assert
            Assert.AreEqual(MediaStrings.Get_FilePath_Anna_Naklab__Supergirl, tempMp3Song.FilePath);
        }

        #endregion
    }
}