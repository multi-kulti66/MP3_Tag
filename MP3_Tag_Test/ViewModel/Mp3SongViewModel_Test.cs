// ///////////////////////////////////
// File: Mp3SongViewModel_Test.cs
// Last Change: 03.11.2016  20:50
// Author: Andre Multerer
// ///////////////////////////////////



namespace MP3_Tag_Test.ViewModel
{
    using System;
    using GalaSoft.MvvmLight.Messaging;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MP3_Tag.Model;
    using MP3_Tag.Properties;
    using MP3_Tag.Services;
    using MP3_Tag.ViewModel;



    [TestClass]
    public class Mp3SongViewModel_Test
    {
        #region  Static Fields and Constants

        private const string InitTitle = "test Title";
        private const string InitArtist = "test Artist";
        private const string InitAlbum = "test Album";

        #endregion



        #region Fields

        private IDialogService dialogServiceYes;
        private IDialogService dialogServiceNo;

        private Mp3SongViewModel mp3SongViewModel;

        #endregion



        #region Test Initialize

        [TestInitialize]
        public void TestInit()
        {
            this.dialogServiceYes = new DialogServiceYes();
            this.dialogServiceNo = new DialogServiceNo();

            this.InitMp3SongViewModel(false, this.dialogServiceYes);
        }

        #endregion



        #region Test Methods

        [TestMethod]
        public void CheckIfInstanceWillBeCreated()
        {
            // Assert
            Assert.IsNotNull(this.mp3SongViewModel);
        }

        [TestMethod]
        public void NotInEditModeAfterInitialization()
        {
            // Assert
            Assert.IsFalse(this.mp3SongViewModel.InEditMode);
        }

        [TestMethod]
        public void InEditModeAfterModifying()
        {
            // Act
            this.mp3SongViewModel.Title = "new value";

            // Assert
            Assert.IsTrue(this.mp3SongViewModel.InEditMode);
        }

