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
    public sealed partial class MainForum : Page
    {
         kidmathwebserviceSoapClient ws;
        public MainForum()
        {
            this.InitializeComponent();
            Loaded += MainForum_Loaded;
        }

        private void MainForum_Loaded(object sender, RoutedEventArgs e)
        {
            DateTime now = DateTime.Now;
            ws = new kidmathwebserviceSoapClient();
            List<mainForum> dsbaiviet = new List<mainForum>();
            dsbaiviet = ws.getDatamainForumAsync().Result.Body.getDatamainForumResult.ToList<mainForum>();
            dsbaiviet = dsbaiviet.FindAll(baiviet => baiviet.tinhTrang != "pheduyet");
            grview_dsbinhluan.ItemsSource = dsbaiviet;
            grview_dsbinhluanmoinhat.ItemsSource = dsbaiviet.FindAll(baiviet => baiviet.luotxem > 30);
        }

        private void navigateto_baiviet(object sender, TappedRoutedEventArgs e)
        {
            ws = new kidmathwebserviceSoapClient();
            StackPanel baivietduocchon = sender as StackPanel;
            mainForum temp = baivietduocchon.DataContext as mainForum;
            binhLuan_Forum.baivietduocchon = temp;
            Forum_Baiviet.baivietdangxem = temp;
            this.Frame.Navigate(typeof(Forum_Baiviet));
        }

        private void trangchu(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage_AffterLogin));
        }

        private void trangcanhan(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Page_UserInfo));
        }

        private void dangbai(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(forum_DangBai));
        }

        private void timkhoacungtag(object sender, RoutedEventArgs e)
        {
            Button btntag = sender as Button;
            mainForum temp = btntag.DataContext as mainForum;
            ws = new kidmathwebserviceSoapClient();
            List<mainForum> dsbaiviet2 = new List<mainForum>();
            dsbaiviet2 = ws.getDatamainForumAsync().Result.Body.getDatamainForumResult.ToList<mainForum>();
            dsbaiviet2 = dsbaiviet2.FindAll(baiviet => baiviet.tinhTrang != "pheduyet" && baiviet.tag == temp.tag);
            grview_dsbinhluan.ItemsSource = dsbaiviet2;
        }
    }
}
