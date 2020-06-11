using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Net;
using Mandrill.Models;
using Microsoft.DotNet.Cli.Utils.CommandParsing;
using System.Net.Security;

namespace kidmathwebapp
{
    /// <summary>
    /// Summary description for kidmathwebservice
    /// </summary>

    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class kidmathwebservice : System.Web.Services.WebService
    {



        SqlConnection sqlConnection;
        void connect()
        {
            if (sqlConnection == null)
                sqlConnection = new SqlConnection(@"Data Source=QUYNH-PC\QUYNH;Initial Catalog=appToanTieuHocDB;User ID=sa;password=gamedev");
            sqlConnection.Open();
        }
        void closeConnect()
        {
            if (sqlConnection != null && sqlConnection.State == System.Data.ConnectionState.Open)
                sqlConnection.Close();
        }
        [WebMethod]
        public int ThucHienLenh(string sql)
        {
            int kq = 0;
            try
            {
                connect();
                SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
                kq = sqlCommand.ExecuteNonQuery();
                closeConnect();
            }
            catch
            {
                closeConnect();
                kq = 0;
            }
            return kq;
        }
        private void Jquery(string sql)
        {
            try
            {
                connect();
                SqlCommand sqlcmd = new SqlCommand(sql, sqlConnection);
                int ketqua = sqlcmd.ExecuteNonQuery();
                closeConnect();

            }
            catch
            {
                closeConnect();
            }
        }
 

       
        [WebMethod]
        public int doanhthu_funct()
        {
            int tongdoanhthu = 0;
            string sql = "SELECT SUM(soTienDaNap) as tongdoanhthu FROM nguoiDung;"; 
            try
            {
                    connect();
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, sqlConnection);
                    DataTable dt = new DataTable();
                    sqlDataAdapter.Fill(dt);
                    tongdoanhthu = Convert.ToInt32(dt.Rows[0][0].ToString());
                closeConnect();
            }
            catch
            {
                closeConnect();
                tongdoanhthu = 0;
            }
            return tongdoanhthu;
        }

