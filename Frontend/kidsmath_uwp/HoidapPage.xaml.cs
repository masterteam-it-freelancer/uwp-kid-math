using kidsmath_uwp.GetDatabaseServiceReference;
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
using static kidsmath_uwp.MainPage_AffterLogin;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace kidsmath_uwp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HoidapPage : Page
    {
        public HoidapPage()
        {
            this.InitializeComponent();
            Loaded += HoidapPage_Loaded;
        }

        private void HoidapPage_Loaded(object sender, RoutedEventArgs e)
        {
            txtbox_username.Text = UserSession.userlogined.username;
        }

        kidmathwebserviceSoapClient ws;
        private void naptien_vetrangchu(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage_AffterLogin));
        }

        private async void naptien_xacnhannap(object sender, RoutedEventArgs e)
        {
            if ((String.IsNullOrEmpty(txtbox_tieude.Text)))
            {
                await (new MessageDialog("Vui lòng nhập tiêu đề", "Error").ShowAsync());
            }
            else if ((String.IsNullOrEmpty(txtbox_noidung.Text)))
            {
                await (new MessageDialog("Vui lòng nhập nội dung", "Error").ShowAsync());
            }
            
            else thongbaoAsync();
        }
        private async System.Threading.Tasks.Task thongbaoAsync()
        {
            string capcha = txtbox_capcha.Text;
            DateTime now = DateTime.Now;
            if (txtbox_nhaplaicapcha.Text != capcha)
            {
                var msg = new MessageDialog("Nhập sai mã", "Error").ShowAsync();
                txtbox_capcha.Text = RandomString(6);
            }
            else
            {
                ws = new kidmathwebserviceSoapClient();
                string sql = "INSERT into thongBao values('phanhoi','c','" + UserSession.userlogined.username + "',N'"+txtbox_tieude.Text+"',N'"+txtbox_noidung+"','" + now + "',0);";
                if (ws.ThucHienLenhAsync(sql).Result.Body.ThucHienLenhResult != 0)
                {
                    var msg = new MessageDialog("Đã gửi câu hỏi đến bộ phận tư vấn");
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

