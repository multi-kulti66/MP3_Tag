// ///////////////////////////////////
// File: Mp3SongViewModel_Test.cs
// Last Change: 17.09.2016  20:42
// Author: Andre Multerer
// ///////////////////////////////////



namespace MP3_Tag_Test.ViewModel
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MP3_Tag.DataAccess;
    using MP3_Tag.Services;
    using MP3_Tag.ViewModel;
    using MP3_Tag_Test.Services;
    using Resources;
    using TagLib;
    using File = System.IO.File;



    [TestClass]
    public class Mp3SongViewModel_Test
    {
        #region Fields

        private IDialogService dialogServiceYes;
        private IDialogService dialogServiceNo;

        private Mp3SongViewModel mp3SongViewModel;
        private Mp3SongRepository mp3SongRepository;

        #endregion



        #region Test Initialize

        [TestInitialize]
        public void TestInit()
        {
            this.dialogServiceYes = new DialogServiceYes();
            this.dialogServiceNo = new DialogServiceNo();

            this.mp3SongRepository = new Mp3SongRepository();
            this.mp3SongViewModel = new Mp3SongViewModel(MediaStrings.Get_FilePath_Anna_Naklab__Supergirl, this.mp3SongRepository, this.dialogServiceYes);
        }

        #endregion



        #region Test Cleanup

        [TestCleanup]
        public void TestCleanup()
        {
            this.mp3SongViewModel.Title = MediaStrings.Get_Tags_Anna_Naklab__Supergirl.Title;
            this.mp3SongViewModel.Artist = MediaStrings.Get_Tags_Anna_Naklab__Supergirl.Artist;
            this.mp3SongViewModel.Album = MediaStrings.Get_Tags_Anna_Naklab__Supergirl.Album;
            this.mp3SongViewModel.SaveCommand.Execute(this);
        }

        #endregion



        #region Test Methods

        [TestMethod]
        public void CheckIfInstanceWasCreated()
        {
            // Assert
            Assert.IsNotNull(this.mp3SongViewModel);
        }

        [TestMethod]
        [ExpectedException(typeof(UnsupportedFormatException))]
        public void ThrowExceptionIfFilePathDoesNotExist()
        {
            // Act
            this.mp3SongViewModel = new Mp3SongViewModel(string.Empty, this.mp3SongRepository, this.dialogServiceYes);

            // Arrange
            // Throw exception
        }

        [TestMethod]
        public void Mp3SongNotIndicatedAsEditedAtInit()
        {
            // Assert
            Assert.IsFalse(this.mp3SongViewModel.IsEdited);
        }

        [TestMethod]
        public void CheckIfEditStateChangesToTrueAfterChangingProperty()
        {
            // Act
            this.mp3SongViewModel.Album = "TestName for Album";

            // Assert
            Assert.IsTrue(this.mp3SongViewModel.IsEdited);
        }

        [TestMethod]
        public void CancelSaveCommandWhenInvalidProperties()
        {
            // Arrange
            this.mp3SongViewModel.Title = "#23asdfads";

            // Act
            this.mp3SongViewModel.SaveCommand.Execute(this);

            // Assert
            Assert.AreEqual(MediaStrings.Get_FilePath_Anna_Naklab__Supergirl, this.mp3SongViewModel.FilePath);
        }

        [TestMethod]
        public void CancelSaveWhenFileExistsAndDialogAnswerIsNo()
        {
            // Arrange
            this.mp3SongViewModel = new Mp3SongViewModel(MediaStrings.Get_FilePath_Anna_Naklab__Supergirl, this.mp3SongRepository, this.dialogServiceNo);
            this.mp3SongViewModel.Title = MediaStrings.Get_Tags_AronChupa__Im_An_Albatraoz.Title;
            this.mp3SongViewModel.Artist = MediaStrings.Get_Tags_AronChupa__Im_An_Albatraoz.Artist;

            // Act
            this.mp3SongViewModel.SaveCommand.Execute(this);

            // Assert
            Assert.AreEqual(MediaStrings.Get_FilePath_Anna_Naklab__Supergirl, this.mp3SongViewModel.FilePath);
        }

        [TestMethod]
        public void OverwriteFileWhenFileExistsAndDialogAnswerIsYes()
        {
            // Arrange
            string ExpectedFilePath = MediaStrings.Get_FolderPath_Mp3_Songs + MediaStrings.Get_Tags_Avicii__You_Make_Me.Artist + " - " + MediaStrings.Get_Tags_Avicii__You_Make_Me.Title + "_copy.mp3";
            File.Copy(MediaStrings.Get_FilePath_Avicii__You_Make_Me, ExpectedFilePath);

            // Act
            this.mp3SongViewModel.Artist = MediaStrings.Get_Tags_Avicii__You_Make_Me.Artist;
            this.mp3SongViewModel.Title = MediaStrings.Get_Tags_Avicii__You_Make_Me.Title + "_copy";
            this.mp3SongViewModel.SaveCommand.Execute(this);

            // Assert
            Assert.AreEqual(ExpectedFilePath, this.mp3SongViewModel.FilePath);
        }

        [TestMethod]
        public void RemoveMp3FileWhenRemoveCommandWillBeExecuted()
        {
            // Arrange
            this.mp3SongRepository.AddMp3Song(MediaStrings.Get_FilePath_Anna_Naklab__Supergirl);

            // Act
            this.mp3SongViewModel.RemoveCommand.Execute(this);

            // Assert
            Assert.AreEqual(0, this.mp3SongRepository.Mp3Songs.Count);
        }

        [TestMethod]
        public void DoNotExecuteRemoveCommandWhenFileNotInRepository()
        {
            // Arrange
            this.mp3SongRepository.AddMp3Song(MediaStrings.Get_FilePath_AronChupa__Im_An_Albatraoz);

            // Act
            this.mp3SongViewModel.RemoveCommand.Execute(this);

            // Assert
            Assert.AreEqual(1, this.mp3SongRepository.Mp3Songs.Count);
        }

        [TestMethod]
        public void UndoChangesShouldRestoreInitState()
        {
            // Arrange
            this.mp3SongViewModel.Title = "TestTitle";
            this.mp3SongViewModel.Artist = "TestArtist";
            this.mp3SongViewModel.Album = "TestAlbum";

            // Act
            this.mp3SongViewModel.UndoChangesCommand.Execute(this);

            // Assert
            Assert.AreEqual(MediaStrings.Get_Tags_Anna_Naklab__Supergirl.Title, this.mp3SongViewModel.Title);
            Assert.AreEqual(MediaStrings.Get_Tags_Anna_Naklab__Supergirl.Artist, this.mp3SongViewModel.Artist);
            Assert.IsTrue(string.IsNullOrEmpty(this.mp3SongViewModel.Album));
        }

        #endregion
    }
}