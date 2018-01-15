using System;
using System.Collections.Generic;
using System.IO;
using Windows.UI.Xaml.Controls;

namespace Enmol.Contents
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Calender : Page
    {
        private BLL.SQLiteTools mySQLiteTools;

        public Calender()
        {
            this.InitializeComponent();
            CalenderContentFrame.Navigate(typeof(Calender_Calender));
            mySQLiteTools = new BLL.SQLiteTools();
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
