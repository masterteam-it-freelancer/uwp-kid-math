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
    public sealed partial class Admin_forum : Page
    {
        kidmathwebserviceSoapClient ws;
        public Admin_forum()
        {
            this.InitializeComponent();
            Loaded += Admin_forum_Loaded;
        }

        private void Admin_forum_Loaded(object sender, RoutedEventArgs e)
        {
            ws = new kidmathwebserviceSoapClient();
            List<mainForum> dsbaiviet = new List<mainForum>();
            dsbaiviet = ws.getDatamainForumAsync().Result.Body.getDatamainForumResult.ToList<mainForum>();
           
            foreach (var baiviet in dsbaiviet)
            {
                if (baiviet.tinhTrang == "chuapheduyet") baiviet.tinhTrang = "Phê duyệt";
                else if (baiviet.tinhTrang == "dongbinhluan") baiviet.tinhTrang = "Mở khóa bình luận";
                else if (baiviet.tinhTrang == "dapheduyet") baiviet.tinhTrang = "Khóa bình luận";
            }
            gvdsachthongbao.ItemsSource = dsbaiviet;
        }

        private async void Xoabaiviet(object sender, RoutedEventArgs e)
        {
            Button userstatus = sender as Button;
            mainForum temp = userstatus.DataContext as mainForum;
            if (temp.tinhTrang != "Phê duyệt")
            {
                var msg_1 = new MessageDialog("Bạn muốn xóa thông báo này ?");
                msg_1.Commands.Add(new UICommand() { Id = 0, Label = "Xác nhận" });
                msg_1.Commands.Add(new UICommand() { Id = 1, Label = "Hủy" });
                var rs = await msg_1.ShowAsync();
                if ((int)rs.Id == 0)
                {
                    string sql_xoathongbao = "DELETE FROM mainForum WHERE mainForum.idbaiviet = '" + temp.idbaiviet + "';";
                    ws = new kidmathwebserviceSoapClient();
                    if (ws.ThucHienLenhAsync(sql_xoathongbao).Result.Body.ThucHienLenhResult != 0)
                    {
                        var msg_ = new MessageDialog("Đã xóa").ShowAsync();
                        this.Frame.Navigate(typeof(Admin_forum));
                    }
                    else { var msg_2 = new MessageDialog("Xảy ra lỗi").ShowAsync(); }


                }
            }
            else if (temp.tinhTrang == "Phê duyệt" )
            {
                var msg_2 = new MessageDialog("Thông báo chưa được duyệt ! Bạn muốn xóa");
                msg_2.Commands.Add(new UICommand() { Id = 0, Label = "Xác nhận" });
                msg_2.Commands.Add(new UICommand() { Id = 1, Label = "Hủy" });
                var rs = await msg_2.ShowAsync();
                if ((int)rs.Id == 0)
                {
                    string sql_xoathongbao = "DELETE FROM mainForum WHERE mainForum.idbaiviet = '" + temp.idbaiviet + "';";
                    ws = new kidmathwebserviceSoapClient();
                    if (ws.ThucHienLenhAsync(sql_xoathongbao).Result.Body.ThucHienLenhResult != 0)
                    {
                        var msg_ = new MessageDialog("Đã xóa").ShowAsync();
                        this.Frame.Navigate(typeof(Admin_forum));
                    }
                    else { var msg_1 = new MessageDialog("Xảy ra lỗi").ShowAsync(); }


                }
            }
        }
    

        private async void action(object sender, RoutedEventArgs e)
        {
            Button userstatus = sender as Button;
            mainForum temp = userstatus.DataContext as mainForum;
            DateTime now = DateTime.Now;
            if (temp.tinhTrang == "Phê duyệt" )
            {
                var msg = new MessageDialog("Phê duyệt bài viết này?");
                msg.Commands.Add(new UICommand() { Id = 0, Label = "Xác nhận" });
                msg.Commands.Add(new UICommand() { Id = 1, Label = "Hủy" });
                var rs = await msg.ShowAsync();
                if ((int)rs.Id == 0)
                {
                    string sql_duyetbai = "UPDATE mainForum SET mainForum.tinhTrang = 'dapheduyet' WHERE mainForum.idbaiviet = '" + temp.idbaiviet + "';";
                    ws = new kidmathwebserviceSoapClient();
                    int kqpheduyet = ws.ThucHienLenhAsync(sql_duyetbai).Result.Body.ThucHienLenhResult;
                    if (kqpheduyet != 0)
                    {
                        temp.tinhTrang = "Khóa bình luận";
                        var msg_ = new MessageDialog("Đã hoàn tất quá trình duyệt bài viết").ShowAsync();
                    }
                 
                    else { var msg_2 = new MessageDialog("Xảy ra lỗi!").ShowAsync(); }
                }
            }
 
            else if (temp.tinhTrang == "Mở khóa bình luận")
            {
                var msg = new MessageDialog("Mở khóa bình luận cho bài viết này?");
                msg.Commands.Add(new UICommand() { Id = 0, Label = "Xác nhận" });
                msg.Commands.Add(new UICommand() { Id = 1, Label = "Hủy" });
                var rs = await msg.ShowAsync();
                if ((int)rs.Id == 0)
                {
                    string sql_duyetbai = "UPDATE mainForum SET mainForum.tinhTrang = 'dapheduyet' WHERE mainForum.idbaiviet = '" + temp.idbaiviet + "';";
                    ws = new kidmathwebserviceSoapClient();
                    int kqpheduyet = ws.ThucHienLenhAsync(sql_duyetbai).Result.Body.ThucHienLenhResult;
                    if (kqpheduyet != 0)
                    {
                        temp.tinhTrang = "Khóa bình luận";
                        var msg_ = new MessageDialog("Đã mở khóa phần bình luận của bài viết").ShowAsync();
                    }

                    else { var msg_2 = new MessageDialog("Xảy ra lỗi!").ShowAsync(); }
                }
            }

            else if (temp.tinhTrang == "Khóa bình luận")
            {
                var msg = new MessageDialog("Đóng bình luận bài viết này?");
                msg.Commands.Add(new UICommand() { Id = 0, Label = "Xác nhận" });
                msg.Commands.Add(new UICommand() { Id = 1, Label = "Hủy" });
                var rs = await msg.ShowAsync();
                if ((int)rs.Id == 0)
                {
                    string sql_duyetbai = "UPDATE mainForum SET mainForum.tinhTrang = 'dongbinhluan' WHERE mainForum.idbaiviet = '" + temp.idbaiviet + "';";
                    ws = new kidmathwebserviceSoapClient();
                    int kqpheduyet = ws.ThucHienLenhAsync(sql_duyetbai).Result.Body.ThucHienLenhResult;
                    if (kqpheduyet != 0)
                    {
                        temp.tinhTrang = "Mở khóa bình luận";
                        var msg_ = new MessageDialog("Đã hoàn tất quá trình khóa bình luận bài viết").ShowAsync();
                    }

                    else { var msg_2 = new MessageDialog("Xảy ra lỗi!").ShowAsync(); }
                }
            }

        }

       
    }
}
