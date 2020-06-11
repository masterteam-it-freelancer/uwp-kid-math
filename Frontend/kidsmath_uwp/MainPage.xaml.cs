using kidsmath_uwp.GetDatabaseServiceReference;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace kidsmath_uwp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            var ws = new kidmathwebserviceSoapClient();
            Loaded += MainPage_Loaded;
        }
        kidmathwebserviceSoapClient ws;
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            ws = new kidmathwebserviceSoapClient();
            List<khoaHoc> dskhoahoc = new List<khoaHoc>();
            dskhoahoc = ws.getDataKhoaHocAsync().Result.Body.getDataKhoaHocResult.ToList<khoaHoc>();
            dskhoahoc = dskhoahoc.FindAll(kh => kh.maKhoaHoc != "");
            flipview_dskhoahoc.ItemsSource = dskhoahoc;
        }

        List<khoaHoc> listkhoahoc = new List<khoaHoc>();
        private void Dangky_Click(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(RegistryPage));
        }

        
        private void Btnclick_Khoahoclop1(object sender, RoutedEventArgs e)
        {
            BuyCoursePage.thongtinkhoahoc = "l1cb";
            this.Frame.Navigate(typeof(BuyCoursePage));
        }

        private async void Mainpage_click(object sender, RoutedEventArgs e)
        {
            var msg = new MessageDialog("Vui lòng đăng nhập để xêm nội dung này");

            msg.Commands.Add(new UICommand() { Id = 0, Label = "Đăng nhập" });

            msg.Commands.Add(new UICommand() { Id = 1, Label = "Đăng ký" });
            msg.Commands.Add(new UICommand() { Id = 2, Label = "Hủy" });

            var rs = await msg.ShowAsync();

            if ((int)rs.Id == 0)
            {
                this.Frame.Navigate(typeof(LoginPage));

            }
            else if ((int)rs.Id == 1)
            {
                this.Frame.Navigate(typeof(RegistryPage));

            }
        }

        private void mainpage_click_dangky(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(RegistryPage));

        }

        private void mainpage_click_dangnhap(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(LoginPage));
        }

        private async void mainpage_click_lienhegiangday(object sender, RoutedEventArgs e)
        {

            var msg = new MessageDialog("Vui lòng đăng nhập để xêm nội dung này");

            msg.Commands.Add(new UICommand() { Id = 0, Label = "Đăng nhập" });

            msg.Commands.Add(new UICommand() { Id = 1, Label = "Đăng ký" });
            msg.Commands.Add(new UICommand() { Id = 2, Label = "Hủy" });

            var rs = await msg.ShowAsync();

            if ((int)rs.Id == 0)
            {
                this.Frame.Navigate(typeof(LoginPage));

            }
            else if ((int)rs.Id == 1)
            {
                this.Frame.Navigate(typeof(RegistryPage));

            }
        }

        private async void mainpage_click_donggopykien(object sender, RoutedEventArgs e)
        {

            var msg = new MessageDialog("Vui lòng đăng nhập để gửi câu hỏi");

            msg.Commands.Add(new UICommand() { Id = 0, Label = "Đăng nhập" });

            msg.Commands.Add(new UICommand() { Id = 1, Label = "Đăng ký" });
            msg.Commands.Add(new UICommand() { Id = 2, Label = "Hủy" });

            var rs = await msg.ShowAsync();

            if ((int)rs.Id == 0)
            {
                this.Frame.Navigate(typeof(LoginPage));

            }
            else if ((int)rs.Id == 1)
            {
                this.Frame.Navigate(typeof(RegistryPage));

            }
        }

        
        static string[] dskhoahocgoiy = new string[] { "Toán lớp 1 cơ bản", "Toán lớp 2 cơ bản", "Toán lớp 3 cơ bản", "Toán lớp 4 cơ bản", "Toán lớp 5 cơ bản", "Ôn tập kiến thức" };
        List<String> listkh = new List<String>(dskhoahocgoiy);
        private void txtSearch_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            
            txtSearch.ItemsSource = listkh.Where(kh => kh.StartsWith(txtSearch.Text, StringComparison.OrdinalIgnoreCase)).ToArray();
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

        private async void gotobuycourse(object sender, RoutedEventArgs e)
        {
            var msg = new MessageDialog("Vui lòng đăng nhập để xêm nội dung này");

            msg.Commands.Add(new UICommand() { Id = 0, Label = "Đăng nhập" });

            msg.Commands.Add(new UICommand() { Id = 1, Label = "Đăng ký" });
            msg.Commands.Add(new UICommand() { Id = 2, Label = "Hủy" });

            var rs = await msg.ShowAsync();

            if ((int)rs.Id == 0)
            {
                this.Frame.Navigate(typeof(LoginPage));

            }
            else if ((int)rs.Id == 1)
            {
                this.Frame.Navigate(typeof(RegistryPage));

            }
        }

        private async void dentranghuongdan(object sender, TappedRoutedEventArgs e)
        {
            var msg = new MessageDialog("Vui lòng đăng nhập để xêm nội dung này");

            msg.Commands.Add(new UICommand() { Id = 0, Label = "Đăng nhập" });

            msg.Commands.Add(new UICommand() { Id = 1, Label = "Đăng ký" });
            msg.Commands.Add(new UICommand() { Id = 2, Label = "Hủy" });

            var rs = await msg.ShowAsync();

            if ((int)rs.Id == 0)
            {
                this.Frame.Navigate(typeof(LoginPage));

            }
            else if ((int)rs.Id == 1)
            {
                this.Frame.Navigate(typeof(RegistryPage));

            }
        }

        private async void dieukhoansudung(object sender, TappedRoutedEventArgs e)
        {
            var msg = new MessageDialog("Vui lòng đăng nhập tham gia diễn đàn KidsMath");

            msg.Commands.Add(new UICommand() { Id = 0, Label = "Đăng nhập" });

            msg.Commands.Add(new UICommand() { Id = 1, Label = "Đăng ký" });
            msg.Commands.Add(new UICommand() { Id = 2, Label = "Hủy" });

            var rs = await msg.ShowAsync();

            if ((int)rs.Id == 0)
            {
                this.Frame.Navigate(typeof(LoginPage));

            }
            else if ((int)rs.Id == 1)
            {
                this.Frame.Navigate(typeof(RegistryPage));

            }
        }

        private async void tap_khoahoc(object sender, TappedRoutedEventArgs e)
        {

            var msg = new MessageDialog("Vui lòng đăng nhập để mua hoặc học thử miễn phí");

            msg.Commands.Add(new UICommand() { Id = 0, Label = "Đăng nhập" });

            msg.Commands.Add(new UICommand() { Id = 1, Label = "Đăng ký" });
            msg.Commands.Add(new UICommand() { Id = 2, Label = "Hủy" });

            var rs = await msg.ShowAsync();

            if ((int)rs.Id == 0)
            {
                this.Frame.Navigate(typeof(LoginPage));

            }
            else if ((int)rs.Id == 1)
            {
                this.Frame.Navigate(typeof(RegistryPage));

            }
        }

        private async void thongbao(object sender, TappedRoutedEventArgs e)
        {

            var msg = new MessageDialog("Vui lòng đăng nhập tham gia diễn đàn KidsMath");

            msg.Commands.Add(new UICommand() { Id = 0, Label = "Đăng nhập" });

            msg.Commands.Add(new UICommand() { Id = 1, Label = "Đăng ký" });
            msg.Commands.Add(new UICommand() { Id = 2, Label = "Hủy" });

            var rs = await msg.ShowAsync();

            if ((int)rs.Id == 0)
            {
                this.Frame.Navigate(typeof(LoginPage));

            }
            else if ((int)rs.Id == 1)
            {
                this.Frame.Navigate(typeof(RegistryPage));

            }
        }
    }
}