        [WebMethod]
        public int nguoidungchi_funct()
        {
            int hienco = 0;
           
            string sql2 = "SELECT SUM(soTienHienCo) as hienco FROM nguoiDung;";
            try
            {
                connect();
               
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql2, sqlConnection);
                DataTable dt = new DataTable();
                sqlDataAdapter.Fill(dt);
              
                hienco = Convert.ToInt32(dt.Rows[0][0].ToString());
                closeConnect();
            }
            catch
            {
                closeConnect();
               hienco = 0;
            }
            return  hienco;
        }
        [WebMethod]
        public int checkExists(string sql)
        {

            try
            {
                connect();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, sqlConnection);
                DataTable dt = new DataTable();
                sqlDataAdapter.Fill(dt);
                closeConnect();
                return dt.Rows.Count;
            }
            catch
            {
                closeConnect();
                return 0;
            }
        }
        [WebMethod]
        public List<bai> getDataBai()
        {

            try
            {
                connect();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from bai order by bai.tenBai ", sqlConnection);
                DataTable dt = new DataTable();
                sqlDataAdapter.Fill(dt);
                //sqlDataAdapter.FillSchema(dt, SchemaType.Mapped);
                List<bai> listbai = new List<bai>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listbai.Add(new bai()
                    {
                        maBai = dt.Rows[i][0].ToString(),
                        tenBai = dt.Rows[i][1].ToString(),
                        maChuong = dt.Rows[i][2].ToString()
                    });
                }
                closeConnect();
                return listbai;
            }
            catch
            {
                closeConnect();
                return null;
            }
        }
        [WebMethod]
        public List<chuong> getDataChuong()
        {
            try
            {
                connect();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from chuong", sqlConnection);
                DataTable dt = new DataTable();
                sqlDataAdapter.Fill(dt);
                //sqlDataAdapter.FillSchema(dt, SchemaType.Mapped);
                List<chuong> listchuong = new List<chuong>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listchuong.Add(new chuong()
                    {
                        maChuong = dt.Rows[i][0].ToString(),
                        tenChuong = dt.Rows[i][1].ToString(),
                        maKhoaHoc = dt.Rows[i][2].ToString()
                    });
                }
                closeConnect();
                return listchuong;
            }
            catch
            {
                closeConnect();
                return null;
            }
        }
        [WebMethod]
        public List<khoaHoc> getDataKhoaHoc()
        {
            try
            {
                connect();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from khoaHoc", sqlConnection);
                DataTable dt = new DataTable();
                sqlDataAdapter.Fill(dt);
                //sqlDataAdapter.FillSchema(dt, SchemaType.Mapped);
                List<khoaHoc> listkhoahoc = new List<khoaHoc>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listkhoahoc.Add(new khoaHoc()
                    {
                        maKhoaHoc =  dt.Rows[i][0].ToString(),
                        tenKhoaHoc = dt.Rows[i][1].ToString(),
                        tenGiaoVien = dt.Rows[i][2].ToString(),
                        thongTinKhoaHoc = dt.Rows[i][3].ToString(),
                        soNguoiDaMua = Convert.ToInt32(dt.Rows[i][4].ToString()),
                        giaKhoaHoc = dt.Rows[i][5].ToString() ,
                        giaKhuyenMai = dt.Rows[i][6].ToString(),
                        thoiGianKhuyenMai = dt.Rows[i][7].ToString(),
                        maLop = dt.Rows[i][8].ToString(),
                        imagelink = dt.Rows[i][9].ToString(),
                        danhGia = Convert.ToInt32(dt.Rows[i][10].ToString()),
                        soLuongDanhGia = Convert.ToInt32(dt.Rows[i][11].ToString()),
                        maKhoaHocHienThi = "Mã khóa học:" + " " + dt.Rows[i][0].ToString(),
                        giaKhoaHocHienThi = "Giá thị trường:" +" " + dt.Rows[i][5].ToString() + "VNĐ",
                        giaKhuyenMaiHienThi = "Học phí:" + " " + dt.Rows[i][6].ToString() + ".VNĐ",
                        thoiGianKhuyenMaiHienThi ="Kết thúc vào:" + " " + Convert.ToDateTime(dt.Rows[i][7].ToString()),
                        maLopHienThi ="Mã lớp:"+ dt.Rows[i][8].ToString(),
                        doanhThu_khongkhuyenmai = Convert.ToInt32(dt.Rows[i][4].ToString()) * Convert.ToInt32(dt.Rows[i][5].ToString()),
                        doanhThu = Convert.ToInt32(dt.Rows[i][4].ToString()) * Convert.ToInt32(dt.Rows[i][6].ToString()),

                    });
                }
                closeConnect();
                return listkhoahoc;
            }
            catch
            {
                closeConnect();
                return null;
            }
        }

        [WebMethod]
        public List<muaKhoaHoc> getDatMuaKhoaHoc()
        {
            try
            {
                connect();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from muaKhoaHoc", sqlConnection);
                DataTable dt = new DataTable();
                sqlDataAdapter.Fill(dt);
                //sqlDataAdapter.FillSchema(dt, SchemaType.Mapped);
                List<muaKhoaHoc> listmuakhoahoc = new List<muaKhoaHoc>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listmuakhoahoc.Add(new muaKhoaHoc()
                    {
                        orderid = dt.Rows[i][0].ToString(),
                        maKhoaHoc = dt.Rows[i][1].ToString(),
                        username = dt.Rows[i][2].ToString(),
                        thoiGianMua = Convert.ToDateTime(dt.Rows[i][3].ToString()),
                        giaKhoaHoc = Convert.ToInt32(dt.Rows[i][4].ToString())
                    });
                }
                closeConnect();
                return listmuakhoahoc;
            }
            catch
            {
                closeConnect();
                return null;
            }
        }


        [WebMethod]
        public List<lichSuNapTien> getDataLichSuNapTien()
        {
            try
            {
                connect();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from lichSuNapTien", sqlConnection);
                DataTable dt = new DataTable();
                sqlDataAdapter.Fill(dt);
                //sqlDataAdapter.FillSchema(dt, SchemaType.Mapped);
                List<lichSuNapTien> listlichsunap = new List<lichSuNapTien>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listlichsunap.Add(new lichSuNapTien()
                    {
                        orderid = dt.Rows[i][0].ToString(),
                        soTienNap = Convert.ToInt32(dt.Rows[i][1].ToString()),
                        username = dt.Rows[i][2].ToString(),
                        thoiGianNapXacNhan = Convert.ToDateTime(dt.Rows[i][3].ToString()),
                        
                    });
                }
                closeConnect();
                return listlichsunap;
            }
            catch
            {
                closeConnect();
                return null;
            }
        }

        [WebMethod]
        public List<nguoiDung> getDataNguoiDung()
        {
            try
            {
                connect();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from nguoiDung", sqlConnection);
                DataTable dt = new DataTable();
                sqlDataAdapter.Fill(dt);
                //sqlDataAdapter.FillSchema(dt, SchemaType.Mapped);
                List<nguoiDung> listnguoidung = new List<nguoiDung>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listnguoidung.Add(new nguoiDung()
                    {
                        username = dt.Rows[i][0].ToString(),
                        password = dt.Rows[i][1].ToString(),
                        tenNguoiDung = dt.Rows[i][2].ToString(),
                        soDienThoai = dt.Rows[i][3].ToString(),
                        email = dt.Rows[i][4].ToString(),
                        soTienDaNap = Convert.ToInt32(dt.Rows[i][5].ToString()),
                        soTienHienCo = Convert.ToInt32(dt.Rows[i][6].ToString()),
                        kieuThanhVien = dt.Rows[i][7].ToString(),
                        
                    });
                }
                closeConnect();
                return listnguoidung;
            }
            catch
            {
                closeConnect();
                return null;
            }
        }

        [WebMethod]
        public List<danhGia> getDataDanhGia()
        {
              try
            {
                connect();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from danhGia", sqlConnection);
                DataTable dt = new DataTable();
                sqlDataAdapter.Fill(dt);
                //sqlDataAdapter.FillSchema(dt, SchemaType.Mapped);
                List<danhGia> listdanhgia = new List<danhGia>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listdanhgia.Add(new danhGia()
                    {
       
                        
                        username = dt.Rows[i][0].ToString(),
                        rate =dt.Rows[i][1].ToString(),
                        maKhoaHoc = dt.Rows[i][2].ToString(),
                        tieuDe = dt.Rows[i][3].ToString(),
                        nhanXet = dt.Rows[i][4].ToString(),
                        ngayDuyet = Convert.ToDateTime(dt.Rows[i][5].ToString()),
                        tinhTrang = dt.Rows[i][6].ToString(),

                    });
                }
                closeConnect();
                return listdanhgia;
            }
            catch
            {
                closeConnect();
                return null;
            }
        }

        [WebMethod]
        public List<video> getDatavideo()
        {
            try
            {
                connect();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from video", sqlConnection);
                DataTable dt = new DataTable();
                sqlDataAdapter.Fill(dt);
                //sqlDataAdapter.FillSchema(dt, SchemaType.Mapped);
                List<video> listvideo = new List<video>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listvideo.Add(new video()
                    {

                        maBai = dt.Rows[i][0].ToString(),
                        tenBai = dt.Rows[i][1].ToString(),                        
                        maChuong = dt.Rows[i][2].ToString(),
                        linkvideo = dt.Rows[i][3].ToString(),
                        
                    });
                }
                closeConnect();
                return listvideo;
            }
            catch
            {
                closeConnect();
                return null;
            }
        }


        [WebMethod]
        public List<baiTap> getDatabaiTap()
        {
            try
            {
                connect();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from baiTap", sqlConnection);
                DataTable dt = new DataTable();
                sqlDataAdapter.Fill(dt);
                //sqlDataAdapter.FillSchema(dt, SchemaType.Mapped);
                List<baiTap> listbaitap = new List<baiTap>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listbaitap.Add(new baiTap()
                    {
                        link = dt.Rows[i][0].ToString(),
                        maBai = dt.Rows[i][1].ToString(),
                    });
                }
                closeConnect();
                return listbaitap;
            }
            catch
            {
                closeConnect();
                return null;
            }
        }

        [WebMethod]
        public List<thongBao> getDatathongBao()
        {
            try
            {
                connect();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from thongBao", sqlConnection);
                DataTable dt = new DataTable();
                sqlDataAdapter.Fill(dt);
                //sqlDataAdapter.FillSchema(dt, SchemaType.Mapped);
                List<thongBao> listthongbao = new List<thongBao>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listthongbao.Add(new thongBao()
                    {

                        loaiThongBao = dt.Rows[i][0].ToString(),
                        tinhTrang = Convert.ToChar(dt.Rows[i][1].ToString()),
                        username = dt.Rows[i][2].ToString(),
                        tieuDe = dt.Rows[i][3].ToString(),
                        noiDung = dt.Rows[i][4].ToString(),
                        ngayGui = Convert.ToDateTime(dt.Rows[i][5].ToString()),
                        soTien = Convert.ToInt32(dt.Rows[i][6].ToString()),


                    });
                }
                closeConnect();
                return listthongbao;
            }
            catch
            {
                closeConnect();
                return null;
            }
        }




        [WebMethod]
        public List<mainForum> getDatamainForum()
        {
            try
            {
                connect();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from mainForum", sqlConnection);
                DataTable dt = new DataTable();
                sqlDataAdapter.Fill(dt);
                //sqlDataAdapter.FillSchema(dt, SchemaType.Mapped);
                List<mainForum> listbaiviet = new List<mainForum>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listbaiviet.Add(new mainForum()
                    {
                        idbaiviet = dt.Rows[i][0].ToString(),
                        username = dt.Rows[i][1].ToString(),
                        tenNguoiDung = dt.Rows[i][2].ToString(),
                        tinhTrang = dt.Rows[i][3].ToString(),
                        ngayDuyet = Convert.ToDateTime(dt.Rows[i][4].ToString()),
                        tieuDe = dt.Rows[i][5].ToString(),
                        noiDung = dt.Rows[i][6].ToString(),
                        tag = dt.Rows[i][7].ToString(),
                        luotbinhluan = Convert.ToInt32(dt.Rows[i][8].ToString()),
                        luotxem = Convert.ToInt32(dt.Rows[i][9].ToString()),

                    });

                }
                closeConnect();
                return listbaiviet;
            }
            catch
            {
                closeConnect();
                return null;
            }

        }


        [WebMethod]
        public List<traloiForum> GetDatatraLoiForum()
        {
            try
            {
                connect();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from traloiForum", sqlConnection);
                DataTable dt = new DataTable();
                sqlDataAdapter.Fill(dt);
                //sqlDataAdapter.FillSchema(dt, SchemaType.Mapped);
                List<traloiForum> listdanhsachcautraloi = new List<traloiForum>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listdanhsachcautraloi.Add(new traloiForum()
                    {
                        idbaiviet = dt.Rows[i][0].ToString(),
                        username = dt.Rows[i][1].ToString(),
                        tenNguoiDung = dt.Rows[i][2].ToString(),
                        binhLuan = dt.Rows[i][3].ToString(),
                        ngayBinhLuan = Convert.ToDateTime(dt.Rows[i][4].ToString()),
                       

                    });
                }
                closeConnect();
                return listdanhsachcautraloi;
            }
            catch
            {
                closeConnect();
                return null;
            }
        }


        [WebMethod]
        public List<danhSachCauHoi>getDataDanhSachCauHoi()
        {
            try
            {
                connect();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from danhSachCauHoi", sqlConnection);
                DataTable dt = new DataTable();
                sqlDataAdapter.Fill(dt);
                //sqlDataAdapter.FillSchema(dt, SchemaType.Mapped);
                List<danhSachCauHoi> listdanhsachcauhoi = new List<danhSachCauHoi>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listdanhsachcauhoi.Add(new danhSachCauHoi()
                    {

                        maCauHoi = dt.Rows[i][0].ToString(),
                        tenCauHoi = dt.Rows[i][1].ToString(),
                        maBai = dt.Rows[i][2].ToString(),
                        goiY = dt.Rows[i][3].ToString(),
                        A = dt.Rows[i][4].ToString(),
                        B = dt.Rows[i][5].ToString(),
                        C = dt.Rows[i][6].ToString(),
                        D = dt.Rows[i][7].ToString(),
                        dapAnDung = Convert.ToChar(dt.Rows[i][8].ToString()),

                    });
                }
                closeConnect();
                return listdanhsachcauhoi;
            }
            catch
            {
                closeConnect();
                return null;
            }
        }
    }

    
    public class nguoiDung
    {
        public string username { set; get; }
        public string password { set; get; }
        public string tenNguoiDung { set; get; }
        public string soDienThoai { set; get; }
        public string email { set; get; }
        public int soTienDaNap { set; get; }
        public int soTienHienCo { set; get; }
        public string kieuThanhVien { set; get; }
       


    }
    public class lichSuNapTien
    {
        public string orderid { set; get; }
        public int soTienNap { get; set; }
        public string username { set; get; }
        public DateTime thoiGianNapXacNhan { set; get; }
    }
