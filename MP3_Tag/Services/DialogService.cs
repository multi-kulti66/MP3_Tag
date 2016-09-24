// ///////////////////////////////////
// File: DialogService.cs
// Last Change: 24.09.2016  21:48
// Author: Andre Multerer
// ///////////////////////////////////



namespace MP3_Tag.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using MahApps.Metro.Controls;
    using MahApps.Metro.Controls.Dialogs;
    using Application = System.Windows.Application;



    public class DialogService : IDialogService
    {
        #region IDialogService Members

        public async void ShowMessage(string paramTitle, string paramMessage)
        {
            MetroWindow metroWindow = Application.Current.MainWindow as MetroWindow;
            await metroWindow.ShowMessageAsync(paramTitle, paramMessage);
        }

        public async Task<bool> ShowDialogYesNo(string paramTitle, string paramMessage)
        {
            MetroWindow metroWindow = Application.Current.MainWindow as MetroWindow;

            MessageDialogResult result = await metroWindow.ShowMessageAsync(paramTitle, paramMessage, MessageDialogStyle.AffirmativeAndNegative);

            if (result == MessageDialogResult.Affirmative)
            {
                return true;
            }

            return false;
        }

        public List<string> ShowFileDialog()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = @"Füge die gewünschten mp3 - Dateien hinzu";
            ofd.Filter = @"mp3 files (*.mp3)|*.mp3";
            ofd.InitialDirectory = Environment.UserName;
            ofd.Multiselect = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                return ofd.FileNames.ToList();
            }

            return null;
        }

        #endregion
    }
}