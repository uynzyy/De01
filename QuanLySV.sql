/*
Created		7/24/2023
Modified		7/24/2023
Project		
Model			
Company		
Author		
Version		
Database		MS SQL 2005 
*/


Drop table [SinhVien] 
go
Drop table [Lop] 
go


Create table [Lop]
(
	[MaLop] Char(3) NOT NULL,
	[TenLop] Nvarchar(30) NOT NULL,
Primary Key ([MaLop])
) 
go

Create table [SinhVien]
(
	[MaSV] Char(6) NOT NULL,
	[HotenSV] Nvarchar(40) NULL,
	[MaLop] Char(3) NOT NULL,
Primary Key ([MaSV])
) 
go


Alter table [SinhVien] add  foreign key([MaLop]) references [Lop] ([MaLop])  on update no action on delete no action 
go


Set quoted_identifier on
go


Set quoted_identifier off
go


