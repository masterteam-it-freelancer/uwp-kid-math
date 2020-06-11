using kidsmath_uwp.GetDatabaseServiceReference;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace kidsmath_uwp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Admin_StatisticalPage : Page
    {
        public Admin_StatisticalPage()
        {
            this.InitializeComponent();
            Loaded += Admin_StatisticalPage_Loaded;
            
        }
        kidmathwebserviceSoapClient ws;
        
        private void Admin_StatisticalPage_Loaded(object sender, RoutedEventArgs e)
        {
            ws = new kidmathwebserviceSoapClient();
            List<khoaHoc> dskhoahoc = new List<khoaHoc>();
            dskhoahoc = ws.getDataKhoaHocAsync().Result.Body.getDataKhoaHocResult.ToList<khoaHoc>();
            dskhoahoc = dskhoahoc.FindAll(kh => kh.maKhoaHoc != "");
            adpage_statistical_dskhoahoc.ItemsSource = dskhoahoc;

            ws = new kidmathwebserviceSoapClient();
            soLuongNguoiDung.Text = ws.checkExistsAsync("select * from nguoiDung").Result.Body.checkExistsResult.ToString();
            ws = new kidmathwebserviceSoapClient();
            soLuongKhoaHocDaBan.Text = ws.checkExistsAsync("select * from muaKhoaHoc").Result.Body.checkExistsResult.ToString();
            ws = new kidmathwebserviceSoapClient();
            tongSoTienNguoiDungNap.Text = ws.doanhthu_functAsync().Result.ToString();
            ws = new kidmathwebserviceSoapClient();
            tongSoTienNguoiDungSuDung.Text = (Convert.ToInt32(tongSoTienNguoiDungNap.Text) - ws.nguoidungchi_functAsync().Result).ToString();



        }
    }
}
