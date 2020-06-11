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
    public class UserSession
    {
        public static nguoiDung userlogined = null;

    }
    public sealed partial class MainPage_AffterLogin : Page
    {
        kidmathwebserviceSoapClient ws;
        public MainPage_AffterLogin()
        {
            this.InitializeComponent();
            User_btn.Content = "Xin chào, " + UserSession.userlogined.tenNguoiDung ?? "";
            Loaded += MainPage_AffterLogin_Loaded;

        }

        private void MainPage_AffterLogin_Loaded(object sender, RoutedEventArgs e)
        {

            ws = new kidmathwebserviceSoapClient();
            List<khoaHoc> dskhoahoc = new List<khoaHoc>();
            dskhoahoc = ws.getDataKhoaHocAsync().Result.Body.getDataKhoaHocResult.ToList<khoaHoc>();
            dskhoahoc = dskhoahoc.FindAll(kh => kh.maKhoaHoc != "");
            flipview_dskhoahoc.ItemsSource = dskhoahoc;
        }

        private void mainpage_click_dangky(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Page_UserInfo));
        }

        private void mainpage_click_dangnhap(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void mainpage_click_donggopykien(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(HoidapPage));
        }

        private void mainpage_click_lienhegiangday(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainForum));
        }

        private void tap_khoahoc(object sender, TappedRoutedEventArgs e)
        {
            ws = new kidmathwebserviceSoapClient();
            StackPanel khoahocduocchon = sender as StackPanel;
            khoaHoc temp = khoahocduocchon.DataContext as khoaHoc;
            Chinhsuakhoahoc.khoahoccanchinhsua = temp;
            temp = ws.getDataKhoaHocAsync().Result.Body.getDataKhoaHocResult.ToList<khoaHoc>().Find(kh => kh.maKhoaHoc == temp.maKhoaHoc);
            string sql = "SELECT * FROM muaKhoaHoc WHERE muaKhoaHoc.username = '" + UserSession.userlogined.username + "' AND muaKhoaHoc.maKhoaHoc ='" + temp.maKhoaHoc + "';";
            ws = new kidmathwebserviceSoapClient();
            if (ws.checkExistsAsync(sql).Result.Body.checkExistsResult != 0)
            {

                ViewCoursePage_AfterBuy.khoahocduocmua = temp;
                this.Frame.Navigate(typeof(ViewCoursePage_AfterBuy));
            }
            else
            {
                BuyCoursePage.khoahocduocmua = temp;
                this.Frame.Navigate(typeof(BuyCoursePage));
            }

        }
        static string[] dskhoahocgoiy = new string[] { "Toán lớp 1 cơ bản", "Toán lớp 2 cơ bản", "Toán lớp 3 cơ bản", "Toán lớp 4 cơ bản", "Toán lớp 5 cơ bản", "Ôn tập kiến thức" };
        List<String> listkh = new List<String>(dskhoahocgoiy);
        private void txtSearch_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {

            txtSearch.ItemsSource = listkh.Where(kh => kh.StartsWith(txtSearch.Text, StringComparison.OrdinalIgnoreCase)).ToArray();
        }

        private void checkbuy(string tenkhoahoc)
        {
            ws = new kidmathwebserviceSoapClient();
            khoaHoc temp = new khoaHoc();
            temp = ws.getDataKhoaHocAsync().Result.Body.getDataKhoaHocResult.ToList<khoaHoc>().Find(kh => kh.tenKhoaHoc == tenkhoahoc);
            string sql = "SELECT * FROM muaKhoaHoc WHERE muaKhoaHoc.username = '" + UserSession.userlogined.username + "' AND muaKhoaHoc.tenKhoaHoc ='" + tenkhoahoc + "';";
            ws = new kidmathwebserviceSoapClient();
            if (ws.checkExistsAsync(sql).Result.Body.checkExistsResult != 0)
            {

                ViewCoursePage_AfterBuy.khoahocduocmua = temp;
                this.Frame.Navigate(typeof(ViewCoursePage_AfterBuy));
            }
            else
            {
                BuyCoursePage.khoahocduocmua = temp;
                this.Frame.Navigate(typeof(BuyCoursePage));
            }
        }
        private void txtSearch_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.ChosenSuggestion != null)
            {
                txtSearch.Text = args.ChosenSuggestion.ToString();
            }
            else
            {
                //listkh.Add(sender.Text);
                //   txt.Text = sender.Text;
            }
        }

        private void gotobuycourse(object sender, RoutedEventArgs e)
        {
            string tenkh = txtSearch.Text.ToString();
            checkbuy(tenkh);
        }

        private void navigate_diendan(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainForum));
        }
    }
}