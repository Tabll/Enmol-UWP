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
            //var loader = new Windows.ApplicationModel.Resources.ResourceLoader();

            if (args.IsSettingsInvoked)
            {
                //Tools.Dialog.ShowSimpleDialog("注意", "点击了设置");
            }
            else
            {
                switch (args.InvokedItem)
                {
                    case "添加日程":
                        //Tools.Dialog.ShowSimpleDialog("注意", "点击了添加日程");
                        var myPopup = new Views.AddSchedulePopupWindow("更改学生详细信息");
                        //msgPopup.LeftClick += (s, e) => { Frame.Navigate(typeof(StudentChangePopupWindow)); };
                        //msgPopup.LeftClick += (s, e) => { this.tb.Text = "您点击了：确定"; };
                        //msgPopup.RightClick += (s, e) => { this.tb.Text = "您点击了：取消"; };
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
