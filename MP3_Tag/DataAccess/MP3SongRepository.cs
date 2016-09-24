// ///////////////////////////////////
// File: MP3SongRepository.cs
// Last Change: 16.09.2016  20:23
// Author: Andre Multerer
// ///////////////////////////////////



namespace MP3_Tag.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MP3_Tag.Model;



    public class Mp3SongRepository
    {
        #region Constructors

        public Mp3SongRepository()
        {
            this.Mp3Songs = new List<Mp3Song>();
        }

        #endregion



        #region Properties, Indexers

        public List<Mp3Song> Mp3Songs { get; }

        #endregion



        #region Events

        public event EventHandler<Mp3SongAddedEventArgs> Mp3SongAdded;

        public event EventHandler<Mp3SongRemovedEventArgs> Mp3SongRemoved;

        #endregion



        #region Methods

        public void AddMp3Song(string paramFilePath)
        {
            if (this.Mp3Songs.Count(x => x.FilePath == paramFilePath) == 0)
            {
                this.Mp3Songs.Add(new Mp3Song(paramFilePath));

                if (this.Mp3SongAdded != null)
                {
                    this.Mp3SongAdded(this, new Mp3SongAddedEventArgs(paramFilePath));
                }
            }
        }

        public void RemoveMp3Song(string paramFilePath)
        {
            if (this.Mp3Songs.Count(x => x.FilePath == paramFilePath) == 0)
            {
                throw new InvalidOperationException(string.Format("The file {0} was not in the list.", paramFilePath));
            }

            this.Mp3Songs.Remove(this.Mp3Songs.First(x => x.FilePath == paramFilePath));

            if (this.Mp3SongRemoved != null)
            {
                this.Mp3SongRemoved(this, new Mp3SongRemovedEventArgs(paramFilePath));
            }
        }

        #endregion
    }
}