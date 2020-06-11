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
    public sealed partial class Admin_UserManagementPage : Page
    {
        public Admin_UserManagementPage()
        {
            this.InitializeComponent();
            Loaded += Userinfomation_Load;  
            
        }
        kidmathwebserviceSoapClient ws;
        List<nguoiDung> listuser = new List<nguoiDung>();
        private void Userinfomation_Load(object sender, RoutedEventArgs e)
        {
            ws = new kidmathwebserviceSoapClient();            
            listuser = ws.getDataNguoiDungAsync().Result.Body.getDataNguoiDungResult.ToList<nguoiDung>();
            listuser = listuser.FindAll(user => user.username != "admin" && user.username != "");
            foreach(var user in listuser)
            {
                if (user.kieuThanhVien == "U") user.kieuThanhVien = "Bình thường";
                else if (user.kieuThanhVien == "B") user.kieuThanhVien = "Bị khóa";
            }
            gvsachnguoidung.ItemsSource = listuser;           

        }

        private async void khoanguoidung_mokhoaclick(object sender, RoutedEventArgs e)
        {
            Button userstatus = sender as Button;
            nguoiDung tempuser = userstatus.DataContext as nguoiDung;

            if(tempuser.kieuThanhVien == "Bình thường")
            {
                userstatus.Content = "Bình thường";
                var msg = new MessageDialog("Bạn muốn khóa tài khoản này ?");
                msg.Commands.Add(new UICommand() { Id = 0, Label = "Xác nhận khóa" });
                msg.Commands.Add(new UICommand() { Id = 1, Label = "Hủy" });
                var rs = await msg.ShowAsync();
                if ((int)rs.Id == 0)
                {
                    ws = new kidmathwebserviceSoapClient();
                    string sql_khoatk = "UPDATE nguoiDung SET nguoiDung.kieuThanhVien ='B' WHERE nguoiDung.username ='"+tempuser.username+"';";
                    if (ws.ThucHienLenhAsync(sql_khoatk).Result.Body.ThucHienLenhResult != 0)
                    {
                        userstatus.Content = "Bị khóa";
                        var msg_ = new MessageDialog("Đã khóa tài khoản").ShowAsync();
                    }
                    else { var msg_2 = new MessageDialog("Xảy ra lỗi").ShowAsync(); }
                }
            }
            else if(tempuser.kieuThanhVien == "Bị khóa")
            {
                userstatus.Content = "Bị khóa";
                var msg = new MessageDialog("Bạn muốn mở khóa tài khoản này ?");
                msg.Commands.Add(new UICommand() { Id = 0, Label = "Xác nhận mở khóa" });
                msg.Commands.Add(new UICommand() { Id = 1, Label = "Hủy" });
                var rs = await msg.ShowAsync();
                if ((int)rs.Id == 0)
                {
                    ws = new kidmathwebserviceSoapClient();
                    string sql_khoatk = "UPDATE nguoiDung SET nguoiDung.kieuThanhVien ='U' WHERE nguoiDung.username ='" + tempuser.username + "';";
                    if (ws.ThucHienLenhAsync(sql_khoatk).Result.Body.ThucHienLenhResult != 0)
                    {
                        userstatus.Content = "Bình thường";
                        var msg_ = new MessageDialog("Đã mở khóa tài khoản").ShowAsync();
                    }
                    else { var msg_2 = new MessageDialog("Xảy ra lỗi").ShowAsync(); }
                }
            }
            
             

        }
    }
   
}
