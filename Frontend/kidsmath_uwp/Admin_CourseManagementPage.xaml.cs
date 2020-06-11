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
    public sealed partial class Admin_CourseManagementLoaded : Page
    {
        public Admin_CourseManagementLoaded()
        {
            this.InitializeComponent();
            Loaded += Admin_CourseInfomationLoaded;
            
        }
        kidmathwebserviceSoapClient ws;

        private void Admin_CourseInfomationLoaded(object sender, RoutedEventArgs e)
        {
            ws = new kidmathwebserviceSoapClient();
            List<khoaHoc> dskhoahoc = new List<khoaHoc>();
            dskhoahoc = ws.getDataKhoaHocAsync().Result.Body.getDataKhoaHocResult.ToList<khoaHoc>();
            dskhoahoc = dskhoahoc.FindAll(khoahoc => khoahoc.maKhoaHoc != "");
            gvdsachKhoaHoc.ItemsSource = dskhoahoc;
            soluongkh.Text = Convert.ToString(dskhoahoc.Count);
        }

        private void Click_Chinhsuakhoahoc(object sender, RoutedEventArgs e)
        {
            Button chinhsuakhoahoc = sender as Button;
            khoaHoc temp = chinhsuakhoahoc.DataContext as khoaHoc;
            Chinhsuakhoahoc.khoahoccanchinhsua = temp;
            this.Frame.Navigate(typeof(Chinhsuakhoahoc));

        }

        private void refesh_page(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Admin_CourseManagementLoaded));
        }

        private void show_msg(object sender, RoutedEventArgs e)
        {
            var msg = new MessageDialog("Vui lòng chọn khóa học và nhấn nút Chỉnh sửa tương ứng").ShowAsync();

        }
    }
   

    }

