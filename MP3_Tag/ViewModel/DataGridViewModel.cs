// ///////////////////////////////////
// File: DataGridViewModel.cs
// Last Change: 07.11.2016  22:47
// Author: Andre Multerer
// ///////////////////////////////////



namespace MP3_Tag.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Messaging;
    using MP3_Tag.Exception;
    using MP3_Tag.Factory;
    using MP3_Tag.Model;
    using MP3_Tag.Properties;
    using MP3_Tag.Services;
    using MP3_Tag.Validation;



    public class DataGridViewModel : BindableValidator
    {
        #region Fields

        private readonly IDialogService dialogService;
        private readonly IModelFactory modelFactory;

        private List<Mp3SongViewModel> _selectedMp3SongViewModels;

        private RelayCommand<object> _dropCommand;
        private RelayCommand _checkAllMp3SongViewModelsCommand;
        private RelayCommand _uncheckAllMp3SongViewModelsCommand;
        private RelayCommand _checkOrUncheckSelectedElementsCommand;
        private RelayCommand _moveUpSelectedElementsCommand;
        private RelayCommand _moveDownSelectedElementsCommand;

        #endregion



        #region Constructors

        public DataGridViewModel(IDialogService paramDialogService, IModelFactory paramModelFactory)
        {
            this.dialogService = paramDialogService;
            this.modelFactory = paramModelFactory;

            this._selectedMp3SongViewModels = new List<Mp3SongViewModel>();
            this.Mp3SongViewModels = new ObservableCollection<Mp3SongViewModel>();
            this.Mp3SongViewModels.CollectionChanged += this.ContentCollectionChanged;

            Messenger.Default.Register<NotificationMessage<List<string>>>(this, this.AddWhenNew);
            Messenger.Default.Register<NotificationMessage<Mp3Tag>>(this, this.RenameCheckedElementsNotification);
            Messenger.Default.Register<NotificationMessage<Mp3SongViewModel>>(this, this.RemoveNotification);
            Messenger.Default.Register<NotificationMessage<string>>(this, this.HandleMp3SongCommandNotification);
        }

        #endregion



        #region Properties, Indexers

        public ObservableCollection<Mp3SongViewModel> Mp3SongViewModels { get; private set; }

        public List<Mp3SongViewModel> SelectedMp3SongViewModels
        {
            get { return this._selectedMp3SongViewModels; }
            set { this.SetProperty(newValue => this._selectedMp3SongViewModels = newValue, value); }
        }

        public RelayCommand<object> DropCommand
        {
            get
            {
                if (this._dropCommand == null)
                {
                    this._dropCommand = new RelayCommand<object>(this.Drop);
                }

                return this._dropCommand;
            }
        }

        public RelayCommand CheckAllMp3SongViewModelsCommand
        {
            get
            {
                if (this._checkAllMp3SongViewModelsCommand == null)
                {
                    this._checkAllMp3SongViewModelsCommand = new RelayCommand(this.CheckAllMp3Songs);
                }

                return this._checkAllMp3SongViewModelsCommand;
            }
        }

        public RelayCommand UncheckAllMp3SongViewModelsCommand
        {
            get
            {
                if (this._uncheckAllMp3SongViewModelsCommand == null)
                {
                    this._uncheckAllMp3SongViewModelsCommand = new RelayCommand(this.UncheckAllMp3Songs);
                }

                return this._uncheckAllMp3SongViewModelsCommand;
            }
        }

        public RelayCommand CheckOrUncheckSelectedElementsCommand
        {
            get
            {
                if (this._checkOrUncheckSelectedElementsCommand == null)
                {
                    this._checkOrUncheckSelectedElementsCommand = new RelayCommand(this.CheckOrUncheckSelectedElements);
                }

                return this._checkOrUncheckSelectedElementsCommand;
            }
        }

        public RelayCommand MoveUpSelectedElementsCommand
        {
            get
            {
                if (this._moveUpSelectedElementsCommand == null)
                {
                    this._moveUpSelectedElementsCommand = new RelayCommand(this.MoveUpSelectedElements);
                }

                return this._moveUpSelectedElementsCommand;
            }
        }

        public RelayCommand MoveDownSelectedElementsCommand
        {
            get
            {
                if (this._moveDownSelectedElementsCommand == null)
                {
                    this._moveDownSelectedElementsCommand = new RelayCommand(this.MoveDownSelectedElements);
                }

                return this._moveDownSelectedElementsCommand;
            }
        }

        #endregion



        #region Methods

        private void MoveUpSelectedElements()
        {
            for (int i = 1; i < this.Mp3SongViewModels.Count; i++)
            {
                int upperElement = i - 1;
                int lowerElement = i;

                if (!this.SelectedMp3SongViewModels.Contains(this.Mp3SongViewModels[upperElement]) && this.SelectedMp3SongViewModels.Contains(this.Mp3SongViewModels[lowerElement]))
                {
                    Mp3SongViewModel tempMp3SongViewModel = this.Mp3SongViewModels[upperElement];
                    this.Mp3SongViewModels.Remove(tempMp3SongViewModel);
                    this.Mp3SongViewModels.Insert(lowerElement, tempMp3SongViewModel);
                }
            }
        }

        private void MoveDownSelectedElements()
        {
            for (int i = this.Mp3SongViewModels.Count - 2; i >= 0; i--)
            {
                int upperElement = i;
                int lowerElement = i + 1;

                if (!this.SelectedMp3SongViewModels.Contains(this.Mp3SongViewModels[lowerElement]) && this.SelectedMp3SongViewModels.Contains(this.Mp3SongViewModels[upperElement]))
                {
                    Mp3SongViewModel tempMp3SongViewModel = this.Mp3SongViewModels[lowerElement];
                    this.Mp3SongViewModels.Remove(tempMp3SongViewModel);
                    this.Mp3SongViewModels.Insert(upperElement, tempMp3SongViewModel);
                }
            }
        }

        private void Drop(object obj)
        {
            var dataObject = obj as IDataObject;

            if (dataObject == null)
            {
                return;
            }

            string[] formats = (string[])dataObject.GetData(DataFormats.FileDrop);

            if (formats == null)
            {
                return;
            }

            foreach (string fileName in formats)
            {
                this.AddWhenNew(fileName);
            }
        }

        public void AddWhenNew(NotificationMessage<List<string>> paramNotificationMessage)
        {
            foreach (string filePath in paramNotificationMessage.Content)
            {
                this.AddWhenNew(filePath);
            }
        }

        public void AddWhenNew(string paramFilePath)
        {
            try
            {
                if (!this.IsNewMp3Song(paramFilePath))
                {
                    return;
                }

                Mp3Song mp3Song = new Mp3Song(this.modelFactory.CreateMp3File(paramFilePath), this.modelFactory.CreateFileModifier());
                Mp3SongViewModel mp3SongViewModel = new Mp3SongViewModel(mp3Song, this.dialogService);

                this.Mp3SongViewModels.Add(mp3SongViewModel);
            }
            catch (FileException)
            {
                this.dialogService.ShowMessage(Resources.DataGridVM_Exception_Add, string.Format(Resources.DataGridVM_Inner_Exception_Add, paramFilePath));
            }
        }

        private bool IsNewMp3Song(string paramFilePath)
        {
            if (this.Mp3SongViewModels.Any(mp3SongViewModel => mp3SongViewModel.FilePath == paramFilePath))
            {
                return false;
            }

            return true;
        }

        private void CheckAllMp3Songs()
        {
            this.IterateAllMp3SongsAndDoAction(mp3SongViewModel => mp3SongViewModel.IsChecked = true);
        }

        private void UncheckAllMp3Songs()
        {
            this.IterateAllMp3SongsAndDoAction(mp3SongViewModel => mp3SongViewModel.IsChecked = false);
        }

        private void CheckOrUncheckSelectedElements()
        {
            if (this.SelectedMp3SongViewModels.All(selectedElement => selectedElement.IsChecked))
            {
                this.IterateSelectedElementsAndDoAction(selectedElement => selectedElement.IsChecked = false);
            }
            else
            {
                this.IterateSelectedElementsAndDoAction(selectedElement => selectedElement.IsChecked = true);
            }
        }

        public void Remove(Mp3SongViewModel paramMp3SongViewModel)
        {
            if (this.Mp3SongViewModels.Remove(paramMp3SongViewModel))
            {
                this.RaisePropertyChanged(() => this.Mp3SongViewModels);
                return;
            }

            this.dialogService.ShowMessage(Resources.DataGridVM_Exception_Remove, string.Format(Resources.DataGridVM_Inner_Exception_Remove, paramMp3SongViewModel.FilePath));
        }

        private void IterateAllMp3SongsAndDoAction(Action<Mp3SongViewModel> paramAction)
        {
            for (int i = this.Mp3SongViewModels.Count - 1; i >= 0; i--)
            {
                paramAction(this.Mp3SongViewModels[i]);
            }
        }

        private void IterateCheckedMp3SongsAndDoAction(Action<Mp3SongViewModel> paramAction)
        {
            for (int i = this.Mp3SongViewModels.Count - 1; i >= 0; i--)
            {
                if (this.Mp3SongViewModels[i].IsChecked)
                {
                    paramAction(this.Mp3SongViewModels[i]);
                }
            }
        }

        private void IterateSelectedElementsAndDoAction(Action<Mp3SongViewModel> paramAction)
        {
            foreach (Mp3SongViewModel mp3SongViewModel in this.SelectedMp3SongViewModels)
            {
                paramAction(mp3SongViewModel);
            }
        }

        private void RenameCheckedElementsNotification(NotificationMessage<Mp3Tag> notificationMessage)
        {
            this.IterateCheckedMp3SongsAndDoAction(mp3SongViewModel => mp3SongViewModel.Rename(notificationMessage.Content));
        }

        private void RemoveNotification(NotificationMessage<Mp3SongViewModel> notificationMessage)
        {
            if (notificationMessage.Notification == Resources.CommandName_Remove)
            {
                this.Remove(notificationMessage.Content);
            }
        }

        private void HandleMp3SongCommandNotification(NotificationMessage<string> paramNotificationMessage)
        {
            if (paramNotificationMessage.Content == Resources.CommandBroadcast_All)
            {
                this.IterateAllMp3SongsAndDoAction(mp3SongVM => mp3SongVM.GetCommand(paramNotificationMessage.Notification).Execute(this));
            }
            else if (paramNotificationMessage.Content == Resources.CommandBroadcast_Checked)
            {
                this.IterateCheckedMp3SongsAndDoAction(mp3SongVM => mp3SongVM.GetCommand(paramNotificationMessage.Notification).Execute(this));
            }
        }

        public void ContentCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (Mp3SongViewModel item in e.OldItems)
                {
                    // Removed items
                    item.PropertyChanged -= this.ItemPropertyChanged;
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (Mp3SongViewModel item in e.NewItems)
                {
                    // Added items
                    item.PropertyChanged += this.ItemPropertyChanged;
                }
            }
        }

        public void ItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.RaisePropertyChanged(() => this.Mp3SongViewModels);
        }

        public override void Cleanup()
        {
            Messenger.Default.Unregister<NotificationMessage<List<string>>>(this);
            Messenger.Default.Unregister<NotificationMessage<Mp3Tag>>(this);
            Messenger.Default.Unregister<NotificationMessage<Mp3SongViewModel>>(this);
            Messenger.Default.Unregister<NotificationMessage<string>>(this);
            base.Cleanup();
        }

        #endregion
    }
}