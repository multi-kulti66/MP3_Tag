// ///////////////////////////////////
// File: IModelFactory.cs
// Last Change: 03.11.2016  20:49
// Author: Andre Multerer
// ///////////////////////////////////



namespace MP3_Tag.Factory
{
    using MP3_Tag.Model;



    public interface IModelFactory
    {
        #region Methods

        IFileModifier CreateFileModifier(bool paramFileExists = false);

        IMp3File CreateMp3File(string paramFilePath);

        #endregion
    }
}