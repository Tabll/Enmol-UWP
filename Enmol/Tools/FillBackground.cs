using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Enmol.Tools
{
    public class FillBackground
    {
        public FillBackground(Grid grid)
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
