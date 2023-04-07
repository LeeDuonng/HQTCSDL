--câu 1--
CREATE PROCEDURE sp_ThemCapNhatNhanvien
    @manv nchar(5),
    @tennv nvarchar(50),
    @gioitinh nvarchar(3),
    @diachi nvarchar(100),
    @sodt nvarchar(20),
    @email nvarchar(50),
    @phong nvarchar(50),
    @Flag bit
AS
BEGIN
    IF (@Flag = 0)
    BEGIN
        IF (@gioitinh <> N'Nam' AND @gioitinh <> N'Nữ')
        BEGIN
            RETURN 1
        END
        INSERT INTO Nhanvien (manv, tennv, gioitinh, diachi, sodt, email, phong)
        VALUES (@manv, @tennv, @gioitinh, @diachi, @sodt, @email, @phong)
        RETURN 0
    END
    ELSE
    BEGIN
        IF EXISTS(SELECT * FROM Nhanvien WHERE manv = @manv)
        BEGIN
            IF (@gioitinh <> N'Nam' AND @gioitinh <> N'Nữ')
            BEGIN
                RETURN 1
            END
            UPDATE Nhanvien
            SET tennv = @tennv,
                gioitinh = @gioitinh,
                diachi = @diachi,
                sodt = @sodt,
                email = @email,
                phong = @phong
            WHERE manv = @manv
            RETURN 0
        END
        ELSE
        BEGIN
            RETURN 2
        END
    END
END

DECLARE @errorCode int
EXEC @errorCode = sp_ThemCapNhatNhanvien 
    @manv = 'NV001',
    @tennv = 'Nguyễn Văn A',
    @gioitinh = 'Nam',
    @diachi = '123 Đường ABC',
    @sodt = '0987654321',
    @email = 'nvA@gmail.com',
    @phong = 'Phòng Kế toán',
    @Flag = 0 

IF (@errorCode = 0)
BEGIN
    PRINT 'Thành công'
END
ELSE IF (@errorCode = 1)
BEGIN
    PRINT 'Giới tính không hợp lệ'
END
ELSE IF (@errorCode = 2)
BEGIN
    PRINT 'Mã nhân viên không tồn tại'
END


--Câu 2--
CREATE PROCEDURE sp_ThemSanpham
    @masp nchar(10),
    @tenhang nvarchar(50),
    @tensp nvarchar(100),
    @soluong int,
    @mausac nvarchar(50),
    @giaban money,
    @donvitinh nvarchar(20),
    @mota nvarchar(max),
    @Flag bit,
    @errorCode int OUTPUT
AS
BEGIN
    IF NOT EXISTS(SELECT * FROM Hangsx WHERE tenhang = @tenhang) -- Kiểm tra tên hãng sản xuất
    BEGIN
        -- Trả về mã lỗi 1 nếu tên hãng không tồn tại
        SET @errorCode = 1
        RETURN
    END

    IF (@soluong < 0) -- Kiểm tra số lượng
    BEGIN
        -- Trả về mã lỗi 2 nếu số lượng không hợp lệ
        SET @errorCode = 2
        RETURN
    END
    IF (@Flag = 0) -- Nếu Flag = 0 thì thêm mới sản phẩm
    BEGIN
        INSERT INTO Sanpham (masp, mahangsx, tensp, soluong, mausac, giaban, donvitinh, mota)
        VALUES (@masp, (SELECT mahangsx FROM Hangsx WHERE tenhang = @tenhang), @tensp, @soluong, @mausac, @giaban, @donvitinh, @mota)

        -- Trả về mã lỗi 0 để thông báo thành công
        SET @errorCode = 0
        RETURN
    END
END

DECLARE @errorCode int
EXEC sp_ThemSanpham 
    @masp = 'SP001',
    @tenhang = 'Hãng A',
    @tensp = 'Sản phẩm A',
    @soluong = 100,
    @mausac = 'Đen',
    @giaban = 500000,
    @donvitinh = 'Chiếc',
    @mota = 'Mô tả sản phẩm A',
    @Flag = 0, -- 0 nếu thêm mới
    @errorCode = @errorCode OUTPUT

IF (@errorCode = 0)
BEGIN
    PRINT 'Thành công'
END
ELSE IF (@errorCode = 1)
BEGIN
    PRINT 'Tên hãng không tồn tại'
END
ELSE IF (@errorCode = 2)
BEGIN
    PRINT 'Số lượng không hợp lệ'
