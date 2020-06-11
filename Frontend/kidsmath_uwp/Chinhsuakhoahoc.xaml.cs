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
using Windows.UI.Xaml.Media.Imaging;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace kidsmath_uwp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Chinhsuakhoahoc : Page
    {
        public Chinhsuakhoahoc()
        {
            this.InitializeComponent();
            Loaded += Chinhsuakhoahoc_Loaded;
        }
        public static khoaHoc khoahoccanchinhsua;
        private khoaHoc khoahocsauchinhsua;
      

        private void Chinhsuakhoahoc_Loaded(object sender, RoutedEventArgs e)
        {
            BitmapImage bitmapImage = new BitmapImage();
            Uri uri = new Uri("ms-appx:///" + khoahoccanchinhsua.imagelink);
            bitmapImage.UriSource = uri;
            course_img.Source = bitmapImage;

            txtblock_adpage_tenkhoahoc.Text = khoahoccanchinhsua.tenKhoaHoc;
            txtblock_tenGiaoVien.Text = khoahoccanchinhsua.tenGiaoVien;
            txtblock_ImageLink.Text = khoahoccanchinhsua.imagelink;
            txtblock_giaKhoaHoc.Text = khoahoccanchinhsua.giaKhoaHoc;
            txtblock_giaKhuyenMai.Text = khoahoccanchinhsua.giaKhuyenMai;
            txtblock_thoiGianKhuyenMai.Text = khoahoccanchinhsua.thoiGianKhuyenMai;
            txtblock_maKhoaHoc.Text = khoahoccanchinhsua.maKhoaHoc;
            txtblock_maLop.Text = khoahoccanchinhsua.maLop;
            txtblock_soNguoiDaMua.Text = Convert.ToString(khoahoccanchinhsua.soNguoiDaMua);
            txtbox_thongTinKhoaHoc.Text = khoahoccanchinhsua.thongTinKhoaHoc;
            khoahocsauchinhsua = khoahoccanchinhsua;

        }

       
        kidmathwebserviceSoapClient ws;

        private void btnclick_adpage_backtomainmenu(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Admin_CourseManagementLoaded));
        }

        private void btnclick_huychinhsua(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Chinhsuakhoahoc));
        }

        
        private void btnclick_suanoidungkhoahoc(object sender, RoutedEventArgs e)
        {
            Chinhsuanoidungkhoahoc.khoahocdangxem = khoahoccanchinhsua;
            this.Frame.Navigate(typeof(Chinhsuanoidungkhoahoc));
        }

        private async void btnclick_luuchinhsuakhoahoc(object sender, RoutedEventArgs e)
        {
            khoahocsauchinhsua.tenKhoaHoc = txtblock_adpage_tenkhoahoc.Text;
            khoahocsauchinhsua.tenGiaoVien = txtblock_tenGiaoVien.Text;
            khoahocsauchinhsua.imagelink = txtblock_ImageLink.Text;
            khoahocsauchinhsua.giaKhoaHoc = txtblock_giaKhoaHoc.Text;
            khoahocsauchinhsua.giaKhuyenMai = txtblock_giaKhuyenMai.Text;
            khoahocsauchinhsua.thoiGianKhuyenMai = txtblock_thoiGianKhuyenMai.Text;
            khoahocsauchinhsua.maKhoaHoc = txtblock_maKhoaHoc.Text;
            khoahocsauchinhsua.maLop = txtblock_maLop.Text;
            khoahocsauchinhsua.soNguoiDaMua = Convert.ToInt32(txtblock_soNguoiDaMua.Text);
            khoahocsauchinhsua.thongTinKhoaHoc = txtbox_thongTinKhoaHoc.Text;
            string sql = "UPDATE khoaHoc SET maKhoaHoc ='" + khoahocsauchinhsua.maKhoaHoc + "',tenKHoaHoc =N'" + khoahocsauchinhsua.tenKhoaHoc + "',tenGiaoVien=N'" + khoahocsauchinhsua.tenGiaoVien + "',thongTinKhoaHoc =N'" + khoahocsauchinhsua.thongTinKhoaHoc + "',soNguoiDaMua=" + khoahocsauchinhsua.soNguoiDaMua + ",giaKHoaHoc =" + Convert.ToInt32(khoahocsauchinhsua.giaKhoaHoc) + ",giaKhuyenMai ="+ Convert.ToInt32(khoahocsauchinhsua.giaKhuyenMai) + ",thoiGianKhuyenMai =N'" + khoahocsauchinhsua.giaKhuyenMai + "',maLop =N'" + khoahocsauchinhsua.maLop + "'," +
                  "imagelink =N'" + khoahocsauchinhsua.imagelink + "' WHERE maKhoaHoc ='" + khoahoccanchinhsua.maKhoaHoc + "';";
            ws = new kidmathwebserviceSoapClient();
            if (ws.ThucHienLenhAsync(sql).Result.Body.ThucHienLenhResult != 0)
            {
                var msg = new MessageDialog("Cập nhật dữ liệu thành công");
                msg.Commands.Add(new UICommand() { Id = 0, Label = "Tải lại trang" });
                msg.Commands.Add(new UICommand() { Id = 1, Label = "Tiếp tục" });
                var rs = await msg.ShowAsync();
                if ((int)rs.Id == 0)
                {
                   
                    this.Frame.Navigate(typeof(Chinhsuakhoahoc));
                }

            }
            else
            {
                var msg = new MessageDialog("Cập nhật dữ liệu lỗi").ShowAsync();
            }
        }
    }
}
