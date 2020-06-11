using System;
using System.Collections.Generic;
using Windows.Foundation;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage;
using Windows.Storage.Pickers;
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


    public sealed partial class LopvaChuongPage_AfterLogin : Page
    {

        public LopvaChuongPage_AfterLogin()
        {
            this.InitializeComponent();
            Loaded += LopvaChuongPage_AfterLogin_Loaded;

        }
        public static khoaHoc khoahocdangxem;
        public static string loadFrameStatus = "video"; // video, test, download
        private List<chuong> dschuong;
        private List<bai> dsbai;
        private List<danhSachCauHoi> dsbaithi;
        private video videoduocchon;
        private baiTap baitapduocchon;
        private chuong chuongduocchon;
        private bai baiduocchon;
        public static string checkbuy;
        kidmathwebserviceSoapClient ws;


        private void LopvaChuongPage_AfterLogin_Loaded(object sender, RoutedEventArgs e)
        {
            txtblock_tenKhoaHoc.Text = "Bạn đang xem khóa học: " + khoahocdangxem.tenKhoaHoc;
            User_btn.Content = "Xin chào, " + UserSession.userlogined.tenNguoiDung;
            

            ws = new kidmathwebserviceSoapClient();
            dschuong = ws.getDataChuongAsync().Result.Body.getDataChuongResult.ToList<chuong>();
            dschuong = dschuong.FindAll(chuong => chuong.maKhoaHoc == khoahocdangxem.maKhoaHoc);
            dstenchuong.ItemsSource = dschuong;
            chuongduocchon = dschuong[0];


            ws = new kidmathwebserviceSoapClient();
            dsbai = ws.getDataBaiAsync().Result.Body.getDataBaiResult.ToList<bai>();
            dsbai = dsbai.FindAll(bai => bai.maChuong == chuongduocchon.maChuong);
            listbox_dsbaihoctrong1chuong.ItemsSource = dsbai;
            baiduocchon = dsbai[0];
            tenbaihienthi.Text = baiduocchon.tenBai;

            ws = new kidmathwebserviceSoapClient();
            videoduocchon = (ws.getDatavideoAsync().Result.Body.getDatavideoResult.ToList<video>()).Find(video => video.maBai == baiduocchon.maBai);
            VideoPage.videoduocchon = videoduocchon;
            Active_frame.Navigate((typeof(VideoPage)));

            ws = new kidmathwebserviceSoapClient();
            dsbaithi = ws.getDataDanhSachCauHoiAsync().Result.Body.getDataDanhSachCauHoiResult.ToList<danhSachCauHoi>();
            dsbaithi = dsbaithi.FindAll(baithi => baithi.maBai == baiduocchon.maBai);
        }


        private void Tap_MainPage(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage_AffterLogin));
        }

        private void Tap_Userinfo(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Page_UserInfo));
        }


        private void selectionchange_dschuong(object sender, SelectionChangedEventArgs e)
        {
            chuong selectedchuong = dstenchuong.SelectedItem as chuong;
            if (selectedchuong != null)
            {
                dsbai = dsbai.FindAll(bai => bai.maChuong == selectedchuong.maChuong);
                chuongduocchon = selectedchuong;

                ws = new kidmathwebserviceSoapClient();
                dsbai = ws.getDataBaiAsync().Result.Body.getDataBaiResult.ToList<bai>();
                dsbai = dsbai.FindAll(bai => bai.maChuong == chuongduocchon.maChuong);
                listbox_dsbaihoctrong1chuong.ItemsSource = dsbai;
            }
        }

        private async void  selectionchange_dsbaitrong1chuong(object sender, SelectionChangedEventArgs e)
        {
           
            bai selectedbai = listbox_dsbaihoctrong1chuong.SelectedItem as bai;
            baiduocchon = selectedbai;
            tenbaihienthi.Text = baiduocchon.tenBai;

            if (selectedbai != null)
            {
                
                ws = new kidmathwebserviceSoapClient();
                dsbaithi = ws.getDataDanhSachCauHoiAsync().Result.Body.getDataDanhSachCauHoiResult.ToList<danhSachCauHoi>();
                dsbaithi = dsbaithi.FindAll(baithi => baithi.maBai == baiduocchon.maBai);

               
                switch (loadFrameStatus)
                {
                    case "video":
                        ws = new kidmathwebserviceSoapClient();
                        videoduocchon = (ws.getDatavideoAsync().Result.Body.getDatavideoResult.ToList<video>()).Find(video => video.maBai == baiduocchon.maBai);
                        loadFrameStatus = "video";
                        VideoPage.videoduocchon = videoduocchon;
                        Active_frame.Navigate(typeof(VideoPage));
                        break;
                    case "test":
                        if (checkbuy == "buy")
                        {
                            BaithiPage.mabaihocdangthi = baiduocchon.maBai;
                            loadFrameStatus = "test";
                            Active_frame.Navigate(typeof(BaithiPage));
                        }
                        else if (checkbuy == "notbuy")
                        {

                            var msg = new MessageDialog("Bạn chưa mua khóa học này");
                            msg.Commands.Add(new UICommand() { Id = 0, Label = "Mua khóa học này" });
                            msg.Commands.Add(new UICommand() { Id = 1, Label = "Tiếp tục học thử" });

                            var rs = await msg.ShowAsync();
                            if ((int)rs.Id == 0)
                            {
                                BuyCoursePage.khoahocduocmua = khoahocdangxem;
                                this.Frame.Navigate(typeof(BuyCoursePage));
                            }
                            else if((int)rs.Id == 1)
                            {
                                loadFrameStatus = "video";
                                this.Frame.Navigate(typeof(VideoPage));
                            }

                        }
                        break;
                    case "download":
                        Active_frame.Navigate(typeof(BaitapPage));
                        break;
                    default:
                        break;
                }

            }
            else
            {
                //  mabai = "";

            }

        }

        private void btnclick_baigiang(object sender, RoutedEventArgs e)
        {

            loadFrameStatus = "video";
            VideoPage.videoduocchon = videoduocchon;
            Active_frame.Navigate(typeof(VideoPage));
        }


      
        private async void btnclick_baithi(object sender, RoutedEventArgs e)
        {
            loadFrameStatus = "test";
            if (checkbuy == "buy")
            {
                BaithiPage.mabaihocdangthi = baiduocchon.maBai;
                Active_frame.Navigate(typeof(BaithiPage));
            }
            else if (checkbuy == "notbuy")
            {

                var msg = new MessageDialog("Bạn chưa mua khóa học này");
                msg.Commands.Add(new UICommand() { Id = 0, Label = "Mua khóa học này" });
                msg.Commands.Add(new UICommand() { Id = 1, Label = "Tiếp tục học thử" });

                var rs = await msg.ShowAsync();
                if ((int)rs.Id == 0)
                {
                    BuyCoursePage.khoahocduocmua = khoahocdangxem;
                    this.Frame.Navigate(typeof(BuyCoursePage));

                }

            }

        }

        private void mainpage_userinfo(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Page_UserInfo));
        }

        private void mainpage_click_dangxuat(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));

        }

        private void baoloi(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(HoidapPage));
        }

        private void backtomainmenu(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage_AffterLogin));
        }

        private void btnclick_baitap(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate((typeof(MainPage_AffterLogin)));
        }
    }
}
