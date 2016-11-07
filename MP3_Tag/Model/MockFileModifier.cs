// ///////////////////////////////////
// File: MockFileModifier.cs
// Last Change: 03.11.2016  20:49
// Author: Andre Multerer
// ///////////////////////////////////



namespace MP3_Tag.Model
{
    public class MockFileModifier : IFileModifier
    {
        #region Fields

        private bool fileExists;

        #endregion



        #region Constructors

        public MockFileModifier(bool paramFileExists)
        {
            this.fileExists = paramFileExists;
        }

        #endregion



        #region IFileModifier Members

        public bool FileExists(string paramFilePath)
        {
            return this.fileExists;
        }

        public void Delete(string paramFilePath)
        {
            return;
        }

        public void Rename(string paramOldFilePath, string paramNewFilePath)
        {
            return;
        }

        #endregion



        #region Methods

        public void SetFileExistsValue(bool paramFileExistsValue)
        {
            this.fileExists = paramFileExistsValue;
        }

        #endregion
    }
}