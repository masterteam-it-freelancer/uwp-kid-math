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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace kidsmath_uwp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class binhLuan_Forum : Page
    {
        kidmathwebserviceSoapClient ws;
        public static mainForum baivietduocchon;
        List<traloiForum> dsbinhluan = new List<traloiForum>();
        public binhLuan_Forum()
        {
            this.InitializeComponent();
            Loaded += BinhLuan_Forum_Loaded;
        }

        private void BinhLuan_Forum_Loaded(object sender, RoutedEventArgs e)
        {
            txtbox_noidung.Text = "";
            ws = new kidmathwebserviceSoapClient();
            dsbinhluan = ws.GetDatatraLoiForumAsync().Result.Body.GetDatatraLoiForumResult.ToList<traloiForum>();
            dsbinhluan = dsbinhluan.FindAll(binhluan => binhluan.idbaiviet == baivietduocchon.idbaiviet);
            foreach (var binhluan in dsbinhluan)
            {
                binhluan.username = '@' + binhluan.username;
            }

            grview_dsbinhluan.ItemsSource = dsbinhluan;
            ws = new kidmathwebserviceSoapClient();
            string sql_congluotview = "UPDATE mainForum SET mainForum.luotxem = mainForum.luotxem + 1 WHERE mainForum.idbaiviet = '" + baivietduocchon.idbaiviet + "';";
            ws.ThucHienLenhAsync(sql_congluotview);
        }

        private void guibinhluan(object sender, RoutedEventArgs e)
        {
            if (txtbox_noidung.Text == "")
            {
                var msgerr = new MessageDialog("Vui lòng nhập nội dung bình luận", "Error").ShowAsync();
            }
            else {
                DateTime now = DateTime.Now;
                traloiForum binhluancuaban = new traloiForum();
                binhluancuaban.idbaiviet = baivietduocchon.idbaiviet;
                binhluancuaban.binhLuan = txtbox_noidung.Text;
                binhluancuaban.tenNguoiDung = "Đặng Rất Giàu"; //UserSession.userlogined.tenNguoiDung;
                binhluancuaban.username = "damuatoanbo";//UserSession.userlogined.username;
                binhluancuaban.ngayBinhLuan = now;

                ws = new kidmathwebserviceSoapClient();

                string sql = "INSERT into traloiForum values('" + binhluancuaban.idbaiviet + "','" + binhluancuaban.username + "',N'" + binhluancuaban.tenNguoiDung + "',N'" + binhluancuaban.binhLuan + "','" + binhluancuaban.ngayBinhLuan + "');";
                if (ws.ThucHienLenhAsync(sql).Result.Body.ThucHienLenhResult != 0)
                {
                    ws = new kidmathwebserviceSoapClient();
                    string sql_congluotxem = "UPDATE mainForum SET mainForum.luotbinhluan = mainForum.luotbinhluan + 1 WHERE mainForum.idbaiviet = '"+baivietduocchon.idbaiviet+"';";
                    if (ws.ThucHienLenhAsync(sql_congluotxem).Result.Body.ThucHienLenhResult != 0)
                    {
                        this.Frame.Navigate(typeof(binhLuan_Forum));
                    }
                    else { var msg = new MessageDialog("Cộng lượt bình luận xảy ra lỗi", "Error").ShowAsync(); }
                }
                else { var msg2 = new MessageDialog("Thêm câu trả lời lỗi", "Error").ShowAsync(); }


            }


        }
    }
}
