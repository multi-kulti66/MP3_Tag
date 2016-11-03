// ///////////////////////////////////
// File: TagLibMp3File.cs
// Last Change: 03.11.2016  20:49
// Author: Andre Multerer
// ///////////////////////////////////



namespace MP3_Tag.Model
{
    using System.IO;
    using MP3_Tag.Exception;
    using MP3_Tag.Properties;
    using TagLib;
    using File = TagLib.File;



    public class TagLibMp3File : IMp3File
    {
        #region Fields

        private File mp3File;

        #endregion



        #region Constructors

        /// <exception cref="FileException">Throws when mp3 file could not be loaded.</exception>
        public TagLibMp3File(string paramFilePath)
        {
            try
            {
                this.mp3File = File.Create(paramFilePath);
            }
            catch (UnsupportedFormatException)
            {
                throw new FileException(Resources.TagLibMp3File_Exception_InvalidFilePath, string.Format(Resources.TagLibMp3File_Inner_Exception_InvalidFilePath, paramFilePath));
            }
        }

        #endregion



        #region IMp3File Members

        public string FilePath
        {
            get { return this.mp3File.Name; }
        }

        public string WishedFilePath
        {
            get
            {
                string folderPath = Path.GetDirectoryName(this.FilePath) + @"\";
                string newFileName = this.Artist + " - " + this.Title;
                string fileExtension = Path.GetExtension(this.FilePath);

                return folderPath + newFileName + fileExtension;
            }
        }

        public string Title
        {
            get { return this.mp3File.Tag.Title; }
            set
            {
                if (this.mp3File.Tag.Title == value)
                {
                    return;
                }

                this.mp3File.Tag.Title = value;
            }
        }

        public string Artist
        {
            get { return this.mp3File.Tag.FirstPerformer; }
            set
            {
                if (this.mp3File.Tag.FirstPerformer == value)
                {
                    return;
                }

                this.mp3File.Tag.Performers = new[] { value };
                this.mp3File.Tag.AlbumArtists = new[] { value };
            }
        }

        public string Album
        {
            get { return this.mp3File.Tag.Album; }
            set
            {
                if (this.mp3File.Tag.Album == value)
                {
                    return;
                }

                this.mp3File.Tag.Album = value;
            }
        }

        public void Save()
        {
            this.RemoveUnnecessaryTags();
            this.mp3File.Save();
        }

        /// <exception cref="FileException">Throws when mp3 file could not be loaded.</exception>
        public void Reload()
        {
            try
            {
                this.mp3File = File.Create(this.WishedFilePath);
            }
            catch (UnsupportedFormatException)
            {
                throw new FileException(string.Format(Resources.TagLibMp3File_Exception_InvalidFilePath, this.WishedFilePath));
            }
        }

        #endregion



        #region Methods

        private void RemoveUnnecessaryTags()
        {
            string title = this.Title, artist = this.Artist, album = this.Album;

            this.mp3File.Tag.Clear();

            this.Title = title;
            this.Artist = artist;
            this.Album = album;
        }

        #endregion
    }
}