END

--Câu 3--
CREATE PROCEDURE sp_XoaNhanvien
    @manv nvarchar(10)
AS
BEGIN
    IF NOT EXISTS(SELECT * FROM Nhanvien WHERE manv = @manv) -- Kiểm tra mã nhân viên
    BEGIN
        -- Trả về mã lỗi 1 nếu mã nhân viên không tồn tại
        RETURN 1
    END
    
    -- Xóa dữ liệu trong bảng Nhap
    DELETE FROM Nhap WHERE manv = @manv

    -- Xóa dữ liệu trong bảng Xuat
    DELETE FROM Xuat WHERE manv = @manv

    -- Xóa dữ liệu trong bảng Nhanvien
    DELETE FROM Nhanvien WHERE manv = @manv

    -- Trả về mã lỗi 0 để thông báo thành công
    RETURN 0
END

DECLARE @errorCode int
EXEC @errorCode = sp_XoaNhanvien @manv = 'NV001'

IF (@errorCode = 0)
BEGIN
    PRINT 'Thành công'
END
ELSE IF (@errorCode = 1)
BEGIN
    PRINT 'Mã nhân viên không tồn tại'
END

--Câu 4--
CREATE PROCEDURE sp_XoaSanpham
    @masp nvarchar(10)
AS
BEGIN
    IF NOT EXISTS(SELECT * FROM Sanpham WHERE masp = @masp) -- Kiểm tra mã sản phẩm
    BEGIN
        -- Trả về mã lỗi 1 nếu mã sản phẩm không tồn tại
        RETURN 1
    END
    
    -- Xóa dữ liệu trong bảng Nhap
    DELETE FROM Nhap WHERE masp = @masp

    -- Xóa dữ liệu trong bảng Xuat
    DELETE FROM Xuat WHERE masp = @masp

    -- Xóa dữ liệu trong bảng Sanpham
    DELETE FROM Sanpham WHERE masp = @masp

    -- Trả về mã lỗi 0 để thông báo thành công
    RETURN 0
END

DECLARE @errorCode int
EXEC @errorCode = sp_XoaSanpham @masp = 'SP001'

IF (@errorCode = 0)
BEGIN
    PRINT 'Thành công'
END
ELSE IF (@errorCode = 1)
BEGIN
    PRINT 'Mã sản phẩm không tồn tại'
END


--Câu 5--
CREATE PROCEDURE sp_NhapHangsx
    @mahangsx nvarchar(10),
    @tenhang nvarchar(50),
    @diachi nvarchar(100),
    @sodt nvarchar(20),
    @email nvarchar(50)
AS
BEGIN
    IF EXISTS(SELECT * FROM Hangsx WHERE tenhang = @tenhang) -- Kiểm tra tên hãng sản xuất
    BEGIN
        -- Trả về mã lỗi 1 nếu tên hãng sản xuất đã tồn tại
        RETURN 1
    END
    
    -- Thêm dữ liệu vào bảng Hangsx
    INSERT INTO Hangsx(mahangsx, tenhang, diachi, sodt, email) 
    VALUES (@mahangsx, @tenhang, @diachi, @sodt, @email)

    -- Trả về mã lỗi 0 để thông báo thành công
    RETURN 0
END

DECLARE @errorCode int
EXEC @errorCode = sp_NhapHangsx @mahangsx = 'HSX001', @tenhang = 'Samsung', @diachi = 'Hà Nội', @sodt = '0987654321', @email = 'contact@samsung.com'

IF (@errorCode = 0)
BEGIN
    PRINT 'Thành công'
END
ELSE IF (@errorCode = 1)
BEGIN
    PRINT 'Tên hãng sản xuất đã tồn tại'
END


--Câu 6--
CREATE PROCEDURE sp_NhapHangHoa
    @sohdn nvarchar(10),
    @masp nvarchar(10),
    @manv nvarchar(10),
    @ngaynhap date,
    @soluongN int,
    @dongiaN money
