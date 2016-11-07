// ///////////////////////////////////
// File: MainViewModel.cs
// Last Change: 07.11.2016  23:55
// Author: Andre Multerer
// ///////////////////////////////////



namespace MP3_Tag.ViewModel
{
    using System.Windows;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using MP3_Tag.Factory;
    using MP3_Tag.Services;



    public class MainViewModel : ViewModelBase
    {
        #region Fields

        private RelayCommand _closeCommand;

        #endregion



        #region Constructors

        public MainViewModel(IDialogService paramDialogService, IModelFactory paramModelFactory)
        {
            this.CheckedElementsViewModel = new CheckedElementsViewModel();
            this.DataGridViewModel = new DataGridViewModel(paramDialogService, paramModelFactory);
            this.MenuViewModel = new MenuViewModel(paramDialogService);
            this.MediaViewModel = new MediaViewModel();
        }

        #endregion



        #region Properties, Indexers

        public RelayCommand CloseCommand
        {
            get
            {
                if (this._closeCommand == null)
                {
                    this._closeCommand = new RelayCommand(this.Close);
                }
                return this._closeCommand;
            }
        }

        public MenuViewModel MenuViewModel { get; private set; }

        public CheckedElementsViewModel CheckedElementsViewModel { get; private set; }

        public DataGridViewModel DataGridViewModel { get; private set; }

        public MediaViewModel MediaViewModel { get; private set; }

        #endregion



        #region Methods

        private void Close()
        {
            Application.Current.Shutdown();
        }

        #endregion
    }
}