public class video {
        public string maBai { get; set; }
        public string tenBai { get; set; }
        public string maChuong { get; set; }
        public string linkvideo { get; set; }
    }
    public class baiTap
    {
        public string maBai { get; set; }
        public string link { get; set; }
    }
    
    public class muaKhoaHoc
    {
        public string orderid { get; set; }
        public string maKhoaHoc { get; set; }
        public int giaKhoaHoc { get; set; }
        public string username { get; set; }
        public DateTime thoiGianMua { get; set; }
    }
    public class chuong
    {
        public string maChuong { get; set; }    
        public string tenChuong { get; set; }
        public string maKhoaHoc { get; set; }
       
    }
    public class bai
    {
        public string maBai { get; set; }
        public string tenBai { get; set; }
        public string maChuong { get; set; }
    }
    public class khoaHoc
    {
        public string maKhoaHoc { get; set; }
        public string maKhoaHocHienThi { get; set; }
        public string tenKhoaHoc { get; set; }       
        public string tenGiaoVien { get; set; }
        public string thongTinKhoaHoc { get; set; }
        public int soNguoiDaMua { get; set; }
        public string giaKhoaHoc { get; set; }
        public string giaKhoaHocHienThi { get; set; }
        public string giaKhuyenMai { get; set; }
        public string giaKhuyenMaiHienThi { get; set; }
        public int doanhThu { get; set; }
        public int doanhThu_khongkhuyenmai { get; set; }
        public string thoiGianKhuyenMai { get; set; }
        public string thoiGianKhuyenMaiHienThi { get; set; }
        public string maLop { get; set; }
        public string maLopHienThi { get; set; }
        public string imagelink { get; set; }
        public int danhGia { get; set; }
        public int soLuongDanhGia { get; set; }

    }

    public class danhGia
    {
        
        public string username { get; set; }
        public string rate { get; set; }
        public string maKhoaHoc { get; set; }
        public string tieuDe { get; set; }
        public string nhanXet { get; set; }
        public DateTime ngayDuyet { get; set; }
        public string tinhTrang { get; set; }
    }
    public class danhSachCauHoi
    {
        public string maCauHoi { get; set; }
        public string tenCauHoi { get; set; }
        
        public string maBai { get; set; }
        public string goiY { get; set; }
        public string A { get; set; }
        public string B { get; set; }
        public string C { get; set; }
        public string D { get; set; }
        public char dapAnDung { get; set; }


    }
    public class thongBao
    {
        public string loaiThongBao { get; set; }
        public char tinhTrang { get; set; }
        public string username { get; set; }
        public string tieuDe { get; set; }
        public string noiDung { get; set; }
        public DateTime ngayGui { get; set; }
        public int soTien { get; set; }
    }

    public class mainForum
    {
        public string idbaiviet { get; set; }
        public string username { get; set; }
        public string tenNguoiDung { get; set; }
        public string tinhTrang { get; set; }
        public DateTime ngayDuyet { get; set; }
        public string tieuDe { get; set; }
        public string noiDung { get; set; }
        public string tag { get; set; }
        public int luotbinhluan { get; set; }
        public int luotxem { get; set; }
      
    }
    public class traloiForum
    {
        public string idbaiviet { get; set; }
        public string username { get; set; }
        public string tenNguoiDung { get; set; }
        public string binhLuan { get; set; }
        public DateTime ngayBinhLuan { get; set; }
    }

}
