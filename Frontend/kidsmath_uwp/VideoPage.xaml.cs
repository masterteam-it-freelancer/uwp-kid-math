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
using Windows.Web.Http;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace kidsmath_uwp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class VideoPage : Page
    {
        public VideoPage()
        {
            this.InitializeComponent();
            Loaded += VideoPage_Loaded;
        }
        public static video videoduocchon;
        public static video videotemplate;
        private async void VideoPage_Loaded(object sender, RoutedEventArgs e)
        {
                if (videoduocchon == null && videotemplate == null)
                {

                    var msg = new MessageDialog("Hiện bài giảng này chưa sẵn có, vui lòng chọn 1 bài khác");
                    msg.Commands.Add(new UICommand() {Id = 0, Label = "OK"});
                    msg.Commands.Add(new UICommand() {Id = 1, Label = "Quay về trang chủ"});
                    var rs = await msg.ShowAsync();
                        if ((int) rs.Id == 1)
                        {
                         Frame.Navigate(typeof(MainPage_AffterLogin));
                        }
                  
                }

                else webView.Source = new Uri(videoduocchon.linkvideo);


        }
    }
}
