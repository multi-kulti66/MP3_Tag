// ///////////////////////////////////
// File: ErrorStrings.cs
// Last Change: 14.09.2016  20:06
// Author: Andre Multerer
// ///////////////////////////////////



namespace Resources
{
    public static class ErrorStrings
    {
        #region  Static Fields and Constants

        public const string Mp3Song_Error_TitleValueFaulty = "The title value has an unauthorized character.";
        public const string Mp3Song_Error_ArtistValueFaulty = "The artist value has an unauthorized character.";
        public const string Mp3Song_Error_AlbumValueFaulty = "The album value has an unauthorized character.";

        public const string Mp3Song_Error_TitleValueMissing = "The mp3 file needs a title.";
        public const string Mp3Song_Error_ArtistValueMissing = "The mp3 file needs an artist.";

        public const string Mp3Song_Exception_PropertiesNotValid = "The properties of the mp3 file are not valid.";
        public const string Mp3Song_Exception_PropertyNotExisting = "The requested property name does not exist.";
        public const string Mp3SongViewModel_Exception_PropertiesOfModelNotValid = "The properties of the mp3song model are not valid.";

        #endregion
    }
}