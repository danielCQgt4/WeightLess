USE [weightless]
GO
/****** Object:  User [denilson]    Script Date: 18/12/2020 6:09:13 p. m. ******/
CREATE USER [denilson] FOR LOGIN [denilson] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [Samuel]    Script Date: 18/12/2020 6:09:13 p. m. ******/
CREATE USER [Samuel] FOR LOGIN [Samuel] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [santiago21]    Script Date: 18/12/2020 6:09:13 p. m. ******/
CREATE USER [santiago21] FOR LOGIN [santiago21] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [stacy]    Script Date: 18/12/2020 6:09:13 p. m. ******/
CREATE USER [stacy] FOR LOGIN [stacy] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [weightless]    Script Date: 18/12/2020 6:09:13 p. m. ******/
CREATE USER [weightless] FOR LOGIN [weightless] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [denilson]
GO
ALTER ROLE [db_owner] ADD MEMBER [Samuel]
GO
ALTER ROLE [db_owner] ADD MEMBER [santiago21]
GO
ALTER ROLE [db_owner] ADD MEMBER [stacy]
GO
ALTER ROLE [db_owner] ADD MEMBER [weightless]
GO
/****** Object:  Table [dbo].[Activity]    Script Date: 18/12/2020 6:09:13 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Activity](
	[idActivity] [int] NOT NULL,
	[name] [varchar](75) NOT NULL,
	[met] [decimal](4, 2) NOT NULL,
	[link] [varchar](max) NOT NULL,
 CONSTRAINT [PK_Activity] PRIMARY KEY CLUSTERED 
(
	[idActivity] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Activity_Assitance]    Script Date: 18/12/2020 6:09:13 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Activity_Assitance](
	[idActivityAssistance] [int] IDENTITY(1,1) NOT NULL,
	[idActivity] [int] NOT NULL,
	[idAssistance] [int] NOT NULL,
	[start] [datetime] NOT NULL,
	[end] [datetime] NULL,
	[kcal] [decimal](12, 2) NOT NULL,
	[timeOcurred] [varchar](50) NOT NULL,
	[status] [bit] NOT NULL,
 CONSTRAINT [PK_Activity_Assitance] PRIMARY KEY CLUSTERED 
(
	[idActivityAssistance] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Assistance]    Script Date: 18/12/2020 6:09:13 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Assistance](
	[idAssistance] [int] IDENTITY(1,1) NOT NULL,
	[datetime] [datetime] NOT NULL,
	[idUser] [int] NOT NULL,
 CONSTRAINT [PK_Assistance] PRIMARY KEY CLUSTERED 
(
	[idAssistance] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Publication]    Script Date: 18/12/2020 6:09:13 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Publication](
	[idPublication] [int] IDENTITY(1,1) NOT NULL,
	[datetime] [datetime] NOT NULL,
	[title] [varchar](50) NOT NULL,
	[description] [nvarchar](550) NOT NULL,
	[type] [varchar](1) NOT NULL,
	[idUser] [int] NOT NULL,
	[likes] [int] NOT NULL,
	[disLikes] [int] NOT NULL,
 CONSTRAINT [PK_Publication] PRIMARY KEY CLUSTERED 
(
	[idPublication] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Publication_Activity]    Script Date: 18/12/2020 6:09:13 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Publication_Activity](
	[idPublication] [int] NOT NULL,
	[idActivity] [int] NOT NULL,
	[description] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_Publication_Activity] PRIMARY KEY CLUSTERED 
(
	[idPublication] ASC,
	[idActivity] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 18/12/2020 6:09:13 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[idUser] [int] IDENTITY(1,1) NOT NULL,
	[dni] [varchar](15) NOT NULL,
	[name] [varchar](75) NOT NULL,
	[lastName] [varchar](75) NOT NULL,
	[email] [varchar](100) NOT NULL,
	[password] [varchar](70) NOT NULL,
	[rol] [varchar](1) NOT NULL,
	[height] [decimal](5, 2) NOT NULL,
	[weight] [decimal](5, 2) NOT NULL,
	[active] [bit] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[idUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserDataHistory]    Script Date: 18/12/2020 6:09:13 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserDataHistory](
	[idUserHistory] [int] IDENTITY(1,1) NOT NULL,
	[weight] [decimal](8, 2) NOT NULL,
	[heigth] [decimal](8, 2) NOT NULL,
	[date] [datetime] NOT NULL,
	[idUser] [int] NOT NULL,
 CONSTRAINT [PK_UserDataHistory] PRIMARY KEY CLUSTERED 
(
	[idUserHistory] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Activity] ([idActivity], [name], [met], [link]) VALUES (1, N'Bicicleta (baja resistencia)', CAST(3.00 AS Decimal(4, 2)), N'/Public/images/bicy.jpeg')
INSERT [dbo].[Activity] ([idActivity], [name], [met], [link]) VALUES (2, N'Bicicleta (alta resistencia)', CAST(14.00 AS Decimal(4, 2)), N'/Public/images/bicycle.jpg')
INSERT [dbo].[Activity] ([idActivity], [name], [met], [link]) VALUES (3, N'Saltar la cuerda', CAST(7.50 AS Decimal(4, 2)), N'/Public/images/jump_the_rope.jpg')
INSERT [dbo].[Activity] ([idActivity], [name], [met], [link]) VALUES (4, N'Correr(10 KM)', CAST(16.00 AS Decimal(4, 2)), N'/Public/images/run.jpg')
INSERT [dbo].[Activity] ([idActivity], [name], [met], [link]) VALUES (5, N'Levantamiento de pesas', CAST(4.80 AS Decimal(4, 2)), N'/Public/images/weigth_lift.jpg')
INSERT [dbo].[Activity] ([idActivity], [name], [met], [link]) VALUES (6, N'Caminadora', CAST(4.00 AS Decimal(4, 2)), N'/Public/images/caminadora.jpg')
GO
SET IDENTITY_INSERT [dbo].[Activity_Assitance] ON 

INSERT [dbo].[Activity_Assitance] ([idActivityAssistance], [idActivity], [idAssistance], [start], [end], [kcal], [timeOcurred], [status]) VALUES (1078, 1, 24, CAST(N'2020-12-13T21:52:06.837' AS DateTime), CAST(N'2020-12-13T21:54:43.757' AS DateTime), CAST(4.20 AS Decimal(12, 2)), N'00:02:01', 0)
INSERT [dbo].[Activity_Assitance] ([idActivityAssistance], [idActivity], [idAssistance], [start], [end], [kcal], [timeOcurred], [status]) VALUES (1079, 5, 23, CAST(N'2020-12-13T21:52:34.120' AS DateTime), CAST(N'2020-12-13T21:53:29.037' AS DateTime), CAST(6.21 AS Decimal(12, 2)), N'00:00:47', 0)
INSERT [dbo].[Activity_Assitance] ([idActivityAssistance], [idActivity], [idAssistance], [start], [end], [kcal], [timeOcurred], [status]) VALUES (1080, 5, 23, CAST(N'2020-12-13T21:53:47.013' AS DateTime), CAST(N'2020-12-13T21:55:51.657' AS DateTime), CAST(6.21 AS Decimal(12, 2)), N'00:01:47', 0)
INSERT [dbo].[Activity_Assitance] ([idActivityAssistance], [idActivity], [idAssistance], [start], [end], [kcal], [timeOcurred], [status]) VALUES (1081, 3, 24, CAST(N'2020-12-13T21:54:50.787' AS DateTime), CAST(N'2020-12-13T21:55:28.300' AS DateTime), CAST(10.50 AS Decimal(12, 2)), N'00:00:03', 0)
INSERT [dbo].[Activity_Assitance] ([idActivityAssistance], [idActivity], [idAssistance], [start], [end], [kcal], [timeOcurred], [status]) VALUES (1082, 1, 24, CAST(N'2020-12-13T21:55:37.167' AS DateTime), CAST(N'2020-12-13T21:55:47.510' AS DateTime), CAST(4.20 AS Decimal(12, 2)), N'00:00:06', 0)
INSERT [dbo].[Activity_Assitance] ([idActivityAssistance], [idActivity], [idAssistance], [start], [end], [kcal], [timeOcurred], [status]) VALUES (1083, 3, 23, CAST(N'2020-12-13T21:56:03.840' AS DateTime), CAST(N'2020-12-13T22:03:26.320' AS DateTime), CAST(9.71 AS Decimal(12, 2)), N'00:06:33', 0)
INSERT [dbo].[Activity_Assitance] ([idActivityAssistance], [idActivity], [idAssistance], [start], [end], [kcal], [timeOcurred], [status]) VALUES (1084, 3, 24, CAST(N'2020-12-13T21:56:06.997' AS DateTime), CAST(N'2020-12-13T21:56:23.223' AS DateTime), CAST(10.50 AS Decimal(12, 2)), N'00:00:10', 0)
INSERT [dbo].[Activity_Assitance] ([idActivityAssistance], [idActivity], [idAssistance], [start], [end], [kcal], [timeOcurred], [status]) VALUES (1085, 1, 24, CAST(N'2020-12-13T21:57:11.680' AS DateTime), CAST(N'2020-12-13T21:58:14.513' AS DateTime), CAST(4.20 AS Decimal(12, 2)), N'00:00:54', 0)
INSERT [dbo].[Activity_Assitance] ([idActivityAssistance], [idActivity], [idAssistance], [start], [end], [kcal], [timeOcurred], [status]) VALUES (1086, 1, 24, CAST(N'2020-12-13T21:58:57.613' AS DateTime), CAST(N'2020-12-13T21:59:04.400' AS DateTime), CAST(4.20 AS Decimal(12, 2)), N'00:00:03', 0)
INSERT [dbo].[Activity_Assitance] ([idActivityAssistance], [idActivity], [idAssistance], [start], [end], [kcal], [timeOcurred], [status]) VALUES (1087, 5, 24, CAST(N'2020-12-13T21:59:16.780' AS DateTime), CAST(N'2020-12-13T22:00:43.053' AS DateTime), CAST(6.72 AS Decimal(12, 2)), N'00:00:05', 0)
INSERT [dbo].[Activity_Assitance] ([idActivityAssistance], [idActivity], [idAssistance], [start], [end], [kcal], [timeOcurred], [status]) VALUES (1088, 2, 26, CAST(N'2020-12-18T17:28:09.077' AS DateTime), CAST(N'2020-12-18T17:31:05.893' AS DateTime), CAST(18.13 AS Decimal(12, 2)), N'00:02:27', 0)
SET IDENTITY_INSERT [dbo].[Activity_Assitance] OFF
GO
SET IDENTITY_INSERT [dbo].[Assistance] ON 

INSERT [dbo].[Assistance] ([idAssistance], [datetime], [idUser]) VALUES (19, CAST(N'2020-11-29T21:09:43.307' AS DateTime), 97)
INSERT [dbo].[Assistance] ([idAssistance], [datetime], [idUser]) VALUES (20, CAST(N'2020-12-05T18:26:07.123' AS DateTime), 97)
INSERT [dbo].[Assistance] ([idAssistance], [datetime], [idUser]) VALUES (21, CAST(N'2020-12-05T20:15:52.613' AS DateTime), 98)
INSERT [dbo].[Assistance] ([idAssistance], [datetime], [idUser]) VALUES (22, CAST(N'2020-12-05T20:31:01.597' AS DateTime), 99)
INSERT [dbo].[Assistance] ([idAssistance], [datetime], [idUser]) VALUES (23, CAST(N'2020-12-13T19:34:18.713' AS DateTime), 99)
INSERT [dbo].[Assistance] ([idAssistance], [datetime], [idUser]) VALUES (24, CAST(N'2020-12-13T19:38:30.083' AS DateTime), 98)
INSERT [dbo].[Assistance] ([idAssistance], [datetime], [idUser]) VALUES (25, CAST(N'2020-12-13T19:42:45.760' AS DateTime), 97)
INSERT [dbo].[Assistance] ([idAssistance], [datetime], [idUser]) VALUES (26, CAST(N'2020-12-18T17:26:37.493' AS DateTime), 99)
SET IDENTITY_INSERT [dbo].[Assistance] OFF
GO
SET IDENTITY_INSERT [dbo].[Publication] ON 

INSERT [dbo].[Publication] ([idPublication], [datetime], [title], [description], [type], [idUser], [likes], [disLikes]) VALUES (1095, CAST(N'2020-11-29T20:59:22.713' AS DateTime), N'Hidratos de carbono y mis entrenamientos', N'Aunque los hidratos de carbono sean tan poco populares, son fundamentales para el rendimiento muscular. La cantidad que necesites consumir dependerá del programa de entrenamiento y de los objetivos que tengas, pero, para que te hagas una idea, cuanto más intenso sea el entrenamiento, más hidratos de carbono deberá aportar tu alimentación.', N'N', 96, 0, 0)
INSERT [dbo].[Publication] ([idPublication], [datetime], [title], [description], [type], [idUser], [likes], [disLikes]) VALUES (1096, CAST(N'2020-11-29T21:00:12.493' AS DateTime), N'Más proteína no significa más músculo', N'Uno de los mayores mitos que hay en alimentación y deporte es que, como los músculos están hechos de proteínas, es necesario consumir una gran cantidad de ellas para desarrollar masa muscular. Ya te imaginarás que esto es más falso que un unicornio y es que para desarrollar la masa muscular lo que hay que hacer es ejercitarla.', N'N', 96, 0, 0)
INSERT [dbo].[Publication] ([idPublication], [datetime], [title], [description], [type], [idUser], [likes], [disLikes]) VALUES (1102, CAST(N'2020-11-29T21:27:08.850' AS DateTime), N'Piernas y glúteos', N'Rutinas de ejercicios con bicicleta estática para principiantes', N'A', 96, 0, 0)
SET IDENTITY_INSERT [dbo].[Publication] OFF
GO
INSERT [dbo].[Publication_Activity] ([idPublication], [idActivity], [description]) VALUES (1102, 1, N'Rutina de una hora Llegar a pedalear una hora continuada es difícil pero no imposible. Por ello, para lograrlo comienza pedaleando 20 minutos seguidos al 80% de tu capacidad cardíaca máxima, agregando a cada sesión 5 minutos más.')
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([idUser], [dni], [name], [lastName], [email], [password], [rol], [height], [weight], [active]) VALUES (96, N'444444444', N'Santiago', N'Hurtado', N'4LeJyrfqE/B/riUaNZWqxErFrJLMJT+Jpl2aou+yBAE=', N'FQB7TMiWei8oJb0uAS0xMg==', N'E', CAST(10.00 AS Decimal(5, 2)), CAST(10.00 AS Decimal(5, 2)), 1)
INSERT [dbo].[User] ([idUser], [dni], [name], [lastName], [email], [password], [rol], [height], [weight], [active]) VALUES (97, N'222222226', N'Samuel', N'Monge', N'RP9UvHGs1cbXvlFGF6tSMhdUVjYGQ0UouqCV2nvPwSw=', N'FQB7TMiWei8oJb0uAS0xMg==', N'C', CAST(175.00 AS Decimal(5, 2)), CAST(79.00 AS Decimal(5, 2)), 1)
INSERT [dbo].[User] ([idUser], [dni], [name], [lastName], [email], [password], [rol], [height], [weight], [active]) VALUES (98, N'666666666', N'Samuel ', N'Monge ', N'J6kCCmulV5CzEhnc9xOSMsuuc8dz6VWahfeb67ruKCo=', N'FQB7TMiWei8oJb0uAS0xMg==', N'C', CAST(178.00 AS Decimal(5, 2)), CAST(80.00 AS Decimal(5, 2)), 1)
INSERT [dbo].[User] ([idUser], [dni], [name], [lastName], [email], [password], [rol], [height], [weight], [active]) VALUES (99, N'305230724', N'Daniel', N'Coto', N'hyG9e2OPiXjBNYbH+h+jt0juX8vmOlOPbMpu0u3ifek=', N'zQhNZ4pMaIpZel+hkj03rA==', N'C', CAST(174.00 AS Decimal(5, 2)), CAST(75.00 AS Decimal(5, 2)), 1)
INSERT [dbo].[User] ([idUser], [dni], [name], [lastName], [email], [password], [rol], [height], [weight], [active]) VALUES (107, N'111111111', N'Master', N'Master', N'tXENb2PFRGFYMx2v/2PxvDkJAXRRoGtdlk2p3GX5Qns=', N'FQB7TMiWei8oJb0uAS0xMg==', N'A', CAST(10.00 AS Decimal(5, 2)), CAST(10.00 AS Decimal(5, 2)), 1)
INSERT [dbo].[User] ([idUser], [dni], [name], [lastName], [email], [password], [rol], [height], [weight], [active]) VALUES (108, N'1122334455', N'Denilson', N'Araya', N'ss1iIhvQetHsZqbGVs3rEWltx3BfvXZEpmcH8E2ftQI=', N'FQB7TMiWei8oJb0uAS0xMg==', N'A', CAST(10.00 AS Decimal(5, 2)), CAST(10.00 AS Decimal(5, 2)), 1)
INSERT [dbo].[User] ([idUser], [dni], [name], [lastName], [email], [password], [rol], [height], [weight], [active]) VALUES (109, N'5544332211', N'Denilson', N'Entrenador', N'uW6PMelRI18zeUm0KW6h67KNf6jyoEzbxxZG3Z6nxLo=', N'FQB7TMiWei8oJb0uAS0xMg==', N'E', CAST(10.00 AS Decimal(5, 2)), CAST(10.00 AS Decimal(5, 2)), 1)
INSERT [dbo].[User] ([idUser], [dni], [name], [lastName], [email], [password], [rol], [height], [weight], [active]) VALUES (110, N'5566778899', N'Cliente', N'Test', N'KKT7Y1ysQ5N3naf655eLUGrEZj2eSafzGfqZvDhAkjY=', N'FQB7TMiWei8oJb0uAS0xMg==', N'C', CAST(175.00 AS Decimal(5, 2)), CAST(80.00 AS Decimal(5, 2)), 1)
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET IDENTITY_INSERT [dbo].[UserDataHistory] ON 

INSERT [dbo].[UserDataHistory] ([idUserHistory], [weight], [heigth], [date], [idUser]) VALUES (4, CAST(80.00 AS Decimal(8, 2)), CAST(180.00 AS Decimal(8, 2)), CAST(N'2020-11-29T19:55:57.543' AS DateTime), 97)
INSERT [dbo].[UserDataHistory] ([idUserHistory], [weight], [heigth], [date], [idUser]) VALUES (5, CAST(80.00 AS Decimal(8, 2)), CAST(170.00 AS Decimal(8, 2)), CAST(N'2020-12-05T20:13:19.887' AS DateTime), 98)
INSERT [dbo].[UserDataHistory] ([idUserHistory], [weight], [heigth], [date], [idUser]) VALUES (6, CAST(74.00 AS Decimal(8, 2)), CAST(174.00 AS Decimal(8, 2)), CAST(N'2020-12-05T20:20:58.670' AS DateTime), 99)
INSERT [dbo].[UserDataHistory] ([idUserHistory], [weight], [heigth], [date], [idUser]) VALUES (7, CAST(80.00 AS Decimal(8, 2)), CAST(176.00 AS Decimal(8, 2)), CAST(N'2020-12-05T22:22:33.373' AS DateTime), 97)
INSERT [dbo].[UserDataHistory] ([idUserHistory], [weight], [heigth], [date], [idUser]) VALUES (8, CAST(84.00 AS Decimal(8, 2)), CAST(176.00 AS Decimal(8, 2)), CAST(N'2020-12-05T22:23:28.600' AS DateTime), 97)
INSERT [dbo].[UserDataHistory] ([idUserHistory], [weight], [heigth], [date], [idUser]) VALUES (9, CAST(78.00 AS Decimal(8, 2)), CAST(176.00 AS Decimal(8, 2)), CAST(N'2020-12-10T16:05:40.703' AS DateTime), 97)
INSERT [dbo].[UserDataHistory] ([idUserHistory], [weight], [heigth], [date], [idUser]) VALUES (10, CAST(90.00 AS Decimal(8, 2)), CAST(176.00 AS Decimal(8, 2)), CAST(N'2020-12-10T16:06:01.787' AS DateTime), 97)
INSERT [dbo].[UserDataHistory] ([idUserHistory], [weight], [heigth], [date], [idUser]) VALUES (11, CAST(105.00 AS Decimal(8, 2)), CAST(176.00 AS Decimal(8, 2)), CAST(N'2020-12-10T16:06:07.543' AS DateTime), 97)
INSERT [dbo].[UserDataHistory] ([idUserHistory], [weight], [heigth], [date], [idUser]) VALUES (12, CAST(85.00 AS Decimal(8, 2)), CAST(176.00 AS Decimal(8, 2)), CAST(N'2020-12-10T16:06:11.827' AS DateTime), 97)
INSERT [dbo].[UserDataHistory] ([idUserHistory], [weight], [heigth], [date], [idUser]) VALUES (13, CAST(74.00 AS Decimal(8, 2)), CAST(176.00 AS Decimal(8, 2)), CAST(N'2020-12-12T21:28:11.823' AS DateTime), 97)
INSERT [dbo].[UserDataHistory] ([idUserHistory], [weight], [heigth], [date], [idUser]) VALUES (14, CAST(79.00 AS Decimal(8, 2)), CAST(175.00 AS Decimal(8, 2)), CAST(N'2020-12-13T19:52:50.707' AS DateTime), 97)
INSERT [dbo].[UserDataHistory] ([idUserHistory], [weight], [heigth], [date], [idUser]) VALUES (15, CAST(70.00 AS Decimal(8, 2)), CAST(178.00 AS Decimal(8, 2)), CAST(N'2020-12-13T19:54:00.447' AS DateTime), 98)
INSERT [dbo].[UserDataHistory] ([idUserHistory], [weight], [heigth], [date], [idUser]) VALUES (16, CAST(80.00 AS Decimal(8, 2)), CAST(175.00 AS Decimal(8, 2)), CAST(N'2020-12-13T20:09:25.027' AS DateTime), 110)
INSERT [dbo].[UserDataHistory] ([idUserHistory], [weight], [heigth], [date], [idUser]) VALUES (17, CAST(74.00 AS Decimal(8, 2)), CAST(178.00 AS Decimal(8, 2)), CAST(N'2020-12-13T20:33:43.957' AS DateTime), 98)
INSERT [dbo].[UserDataHistory] ([idUserHistory], [weight], [heigth], [date], [idUser]) VALUES (18, CAST(77.00 AS Decimal(8, 2)), CAST(178.00 AS Decimal(8, 2)), CAST(N'2020-12-13T20:33:48.527' AS DateTime), 98)
INSERT [dbo].[UserDataHistory] ([idUserHistory], [weight], [heigth], [date], [idUser]) VALUES (19, CAST(80.00 AS Decimal(8, 2)), CAST(178.00 AS Decimal(8, 2)), CAST(N'2020-12-13T20:33:53.897' AS DateTime), 98)
INSERT [dbo].[UserDataHistory] ([idUserHistory], [weight], [heigth], [date], [idUser]) VALUES (23, CAST(75.00 AS Decimal(8, 2)), CAST(174.00 AS Decimal(8, 2)), CAST(N'2020-12-18T17:56:02.980' AS DateTime), 99)
SET IDENTITY_INSERT [dbo].[UserDataHistory] OFF
GO
ALTER TABLE [dbo].[Activity_Assitance]  WITH CHECK ADD  CONSTRAINT [FK_Activity_Assitance_Activity] FOREIGN KEY([idActivity])
REFERENCES [dbo].[Activity] ([idActivity])
GO
ALTER TABLE [dbo].[Activity_Assitance] CHECK CONSTRAINT [FK_Activity_Assitance_Activity]
GO
ALTER TABLE [dbo].[Activity_Assitance]  WITH CHECK ADD  CONSTRAINT [FK_Activity_Assitance_Assistance] FOREIGN KEY([idAssistance])
REFERENCES [dbo].[Assistance] ([idAssistance])
GO
ALTER TABLE [dbo].[Activity_Assitance] CHECK CONSTRAINT [FK_Activity_Assitance_Assistance]
GO
ALTER TABLE [dbo].[Assistance]  WITH CHECK ADD  CONSTRAINT [FK_Assistance_User] FOREIGN KEY([idUser])
REFERENCES [dbo].[User] ([idUser])
GO
ALTER TABLE [dbo].[Assistance] CHECK CONSTRAINT [FK_Assistance_User]
GO
ALTER TABLE [dbo].[Publication]  WITH CHECK ADD  CONSTRAINT [FK_Publication_User] FOREIGN KEY([idUser])
REFERENCES [dbo].[User] ([idUser])
GO
ALTER TABLE [dbo].[Publication] CHECK CONSTRAINT [FK_Publication_User]
GO
ALTER TABLE [dbo].[Publication_Activity]  WITH CHECK ADD  CONSTRAINT [FK_Publication_Activity_Activity] FOREIGN KEY([idActivity])
REFERENCES [dbo].[Activity] ([idActivity])
GO
ALTER TABLE [dbo].[Publication_Activity] CHECK CONSTRAINT [FK_Publication_Activity_Activity]
GO
ALTER TABLE [dbo].[Publication_Activity]  WITH CHECK ADD  CONSTRAINT [FK_Publication_Activity_Publication] FOREIGN KEY([idPublication])
REFERENCES [dbo].[Publication] ([idPublication])
GO
ALTER TABLE [dbo].[Publication_Activity] CHECK CONSTRAINT [FK_Publication_Activity_Publication]
GO
ALTER TABLE [dbo].[UserDataHistory]  WITH CHECK ADD  CONSTRAINT [FK_UserDataHistory_User] FOREIGN KEY([idUser])
REFERENCES [dbo].[User] ([idUser])
GO
ALTER TABLE [dbo].[UserDataHistory] CHECK CONSTRAINT [FK_UserDataHistory_User]
GO
/****** Object:  StoredProcedure [dbo].[sp_Report_Assistance]    Script Date: 18/12/2020 6:09:14 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Report_Assistance]
@date datetime
AS
BEGIN
	select U.dni, U.name, U.lastName, A.datetime, Count(AA.idAssistance) as cantActivities from [dbo].[User] U, Assistance A, Activity_Assitance AA
	where U.idUser = A.idUser and
	A.idAssistance = AA.idAssistance and Convert(date, A.datetime) = Convert(date, @date)
	group by U.dni, U.name, U.lastName, A.datetime
END
GO
/****** Object:  StoredProcedure [dbo].[validate_login]    Script Date: 18/12/2020 6:09:14 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[validate_login]
@Email varchar(100),
@Password varchar(70)
AS
BEGIN
	select * from [dbo].[User]
	where email = @Email and password = @Password
END
GO
