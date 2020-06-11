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
using kidsmath_uwp.GetDatabaseServiceReference;
using Windows.UI.Xaml.Navigation;
using static kidsmath_uwp.MainPage_AffterLogin;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace kidsmath_uwp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Page_UserInfo : Page
    {
        public Page_UserInfo()
        {
            this.InitializeComponent();
            Loaded += Page_UserInfo_Loaded;
        }
        kidmathwebserviceSoapClient ws;
        private void Page_UserInfo_Loaded(object sender, RoutedEventArgs e)
        {
            ws = new kidmathwebserviceSoapClient();
            List<khoaHoc> dskhoahoc = new List<khoaHoc>();
            dskhoahoc = ws.getDataKhoaHocAsync().Result.Body.getDataKhoaHocResult.ToList<khoaHoc>();
            flipview_dskhoahoc.ItemsSource = dskhoahoc;



            txtblock_userloginedname.Text = UserSession.userlogined.tenNguoiDung;
            txtblock_email.Text = UserSession.userlogined.email;
            txtblock_soDienThoai.Text = UserSession.userlogined.soDienThoai;
            txtblock_tenNguoiDung.Text = UserSession.userlogined.tenNguoiDung;
            txtblock_username.Text = UserSession.userlogined.username;
            txtblock_soTienDaNap.Text = Convert.ToString(UserSession.userlogined.soTienDaNap) + "  ( VNĐ)";
            txtblock_soTienHienCo.Text = UserSession.userlogined.soTienHienCo + "  ( VNĐ)";




            ws = new kidmathwebserviceSoapClient();
            List<muaKhoaHoc> dslichsumua = new List<muaKhoaHoc>();
            dslichsumua = ws.getDatMuaKhoaHocAsync().Result.Body.getDatMuaKhoaHocResult.ToList<muaKhoaHoc>();
            dslichsumua = dslichsumua.FindAll(hoadon => hoadon.username == UserSession.userlogined.username);
            foreach (var oder in dslichsumua)
            {
                oder.giaKhoaHoc = - oder.giaKhoaHoc;
                if (oder.maKhoaHoc == "l1cb") { oder.maKhoaHoc = "Mua khóa lớp 1"; }
                else if (oder.maKhoaHoc == "l2cb") { oder.maKhoaHoc = "Mua khóa lớp 2"; }
                else if (oder.maKhoaHoc == "l3cb") { oder.maKhoaHoc = "Mua khóa lớp 3"; }
                else if (oder.maKhoaHoc == "l4cb") { oder.maKhoaHoc = "Mua khóa lớp 4"; }
                else if (oder.maKhoaHoc == "l5cb") { oder.maKhoaHoc = "Mua khóa lớp 5"; }
                else if (oder.maKhoaHoc == "ontapcb") { oder.maKhoaHoc = "Mua khóa ôn tập"; }
                else { oder.maKhoaHoc = "Không xác định"; }
            }
            gvlichsumua.ItemsSource = dslichsumua;

            ws = new kidmathwebserviceSoapClient();
            List<lichSuNapTien> dslichsunap = new List<lichSuNapTien>();
            dslichsunap = ws.getDataLichSuNapTienAsync().Result.Body.getDataLichSuNapTienResult.ToList<lichSuNapTien>();
            dslichsunap = dslichsunap.FindAll(hoadon => hoadon.username == UserSession.userlogined.username);
            
            gvlichsunap.ItemsSource = dslichsunap;
        }

        private void btnclick_MainMenu(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage_AffterLogin));
        }

        private void naptien(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(NaptienPage));
        }

       
        private void chinhsuathongtin(object sender, RoutedEventArgs e)
        {
            txtblock_password.IsEnabled = true;
            passwdbox_xacnhanmatkhau.IsEnabled = true;
            txtblock_tenNguoiDung.IsEnabled = true;
            txtblock_email.IsEnabled = true;
            txtblock_soDienThoai.IsEnabled = true;
            suathongtin.IsEnabled = false;
            suathongtin.Opacity = 0.5;
            huysuathongtin.Visibility = Visibility;
            luuthongtin.Visibility = Visibility;
            passwdbox_xacnhanmatkhau.Visibility = Visibility;
            stpn1.Opacity = 0.5;
            stpn2.Opacity = 0.5;
            txtblock_password.PlaceholderText = "Nhập mật khẩu mới";

        }

        private void tap_khoahoc(object sender, TappedRoutedEventArgs e)
        {

            ws = new kidmathwebserviceSoapClient();
            StackPanel khoahocduocchon = sender as StackPanel;
            khoaHoc temp = khoahocduocchon.DataContext as khoaHoc;
            Chinhsuakhoahoc.khoahoccanchinhsua = temp;
            temp = ws.getDataKhoaHocAsync().Result.Body.getDataKhoaHocResult.ToList<khoaHoc>().Find(kh => kh.maKhoaHoc == temp.maKhoaHoc);
            string sql = "SELECT * FROM khoaHoc WHERE muaKhoaHoc.username = '" + UserSession.userlogined.username + "' AND muaKhoaHoc.maKhoaHoc ='" + temp.maKhoaHoc + "';";
            ws = new kidmathwebserviceSoapClient();
            if (ws.checkExistsAsync(sql).Result.Body.checkExistsResult != 0)
            {

                ViewCoursePage_AfterBuy.khoahocduocmua = temp;
                this.Frame.Navigate(typeof(ViewCoursePage_AfterBuy));
            }
            else
            {
                BuyCoursePage.khoahocduocmua = temp;
                this.Frame.Navigate(typeof(BuyCoursePage));
            }
        }

        private async void luuchinhsuathongtin(object sender, RoutedEventArgs e)
        {
            if ((String.IsNullOrEmpty(txtblock_tenNguoiDung.Text)))
            {
                await (new MessageDialog("Vui lòng nhập tên người dùng", "Error").ShowAsync());
            }
            else if ((String.IsNullOrEmpty(txtblock_soDienThoai.Text)))
            {
                await (new MessageDialog("Vui lòng nhập số điện thoại", "Error").ShowAsync());
            }
            else if ((String.IsNullOrEmpty(txtblock_email.Text)))
            {
                await (new MessageDialog("Vui lòng nhập số điện thoại", "Error").ShowAsync());
            }
            else if ((String.IsNullOrEmpty(txtblock_password.ToString())))
            {
                await (new MessageDialog("Vui lòng nhập mật khẩu", "Error").ShowAsync());
            }


            else
            {
                updateUser();
               
            }
        }
    

        private void huychinhsuathongtin(object sender, RoutedEventArgs e)
        {
            txtblock_password.IsEnabled = false;
            passwdbox_xacnhanmatkhau.IsEnabled = false;
            txtblock_tenNguoiDung.IsEnabled = false;
            txtblock_email.IsEnabled = false;
            txtblock_soDienThoai.IsEnabled = false;
            suathongtin.IsEnabled = true;
            huysuathongtin.Visibility = Visibility.Collapsed;
            luuthongtin.Visibility = Visibility.Collapsed;
            passwdbox_xacnhanmatkhau.Visibility = Visibility.Collapsed;
            suathongtin.Opacity = 1;
            stpn1.Opacity = 1;
            stpn2.Opacity = 1;
            txtblock_password.PlaceholderText = "******";
        }
        private async void updateUser()
        {
            txtblock_password.IsEnabled = true;
            txtblock_tenNguoiDung.IsEnabled = true;
            txtblock_email.IsEnabled = true;
            txtblock_soDienThoai.IsEnabled = true;
            ws = new kidmathwebserviceSoapClient();
            string sql = " UPDATE nguoiDung SET nguoiDung.passwd = '" + txtblock_password.Text +
                         "', nguoiDung.tenNguoiDung = N'" + txtblock_tenNguoiDung.Text + "',nguoiDung.email = '" +
                         txtblock_email.Text + "',nguoiDung.soDienThoai= '" + txtblock_soDienThoai.Text +
                         "' WHERE nguoiDung.username = '" + UserSession.userlogined.username + "'; ";
            int ketqua = ws.ThucHienLenhAsync(sql).Result.Body.ThucHienLenhResult;
            if (ketqua != 0)
            {
                await dangkythanhcongAsync();
                txtblock_password.IsEnabled = false;
                passwdbox_xacnhanmatkhau.IsEnabled = false;
                txtblock_tenNguoiDung.IsEnabled = false;
                txtblock_email.IsEnabled = false;
                txtblock_soDienThoai.IsEnabled = false;
                suathongtin.IsEnabled = true;
                huysuathongtin.Visibility = Visibility.Collapsed;
                luuthongtin.Visibility = Visibility.Collapsed;
                suathongtin.Opacity = 1;
                stpn1.Opacity = 1;
                stpn2.Opacity = 1;
                txtblock_password.PlaceholderText = "******";
                passwdbox_xacnhanmatkhau.Visibility = Visibility.Collapsed;
                }
            else if (txtblock_password.Text != passwdbox_xacnhanmatkhau.Text)
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

            var msg = new MessageDialog("Cập nhật thông tin thành công !");

            msg.Commands.Add(new UICommand() { Id = 0, Label = "Trở về trang chủ" });

            msg.Commands.Add(new UICommand() { Id = 1, Label = "Hủy" });

            var rs = await msg.ShowAsync();

            if ((int)rs.Id == 0)
            {
                this.Frame.Navigate(typeof(MainPage_AffterLogin));
            }

        }
       

    }
}
