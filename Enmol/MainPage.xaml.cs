using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace Enmol
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            FillBackground(UserInfoGridBackground);//填充亚克力玻璃
            ShowVersion();//显示版本号
        }

        private void ShowVersion()
        {
            VersionTextBlock.Text = BLL.Tools.GetVersion();
        }

        private void FillBackground(Grid grid)
        {
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.Xaml.Media.XamlCompositionBrushBase"))
            {
                AcrylicBrush myBrush = new AcrylicBrush
                {
                    BackgroundSource = AcrylicBackgroundSource.Backdrop,
                    //myBrush.BackgroundSource = Windows.UI.Xaml.Media.AcrylicBackgroundSource.HostBackdrop;
                    TintColor = Color.FromArgb(255, 255, 255, 255),//亚克力颜色
                    FallbackColor = Color.FromArgb(255, 255, 255, 255),//失去焦点的时候的颜色
                    TintOpacity = 0.6//模糊度
                };
                grid.Background = myBrush;
            }
            else
            {
                SolidColorBrush myBrush = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                grid.Background = myBrush;
            }
        }
    }
}
