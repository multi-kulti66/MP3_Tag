// ///////////////////////////////////
// File: MainViewModel.cs
// Last Change: 24.09.2016  14:53
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
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using MP3_Tag.DataAccess;
    using MP3_Tag.Services;
    using Resources;



    public class MainViewModel : ViewModelBase, IDataErrorInfo
    {
        #region  Static Fields and Constants

        private static readonly string[] ValidatedProperties = { nameof(ChangingTitle), nameof(ChangingArtist), nameof(ChangingAlbum) };

        #endregion



        #region Fields

        private readonly IDialogService _dialogService;

        private readonly Mp3SongRepository _mp3SongRepository;

        private string _changingTitle;
        private string _changingArtist;
        private string _changingAlbum;
        private bool? _allMp3SongsSelected;

        private ObservableCollection<Mp3SongViewModel> _mp3SongViewModels;

        private RelayCommand _addMp3FilesCommand;
        private RelayCommand _selectAllMp3SongsCommand;
        private RelayCommand _deselectAllMp3SongsCommand;
        private RelayCommand _saveAllMp3SongsCommand;
        private RelayCommand _changeSelectedMp3SongsCommand;
        private RelayCommand _saveSelectedMp3SongsCommand;
        private RelayCommand _undoSelectedMp3SongsCommand;
        private RelayCommand _deleteSelectedMp3SongsCommand;
        private RelayCommand _cancelAllMp3SongChangesCommand;
        private RelayCommand _deleteAllMp3SongsCommand;
        private RelayCommand _clearAlbumBySelectedMp3SongsCommand;

        #endregion



        #region Constructors

        public MainViewModel(IDialogService paramDialogService)
        {
            this._dialogService = paramDialogService;

            this._mp3SongRepository = new Mp3SongRepository();
            this._mp3SongViewModels = new ObservableCollection<Mp3SongViewModel>();

            // Wird nur aufgerufen, falls Item hinzugefügt oder entfernt wurde
            this.Mp3SongViewModels.CollectionChanged += this.OnCollectionChanged;

            // Werden aufgerufen, falls MP3 - Dateien zum Repository hinzugefügt oder entfernt werden
            this._mp3SongRepository.Mp3SongAdded += this.OnMp3SongAddedToRepository;
            this._mp3SongRepository.Mp3SongRemoved += this.OnMp3SongRemovedFromRepository;
        }

        #endregion



        #region Properties, Indexers

        public string ChangingTitle
        {
            get { return this._changingTitle; }
            set
            {
                if (this._changingTitle == value)
                {
                    return;
                }

                this._changingTitle = value;
                this.RaisePropertyChanged(() => this.ChangingTitle);
                this.ChangeSelectedMp3SongsCommand.RaiseCanExecuteChanged();
            }
        }

        public string ChangingArtist
        {
            get { return this._changingArtist; }
            set
            {
                if (this._changingArtist == value)
                {
                    return;
                }

                this._changingArtist = value;
                this.RaisePropertyChanged(() => this.ChangingArtist);
                this.ChangeSelectedMp3SongsCommand.RaiseCanExecuteChanged();
            }
        }

        public string ChangingAlbum
        {
            get { return this._changingAlbum; }
            set
            {
                if (this._changingAlbum == value)
                {
                    return;
                }

                this._changingAlbum = value;
                this.RaisePropertyChanged(() => this.ChangingAlbum);
                this.ChangeSelectedMp3SongsCommand.RaiseCanExecuteChanged();
            }
        }

        public bool? AllMp3SongsSelected
        {
            get { return this._allMp3SongsSelected; }
            set
            {
                if (this._allMp3SongsSelected == value)
                {
                    return;
                }

                this._allMp3SongsSelected = value;
                this.RaisePropertyChanged(() => this.AllMp3SongsSelected);
            }
        }

        public ObservableCollection<Mp3SongViewModel> Mp3SongViewModels
        {
            get { return this._mp3SongViewModels; }
            set
            {
                if (this._mp3SongViewModels == value)
                {
                    return;
                }

                this._mp3SongViewModels = value;
            }
        }

        public RelayCommand AddMp3FilesCommand
        {
            get
            {
                if (this._addMp3FilesCommand == null)
                {
                    this._addMp3FilesCommand = new RelayCommand(this.AddMp3Files);
                }

                return this._addMp3FilesCommand;
            }
        }

        public RelayCommand SelectAllMp3SongsCommand
        {
            get
            {
                if (this._selectAllMp3SongsCommand == null)
                {
                    this._selectAllMp3SongsCommand = new RelayCommand(this.SelectAllMp3Songs);
                }

                return this._selectAllMp3SongsCommand;
            }
        }

        public RelayCommand DeselectAllMp3SongsCommand
        {
            get
            {
                if (this._deselectAllMp3SongsCommand == null)
                {
                    this._deselectAllMp3SongsCommand = new RelayCommand(this.DeselectAllMp3Songs);
                }

                return this._deselectAllMp3SongsCommand;
            }
        }

        public RelayCommand SaveAllMp3SongsCommand
        {
            get
            {
                if (this._saveAllMp3SongsCommand == null)
                {
                    this._saveAllMp3SongsCommand = new RelayCommand(this.SaveAllMp3Songs);
                }

                return this._saveAllMp3SongsCommand;
            }
        }

        public RelayCommand ChangeSelectedMp3SongsCommand
        {
            get
            {
                if (this._changeSelectedMp3SongsCommand == null)
                {
                    this._changeSelectedMp3SongsCommand = new RelayCommand(this.ChangeSelectedMp3Songs, this.CanSaveChangingOfSelectedMp3Songs);
                }

                return this._changeSelectedMp3SongsCommand;
            }
        }

        public RelayCommand SaveSelectedMp3SongsCommand
        {
            get
            {
                if (this._saveSelectedMp3SongsCommand == null)
                {
                    this._saveSelectedMp3SongsCommand = new RelayCommand(this.SaveSelectedMp3Songs);
                }

                return this._saveSelectedMp3SongsCommand;
            }
        }

        public RelayCommand UndoSelectedMp3SongsCommand
        {
            get
            {
                if (this._undoSelectedMp3SongsCommand == null)
                {
                    this._undoSelectedMp3SongsCommand = new RelayCommand(this.UndoSelectedMp3Songs);
                }

                return this._undoSelectedMp3SongsCommand;
            }
        }

        public RelayCommand DeleteSelectedMp3SongsCommand
        {
            get
            {
                if (this._deleteSelectedMp3SongsCommand == null)
                {
                    this._deleteSelectedMp3SongsCommand = new RelayCommand(this.DeleteSelectedMp3Songs);
                }

                return this._deleteSelectedMp3SongsCommand;
            }
        }

        public RelayCommand CancelAllMp3SongChangesCommand
        {
            get
            {
                if (this._cancelAllMp3SongChangesCommand == null)
                {
                    this._cancelAllMp3SongChangesCommand = new RelayCommand(this.CancelAllMp3SongChanges);
                }

                return this._cancelAllMp3SongChangesCommand;
            }
        }

        public RelayCommand DeleteAllMp3SongsCommand
        {
            get
            {
                if (this._deleteAllMp3SongsCommand == null)
                {
                    this._deleteAllMp3SongsCommand = new RelayCommand(this.DeleteAllMp3Songs);
                }

                return this._deleteAllMp3SongsCommand;
            }
        }

        public RelayCommand ClearAlbumBySelectedMp3SongsCommand
        {
            get
            {
                if (this._clearAlbumBySelectedMp3SongsCommand == null)
                {
                    this._clearAlbumBySelectedMp3SongsCommand = new RelayCommand(this.ClearAlbumBySelectedMp3Songs);
                }

                return this._clearAlbumBySelectedMp3SongsCommand;
            }
        }

        private bool IsValid
        {
            get { return ValidatedProperties.All(property => this.GetValidationError(property) == null) && (!string.IsNullOrEmpty(this.ChangingTitle) || !string.IsNullOrEmpty(this.ChangingArtist) || !string.IsNullOrEmpty(this.ChangingAlbum)); }
        }

        #endregion



        #region IDataErrorInfo Members

        public string this[string propertyName]
        {
            get { return this.GetValidationError(propertyName); }
        }

        public string Error
        {
            get { return null; }
        }

        #endregion



        #region Methods

        /// <summary>
        ///     Wird aufgerufen, falls Dateien zur Liste hinzugefügt oder entfernt wurden.
        ///     Zu neu hinzugefügten Elementen wird das Event PropertyChanged gebunden.
        ///     Zu entfernten Elementen wird das Event PropertyChanged entfernt.
        /// </summary>
        /// <param name="sender">Mp3SongViewModel</param>
        /// <param name="e">NotifyCollectionChangedEventArgs</param>
        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (object newItem in e.NewItems)
                {
                    ((INotifyPropertyChanged)newItem).PropertyChanged += new PropertyChangedEventHandler(this.Collection_PropertyChanged);
                }
            }

            if (e.OldItems != null)
            {
                foreach (object oldItem in e.OldItems)
                {
                    ((INotifyPropertyChanged)oldItem).PropertyChanged -= new PropertyChangedEventHandler(this.Collection_PropertyChanged);
                }
            }
        }

        /// <summary>
        ///     Wird aufgerufen, falls sich ein Property in der Liste ändert.
        /// </summary>
        /// <param name="sender">Mp3SongViewModel</param>
        /// <param name="e">PropertyChangedEventArgs</param>
        private void Collection_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.RaisePropertyChanged(() => this.Mp3SongViewModels);
        }

        private void OnMp3SongAddedToRepository(object sender, Mp3SongAddedEventArgs e)
        {
            Mp3SongViewModel tempMp3SongViewModel = new Mp3SongViewModel(e.FilePath, this._mp3SongRepository, this._dialogService);
            this.Mp3SongViewModels.Add(tempMp3SongViewModel);
        }

        private void OnMp3SongRemovedFromRepository(object sender, Mp3SongRemovedEventArgs e)
        {
            this.Mp3SongViewModels.Remove(this.Mp3SongViewModels.First(x => x.FilePath == e.FilePath));
        }

        private void AddMp3Files()
        {
            List<string> tempFilePaths = this._dialogService.ShowFileDialog();

            if (tempFilePaths != null)
            {
                foreach (string filePath in tempFilePaths)
                {
                    this._mp3SongRepository.AddMp3Song(filePath);
                }
            }
        }

        private void SelectAllMp3Songs()
        {
            foreach (Mp3SongViewModel mp3SongViewModel in this.Mp3SongViewModels)
            {
                mp3SongViewModel.IsSelected = true;
            }
        }

        private void DeselectAllMp3Songs()
        {
            foreach (Mp3SongViewModel mp3SongViewModel in this.Mp3SongViewModels)
            {
                mp3SongViewModel.IsSelected = false;
            }
        }

        private void SaveAllMp3Songs()
        {
            foreach (Mp3SongViewModel mp3SongViewModel in this.Mp3SongViewModels)
            {
                mp3SongViewModel.SaveCommand.Execute(this);
            }
        }

        private void ChangeSelectedMp3Songs()
        {
            foreach (Mp3SongViewModel mp3SongViewModel in this.Mp3SongViewModels)
            {
                if (mp3SongViewModel.IsSelected)
                {
                    if (!string.IsNullOrEmpty(this.ChangingTitle))
                    {
                        mp3SongViewModel.Title = this.ChangingTitle;
                    }

                    if (!string.IsNullOrEmpty(this.ChangingArtist))
                    {
                        mp3SongViewModel.Artist = this.ChangingArtist;
                    }

                    if (!string.IsNullOrEmpty(this.ChangingAlbum))
                    {
                        mp3SongViewModel.Album = this.ChangingAlbum;
                    }
                }
            }
        }

        private void SaveSelectedMp3Songs()
        {
            foreach (Mp3SongViewModel mp3SongViewModel in this.Mp3SongViewModels)
            {
                if (mp3SongViewModel.IsSelected)
                {
                    mp3SongViewModel.SaveCommand.Execute(this);
                }
            }
        }

        private void UndoSelectedMp3Songs()
        {
            foreach (Mp3SongViewModel mp3SongViewModel in this.Mp3SongViewModels)
            {
                if (mp3SongViewModel.IsSelected)
                {
                    mp3SongViewModel.UndoChangesCommand.Execute(this);
                }
            }
        }

        private void DeleteSelectedMp3Songs()
        {
            for (int i = this.Mp3SongViewModels.Count; i > 0; i--)
            {
                if (this.Mp3SongViewModels[i - 1].IsSelected)
                {
                    this.Mp3SongViewModels[i - 1].RemoveCommand.Execute(this);
                }
            }

            this.RaisePropertyChanged(() => this.Mp3SongViewModels);
        }

        private void ClearAlbumBySelectedMp3Songs()
        {
            foreach (Mp3SongViewModel mp3SongViewModel in this.Mp3SongViewModels)
            {
                if (mp3SongViewModel.IsSelected)
                {
                    mp3SongViewModel.Album = string.Empty;
                    mp3SongViewModel.SaveCommand.Execute(this);
                }
            }
        }

        private bool CanSaveChangingOfSelectedMp3Songs()
        {
            if (this.IsValid)
            {
                return true;
            }

            return false;
        }

        private void CancelAllMp3SongChanges()
        {
            foreach (Mp3SongViewModel mp3SongViewModel in this.Mp3SongViewModels)
            {
                mp3SongViewModel.UndoChangesCommand.Execute(this);
            }
        }

        private void DeleteAllMp3Songs()
        {
            for (int i = this.Mp3SongViewModels.Count - 1; i >= 0; i--)
            {
                this.Mp3SongViewModels[i].RemoveCommand.Execute(this);
            }

            this.RaisePropertyChanged(() => this.Mp3SongViewModels);
        }

        private string GetValidationError(string propertyName)
        {
            string error;

            switch (propertyName)
            {
                case nameof(this.ChangingTitle):
                    error = this.ValidateChangingTitle();
                    break;
                case nameof(this.ChangingArtist):
                    error = this.ValidateChangingArtist();
                    break;

                case nameof(this.ChangingAlbum):
                    error = this.ValidateChangingAlbum();
                    break;
                default:
                    throw new InvalidOperationException(ErrorStrings.Mp3Song_Exception_PropertyNotExisting);
            }

            return error;
        }

        private bool IsValidString(string paramValue)
        {
            return paramValue.All(c => ((c >= '0') && (c <= '9')) || ((c >= 'A') && (c <= 'Z'))
                                       || (c == 'Ä') || (c == 'Ö') || (c == 'Ü')
                                       || (c == 'ä') || (c == 'ö') || (c == 'ü')
                                       || ((c >= 'a') && (c <= 'z'))
                                       || (c == '.') || (c == '_') || (c == ' ') || (c == '\'')
                                       || (c == '(') || (c == ')'));
        }

        private string ValidateChangingTitle()
        {
            if (string.IsNullOrEmpty(this.ChangingTitle))
            {
                return null;
            }

            if (!this.IsValidString(this.ChangingTitle))
            {
                return ErrorStrings.Mp3Song_Error_TitleValueFaulty;
            }

            return null;
        }

        private string ValidateChangingArtist()
        {
            if (string.IsNullOrEmpty(this.ChangingArtist))
            {
                return null;
            }

            if (!this.IsValidString(this.ChangingArtist))
            {
                return ErrorStrings.Mp3Song_Error_ArtistValueFaulty;
            }

            return null;
        }

        private string ValidateChangingAlbum()
        {
            if (string.IsNullOrEmpty(this.ChangingAlbum))
            {
                return null;
            }

            if (!this.IsValidString(this.ChangingAlbum))
            {
                return ErrorStrings.Mp3Song_Error_AlbumValueFaulty;
            }

            return null;
        }

        #endregion
    }
}