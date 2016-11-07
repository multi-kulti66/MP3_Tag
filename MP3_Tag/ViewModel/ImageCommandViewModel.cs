// ///////////////////////////////////
// File: ImageCommandViewModel.cs
// Last Change: 03.11.2016  20:50
// Author: Andre Multerer
// ///////////////////////////////////



namespace MP3_Tag.ViewModel
{
    using System.Drawing;
    using GalaSoft.MvvmLight.Command;



    public class ImageCommandViewModel : CommandViewModel
    {
        #region Constructors

        public ImageCommandViewModel(Bitmap paramImage, string paramDisplayName, string paramCommandName, RelayCommand paramRelayCommand) : base(paramDisplayName, paramCommandName, paramRelayCommand)
        {
            this.Image = paramImage;
        }

        #endregion



        #region Properties, Indexers

        public Bitmap Image { get; set; }

        #endregion
    }
}