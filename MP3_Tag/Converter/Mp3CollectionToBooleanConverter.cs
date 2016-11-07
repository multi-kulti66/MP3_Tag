// ///////////////////////////////////
// File: Mp3CollectionToBooleanConverter.cs
// Last Change: 03.11.2016  20:49
// Author: Andre Multerer
// ///////////////////////////////////



namespace MP3_Tag.Converter
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Windows.Data;
    using MP3_Tag.ViewModel;



    public class Mp3CollectionToBooleanConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            IList<Mp3SongViewModel> mp3SongViewModels = value as IList<Mp3SongViewModel>;

            if ((mp3SongViewModels != null) && (mp3SongViewModels.Count > 0))
            {
                int selectedMp3Songs = mp3SongViewModels.Count(x => x.IsChecked);

                if (selectedMp3Songs.Equals(mp3SongViewModels.Count))
                {
                    return true;
                }

                if (selectedMp3Songs > 0)
                {
                    return null;
                }
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}