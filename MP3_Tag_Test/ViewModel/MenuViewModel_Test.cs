// ///////////////////////////////////
// File: MenuViewModel_Test.cs
// Last Change: 03.11.2016  20:50
// Author: Andre Multerer
// ///////////////////////////////////



namespace MP3_Tag_Test.ViewModel
{
    using System.Collections.Generic;
    using GalaSoft.MvvmLight.Messaging;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MP3_Tag.Properties;
    using MP3_Tag.Services;
    using MP3_Tag.ViewModel;



    [TestClass]
    public class MenuViewModel_Test
    {
        #region Fields

        private IDialogService dialogServiceYes;
        private IDialogService dialogServiceNo;

        private MenuViewModel menuViewModel;

        #endregion



        #region Test Initialize

        [TestInitialize]
        public void TestInit()
        {
            this.dialogServiceYes = new DialogServiceYes();
            this.dialogServiceNo = new DialogServiceNo();

            this.InitMenuViewModel(this.dialogServiceNo);
        }

        #endregion



        #region Test Methods

        [TestMethod]
        public void ReceiveNoNotificationWhenAddingServiceWasCancelled()
        {
            // Arrange
            List<string> notificationValue = null;
            Messenger.Default.Register<NotificationMessage<List<string>>>(this, x => notificationValue = x.Content);

            // Act
            this.menuViewModel.Commands
                 .Find(x => x.CommandName == Resources.CommandName_Add)
                 .RelayCommand.Execute(this);

            // Assert
            Assert.IsNull(notificationValue);
        }

        [TestMethod]
        public void ReceiveListElementsThatShouldBeAdded()
        {
            // Arrange
            this.InitMenuViewModel(this.dialogServiceYes);
            List<string> notificationValue = null;
            Messenger.Default.Register<NotificationMessage<List<string>>>(this, x => notificationValue = x.Content);

            // Act
            this.menuViewModel.Commands
                 .Find(x => x.CommandName == Resources.CommandName_Add)
                 .RelayCommand.Execute(this);

            // Assert
            Assert.AreEqual(5, notificationValue.Count);
        }

        [TestMethod]
        public void ReceiveSaveAllMessage()
        {
            // Arrange
            string notificationBroadcast = null;
            string notificationMessage = null;
            Messenger.Default.Register<NotificationMessage<string>>(this, x => this.SetNotificationValues(x, ref notificationBroadcast, ref notificationMessage));

            // Act
            this.menuViewModel.Commands
                 .Find(x => x.CommandName == Resources.CommandName_Save)
                 .RelayCommand.Execute(this);

            // Assert
            Assert.AreEqual(Resources.CommandBroadcast_All, notificationBroadcast);
            Assert.AreEqual(Resources.CommandName_Save, notificationMessage);
        }

        [TestMethod]
        public void ReceiveUndoAllMessage()
        {
            // Arrange
            string notificationBroadcast = null;
            string notificationMessage = null;
            Messenger.Default.Register<NotificationMessage<string>>(this, x => this.SetNotificationValues(x, ref notificationBroadcast, ref notificationMessage));

            // Act
            this.menuViewModel.Commands
                 .Find(x => x.CommandName == Resources.CommandName_Undo)
                 .RelayCommand.Execute(this);

            // Assert
            Assert.AreEqual(Resources.CommandBroadcast_All, notificationBroadcast);
            Assert.AreEqual(Resources.CommandName_Undo, notificationMessage);
        }

        [TestMethod]
        public void ReceiveRemoveAllMessage()
        {
            // Arrange
            string notificationBroadcast = null;
            string notificationMessage = null;
            Messenger.Default.Register<NotificationMessage<string>>(this, x => this.SetNotificationValues(x, ref notificationBroadcast, ref notificationMessage));

            // Act
            this.menuViewModel.Commands
                 .Find(x => x.CommandName == Resources.CommandName_Remove)
                 .RelayCommand.Execute(this);

            // Assert
            Assert.AreEqual(Resources.CommandBroadcast_All, notificationBroadcast);
            Assert.AreEqual(Resources.CommandName_Remove, notificationMessage);
        }

        [TestMethod]
        public void ReceiveClearAlbumOfAllMessage()
        {
            // Arrange
            string notificationBroadcast = null;
            string notificationMessage = null;
            Messenger.Default.Register<NotificationMessage<string>>(this, x => this.SetNotificationValues(x, ref notificationBroadcast, ref notificationMessage));

            // Act
            this.menuViewModel.Commands
                 .Find(x => x.CommandName == Resources.CommandName_ClearAlbum)
                 .RelayCommand.Execute(this);

            // Assert
            Assert.AreEqual(Resources.CommandBroadcast_All, notificationBroadcast);
            Assert.AreEqual(Resources.CommandName_ClearAlbum, notificationMessage);
        }

        #endregion



        #region Methods

        private void InitMenuViewModel(IDialogService paramDialogService)
        {
            this.menuViewModel = new MenuViewModel(paramDialogService);
        }

        private void SetNotificationValues(NotificationMessage<string> paramNotificationMessage, ref string paramBroadcast, ref string paramMessage)
        {
            paramBroadcast = paramNotificationMessage.Content;
            paramMessage = paramNotificationMessage.Notification;
        }

        #endregion
    }
}