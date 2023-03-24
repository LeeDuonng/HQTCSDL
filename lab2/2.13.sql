--13. Đưa ra sohdn, ngaynhap có tiền nhập phải trả cao nhất trong năm 2018

SELECT TOP 1 sohdn, ngaynhap
FROM Nhap
WHERE YEAR(ngaynhap) = 2018
ORDER BY soluongN*dongiaN;