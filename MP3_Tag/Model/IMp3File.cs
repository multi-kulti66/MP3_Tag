// ///////////////////////////////////
// File: IMp3File.cs
// Last Change: 03.11.2016  20:49
// Author: Andre Multerer
// ///////////////////////////////////



namespace MP3_Tag.Model
{
    using MP3_Tag.Exception;



    public interface IMp3File : IMp3Tag
    {
        #region Properties, Indexers

        string FilePath { get; }

        string WishedFilePath { get; }

        #endregion



        #region Methods

        void Save();

        /// <exception cref="FileException">Throws when mp3 file could not be loaded.</exception>
        void Reload();

        #endregion
    }
}