AS
BEGIN
    IF NOT EXISTS(SELECT * FROM Sanpham WHERE masp = @masp) -- Kiểm tra mã sản phẩm
    BEGIN
        -- Trả về mã lỗi 1 nếu mã sản phẩm không tồn tại
        RETURN 1
    END
    
    IF NOT EXISTS(SELECT * FROM Nhanvien WHERE manv = @manv) -- Kiểm tra mã nhân viên
    BEGIN
        -- Trả về mã lỗi 2 nếu mã nhân viên không tồn tại
        RETURN 2
    END
    
    IF EXISTS(SELECT * FROM Nhap WHERE sohdn = @sohdn) -- Kiểm tra số hóa đơn nhập
    BEGIN
        -- Cập nhật dữ liệu trong bảng Nhap
        UPDATE Nhap SET masp = @masp, manv = @manv, ngaynhap = @ngaynhap, soluongN = @soluongN, dongiaN = @dongiaN WHERE sohdn = @sohdn
    END
    ELSE
    BEGIN
        -- Thêm dữ liệu vào bảng Nhap
        INSERT INTO Nhap(sohdn, masp, manv, ngaynhap, soluongN, dongiaN) 
        VALUES (@sohdn, @masp, @manv, @ngaynhap, @soluongN, @dongiaN)
    END

    -- Trả về mã lỗi 0 để thông báo thành công
    RETURN 0
END

DECLARE @errorCode int
EXEC @errorCode = sp_NhapHangHoa @sohdn = 'HDN001', @masp = 'SP001', @manv = 'NV001', @ngaynhap = '2023-04-09', @soluongN = 10, @dongiaN = 500000

IF (@errorCode = 0)
BEGIN
    PRINT 'Thành công'
END
ELSE IF (@errorCode = 1)
BEGIN
    PRINT 'Mã sản phẩm không tồn tại'
END
ELSE IF (@errorCode = 2)
BEGIN
    PRINT 'Mã nhân viên không tồn tại'
END


--Câu 7--
CREATE PROCEDURE sp_XuatHangHoa
    @sohdx nvarchar(10),
    @masp nvarchar(10),
    @manv nvarchar(10),
    @ngayxuat date,
    @soluongX int
AS
BEGIN
    IF NOT EXISTS(SELECT * FROM Sanpham WHERE masp = @masp) -- Kiểm tra mã sản phẩm
    BEGIN
        -- Trả về mã lỗi 1 nếu mã sản phẩm không tồn tại
        RETURN 1
    END
    
    IF NOT EXISTS(SELECT * FROM Nhanvien WHERE manv = @manv) -- Kiểm tra mã nhân viên
    BEGIN
        -- Trả về mã lỗi 2 nếu mã nhân viên không tồn tại
        RETURN 2
    END
    
    DECLARE @SoluongS int
    SELECT @SoluongS = soluong FROM Sanpham WHERE masp = @masp -- Lấy số lượng sản phẩm trong kho
    
    IF @soluongX <= @SoluongS -- Kiểm tra số lượng xuất
    BEGIN
        IF EXISTS(SELECT * FROM Xuat WHERE sohdx = @sohdx) -- Kiểm tra số hóa đơn xuất
        BEGIN
            -- Cập nhật dữ liệu trong bảng Xuat
            UPDATE Xuat SET masp = @masp, manv = @manv, ngayxuat = @ngayxuat, soluongX = @soluongX WHERE sohdx = @sohdx
        END
        ELSE
        BEGIN
            -- Thêm dữ liệu vào bảng Xuat
            INSERT INTO Xuat(sohdx, masp, manv, ngayxuat, soluongX) 
            VALUES (@sohdx, @masp, @manv, @ngayxuat, @soluongX)
        END

        -- Cập nhật số lượng sản phẩm còn lại trong kho
        UPDATE Sanpham SET soluong = @SoluongS - @soluongX WHERE masp = @masp

        -- Trả về mã lỗi 0 để thông báo thành công
        RETURN 0
    END
    ELSE
    BEGIN
        -- Trả về mã lỗi 3 nếu số lượng sản phẩm không đủ để xuất
        RETURN 3
    END
END

DECLARE @errorCode int
EXEC @errorCode = sp_XuatHangHoa @sohdx = 'HDX001', @masp = 'SP001', @manv = 'NV001', @ngayxuat = '2023-04-09', @soluongX = 5

IF (@errorCode = 0)
BEGIN
    PRINT 'Thành công'
END
ELSE IF (@errorCode = 1)
BEGIN
    PRINT 'Mã sản phẩm không tồn tại'
END
ELSE IF (@errorCode = 2)
BEGIN
    PRINT 'Mã nhân viên không tồn tại'
END
ELSE IF (@errorCode = 3)
BEGIN
    PRINT 'Số lượng sản phẩm không đủ để xuất'
END