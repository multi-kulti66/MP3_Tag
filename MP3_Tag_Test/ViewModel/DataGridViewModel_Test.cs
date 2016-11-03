// ///////////////////////////////////
// File: DataGridViewModel_Test.cs
// Last Change: 03.11.2016  20:50
// Author: Andre Multerer
// ///////////////////////////////////



namespace MP3_Tag_Test.ViewModel
{
    using GalaSoft.MvvmLight.Messaging;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MP3_Tag.Factory;
    using MP3_Tag.Model;
    using MP3_Tag.Properties;
    using MP3_Tag.Services;
    using MP3_Tag.ViewModel;



    [TestClass]
    public class DataGridViewModel_Test
    {
        #region  Static Fields and Constants

        private const string ExpectedArtist1 = "TestArtist1";
        private const string ExpectedArtist2 = "TestArtist2";

        private const string ExpectedTitle1 = "TestTitle1";
        private const string ExpectedTitle2 = "TestTitle2";

        #endregion



        #region Fields

        private readonly string ExpectedFilePath1 = MockMp3File.FolderPath + ExpectedArtist1 + " - " + ExpectedTitle1 + MockMp3File.Extension;
        private readonly string ExpectedFilePath2 = MockMp3File.FolderPath + ExpectedArtist2 + " - " + ExpectedTitle2 + MockMp3File.Extension;

        private IDialogService dialogServiceYes;
        private IDialogService dialogServiceNo;

        private IModelFactory modelFactory;

        private DataGridViewModel dataGridViewModel;

        #endregion



        #region Test Initialize

        [TestInitialize]
        public void TestInit()
        {
            this.dialogServiceYes = new DialogServiceYes();
            this.dialogServiceNo = new DialogServiceNo();

            this.modelFactory = new MockModelFactory();

            this.InitDataGridViewModel(this.dialogServiceYes);
            this.dataGridViewModel.AddWhenNew(this.ExpectedFilePath1);
        }

        #endregion



        #region Test Methods

        [TestMethod]
        public void CheckIfMp3SongWasAddedToList()
        {
            // Assert
            Assert.AreEqual(1, this.dataGridViewModel.Mp3SongViewModels.Count);
        }

        [TestMethod]
        public void DontAddExistingMp3SongToList()
        {
            // Act
            this.dataGridViewModel.AddWhenNew(this.ExpectedFilePath1);

            // Assert
            Assert.AreEqual(1, this.dataGridViewModel.Mp3SongViewModels.Count);
        }

        [TestMethod]
        public void CheckIfMp3SongWasRemovedFromList()
        {
            // Arrange
            Mp3SongViewModel mp3SongViewModel = this.dataGridViewModel.Mp3SongViewModels[0];

            // Act
            this.dataGridViewModel.Remove(mp3SongViewModel);

            // Assert
            Assert.AreEqual(0, this.dataGridViewModel.Mp3SongViewModels.Count);
        }

        [TestMethod]
        public void DontRemoveMp3SongThatIsNotInList()
        {
            // Arrange
            Mp3SongViewModel newMp3SongViewModel = new Mp3SongViewModel(new Mp3Song(new MockMp3File(), new MockFileModifier(false)), this.dialogServiceYes);

            // Act
            this.dataGridViewModel.Remove(newMp3SongViewModel);

            // Assert
            Assert.AreEqual(1, this.dataGridViewModel.Mp3SongViewModels.Count);
        }

        [TestMethod]
        public void DontRenameNotCheckedMp3Songs()
        {
            // Arrange
            const string NewTitleValue = "new title value";
            this.dataGridViewModel.AddWhenNew(this.ExpectedFilePath2);
            IMp3Song mp3Song = new Mp3Song(new MockMp3File(NewTitleValue), new MockFileModifier(false));

            // Act
            Messenger.Default.Send(new NotificationMessage<IMp3Song>(mp3Song, Resources.CommandName_Rename));

            // Assert
            Assert.AreEqual(ExpectedTitle1, this.dataGridViewModel.Mp3SongViewModels[0].Title);
            Assert.AreEqual(ExpectedTitle2, this.dataGridViewModel.Mp3SongViewModels[1].Title);
        }

        [TestMethod]
        public void RenameCheckedMp3Songs()
        {
            // Arrange
            const string NewTitleValue = "new title value";
            this.dataGridViewModel.AddWhenNew(this.ExpectedFilePath2);
            Mp3Tag mp3Tag = new Mp3Tag(NewTitleValue);

            foreach (Mp3SongViewModel mp3SongViewModel in this.dataGridViewModel.Mp3SongViewModels)
            {
                mp3SongViewModel.IsChecked = true;
            }

            // Act
            Messenger.Default.Send(new NotificationMessage<Mp3Tag>(mp3Tag, Resources.CommandName_Rename));

            // Assert
            foreach (Mp3SongViewModel mp3SongViewModel in this.dataGridViewModel.Mp3SongViewModels)
            {
                Assert.AreEqual(NewTitleValue, mp3SongViewModel.Title);
            }
        }

        [TestMethod]
        public void RemoveAllMp3SongsViaMessenger()
        {
            // Arrange
            this.dataGridViewModel.AddWhenNew(this.ExpectedFilePath2);

            // Act
            Messenger.Default.Send(new NotificationMessage<string>(Resources.CommandBroadcast_All, Resources.CommandName_Remove));

            // Assert
            Assert.AreEqual(0, this.dataGridViewModel.Mp3SongViewModels.Count);
        }

        [TestMethod]
        public void RemoveCheckedMp3SongsViaMessenger()
        {
            // Arrange
            this.dataGridViewModel.AddWhenNew(this.ExpectedFilePath2);
            this.dataGridViewModel.Mp3SongViewModels[0].IsChecked = true;

            // Act
            Messenger.Default.Send(new NotificationMessage<string>(Resources.CommandBroadcast_Checked, Resources.CommandName_Remove));

            // Assert
            Assert.AreEqual(1, this.dataGridViewModel.Mp3SongViewModels.Count);
        }

        #endregion



        #region Methods

        private void InitDataGridViewModel(IDialogService paramDialogService)
        {
            this.dataGridViewModel = new DataGridViewModel(paramDialogService, this.modelFactory);
        }

        #endregion
    }
}