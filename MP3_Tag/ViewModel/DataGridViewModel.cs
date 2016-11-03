// ///////////////////////////////////
// File: DataGridViewModel.cs
// Last Change: 03.11.2016  20:50
// Author: Andre Multerer
// ///////////////////////////////////



namespace MP3_Tag.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Messaging;
    using MP3_Tag.Exception;
    using MP3_Tag.Factory;
    using MP3_Tag.Model;
    using MP3_Tag.Properties;
    using MP3_Tag.Services;



    public class DataGridViewModel : ViewModelBase
    {
        #region Fields

        private readonly IDialogService dialogService;
        private readonly IModelFactory modelFactory;

        private RelayCommand<object> _dropCommand;

        #endregion



        #region Constructors

        public DataGridViewModel(IDialogService paramDialogService, IModelFactory paramModelFactory)
        {
            this.dialogService = paramDialogService;
            this.modelFactory = paramModelFactory;
            this.Mp3SongViewModels = new ObservableCollection<Mp3SongViewModel>();

            Messenger.Default.Register<NotificationMessage<List<string>>>(this, this.AddWhenNew);
            Messenger.Default.Register<NotificationMessage<Mp3Tag>>(this, this.RenameCheckedElementsNotification);
            Messenger.Default.Register<NotificationMessage<Mp3SongViewModel>>(this, this.RemoveNotification);
            Messenger.Default.Register<NotificationMessage<string>>(this, this.HandleMp3SongCommandNotification);
        }

        #endregion



        #region Properties, Indexers

        public ObservableCollection<Mp3SongViewModel> Mp3SongViewModels { get; }

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

        #endregion



        #region Methods

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

        public void Remove(Mp3SongViewModel paramMp3SongViewModel)
        {
            if (this.Mp3SongViewModels.Remove(paramMp3SongViewModel))
            {
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
            for (int i = this.Mp3SongViewModels.Count(x => x.IsChecked) - 1; i >= 0; i--)
            {
                paramAction(this.Mp3SongViewModels[i]);
            }
        }

        private void RenameCheckedElementsNotification(NotificationMessage<Mp3Tag> notificationMessage)
        {
            this.IterateCheckedMp3SongsAndDoAction(mp3SongViewModel => mp3SongViewModel.Rename(notificationMessage.Content));
        }

        private void RemoveNotification(NotificationMessage<Mp3SongViewModel> notificationMessage)
        {
            this.Remove(notificationMessage.Content);
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