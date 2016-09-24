// ///////////////////////////////////
// File: DialogServiceYes.cs
// Last Change: 16.09.2016  20:27
// Author: Andre Multerer
// ///////////////////////////////////



namespace MP3_Tag_Test.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MP3_Tag.Services;
    using Resources;



    public class DialogServiceYes : IDialogService
    {
        #region IDialogService Members

        public void ShowMessage(string paramTitle, string paramMessage)
        {
            // do nothing
        }

        public Task<bool> ShowDialogYesNo(string paramTitle, string paramMessage)
        {
            return Task.FromResult(true);
        }

        public List<string> ShowFileDialog()
        {
            return MediaStrings.GetAllFilePaths.ToList();
        }

        #endregion
    }
}