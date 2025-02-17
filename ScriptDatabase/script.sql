USE [master]
GO
/****** Object:  Database [InventoryDb]    Script Date: 6/29/2024 9:54:23 AM ******/
CREATE DATABASE [InventoryDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'InventoryDb', FILENAME = N'/var/opt/mssql/data/InventoryDb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'InventoryDb_log', FILENAME = N'/var/opt/mssql/data/InventoryDb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [InventoryDb] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [InventoryDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [InventoryDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [InventoryDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [InventoryDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [InventoryDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [InventoryDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [InventoryDb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [InventoryDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [InventoryDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [InventoryDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [InventoryDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [InventoryDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [InventoryDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [InventoryDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [InventoryDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [InventoryDb] SET  DISABLE_BROKER 
GO
ALTER DATABASE [InventoryDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [InventoryDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [InventoryDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [InventoryDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [InventoryDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [InventoryDb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [InventoryDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [InventoryDb] SET RECOVERY FULL 
GO
ALTER DATABASE [InventoryDb] SET  MULTI_USER 
GO
ALTER DATABASE [InventoryDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [InventoryDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [InventoryDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [InventoryDb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [InventoryDb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [InventoryDb] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'InventoryDb', N'ON'
GO
ALTER DATABASE [InventoryDb] SET QUERY_STORE = ON
GO
ALTER DATABASE [InventoryDb] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [InventoryDb]
GO
/****** Object:  User [apud]    Script Date: 6/29/2024 9:54:23 AM ******/
CREATE USER [apud] FOR LOGIN [apud] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [apud]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 6/29/2024 9:54:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[StockLevel] [int] NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transactions]    Script Date: 6/29/2024 9:54:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transactions](
	[TransactionID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NOT NULL,
	[TransactionType] [bit] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
 CONSTRAINT [PK_Transactions] PRIMARY KEY CLUSTERED 
(
	[TransactionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Transactions] ADD  CONSTRAINT [DF_Transactions_Date]  DEFAULT (getdate()) FOR [Date]
GO
ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD  CONSTRAINT [FK_Transactions_Products] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ProductID])
GO
ALTER TABLE [dbo].[Transactions] CHECK CONSTRAINT [FK_Transactions_Products]
GO
USE [master]
GO
ALTER DATABASE [InventoryDb] SET  READ_WRITE 
GO
