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
        private int weekFromNow = 0;

        public Calender_Calender()
        {
            this.InitializeComponent();
            TimeTextBlock.Text = BLL.TimeTools.GetAllDateTime(DateTime.Now);
            UpdateDate();
            UpdateTime();
        }

        private void UpdateDate()
        {
            switch (DateTime.Now.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    MarkToday(SUN, SUN_DAY, SUN_CDAY);
                    break;
                case DayOfWeek.Monday:
                    MarkToday(MON, MON_DAY, MON_CDAY);
                    break;
                case DayOfWeek.Tuesday:
                    MarkToday(TUE, TUE_DAY, TUE_CDAY);
                    break;
                case DayOfWeek.Wednesday:
                    MarkToday(WED, WED_DAY, WED_CDAY);
                    break;
                case DayOfWeek.Thursday:
                    MarkToday(THU, THU_DAY, THU_CDAY);
                    break;
                case DayOfWeek.Friday:
                    MarkToday(FRI, FRI_DAY, FRI_CDAY);
                    break;
                case DayOfWeek.Saturday:
                    MarkToday(SAT, SAT_DAY, SAT_CDAY);
                    break;
                default:
                    break;
            }
        }

        private void MarkToday(TextBlock weekTextBlock, TextBlock dayTextBlock, TextBlock chineseTextBlock)
        {
            Brush todayBrush = Resources["SystemControlAccentDark1AcrylicWindowAccentDark1Brush"] as Brush;
            weekTextBlock.Foreground = todayBrush;
            dayTextBlock.Foreground = todayBrush;
            chineseTextBlock.Foreground = todayBrush;
            SUN_DAY.Text = DateTime.Now.AddDays(-((double)DateTime.Now.DayOfWeek)).Day.ToString();
            MON_DAY.Text = DateTime.Now.AddDays(-((double)DateTime.Now.DayOfWeek) + 1).Day.ToString();
            TUE_DAY.Text = DateTime.Now.AddDays(-((double)DateTime.Now.DayOfWeek) + 2).Day.ToString();
            WED_DAY.Text = DateTime.Now.AddDays(-((double)DateTime.Now.DayOfWeek) + 3).Day.ToString();
            THU_DAY.Text = DateTime.Now.AddDays(-((double)DateTime.Now.DayOfWeek) + 4).Day.ToString();
            FRI_DAY.Text = DateTime.Now.AddDays(-((double)DateTime.Now.DayOfWeek) + 5).Day.ToString();
            SAT_DAY.Text = DateTime.Now.AddDays(-((double)DateTime.Now.DayOfWeek) + 6).Day.ToString();

            SUN_CDAY.Text = BLL.TimeTools.GetChineseDate(DateTime.Now.AddDays(-((double)DateTime.Now.DayOfWeek)));
            MON_CDAY.Text = BLL.TimeTools.GetChineseDate(DateTime.Now.AddDays(-((double)DateTime.Now.DayOfWeek) + 1));
            TUE_CDAY.Text = BLL.TimeTools.GetChineseDate(DateTime.Now.AddDays(-((double)DateTime.Now.DayOfWeek) + 2));
            WED_CDAY.Text = BLL.TimeTools.GetChineseDate(DateTime.Now.AddDays(-((double)DateTime.Now.DayOfWeek) + 3));
            THU_CDAY.Text = BLL.TimeTools.GetChineseDate(DateTime.Now.AddDays(-((double)DateTime.Now.DayOfWeek) + 4));
            FRI_CDAY.Text = BLL.TimeTools.GetChineseDate(DateTime.Now.AddDays(-((double)DateTime.Now.DayOfWeek) + 5));
            SAT_CDAY.Text = BLL.TimeTools.GetChineseDate(DateTime.Now.AddDays(-((double)DateTime.Now.DayOfWeek) + 6));

            Grid markerGrid = new Grid
            {
                Background = todayBrush,
            };
            weekTitleGrid.Children.Add(markerGrid);
            markerGrid.SetValue(Grid.RowProperty, 3);
            markerGrid.SetValue(Grid.ColumnProperty, ((int)DateTime.Now.DayOfWeek + 1));
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
