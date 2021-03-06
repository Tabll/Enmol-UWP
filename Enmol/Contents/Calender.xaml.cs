﻿using Model;
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
                        var myPopup = new Views.AddSchedulePopupWindow("添加日程");
                        myPopup.LeftClick += MyPopup_LeftClick;
                        myPopup.ShowWIndow();
                        break;
                    case "时间轴":
                        Tools.Dialog.ShowSimpleDialog("Sorry", "功能正在开发中");
                        break;
                    case "计划":
                        //ReflashEvents();
                        break;
                    default:
                        Tools.Dialog.ShowSimpleDialog("Sorry", "功能正在开发中~");
                        break;
                }
            }
        }

        private void MyPopup_LeftClick(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Tools.Dialog.ShowSimpleDialog("提示", "已添加");
            CalenderContentFrame.Navigate(typeof(Calender_Calender));
        }
    }
}
