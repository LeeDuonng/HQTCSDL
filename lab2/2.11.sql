--11. Tính tổng tiền đã nhập trong năm 2018 của hãng samsung.

--SELECT * FROM Nhap;
--SELECT * FROM Sanpham;
--SELECT * FROM Hangsx;


SELECT Hangsx.tenhang, Nhap.soluongN*Nhap.dongiaN AS N'Tổng tiền nhập'
FROM Nhap
JOIN Sanpham ON Sanpham.masp = Nhap.masp	
JOIN Hangsx ON Hangsx.mahangsx = Sanpham.mahangsx
WHERE Hangsx.tenhang = 'Samsung' AND YEAR(Nhap.ngaynhap) = 2018;