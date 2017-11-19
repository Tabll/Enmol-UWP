using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Enmol.Tools
{
    class Dialog
    {
        public static async void ShowSimpleDialog(string title, string content)
        {
            ContentDialog myDialog = new ContentDialog()
            {
                Title = title,
                Content = content,
                PrimaryButtonText = "好的"
            };
            ContentDialogResult thisResult = await myDialog.ShowAsync();
        }

        public static async void ShowTestDialog()
        {
            ContentDialog myDialog = new ContentDialog()
            {
                Title = "测试",
                Content = "这是一个测试框",
                PrimaryButtonText = "好的"
            };
            ContentDialogResult thisResult = await myDialog.ShowAsync();
        }
    }
}
