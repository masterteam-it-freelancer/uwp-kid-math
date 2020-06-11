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
    public sealed partial class AdminMainPage : Page
    {
        public AdminMainPage()
        {
            this.InitializeComponent();
        }

        private void btnclick_ShowMenu(object sender, RoutedEventArgs e)
        {
            splitview_adminpage.IsPaneOpen = !splitview_adminpage.IsPaneOpen;
            if(splitview_adminpage.IsPaneOpen == true)
            {
                Info_adminpage.Visibility = Visibility.Collapsed;
            }
            else
            {
                Info_adminpage.Visibility = Visibility.Visible;
            }
        }

        private void btnclick_MainMenu(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AdminMainPage));
        }

        private void btnclick_UserManagement(object sender, RoutedEventArgs e)
        {
            Admin_ManagementFrame.Navigate(typeof(Admin_UserManagementPage));
        }

        private void btnclick_CourseManagement(object sender, RoutedEventArgs e)
        {
            Admin_ManagementFrame.Navigate(typeof(Admin_CourseManagementLoaded));
        }

        private void btnclick_UserComments(object sender, RoutedEventArgs e)
        {
            Admin_ManagementFrame.Navigate(typeof(Admin_forum));
        }

        private void btnclick_Mailbox(object sender, RoutedEventArgs e)
        {
            Admin_ManagementFrame.Navigate(typeof(Admin_MailboxPage));
        }

        private void btnclick_Statistical(object sender, RoutedEventArgs e)
        {
            Admin_ManagementFrame.Navigate(typeof(Admin_StatisticalPage));
        }

        private void btnclick_adpagelogout(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void danhgia(object sender, RoutedEventArgs e)
        {
            Admin_ManagementFrame.Navigate(typeof(PageDanhGia));
        }
    }
}
