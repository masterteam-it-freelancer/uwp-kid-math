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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace kidsmath_uwp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ViewCoursePage_AfterBuy : Page
    {
        private kidmathwebserviceSoapClient ws;
        public ViewCoursePage_AfterBuy()
        {
            this.InitializeComponent();
            Loaded += ViewCoursePage_AfterBuy_Loaded;
        }

        private void ViewCoursePage_AfterBuy_Loaded(object sender, RoutedEventArgs e)
        {
            DanhGiaPage.khoahocdangxem = khoahocduocmua;
            fr_binhluan.Navigate(typeof(DanhGiaPage));
            txtblock_tengiaovien.Text = "Giảng viên: " + khoahocduocmua.tenGiaoVien;
            txtblock_soNguoiDaMua.Text = "Số người đã mua: "+Convert.ToString(khoahocduocmua.soNguoiDaMua) + " Lượt mua";
            Txtblockbuycourse_Username.Text = "Xin chào, " + UserSession.userlogined.username;
             hocphi.Text = khoahocduocmua.giaKhoaHocHienThi;
             danhgia.Text ="Đánh giá trung bình: "+ Convert.ToString(khoahocduocmua.danhGia) + " / 5";
            txtbox_thongTinKhoaHoc.Text = khoahocduocmua.thongTinKhoaHoc;

            BitmapImage bitmapImage = new BitmapImage();
            Uri uri = new Uri("ms-appx:///" + khoahocduocmua.imagelink);
            bitmapImage.UriSource = uri;
            courseimage_buycoursepage.Source = bitmapImage;


            ws = new kidmathwebserviceSoapClient();
            List<khoaHoc> dskhoahoc = new List<khoaHoc>();
            dskhoahoc = ws.getDataKhoaHocAsync().Result.Body.getDataKhoaHocResult.ToList<khoaHoc>();
            dskhoahoc = dskhoahoc.FindAll(khoahoc => khoahoc.maKhoaHoc != "" && khoahoc.maKhoaHoc != khoahocduocmua.maKhoaHoc);
            dscackhoahoclienquan.ItemsSource = dskhoahoc;
        }

        public static khoaHoc khoahocduocmua;
       
        
        private void btn_batdauhoc_khoahoc(object sender, RoutedEventArgs e)
        {
            LopvaChuongPage_AfterLogin.checkbuy = "buy";
            LopvaChuongPage_AfterLogin.khoahocdangxem = khoahocduocmua;
            this.Frame.Navigate(typeof(LopvaChuongPage_AfterLogin));
        }

        private void Tap_MainPage_usercoursepage(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage_AffterLogin));
        }

        private void Tap_Userinfo_viewcoursepage(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Page_UserInfo));

        }

        private void msg_viewcourse_hotro(object sender, TappedRoutedEventArgs e)
        {
            var msg = new MessageDialog("Vui lòng liên hệ Admin qua email: kidmath@help.com để được hỗ trợ").ShowAsync();
        }

        private void dangxuat_viewcourse(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void guiphanhoi_click(object sender, RoutedEventArgs e)
        {

        }

        private void tap_khoahoc(object sender, TappedRoutedEventArgs e)
        {
            ws = new kidmathwebserviceSoapClient();
            StackPanel khoahocduocchon = sender as StackPanel;
            khoaHoc temp = khoahocduocchon.DataContext as khoaHoc;
            Chinhsuakhoahoc.khoahoccanchinhsua = temp;
            temp = ws.getDataKhoaHocAsync().Result.Body.getDataKhoaHocResult.ToList<khoaHoc>().Find(kh => kh.maKhoaHoc == temp.maKhoaHoc);
            string sql = "SELECT * FROM muaKhoaHoc WHERE muaKhoaHoc.username = '" + UserSession.userlogined.username + "' AND muaKhoaHoc.maKhoaHoc ='" + temp.maKhoaHoc + "';";
            ws = new kidmathwebserviceSoapClient();
            if (ws.checkExistsAsync(sql).Result.Body.checkExistsResult != 0)
            {
                LopvaChuongPage_AfterLogin.checkbuy = "buy";
                LopvaChuongPage_AfterLogin.khoahocdangxem = temp;
                ViewCoursePage_AfterBuy.khoahocduocmua = temp;
                this.Frame.Navigate(typeof(ViewCoursePage_AfterBuy));
            }
            else
            {
                BuyCoursePage.khoahocduocmua = temp;
                this.Frame.Navigate(typeof(BuyCoursePage));
            }
        }
    }
}
