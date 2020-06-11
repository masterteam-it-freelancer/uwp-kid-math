using kidsmath_uwp.GetDatabaseServiceReference;
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
using Windows.UI.Popups;
using static kidsmath_uwp.MainPage_AffterLogin;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace kidsmath_uwp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
        }
        kidmathwebserviceSoapClient ws;

        public static string username = "";
        public static string password = "";
         async private void Btn_Dangnhap_Click(object sender, RoutedEventArgs e)
        {
            username = Txtbox_Username.Text.ToString();
            password = Passwdbox_Password.Password.ToString();
            if (username == "")
            {
                await (new MessageDialog("Vui lòng nhập tên người dùng","Thông báo").ShowAsync());
                return;
            }
            if (password == "")
            {
                await (new MessageDialog("Vui lòng nhập mật khẩu", "Thông báo").ShowAsync());
                return;
            }
                ws = new kidmathwebserviceSoapClient();
                List<nguoiDung> listnguoidung = new List<nguoiDung>();
                listnguoidung = ws.getDataNguoiDungAsync().Result.Body.getDataNguoiDungResult.ToList<nguoiDung>();
                nguoiDung sessionuser= listnguoidung.Find(user => user.username == username && user.password == password);
                if(sessionuser == null)
                {
                await (new MessageDialog("Tên tài khoản hoặc mật khẩu không chính xác","Error").ShowAsync());
                }
                else
                {
                UserSession.userlogined = sessionuser;
                if (sessionuser.kieuThanhVien == "B") { var msg = new MessageDialog("Tài khoản của bạn đã bị khóa, liên hệ kidmath@userhelp.com để được hỗ trợ", "Error").ShowAsync(); }
                if (sessionuser.kieuThanhVien == "A") this.Frame.Navigate(typeof(AdminMainPage));
                if (sessionuser.kieuThanhVien == "U") this.Frame.Navigate(typeof(MainPage_AffterLogin));

                }

            
        }

        private void Btn_Dangky_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(RegistryPage));
        }

        private void Btn_Trovetrangchu_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
