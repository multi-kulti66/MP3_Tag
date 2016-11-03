// ///////////////////////////////////
// File: FileModifier.cs
// Last Change: 03.11.2016  20:49
// Author: Andre Multerer
// ///////////////////////////////////



namespace MP3_Tag.Model
{
    using System.IO;
    using MP3_Tag.Exception;
    using MP3_Tag.Properties;



    public class FileModifier : IFileModifier
    {
        #region IFileModifier Members

        public bool FileExists(string paramFilePath)
        {
            return File.Exists(paramFilePath);
        }

        public void Delete(string paramFilePath)
        {
            try
            {
                File.Delete(paramFilePath);
            }
            catch
            {
                throw new FileException(Resources.FileModifierM_Exception_Delete, string.Format(Resources.FileModifierM_Inner_Exception_Delete, paramFilePath));
            }
        }

        public void Rename(string paramOldFilePath, string paramNewFilePath)
        {
            try
            {
                File.Move(paramOldFilePath, paramNewFilePath);
            }
            catch
            {
                throw new FileException(Resources.FileModifierM_Exception_Move, string.Format(Resources.FileModifierM_Inner_Exception_Move, paramOldFilePath, paramNewFilePath));
            }
        }

        #endregion
    }
}