--9. Đưa ra thông tin 10 sản phẩm có giá bán cao nhất trong cữa hàng, theo chiều giảm dần gía bán.

--SELECT * FROM Sanpham;

SELECT TOP 10 masp, mahangsx, tensp, giaban
FROM Sanpham
ORDER BY giaban DESC
