// ///////////////////////////////////
// File: TagLibModelFactory.cs
// Last Change: 03.11.2016  20:49
// Author: Andre Multerer
// ///////////////////////////////////



namespace MP3_Tag.Factory
{
    using MP3_Tag.Model;



    public class TagLibModelFactory : IModelFactory
    {
        #region IModelFactory Members

        public IFileModifier CreateFileModifier(bool paramFileExists = false)
        {
            return new FileModifier();
        }

        /// <exception cref="FileException">Throws when mp3 file could not be loaded.</exception>
        public IMp3File CreateMp3File(string paramFilePath)
        {
            return new TagLibMp3File(paramFilePath);
        }

        #endregion
    }
}