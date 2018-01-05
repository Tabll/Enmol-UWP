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

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Enmol.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class SetPassword : Page
    {

        private static string verifyCode = "";
        private static string phoneNumber = "";

        public SetPassword()
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
            //new Tools.FillBackground(UserInfoGridBackground);//填充亚克力玻璃
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
            Frame.Navigate(typeof(MainPage));
        }

        public static void SetVerifyCode(string code)
        {
            verifyCode = code;
        }

        public static void SetPhoneNumber(string number)
        {
            phoneNumber = number;
        }

        private void conformButton_Click(object sender, RoutedEventArgs e)
        {
            VertifyCode();
        }

        private async void VertifyCode()
        {
            if(verifyCode!="" && phoneNumber != "")
            {
                BLL.PostMessages setPasswordMessage = new BLL.PostMessages();
                setPasswordMessage.AddPostInfomation("sign-up-state", "3");
                setPasswordMessage.AddPostInfomation("user-phone-number", phoneNumber);
                setPasswordMessage.AddPostInfomation("user-verify-code", verifyCode);
                setPasswordMessage.AddPostInfomation("user-password", setPasswordBox.Password);
                setPasswordMessage.AddPostHeader("user", "uwp-user");

                setPasswordMessage.SetUri("signup.php");

                string result = await setPasswordMessage.SendMessages();

                if(result == "success")
                {
                    ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("BackToMain", UserInfoGridBackground);
                    Frame.Navigate(typeof(MainPage));
                    Tools.Dialog.ShowSimpleDialog("提示", "注册成功");
                }
                else
                {
                    Tools.Dialog.ShowSimpleDialog("提示", result);
                }

            }
            else
            {
                Tools.Dialog.ShowSimpleDialog("警告", "ERROR：9001");
            }
        }


    }
}
