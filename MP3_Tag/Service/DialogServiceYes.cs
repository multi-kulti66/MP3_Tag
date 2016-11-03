// ///////////////////////////////////
// File: DialogServiceYes.cs
// Last Change: 03.11.2016  20:49
// Author: Andre Multerer
// ///////////////////////////////////



namespace MP3_Tag.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MP3_Tag.Model;



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
            List<string> tempList = new List<string>();

            for (int counter = 1; counter <= 5; counter++)
            {
                tempList.Add(string.Format(MockMp3File.FolderPath + "TestArtist{0} - TestTitle{0}" + MockMp3File.Extension, counter));
            }

            return tempList;
        }

        #endregion
    }
}