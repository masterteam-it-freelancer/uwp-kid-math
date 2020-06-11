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
    public sealed partial class PageDanhGia : Page
    {

        kidmathwebserviceSoapClient ws;
        List<danhGia> dsdanhGia = new List<danhGia>();
      
        public PageDanhGia()
        {
            this.InitializeComponent();
            Loaded += PageDanhGia_Loaded;
        }

        private void PageDanhGia_Loaded(object sender, RoutedEventArgs e)
        {
            ws = new kidmathwebserviceSoapClient();
            dsdanhGia = ws.getDataDanhGiaAsync().Result.Body.getDataDanhGiaResult.ToList<danhGia>();
            dsdanhGia = dsdanhGia.FindAll(danhGia => danhGia.username != "");
           
            foreach (var danhGia in dsdanhGia)
            {
                //c : chưa duyệt ; d: đã duyệt
                if (danhGia.tinhTrang == "d")
                {
                    danhGia.tinhTrang = "Đã duyệt";
                }
                else if(danhGia.tinhTrang == "c")
                {
                    danhGia.tinhTrang = "Phê duyệt";
                }

               // danhGia.maKhoaHoc = "Mã khóa học: " + danhGia.maKhoaHoc + " Điểm đánh giá: ";
            }
            gvdsachdanhgia.ItemsSource = dsdanhGia;
        }






        private async void pheduyet(object sender, RoutedEventArgs e)
        {
            Button userstatus = sender as Button;
            danhGia temp = userstatus.DataContext as danhGia;
            khoaHoc khoahocdangsua = new khoaHoc();
            if (temp.tinhTrang == "Phê duyệt")
            {
                var msg = new MessageDialog("Phê duyệt bài viết này?");
                msg.Commands.Add(new UICommand() { Id = 0, Label = "Xác nhận" });
                msg.Commands.Add(new UICommand() { Id = 1, Label = "Hủy" });
                var rs = await msg.ShowAsync();
                if ((int)rs.Id == 0)
                {
                    string sql_duyetbai = "UPDATE [appToanTieuHocDB].[dbo].danhGia SET danhGia.tinhTrang = 'd' WHERE danhGia.ngayDuyet = '" + temp.ngayDuyet + "';";
                    ws = new kidmathwebserviceSoapClient();
                    int kqpheduyet = ws.ThucHienLenhAsync(sql_duyetbai).Result.Body.ThucHienLenhResult;
                    if (kqpheduyet != 0)
                    { 
                        string sql_updatedanhgia = "UPDATE [appToanTieuHocDB].[dbo].khoaHoc SET khoaHoc.soLuongDanhGia = khoaHoc.soLuongDanhGia + 1 WHERE khoaHoc.maKhoaHoc = '" + temp.maKhoaHoc + "' ;";
                        ws = new kidmathwebserviceSoapClient();
                        int kqupdate = ws.ThucHienLenhAsync(sql_updatedanhgia).Result.Body.ThucHienLenhResult;
                        if (kqupdate != 0)
                        {
                            ws = new kidmathwebserviceSoapClient();
                            khoahocdangsua = (ws.getDataKhoaHocAsync().Result.Body.getDataKhoaHocResult.ToList<khoaHoc>()).Find(kh => kh.maKhoaHoc == temp.maKhoaHoc);
                            double diemdanhgiamoi = 5;
                            diemdanhgiamoi = ( Convert.ToInt32(khoahocdangsua.danhGia) * Convert.ToInt32(khoahocdangsua.soLuongDanhGia) + Convert.ToInt32(temp.rate) ) / (Convert.ToInt32(khoahocdangsua.soLuongDanhGia) + 1);
                            int diemdanhgia = Convert.ToInt32(diemdanhgiamoi);
                            string sql_updatedanhgia2 = "UPDATE [appToanTieuHocDB].[dbo].[khoaHoc] SET khoaHoc.danhGia = '" + diemdanhgia + "' WHERE khoaHoc.maKhoaHoc = '" + temp.maKhoaHoc + "';";
                            ws = new kidmathwebserviceSoapClient();
                            int kqupdate2 = ws.ThucHienLenhAsync(sql_updatedanhgia2).Result.Body.ThucHienLenhResult;
                            if (kqupdate2 != 0)
                            {
                                temp.tinhTrang = "Đã duyệt";
                                var msg_ = new MessageDialog("Đã hoàn tất quá trình duyệt bài viết").ShowAsync();
                            }
                            else { var msg_ = new MessageDialog("Lỗi khi cập nhật điểm đánh giá").ShowAsync(); }
                        }
                        else { var msg_ = new MessageDialog("Lỗi khi cập nhật số lượng đánh giá").ShowAsync(); }

                    }
                    else { var msg_ = new MessageDialog("Lỗi khi cập nhật trạng thái của đánh giá").ShowAsync(); }
                }
            }
        }
        

        private async void Xoabaidanhgia(object sender, RoutedEventArgs e) {
            Button userstatus = sender as Button;
            danhGia temp = userstatus.DataContext as danhGia;
            if (temp.tinhTrang == "Đã duyệt")
            {
                var msg_ = new MessageDialog("Không thể xóa đánh giá đã phê duyệt").ShowAsync();
            }
            else if (temp.tinhTrang == "Phê duyệt")
            {
                var msg_2 = new MessageDialog("Đánh giá chưa được duyệt ! Bạn muốn xóa");
                msg_2.Commands.Add(new UICommand() { Id = 0, Label = "Xác nhận" });
                msg_2.Commands.Add(new UICommand() { Id = 1, Label = "Hủy" });
                var rs = await msg_2.ShowAsync();
                if ((int)rs.Id == 0)
                {
                    string sql_xoathongbao = "DELETE FROM danhGia WHERE danhGia.ngayDuyet = '" + temp.ngayDuyet + "';";
                    ws = new kidmathwebserviceSoapClient();
                    if (ws.ThucHienLenhAsync(sql_xoathongbao).Result.Body.ThucHienLenhResult != 0)
                    {
                        var msg_ = new MessageDialog("Đã xóa").ShowAsync();
                        this.Frame.Navigate(typeof(PageDanhGia));
                    }
                    else { var msg_1 = new MessageDialog("Xảy ra lỗi").ShowAsync(); }


                }
            }
        }
    }
}
