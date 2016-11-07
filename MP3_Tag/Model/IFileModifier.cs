// ///////////////////////////////////
// File: IFileModifier.cs
// Last Change: 03.11.2016  20:49
// Author: Andre Multerer
// ///////////////////////////////////



namespace MP3_Tag.Model
{
    using MP3_Tag.Exception;



    public interface IFileModifier
    {
        #region Methods

        bool FileExists(string paramFilePath);

        /// <exception cref="FileException">Throws when file does not exist or parameter is invalid</exception>
        void Delete(string paramFilePath);

        /// <exception cref="FileException">Throws when file already exists or parameter are invalid</exception>
        void Rename(string paramOldFilePath, string paramNewFilePath);

        #endregion
    }
}