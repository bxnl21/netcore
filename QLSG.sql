use master 
go
if exists(select name from sys.databases where name = 'QuanLyShopGame')
drop database QuanLyShopGame
go
create database QuanLyShopGame
	on (name = 'QuanLyShopGame_data', filename = 'F:\QuanLyShopGame.MDF')
	log on (name = 'QuanLyShopGame_log', filename = 'F:\QuanLyShopGame.LDF')
go

use  QuanLyShopGame
go

create table LoaiSanPham (
	MaLoai  int identity(1,1) primary key,
	TenLoai nvarchar(50)
)

go

create table SanPham (
	MaSanPham int identity(1,1) primary key,
	TenSanPham nvarchar(100), 
	GiaBan decimal,
	MaLoai int foreign key references LoaiSanPham(MaLoai),
	Anh varchar(500)
)

go

create table HoaDon (
	MaHoaDon int identity(1,1) primary key,
	TenKhachHang nvarchar(100), 
	SoDienThoai varchar(11),
	DiaChi nvarchar(200),
	Email varchar(50),
	TongTien decimal
)

go 

create table ChiTietHoaDon (
	MaChiTietHoaDon int identity(1,1) primary key,
	MaHoaDon int foreign key references HoaDon(MaHoaDon),
	MaSanPham int foreign key references SanPham(MaSanPham),
	SoLuong int,
	DonGia decimal
)

go

create table NhanVien(
	MaNhanVien int identity(1,1) primary key,
	HoTenNhanVien nvarchar(100),
	Id varchar(30),
	PassWordd varchar(30)
)

go 

create table KhachHang(
	MaKhachHang int identity(1,1) primary key,
	HoTenKhachHang nvarchar(100),
	Id varchar(30),
	PassWordd varchar(30)
)

go 
insert into LoaiSanPham (TenLoai)
values
	('Steam'),
	('Uplay'),
	('Origin'),
	('GOG')
go

insert into SanPham(TenSanPham, MaLoai, GiaBan, Anh)
values
	(N'Random Code Steam',1,20000,'chua co anh - 640x522.png'),
	(N'Elden Ring',1,800000,'chua co anh - 640x510.png'),
	(N'GTA Grand Theft Auto V',1,4560000,'chua co anh - 640x450.png'),
	(N'PlayerUnknowns Battlegrounds PUBG',1,340000,'chua co anh - 480x534.png'),
	(N'Assassin’s Creed III Uplay Key',2,340000,'chua co anh - 640x392.png'),
	(N'Anno 1800 Ubisoft Connect',2,1600000,'chua co anh - 640x516.png'),
	(N'Far Cry 4 Gold Edition Uplay Key',2,1400000,'chua co anh - 640x429.png'),
	(N'Trials Fusion (Awesome Max Edition) Ubisoft Connect',2,990000,'chua co anh - 640x429.png'),
	(N'Battlefield 1',3,500000,'chua co anh - 960x960.png'),
	(N'Battlefield 3',3,470000,'chua co anh - 640x455.png'),
	(N'It Takes Two',3,750000,'chua co anh - 640x504.png'),
	(N'FIFA 22',3,940000,'chua co anh - 640x457.png'),
	(N'Starship Troopers: Terran Command',4,700000,'chua co anh - 640x531.png'),
	(N'Born Punk',4,400000,'chua co anh - 640x522.png'),
	(N'Gothic 2 Gold Edition',4,200000,'chua co anh - 640x489.png'),
	(N'DOOM II + Final DOOM',4,250000,'chua co anh - 640x400.png')
go

insert into KhachHang(HoTenKhachHang, Id, PassWordd)
values
	(N'Nguy?n V?n A', 'NVA001','NVA001')


insert into NhanVien(HoTenNhanVien, Id, PassWordd)
values
	(N'Qu? Tèo', 'queteo123','queteo123')