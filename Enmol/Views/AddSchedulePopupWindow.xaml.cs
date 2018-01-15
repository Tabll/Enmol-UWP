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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

//https://go.microsoft.com/fwlink/?LinkId=234236 上介绍了“用户控件”项模板

namespace Enmol.Views
{
    public sealed partial class AddSchedulePopupWindow : UserControl
    {
        private Popup m_Popup;
        private string m_TextBlockContent;

        public AddSchedulePopupWindow()
        {
            this.InitializeComponent();

            m_Popup = new Popup();
            this.Width = Window.Current.Bounds.Width;
            this.Height = Window.Current.Bounds.Height;
            m_Popup.Child = this;
            this.Loaded += MessagePopupWindow_Loaded;
            this.Unloaded += MessagePopupWindow_Unloaded;

            endTimeDatePicker.MinYear = startTimeDatePicker.Date;
        }

        public AddSchedulePopupWindow(string showMsg) : this()
        {
            this.m_TextBlockContent = showMsg;
        }

        private void MessagePopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //this.tbContent.Text = m_TextBlockContent;
            Window.Current.SizeChanged += MessagePopupWindow_SizeChanged; ;
        }
        private void MessagePopupWindow_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            this.Width = e.Size.Width;
            this.Height = e.Size.Height;
        }
        private void MessagePopupWindow_Unloaded(object sender, RoutedEventArgs e)
        {
            Window.Current.SizeChanged -= MessagePopupWindow_SizeChanged; ;
        }
        public void ShowWIndow()
        {
            m_Popup.IsOpen = true;
        }
        private void DismissWindow()
        {
            //PopupAnimation.Fade;
            //var storyBoard = new Storyboard();
            m_Popup.IsOpen = false;
        }
        private void LeftButton_Click(object sender, RoutedEventArgs e)
        {
            DismissWindow();
            LeftClick?.Invoke(this, e);
            //ChangeStudentInformation();
        }
        private void RightButton_Click(object sender, RoutedEventArgs e)
        {
            DismissWindow();
            RightClick?.Invoke(this, e);
        }

        public event EventHandler<RoutedEventArgs> LeftClick;
        public event EventHandler<RoutedEventArgs> RightClick;

        private void OutBorder_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            DismissWindow();
        }

        private void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            ToggleSwitch toggleSwitch = sender as ToggleSwitch;

            switch (toggleSwitch.Name)
            {
                case "allDayEventToggleSwitch":
                    if (toggleSwitch.IsOn)
                    {
                        startTimeTimePicker.IsEnabled = false;
                        endTimeTimePicker.IsEnabled = false;
                        endTimeDatePicker.Date = new DateTimeOffset(DateTime.Now.AddDays(1));
                        startTimeTimePicker.Time = new TimeSpan(0, 0, 0);
                        endTimeTimePicker.Time = new TimeSpan(0, 0, 0);
                    }
                    else
                    {
                        startTimeTimePicker.IsEnabled = true;
                        endTimeTimePicker.IsEnabled = true;
                        endTimeDatePicker.Date = new DateTimeOffset(DateTime.Now);
                        startTimeTimePicker.Time = new TimeSpan(DateTime.Now.Hour, 0, 0);
                        endTimeTimePicker.Time = new TimeSpan(DateTime.Now.Hour + 1, 0, 0);
                    }

                    break;
                case "repeatToggleSwitch":
                    if (toggleSwitch.IsOn)
                    {
                        repeatTypeListViewItem.Visibility = Visibility.Visible;
                        repeatCycleListViewItem.Visibility = Visibility.Visible;
                        repeatUntilListViewItem.Visibility = Visibility.Visible;
                        repeatAlwaysListViewItem.Visibility = Visibility.Visible;
                        if ($"{repeatTypeComboBox.SelectedValue}" == "")
                        {
                            repeatTypeComboBox.SelectedValue = "按天重复";
                        }
                        repeatEndTimeTimePicker.Time = new TimeSpan(23, 59, 59);
                    }
                    else
                    {
                        repeatTypeListViewItem.Visibility = Visibility.Collapsed;
                        repeatCycleListViewItem.Visibility = Visibility.Collapsed;
                        repeatUntilListViewItem.Visibility = Visibility.Collapsed;
                        repeatAlwaysListViewItem.Visibility = Visibility.Collapsed;
                    }
                    break;
                case "repeatAlwaysToggleSwitch":
                    if (toggleSwitch.IsOn)
                    {
                        repeatEndTimeDatePicker.IsEnabled = false;
                    }
                    else
                    {
                        repeatEndTimeDatePicker.IsEnabled = true;
                    }
                    break;
                default:
                    Tools.Dialog.ShowSimpleDialog("测试", "点击了？？");
                    break;
            }


        }

        private void RepeatTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            switch ($"{comboBox.SelectedValue}")
            {
                case "按天重复":
                    repeatCycleUnitTextBlock.Text = "天";
                    repeatEndTimeDatePicker.Date = new DateTimeOffset(DateTime.Now.AddDays(1));
                    break;
                case "按周重复":
                    repeatCycleUnitTextBlock.Text = "周";
                    repeatEndTimeDatePicker.Date = new DateTimeOffset(DateTime.Now.AddDays(7));
                    break;
                case "按月重复":
                    repeatCycleUnitTextBlock.Text = "月";
                    repeatEndTimeDatePicker.Date = new DateTimeOffset(DateTime.Now.AddMonths(1));
                    break;
                case "按年重复":
                    repeatCycleUnitTextBlock.Text = "年";
                    repeatEndTimeDatePicker.Date = new DateTimeOffset(DateTime.Now.AddYears(1));
                    break;
                default:
                    break;
            }
            //Tools.Dialog.ShowSimpleDialog("提示", $"{comboBox.SelectedValue}");
        }

        private void ColorPicker_ColorChanged(ColorPicker sender, ColorChangedEventArgs args)
        {
            colorChoiceIcon.Foreground = new SolidColorBrush(sender.Color);
            AcrylicBrush myBrush = new AcrylicBrush
            {
                BackgroundSource = AcrylicBackgroundSource.HostBackdrop,
                TintColor = sender.Color,
                FallbackColor = sender.Color,
                TintOpacity = 0.8
            };
            colorChoiceShowRectangle.Fill = myBrush;
            //if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.Xaml.Media.XamlCompositionBrushBase"))
            //{
            //    AcrylicBrush myBrush = new AcrylicBrush
            //    {
            //        BackgroundSource = AcrylicBackgroundSource.HostBackdrop,
            //        TintColor = sender.Color,
            //        FallbackColor = sender.Color,
            //        TintOpacity = 0.8
            //    };
            //    colorChoiceShowRectangle.Fill = myBrush;
            //}
            //else
            //{
            //    SolidColorBrush myBrush = new SolidColorBrush(Color.FromArgb(255, 202, 24, 37));
            //    colorChoiceShowRectangle.Fill = myBrush;
            //}
        }

        private void ColorRectangle_Tapped(object sender, TappedRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (DataWriteCompleted())
            {
                DismissWindow();
                LeftClick?.Invoke(this, e);
            }
            else
            {
                Tools.Dialog.ShowSimpleDialog("警告", "请填写事件名称、检查重复截止日期的合理性");
            }
        }

        //数据合理性检验
        private bool DataWriteCompleted()
        {
            return eventTitleTextBox.Text != ""
                && endTimeDatePicker.Date > startTimeDatePicker.Date
                && !(endTimeDatePicker.Date.Year == startTimeDatePicker.Date.Year && endTimeDatePicker.Date.Month == startTimeDatePicker.Date.Month && endTimeDatePicker.Date.Day == startTimeDatePicker.Date.Day && endTimeTimePicker.Time < startTimeTimePicker.Time)
                && !((repeatToggleSwitch.IsOn && repeatEndTimeDatePicker.Date < endTimeDatePicker.Date));
        }

        private void InsertData()
        {
            BLL.SQLiteTools mySQLiteTools = new BLL.SQLiteTools();

        }

        private void StartTimeDatePicker_DateChanged(object sender, DatePickerValueChangedEventArgs e)
        {
            if(endTimeDatePicker.Date < startTimeDatePicker.Date)
            {
                endTimeDatePicker.Date = startTimeDatePicker.Date;
            }
            endTimeDatePicker.MinYear = startTimeDatePicker.Date;
        }

        private void StartTimeTimePicker_TimeChanged(object sender, TimePickerValueChangedEventArgs e)
        {
            if (endTimeDatePicker.Date.Year == startTimeDatePicker.Date.Year && endTimeDatePicker.Date.Month == startTimeDatePicker.Date.Month && endTimeDatePicker.Date.Day == startTimeDatePicker.Date.Day && endTimeTimePicker.Time < startTimeTimePicker.Time)
            {
                endTimeTimePicker.Time = startTimeTimePicker.Time;
            }
        }

        private void EndTimeDatePicker_DateChanged(object sender, DatePickerValueChangedEventArgs e)
        {
            if (endTimeDatePicker.Date < startTimeDatePicker.Date)
            {
                endTimeDatePicker.Date = startTimeDatePicker.Date;
                Tools.Dialog.ShowSimpleDialog("警告", "结束日期小于起始日期，请重新选择");
            }
        }

        private void EndTimeTimePicker_TimeChanged(object sender, TimePickerValueChangedEventArgs e)
        {
            if (endTimeDatePicker.Date.Year == startTimeDatePicker.Date.Year && endTimeDatePicker.Date.Month == startTimeDatePicker.Date.Month && endTimeDatePicker.Date.Day == startTimeDatePicker.Date.Day && endTimeTimePicker.Time < startTimeTimePicker.Time)
            {
                endTimeTimePicker.Time = startTimeTimePicker.Time;
                Tools.Dialog.ShowSimpleDialog("警告", "结束时间小于起始时间，请重新选择");
            }
        }
    }
}
