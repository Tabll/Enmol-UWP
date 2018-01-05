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

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace Enmol
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            MyInit();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            ConnectedAnimation imageAnimation =
                ConnectedAnimationService.GetForCurrentView().GetAnimation("BackToMain");
            if (imageAnimation != null)
            {
                imageAnimation.TryStart(UserInfoGridBackground);
            }
        }

        private void MyInit()
        {
            FillBackground(UserInfoGridBackground);//填充亚克力玻璃
            ShowVersion();//显示版本号
        }

        private void ShowVersion()
        {
            VersionTextBlock.Text = BLL.Tools.GetVersion();
        }

        private void FillBackground(Grid grid)
        {
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.Xaml.Media.XamlCompositionBrushBase"))
            {
                AcrylicBrush myBrush = new AcrylicBrush
                {
                    BackgroundSource = AcrylicBackgroundSource.Backdrop,
                    //myBrush.BackgroundSource = Windows.UI.Xaml.Media.AcrylicBackgroundSource.HostBackdrop;
                    TintColor = Color.FromArgb(255, 255, 255, 255),//亚克力颜色
                    FallbackColor = Color.FromArgb(255, 255, 255, 255),//失去焦点的时候的颜色
                    TintOpacity = 0.6//模糊度
                };
                grid.Background = myBrush;
            }
            else
            {
                SolidColorBrush myBrush = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                grid.Background = myBrush;
            }
        }

        private void Next_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            if (UserNameTextBox.Text == "")
            {
                Tools.Dialog.ShowSimpleDialog("提示", "用户名不能为空");
            }
            else
            {
                Next();
            }
        }

        private void Page_PreviewKeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key.ToString() == "Enter")
            {
                if (UserPasswordBox.Password != "" && UserNameTextBox.Text != "")
                {
                    Next();
                }
            }
        }

        private async void Next()
        {
            if (BLL.Check.CheckUserWordAndPasswordEnter(UserNameTextBox.Text, UserPasswordBox.Password))
            {
                BLL.LocalSettings localSettings = new BLL.LocalSettings();
                BLL.PostMessages signInMessage = new BLL.PostMessages();
                
                localSettings.SaveLocalSettings("Phone", UserNameTextBox.Text);
                localSettings.SaveLocalSettings("Password", UserPasswordBox.Password);
                
                if (localSettings.IsSetLocalSettings("Token"))
                {
                    signInMessage.AddPostInfomation("sign-in-state", "2");
                    signInMessage.AddPostInfomation("token", localSettings.GetLocalSettings("Token"));
                    signInMessage.AddPostInfomation("user-password", UserPasswordBox.Password);
                }
                else
                {
                    signInMessage.AddPostInfomation("sign-in-state", "1");
                    signInMessage.AddPostInfomation("user-password", UserPasswordBox.Password);
                }
                signInMessage.AddPostInfomation("user-phone-number", UserNameTextBox.Text);
                signInMessage.SetUri("signin.php");
                signInMessage.AddPostHeader("user", "uwp-user");
                string result = await signInMessage.SendMessages();
                BLL.JsonTools jsonObject = new BLL.JsonTools(result);

                if(jsonObject.GetJson("State") == "SUCCESS")
                {
                    if(jsonObject.GetJson("NeedUpdate") == "TRUE")
                    {
                        localSettings.SaveLocalSettings("Token", jsonObject.GetJson("Token"));
                    }
                    Tools.Dialog.ShowSimpleDialog("提示", jsonObject.GetJson("State"));
                }
                else
                {
                    Tools.Dialog.ShowSimpleDialog("提示", jsonObject.GetJson("WARNING"));
                }
            }
            else
            {
                Tools.Dialog.ShowSimpleDialog("提示", "错误的用户名格式");
            }
            
        }

        private void TextBlock_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            switch (textBlock.Name)
            {
                case "SignUpTextBlock":
                    JumpToSignUpPage();
                    break;
                case "ForgetPasswordTextBlock":
                    JumpToSignUpPage();
                    //Tools.Dialog.ShowSimpleDialog("提示", textBlock.Name);
                    break;
                case "VersionTextBlock":
                    Tools.Dialog.ShowSimpleDialog("提示", textBlock.Name);
                    break;
                default:
                    Tools.Dialog.ShowSimpleDialog("警告", "程序出了问题");
                    break;
            }
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

        private void JumpToSignUpPage()
        {
            ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("image", UserInfoGridBackground);
            Frame.Navigate(typeof(Views.SignUp));
        }
    }
}
