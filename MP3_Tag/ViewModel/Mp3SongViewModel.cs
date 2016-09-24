// ///////////////////////////////////
// File: Mp3SongViewModel.cs
// Last Change: 24.09.2016  19:32
// Author: Andre Multerer
// ///////////////////////////////////



namespace MP3_Tag.ViewModel
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading.Tasks;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using MP3_Tag.DataAccess;
    using MP3_Tag.Model;
    using MP3_Tag.Services;
    using Resources;



    public class Mp3SongViewModel : ViewModelBase, IDataErrorInfo
    {
        #region Fields

        private readonly IDialogService dialogService;
        private readonly Mp3SongRepository mp3SongRepository;
        private readonly Mp3Song mp3Song;

        private bool _isSelected;

        private RelayCommand saveCommand;
        private RelayCommand removeCommand;
        private RelayCommand undoChangesCommand;

        #endregion



        #region Constructors

        public Mp3SongViewModel(string paramFilePath, Mp3SongRepository paramMp3SongRepository, IDialogService paramDialogService)
        {
            this.dialogService = paramDialogService;
            this.mp3SongRepository = paramMp3SongRepository;
            this.mp3Song = this.mp3SongRepository.Mp3Songs.First(x => x.FilePath == paramFilePath);

            this._isSelected = false;
        }

        #endregion



        #region Properties, Indexers

        public bool IsEdited
        {
            get { return this.mp3Song.IsEdited; }
        }

        public string FilePath
        {
            get { return this.mp3Song.FilePath; }
        }

        public bool IsSelected
        {
            get { return this._isSelected; }
            set
            {
                if (this._isSelected == value)
                {
                    return;
                }

                this._isSelected = value;
                this.RaisePropertyChanged(() => this.IsSelected);
            }
        }

        public RelayCommand SaveCommand
        {
            get
            {
                if (this.saveCommand == null)
                {
                    this.saveCommand = new RelayCommand(this.Save, this.CanSave);
                }

                return this.saveCommand;
            }
        }

        public RelayCommand RemoveCommand
        {
            get
            {
                if (this.removeCommand == null)
                {
                    this.removeCommand = new RelayCommand(this.Remove, this.CanRemove);
                }

                return this.removeCommand;
            }
        }

        public RelayCommand UndoChangesCommand
        {
            get
            {
                if (this.undoChangesCommand == null)
                {
                    this.undoChangesCommand = new RelayCommand(this.UndoChanges);
                }

                return this.undoChangesCommand;
            }
        }

        /// <summary>
        ///     Gets or sets the album.
        /// </summary>
        public string Album
        {
            get { return this.mp3Song.Album; }

            set
            {
                if (this.mp3Song.Album != value)
                {
                    this.mp3Song.Album = value;
                    this.RaisePropertyChanged(() => this.Album);
                    this.RaisePropertyChanged(() => this.IsEdited);
                    this.SaveCommand.RaiseCanExecuteChanged();
                }
            }
        }

        /// <summary>
        ///     Gets or sets the artist.
        /// </summary>
        public string Artist
        {
            get { return this.mp3Song.Artist; }

            set
            {
                if (this.mp3Song.Artist != value)
                {
                    this.mp3Song.Artist = value;
                    this.RaisePropertyChanged(() => this.Artist);
                    this.RaisePropertyChanged(() => this.IsEdited);
                    this.SaveCommand.RaiseCanExecuteChanged();
                }
            }
        }

        /// <summary>
        ///     Gets or sets the title.
        /// </summary>
        public string Title
        {
            get { return this.mp3Song.Title; }

            set
            {
                if (this.mp3Song.Title != value)
                {
                    this.mp3Song.Title = value;
                    this.RaisePropertyChanged(() => this.Title);
                    this.RaisePropertyChanged(() => this.IsEdited);
                    this.SaveCommand.RaiseCanExecuteChanged();
                }
            }
        }

        #endregion



        #region IDataErrorInfo Members

        public string this[string propertyName]
        {
            get { return (this.mp3Song as IDataErrorInfo)[propertyName]; }
        }

        public string Error
        {
            get { return (this.mp3Song as IDataErrorInfo).Error; }
        }

        #endregion



        #region Methods

        private bool CanSave()
        {
            if (this.mp3Song.IsValid)
            {
                return true;
            }

            return false;
        }

        private void Save()
        {
            if (!this.mp3Song.IsValid)
            {
                throw new InvalidOperationException(ErrorStrings.Mp3SongViewModel_Exception_PropertiesOfModelNotValid);
            }

            if (this.mp3Song.FileExistsAlready)
            {
                Task<bool> replaceFile = this.dialogService.ShowDialogYesNo("Achtung!", "Die Datei existiert bereits. Wollen Sie diese ersetzen?");

                if (!replaceFile.Result)
                {
                    return;
                }
            }

            this.mp3Song.Save();
            this.RaisePropertyChanged(() => this.IsEdited);
            this.RaisePropertyChanged(() => this.FilePath);
        }

        private bool CanRemove()
        {
            if (this.mp3SongRepository.Mp3Songs.Count(x => x.FilePath == this.FilePath) == 0)
            {
                return false;
            }

            return true;
        }

        private void Remove()
        {
            this.mp3SongRepository.RemoveMp3Song(this.FilePath);
        }

        private void UndoChanges()
        {
            this.mp3Song.CancelEdit();
            this.RaisePropertyChanged(() => this.Title);
            this.RaisePropertyChanged(() => this.Artist);
            this.RaisePropertyChanged(() => this.Album);
            this.RaisePropertyChanged(() => this.IsEdited);
        }

        #endregion
    }
}