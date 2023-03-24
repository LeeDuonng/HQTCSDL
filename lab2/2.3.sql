--SELECT * FROM Sanpham;
--SELECT * FROM Hangsx;

SELECT masp, tensp, tenhang, soluong, mausac, giaban, donvitinh, mota
FROM Sanpham sp
JOIN Hangsx hsx ON sp.mahangsx = hsx.mahangsx
WHERE hsx.tenhang = 'Samsung';
