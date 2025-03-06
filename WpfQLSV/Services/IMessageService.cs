using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfQLSV.Services
{
    public interface IMessageService
    {
        void ShowMessage(string message, string title);
        void ShowError(string message, string title);
    }

 
}
