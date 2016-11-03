// ///////////////////////////////////
// File: MockModelFactory.cs
// Last Change: 03.11.2016  20:49
// Author: Andre Multerer
// ///////////////////////////////////



namespace MP3_Tag.Factory
{
    using MP3_Tag.Model;



    public class MockModelFactory : IModelFactory
    {
        #region IModelFactory Members

        public IFileModifier CreateFileModifier(bool paramFileExists = false)
        {
            return new MockFileModifier(paramFileExists);
        }

        public IMp3File CreateMp3File(string paramFilePath)
        {
            string artist = paramFilePath.Substring(paramFilePath.LastIndexOf("\\") + 1, (paramFilePath.IndexOf("-") - 1) - (paramFilePath.LastIndexOf("\\") + 1));
            string title = paramFilePath.Substring(paramFilePath.IndexOf("-") + 2, paramFilePath.IndexOf(".") - (paramFilePath.IndexOf("-") + 2));

            return new MockMp3File(title, artist);
        }

        #endregion
    }
}