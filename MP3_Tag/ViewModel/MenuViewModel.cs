// ///////////////////////////////////
// File: MenuViewModel.cs
// Last Change: 03.11.2016  20:50
// Author: Andre Multerer
// ///////////////////////////////////



namespace MP3_Tag.ViewModel
{
    using System.Collections.Generic;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Messaging;
    using MP3_Tag.Properties;
    using MP3_Tag.Services;



    public class MenuViewModel : ViewModelBase
    {
        #region Fields

        private readonly IDialogService dialogService;

        #endregion



        #region Constructors

        public MenuViewModel(IDialogService paramDialogService)
        {
            this.dialogService = paramDialogService;

            this.InitCommands();
        }

        #endregion



        #region Properties, Indexers

        public List<ImageCommandViewModel> Commands { get; private set; }

        #endregion



        #region Methods

        private void InitCommands()
        {
            this.Commands = new List<ImageCommandViewModel>();
            this.Commands.Add(new ImageCommandViewModel(Resources.img_add, Resources.MenuVM_DisplayName_Add, Resources.CommandName_Add, new RelayCommand(this.AddMp3Files)));
            this.Commands.Add(new ImageCommandViewModel(Resources.img_save, Resources.MenuVM_DisplayName_Save, Resources.CommandName_Save, new RelayCommand(this.SaveAllMessage)));
            this.Commands.Add(new ImageCommandViewModel(Resources.img_undo, Resources.MenuVM_DisplayName_Undo, Resources.CommandName_Undo, new RelayCommand(this.UndoAllMessage)));
            this.Commands.Add(new ImageCommandViewModel(Resources.img_delete, Resources.MenuVM_DisplayName_Remove, Resources.CommandName_Remove, new RelayCommand(this.RemoveAllMessage)));
            this.Commands.Add(new ImageCommandViewModel(Resources.img_clearAlbum, Resources.MenuVM_DisplayName_ClearAlbum, Resources.CommandName_ClearAlbum, new RelayCommand(this.ClearAlbumOfAllMessage)));
        }

        private void AddMp3Files()
        {
            List<string> tempFilePaths = this.dialogService.ShowFileDialog();

            if (tempFilePaths != null)
            {
                Messenger.Default.Send(new NotificationMessage<List<string>>(tempFilePaths, Resources.CommandName_Add));
            }
        }

        private void SaveAllMessage()
        {
            Messenger.Default.Send(new NotificationMessage<string>(Resources.CommandBroadcast_All, Resources.CommandName_Save));
        }

        private void UndoAllMessage()
        {
            Messenger.Default.Send(new NotificationMessage<string>(Resources.CommandBroadcast_All, Resources.CommandName_Undo));
        }

        private void RemoveAllMessage()
        {
            Messenger.Default.Send(new NotificationMessage<string>(Resources.CommandBroadcast_All, Resources.CommandName_Remove));
        }

        private void ClearAlbumOfAllMessage()
        {
            Messenger.Default.Send(new NotificationMessage<string>(Resources.CommandBroadcast_All, Resources.CommandName_ClearAlbum));
        }

        #endregion
    }
}