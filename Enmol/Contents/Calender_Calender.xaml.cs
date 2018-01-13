using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
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
    public sealed partial class Calender_Calender : Page
    {
        private bool isRunning = true;

        public Calender_Calender()
        {
            this.InitializeComponent();
            TimeTextBlock.Text = BLL.TimeTools.GetAllDateTime(DateTime.Now);
            UpdateTime();
        }

        private void UpdateTime()
        {
            DispatcherTimer timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 1) };
            timer.Tick += new EventHandler<object>(async (sender, e) =>
            {
                await Dispatcher.TryRunAsync(CoreDispatcherPriority.Normal, new DispatchedHandler(() =>
                {
                    TimeTextBlock.Text = BLL.TimeTools.GetAllDateTime(DateTime.Now);
                    if (!isRunning)
                    {
                        timer.Stop();
                    }
                }));
            });
            timer.Start();
        }//时间更新线程
    }
}
