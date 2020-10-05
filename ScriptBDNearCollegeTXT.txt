USE [master]
GO
/****** Object:  Database [NC]    Script Date: 05/10/2020 5:41:16 p. m. ******/
CREATE DATABASE [NC]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'NC', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\NC.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'NC_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\NC_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [NC] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [NC].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [NC] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [NC] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [NC] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [NC] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [NC] SET ARITHABORT OFF 
GO
ALTER DATABASE [NC] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [NC] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [NC] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [NC] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [NC] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [NC] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [NC] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [NC] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [NC] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [NC] SET  DISABLE_BROKER 
GO
ALTER DATABASE [NC] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [NC] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [NC] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [NC] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [NC] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [NC] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [NC] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [NC] SET RECOVERY FULL 
GO
ALTER DATABASE [NC] SET  MULTI_USER 
GO
ALTER DATABASE [NC] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [NC] SET DB_CHAINING OFF 
GO
ALTER DATABASE [NC] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [NC] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [NC] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'NC', N'ON'
GO
ALTER DATABASE [NC] SET QUERY_STORE = OFF
GO
USE [NC]
GO
/****** Object:  Table [dbo].[Tbl_Fichos]    Script Date: 05/10/2020 5:41:16 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Fichos](
	[IdFicho] [int] IDENTITY(1,1) NOT NULL,
	[IdUsuario] [int] NULL,
	[HoraFicho] [varchar](50) NULL,
	[FechaFicho] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdFicho] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Grados]    Script Date: 05/10/2020 5:41:16 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Grados](
	[IdGrado] [int] IDENTITY(1,1) NOT NULL,
	[NombreGrado] [varchar](20) NOT NULL,
	[JornadaGrado] [varchar](8) NOT NULL,
	[DisponibilidadCupoGrado] [varchar](13) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdGrado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Grados_Fichos]    Script Date: 05/10/2020 5:41:16 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Grados_Fichos](
	[IdGRadosFichos] [int] IDENTITY(1,1) NOT NULL,
	[IdFicho] [int] NULL,
	[IdGrado] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdGRadosFichos] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Modulos]    Script Date: 05/10/2020 5:41:16 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Modulos](
	[IdModulo] [int] IDENTITY(1,1) NOT NULL,
	[NombreModulo] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdModulo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Operaciones]    Script Date: 05/10/2020 5:41:16 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Operaciones](
	[IdOperacion] [int] NOT NULL,
	[NombreOperacion] [varchar](500) NOT NULL,
	[IdModulo] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdOperacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Roles]    Script Date: 05/10/2020 5:41:16 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Roles](
	[IdRol] [int] IDENTITY(1,1) NOT NULL,
	[NombreRol] [varchar](13) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Roles_Operaciones]    Script Date: 05/10/2020 5:41:16 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Roles_Operaciones](
	[IdRolOperacion] [int] NOT NULL,
	[IdRol] [int] NULL,
	[IdOperacion] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdRolOperacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Usuarios]    Script Date: 05/10/2020 5:41:16 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Usuarios](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[NombreUsuario] [varchar](15) NOT NULL,
	[ApellidoUsuario] [varchar](16) NOT NULL,
	[CorreoUsuario] [varchar](55) NOT NULL,
	[ContraseñaUsuario] [varchar](500) NULL,
	[IdRol] [int] NULL,
	[Token_Recovery] [varchar](200) NULL,
	[Sal] [varchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Tbl_Fichos] ON 
GO
INSERT [dbo].[Tbl_Fichos] ([IdFicho], [IdUsuario], [HoraFicho], [FechaFicho]) VALUES (1008, 1, N'01:28', NULL)
GO
INSERT [dbo].[Tbl_Fichos] ([IdFicho], [IdUsuario], [HoraFicho], [FechaFicho]) VALUES (1009, 6, N'12:28', NULL)
GO
SET IDENTITY_INSERT [dbo].[Tbl_Fichos] OFF
GO
SET IDENTITY_INSERT [dbo].[Tbl_Grados] ON 
GO
INSERT [dbo].[Tbl_Grados] ([IdGrado], [NombreGrado], [JornadaGrado], [DisponibilidadCupoGrado]) VALUES (1, N'Preescolar', N'Mañana', N'Disponible')
GO
INSERT [dbo].[Tbl_Grados] ([IdGrado], [NombreGrado], [JornadaGrado], [DisponibilidadCupoGrado]) VALUES (2, N'Preescolar', N'Tarde', N'Disponible')
GO
INSERT [dbo].[Tbl_Grados] ([IdGrado], [NombreGrado], [JornadaGrado], [DisponibilidadCupoGrado]) VALUES (3, N'1°', N'Tarde', N'Disponible')
GO
INSERT [dbo].[Tbl_Grados] ([IdGrado], [NombreGrado], [JornadaGrado], [DisponibilidadCupoGrado]) VALUES (4, N'2°', N'Tarde', N'No disponible')
GO
INSERT [dbo].[Tbl_Grados] ([IdGrado], [NombreGrado], [JornadaGrado], [DisponibilidadCupoGrado]) VALUES (5, N'3°', N'Tarde', N'No disponible')
GO
INSERT [dbo].[Tbl_Grados] ([IdGrado], [NombreGrado], [JornadaGrado], [DisponibilidadCupoGrado]) VALUES (6, N'4°', N'Tarde', N'Disponible')
GO
INSERT [dbo].[Tbl_Grados] ([IdGrado], [NombreGrado], [JornadaGrado], [DisponibilidadCupoGrado]) VALUES (7, N'5°', N'Tarde', N'Disponible')
GO
SET IDENTITY_INSERT [dbo].[Tbl_Grados] OFF
GO
SET IDENTITY_INSERT [dbo].[Tbl_Modulos] ON 
GO
INSERT [dbo].[Tbl_Modulos] ([IdModulo], [NombreModulo]) VALUES (1, N'Administrador')
GO
INSERT [dbo].[Tbl_Modulos] ([IdModulo], [NombreModulo]) VALUES (2, N'Institución')
GO
INSERT [dbo].[Tbl_Modulos] ([IdModulo], [NombreModulo]) VALUES (3, N'Usuario')
GO
SET IDENTITY_INSERT [dbo].[Tbl_Modulos] OFF
GO
INSERT [dbo].[Tbl_Operaciones] ([IdOperacion], [NombreOperacion], [IdModulo]) VALUES (1, N'Agregar usuario', 1)
GO
INSERT [dbo].[Tbl_Operaciones] ([IdOperacion], [NombreOperacion], [IdModulo]) VALUES (2, N'Editar usuario', 1)
GO
INSERT [dbo].[Tbl_Operaciones] ([IdOperacion], [NombreOperacion], [IdModulo]) VALUES (3, N'Detalles usuario', 1)
GO
INSERT [dbo].[Tbl_Operaciones] ([IdOperacion], [NombreOperacion], [IdModulo]) VALUES (4, N'Eliminar usuario', 1)
GO
INSERT [dbo].[Tbl_Operaciones] ([IdOperacion], [NombreOperacion], [IdModulo]) VALUES (5, N'Ver usuarios', 1)
GO
INSERT [dbo].[Tbl_Operaciones] ([IdOperacion], [NombreOperacion], [IdModulo]) VALUES (6, N'Agregar grado', 2)
GO
INSERT [dbo].[Tbl_Operaciones] ([IdOperacion], [NombreOperacion], [IdModulo]) VALUES (7, N'Editar grado', 2)
GO
INSERT [dbo].[Tbl_Operaciones] ([IdOperacion], [NombreOperacion], [IdModulo]) VALUES (8, N'Detalles grado', 2)
GO
INSERT [dbo].[Tbl_Operaciones] ([IdOperacion], [NombreOperacion], [IdModulo]) VALUES (9, N'Eliminar grado', 2)
GO
INSERT [dbo].[Tbl_Operaciones] ([IdOperacion], [NombreOperacion], [IdModulo]) VALUES (10, N'Ver grados', 2)
GO
INSERT [dbo].[Tbl_Operaciones] ([IdOperacion], [NombreOperacion], [IdModulo]) VALUES (11, N'Reservar ficho', 3)
GO
INSERT [dbo].[Tbl_Operaciones] ([IdOperacion], [NombreOperacion], [IdModulo]) VALUES (12, N'Cancelar/Eliminar ficho', 3)
GO
SET IDENTITY_INSERT [dbo].[Tbl_Roles] ON 
GO
INSERT [dbo].[Tbl_Roles] ([IdRol], [NombreRol]) VALUES (1, N'Administrador')
GO
INSERT [dbo].[Tbl_Roles] ([IdRol], [NombreRol]) VALUES (2, N'Institución')
GO
INSERT [dbo].[Tbl_Roles] ([IdRol], [NombreRol]) VALUES (3, N'Usuario')
GO
SET IDENTITY_INSERT [dbo].[Tbl_Roles] OFF
GO
INSERT [dbo].[Tbl_Roles_Operaciones] ([IdRolOperacion], [IdRol], [IdOperacion]) VALUES (1, 1, 1)
GO
INSERT [dbo].[Tbl_Roles_Operaciones] ([IdRolOperacion], [IdRol], [IdOperacion]) VALUES (2, 1, 2)
GO
INSERT [dbo].[Tbl_Roles_Operaciones] ([IdRolOperacion], [IdRol], [IdOperacion]) VALUES (3, 1, 3)
GO
INSERT [dbo].[Tbl_Roles_Operaciones] ([IdRolOperacion], [IdRol], [IdOperacion]) VALUES (4, 1, 4)
GO
INSERT [dbo].[Tbl_Roles_Operaciones] ([IdRolOperacion], [IdRol], [IdOperacion]) VALUES (5, 1, 5)
GO
INSERT [dbo].[Tbl_Roles_Operaciones] ([IdRolOperacion], [IdRol], [IdOperacion]) VALUES (6, 1, 6)
GO
INSERT [dbo].[Tbl_Roles_Operaciones] ([IdRolOperacion], [IdRol], [IdOperacion]) VALUES (7, 1, 7)
GO
INSERT [dbo].[Tbl_Roles_Operaciones] ([IdRolOperacion], [IdRol], [IdOperacion]) VALUES (8, 1, 8)
GO
INSERT [dbo].[Tbl_Roles_Operaciones] ([IdRolOperacion], [IdRol], [IdOperacion]) VALUES (9, 1, 9)
GO
INSERT [dbo].[Tbl_Roles_Operaciones] ([IdRolOperacion], [IdRol], [IdOperacion]) VALUES (10, 1, 10)
GO
INSERT [dbo].[Tbl_Roles_Operaciones] ([IdRolOperacion], [IdRol], [IdOperacion]) VALUES (11, 1, 11)
GO
INSERT [dbo].[Tbl_Roles_Operaciones] ([IdRolOperacion], [IdRol], [IdOperacion]) VALUES (12, 1, 12)
GO
INSERT [dbo].[Tbl_Roles_Operaciones] ([IdRolOperacion], [IdRol], [IdOperacion]) VALUES (13, 2, 6)
GO
INSERT [dbo].[Tbl_Roles_Operaciones] ([IdRolOperacion], [IdRol], [IdOperacion]) VALUES (14, 2, 7)
GO
INSERT [dbo].[Tbl_Roles_Operaciones] ([IdRolOperacion], [IdRol], [IdOperacion]) VALUES (15, 2, 8)
GO
INSERT [dbo].[Tbl_Roles_Operaciones] ([IdRolOperacion], [IdRol], [IdOperacion]) VALUES (16, 2, 9)
GO
INSERT [dbo].[Tbl_Roles_Operaciones] ([IdRolOperacion], [IdRol], [IdOperacion]) VALUES (17, 2, 10)
GO
INSERT [dbo].[Tbl_Roles_Operaciones] ([IdRolOperacion], [IdRol], [IdOperacion]) VALUES (18, 3, 11)
GO
INSERT [dbo].[Tbl_Roles_Operaciones] ([IdRolOperacion], [IdRol], [IdOperacion]) VALUES (19, 3, 12)
GO
INSERT [dbo].[Tbl_Roles_Operaciones] ([IdRolOperacion], [IdRol], [IdOperacion]) VALUES (20, 3, 10)
GO
SET IDENTITY_INSERT [dbo].[Tbl_Usuarios] ON 
GO
INSERT [dbo].[Tbl_Usuarios] ([IdUsuario], [NombreUsuario], [ApellidoUsuario], [CorreoUsuario], [ContraseñaUsuario], [IdRol], [Token_Recovery], [Sal]) VALUES (1, N'Camilo', N'Echeverri', N'winny0507c@gmail.com', N'123', 1, N'', N'')
GO
INSERT [dbo].[Tbl_Usuarios] ([IdUsuario], [NombreUsuario], [ApellidoUsuario], [CorreoUsuario], [ContraseñaUsuario], [IdRol], [Token_Recovery], [Sal]) VALUES (4, N'Andres', N'Torres', N'b@gmail.com', N'$2a$12$h00MkRVSG/uku9u1bKn4tuVKftUQxV1v8c0h5lm1FR9iKbunOlT/G', 3, NULL, NULL)
GO
INSERT [dbo].[Tbl_Usuarios] ([IdUsuario], [NombreUsuario], [ApellidoUsuario], [CorreoUsuario], [ContraseñaUsuario], [IdRol], [Token_Recovery], [Sal]) VALUES (6, N'Stefania', N'Martinez', N'niaamt1880@gmail.com', N'1234', 1, NULL, NULL)
GO
INSERT [dbo].[Tbl_Usuarios] ([IdUsuario], [NombreUsuario], [ApellidoUsuario], [CorreoUsuario], [ContraseñaUsuario], [IdRol], [Token_Recovery], [Sal]) VALUES (7, N'Jorge Eliecer', N'Gaitan', N'jega@gmail.com', N'$2a$12$hB77TziQoHZYdyfM/lO9UOvZNWtfzyJJXIOtciiWT23sYaLugyaue', 2, NULL, NULL)
GO
INSERT [dbo].[Tbl_Usuarios] ([IdUsuario], [NombreUsuario], [ApellidoUsuario], [CorreoUsuario], [ContraseñaUsuario], [IdRol], [Token_Recovery], [Sal]) VALUES (8, N'Stefania', N'Martinez', N'stefaniam18t@gmail.com', N'123', 3, NULL, NULL)
GO
INSERT [dbo].[Tbl_Usuarios] ([IdUsuario], [NombreUsuario], [ApellidoUsuario], [CorreoUsuario], [ContraseñaUsuario], [IdRol], [Token_Recovery], [Sal]) VALUES (9, N'Cami', N'Echeverri', N'c@gmail.com', N'$2a$12$AlhRqvcZjl0zwNdiu3mNmOsPjb0fY2FNhwgF27REZGeFVcELZP9WO', 3, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Tbl_Usuarios] OFF
GO
ALTER TABLE [dbo].[Tbl_Fichos]  WITH CHECK ADD FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Tbl_Usuarios] ([IdUsuario])
GO
ALTER TABLE [dbo].[Tbl_Grados_Fichos]  WITH CHECK ADD FOREIGN KEY([IdFicho])
REFERENCES [dbo].[Tbl_Fichos] ([IdFicho])
GO
ALTER TABLE [dbo].[Tbl_Grados_Fichos]  WITH CHECK ADD FOREIGN KEY([IdGrado])
REFERENCES [dbo].[Tbl_Grados] ([IdGrado])
GO
ALTER TABLE [dbo].[Tbl_Operaciones]  WITH CHECK ADD FOREIGN KEY([IdModulo])
REFERENCES [dbo].[Tbl_Modulos] ([IdModulo])
GO
ALTER TABLE [dbo].[Tbl_Roles_Operaciones]  WITH CHECK ADD FOREIGN KEY([IdOperacion])
REFERENCES [dbo].[Tbl_Operaciones] ([IdOperacion])
GO
ALTER TABLE [dbo].[Tbl_Roles_Operaciones]  WITH CHECK ADD FOREIGN KEY([IdRol])
REFERENCES [dbo].[Tbl_Roles] ([IdRol])
GO
ALTER TABLE [dbo].[Tbl_Usuarios]  WITH CHECK ADD FOREIGN KEY([IdRol])
REFERENCES [dbo].[Tbl_Roles] ([IdRol])
GO
USE [master]
GO
ALTER DATABASE [NC] SET  READ_WRITE 
GO
