// ///////////////////////////////////
// File: MP3SongRemovedEventArgs.cs
// Last Change: 14.09.2016  20:06
// Author: Andre Multerer
// ///////////////////////////////////



namespace MP3_Tag.DataAccess
{
    using System;



    public class Mp3SongRemovedEventArgs : EventArgs
    {
        #region Constructors

        public Mp3SongRemovedEventArgs(string paramFilePath)
        {
            this.FilePath = paramFilePath;
        }

        #endregion



        #region Properties, Indexers

        public string FilePath { get; private set; }

        #endregion
    }
}