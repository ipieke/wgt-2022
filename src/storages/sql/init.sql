CREATE DATABASE WomenGoTech;
GO

CREATE LOGIN wgt_user WITH
    PASSWORD = N'wgt01WGT',
    DEFAULT_DATABASE = [WomenGoTech],
    DEFAULT_LANGUAGE = [us_english],
    CHECK_EXPIRATION = OFF,
    CHECK_POLICY = ON;
GO

USE WomenGoTech;
GO

CREATE USER wgt_user FOR LOGIN wgt_user WITH DEFAULT_SCHEMA=[WomenGoTech];
GO

EXEC sp_addrolemember 'db_owner', 'wgt_user';
GO

CREATE TABLE dbo.Products
(
	[product_id] [int] IDENTITY(1,1) NOT NULL,
	[name]       [nvarchar](100) NOT NULL,
	[price]      [decimal](18, 2) NOT NULL,
	[quantity]   [int] NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED ([product_id] ASC)
) ON [PRIMARY]
GO

INSERT into dbo.Products (name, price, quantity) 
VALUES ('Dress', 39.99, 1), ('Skirt', 15.99, 3), ('Jeans', 49.99, 5)
GO