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
using kidsmath_uwp.GetDatabaseServiceReference;
using Windows.UI.Popups;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace kidsmath_uwp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RegistryPage : Page
      
    {
        kidmathwebserviceSoapClient ws;

        public RegistryPage()
        {
            this.InitializeComponent();
           
        }

        private async void click_hoanthanhdangky(object sender, RoutedEventArgs e)
        {
            if ((String.IsNullOrEmpty(txtbox_username.Text)))
            {
                await (new MessageDialog("Vui lòng nhập tên người dùng", "Error").ShowAsync());
            }
            else if (checkusername(txtbox_username.Text) != 0)
            {
                await (new MessageDialog("Username đã được sử dụng, vui lòng sử dụng username khác", "Error").ShowAsync());
            }
            else if ((String.IsNullOrEmpty(txtbox_hovaten.Text)))
            {
                await (new MessageDialog("Vui lòng nhập tên người dùng","Error").ShowAsync());
            }
            else if ((String.IsNullOrEmpty(txtbox_sdt.Text)))
            {
                await (new MessageDialog("Vui lòng nhập số điện thoại", "Error").ShowAsync());
            }
            else if ((String.IsNullOrEmpty(passwdbox_matkhau.Password.ToString())))
            {
                await (new MessageDialog("Vui lòng nhập mật khẩu", "Error").ShowAsync());
            }


            else insertUser();

        }
        private async void insertUser()
        {
            ws = new kidmathwebserviceSoapClient();
            string sql = "INSERT into nguoiDung values('" + txtbox_username.Text + "','" + passwdbox_xacnhanmatkhau.Password.ToString() + "'" +
                ",'" + txtbox_hovaten.Text + "','" + txtbox_sdt.Text + "','" + txtbox_email.Text + "',0,0,'U');";
            int ketqua = ws.ThucHienLenhAsync(sql).Result.Body.ThucHienLenhResult;
            if (ketqua != 0)
            {
                await dangkythanhcongAsync();
            }
            else if (passwdbox_matkhau.Password.ToString() != passwdbox_xacnhanmatkhau.Password.ToString())
            {

                await baoloidangky_password();
            }
            else await baoloidangky_null();
        }
        private async System.Threading.Tasks.Task baoloidangky_null()
        {
            var msg = new MessageDialog("Vui lòng điền đầy đủ thông tin tin");

            msg.Commands.Add(new UICommand() { Id = 0, Label = "Đồng ý" });
            var rs = await msg.ShowAsync();

        }
        private async System.Threading.Tasks.Task baoloidangky_password()
        {
            var msg = new MessageDialog("Mật khẩu không khớp, vui lòng nhập lại");

            msg.Commands.Add(new UICommand() { Id = 0, Label = "Đồng ý" });
            var rs = await msg.ShowAsync();
        }
        private async System.Threading.Tasks.Task dangkythanhcongAsync()
        {
            
                var msg = new MessageDialog("Đăng ký thành công !");

                msg.Commands.Add(new UICommand() { Id = 0, Label = "Đăng nhập" });

                msg.Commands.Add(new UICommand() { Id = 1, Label = "Hủy" });

                var rs = await msg.ShowAsync();

                if ((int)rs.Id == 0)
                {                   
                    this.Frame.Navigate(typeof(LoginPage));
                }

            }
        private int checkusername(string username)
        {
            string sql = "SELECT * from nguoiDung WHERE username = '" + username + "';";
            ws = new kidmathwebserviceSoapClient();
            int check = ws.checkExistsAsync(sql).Result.Body.checkExistsResult;
            return check;
        }

        private void dangnhap_click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(LoginPage));
        }

        private void vetrangchu_click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
    }

