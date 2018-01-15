using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Enmol.Contents
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Calender_Calender : Page
    {
        private bool isRunning = true;
        public static int weekFromNow = 0;
        private BLL.SQLiteTools mySQLiteTools = new BLL.SQLiteTools();

        public Calender_Calender()
        {
            this.InitializeComponent();
            TimeTextBlock.Text = BLL.TimeTools.GetAllDateTime(DateTime.Now);
            UpdateDate();
            UpdateTime();
            ReflashEvents();
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
                Name = "markerGrid"
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

        private void Reflash()
        {
            if(weekFromNow == 0)
            {
                UpdateDate();
            }
            else
            {
                Brush brush = new SolidColorBrush(Colors.Black);
                SUN.Foreground = brush;
                MON.Foreground = brush;
                TUE.Foreground = brush;
                WED.Foreground = brush;
                THU.Foreground = brush;
                FRI.Foreground = brush;
                SAT.Foreground = brush;
                SUN_DAY.Foreground = brush;
                MON_DAY.Foreground = brush;
                TUE_DAY.Foreground = brush;
                WED_DAY.Foreground = brush;
                THU_DAY.Foreground = brush;
                FRI_DAY.Foreground = brush;
                SAT_DAY.Foreground = brush;
                SUN_CDAY.Foreground = brush;
                MON_CDAY.Foreground = brush;
                TUE_CDAY.Foreground = brush;
                WED_CDAY.Foreground = brush;
                THU_CDAY.Foreground = brush;
                FRI_CDAY.Foreground = brush;
                SAT_CDAY.Foreground = brush;
                
                weekTitleGrid.Children.Remove(FindName("markerGrid") as Grid);

                SUN_DAY.Text = DateTime.Now.AddDays(-((double)DateTime.Now.DayOfWeek) + (weekFromNow * 7)).Day.ToString();
                MON_DAY.Text = DateTime.Now.AddDays(-((double)DateTime.Now.DayOfWeek) + 1 + (weekFromNow * 7)).Day.ToString();
                TUE_DAY.Text = DateTime.Now.AddDays(-((double)DateTime.Now.DayOfWeek) + 2 + (weekFromNow * 7)).Day.ToString();
                WED_DAY.Text = DateTime.Now.AddDays(-((double)DateTime.Now.DayOfWeek) + 3 + (weekFromNow * 7)).Day.ToString();
                THU_DAY.Text = DateTime.Now.AddDays(-((double)DateTime.Now.DayOfWeek) + 4 + (weekFromNow * 7)).Day.ToString();
                FRI_DAY.Text = DateTime.Now.AddDays(-((double)DateTime.Now.DayOfWeek) + 5 + (weekFromNow * 7)).Day.ToString();
                SAT_DAY.Text = DateTime.Now.AddDays(-((double)DateTime.Now.DayOfWeek) + 6 + (weekFromNow * 7)).Day.ToString();
                SUN_CDAY.Text = BLL.TimeTools.GetChineseDate(DateTime.Now.AddDays(-((double)DateTime.Now.DayOfWeek) + (weekFromNow * 7)));
                MON_CDAY.Text = BLL.TimeTools.GetChineseDate(DateTime.Now.AddDays(-((double)DateTime.Now.DayOfWeek) + 1 + (weekFromNow * 7)));
                TUE_CDAY.Text = BLL.TimeTools.GetChineseDate(DateTime.Now.AddDays(-((double)DateTime.Now.DayOfWeek) + 2 + (weekFromNow * 7)));
                WED_CDAY.Text = BLL.TimeTools.GetChineseDate(DateTime.Now.AddDays(-((double)DateTime.Now.DayOfWeek) + 3 + (weekFromNow * 7)));
                THU_CDAY.Text = BLL.TimeTools.GetChineseDate(DateTime.Now.AddDays(-((double)DateTime.Now.DayOfWeek) + 4 + (weekFromNow * 7)));
                FRI_CDAY.Text = BLL.TimeTools.GetChineseDate(DateTime.Now.AddDays(-((double)DateTime.Now.DayOfWeek) + 5 + (weekFromNow * 7)));
                SAT_CDAY.Text = BLL.TimeTools.GetChineseDate(DateTime.Now.AddDays(-((double)DateTime.Now.DayOfWeek) + 6 + (weekFromNow * 7)));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            switch (button.Content)
            {
                case "<":
                    weekFromNow -= 1;
                    break;
                case ">":
                    weekFromNow += 1;
                    break;
                default:
                    break;
            }
            Reflash();
            ReflashEvents();
        }

        private List<Schedule> schedulesList;

        private void ReflashEvents()
        {
            contentShowGrid.Children.Clear();
            DateTime dateTime = DateTime.Now.AddDays(-((double)DateTime.Now.DayOfWeek) + (Calender_Calender.weekFromNow * 7));
            schedulesList = mySQLiteTools.SearchSchedule(dateTime);
            foreach (var schedule in schedulesList)
            {
                Grid newGrid = new Grid
                {
                    VerticalAlignment = VerticalAlignment.Top,
                };
                TextBlock titleText = new TextBlock();
                titleText.HorizontalAlignment = HorizontalAlignment.Left;
                titleText.VerticalAlignment = VerticalAlignment.Top;
                Thickness titleThickness = new Thickness(20, 10, 0, 0);
                titleText.Margin = titleThickness;
                titleText.Foreground = new SolidColorBrush(Colors.White);
                titleText.Text = schedule.Name;

                TextBlock addressText = new TextBlock();
                addressText.HorizontalAlignment = HorizontalAlignment.Left;
                addressText.VerticalAlignment = VerticalAlignment.Top;
                Thickness addressTextThickness = new Thickness(20, 30, 0, 0);
                addressText.Margin = addressTextThickness;
                addressText.Foreground = new SolidColorBrush(Colors.White);
                addressText.Text = schedule.Address;
                if (schedule.StartDay == schedule.EndDay)
                {
                    newGrid.Height = (schedule.EndHour - schedule.StartHour) * 60 + schedule.EndMinute + 60 - schedule.StartMinute;
                    Thickness thickness = new Thickness(0, schedule.StartHour * 60 + schedule.StartMinute, 0, 0);
                    newGrid.Margin = thickness;
                    Tools.ColorHelpTools colorHelpTools = new Tools.ColorHelpTools();
                    AcrylicBrush brush = new AcrylicBrush
                    {
                        BackgroundSource = AcrylicBackgroundSource.HostBackdrop,
                        TintColor = colorHelpTools.ColorHx16toRGB(schedule.Color),
                        FallbackColor = colorHelpTools.ColorHx16toRGB(schedule.Color),
                        TintOpacity = 0.8
                    };
                    newGrid.Background = brush;
                    newGrid.Children.Add(titleText);
                    newGrid.Children.Add(addressText);
                    contentShowGrid.Children.Add(newGrid);

                    newGrid.SetValue(Grid.ColumnProperty, schedule.StartDay - dateTime.Day + 1);
                    newGrid.SetValue(Grid.RowSpanProperty, 24);
                    newGrid.Name = schedule.ID.ToString();
                    newGrid.Tapped += Grid_Tapped;
                }
            }
            //Tools.Dialog.ShowSimpleDialog("提示", count.ToString());
        }

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Grid thisGrid = sender as Grid;
            Schedule editSchedule = null;
            foreach (Schedule schedule in schedulesList)
            {
                if(schedule.ID.ToString() == thisGrid.Name)
                {
                    editSchedule = schedule;
                }
            }
            if (editSchedule != null)
            {
                var myPopup = new Views.EditSchedulePopupWindow(editSchedule);
                //myPopup.LeftClick += MyPopup_LeftClick;
                myPopup.ShowWIndow();
            }
        }
    }
}
