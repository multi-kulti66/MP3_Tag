// ///////////////////////////////////
// File: DialogServiceNoTest.cs
// Last Change: 14.09.2016  20:06
// Author: Andre Multerer
// ///////////////////////////////////



namespace MP3_Tag_Test.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MP3_Tag.Services;



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