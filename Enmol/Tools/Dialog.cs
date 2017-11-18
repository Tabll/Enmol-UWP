using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Enmol.Tools
{
    class Dialog : IDialog
    {
        public async void ShowSimpleDialog(string title, string content)
        {
            ContentDialog myDialog = new ContentDialog()
            {
                Title = title,
                Content = content,
                PrimaryButtonText = "好的"
            };
            ContentDialogResult thisResult = await myDialog.ShowAsync();
            //throw new NotImplementedException();
        }
    }
}
