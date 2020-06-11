using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using kidsmath_uwp.GetDatabaseServiceReference;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace kidsmath_uwp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NaptienPage : Page
    {
        public NaptienPage()
        {
            this.InitializeComponent();
            Loaded += NaptienPage_Loaded;
        }

        private void NaptienPage_Loaded(object sender, RoutedEventArgs e)
        {
            
            txtbox_Naptien_username.Text = UserSession.userlogined.username;
            txtbox_capcha.Text = RandomString(6);
        }

        kidmathwebserviceSoapClient ws;
        private void naptien_vetrangchu(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage_AffterLogin));
        }

        private async void naptien_xacnhannap(object sender, RoutedEventArgs e)
        {
             if ((String.IsNullOrEmpty(txtbox_Naptien_username.Text)))
            {
                await(new MessageDialog("Vui lòng nhập tên người dùng", "Error").ShowAsync());
            }
            else if ((String.IsNullOrEmpty(txtbox_magiaodich.Text)))
            {
                await(new MessageDialog("Vui lòng nhập mã giao dịch", "Error").ShowAsync());
            }
            else if ((String.IsNullOrEmpty(txtbox_sotaikhoan.Text)))
            {
                await(new MessageDialog("Vui lòng nhập số tài khoản của bạn", "Error").ShowAsync());
            }
            else naptienAsync();
        }
        private async System.Threading.Tasks.Task naptienAsync()
        {
            string capcha = txtbox_capcha.Text;
            DateTime now = DateTime.Now;
            if(txtbox_nhaplaicapcha.Text != capcha)
            {
                var msg = new MessageDialog("Nhập sai mã", "Error").ShowAsync();
                txtbox_capcha.Text = RandomString(6);
            }
            else
            {
                ws = new kidmathwebserviceSoapClient();
                string sql = "INSERT into thongBao values('naptien','c','" + UserSession.userlogined.username + "',N'Yêu cầu nạp tiền',N'Số tiền: "+txtbox_sotien.Text+" User: " + UserSession.userlogined.username + "" +
                    " Mã giao dịch: "+txtbox_magiaodich.Text+" Số tài khoản: "+txtbox_sotaikhoan.Text+"','"+now+"',"+txtbox_sotien.Text+");";
                if (ws.ThucHienLenhAsync(sql).Result.Body.ThucHienLenhResult != 0)
                {
                    var msg = new MessageDialog("Đã gửi yêu cầu nạp tới Admin");
                    msg.Commands.Add(new UICommand() { Id = 0, Label = "Tiếp tục" });
                    msg.Commands.Add(new UICommand() { Id = 1, Label = "Quay về trang chủ" });
                    var rs = await msg.ShowAsync();
                    if ((int)rs.Id == 0)
                    {
                        this.Frame.Navigate(typeof(NaptienPage));
                    }
                    else if ((int)rs.Id == 1)
                    {
                        this.Frame.Navigate(typeof(MainPage_AffterLogin));
                    }

                }
            }
        }
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789qwertyuiopasdfghjklzxcvbnm";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
