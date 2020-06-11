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
using static kidsmath_uwp.MainPage_AffterLogin;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace kidsmath_uwp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BuyCoursePage : Page
    {
        public static string thongtinkhoahoc = "";
        public static khoaHoc khoahocduocmua;
        public BuyCoursePage()
        {
            this.InitializeComponent();
            Txtblockbuycourse_Username.Text = "Xin chào, " + UserSession.userlogined.tenNguoiDung ?? "";
            DanhGiaPage pagedanhgia = new DanhGiaPage();
            pagedanhgia.tieuDe.IsEnabled = false;
            pagedanhgia.noiDung.IsEnabled = false;
            pagedanhgia.guiNhanXet.Content = "Bạn cần mua khóa học trước khi nhận xét";
            pagedanhgia.guiNhanXet.IsEnabled = false;
            Loaded += BuyCoursePage_Loaded;
            

        }


        private void BuyCoursePage_Loaded(object sender, RoutedEventArgs e)
        {
            DanhGiaPage.khoahocdangxem = khoahocduocmua;
            fr_binhluan.Navigate(typeof(DanhGiaPage));
            txtblock_tengiaovien.Text = "Giảng viên: " + khoahocduocmua.tenGiaoVien;
            txtblock_soNguoiDaMua.Text = "Số người đã mua: " + Convert.ToString(khoahocduocmua.soNguoiDaMua) + " Lượt mua";
            Txtblockbuycourse_Username.Text = "Xin chào, " + UserSession.userlogined.username;
            hocphi.Text = khoahocduocmua.giaKhoaHocHienThi;
            danhgia.Text = "Đánh giá trung bình: " + Convert.ToString(khoahocduocmua.danhGia) + " / 5";
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

        kidmathwebserviceSoapClient ws;
       
        private void Tap_Userinfo(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Page_UserInfo));
        }

        private void Tap_MainPage(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage_AffterLogin));
        }
       
        private async void Muakhoahoc()
        {
            ws = new kidmathwebserviceSoapClient();
            decimal sodu = UserSession.userlogined.soTienHienCo;
            decimal giakhoahoc = Convert.ToDecimal(khoahocduocmua.giaKhuyenMai);
            DateTime now = DateTime.Now;
            string orderid = Convert.ToString(now);
            string sql_muakhoahoc = "INSERT into muaKhoaHoc values('"+orderid+"' ,'"+ khoahocduocmua.maKhoaHoc + "','" + UserSession.userlogined.username + "','" + now + "','"+khoahocduocmua.giaKhuyenMai+"');";
            if (giakhoahoc <= sodu)
            {
                sodu = sodu - giakhoahoc;
                string sql_trutien = "UPDATE nguoiDung SET nguoiDung.soTienHienCo = '" + sodu + "' WHERE nguoiDung.username = '" + UserSession.userlogined.username + "';";
                string sql_songuoidamua = "UPDATE khoaHoc SET khoaHoc.soNguoiDaMua = khoaHoc.soNguoiDaMua + 1 WHERE khoaHoc.maKhoaHoc ='"+khoahocduocmua.maKhoaHoc+"';";
               if(ws.ThucHienLenhAsync(sql_trutien).Result.Body.ThucHienLenhResult != 0 && ws.ThucHienLenhAsync(sql_songuoidamua).Result.Body.ThucHienLenhResult != 0 && ws.ThucHienLenhAsync(sql_muakhoahoc).Result.Body.ThucHienLenhResult != 0)
                {
                    
                    var msg = new MessageDialog("Mua khóa học thành công").ShowAsync();
                    this.Frame.Navigate(typeof(ViewCoursePage_AfterBuy));
                }
            }
            else
            {
                var msg = new MessageDialog("Bạn không đủ tiền để mua khóa học này");
                msg.Commands.Add(new UICommand() { Id = 0, Label = "Nạp tiền vào tài khoản" });
                msg.Commands.Add(new UICommand() { Id = 1, Label = "Học thử" });
                msg.Commands.Add(new UICommand() { Id = 2, Label = "Hủy" });
                var rs = await msg.ShowAsync();
                if((int)rs.Id == 0)
                {
                    this.Frame.Navigate(typeof(NaptienPage));
                }
                else if((int)rs.Id == 1)
                {
                    LopvaChuongPage_AfterLogin.checkbuy = "notbuy";
                    this.Frame.Navigate(typeof(LopvaChuongPage_AfterLogin));
                }

            }
        }
        private async void  btn_muakhoahoc_khoahoc(object sender, RoutedEventArgs e)
        {
            var msg = new MessageDialog("Bạn muốn mua khóa học này ?");

            msg.Commands.Add(new UICommand() { Id = 0, Label = "Mua" });

            msg.Commands.Add(new UICommand() { Id = 1, Label = "Hủy" });

            var rs = await msg.ShowAsync();

            if ((int)rs.Id == 0)
            {
                    Muakhoahoc();
                   
            }

        }


        private void mainpage(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage_AffterLogin));
        }

        private void btn_hocthu(object sender, RoutedEventArgs e)
        {
            LopvaChuongPage_AfterLogin.checkbuy = "notbuy";
            LopvaChuongPage_AfterLogin.khoahocdangxem = khoahocduocmua;
            this.Frame.Navigate(typeof(LopvaChuongPage_AfterLogin));
        }

        private void btn_tuvan(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(HoidapPage));
        }

        private void msg_viewcourse_hotro(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainForum));
        }

        private void Tap_MainPage_usercoursepage(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage_AffterLogin));
        }

        private void Tap_Userinfo_viewcoursepage(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Page_UserInfo));
        }

        private void dangxuat_viewcourse(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
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
