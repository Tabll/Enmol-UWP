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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Enmol.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class SignUp : Page
    {
        public SignUp()
        {
            this.InitializeComponent();
            MyInit();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            ConnectedAnimation imageAnimation =
                ConnectedAnimationService.GetForCurrentView().GetAnimation("image");
            if (imageAnimation != null)
            {
                imageAnimation.TryStart(UserInfoGridBackground);
            }
        }

        private void MyInit()
        {
            //ImageBrush imageBrush = new ImageBrush();
            //imageBrush.ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Background/MainBackground.png", UriKind.Absolute));
            //imageBrush.Stretch = Stretch.UniformToFill;
            //MainBackgroundGrid.Background = imageBrush;
            new Tools.FillBackground(UserInfoGridBackground);//填充亚克力玻璃
        }

        private void TextBlock_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            textBlock.Foreground = new SolidColorBrush(Color.FromArgb(255, 113, 97, 82));
        }

        private void TextBlock_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            textBlock.Foreground = new SolidColorBrush(Colors.White);
        }

        private void BackTextBlock_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("BackToMain", UserInfoGridBackground);
            //Frame.Navigate(typeof(MainPage));

            Frame.GoBack();
        }

        private void GetVertifyCodeButton_Click(object sender, RoutedEventArgs e)
        {
            if (BLL.Check.CheckPhoneNumber(TelephoneNumberTextBox.Text))
            {
                GetVertifyCodeButton.IsEnabled = false;
                TelephoneNumberTextBox.IsEnabled = false;
                GetVertifyCode();//暂时停止发送短信的功能
                SetTime();
            }
            else
            {
                Tools.Dialog.ShowSimpleDialog("警告", "手机号不符合规则");
            }
        }

        private void SetTime()
        {
            int i = 0;
            int TimeCount = 60;//开始秒数
            DispatcherTimer timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 1) };
            timer.Tick += new EventHandler<object>(async (sender, e) =>
            {
                await Dispatcher.TryRunAsync(CoreDispatcherPriority.Normal, new DispatchedHandler(() =>
                {
                    i -= 1;
                    double temp = (385 * Math.PI) * i / TimeCount / 15;
                    //MyEllipse.StrokeDashArray = new DoubleCollection() { temp, 1000 };
                    GetVertifyCodeButton.Content = ((TimeCount + i) % 60).ToString();
                    if (TimeCount + i == 0)
                    {
                        GetVertifyCodeButton.Content = "获取验证码";
                        GetVertifyCodeButton.IsEnabled = true;
                        timer.Stop();
                    }
                }));
            });
            timer.Start();
        }//时间更新线程

        private async void GetVertifyCode()
        {
            BLL.PostMessages getVertifyCodeMessage = new BLL.PostMessages();
            getVertifyCodeMessage.AddPostInfomation("sign-up-state", "1");
            getVertifyCodeMessage.AddPostInfomation("user-phone-number", TelephoneNumberTextBox.Text);
            getVertifyCodeMessage.SetUri("signup.php");
            getVertifyCodeMessage.AddPostHeader("user", "uwp-user");
            string result = await getVertifyCodeMessage.SendMessages();
            if (result == "success")
            {
                Tools.Dialog.ShowSimpleDialog("提示", "短信已发送成功，请填写验证码");
            }
            else
            {
                Tools.Dialog.ShowSimpleDialog("提示", result);
            }
        }

        private void NextImage_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            if (TelephoneNumberTextBox.IsEnabled)
            {
                Tools.Dialog.ShowSimpleDialog("提示", "请输入正确的手机号和验证码");
            }
            else
            {
                if(VertifyCodeBox.Text == "")
                {
                    Tools.Dialog.ShowSimpleDialog("提示", "请填写验证码");
                }
                else
                {
                    VerifyCode();
                    
                }
            }
        }

        private async void VerifyCode()
        {
            BLL.PostMessages verifyCodeMessage = new BLL.PostMessages();
            verifyCodeMessage.AddPostInfomation("sign-up-state", "2");
            verifyCodeMessage.AddPostInfomation("user-phone-number", TelephoneNumberTextBox.Text);
            verifyCodeMessage.AddPostInfomation("user-verify-code", VertifyCodeBox.Text);
            verifyCodeMessage.SetUri("signup.php");
            verifyCodeMessage.AddPostHeader("user", "uwp-user");
            string result = await verifyCodeMessage.SendMessages();
            if (result == "success")
            {
                SetPassword.SetVerifyCode(VertifyCodeBox.Text);
                SetPassword.SetPhoneNumber(TelephoneNumberTextBox.Text);

                ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("image", UserInfoGridBackground);
                Frame.Navigate(typeof(SetPassword));
            }
            else
            {
                Tools.Dialog.ShowSimpleDialog("提示", "验证失败");
            }
        }
    }
}
