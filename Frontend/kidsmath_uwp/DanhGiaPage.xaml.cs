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
using kidsmath_uwp.GetDatabaseServiceReference;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace kidsmath_uwp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DanhGiaPage : Page
    {

        public static khoaHoc khoahocdangxem;
        
        kidmathwebserviceSoapClient ws;
        public DanhGiaPage()
        {
          this.InitializeComponent();
          Loaded += DanhGiaPage_Loaded;
            //  guiNhanXet.Content = "Demo";
            //GridView_dsBinhLuan
        }

        private void DanhGiaPage_Loaded(object sender, RoutedEventArgs e)
        {
            ws = new kidmathwebserviceSoapClient();
            List<danhGia> dsbinhluan = new List<danhGia>();
            dsbinhluan = ws.getDataDanhGiaAsync().Result.Body.getDataDanhGiaResult.ToList<danhGia>();
            dsbinhluan = dsbinhluan.FindAll(binhluan => binhluan.maKhoaHoc == khoahocdangxem.maKhoaHoc && binhluan.tinhTrang == "d");
            danhGiaTrungBinh.Text = Convert.ToString(khoahocdangxem.danhGia) + " / 5";
            soluongnhanxet.Text = Convert.ToString(khoahocdangxem.soLuongDanhGia) + " Nhận xét";
            foreach (var binhluan in dsbinhluan)
            {
                string imglink = Convert.ToString(binhluan.rate);
                string imglink2 = "";
                switch (imglink)
                {
                    case "1":
                        imglink = @"Assets\StarVote\1.png";
                        break;
                    case "2":
                        imglink = @"Assets\StarVote\2.png";
                        break;
                    case "3":
                        imglink = @"Assets\StarVote\3.png";
                        break;
                    case "4":
                        imglink = @"Assets\StarVote\4.png";
                        break;
                    case "5":
                        imglink = @"Assets\StarVote\5.png";
                        break;
                    default:
                         imglink = @"Assets\StarVote\5.png";
                        break;

                }
                switch (khoahocdangxem.danhGia)
                {
                    case 1:
                        imglink2 = @"Assets\StarVote\1.png";
                        break;
                    case 2:
                        imglink2 = @"Assets\StarVote\2.png";
                        break;
                    case 3:
                        imglink2 = @"Assets\StarVote\3.png";
                        break;
                    case 4:
                        imglink2 = @"Assets\StarVote\4.png";
                        break;
                    case 5:
                        imglink2 = @"Assets\StarVote\5.png";
                        break;
                    default:
                        imglink2 = @"Assets\StarVote\5.png";
                        break;

                }
                binhluan.rate = imglink;
                BitmapImage bitmapImage = new BitmapImage();
                Uri uri = new Uri("ms-appx:///" + imglink2);
                bitmapImage.UriSource = uri;
                starimg.Source = bitmapImage;


            }
            GridView_dsBinhLuan.ItemsSource = dsbinhluan;
        }

        private async void btnclick_guinhanxet(object sender, RoutedEventArgs e)
        {
            DateTime now = DateTime.Now;
            int diemdanhgia ;

            if (Convert.ToInt32(diemDanhGia.Text) != 1 && Convert.ToInt32(diemDanhGia.Text) != 2 &&
                Convert.ToInt32(diemDanhGia.Text) != 3 && Convert.ToInt32(diemDanhGia.Text) != 4 &&
                Convert.ToInt32(diemDanhGia.Text) != 5)
            {
                var msg = new MessageDialog("Vui lòng nhập đánh giá từ 1 tới 5", "Error").ShowAsync();
            }
            else if (tieuDe.Text == "" || tieuDe.Text.Length > 100)
            {
                var msg = new MessageDialog("Tiêu đề không phù hợp", "Error").ShowAsync();

            }
            else if (noiDung.Text == "")
            {
                var msg = new MessageDialog("Phần nội dung không dược để trống", "Error").ShowAsync();
            }
           
            else
            {
                diemdanhgia = Convert.ToInt32(diemDanhGia.Text);
                ws = new kidmathwebserviceSoapClient();
                string idbaiviet = UserSession.userlogined.username + DateTime.Now;

                string sql = "INSERT into danhGia values('" + UserSession.userlogined.username + "','"+diemdanhgia+"','"+khoahocdangxem.maKhoaHoc+"',N'" + tieuDe.Text + "',N'" + noiDung.Text + "','" + DateTime.Now + "','c');";
                if (ws.ThucHienLenhAsync(sql).Result.Body.ThucHienLenhResult != 0)
                {
                    var msg = new MessageDialog("Bài đánh giá của bạn đang chờ phê duyệt");
                    msg.Commands.Add(new UICommand() { Id = 0, Label = "Gửi bài mới" });
                    msg.Commands.Add(new UICommand() { Id = 1, Label = "OK" });
                    var rs = await msg.ShowAsync();
                    if ((int)rs.Id == 0)
                    {
                        this.Frame.Navigate(typeof(DanhGiaPage));
                    }
                   
                }
                else
                {
                    var msg = new MessageDialog("Xảy ra lỗi khi gửi đánh giá, vui lòng thử lại sau", "Error").ShowAsync();
                }
            }
        }
    }
    
}
