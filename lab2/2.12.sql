--12. Thống kê tổng tiền đã xuất trong ngày 2/9/2018

SELECT Xuat.soluongX*Sanpham.giaban AS N'TỔNG TIỀN'
FROM Xuat 
JOIN Sanpham ON Xuat.masp = Sanpham.masp
WHERE Xuat.ngayxuat = '2018-09-02'