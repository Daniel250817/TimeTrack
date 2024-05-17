USE [master]
GO
/****** Object:  Database [SISHRS]    Script Date: 17/5/2024 06:14:38 ******/
CREATE DATABASE [SISHRS]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SISHRS', FILENAME = N'/var/opt/mssql/data/SISHRS.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SISHRS_log', FILENAME = N'/var/opt/mssql/data/SISHRS_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [SISHRS] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SISHRS].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SISHRS] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SISHRS] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SISHRS] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SISHRS] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SISHRS] SET ARITHABORT OFF 
GO
ALTER DATABASE [SISHRS] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SISHRS] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SISHRS] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SISHRS] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SISHRS] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SISHRS] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SISHRS] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SISHRS] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SISHRS] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SISHRS] SET  ENABLE_BROKER 
GO
ALTER DATABASE [SISHRS] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SISHRS] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SISHRS] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SISHRS] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SISHRS] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SISHRS] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SISHRS] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SISHRS] SET RECOVERY FULL 
GO
ALTER DATABASE [SISHRS] SET  MULTI_USER 
GO
ALTER DATABASE [SISHRS] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SISHRS] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SISHRS] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SISHRS] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SISHRS] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SISHRS] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'SISHRS', N'ON'
GO
ALTER DATABASE [SISHRS] SET QUERY_STORE = ON
GO
ALTER DATABASE [SISHRS] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [SISHRS]
GO
/****** Object:  Table [dbo].[cargo]    Script Date: 17/5/2024 06:14:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cargo](
	[id_cargo] [int] IDENTITY(1,1) NOT NULL,
	[cargo_empleado] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_cargo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[empleado]    Script Date: 17/5/2024 06:14:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[empleado](
	[id_empleado] [int] IDENTITY(1,1) NOT NULL,
	[nombres] [varchar](50) NULL,
	[apellidos] [varchar](50) NULL,
	[fecha_nac] [date] NULL,
	[sueldo] [decimal](10, 2) NULL,
	[direccion] [varchar](100) NULL,
	[id_cargo] [int] NULL,
	[telefono] [varchar](15) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_empleado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[empleado_horario]    Script Date: 17/5/2024 06:14:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[empleado_horario](
	[id_empleado_horario] [int] IDENTITY(1,1) NOT NULL,
	[id_empleado] [int] NULL,
	[id_horario] [int] NULL,
	[fecha_inicio] [date] NULL,
	[fecha_fin] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_empleado_horario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[horario]    Script Date: 17/5/2024 06:14:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[horario](
	[id_horario] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NULL,
	[hora_entrada_lunes_viernes] [time](7) NULL,
	[hora_salida_lunes_viernes] [time](7) NULL,
	[hora_entrada_sabado] [time](7) NULL,
	[hora_salida_sabado] [time](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_horario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[nomina]    Script Date: 17/5/2024 06:14:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[nomina](
	[id_nomina] [int] IDENTITY(1,1) NOT NULL,
	[id_empleado] [int] NULL,
	[fecha] [date] NULL,
	[salario] [decimal](10, 2) NULL,
	[concepto] [varchar](150) NULL,
	[descuento] [decimal](10, 2) NULL,
	[descripcion] [text] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_nomina] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[registro_hora]    Script Date: 17/5/2024 06:14:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[registro_hora](
	[id_registro_hora] [int] IDENTITY(1,1) NOT NULL,
	[id_empleado] [int] NULL,
	[fecha] [date] NULL,
	[hora_entrada] [datetime] NULL,
	[hora_salida] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_registro_hora] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[usuarios]    Script Date: 17/5/2024 06:14:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[usuarios](
	[id_usuario] [int] IDENTITY(1,1) NOT NULL,
	[nombre_usuario] [varchar](50) NULL,
	[contrasena] [varchar](50) NULL,
	[id_empleado] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[cargo] ON 

INSERT [dbo].[cargo] ([id_cargo], [cargo_empleado]) VALUES (1, N'Administrador')
INSERT [dbo].[cargo] ([id_cargo], [cargo_empleado]) VALUES (2, N'Empleado')
SET IDENTITY_INSERT [dbo].[cargo] OFF
GO
SET IDENTITY_INSERT [dbo].[empleado] ON 

INSERT [dbo].[empleado] ([id_empleado], [nombres], [apellidos], [fecha_nac], [sueldo], [direccion], [id_cargo], [telefono]) VALUES (1, N'Julio Daniel', N'Guardado Martínez', CAST(N'2003-03-30' AS Date), CAST(365.00 AS Decimal(10, 2)), N'San Salvador', 1, N'79591767')
INSERT [dbo].[empleado] ([id_empleado], [nombres], [apellidos], [fecha_nac], [sueldo], [direccion], [id_cargo], [telefono]) VALUES (2, N'Francisco Alexander', N'Arbaiza Orellana', CAST(N'1994-05-15' AS Date), CAST(200.00 AS Decimal(10, 2)), N'Dirección 123', 2, N'12345789')
INSERT [dbo].[empleado] ([id_empleado], [nombres], [apellidos], [fecha_nac], [sueldo], [direccion], [id_cargo], [telefono]) VALUES (3, N'Wendy Cristabel', N'Hernández Urías', CAST(N'2001-08-10' AS Date), CAST(220.00 AS Decimal(10, 2)), N'Avenida Central 456', 2, N'98754321')
INSERT [dbo].[empleado] ([id_empleado], [nombres], [apellidos], [fecha_nac], [sueldo], [direccion], [id_cargo], [telefono]) VALUES (4, N'Manuel Alejandro', N'Pérez Ramírez', CAST(N'2004-01-01' AS Date), CAST(150.00 AS Decimal(10, 2)), N'Calle Principal 789', 2, N'55512456')
INSERT [dbo].[empleado] ([id_empleado], [nombres], [apellidos], [fecha_nac], [sueldo], [direccion], [id_cargo], [telefono]) VALUES (5, N'Wilson Alexander', N'Portillo Marroquín', CAST(N'2003-10-23' AS Date), CAST(365.00 AS Decimal(10, 2)), N'Carretera Sur Km 10', 1, N'33398765')
SET IDENTITY_INSERT [dbo].[empleado] OFF
GO
SET IDENTITY_INSERT [dbo].[usuarios] ON 

INSERT [dbo].[usuarios] ([id_usuario], [nombre_usuario], [contrasena], [id_empleado]) VALUES (1, N'JulioAdmin', N'12345', 1)
INSERT [dbo].[usuarios] ([id_usuario], [nombre_usuario], [contrasena], [id_empleado]) VALUES (2, N'FranciscoEmpleado', N'123456', 2)
INSERT [dbo].[usuarios] ([id_usuario], [nombre_usuario], [contrasena], [id_empleado]) VALUES (3, N'WendyEmpleado', N'1234567', 3)
INSERT [dbo].[usuarios] ([id_usuario], [nombre_usuario], [contrasena], [id_empleado]) VALUES (4, N'AlejandroEmpleado', N'12345678', 4)
INSERT [dbo].[usuarios] ([id_usuario], [nombre_usuario], [contrasena], [id_empleado]) VALUES (5, N'WilalexAdmin', N'12345', 5)
SET IDENTITY_INSERT [dbo].[usuarios] OFF
GO
ALTER TABLE [dbo].[empleado]  WITH CHECK ADD FOREIGN KEY([id_cargo])
REFERENCES [dbo].[cargo] ([id_cargo])
GO
ALTER TABLE [dbo].[empleado_horario]  WITH CHECK ADD FOREIGN KEY([id_empleado])
REFERENCES [dbo].[empleado] ([id_empleado])
GO
ALTER TABLE [dbo].[empleado_horario]  WITH CHECK ADD FOREIGN KEY([id_horario])
REFERENCES [dbo].[horario] ([id_horario])
GO
ALTER TABLE [dbo].[nomina]  WITH CHECK ADD FOREIGN KEY([id_empleado])
REFERENCES [dbo].[empleado] ([id_empleado])
GO
ALTER TABLE [dbo].[registro_hora]  WITH CHECK ADD FOREIGN KEY([id_empleado])
REFERENCES [dbo].[empleado] ([id_empleado])
GO
ALTER TABLE [dbo].[usuarios]  WITH CHECK ADD FOREIGN KEY([id_empleado])
REFERENCES [dbo].[empleado] ([id_empleado])
GO
USE [master]
GO
ALTER DATABASE [SISHRS] SET  READ_WRITE 
GO
