// ///////////////////////////////////
// File: CheckedElementsViewModel.cs
// Last Change: 07.11.2016  19:43
// Author: Andre Multerer
// ///////////////////////////////////



namespace MP3_Tag.ViewModel
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Messaging;
    using MP3_Tag.Model;
    using MP3_Tag.Properties;
    using MP3_Tag.Validation;



    public class CheckedElementsViewModel : BindableValidator
    {
        #region Fields

        private readonly Mp3Tag mp3Tag;

        #endregion



        #region Constructors

        public CheckedElementsViewModel()
        {
            this.mp3Tag = new Mp3Tag();
            this.InitCommands();
        }

        #endregion



        #region Properties, Indexers

        public List<CommandViewModel> Commands { get; private set; }

        [DisplayName]
        [ValidString]
        public string Title
        {
            get { return this.mp3Tag.Title; }
            set
            {
                this.SetProperty(newValue => this.mp3Tag.Title = newValue, value);
                this.Commands.Find(x => x.CommandName == Resources.CommandName_Rename).RelayCommand.RaiseCanExecuteChanged();
            }
        }

        [DisplayName]
        [ValidString]
        public string Artist
        {
            get { return this.mp3Tag.Artist; }
            set
            {
                this.SetProperty(newValue => this.mp3Tag.Artist = newValue, value);
                this.Commands.Find(x => x.CommandName == Resources.CommandName_Rename).RelayCommand.RaiseCanExecuteChanged();
            }
        }

        [DisplayName]
        [ValidString]
        public string Album
        {
            get { return this.mp3Tag.Album; }
            set
            {
                this.SetProperty(newValue => this.mp3Tag.Album = newValue, value);
                this.Commands.Find(x => x.CommandName == Resources.CommandName_Rename).RelayCommand.RaiseCanExecuteChanged();
            }
        }

        #endregion



        #region Methods

        private void InitCommands()
        {
            this.Commands = new List<CommandViewModel>();
            this.Commands.Add(new CommandViewModel(Resources.CheckedElementsVM_DisplayName_Rename, Resources.CommandName_Rename, new RelayCommand(this.RenameCheckedElementsMessage, this.CanRenameElements)));
            this.Commands.Add(new CommandViewModel(Resources.CheckedElementsVM_DisplayName_Save, Resources.CommandName_Save, new RelayCommand(this.SaveCheckedElementsMessage)));
            this.Commands.Add(new CommandViewModel(Resources.CheckedElementsVM_DisplayName_Undo, Resources.CommandName_Undo, new RelayCommand(this.UndoCheckedElementsMessage)));
            this.Commands.Add(new CommandViewModel(Resources.CheckedElementsVM_DisplayName_Remove, Resources.CommandName_Remove, new RelayCommand(this.RemoveCheckedElementsMessage)));
            this.Commands.Add(new CommandViewModel(Resources.CheckedElementsVM_DisplayName_ClearAlbum, Resources.CommandName_ClearAlbum, new RelayCommand(this.ClearAlbumOfCheckedElementsMessage)));
        }

        private bool CanRenameElements()
        {
            if (this.IsValid)
            {
                return true;
            }

            return false;
        }

        private void RenameCheckedElementsMessage()
        {
            Messenger.Default.Send(new NotificationMessage<Mp3Tag>(this.mp3Tag, Resources.CommandName_Rename));
        }

        private void SaveCheckedElementsMessage()
        {
            Messenger.Default.Send(new NotificationMessage<string>(Resources.CommandBroadcast_Checked, Resources.CommandName_Save));
        }

        private void UndoCheckedElementsMessage()
        {
            Messenger.Default.Send(new NotificationMessage<string>(Resources.CommandBroadcast_Checked, Resources.CommandName_Undo));
        }

        private void RemoveCheckedElementsMessage()
        {
            Messenger.Default.Send(new NotificationMessage<string>(Resources.CommandBroadcast_Checked, Resources.CommandName_Remove));
        }

        private void ClearAlbumOfCheckedElementsMessage()
        {
            Messenger.Default.Send(new NotificationMessage<string>(Resources.CommandBroadcast_Checked, Resources.CommandName_ClearAlbum));
        }

        #endregion
    }
}