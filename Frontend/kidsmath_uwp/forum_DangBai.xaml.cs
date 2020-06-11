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
    public sealed partial class forum_DangBai : Page
    {
        kidmathwebserviceSoapClient ws;
        public forum_DangBai()
        {
            this.InitializeComponent();
        }

        private void navigate_mainforum(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainForum));
        }

        private void clearall(object sender, RoutedEventArgs e)
        {
            txtbox_tieuDe.Text = "";
            txtblock_noidung.Text = "";
            this.Frame.Navigate(typeof(forum_DangBai));

        }

        private async void dangbai(object sender, RoutedEventArgs e)
        {
            string thetag = "";
            if (tagl1.IsChecked == true) thetag = "Lop1";
            else if (tagl2.IsChecked == true) thetag = "Lop2";
            else if (tagl3.IsChecked == true) thetag = "Lop3";
            else if (tagl4.IsChecked == true) thetag = "Lop4";
            else if (tagl5.IsChecked == true) thetag = "Lop5";
            
            DateTime now = DateTime.Now;
           
            if (txtbox_tieuDe.Text == "" || txtbox_tieuDe.Text.Length > 100)
            {
                var msg = new MessageDialog("Tiêu đề không phù hợp", "Error").ShowAsync();
              
            }
            else if (txtblock_noidung.Text == "" )
            {
                var msg = new MessageDialog("Phần nội dung không dược để trống", "Error").ShowAsync();
            }
            else if(thetag == "")
            {
                var msg = new MessageDialog("Chọn 1 thẻ phù hợp với bài viết", "Error").ShowAsync();
            }
            else
            {
                ws = new kidmathwebserviceSoapClient();
                string idbaiviet = UserSession.userlogined.username + DateTime.Now;
            
                string sql = "INSERT into mainForum values('"+idbaiviet+"','" + UserSession.userlogined.username+ "',N'" + UserSession.userlogined.tenNguoiDung + "','chuapheduyet','" + DateTime.Now + "',N'" + txtbox_tieuDe.Text + "',N'" + txtblock_noidung.Text + "','"+ thetag + "',0,0);";
                if (ws.ThucHienLenhAsync(sql).Result.Body.ThucHienLenhResult != 0)
                {
                    var msg = new MessageDialog("Bài viết của bạn đang chờ phê duyệt");
                    msg.Commands.Add(new UICommand() { Id = 0, Label = "Gửi bài mới" });
                    msg.Commands.Add(new UICommand() { Id = 1, Label = "Quay về trang chủ" });
                    var rs = await msg.ShowAsync();
                    if ((int)rs.Id == 0)
                    {
                        this.Frame.Navigate(typeof(forum_DangBai));
                    }
                    else if ((int)rs.Id == 1)
                    {
                        this.Frame.Navigate(typeof(MainForum));
                    }

                }
                else
                {
                    var msg = new MessageDialog("Xảy ra lỗi khi đăng bài, vui lòng thử lại sau", "Error").ShowAsync();
                }
            }
        }
    }
}
