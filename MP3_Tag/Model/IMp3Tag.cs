// ///////////////////////////////////
// File: IMp3Tag.cs
// Last Change: 03.11.2016  20:49
// Author: Andre Multerer
// ///////////////////////////////////



namespace MP3_Tag.Model
{
    public interface IMp3Tag
    {
        #region Properties, Indexers

        string Title { get; set; }

        string Artist { get; set; }

        string Album { get; set; }

        #endregion
    }
}