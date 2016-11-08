// ///////////////////////////////////
// File: MediaViewModel.cs
// Last Change: 07.11.2016  23:38
// Author: Andre Multerer
// ///////////////////////////////////



namespace MP3_Tag.ViewModel
{
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Messaging;
    using MP3_Tag.Properties;
    using MP3_Tag.Validation;



    public class MediaViewModel : BindableValidator
    {
        #region Fields

        private Mp3SongViewModel _selectedMp3SongViewModel;

        private RelayCommand _unloadSelectedMp3SongCommand;

        #endregion



        #region Constructors

        public MediaViewModel()
        {
            Messenger.Default.Register<NotificationMessage<Mp3SongViewModel>>(this, this.SetMp3SongViewModel);
        }

        #endregion



        #region Properties, Indexers

        private Mp3SongViewModel SelectedMp3SongViewModel
        {
            set
            {
                this.SetProperty(newValue => this._selectedMp3SongViewModel = newValue, value);
                this.RaisePropertyChanged(() => this.DisplayName);
                this.RaisePropertyChanged(() => this.FileName);
            }
        }

        public RelayCommand UnloadSelectedMp3SongCommand
        {
            get
            {
                if (this._unloadSelectedMp3SongCommand == null)
                {
                    this._unloadSelectedMp3SongCommand = new RelayCommand(this.UnloadSelectedMp3Song);
                }

                return this._unloadSelectedMp3SongCommand;
            }
        }

        public string DisplayName
        {
            get
            {
                if (this._selectedMp3SongViewModel == null)
                {
                    return string.Empty;
                }

                return this._selectedMp3SongViewModel.Artist + " - " + this._selectedMp3SongViewModel.Title;
            }
        }

        public string FileName
        {
            get
            {
                if (this._selectedMp3SongViewModel == null)
                {
                    return string.Empty;
                }

                return this._selectedMp3SongViewModel.FilePath;
            }
        }

        #endregion



        #region Methods

        private void SetMp3SongViewModel(NotificationMessage<Mp3SongViewModel> notificationMessage)
        {
            if (notificationMessage.Notification == Resources.CommandName_Load)
            {
                this.SelectedMp3SongViewModel = notificationMessage.Content;
            }
            else if ((notificationMessage.Notification == Resources.CommandName_Remove) && (this._selectedMp3SongViewModel != null) && (notificationMessage.Content.FilePath == this.FileName))
            {
                this.SelectedMp3SongViewModel = null;
            }
        }

        private void UnloadSelectedMp3Song()
        {
            this.SelectedMp3SongViewModel = null;
        }

        public override void Cleanup()
        {
            Messenger.Default.Unregister<NotificationMessage<Mp3SongViewModel>>(this);
            base.Cleanup();
        }

        #endregion
    }
}