using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
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
    public sealed partial class BaithiPage : Page
    {
        private DispatcherTimer timer;
        public static string mabaihocdangthi;
        private int thoigianthi_giay = 0;
        private int thoigianthi_phut = 1;
        private int checktamdung = 1;
        int caudangchon = -1;
        kidmathwebserviceSoapClient ws;
       
        public BaithiPage()
        {

           
            this.InitializeComponent();
          
            Loaded += BaithiPage_Loaded;
        }

        private void BaithiPage_Loaded(object sender, RoutedEventArgs e)
        {
            btn_batdauthi.IsEnabled = true;
            btntamdung.IsEnabled = false;
            this.NavigationCacheMode = NavigationCacheMode.Required;
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += timer_Tick;
        }

        void timer_Tick(object sender, object e)
        {
            thoigianthi_giay = thoigianthi_giay - 1;
            Thoigianthiconlai_giay.Text = thoigianthi_giay.ToString();
            if (thoigianthi_giay == -1)
            {
                thoigianthi_giay = 59;
                thoigianthi_phut = thoigianthi_phut - 1;               
                Thoigianthiconlai_phut.Text = thoigianthi_phut.ToString();
                Thoigianthiconlai_giay.Text = thoigianthi_giay.ToString();



                if (thoigianthi_phut < 0)
                {
                    timer.Stop();
                    btn_batdauthi.IsEnabled = true;
                    hoanthanhbaithi();
                }
               
            }

        }
        
       async void hoanthanhbaithi()
        {
            int ketqua = kiemtradapan(ketquabaithi,dapandung);
            switch (ketqua)
            {

                case 0:

                    var msg0 = new MessageDialog("Bạn không hề làm bài ? Hay thật sự chỉ được 0 điểm ....Agrrr ");

                    msg0.Commands.Add(new UICommand() { Id = 0, Label = "Thi lại" });
                    msg0.Commands.Add(new UICommand() { Id = 1, Label = "Hủy" });


                    var rs0 = await msg0.ShowAsync();

                    if ((int)rs0.Id == 0)
                    {
                        this.Frame.Navigate(typeof(BaithiPage));

                    }
                    else if ((int)rs0.Id == 1)
                    {
                    }
                    break;
                case 1:

                    var msg1 = new MessageDialog("Bạn được 1 điểm ! Hãy xem lại kiến thức ");

                    msg1.Commands.Add(new UICommand() { Id = 0, Label = "Thi lại" });
                    msg1.Commands.Add(new UICommand() { Id = 1, Label = "Hủy" });


                    var rs1 = await msg1.ShowAsync();

                    if ((int)rs1.Id == 0)
                    {
                        this.Frame.Navigate(typeof(BaithiPage));

                    }
                    else if ((int)rs1.Id == 1)
                    {
                    }
                    break;
                case 2:

                    var msg2 = new MessageDialog("Bạn được 2 điểm ! Hãy ôn tập thật kỹ trước khi thi ");

                    msg2.Commands.Add(new UICommand() { Id = 0, Label = "Thi lại" });
                    msg2.Commands.Add(new UICommand() { Id = 1, Label = "Hủy" });


                    var rs2 = await msg2.ShowAsync();

                    if ((int)rs2.Id == 0)
                    {
                        this.Frame.Navigate(typeof(BaithiPage));

                    }
                    else if ((int)rs2.Id == 1)
                    {
                    }
                    break;
                case 3:

                    var msg3 = new MessageDialog("Bạn được 3 điểm ! Đọc kỹ lại đề và làm lần nữa ");

                    msg3.Commands.Add(new UICommand() { Id = 0, Label = "Thi lại" });
                    msg3.Commands.Add(new UICommand() { Id = 1, Label = "Hủy" });


                    var rs3 = await msg3.ShowAsync();

                    if ((int)rs3.Id == 0)
                    {
                        this.Frame.Navigate(typeof(BaithiPage));

                    }
                    else if ((int)rs3.Id == 1)
                    {
                    }
                    break;
                case 4:

                    var msg4 = new MessageDialog("Bạn được 4 điểm ! Vẫn chưa đạt yêu cầu");

                    msg4.Commands.Add(new UICommand() { Id = 0, Label = "Thi lại" });
                    msg4.Commands.Add(new UICommand() { Id = 1, Label = "Hủy" });


                    var rs4 = await msg4.ShowAsync();

                    if ((int)rs4.Id == 0)
                    {
                        this.Frame.Navigate(typeof(BaithiPage));

                    }
                    else if ((int)rs4.Id == 1)
                    {
                    }
                    break;
                case 5:

                    var msg5 = new MessageDialog("Bạn được 5 điểm ! Hãy thử làm những đạng toán khó hơn đi ");

                    msg5.Commands.Add(new UICommand() { Id = 0, Label = "Thi lại" });
                    msg5.Commands.Add(new UICommand() { Id = 1, Label = "Hủy" });


                    var rs5 = await msg5.ShowAsync();

                    if ((int)rs5.Id == 0)
                    {
                        this.Frame.Navigate(typeof(BaithiPage));

                    }
                    else if ((int)rs5.Id == 1)
                    {
                    }
                    break;
                case 6:

                    var msg6 = new MessageDialog("Bạn được 6 điểm ! Cố thêm 1 chút nữa nào ");

                    msg6.Commands.Add(new UICommand() { Id = 0, Label = "Thi lại" });
                    msg6.Commands.Add(new UICommand() { Id = 1, Label = "Hủy" });


                    var rs6 = await msg6.ShowAsync();

                    if ((int)rs6.Id == 0)
                    {
                        this.Frame.Navigate(typeof(BaithiPage));

                    }
                    else if ((int)rs6.Id == 1)
                    {
                    }
                    break;
                case 7:

                    var msg7 = new MessageDialog("Bạn được 7 điểm ! Khá ổn rồi đó");

                    msg7.Commands.Add(new UICommand() { Id = 0, Label = "Thi lại" });
                    msg7.Commands.Add(new UICommand() { Id = 1, Label = "Hủy" });


                    var rs7 = await msg7.ShowAsync();

                    if ((int)rs7.Id == 0)
                    {
                        this.Frame.Navigate(typeof(BaithiPage));

                    }
                    else if ((int)rs7.Id == 1)
                    {
                    }
                    break;
                case 8:

                    var msg8 = new MessageDialog("Bạn được 8 điểm ! Không tồi nhỉ ");

                    msg8.Commands.Add(new UICommand() { Id = 0, Label = "Thi lại" });
                    msg8.Commands.Add(new UICommand() { Id = 1, Label = "Hủy" });


                    var rs8 = await msg8.ShowAsync();

                    if ((int)rs8.Id == 0)
                    {
                        this.Frame.Navigate(typeof(BaithiPage));

                    }
                    else if ((int)rs8.Id == 1)
                    {
                    }
                    break;
                case 9:

                    var msg9 = new MessageDialog("Bạn được 9 điểm ! Xuất sắc - 9 điểm là điểm số cao nhất ");

                    msg9.Commands.Add(new UICommand() { Id = 0, Label = "Thi lại" });
                    msg9.Commands.Add(new UICommand() { Id = 1, Label = "Hủy" });


                    var rs9 = await msg9.ShowAsync();

                    if ((int)rs9.Id == 0)
                    {
                        this.Frame.Navigate(typeof(BaithiPage));

                    }
                    else if ((int)rs9.Id == 1)
                    {
                    }
                    break;

                default:
                    break;
            }

        }
        private void btnbatdauthi_Click(object sender, RoutedEventArgs e)
        {
            if (mabaihocdangthi == "" | mabaihocdangthi == null)
            {
                var msgerr = new MessageDialog("Vui lòng chọn bài ").ShowAsync();
            }


           else if (btntamdung.IsEnabled == false && checktamdung< 3)
            {
                
                btn_batdauthi.IsEnabled = false;
                btn_batdauthi.Opacity = 0.5;
                txtblock_character.Text = " : ";
                btntamdung.IsEnabled = true;
                checktamdung += 1;
                Thoigianthiconlai_phut.Text = thoigianthi_phut.ToString();
                Thoigianthiconlai_giay.Text = thoigianthi_giay.ToString();
                timer.Start();
            }
            else if (btntamdung.IsEnabled == false && checktamdung >= 3)
            {

            }
            else {
                thoigianthi_giay = Convert.ToInt32(Thoigianthiconlai_giay.Text);
                thoigianthi_phut = Convert.ToInt32(Thoigianthiconlai_phut.Text);
                btn_batdauthi.Opacity = 0.5;
                btn_batdauthi.IsEnabled = false;
                txtblock_character.Text = " : ";
                btntamdung.IsEnabled = true;
                Thoigianthiconlai_phut.Text = thoigianthi_phut.ToString();
                Thoigianthiconlai_giay.Text = thoigianthi_giay.ToString();
                timer.Start();
            }
           
        }
        private async void btntamdung_click(object sender, RoutedEventArgs e)
        {
            if (checktamdung < 3)
            {
                timer.Stop();
                thoigianthi_giay = Convert.ToInt32(Thoigianthiconlai_giay.Text);
                thoigianthi_phut = Convert.ToInt32(Thoigianthiconlai_phut.Text);
                Thoigianthiconlai_phut.Text = thoigianthi_phut.ToString();
                Thoigianthiconlai_giay.Text = thoigianthi_giay.ToString();
                checktamdung += 1;
                btn_batdauthi.Opacity = 1;
                btntamdung.Opacity = 0.5;
                btntamdung.IsEnabled = false;
                btn_batdauthi.IsEnabled = true;
                
            }
            else
            {
                await(new MessageDialog("Bạn chỉ có thể tạm dừng 3 lần trong 1 bài thi", "Error").ShowAsync());
            }

        }
        private void btnclick_cauhoi(string buttonnumber)
        {
            if (btn_batdauthi.IsEnabled == false)
            {
               
                caudangchon = int.Parse(buttonnumber) - 1;
                namedapanA.Visibility = Visibility.Visible;
                namedapanB.Visibility = Visibility.Visible;
                namedapanC.Visibility = Visibility.Visible;
                namedapanD.Visibility = Visibility.Visible;
                daa.Visibility = Visibility.Visible;
                dab.Visibility = Visibility.Visible;
                dac.Visibility = Visibility.Visible;
                dad.Visibility = Visibility.Visible;
                ws = new kidmathwebserviceSoapClient();
                List<danhSachCauHoi> dscauhoi = new List<danhSachCauHoi>();
                dscauhoi = ws.getDataDanhSachCauHoiAsync().Result.Body.getDataDanhSachCauHoiResult.ToList<danhSachCauHoi>();
                danhSachCauHoi cauhoi = new danhSachCauHoi();
                cauhoi = dscauhoi.Find(quest => quest.maCauHoi == buttonnumber && quest.maBai == mabaihocdangthi);
                txtblock_Cauhoi.Text = cauhoi.tenCauHoi;
                txtblock_A.Text = cauhoi.A;
                txtblock_B.Text = cauhoi.B;
                txtblock_C.Text = cauhoi.C;
                txtblock_D.Text = cauhoi.D;
                if (ketquabaithi[caudangchon] == 'A') namedapanA.IsChecked = true;
                else if(ketquabaithi[caudangchon] == 'B') namedapanB.IsChecked = true;
                else if (ketquabaithi[caudangchon] == 'C') namedapanC.IsChecked = true;
                else if (ketquabaithi[caudangchon] == 'D') namedapanD.IsChecked = true;
                dapandung[caudangchon] = cauhoi.dapAnDung;
                
            }
            else { var msgerr = new MessageDialog("Lỗi xảy ra khi chọn câu hỏi. Hãy chắc chắn rằng bạn đã chọn bài và ấn vào nút Bắt đầu thi").ShowAsync(); };
           
            


        }
        Char[] ketquabaithi = new char[9];
        Char[] dapandung = new char[9];
        private async void btnclick_nopbaithi(object sender, RoutedEventArgs e)
        {
            int ketqua = kiemtradapan(ketquabaithi, dapandung);
            switch (ketqua)
            {

                case 0:

                    var msg0 = new MessageDialog("Bạn không hề làm bài ? Hay thật sự chỉ được 0 điểm ....Agrrr ");

                    msg0.Commands.Add(new UICommand() { Id = 0, Label = "Thi lại" });
                    msg0.Commands.Add(new UICommand() { Id = 1, Label = "Hủy" });


                    var rs0 = await msg0.ShowAsync();

                    if ((int)rs0.Id == 0)
                    {
                        this.Frame.Navigate(typeof(BaithiPage));

                    }
                    else if ((int)rs0.Id == 1)
                    {
                    }
                    break;
                case 1:

                    var msg1 = new MessageDialog("Bạn được 1 điểm ! Hãy xem lại kiến thức ");

                    msg1.Commands.Add(new UICommand() { Id = 0, Label = "Thi lại" });
                    msg1.Commands.Add(new UICommand() { Id = 1, Label = "Hủy" });
                    

                    var rs1 = await msg1.ShowAsync();

                    if ((int)rs1.Id == 0)
                    {
                        this.Frame.Navigate(typeof(BaithiPage));

                    }
                    else if ((int)rs1.Id == 1)
                    {
                    }
                    break;
                case 2:

                    var msg2 = new MessageDialog("Bạn được 2 điểm ! Hãy ôn tập thật kỹ trước khi thi ");

                    msg2.Commands.Add(new UICommand() { Id = 0, Label = "Thi lại" });
                    msg2.Commands.Add(new UICommand() { Id = 1, Label = "Hủy" });


                    var rs2 = await msg2.ShowAsync();

                    if ((int)rs2.Id == 0)
                    {
                        this.Frame.Navigate(typeof(BaithiPage));

                    }
                    else if ((int)rs2.Id == 1)
                    {
                    }
                    break;
                case 3:

                    var msg3 = new MessageDialog("Bạn được 3 điểm ! Đọc kỹ lại đề và làm lần nữa ");

                    msg3.Commands.Add(new UICommand() { Id = 0, Label = "Thi lại" });
                    msg3.Commands.Add(new UICommand() { Id = 1, Label = "Hủy" });


                    var rs3 = await msg3.ShowAsync();

                    if ((int)rs3.Id == 0)
                    {
                        this.Frame.Navigate(typeof(BaithiPage));

                    }
                    else if ((int)rs3.Id == 1)
                    {
                    }
                    break;
                case 4:

                    var msg4 = new MessageDialog("Bạn được 4 điểm ! Vẫn chưa đạt yêu cầu");

                    msg4.Commands.Add(new UICommand() { Id = 0, Label = "Thi lại" });
                    msg4.Commands.Add(new UICommand() { Id = 1, Label = "Hủy" });


                    var rs4 = await msg4.ShowAsync();

                    if ((int)rs4.Id == 0)
                    {
                        this.Frame.Navigate(typeof(BaithiPage));

                    }
                    else if ((int)rs4.Id == 1)
                    {
                    }
                    break;
                case 5:

                    var msg5 = new MessageDialog("Bạn được 5 điểm ! Hãy thử làm những đạng toán khó hơn đi ");

                    msg5.Commands.Add(new UICommand() { Id = 0, Label = "Thi lại" });
                    msg5.Commands.Add(new UICommand() { Id = 1, Label = "Hủy" });


                    var rs5 = await msg5.ShowAsync();

                    if ((int)rs5.Id == 0)
                    {
                        this.Frame.Navigate(typeof(BaithiPage));

                    }
                    else if ((int)rs5.Id == 1)
                    {
                    }
                    break;
                case 6:

                    var msg6 = new MessageDialog("Bạn được 6 điểm ! Cố thêm 1 chút nữa nào ");

                    msg6.Commands.Add(new UICommand() { Id = 0, Label = "Thi lại" });
                    msg6.Commands.Add(new UICommand() { Id = 1, Label = "Hủy" });


                    var rs6 = await msg6.ShowAsync();

                    if ((int)rs6.Id == 0)
                    {
                        this.Frame.Navigate(typeof(BaithiPage));

                    }
                    else if ((int)rs6.Id == 1)
                    {
                    }
                    break;
                case 7:

                    var msg7 = new MessageDialog("Bạn được 7 điểm ! Khá ổn rồi đó");

                    msg7.Commands.Add(new UICommand() { Id = 0, Label = "Thi lại" });
                    msg7.Commands.Add(new UICommand() { Id = 1, Label = "Hủy" });


                    var rs7 = await msg7.ShowAsync();

                    if ((int)rs7.Id == 0)
                    {
                        this.Frame.Navigate(typeof(BaithiPage));

                    }
                    else if ((int)rs7.Id == 1)
                    {
                    }
                    break;
                case 8:

                    var msg8 = new MessageDialog("Bạn được 8 điểm ! Không tồi nhỉ ");

                    msg8.Commands.Add(new UICommand() { Id = 0, Label = "Thi lại" });
                    msg8.Commands.Add(new UICommand() { Id = 1, Label = "Hủy" });


                    var rs8 = await msg8.ShowAsync();

                    if ((int)rs8.Id == 0)
                    {
                        this.Frame.Navigate(typeof(BaithiPage));

                    }
                    else if ((int)rs8.Id == 1)
                    {
                    }
                    break;
                case 9:

                    var msg9 = new MessageDialog("Bạn được 9 điểm ! Xuất sắc - 9 điểm là điểm số cao nhất ");

                    msg9.Commands.Add(new UICommand() { Id = 0, Label = "Thi lại" });
                    msg9.Commands.Add(new UICommand() { Id = 1, Label = "Hủy" });


                    var rs9 = await msg9.ShowAsync();

                    if ((int)rs9.Id == 0)
                    {
                        this.Frame.Navigate(typeof(BaithiPage));

                    }
                    else if ((int)rs9.Id == 1)
                    {
                    }
                    break;

                default:
                    break;
            }
        }
        private int kiemtradapan(char[] dsdapan , char[] dsdapandung)
        {
            int ketqua = 0;

            for (int i = 0; i < 9; i++)
            {
                if (dapandung[i] == ketquabaithi[i]) ketqua += 1;
            }
            return ketqua;
        }
        private void btn1_click(object sender, RoutedEventArgs e)
        {
            btnclick_cauhoi("1");
            bt1.Opacity = 0.5;


        }
        private void btn2_click(object sender, RoutedEventArgs e)
        {
            btnclick_cauhoi("2");
            bt2.Opacity = 0.5;

        }
        private void btn3_click(object sender, RoutedEventArgs e)
        {
            btnclick_cauhoi("3");
            bt3.Opacity = 0.5;

        }
        private void btn4_click(object sender, RoutedEventArgs e)
        {
            btnclick_cauhoi("4");
            bt4.Opacity = 0.5;

        }
        private void btn5_click(object sender, RoutedEventArgs e)
        {
            btnclick_cauhoi("5");
            bt5.Opacity = 0.5;
           

        }
        private void btn6_click(object sender, RoutedEventArgs e)
        {
            btnclick_cauhoi("6");
            bt6.Opacity = 0.5;
       

        }
        private void btn7_click(object sender, RoutedEventArgs e)
        {
            btnclick_cauhoi("7");
            bt7.Opacity = 0.5;
        
        }
        private void btn8_click(object sender, RoutedEventArgs e)
        {
            btnclick_cauhoi("8");
            bt8.Opacity = 0.5;
           

        }

        private void btn9_click(object sender, RoutedEventArgs e)
        {
            btnclick_cauhoi("9");
            bt9.Opacity = 0.5;
        

        }

        private void dapanA(object sender, RoutedEventArgs e)
        {
            if(caudangchon != -1)
            {
                ketquabaithi[caudangchon] = 'A';
            }
        }

        private void dapanB(object sender, RoutedEventArgs e)
        {
            if (caudangchon != -1 )
            {
                ketquabaithi[caudangchon] = 'B';
            }
        }

        private void dapanC(object sender, RoutedEventArgs e)
        {
            if (caudangchon != -1)
            {
                ketquabaithi[caudangchon] = 'C';
            }
        }

        private void dapanD(object sender, RoutedEventArgs e)
        {
            if (caudangchon != -1)
            {
                ketquabaithi[caudangchon] = 'D';
            }
        }
    }
}

