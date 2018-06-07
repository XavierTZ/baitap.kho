use baitaplon
SET DATEFORMAT dmy
alter database baitaplon add collation;

-------------------------------------------------------------------
create table tblNhanVien(
	iMaNhanVien int primary key identity(1,1) not null,
	sHoTen nvarchar(100) not null,
	bGioiTinh bit not null,
	dNgaySinh date not null,
	tDiaChi text not null,
	tEmail text not null
)
insert into tblNhanVien (sHoTen,bGioiTinh,dNgaySinh,tDiaChi,tEmail)
values (N'Trần Mạnh Hùng', 1, N'27-03-1997', N'Thiên đường nơi tôi ở', N'Con lâu mới nói');
insert into tblNhanVien (sHoTen,bGioiTinh,dNgaySinh,tDiaChi,tEmail)
values (N'Tôn Ngộ Không', 1, N'27-03-1997', N'Hoa Quả Sơn', N'Đi mà hỏi nó ấy');

create proc pro_timtheoma
	@manv int
	as
	begin
		select * from tblNhanVien where tblNhanVien.iMaNhanVien = @manv;
	end

create procedure _pro_HienHinhNhanVien
	as
	begin
		select * from tblNhanVien;
	end

create procedure pro_ThemNhanVien

	@sHoTen nvarchar(100),
	@bGioiTinh bit,
	@dNgaySinh date,
	@tDiaChi text,
	@tEmail text
	as
	begin
		insert into tblNhanVien (sHoTen,bGioiTinh,dNgaySinh,tDiaChi,tEmail) 
		values(@sHoTen ,@bGioiTinh, @dNgaySinh, @tDiaChi, @tEmail);
	end

create procedure pro_UpdateNhanVien
	@iMaNhanVien int,
	@sHoTen nvarchar(100),
	@bGioiTinh bit,
	@dNgaySinh date,
	@tDiaChi text,
	@tEmail text
	as
	begin
		update tblNhanVien 
		set sHoTen = @sHoTen, bGioiTinh = @bGioiTinh, 
		dNgaySinh = @dNgaySinh, tDiaChi =@tDiaChi, tEmail = @tEmail
		where iMaNhanVien = @iMaNhanVien
	end

-----------------------------------------------------------------------------

create table tblKhachHang(
iMaKhachHang int primary key identity(1,1) not null,
sHoTen nvarchar(100) not null,
bGioiTinh bit not null,
dNgaySinh date not null,
tDiaChi text not null,
tEmail text not null
)

-------------------------------------------------------------------------------
create procedure pro_ThemKhachHang
	@sHoTen nvarchar(100),
	@bGioiTinh bit,
	@dNgaySinh date,
	@tDiaChi text,
	@tEmail text
	as
	begin
		insert into tblKhachHang (sHoTen, bGioiTinh, dNgaySinh, tDiaChi, tEmail)
		values(@sHoTen, @bGioiTinh, @dNgaySinh, @tDiaChi, @tEmail);
	end;

create procedure pro_UpdateKhachHang
	@iMaKhachHang int,
	@sHoTen nvarchar(100),
	@bGioiTinh bit,
	@dNgaySinh date,
	@tDiaChi text,
	@tEmail text
	as
	begin
		update tblKhachHang
		set sHoTen = @sHoTen, bGioiTinh = @bGioiTinh, dNgaySinh = @dNgaySinh, tDiaChi = @tDiaChi, tEmail = @tEmail
		where @iMaKhachHang = iMaKhachHang;
	end;
-------------------------------------------------------------------------------
create table tblSanPham(
	iMaSanPham int primary key identity(1,1),
	sTenSanPham nvarchar(100) not null,
	dNgayNhap date not null,
	iSoLuong int not null,
	sThongTinSP text not null,
	iGia int not null
)
insert into tblSanPham (sTenSanPham, dNgayNhap, iSoLuong, sThongTinSP, iGia)
values (N'Dell M4800', N'12/12/2015', 15, N'Sản phẩm chất lượng cao cấu hình khủng như khủng long bạo chúa', 17500000);
create proc pro_XoaSanPham
	@iMaSanPham int,
	@sTenSanPham nvarchar(100),
	@dNgayNhap date,
	@iSoLuong int,
	@sThongTinSP text,
	@iGia int
	as
	begin
		DELETE from tblSanPham 
		where iMaSanPham = @iMaSanPham
	end
create procedure pro_ThemSanPham
	@sTenSanPham nvarchar(100),
	@dNgayNhap date,
	@iSoLuong int,
	@sThongTinSP text,
	@iGia int
	as
	begin
		insert into tblSanPham (sTenSanPham, dNgayNhap, iSoLuong, sThongTinSP, iGia)
		values (@sTenSanPham, @dNgayNhap, @iSoLuong, @sThongTinSP,@iGia);
	end
	create proc pro_hienbangSP
	as
	begin
		select * from tblSanPham;
	end
--------------------------------------------------------------------------------
create table tblHoaDon(
	imaHoaDon int primary key identity(1,1) not null,
	iMaNhanVien int not null,
	iMaKhachHang int not null,
	dNgayLap date not null
)

create table tblChiTiestHoaDon(
	iMaChiTietHoaDon int primary key identity(1,1) not null,
	iMaSanPham int not null,
	iSoLuong int not null,
	iMaHoaDon int not null
)

create proc addHoaDon
	@iMaNhanVien int,
	@iMaKhachHang int,
	@dNgayLap date 
as
begin
	insert into tblHoaDon (iMaNhanVien, iMaKhachHang, dNgayLap)
	values (@iMaNhanVien, @iMaKhachHang, @dNgayLap)
end

create proc addChiTiet
	@iMaSanPham int,
	@iSoLuong int,
	@iMaHoaDon int
as
begin
	insert into tblChiTiestHoaDon (iMaSanPham, iSoLuong, iMaHoaDon)
	values (@iMaSanPham, @iSoLuong, @iMaHoaDon)
end

create table tblLoaiSanPham(
	iMaLoaiSanPham int primary key not null,
	iMaSanPham int not null,
	nTenLoaiSanPham nvarchar(50) not null,
	nNhaSanXuat nvarchar(50) not null,
	nNuocSanXuat nvarchar(50) not null
)

create table tblTongTienHoaDon(
	iMaHoaDon int not null,
	iTongTien int not null,
)

create table tblBangGia(
	stt int primary key identity(0,1),
	iMaSanPham int not null,
	iGia int not null,
)