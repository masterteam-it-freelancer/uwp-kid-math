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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace kidsmath_uwp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    public sealed partial class Admin_MailboxPage : Page
    {
        public Admin_MailboxPage()
        {
            this.InitializeComponent();
            Loaded += Admin_MailboxPage_Loaded;
        }
        kidmathwebserviceSoapClient ws;
        List<thongBao> dsthongbao = new List<thongBao>();
        private static Random Random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789qwertyuiopasdfghjklzxcvbnm";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[Random.Next(s.Length)]).ToArray());
        }
        private void Admin_MailboxPage_Loaded(object sender, RoutedEventArgs e)
        {
            ws = new kidmathwebserviceSoapClient();
            dsthongbao = ws.getDatathongBaoAsync().Result.Body.getDatathongBaoResult.ToList<thongBao>();
            dsthongbao = dsthongbao.FindAll(thongbao => thongbao.username != "");
            foreach (var thongbao in dsthongbao)
            {
                if (thongbao.tinhTrang == 'd')
                {
                    thongbao.loaiThongBao = "Đã duyệt";
                }

                else if (thongbao.tieuDe == "Yêu cầu nạp tiền" && thongbao.tinhTrang == 'c')
                {
                    thongbao.loaiThongBao = "Nạp tiền";
                }
                else if (thongbao.tieuDe != "Yêu cầu nạp tiền" && thongbao.tinhTrang == 'c')
                {
                    thongbao.loaiThongBao = "Phản hồi";
                }

            }
            foreach (var thongbao in dsthongbao)
            {
                //c : chưa duyệt ; d: đã duyệt
                if (thongbao.tinhTrang == 'd')
                {
                    thongbao.loaiThongBao = "Đã duyệt";
                }
            }
            gvdsachthongbao.ItemsSource = dsthongbao;

        }

        private async void pheduyet(object sender, RoutedEventArgs e)
        {

            Button userstatus = sender as Button;
            thongBao temp = userstatus.DataContext as thongBao;
            DateTime now = DateTime.Now;
            if (temp.tieuDe == "Yêu cầu nạp tiền" && temp.tinhTrang != 'd')
            {
                int sotien = temp.soTien;
                var msg = new MessageDialog("Xác nhận nạp " + sotien + " vào tài khoản: " + temp.username);
                msg.Commands.Add(new UICommand() { Id = 0, Label = "Xác nhận" });
                msg.Commands.Add(new UICommand() { Id = 1, Label = "Hủy" });
                var rs = await msg.ShowAsync();
                string orderid = RandomString(5);
                if ((int)rs.Id == 0)
                {

                    string sql_taolichsu = "INSERT into lichSuNapTien values('" + orderid + "'," + sotien + ",'" + temp.username + "','" + now + "');";
                    string sql_congtien = "UPDATE nguoiDung SET nguoiDung.soTienHienCo = soTienHienCo +" + sotien + ",nguoiDung.soTienDaNap = soTienDaNap +" + sotien + " WHERE nguoiDung.username = '" + temp.username + "';";
                    string sql_dapheduyet = "UPDATE thongBao SET thongBao.tinhTrang ='d' WHERE thongBao.username = '" + temp.username + "';";
                    ws = new kidmathwebserviceSoapClient();
                    int kqtaolichsu = ws.ThucHienLenhAsync(sql_taolichsu).Result.Body.ThucHienLenhResult;
                    ws = new kidmathwebserviceSoapClient();
                    int kqcongtien = ws.ThucHienLenhAsync(sql_congtien).Result.Body.ThucHienLenhResult;
                    ws = new kidmathwebserviceSoapClient();
                    int kqpheduyet = ws.ThucHienLenhAsync(sql_dapheduyet).Result.Body.ThucHienLenhResult;

                    if (kqcongtien != 0 && kqpheduyet != 0 && kqtaolichsu != 0)
                    {
                        temp.loaiThongBao = "Đã duyệt";
                        userstatus.IsEnabled = false;
                        var msg_ = new MessageDialog("Đã nạp " + sotien + " vào tài khoản: " + temp.username).ShowAsync();
                    }
                    else if (kqcongtien == 0) { var msg_2 = new MessageDialog("Xảy ra lỗi khi update table nguoiDung").ShowAsync(); }
                    else if (kqpheduyet == 0) { var msg_2 = new MessageDialog("Xảy ra lỗi khi update table thongBao").ShowAsync(); }
                    else if (kqtaolichsu == 0) { var msg_2 = new MessageDialog("Xảy ra lỗi insert table lichSuGD").ShowAsync(); }
                    else if (temp.tieuDe != "Yêu cầu nạp tiền" && temp.tinhTrang != 'd')
                    {
                        var msg_2 = new MessageDialog("Phải hồi").ShowAsync();
                    }
                    else { var msg_3 = new MessageDialog("Thông báo này đã được phê duyệt").ShowAsync(); }
                }


            }

        }

        private async void Xoathongbao(object sender, RoutedEventArgs e)
        {

            Button userstatus = sender as Button;
            thongBao temp = userstatus.DataContext as thongBao;
            if (temp.loaiThongBao == "Đã duyệt")
            {
                var msg_1 = new MessageDialog("Bạn muốn xóa thông báo này ?");
                msg_1.Commands.Add(new UICommand() { Id = 0, Label = "Xác nhận" });
                msg_1.Commands.Add(new UICommand() { Id = 1, Label = "Hủy" });
                var rs = await msg_1.ShowAsync();
                if ((int)rs.Id == 0)
                {
                    string sql_xoathongbao = "DELETE FROM thongBao WHERE thongBao.ngayGui = '" + temp.ngayGui + "';";
                    ws = new kidmathwebserviceSoapClient();
                    if (ws.ThucHienLenhAsync(sql_xoathongbao).Result.Body.ThucHienLenhResult != 0)
                    {
                        var msg_ = new MessageDialog("Đã xóa").ShowAsync();
                        this.Frame.Navigate(typeof(Admin_MailboxPage));
                    }
                    else { var msg_2 = new MessageDialog("Xảy ra lỗi").ShowAsync(); }


                }
            }
            else if (temp.loaiThongBao != "Đã duyệt")
            {
                var msg_2 = new MessageDialog("Thông báo chưa được duyệt ! Bạn muốn xóa");
                msg_2.Commands.Add(new UICommand() { Id = 0, Label = "Xác nhận" });
                msg_2.Commands.Add(new UICommand() { Id = 1, Label = "Hủy" });
                var rs = await msg_2.ShowAsync();
                if ((int)rs.Id == 0)
                {
                    string sql_xoathongbao = "DELETE FROM thongBao WHERE thongBao.ngayGui = '" + temp.ngayGui + "';";
                    ws = new kidmathwebserviceSoapClient();
                    if (ws.ThucHienLenhAsync(sql_xoathongbao).Result.Body.ThucHienLenhResult != 0)
                    {
                        var msg_2q = new MessageDialog("Đã xóa").ShowAsync();
                        this.Frame.Navigate(typeof(Admin_MailboxPage));
                    }
                    else { var msg_2a = new MessageDialog("Xảy ra lỗi").ShowAsync(); }

                }
            }
        }
    }  
}
