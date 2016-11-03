// ///////////////////////////////////
// File: MockMp3File.cs
// Last Change: 03.11.2016  20:49
// Author: Andre Multerer
// ///////////////////////////////////



namespace MP3_Tag.Model
{
    public class MockMp3File : IMp3File
    {
        #region Fields

        private string _filePath;

        #endregion



        #region Constructors

        public MockMp3File(string paramTitle = "", string paramArtist = "", string paramAlbum = "")
        {
            this.Title = paramTitle;
            this.Artist = paramArtist;
            this.Album = paramAlbum;

            this.SetFilePath();
        }

        #endregion



        #region Properties, Indexers

        public static string FolderPath
        {
            get { return "C:\\Music\\"; }
        }

        public static string Extension
        {
            get { return ".mp3"; }
        }

        #endregion



        #region IMp3File Members

        public string FilePath
        {
            get { return this._filePath; }
        }

        public string Title { get; set; }

        public string Artist { get; set; }

        public string Album { get; set; }

        public string WishedFilePath
        {
            get { return this.GetExpectedWishedFilePath(); }
        }

        public void Save()
        {
            return;
        }

        public void Reload()
        {
            this.SetFilePath();
        }

        #endregion



        #region Methods

        public void SetFilePath()
        {
            this._filePath = this.WishedFilePath;
        }

        private string GetExpectedWishedFilePath()
        {
            return string.Format("{0}{1} - {2}{3}", FolderPath, this.Artist, this.Title, Extension);
        }

        #endregion
    }
}