        [TestMethod]
        public void GetRenamedValue()
        {
            // Assert
            const string ExpectedValue = "new name";

            // Act
            this.mp3SongViewModel.Title = ExpectedValue;

            // Assert
            Assert.AreEqual(ExpectedValue, this.mp3SongViewModel.Title);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ThrowExceptionWhenCommandNameDoesNotExist()
        {
            // Act
            this.mp3SongViewModel.GetCommand("invalid name").Execute(this);

            // throw exception
        }

        [TestMethod]
        public void CancelSaveCommandWhenInvalidProperties()
        {
            // Arrange
            string ExpectedValue = this.mp3SongViewModel.FilePath;
            this.mp3SongViewModel.Title = "#23asdfads";

            // Act
            this.mp3SongViewModel.Commands
                 .Find(x => x.CommandName == Resources.CommandName_Save)
                 .RelayCommand.Execute(this);

            // Assert
            Assert.AreEqual(ExpectedValue, this.mp3SongViewModel.FilePath);
        }

        [TestMethod]
        public void CancelSaveWhenFileExistsAndDialogAnswerIsNo()
        {
            // Arrange
            string ExpectedValue = this.mp3SongViewModel.FilePath;
            this.InitMp3SongViewModel(true, this.dialogServiceNo);
            this.mp3SongViewModel.Title = "new title";

            // Act
            this.mp3SongViewModel.Commands
                 .Find(x => x.CommandName == Resources.CommandName_Save)
                 .RelayCommand.Execute(this);

            // Assert
            Assert.AreEqual(ExpectedValue, this.mp3SongViewModel.FilePath);
        }

        [TestMethod]
        public void OverwriteFileWhenFileExistsAndDialogAnswerIsYes()
        {
            // Arrange
            const string NewValue = "new title string";
            string ExpectedValue = MockMp3File.FolderPath + InitArtist + " - " + NewValue + MockMp3File.Extension;
            this.InitMp3SongViewModel(true, this.dialogServiceYes);
            this.mp3SongViewModel.Title = NewValue;

            // Act
            this.mp3SongViewModel.Commands
                 .Find(x => x.CommandName == Resources.CommandName_Save)
                 .RelayCommand.Execute(this);

            // Assert
            Assert.AreEqual(ExpectedValue, this.mp3SongViewModel.FilePath);
        }

        [TestMethod]
        public void UndoChangesShouldRestoreInitState()
        {
            // Arrange
            this.mp3SongViewModel.Title = "new title";
            this.mp3SongViewModel.Artist = "new artist";
            this.mp3SongViewModel.Album = "new album";

            // Act
            this.mp3SongViewModel.Commands
                 .Find(x => x.CommandName == Resources.CommandName_Undo)
                 .RelayCommand.Execute(this);

            // Assert
            Assert.AreEqual(InitTitle, this.mp3SongViewModel.Title);
            Assert.AreEqual(InitArtist, this.mp3SongViewModel.Artist);
            Assert.AreEqual(InitAlbum, this.mp3SongViewModel.Album);
        }

        [TestMethod]
        public void ReceiveRemoveMessage()
        {
            // Assert
            string notificationMessage = null;
            Messenger.Default.Register<NotificationMessage<Mp3SongViewModel>>(this, x => notificationMessage = x.Notification);

            // Act
            this.mp3SongViewModel.Commands
                 .Find(x => x.CommandName == Resources.CommandName_Remove)
                 .RelayCommand.Execute(this);

            // Assert
            Assert.AreEqual(Resources.CommandName_Remove, notificationMessage);
        }

        [TestMethod]
        public void GetRemovedMp3SongFromMessage()
        {
            // Assert
            Mp3SongViewModel notificationValue = null;
            Messenger.Default.Register<NotificationMessage<Mp3SongViewModel>>(this, x => notificationValue = x.Content);

            // Act
            this.mp3SongViewModel.Commands
                 .Find(x => x.CommandName == Resources.CommandName_Remove)
                 .RelayCommand.Execute(this);

            // Assert
            Assert.AreEqual(this.mp3SongViewModel, notificationValue);
        }

        [TestMethod]
        public void GetNotSavedMp3SongFromMessageWhenDialogResultIsNo()
        {
            this.InitMp3SongViewModel(true, this.dialogServiceNo);
            Mp3SongViewModel notificationValue = null;
            Messenger.Default.Register<NotificationMessage<Mp3SongViewModel>>(this, x => notificationValue = x.Content);

            // Act
            this.mp3SongViewModel.Title = "newValue";

            this.mp3SongViewModel.Commands
                 .Find(x => x.CommandName == Resources.CommandName_Remove)
                 .RelayCommand.Execute(this);

            // Assert
            Assert.IsTrue(notificationValue.InEditMode);
        }

        [TestMethod]
        public void GetSavedMp3SongFromMessageWhenDialogResultIsYes()
        {
            this.InitMp3SongViewModel(true, this.dialogServiceYes);
            Mp3SongViewModel notificationValue = null;
            Messenger.Default.Register<NotificationMessage<Mp3SongViewModel>>(this, x => notificationValue = x.Content);

            // Act
            this.mp3SongViewModel.Title = "newValue";

            this.mp3SongViewModel.Commands
                 .Find(x => x.CommandName == Resources.CommandName_Remove)
                 .RelayCommand.Execute(this);

            // Assert
            Assert.IsFalse(notificationValue.InEditMode);
        }

        #endregion



        #region Methods

        private void InitMp3SongViewModel(bool paramFileExists, IDialogService paramDialogService)
        {
            MockMp3File mp3File = new MockMp3File(InitTitle, InitArtist, InitAlbum);
            MockFileModifier fileModifier = new MockFileModifier(paramFileExists);

            Mp3Song tempMp3Song = new Mp3Song(mp3File, fileModifier);
            this.mp3SongViewModel = new Mp3SongViewModel(tempMp3Song, paramDialogService);
        }

        #endregion
    }
}