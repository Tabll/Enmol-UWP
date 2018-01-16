using Model;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace Enmol.Views
{
    public sealed partial class EditSchedulePopupWindow : UserControl
    {
        private Popup m_Popup;
        private Schedule thisSchedule;

        public EditSchedulePopupWindow()
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

        public EditSchedulePopupWindow(Schedule schedule) : this()
        {
            thisSchedule = schedule;
            eventTitleTextBox.Text = schedule.Name;
            allDayEventToggleSwitch.IsOn = (schedule.StartHour == schedule.EndHour && schedule.StartMinute == schedule.EndMinute);
            startTimeDatePicker.Date = new DateTimeOffset(schedule.StartYear, schedule.StartMonth, schedule.StartDay, 1, 1, 1, new TimeSpan(0, 0, 0));
            endTimeDatePicker.Date = new DateTimeOffset(schedule.EndYear, schedule.EndMonth, schedule.EndDay, 1, 1, 1, new TimeSpan(0, 0, 0));
            startTimeTimePicker.Time = new TimeSpan(schedule.StartHour, schedule.StartMinute, 0);
            endTimeTimePicker.Time = new TimeSpan(schedule.EndHour, schedule.EndMinute, 0);
            repeatToggleSwitch.IsOn = schedule.RepeatType != 0;
            //repeatTypeComboBox.SelectedValue = schedule.RepeatType
            repeatCycleCountTextBlock.Text = schedule.RepeatCount.ToString();
            repeatEndTimeDatePicker.Date = new DateTimeOffset(schedule.RepeatEndYear, schedule.RepeatEndMonth, schedule.RepeatEndDay, 1, 1, 1, new TimeSpan(0, 0, 0));
            repeatAlwaysToggleSwitch.IsOn = schedule.RepeatEndYear == 9999;
            addressEditTextBox.Text = schedule.Address;
            eventTypeChoiceComboBox.SelectedValue = schedule.Type;
            tipsEditTextBox.Text = schedule.Remark;
            eventShowInfoEditTextBox.Text = schedule.ShowAs;
            colorChoiceShowRectangle.Fill = new SolidColorBrush(new Tools.ColorHelpTools().ColorHx16toRGB(schedule.Color));
            colorChoiceColorPicker.Color = new Tools.ColorHelpTools().ColorHx16toRGB(schedule.Color);
            isEventSecretToggleSwitch.IsOn = schedule.Secret;
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
                UpdateData();
                DismissWindow();
                LeftClick?.Invoke(this, e);
            }
            else
            {
                if(eventTitleTextBox.Text == "")
                {
                    BLL.SQLiteTools mySQLiteTools = new BLL.SQLiteTools();
                    mySQLiteTools.DeleteSchedule(thisSchedule.ID);
                }
                else
                {
                    Tools.Dialog.ShowSimpleDialog("警告", "请填写事件名称、检查重复截止日期的合理性");
                }
            }
        }

        //数据合理性检验
        private bool DataWriteCompleted()
        {
            return eventTitleTextBox.Text != ""
                && !((repeatToggleSwitch.IsOn && repeatEndTimeDatePicker.Date < endTimeDatePicker.Date));
        }

        private int GetRepeatType()
        {
            if (repeatToggleSwitch.IsOn)
            {
                switch ($"{repeatTypeComboBox.SelectedValue}")
                {
                    case "按天重复":
                        return 1;
                    case "按周重复":
                        return 2;
                    case "按月重复":
                        return 3;
                    case "按年重复":
                        return 4;
                    default:
                        return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        private int GetRepeatCount()
        {
            try
            {
                return int.Parse(repeatCycleCountTextBlock.Text);
            }
            catch (Exception)
            {
                return 1;
            }
        }

        private int GetRepeatEndYear()
        {
            return repeatAlwaysToggleSwitch.IsOn ? 9999 : repeatEndTimeDatePicker.Date.Year;
        }

        private void UpdateData()
        {
            BLL.SQLiteTools mySQLiteTools = new BLL.SQLiteTools();
            Tools.ColorHelpTools colorHelpTools = new Tools.ColorHelpTools();

            thisSchedule.Name = eventTitleTextBox.Text;
            thisSchedule.StartYear = startTimeDatePicker.Date.Year;
            thisSchedule.StartMonth = startTimeDatePicker.Date.Month;
            thisSchedule.StartDay = startTimeDatePicker.Date.Day;
            thisSchedule.StartHour = startTimeTimePicker.Time.Hours;
            thisSchedule.StartMinute = startTimeTimePicker.Time.Minutes;
            thisSchedule.EndYear = endTimeDatePicker.Date.Year;
            thisSchedule.EndMonth = endTimeDatePicker.Date.Month;
            thisSchedule.EndDay = endTimeDatePicker.Date.Day;
            thisSchedule.EndHour = endTimeTimePicker.Time.Hours;
            thisSchedule.EndMinute = endTimeTimePicker.Time.Minutes;
            thisSchedule.Address = addressEditTextBox.Text;
            thisSchedule.Color = colorHelpTools.ToHexColor(colorChoiceColorPicker.Color);
            thisSchedule.Type = $"{eventTypeChoiceComboBox.SelectedValue}";
            thisSchedule.Remark = tipsEditTextBox.Text;
            thisSchedule.Secret = isEventSecretToggleSwitch.IsOn;
            thisSchedule.ShowAs = eventShowInfoEditTextBox.Text;
            thisSchedule.RepeatType = GetRepeatType();
            thisSchedule.RepeatCount = GetRepeatCount();
            thisSchedule.RepeatStartYear = startTimeDatePicker.Date.Year;
            thisSchedule.RepeatStartMonth = startTimeDatePicker.Date.Month;
            thisSchedule.RepeatStartDay = startTimeDatePicker.Date.Day;
            thisSchedule.RepeatEndYear = GetRepeatEndYear();
            thisSchedule.RepeatEndMonth = repeatEndTimeDatePicker.Date.Month;
            thisSchedule.RepeatEndDay = repeatEndTimeDatePicker.Date.Day;

            mySQLiteTools.UpdateSchedule(thisSchedule);
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
