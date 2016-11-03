// ///////////////////////////////////
// File: ViewModelLocator.cs
// Last Change: 03.11.2016  20:50
// Author: Andre Multerer
// ///////////////////////////////////



namespace MP3_Tag.ViewModel
{
    using GalaSoft.MvvmLight.Ioc;
    using Microsoft.Practices.ServiceLocation;
    using MP3_Tag.Factory;
    using MP3_Tag.Services;



    /// <summary>
    ///     This class contains static references to all the view models in the
    ///     application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<IDialogService, DialogService>();
            SimpleIoc.Default.Register<IModelFactory, TagLibModelFactory>();
            SimpleIoc.Default.Register<MainViewModel>();
        }

        #endregion



        #region Properties, Indexers

        public MainViewModel Main
        {
            get { return ServiceLocator.Current.GetInstance<MainViewModel>(); }
        }

        #endregion



        #region Methods

        public static void Cleanup()
        {
            // Do nothing...
        }

        #endregion
    }
}