using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Enmol.Contents
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Calender : Page
    {
        public Calender()
        {
            this.InitializeComponent();
            CalenderContentFrame.Navigate(typeof(Calender_Calender));
        }

        private void CalenderNavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                Frame.Navigate(typeof(Views.Settings));
            }
            else
            {
                switch (args.InvokedItem)
                {
                    case "添加日程":
                        var myPopup = new Views.AddSchedulePopupWindow("更改学生详细信息");
                        myPopup.ShowWIndow();
                        break;
                    case "时间轴":
                        Tools.Dialog.ShowSimpleDialog("Sorry", "功能正在开发中");
                        break;
                    case "计划":
                        Tools.Dialog.ShowSimpleDialog("Sorry", "功能正在开发中");
                        break;
                    default:
                        Tools.Dialog.ShowSimpleDialog("Sorry", "功能正在开发中~");
                        break;
                }
            }
        }
    }
}
