using Microsoft.Win32;
using System;

namespace WpfQLSV.Services
{
    public class FileDialogService : IFileDialogService
    {
        public string OpenFileDialog(string filter, string title)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = filter,
                Title = title
            };

            return openFileDialog.ShowDialog() == true ? openFileDialog.FileName : null;
        }

        public string SaveFileDialog(string filter, string title, string defaultFileName)
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = filter,
                Title = title,
                FileName = defaultFileName
            };

            return saveFileDialog.ShowDialog() == true ? saveFileDialog.FileName : null;
        }
    }
}