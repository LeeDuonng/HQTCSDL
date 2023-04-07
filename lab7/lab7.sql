--câu 1--
CREATE PROCEDURE sp_InsertHangsx
@mahangsx nchar(10),
@tenhang nvarchar(20),
@diachi nvarchar(30),
@sodt nvarchar(20),
@email nvarchar(30)
AS
BEGIN
IF NOT EXISTS(SELECT * FROM Hangsx WHERE tenhang = @tenhang)
BEGIN
INSERT INTO Hangsx(mahangsx, tenhang, diachi, sodt, email)
VALUES(@mahangsx, @tenhang, @diachi, @sodt, @email)
PRINT 'Thêm mới thành công'
END
ELSE
BEGIN
PRINT 'Tên hãng sản xuất đã tồn tại'
END
END

select * from Hangsx
EXEC sp_InsertHangsx 'H05', 'Samsung', 'Việt Nam', '0123456789', 'sssssamsung@gmail.com'

--câu 2--
CREATE PROCEDURE sp_InsertOrUpdateSanpham
    @masp nchar(10),
    @mahangsx nchar(10),
    @tensp nvarchar(20),
    @soluong INT,
    @mausac nvarchar(20),
    @giaban money,
    @donvitinh nchar(10),
    @mota nvarchar(max)
AS
BEGIN
    IF EXISTS(SELECT * FROM Sanpham WHERE masp = @masp)
    BEGIN
        UPDATE Sanpham SET
            mahangsx = @mahangsx,
            tensp = @tensp,
            soluong = @soluong,
            mausac = @mausac,
            giaban = @giaban,
            donvitinh = @donvitinh,
            mota = @mota
        WHERE
            masp = @masp
        PRINT 'Cập nhật thành công'
    END
    ELSE
    BEGIN
        INSERT INTO Sanpham(masp, mahangsx, tensp, soluong, mausac, giaban, donvitinh, mota)
        VALUES(@masp, @mahangsx, @tensp, @soluong, @mausac, @giaban, @donvitinh, @mota)
        PRINT 'Thêm mới thành công'
    END
END

select * from Sanpham
EXEC sp_InsertOrUpdateSanpham 'SP001', 'H01', 'VSmart Bee', 10, 'Đen', 5000000, 'chiếc', 'Hàng phổ thông'

--Câu 3--
CREATE PROCEDURE sp_DeleteHangsx
    @tenhang nvarchar(20)
AS
BEGIN
    IF EXISTS(SELECT * FROM Hangsx WHERE tenhang = @tenhang)
    BEGIN
        DECLARE @mahangsx nchar(10)
        SET @mahangsx = (SELECT mahangsx FROM Hangsx WHERE tenhang = @tenhang)
        
        BEGIN TRANSACTION

        DELETE FROM Sanpham WHERE mahangsx = @mahangsx
        DELETE FROM Hangsx WHERE mahangsx = @mahangsx

        IF @@ERROR = 0
        BEGIN
            COMMIT TRANSACTION
            PRINT 'Xóa hãng sản xuất và các sản phẩm liên quan thành công'
        END
        ELSE
        BEGIN
            ROLLBACK TRANSACTION
            PRINT 'Lỗi khi xóa dữ liệu'
        END
    END
    ELSE
    BEGIN
        PRINT 'Hãng sản xuất không tồn tại'
    END
END

select * from Hangsx
EXEC sp_DeleteHangsx 'Vinfone'

--Câu 4--
CREATE PROCEDURE sp_InsertOrUpdateNhanvien
    @manv nchar(5),
    @tennv nvarchar(30),
    @gioitinh nvarchar(3),
    @diachi nvarchar(50),
    @sodt nvarchar(15),
    @email nvarchar(50),
    @phong nvarchar(20),
    @flag bit
AS
BEGIN
    IF @flag = 0
    BEGIN
        IF EXISTS(SELECT * FROM Nhanvien WHERE manv = @manv)
        BEGIN
            UPDATE Nhanvien SET
                tennv = @tennv,
                gioitinh = @gioitinh,
                diachi = @diachi,
                sodt = @sodt,
                email = @email,
                phong = @phong
            WHERE manv = @manv

            IF @@ERROR = 0
            BEGIN
                PRINT 'Cập nhật thông tin nhân viên thành công'
            END
            ELSE
            BEGIN
                PRINT 'Lỗi khi cập nhật thông tin nhân viên'
            END
        END
        ELSE
        BEGIN
            PRINT 'Nhân viên không tồn tại'
        END
    END
    ELSE
    BEGIN
        INSERT INTO Nhanvien (manv, tennv, gioitinh, diachi, sodt, email, phong)
        VALUES (@manv, @tennv, @gioitinh, @diachi, @sodt, @email, @phong)

        IF @@ERROR = 0
        BEGIN
            PRINT 'Thêm mới nhân viên thành công'
        END
        ELSE
        BEGIN
            PRINT 'Lỗi khi thêm mới nhân viên'
        END
    END
END

