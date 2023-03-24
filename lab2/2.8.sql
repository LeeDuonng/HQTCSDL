--8.Đưa ra Top 10 hóa đơn xuất có số lượng xuất nhiều nhất trong năm 2018, sắp xếp theo chiều giảm dần của soluongX.

--SELECT * FROM Xuat;

SELECT TOP 10 sohdx, masp, manv, ngayxuat, soluongX
FROM Xuat
ORDER BY soluongX DESC;