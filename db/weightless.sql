USE [weightless]
GO
/****** Object:  User [weightless]    Script Date: 11/10/2020 10:40:58 p. m. ******/
CREATE USER [weightless] FOR LOGIN [weightless] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [weightless]
GO
/****** Object:  Table [dbo].[Activity]    Script Date: 11/10/2020 10:40:58 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Activity](
	[idActivity] [int] NOT NULL,
	[name] [varbinary](75) NOT NULL,
	[met] [decimal](4, 2) NOT NULL,
 CONSTRAINT [PK_Activity] PRIMARY KEY CLUSTERED 
(
	[idActivity] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Activity_Assitance]    Script Date: 11/10/2020 10:40:58 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Activity_Assitance](
	[idActivity] [int] NOT NULL,
	[idAssistance] [int] NOT NULL,
	[start] [datetime] NOT NULL,
	[end] [datetime] NULL,
	[kcal] [decimal](12, 2) NOT NULL,
 CONSTRAINT [PK_Activity_Assitance] PRIMARY KEY CLUSTERED 
(
	[idActivity] ASC,
	[idAssistance] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Assistance]    Script Date: 11/10/2020 10:40:58 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Assistance](
	[idAssistance] [int] NOT NULL,
	[date] [date] NOT NULL,
	[time] [datetime] NOT NULL,
	[idUser] [int] NOT NULL,
 CONSTRAINT [PK_Assistance] PRIMARY KEY CLUSTERED 
(
	[idAssistance] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Publication]    Script Date: 11/10/2020 10:40:58 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Publication](
	[idPublication] [int] NOT NULL,
	[date] [date] NOT NULL,
	[time] [datetime] NOT NULL,
	[description] [nvarchar](550) NOT NULL,
	[type] [char](1) NOT NULL,
	[idUser] [int] NOT NULL,
	[likes] [int] NOT NULL,
	[disLikes] [int] NOT NULL,
 CONSTRAINT [PK_Publication] PRIMARY KEY CLUSTERED 
(
	[idPublication] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Publication_Activity]    Script Date: 11/10/2020 10:40:58 p. m. ******/
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
/****** Object:  Table [dbo].[User]    Script Date: 11/10/2020 10:40:58 p. m. ******/
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
/****** Object:  Table [dbo].[UserDataHistory]    Script Date: 11/10/2020 10:40:58 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserDataHistory](
	[idUserHistory] [int] NOT NULL,
	[weight] [decimal](3, 2) NOT NULL,
	[heigth] [decimal](4, 2) NOT NULL,
	[date] [date] NOT NULL,
	[idUser] [int] NOT NULL,
 CONSTRAINT [PK_UserDataHistory] PRIMARY KEY CLUSTERED 
(
	[idUserHistory] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
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