select * from Nhanvien
EXEC sp_InsertOrUpdateNhanvien 
   @manv = 'NV04',
   @tennv = 'Trần Thanh Tùng',
   @gioitinh = 'Nam',
   @diachi = 'Hà Nội',
   @sodt = '0987654321',
   @email = 'tung@gmail.com',
   @phong = 'Kế toán',
   @flag = 1

--Câu 5--
CREATE PROCEDURE sp_InsertOrUpdateNhap
    @sohdn nvarchar(10),
    @masp nchar(5),
    @manv nchar(5),
    @ngaynhap datetime,
    @soluongN int,
    @dongiaN money
AS
BEGIN
    DECLARE @check_masp int
    DECLARE @check_manv int
    SET @check_masp = (SELECT COUNT(*) FROM Sanpham WHERE masp = @masp)
    SET @check_manv = (SELECT COUNT(*) FROM Nhanvien WHERE manv = @manv)
    IF @check_masp > 0 AND @check_manv > 0
    BEGIN
        IF EXISTS(SELECT * FROM Nhap WHERE sohdn = @sohdn)
        BEGIN
            UPDATE Nhap SET
                masp = @masp,
                manv = @manv,
                ngaynhap = @ngaynhap,
                soluongN = @soluongN,
                dongiaN = @dongiaN
            WHERE sohdn = @sohdn
        END
        ELSE
        BEGIN

            INSERT INTO Nhap (sohdn, masp, manv, ngaynhap, soluongN, dongiaN)
            VALUES (@sohdn, @masp, @manv, @ngaynhap, @soluongN, @dongiaN)
        END
    END
    ELSE
    BEGIN   
        RAISERROR('Mã sản phẩm hoặc mã nhân viên không hợp lệ!', 16, 1)
    END
END

select * from Nhap
select * from Sanpham
select * from Nhanvien
EXEC sp_InsertOrUpdateNhap @sohdn = 'N04', @masp = 'SP001', @manv = 'NV001', @ngaynhap = '2021-01-01', @soluongN = 10, @dongiaN = 10000

--câu 6--
CREATE PROCEDURE sp_InsertOrUpdateXuat
    @sohdx nvarchar(10),
    @masp nchar(5),
    @manv nchar(5),
    @ngay datetime,
    @soluongX int
AS
BEGIN
    DECLARE @check_masp int
    DECLARE @check_manv int
    DECLARE @soluong int
    SET @check_masp = (SELECT COUNT(*) FROM Sanpham WHERE masp = @masp)
    SET @check_manv = (SELECT COUNT(*) FROM Nhanvien WHERE manv = @manv)
    SET @soluong = (SELECT soluong FROM Sanpham WHERE masp = @masp)
    IF @check_masp > 0 AND @check_manv > 0 AND @soluong >= @soluongX
    BEGIN
        IF EXISTS(SELECT * FROM Xuat WHERE sohdx = @sohdx)
        BEGIN
            UPDATE Xuat SET
                masp = @masp,
                manv = @manv,
                ngayxuat = @ngay,
                soluongX = @soluongX
            WHERE sohdx = @sohdx
        END
        ELSE
        BEGIN
            INSERT INTO Xuat (sohdx, masp, manv, ngayxuat, soluongX)
            VALUES (@sohdx, @masp, @manv, @ngay, @soluongX)
        END
    END
    ELSE
    BEGIN       
        RAISERROR('Mã sản phẩm hoặc mã nhân viên không hợp lệ hoặc số lượng sản phẩm không đủ!', 16, 1)
    END
END

select * from Xuat
select * from Sanpham
select * from Nhanvien
EXEC sp_InsertOrUpdateXuat @sohdx = 'HX001', @masp = 'SP001', @manv = 'NV001', @ngay = '2021-01-01', @soluongX = 5

--câu 7--
CREATE PROCEDURE sp_DeleteNhanvien
    @manv nchar(5)
AS
BEGIN
    IF EXISTS(SELECT * FROM Nhanvien WHERE manv = @manv)
    BEGIN
        BEGIN TRANSACTION
        DELETE FROM Nhap WHERE manv = @manv
        DELETE FROM Xuat WHERE manv = @manv
        DELETE FROM Nhanvien WHERE manv = @manv
        COMMIT TRANSACTION
    END
    ELSE
    BEGIN
        RAISERROR('Mã nhân viên không tồn tại!', 16, 1)
    END
END


select * from Nhap
select * from Xuat
select * from Nhanvien
EXEC sp_DeleteNhanvien @manv = 'NV01'

--câu 8--
CREATE PROCEDURE sp_DeleteSanpham
    @masp nchar(5)
AS
BEGIN
    IF EXISTS(SELECT * FROM Sanpham WHERE masp = @masp)
    BEGIN
        BEGIN TRANSACTION
        DELETE FROM Nhap WHERE masp = @masp
        DELETE FROM Xuat WHERE masp = @masp
        DELETE FROM Sanpham WHERE masp = @masp
        COMMIT TRANSACTION
    END
    ELSE
    BEGIN
        RAISERROR('Mã sản phẩm không tồn tại!', 16, 1)
    END
END

select * from Nhap
select * from Xuat
select * from Sanpham
EXEC sp_DeleteSanpham @masp = 'SP02'
