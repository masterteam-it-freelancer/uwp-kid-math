USE [master]
GO
/****** Object:  Database [appToanTieuHocDB]    Script Date: 11/27/2019 11:31:41 PM ******/
CREATE DATABASE [appToanTieuHocDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'appToanTieuHocDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.QUYNH\MSSQL\DATA\appToanTieuHocDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'appToanTieuHocDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.QUYNH\MSSQL\DATA\appToanTieuHocDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [appToanTieuHocDB] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [appToanTieuHocDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [appToanTieuHocDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [appToanTieuHocDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [appToanTieuHocDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [appToanTieuHocDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [appToanTieuHocDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [appToanTieuHocDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [appToanTieuHocDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [appToanTieuHocDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [appToanTieuHocDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [appToanTieuHocDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [appToanTieuHocDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [appToanTieuHocDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [appToanTieuHocDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [appToanTieuHocDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [appToanTieuHocDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [appToanTieuHocDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [appToanTieuHocDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [appToanTieuHocDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [appToanTieuHocDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [appToanTieuHocDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [appToanTieuHocDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [appToanTieuHocDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [appToanTieuHocDB] SET RECOVERY FULL 
GO
ALTER DATABASE [appToanTieuHocDB] SET  MULTI_USER 
GO
ALTER DATABASE [appToanTieuHocDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [appToanTieuHocDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [appToanTieuHocDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [appToanTieuHocDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [appToanTieuHocDB] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'appToanTieuHocDB', N'ON'
GO
ALTER DATABASE [appToanTieuHocDB] SET QUERY_STORE = OFF
GO
USE [appToanTieuHocDB]
GO
/****** Object:  Table [dbo].[bai]    Script Date: 11/27/2019 11:31:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bai](
	[maBai] [varchar](16) NOT NULL,
	[tenBai] [nvarchar](100) NULL,
	[maChuong] [varchar](16) NULL,
PRIMARY KEY CLUSTERED 
(
	[maBai] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[chuong]    Script Date: 11/27/2019 11:31:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[chuong](
	[maChuong] [varchar](16) NOT NULL,
	[tenChuong] [nvarchar](500) NULL,
	[maKhoaHoc] [varchar](16) NULL,
PRIMARY KEY CLUSTERED 
(
	[maChuong] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[danhGia]    Script Date: 11/27/2019 11:31:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[danhGia](
	[username] [varchar](30) NULL,
	[rate] [int] NULL,
	[maKhoaHoc] [varchar](16) NULL,
	[tieuDe] [nvarchar](100) NULL,
	[nhanXet] [nvarchar](2000) NULL,
	[ngayDuyet] [datetime] NULL,
	[tinhtrang] [char](1) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[danhSachBaiTap]    Script Date: 11/27/2019 11:31:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[danhSachBaiTap](
	[linkBaiTap] [varchar](100) NULL,
	[maBai] [varchar](16) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[danhSachCauHoi]    Script Date: 11/27/2019 11:31:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[danhSachCauHoi](
	[maCauHoi] [varchar](2) NULL,
	[tenCauHoi] [nvarchar](100) NULL,
	[maBai] [varchar](16) NULL,
	[goiY] [nvarchar](500) NULL,
	[A] [nvarchar](20) NULL,
	[B] [nvarchar](20) NULL,
	[C] [nvarchar](20) NULL,
	[D] [nvarchar](20) NULL,
	[dapAnDung] [char](1) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[khoaHoc]    Script Date: 11/27/2019 11:31:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[khoaHoc](
	[maKhoaHoc] [varchar](16) NOT NULL,
	[tenKhoaHoc] [nvarchar](200) NULL,
	[tenGiaoVien] [nvarchar](50) NULL,
	[thongTinKhoaHoc] [ntext] NULL,
	[soNguoiDaMua] [int] NULL,
	[giaKhoaHoc] [int] NULL,
	[giaKhuyenMai] [int] NULL,
	[thoiGianKhuyenMai] [datetime] NULL,
	[maLop] [varchar](16) NULL,
	[imagelink] [nvarchar](100) NULL,
	[danhGia] [int] NULL,
	[soLuongDanhGia] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[maKhoaHoc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[lichSuNapTien]    Script Date: 11/27/2019 11:31:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[lichSuNapTien](
	[orderidNap] [varchar](16) NOT NULL,
	[soTienNap] [int] NULL,
	[username] [varchar](30) NULL,
	[thoiGianNapXacNhan] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[orderidNap] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[lop]    Script Date: 11/27/2019 11:31:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[lop](
	[maLop] [varchar](16) NOT NULL,
	[tenLop] [nvarchar](5) NULL,
PRIMARY KEY CLUSTERED 
(
	[maLop] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mainForum]    Script Date: 11/27/2019 11:31:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mainForum](
	[idbaiviet] [nvarchar](50) NOT NULL,
	[username] [varchar](30) NULL,
	[tenNguoiDung] [nvarchar](100) NULL,
	[tinhTrang] [varchar](20) NULL,
	[ngayDuyet] [datetime] NULL,
	[tieuDe] [nvarchar](200) NULL,
	[noiDung] [nvarchar](2000) NULL,
	[tag] [nvarchar](5) NULL,
	[luotbinhluan] [int] NULL,
	[luotxem] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[idbaiviet] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[muaKhoaHoc]    Script Date: 11/27/2019 11:31:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[muaKhoaHoc](
	[orderid] [varchar](16) NOT NULL,
	[maKhoaHoc] [varchar](16) NULL,
	[username] [varchar](30) NULL,
	[thoiGianMua] [datetime] NULL,
	[giaKhoaHoc] [decimal](18, 0) NULL,
PRIMARY KEY CLUSTERED 
(
	[orderid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[nguoiDung]    Script Date: 11/27/2019 11:31:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[nguoiDung](
	[username] [varchar](30) NOT NULL,
	[passwd] [varchar](30) NULL,
	[tenNguoiDung] [nvarchar](100) NULL,
	[soDienThoai] [varchar](10) NULL,
	[email] [varchar](50) NULL,
	[soTienDaNap] [int] NULL,
	[soTienHienCo] [int] NULL,
	[kieuThanhVien] [char](1) NULL,
PRIMARY KEY CLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[thongBao]    Script Date: 11/27/2019 11:31:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[thongBao](
	[loaiThongBao] [varchar](20) NULL,
	[tinhTrang] [char](1) NULL,
	[username] [varchar](30) NULL,
	[tieuDe] [nvarchar](100) NULL,
	[noiDung] [nvarchar](500) NULL,
	[ngayGui] [datetime] NULL,
	[soTien] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[traloiForum]    Script Date: 11/27/2019 11:31:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[traloiForum](
	[idbaiviet] [nvarchar](50) NOT NULL,
	[username] [varchar](30) NULL,
	[tenNguoiDung] [nvarchar](100) NULL,
	[binhLuan] [nvarchar](500) NULL,
	[ngayBinhLuan] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[video]    Script Date: 11/27/2019 11:31:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[video](
	[maBai] [varchar](16) NULL,
	[tenBai] [nvarchar](100) NULL,
	[maChuong] [varchar](16) NULL,
	[linkVideo] [varchar](200) NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[bai]  WITH CHECK ADD FOREIGN KEY([maChuong])
REFERENCES [dbo].[chuong] ([maChuong])
GO
ALTER TABLE [dbo].[chuong]  WITH CHECK ADD FOREIGN KEY([maKhoaHoc])
REFERENCES [dbo].[khoaHoc] ([maKhoaHoc])
GO
ALTER TABLE [dbo].[danhGia]  WITH CHECK ADD FOREIGN KEY([username])
REFERENCES [dbo].[nguoiDung] ([username])
GO
ALTER TABLE [dbo].[danhSachBaiTap]  WITH CHECK ADD FOREIGN KEY([maBai])
REFERENCES [dbo].[bai] ([maBai])
GO
ALTER TABLE [dbo].[danhSachCauHoi]  WITH CHECK ADD FOREIGN KEY([maBai])
REFERENCES [dbo].[bai] ([maBai])
GO
ALTER TABLE [dbo].[khoaHoc]  WITH CHECK ADD FOREIGN KEY([maLop])
REFERENCES [dbo].[lop] ([maLop])
GO
ALTER TABLE [dbo].[lichSuNapTien]  WITH CHECK ADD FOREIGN KEY([username])
REFERENCES [dbo].[nguoiDung] ([username])
GO
ALTER TABLE [dbo].[mainForum]  WITH CHECK ADD FOREIGN KEY([username])
REFERENCES [dbo].[nguoiDung] ([username])
GO
ALTER TABLE [dbo].[muaKhoaHoc]  WITH CHECK ADD FOREIGN KEY([maKhoaHoc])
REFERENCES [dbo].[khoaHoc] ([maKhoaHoc])
GO
ALTER TABLE [dbo].[muaKhoaHoc]  WITH CHECK ADD FOREIGN KEY([username])
REFERENCES [dbo].[nguoiDung] ([username])
GO
ALTER TABLE [dbo].[thongBao]  WITH CHECK ADD FOREIGN KEY([username])
REFERENCES [dbo].[nguoiDung] ([username])
GO
ALTER TABLE [dbo].[traloiForum]  WITH CHECK ADD FOREIGN KEY([idbaiviet])
REFERENCES [dbo].[mainForum] ([idbaiviet])
GO
ALTER TABLE [dbo].[video]  WITH CHECK ADD FOREIGN KEY([maBai])
REFERENCES [dbo].[bai] ([maBai])
GO
USE [master]
GO
ALTER DATABASE [appToanTieuHocDB] SET  READ_WRITE 
GO
