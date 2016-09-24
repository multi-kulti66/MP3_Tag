// ///////////////////////////////////
// File: MediaStrings.cs
// Last Change: 16.09.2016  18:57
// Author: Andre Multerer
// ///////////////////////////////////



namespace Resources
{
    using System.IO;



    public struct TagValues
    {
        public string Title;
        public string Artist;
        public string Album;
    };

    public static class MediaStrings
    {
        #region  Static Fields and Constants

        public static readonly string Get_FolderPath_Mp3_Songs = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + @"\Resources\MediaFiles\";

        public static readonly string Get_Changing_File_For_MainViewModel = Get_FolderPath_Mp3_Songs + "MainViewModel_Changing_File.mp3";

        public static readonly string Get_FilePath_Anna_Naklab__Supergirl = Get_FolderPath_Mp3_Songs + "Anna Naklab - Supergirl.mp3";
        public static readonly TagValues Get_Tags_Anna_Naklab__Supergirl = new TagValues { Title = "Supergirl", Artist = "Anna Naklab", Album = string.Empty };

        public static readonly string Get_FilePath_AronChupa__Im_An_Albatraoz = Get_FolderPath_Mp3_Songs + "AronChupa - I'm An Albatraoz.mp3";
        public static readonly TagValues Get_Tags_AronChupa__Im_An_Albatraoz = new TagValues { Title = "I'm An Albatraoz", Artist = "AronChupa", Album = string.Empty };

        public static readonly string Get_FilePath_Avicii__You_Make_Me = Get_FolderPath_Mp3_Songs + "Avicii - You Make Me.mp3";
        public static readonly TagValues Get_Tags_Avicii__You_Make_Me = new TagValues { Title = "You Make Me", Artist = "Avicii", Album = string.Empty };

        public static readonly string Get_FilePath_Horizon__Avalanche = Get_FolderPath_Mp3_Songs + "Bring Me The Horizon - Avalanche.mp3";
        public static readonly TagValues Get_Tags_Horizon__Avalanche = new TagValues { Title = "Avalanche", Artist = "Bring Me The Horizon", Album = string.Empty };

        public static readonly string Get_FilePath_Horizon__Blasphemy = Get_FolderPath_Mp3_Songs + "Bring Me The Horizon - Blasphemy.mp3";
        public static readonly TagValues Get_Tags_Horizon_Blasphemy = new TagValues { Title = "Blasphemy", Artist = "Bring Me The Horizon", Album = string.Empty };

        public static readonly string[] GetAllFilePaths = new string[] { Get_FilePath_Anna_Naklab__Supergirl, Get_FilePath_AronChupa__Im_An_Albatraoz, Get_FilePath_Avicii__You_Make_Me, Get_FilePath_Horizon__Avalanche, Get_FilePath_Horizon__Blasphemy };
        public static readonly TagValues[] GetAllTagValues = new TagValues[] { Get_Tags_Anna_Naklab__Supergirl, Get_Tags_AronChupa__Im_An_Albatraoz, Get_Tags_Avicii__You_Make_Me, Get_Tags_Horizon__Avalanche, Get_Tags_Horizon_Blasphemy };

        #endregion
    }
}