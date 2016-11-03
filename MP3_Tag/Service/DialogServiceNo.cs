// ///////////////////////////////////
// File: DialogServiceNo.cs
// Last Change: 03.11.2016  20:49
// Author: Andre Multerer
// ///////////////////////////////////



namespace MP3_Tag.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;



    public class DialogServiceNo : IDialogService
    {
        #region IDialogService Members

        public void ShowMessage(string paramTitle, string paramMessage)
        {
            // do nothing
        }

        public Task<bool> ShowDialogYesNo(string paramTitle, string paramMessage)
        {
            return Task.FromResult(false);
        }

        public List<string> ShowFileDialog()
        {
            return null;
        }

        #endregion
    }
}