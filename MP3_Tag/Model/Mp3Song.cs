// ///////////////////////////////////
// File: Mp3Song.cs
// Last Change: 03.11.2016  20:49
// Author: Andre Multerer
// ///////////////////////////////////



namespace MP3_Tag.Model
{
    using MP3_Tag.Helper;



    public class Mp3Song : ObjectBase, IMp3Song
    {
        #region Fields

        private readonly IFileModifier fileModifier;
        private readonly IMp3File mp3File;

        #endregion



        #region Constructors

        public Mp3Song(IMp3File paramMp3File, IFileModifier paramFileModifier)
        {
            this.mp3File = paramMp3File;
            this.fileModifier = paramFileModifier;
        }

        #endregion



        #region IMp3Song Members

        public string FilePath
        {
            get { return this.mp3File.FilePath; }
        }

        public string WishedFilePath
        {
            get { return this.mp3File.WishedFilePath; }
        }

        public string Title
        {
            get { return this.mp3File.Title; }
            set { this.SetProperty(newValue => this.mp3File.Title = newValue, value); }
        }

        public string Artist
        {
            get { return this.mp3File.Artist; }
            set { this.SetProperty(newValue => this.mp3File.Artist = newValue, value); }
        }

        public string Album
        {
            get { return this.mp3File.Album; }
            set { this.SetProperty(newValue => this.mp3File.Album = newValue, value); }
        }

        public bool FileExistsAlready
        {
            get
            {
                if ((this.WishedFilePath != this.FilePath) && this.fileModifier.FileExists(this.WishedFilePath))
                {
                    return true;
                }

                return false;
            }
        }

        public void SaveAndRename()
        {
            this.Save();
            this.Rename();
            this.Reload();
            this.EndEdit();
        }

        public void Save()
        {
            this.mp3File.Save();
        }

        public void Reload()
        {
            this.mp3File.Reload();
        }

        public void Undo()
        {
            this.CancelEdit();
        }

        #endregion



        #region Methods

        private void Rename()
        {
            if (this.FileExistsAlready)
            {
                this.fileModifier.Delete(this.WishedFilePath);
            }

            this.fileModifier.Rename(this.FilePath, this.WishedFilePath);
        }

        #endregion
    }
}