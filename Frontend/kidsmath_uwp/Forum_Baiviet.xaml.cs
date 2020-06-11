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
    public sealed partial class Forum_Baiviet : Page
    {
        kidmathwebserviceSoapClient ws;

        public static mainForum baivietdangxem;
      //  public static  ;
        public Forum_Baiviet()
        {
            Loaded += Forum_Baiviet_Loaded;
            this.InitializeComponent();
        }

        private void Forum_Baiviet_Loaded(object sender, RoutedEventArgs e)
        {
            txtblock_noidung.Text = baivietdangxem.noiDung;
            txtblock_tenNguoiDung.Text = baivietdangxem.tenNguoiDung;
            txtblock_tieude.Text = baivietdangxem.tieuDe;
            txtblock_username.Text = '@' +baivietdangxem.username;
            btn_content.Content = baivietdangxem.tag;
            Luotxem.Text = Convert.ToString(baivietdangxem.luotxem);
            luotbinhluan.Text = Convert.ToString(baivietdangxem.luotbinhluan);
            ngayDang.Text = Convert.ToString(baivietdangxem.ngayDuyet);
            ws = new kidmathwebserviceSoapClient();
            List<mainForum> dsbaiviet = new List<mainForum>();
            List<mainForum> dsbaiviet1 = new List<mainForum>();
            List<mainForum> dsbaiviet2 = new List<mainForum>();
            dsbaiviet = ws.getDatamainForumAsync().Result.Body.getDatamainForumResult.ToList<mainForum>();
            dsbaiviet1 = dsbaiviet.FindAll(baiviet => baiviet.idbaiviet != baivietdangxem.idbaiviet);
            grview_cacbaivietkhac.ItemsSource = dsbaiviet;
            dsbaiviet2 = dsbaiviet.FindAll(baiviet => baiviet.username == baivietdangxem.username);
            grview_cacbaivietcungtacgia.ItemsSource = dsbaiviet2;
            cacbaivietcungtacgia.Text = "Các bài viết khác từ " + baivietdangxem.tenNguoiDung;
            binhluan.Text = "Bình luận( " + baivietdangxem.luotbinhluan + "):";
            fr_binhluan.Navigate(typeof(binhLuan_Forum));

        }

        private void btnclick_navigate(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainForum));
        }
    }
}
