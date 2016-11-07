// ///////////////////////////////////
// File: CheckedElementsViewModel_Test.cs
// Last Change: 03.11.2016  20:50
// Author: Andre Multerer
// ///////////////////////////////////



namespace MP3_Tag_Test.ViewModel
{
    using GalaSoft.MvvmLight.Messaging;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MP3_Tag.Model;
    using MP3_Tag.Properties;
    using MP3_Tag.ViewModel;



    [TestClass]
    public class CheckedElementsViewModel_Test
    {
        #region Fields

        private CheckedElementsViewModel checkedElementsViewModel;

        #endregion



        #region Test Initialize

        [TestInitialize]
        public void TestInit()
        {
            this.checkedElementsViewModel = new CheckedElementsViewModel();
        }

        #endregion



        #region Test Methods

        [TestMethod]
        public void GetRenameNotificationMessage()
        {
            // Arrange
            string notificationMessage = null;
            Messenger.Default.Register<NotificationMessage<Mp3Tag>>(this, x => notificationMessage = x.Notification);

            // Act
            this.checkedElementsViewModel.Commands
                 .Find(x => x.CommandName == Resources.CommandName_Rename)
                 .RelayCommand.Execute(this);

            // Assert
            Assert.AreEqual(Resources.CommandName_Rename, notificationMessage);
        }

        [TestMethod]
        public void GetRenameValues()
        {
            // Arrange
            IMp3Tag mp3Tag = new Mp3Tag();

            const string ExpectedTitle = "Title value";
            const string ExpectedArtist = "Artist value";
            const string ExpectedAlbum = "Album value";

            this.checkedElementsViewModel.Title = ExpectedTitle;
            this.checkedElementsViewModel.Artist = ExpectedArtist;
            this.checkedElementsViewModel.Album = ExpectedAlbum;

            Messenger.Default.Register<NotificationMessage<Mp3Tag>>(this, x => mp3Tag = x.Content);

            // Act
            this.checkedElementsViewModel.Commands
                 .Find(x => x.CommandName == Resources.CommandName_Rename)
                 .RelayCommand.Execute(this);

            // Assert
            Assert.AreEqual(ExpectedTitle, mp3Tag.Title, "Title value was not transmitted");
            Assert.AreEqual(ExpectedArtist, mp3Tag.Artist, "Artist value was not transmitted");
            Assert.AreEqual(ExpectedAlbum, mp3Tag.Album, "Album value was not transmitted");
        }

        [TestMethod]
        public void GetSaveNotificationMessage()
        {
            // Arrange
            string notificationMessage = null;
            Messenger.Default.Register<NotificationMessage<string>>(this, x => notificationMessage = x.Notification);

            // Act
            this.checkedElementsViewModel.Commands
                 .Find(x => x.CommandName == Resources.CommandName_Save)
                 .RelayCommand.Execute(this);

            // Assert
            Assert.AreEqual(Resources.CommandName_Save, notificationMessage);
        }

        [TestMethod]
        public void GetUndoNotificationMessage()
        {
            // Arrange
            string notificationMessage = null;
            Messenger.Default.Register<NotificationMessage<string>>(this, x => notificationMessage = x.Notification);

            // Act
            this.checkedElementsViewModel.Commands
                 .Find(x => x.CommandName == Resources.CommandName_Undo)
                 .RelayCommand.Execute(this);

            // Assert
            Assert.AreEqual(Resources.CommandName_Undo, notificationMessage);
        }

        [TestMethod]
        public void GetRemoveNotificationMessage()
        {
            // Arrange
            string notificationMessage = null;
            Messenger.Default.Register<NotificationMessage<string>>(this, x => notificationMessage = x.Notification);

            // Act
            this.checkedElementsViewModel.Commands
                 .Find(x => x.CommandName == Resources.CommandName_Remove)
                 .RelayCommand.Execute(this);

            // Assert
            Assert.AreEqual(Resources.CommandName_Remove, notificationMessage);
        }

        [TestMethod]
        public void GetClearAlbumNotificationMessage()
        {
            // Arrange
            string notificationMessage = null;
            Messenger.Default.Register<NotificationMessage<string>>(this, x => notificationMessage = x.Notification);

            // Act
            this.checkedElementsViewModel.Commands
                 .Find(x => x.CommandName == Resources.CommandName_ClearAlbum)
                 .RelayCommand.Execute(this);

            // Assert
            Assert.AreEqual(Resources.CommandName_ClearAlbum, notificationMessage);
        }

        #endregion
    }
}