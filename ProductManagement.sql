Create Database ProductManagement

Create Table Products
(
ProductId int identity(1,1) primary key,
ProductName varchar(60),
ProductDiscp varchar(60),
Quantity int,
Price bigint
)

select * from Products
