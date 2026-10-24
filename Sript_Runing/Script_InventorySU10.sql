-- Create the application database if it does not already exist.
IF DB_ID(N'InventorySU10') IS NULL
    CREATE DATABASE [InventorySU10];
GO

USE [InventorySU10];
GO

SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

-- Create the tables used by the application when they do not exist.
IF OBJECT_ID(N'dbo.orders', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.orders (
        id INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_orders PRIMARY KEY,
        [user] NVARCHAR(100) NOT NULL,
        details NVARCHAR(MAX) NOT NULL,
        orderDate DATETIME2(0) NOT NULL
            CONSTRAINT DF_orders_orderDate DEFAULT (SYSDATETIME()),
        price DECIMAL(10,2) NOT NULL,
        item INT NOT NULL CONSTRAINT DF_orders_item DEFAULT (1),
        paid NVARCHAR(30) NOT NULL CONSTRAINT DF_orders_paid DEFAULT (N'no')
    );
END;
GO

IF OBJECT_ID(N'dbo.spareparts', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.spareparts (
        id INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_spareparts PRIMARY KEY,
        model NVARCHAR(100) NOT NULL,
        part NVARCHAR(100) NOT NULL,
        type NVARCHAR(50) NOT NULL,
        price DECIMAL(10,2) NOT NULL,
        instock INT NOT NULL
    );
END;
GO

IF OBJECT_ID(N'dbo.users', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.users (
        id INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_users PRIMARY KEY,
        [first] NVARCHAR(100) NOT NULL,
        [last] NVARCHAR(100) NOT NULL,
        username NVARCHAR(100) NOT NULL CONSTRAINT UQ_users_username UNIQUE,
        phone NVARCHAR(10) NOT NULL,
        [password] INT NOT NULL,
        usertype NVARCHAR(20) NOT NULL CONSTRAINT DF_users_usertype DEFAULT (N'member')
    );
END;
GO

-- Add the checkout columns to databases created by the earlier script.
IF COL_LENGTH(N'dbo.orders', N'orderDate') IS NULL
BEGIN
    ALTER TABLE dbo.orders
        ADD orderDate DATETIME2(0) NOT NULL
            CONSTRAINT DF_orders_orderDate DEFAULT (SYSDATETIME());
END;
GO

IF COL_LENGTH(N'dbo.orders', N'item') IS NULL
BEGIN
    ALTER TABLE dbo.orders
        ADD item INT NOT NULL
            CONSTRAINT DF_orders_item DEFAULT (1);
END;
GO

-- Add the sample data only when each table is empty; existing data is preserved.
IF NOT EXISTS (SELECT 1 FROM dbo.spareparts)
BEGIN
    INSERT INTO dbo.spareparts (model, part, type, price, instock) VALUES
    (N'Model 1', N'Speedometer', N'imported', 216, 6),
    (N'Model 2', N'Head Light', N'manufactured', 511, 4),
    (N'Model 3', N'Head Light', N'imported', 339, 3),
    (N'Model 2', N'Gear Lever', N'imported', 401, 33),
    (N'Model 2', N'Seat Belt', N'imported', 88, 20),
    (N'Model 4', N'Battery', N'imported', 201, 33),
    (N'Model 1', N'Battery', N'imported', 244, 12),
    (N'Model 3', N'Muffler', N'manufactured', 830, 22),
    (N'Model 5', N'Steering Wheel', N'imported', 339, 2),
    (N'DemoModel', N'Demo Parts', N'imported', 255, 82),
    (N'Model 6', N'Dashcam', N'imported', 266, 34),
    (N'Model 2', N'Airbag Sensors', N'imported', 560, 16),
    (N'Model 1', N'Spoiler', N'imported', 190, 50),
    (N'Model 1', N'Decklid', N'imported', 499, 10),
    (N'Model 3', N'Speedometer', N'imported', 159, 28),
    (N'Model 5', N'Bonnet/hood', N'imported', 579, 15),
    (N'Model 7', N'Indicator Lights', N'imported', 149, 61);
END;
GO

IF NOT EXISTS (SELECT 1 FROM dbo.users)
BEGIN
    INSERT INTO dbo.users ([first], [last], username, phone, [password], usertype) VALUES
    (N'Roosevelt', N'Mitchell', N'roose', N'1450214580', 123456, N'member'),
    (N'Thom', N'Rodriquez', N'manager', N'7854541250', 654321, N'manager'),
    (N'Demo', N'Account', N'demoaccount', N'7000000020', 123123, N'member'),
    (N'Christine', N'Moore', N'christine', N'8521111101', 456456, N'member'),
    (N'Liam', N'Moore', N'admin', N'1470001011', 789789, N'admin'),
    (N'Estela', N'Choate', N'estela', N'1254785555', 101010, N'member'),
    (N'Robert', N'Grote', N'robert', N'8520002014', 202020, N'member'),
    (N'Jimmy', N'Lucas', N'jimmy', N'1478569800', 303030, N'member'),
    (N'Catherine', N'Lawrence', N'cath', N'8522222220', 404040, N'member'),
    (N'Joseph', N'Warren', N'warren', N'6545214500', 505050, N'member'),
    (N'Marian', N'Sacco', N'sacco', N'1478545874', 606060, N'member');
END;
GO

IF NOT EXISTS (SELECT 1 FROM dbo.orders)
BEGIN
    INSERT INTO dbo.orders ([user], details, orderDate, price, item, paid) VALUES
    (N'roose', N'Model 2 Head Light manufactured 511*2', SYSDATETIME(), 511, 2, N'yes'),
    (N'roose', N'Model 1 Battery imported 224*1', SYSDATETIME(), 224, 1, N'cancelled'),
    (N'roose', N'Model 5 Steering Wheel imported 339*1', SYSDATETIME(), 339, 1, N'cancelled'),
    (N'demoaccount', N'DemoModel Demo Parts imported 255*3', SYSDATETIME(), 765, 3, N'yes'),
    (N'christine', N'Model 2 Seat Belt imported 88*1', SYSDATETIME(), 88, 1, N'yes'),
    (N'estela', N'Model 1 Spoiler imported 190*2', SYSDATETIME(), 380, 2, N'yes'),
    (N'robert', N'Model 1 Decklid imported 499*1', SYSDATETIME(), 499, 1, N'yes'),
    (N'jimmy', N'Model 3 Muffler manufactured 830*1', SYSDATETIME(), 830, 1, N'yes'),
    (N'cath', N'Model 4 Battery imported 201*1', SYSDATETIME(), 201, 1, N'cancelled'),
    (N'warren', N'Model 2 Seat Belt imported 88*2', SYSDATETIME(), 176, 2, N'no'),
    (N'sacco', N'Model 2 Gear Lever imported 401*1', SYSDATETIME(), 401, 1, N'no'),
    (N'demoaccount', N'Model 7 Indicator Lights imported 149*2', SYSDATETIME(), 298, 2, N'yes'),
    (N'demoaccount', N'Model 6 Dashcam imported 266*1', SYSDATETIME(), 266, 1, N'yes');
END;
GO
