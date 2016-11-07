// ///////////////////////////////////
// File: IDialogService.cs
// Last Change: 03.11.2016  20:49
// Author: Andre Multerer
// ///////////////////////////////////



namespace MP3_Tag.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;



    public interface IDialogService
    {
        #region Methods

        void ShowMessage(string paramTitle, string paramMessage);

        Task<bool> ShowDialogYesNo(string paramTitle, string paramMessage);

        List<string> ShowFileDialog();

        #endregion
    }
}