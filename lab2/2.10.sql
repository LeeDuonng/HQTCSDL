--10. Đưa ra các thông tin sản phẩm có gía bán từ 100.000 đến 500.000 của hãng samsung.

--SELECT * FROM Hangsx;
--SELECT * FROM Sanpham;

SELECT Hangsx.tenhang, Sanpham.tensp, Sanpham.giaban 
FROM Sanpham
JOIN Hangsx ON Hangsx.mahangsx = Sanpham.mahangsx
WHERE Sanpham.giaban >= 100000 AND Sanpham.giaban <= 500000;