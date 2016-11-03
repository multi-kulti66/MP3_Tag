// ///////////////////////////////////
// File: MainViewModel.cs
// Last Change: 03.11.2016  20:50
// Author: Andre Multerer
// ///////////////////////////////////



namespace MP3_Tag.ViewModel
{
    using GalaSoft.MvvmLight;
    using MP3_Tag.Factory;
    using MP3_Tag.Services;



    public class MainViewModel : ViewModelBase
    {
        #region Fields

        private readonly IDialogService dialogService;
        private readonly IModelFactory modelFactory;

        #endregion



        #region Constructors

        public MainViewModel(IDialogService paramDialogService, IModelFactory paramModelFactory)
        {
            this.dialogService = paramDialogService;
            this.modelFactory = paramModelFactory;

            this.CheckedElementsViewModel = new CheckedElementsViewModel();
            this.DataGridViewModel = new DataGridViewModel(this.dialogService, this.modelFactory);
            this.MenuViewModel = new MenuViewModel(this.dialogService);
        }

        #endregion



        #region Properties, Indexers

        public MenuViewModel MenuViewModel { get; private set; }

        public CheckedElementsViewModel CheckedElementsViewModel { get; private set; }

        public DataGridViewModel DataGridViewModel { get; private set; }

        #endregion
    }
}