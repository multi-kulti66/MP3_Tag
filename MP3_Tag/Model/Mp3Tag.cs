// ///////////////////////////////////
// File: Mp3Tag.cs
// Last Change: 03.11.2016  20:49
// Author: Andre Multerer
// ///////////////////////////////////



namespace MP3_Tag.Model
{
    public class Mp3Tag : IMp3Tag
    {
        #region Constructors

        public Mp3Tag(string paramTitle = "", string paramArtist = "", string paramAlbum = "")
        {
            this.Title = paramTitle;
            this.Artist = paramArtist;
            this.Album = paramAlbum;
        }

        #endregion



        #region IMp3Tag Members

        public string Title { get; set; }

        public string Artist { get; set; }

        public string Album { get; set; }

        #endregion
    }
}