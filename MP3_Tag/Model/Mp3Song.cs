// ///////////////////////////////////
// File: Mp3Song.cs
// Last Change: 24.09.2016  21:28
// Author: Andre Multerer
// ///////////////////////////////////



namespace MP3_Tag.Model
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using Resources;
    using TagLib;



    public class Mp3Song : IDataErrorInfo, IEditableObject
    {
        #region  Static Fields and Constants

        private static readonly string[] ValidatedProperties = { nameof(Title), nameof(Artist), nameof(Album) };

        #endregion



        #region Fields

        private File _mp3Song;
        private string _filePath;

        private PropertyValues _backupMp3song;
        private bool _isEdited;

        #endregion



        #region Constructors

        public Mp3Song(string paramFilePath)
        {
            try
            {
                this._mp3Song = File.Create(paramFilePath);
            }
            catch (UnsupportedFormatException)
            {
                throw new UnsupportedFormatException("Invalid file path.");
            }

            this._filePath = paramFilePath;
            this.IsEdited = false;
            this._backupMp3song = new PropertyValues() { Title = this.Title, Artist = this.Artist, Album = this.Album };
        }

        #endregion



        #region Properties, Indexers

        /// <summary>
        ///     Gets or sets the album of the mp3.
        /// </summary>
        public string Album
        {
            get { return this._mp3Song.Tag.Album; }

            set
            {
                if (this._mp3Song.Tag.Album == value)
                {
                    return;
                }

                this._mp3Song.Tag.Album = value;
                this.OnMp3Changed();
            }
        }

        /// <summary>
        ///     Gets or sets the artist of the mp3.
        /// </summary>
        public string Artist
        {
            get { return this._mp3Song.Tag.FirstPerformer; }

            set
            {
                if ((this._mp3Song.Tag.FirstPerformer == value) && (this._mp3Song.Tag.FirstAlbumArtist == value))
                {
                    return;
                }

                this._mp3Song.Tag.Performers = new[] { value };
                this._mp3Song.Tag.AlbumArtists = new[] { value };
                this.OnMp3Changed();
            }
        }

        /// <summary>
        ///     Gets or sets the title of the mp3.
        /// </summary>
        public string Title
        {
            get { return this._mp3Song.Tag.Title; }

            set
            {
                if (this._mp3Song.Tag.Title == value)
                {
                    return;
                }

                this._mp3Song.Tag.Title = value;
                this.OnMp3Changed();
            }
        }

        /// <summary>
        ///     Gets the file path to the mp3.
        /// </summary>
        public string FilePath
        {
            get { return this._filePath; }

            private set { this._filePath = value; }
        }

        /// <summary>
        ///     Gets a value indicating whether the mp3 file was changed and is not saved yet.
        /// </summary>
        public bool IsEdited
        {
            get { return this._isEdited; }

            private set { this._isEdited = value; }
        }

        /// <summary>
        ///     Gets a value indicating whether all properties are valid.
        /// </summary>
        public bool IsValid
        {
            get { return ValidatedProperties.All(property => this.GetValidationError(property) == null); }
        }

        /// <summary>
        ///     Gets a value indicating whether the file name exists already.
        /// </summary>
        public bool FileExistsAlready
        {
            get
            {
                if (System.IO.File.Exists(this.WishedFilePath) && (this.WishedFilePath != this.FilePath))
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        ///     Gets the wished folder path for the mp3 file.
        /// </summary>
        public string WishedFilePath
        {
            get { return this._filePath.Substring(0, this._filePath.LastIndexOf(@"\", StringComparison.Ordinal) + 1) + this.Artist + " - " + this.Title + ".mp3"; }
        }

        #endregion



        #region IDataErrorInfo Members

        public string Error
        {
            get { return null; }
        }

        public string this[string propertyName]
        {
            get { return this.GetValidationError(propertyName); }
        }

        #endregion



        #region IEditableObject Members

        public void BeginEdit()
        {
            if (!this.IsEdited)
            {
                this.IsEdited = true;
            }
        }

        public void CancelEdit()
        {
            if (this.IsEdited)
            {
                this.Title = this._backupMp3song.Title;
                this.Artist = this._backupMp3song.Artist;
                this.Album = this._backupMp3song.Album;
                this.IsEdited = false;
            }
        }

        public void EndEdit()
        {
            if (this.IsEdited)
            {
                this._backupMp3song = new PropertyValues() { Title = this.Title, Artist = this.Artist, Album = this.Album };
                this.IsEdited = false;
            }
        }

        #endregion



        #region Methods

        public bool HasSameFilePath(Mp3Song paramMp3Song)
        {
            if (this.FilePath == paramMp3Song.FilePath)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        ///     Saves all changes to the mp3 file.
        /// </summary>
        public void Save()
        {
            if (!this.IsValid)
            {
                throw new InvalidOperationException("The mp3 file has invalid property values.");
            }

            this.ClearTags();
            this._mp3Song.Save();
            this.Rename();
            this.EndEdit();
        }

        /// <summary>
        ///     Clears all tags from the mp3 file.
        /// </summary>
        private void ClearTags()
        {
            string tempTitle = this.Title;
            string tempArtist = this.Artist;
            string tempAlbum = this.Album;

            this._mp3Song.Tag.Clear();

            this.Title = tempTitle;
            this.Artist = tempArtist;
            this.Album = tempAlbum;
        }

        private void Rename()
        {
            if (this.FileExistsAlready)
            {
                System.IO.File.Delete(this.WishedFilePath);
            }

            System.IO.File.Move(this.FilePath, this.WishedFilePath);
            this.FilePath = this.WishedFilePath;
            this._mp3Song = File.Create(this.FilePath);
        }

        private void OnMp3Changed()
        {
            if (!this.IsEdited)
            {
                this.BeginEdit();
            }
            else
            {
                if ((this.Title == this._backupMp3song.Title) &&
                    (this.Artist == this._backupMp3song.Artist) &&
                    (this.Album == this._backupMp3song.Album))
                {
                    this.EndEdit();
                }
            }
        }

        private bool IsValidString(string paramValue)
        {
            return paramValue.All(c => ((c >= '0') && (c <= '9')) || ((c >= 'A') && (c <= 'Z'))
                                       || (c == 'Ä') || (c == 'Ö') || (c == 'Ü')
                                       || (c == 'ä') || (c == 'ö') || (c == 'ü')
                                       || ((c >= 'a') && (c <= 'z')) || (c == ' ')
                                       || (c == '.') || (c == ',') || (c == '_') || (c == '-')
                                       || (c == '(') || (c == ')') || (c == '&') || (c == '\''));
        }

        private string GetValidationError(string propertyName)
        {
            string error;

            switch (propertyName)
            {
                case nameof(this.Album):
                    error = this.ValidateAlbum();
                    break;
                case nameof(this.Artist):
                    error = this.ValidateArtist();
                    break;

                case nameof(this.Title):
                    error = this.ValidateTitle();
                    break;
                default:
                    throw new InvalidOperationException(ErrorStrings.Mp3Song_Exception_PropertyNotExisting);
            }

            return error;
        }

        private string ValidateAlbum()
        {
            if (string.IsNullOrEmpty(this.Album))
            {
                return null;
            }

            if (!this.IsValidString(this.Album))
            {
                return ErrorStrings.Mp3Song_Error_AlbumValueFaulty;
            }

            return null;
        }

        private string ValidateArtist()
        {
            if (string.IsNullOrEmpty(this.Artist))
            {
                return ErrorStrings.Mp3Song_Error_ArtistValueMissing;
            }

            if (!this.IsValidString(this.Artist))
            {
                return ErrorStrings.Mp3Song_Error_ArtistValueFaulty;
            }

            return null;
        }

        private string ValidateTitle()
        {
            if (string.IsNullOrEmpty(this.Title))
            {
                return ErrorStrings.Mp3Song_Error_TitleValueMissing;
            }

            if (!this.IsValidString(this.Title))
            {
                return ErrorStrings.Mp3Song_Error_TitleValueFaulty;
            }

            return null;
        }

        #endregion



        #region Nested type: PropertyValues

        private struct PropertyValues
        {
            public string Title;
            public string Artist;
            public string Album;
        }

        #endregion
    }
}