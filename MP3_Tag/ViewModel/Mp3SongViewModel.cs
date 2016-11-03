// ///////////////////////////////////
// File: Mp3SongViewModel.cs
// Last Change: 03.11.2016  20:50
// Author: Andre Multerer
// ///////////////////////////////////



namespace MP3_Tag.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Messaging;
    using MP3_Tag.Model;
    using MP3_Tag.Properties;
    using MP3_Tag.Services;
    using MP3_Tag.Validation;



    public class Mp3SongViewModel : BindableValidator
    {
        #region Fields

        private readonly IDialogService dialogService;
        private readonly Mp3Song mp3Song;

        private bool _isChecked;

        #endregion



        #region Constructors

        public Mp3SongViewModel(Mp3Song paramMp3Song, IDialogService paramDialogService)
        {
            this.dialogService = paramDialogService;
            this.mp3Song = paramMp3Song;

            this.InitCommands();

            this._isChecked = false;
        }

        #endregion



        #region Properties, Indexers

        public string FilePath
        {
            get { return this.mp3Song.FilePath; }
        }

        public bool InEditMode
        {
            get { return this.mp3Song.InEditMode; }
        }

        public bool IsChecked
        {
            get { return this._isChecked; }
            set { this.SetProperty(newValue => this._isChecked = newValue, value); }
        }

        [DisplayName]
        [Required]
        [ValidString()]
        public string Title
        {
            get { return this.mp3Song.Title; }

            set
            {
                this.SetProperty(newValue => this.mp3Song.Title = newValue, value);
                this.RaisePropertyChanged(() => this.InEditMode);
            }
        }

        [DisplayName]
        [Required]
        [ValidString()]
        public string Artist
        {
            get { return this.mp3Song.Artist; }

            set
            {
                this.SetProperty(newValue => this.mp3Song.Artist = newValue, value);
                this.RaisePropertyChanged(() => this.InEditMode);
            }
        }

        [DisplayName]
        [ValidString()]
        public string Album
        {
            get { return this.mp3Song.Album; }

            set
            {
                this.SetProperty(newValue => this.mp3Song.Album = newValue, value);
                this.RaisePropertyChanged(() => this.InEditMode);
            }
        }

        public List<CommandViewModel> Commands { get; private set; }

        #endregion



        #region Methods

        private void InitCommands()
        {
            this.Commands = new List<CommandViewModel>();
            this.Commands.Add(new CommandViewModel(Resources.Mp3SongVM_DisplayName_Save, Resources.CommandName_Save, new RelayCommand(this.Save, this.CanSave)));
            this.Commands.Add(new CommandViewModel(Resources.Mp3SongVM_DisplayName_Undo, Resources.CommandName_Undo, new RelayCommand(this.Undo, this.CanUndo)));
            this.Commands.Add(new CommandViewModel(Resources.Mp3SongVM_DisplayName_Remove, Resources.CommandName_Remove, new RelayCommand(this.RemoveMessage)));
            this.Commands.Add(new CommandViewModel(Resources.Mp3SongVM_DisplayName_ClearAlbum, Resources.CommandName_ClearAlbum, new RelayCommand(this.ClearAlbum)));
        }

        public RelayCommand GetCommand(string paramCommandName)
        {
            try
            {
                CommandViewModel command = this.Commands.Find(x => x.CommandName == paramCommandName);
                return command.RelayCommand;
            }
            catch
            {
                throw new ArgumentException(Resources.Exception_InvalidCommandName, paramCommandName);
            }
        }

        public void Rename(IMp3Tag paramMp3Tag)
        {
            this.Title = paramMp3Tag.Title;
            this.Artist = paramMp3Tag.Artist;
            this.Album = paramMp3Tag.Album;
        }

        private bool CanSave()
        {
            if (this.IsValid && this.InEditMode)
            {
                return true;
            }

            return false;
        }

        private async void Save()
        {
            if (this.mp3Song.FileExistsAlready)
            {
                bool replaceFile = await this.dialogService.ShowDialogYesNo("Warning!", "The file exists already. Do you want to replace it?");

                if (!replaceFile)
                {
                    return;
                }
            }

            this.mp3Song.SaveAndRename();
            this.UpdateProperties();
        }

        private bool CanUndo()
        {
            if (this.InEditMode)
            {
                return true;
            }

            return false;
        }

        private void Undo()
        {
            this.mp3Song.Undo();
            this.UpdateProperties();
        }

        private void UpdateProperties()
        {
            PropertyInfo[] propertyInfos = this.GetAllPropertyInfos();

            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                this.RaisePropertyChanged(propertyInfo.Name);
            }
        }

        private PropertyInfo[] GetAllPropertyInfos()
        {
            return this.GetType()
                        .GetProperties(BindingFlags.Public | BindingFlags.Instance).ToArray();
        }

        private async void RemoveMessage()
        {
            if (this.mp3Song.InEditMode)
            {
                bool saveFile = await this.dialogService.ShowDialogYesNo("Warning!", "The file was not saved. Do you want to save it?");

                if (saveFile)
                {
                    this.Save();
                }
            }

            Messenger.Default.Send(new NotificationMessage<Mp3SongViewModel>(this, Resources.CommandName_Remove));
        }

        private void ClearAlbum()
        {
            this.Album = string.Empty;
        }

        #endregion
    }
}