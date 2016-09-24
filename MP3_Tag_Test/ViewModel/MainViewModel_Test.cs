// ///////////////////////////////////
// File: MainViewModel_Test.cs
// Last Change: 17.09.2016  10:07
// Author: Andre Multerer
// ///////////////////////////////////



namespace MP3_Tag_Test.ViewModel
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MP3_Tag.Services;
    using MP3_Tag.ViewModel;
    using MP3_Tag_Test.Services;
    using Resources;



    [TestClass]
    public class MainViewModel_Test
    {
        #region Fields

        private IDialogService dialogServiceYes;
        private IDialogService dialogServiceNo;

        private MainViewModel mainViewModel;

        #endregion



        #region Test Initialize

        [TestInitialize]
        public void TestInit()
        {
            this.dialogServiceYes = new DialogServiceYes();
            this.dialogServiceNo = new DialogServiceNo();

            this.mainViewModel = new MainViewModel(this.dialogServiceYes);
        }

        #endregion



        #region Test Methods

        [TestMethod]
        public void AddCommandShouldInsertMp3ViewModelsToList()
        {
            // Act
            this.mainViewModel.AddMp3FilesCommand.Execute(this);

            // Assert
            Assert.AreEqual(MediaStrings.GetAllFilePaths.Length, this.mainViewModel.Mp3SongViewModels.Count);
        }

        [TestMethod]
        public void AddCommandShouldSkipAddingMp3FilesThatAreAlreadyInTheList()
        {
            // Act
            this.mainViewModel.AddMp3FilesCommand.Execute(this);
            this.mainViewModel.AddMp3FilesCommand.Execute(this);

            // Assert
            Assert.AreEqual(MediaStrings.GetAllFilePaths.Length, this.mainViewModel.Mp3SongViewModels.Count);
        }

        [TestMethod]
        public void AddCommandShouldNotAddAnyFilesWhenDialogServiceWasCanceled()
        {
            // Arrange
            this.mainViewModel = new MainViewModel(this.dialogServiceNo);

            // Act
            this.mainViewModel.AddMp3FilesCommand.Execute(this);

            // Assert
            Assert.AreEqual(0, this.mainViewModel.Mp3SongViewModels.Count);
        }

        [TestMethod]
        public void RemovingMp3FileFromRepositoryShouldRemoveFileFromList()
        {
            // Arrange
            this.mainViewModel.AddMp3FilesCommand.Execute(this);

            // Act
            this.mainViewModel.Mp3SongViewModels[0].RemoveCommand.Execute(this);

            // Assert
            Assert.AreEqual(MediaStrings.GetAllFilePaths.Length - 1, this.mainViewModel.Mp3SongViewModels.Count);
        }

        [TestMethod]
        public void ClearListCommandShouldRemoveAllElementsFromList()
        {
            // Arrange
            this.mainViewModel.AddMp3FilesCommand.Execute(this);

            // Act
            this.mainViewModel.ClearMp3SongListCommand.Execute(this);

            // Assert
            Assert.AreEqual(0, this.mainViewModel.Mp3SongViewModels.Count);
        }

        [TestMethod]
        public void SelectAllCommandShouldChangeSelectedStateOfAllElementsToTrue()
        {
            // Arrange
            this.mainViewModel.AddMp3FilesCommand.Execute(this);

            // Act
            this.mainViewModel.SelectAllMp3SongsCommand.Execute(this);

            // Assert
            Assert.AreEqual(MediaStrings.GetAllFilePaths.Length, this.mainViewModel.Mp3SongViewModels.Count(x => x.IsSelected == true));
        }

        [TestMethod]
        public void DeselectAllCommandShouldChangeSelectedStateOfAllElementsToFalse()
        {
            // Arrange
            this.mainViewModel.AddMp3FilesCommand.Execute(this);

            // Act
            this.mainViewModel.SelectAllMp3SongsCommand.Execute(this);
            this.mainViewModel.DeselectAllMp3SongsCommand.Execute(this);

            // Assert
            Assert.AreEqual(5, this.mainViewModel.Mp3SongViewModels.Count(x => x.IsSelected == false));
        }
        #endregion
    }
}