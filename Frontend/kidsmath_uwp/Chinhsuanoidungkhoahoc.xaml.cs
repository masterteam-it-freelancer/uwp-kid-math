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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace kidsmath_uwp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Chinhsuanoidungkhoahoc : Page
    {
        public static khoaHoc khoahocdangxem;
        private List<chuong> dschuong;
        private List<bai> dsbai;
        private List<danhSachCauHoi> dsbaithi;
        private video videoduocchon;
        private chuong chuongduocchon;
        private bai baiduocchon;
        kidmathwebserviceSoapClient ws;


        public Chinhsuanoidungkhoahoc()
        {
            this.InitializeComponent();
            Loaded += Chinhsuanoidungkhoahoc_Loaded;
        }

        private void Chinhsuanoidungkhoahoc_Loaded(object sender, RoutedEventArgs e)
        {

            ws = new kidmathwebserviceSoapClient();
            dschuong = ws.getDataChuongAsync().Result.Body.getDataChuongResult.ToList<chuong>();
            dschuong = dschuong.FindAll(chuong => chuong.maKhoaHoc == khoahocdangxem.maKhoaHoc);
            ListView_dschuong.ItemsSource = dschuong;
            chuongduocchon = dschuong[0];


            ws = new kidmathwebserviceSoapClient();
            dsbai = ws.getDataBaiAsync().Result.Body.getDataBaiResult.ToList<bai>();
            dsbai = dsbai.FindAll(bai => bai.maChuong == chuongduocchon.maChuong);
            ListView_dsbai.ItemsSource = dsbai;
            baiduocchon = dsbai[0];


        }

        private void btnclick_suathongtinkhoahoc(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Chinhsuakhoahoc));
        }

        private void btnclick_huychinhsua(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Chinhsuanoidungkhoahoc));
        }

        private void btnclick_luuchinhsuanoidungkhoahoc(object sender, RoutedEventArgs e)
        {
            txtblock_linkVideo.Text = " Trống !";
            txtblock_tenVideo.Text = " Trống !";
            txtblock_maVideo.Text = " Trống !";
        }

        private void btnclick_adpage_backtomainmenu(object sender, RoutedEventArgs e)
        {

            this.Frame.Navigate(typeof(Admin_CourseManagementLoaded));


        }

        private void selectionchange_dschuong(object sender, SelectionChangedEventArgs e)
        {
            chuong selectedchuong = ListView_dschuong.SelectedItem as chuong;
            if (selectedchuong != null)
            {
                dsbai = dsbai.FindAll(bai => bai.maChuong == selectedchuong.maChuong);
                chuongduocchon = selectedchuong;

                ws = new kidmathwebserviceSoapClient();
                dsbai = ws.getDataBaiAsync().Result.Body.getDataBaiResult.ToList<bai>();
                dsbai = dsbai.FindAll(bai => bai.maChuong == chuongduocchon.maChuong);
                ListView_dsbai.ItemsSource = dsbai;
            }
        }

        private void selectionchange_dsbaitrong1chuong(object sender, SelectionChangedEventArgs e)
        {
            bai selectedbai = ListView_dsbai.SelectedItem as bai;
            baiduocchon = selectedbai;

            if (selectedbai != null || videoduocchon != null)
            {

                ws = new kidmathwebserviceSoapClient();
                videoduocchon = (ws.getDatavideoAsync().Result.Body.getDatavideoResult.ToList<video>()).Find(video => video.maBai == baiduocchon.maBai);
                txtblock_linkVideo.Text = videoduocchon.linkvideo;
                txtblock_tenVideo.Text = videoduocchon.tenBai;
                txtblock_maVideo.Text = videoduocchon.maBai;

            }
            else
            {
                txtblock_linkVideo.Text = " Trống !";
                txtblock_tenVideo.Text = " Trống !";
                txtblock_maVideo.Text = " Trống !";
            }
        }
    }
}

