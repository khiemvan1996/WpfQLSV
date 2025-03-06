using Microsoft.Win32;

namespace WpfQLSV.Services
{
    public interface IFileDialogService
    {
        string OpenFileDialog(string filter, string title);
        string SaveFileDialog(string filter, string title, string defaultFileName);
    }
}