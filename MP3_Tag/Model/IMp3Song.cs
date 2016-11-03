// ///////////////////////////////////
// File: IMp3Song.cs
// Last Change: 03.11.2016  20:49
// Author: Andre Multerer
// ///////////////////////////////////



namespace MP3_Tag.Model
{
    public interface IMp3Song : IMp3File
    {
        #region Properties, Indexers

        bool FileExistsAlready { get; }

        bool InEditMode { get; }

        #endregion



        #region Methods

        void SaveAndRename();

        void Undo();

        #endregion
    }
}