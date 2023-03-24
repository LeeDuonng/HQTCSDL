--14. Đưa ra 10 mặt hàng có soluongN nhiều nhất trong năm 2019.

SELECT TOP 10 masp, SUM(soluongN) as TongSoLuongN
FROM Nhap
WHERE YEAR(ngaynhap) = 2019
GROUP BY masp
ORDER BY TongSoLuongN DESC;