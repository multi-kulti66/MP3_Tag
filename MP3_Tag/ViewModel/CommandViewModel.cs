// ///////////////////////////////////
// File: CommandViewModel.cs
// Last Change: 03.11.2016  20:50
// Author: Andre Multerer
// ///////////////////////////////////



namespace MP3_Tag.ViewModel
{
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;



    public class CommandViewModel : ViewModelBase
    {
        #region Constructors

        public CommandViewModel(string paramDisplayName, string paramCommandName, RelayCommand paramRelayCommand)
        {
            this.DisplayName = paramDisplayName;
            this.CommandName = paramCommandName;
            this.RelayCommand = paramRelayCommand;
        }

        #endregion



        #region Properties, Indexers

        public string DisplayName { get; private set; }

        public string CommandName { get; private set; }

        public RelayCommand RelayCommand { get; private set; }

        #endregion
    }
}