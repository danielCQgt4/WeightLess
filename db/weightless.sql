USE [weightless]
GO
/****** Object:  Trigger [tr_userdatahistory_upd]    Script Date: 10/17/2020 11:02:50 PM ******/
DROP TRIGGER [dbo].[tr_userdatahistory_upd]
GO
/****** Object:  Trigger [tr_userdatahistory_ins]    Script Date: 10/17/2020 11:02:50 PM ******/
DROP TRIGGER [dbo].[tr_userdatahistory_ins]
GO
/****** Object:  StoredProcedure [dbo].[validate_login]    Script Date: 10/17/2020 11:02:50 PM ******/
DROP PROCEDURE [dbo].[validate_login]
GO
ALTER TABLE [dbo].[UserDataHistory] DROP CONSTRAINT [FK_UserDataHistory_User]
GO
ALTER TABLE [dbo].[Publication_Activity] DROP CONSTRAINT [FK_Publication_Activity_Publication]
GO
ALTER TABLE [dbo].[Publication_Activity] DROP CONSTRAINT [FK_Publication_Activity_Activity]
GO
ALTER TABLE [dbo].[Publication] DROP CONSTRAINT [FK_Publication_User]
GO
ALTER TABLE [dbo].[Assistance] DROP CONSTRAINT [FK_Assistance_User]
GO
ALTER TABLE [dbo].[Activity_Assitance] DROP CONSTRAINT [FK_Activity_Assitance_Assistance]
GO
ALTER TABLE [dbo].[Activity_Assitance] DROP CONSTRAINT [FK_Activity_Assitance_Activity]
GO
/****** Object:  Table [dbo].[UserDataHistory]    Script Date: 10/17/2020 11:02:50 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserDataHistory]') AND type in (N'U'))
DROP TABLE [dbo].[UserDataHistory]
GO
/****** Object:  Table [dbo].[User]    Script Date: 10/17/2020 11:02:50 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND type in (N'U'))
DROP TABLE [dbo].[User]
GO
/****** Object:  Table [dbo].[Publication_Activity]    Script Date: 10/17/2020 11:02:50 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Publication_Activity]') AND type in (N'U'))
DROP TABLE [dbo].[Publication_Activity]
GO
/****** Object:  Table [dbo].[Publication]    Script Date: 10/17/2020 11:02:50 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Publication]') AND type in (N'U'))
DROP TABLE [dbo].[Publication]
GO
/****** Object:  Table [dbo].[Assistance]    Script Date: 10/17/2020 11:02:50 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Assistance]') AND type in (N'U'))
DROP TABLE [dbo].[Assistance]
GO
/****** Object:  Table [dbo].[Activity_Assitance]    Script Date: 10/17/2020 11:02:50 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Activity_Assitance]') AND type in (N'U'))
DROP TABLE [dbo].[Activity_Assitance]
GO
/****** Object:  Table [dbo].[Activity]    Script Date: 10/17/2020 11:02:50 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Activity]') AND type in (N'U'))
DROP TABLE [dbo].[Activity]
GO
/****** Object:  User [weightless]    Script Date: 10/17/2020 11:02:50 PM ******/
DROP USER [weightless]
GO
/****** Object:  User [stacy]    Script Date: 10/17/2020 11:02:50 PM ******/
DROP USER [stacy]
GO
/****** Object:  User [santiago21]    Script Date: 10/17/2020 11:02:50 PM ******/
DROP USER [santiago21]
GO
/****** Object:  User [Samuel]    Script Date: 10/17/2020 11:02:50 PM ******/
DROP USER [Samuel]
GO
/****** Object:  User [denilson]    Script Date: 10/17/2020 11:02:50 PM ******/
DROP USER [denilson]
GO
/****** Object:  User [denilson]    Script Date: 10/17/2020 11:02:50 PM ******/
CREATE USER [denilson] FOR LOGIN [denilson] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [Samuel]    Script Date: 10/17/2020 11:02:50 PM ******/
CREATE USER [Samuel] FOR LOGIN [Samuel] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [santiago21]    Script Date: 10/17/2020 11:02:50 PM ******/
CREATE USER [santiago21] FOR LOGIN [santiago21] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [stacy]    Script Date: 10/17/2020 11:02:50 PM ******/
CREATE USER [stacy] FOR LOGIN [stacy] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [weightless]    Script Date: 10/17/2020 11:02:50 PM ******/
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
/****** Object:  Table [dbo].[Activity]    Script Date: 10/17/2020 11:02:50 PM ******/
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
/****** Object:  Table [dbo].[Activity_Assitance]    Script Date: 10/17/2020 11:02:50 PM ******/
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
/****** Object:  Table [dbo].[Assistance]    Script Date: 10/17/2020 11:02:50 PM ******/
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
/****** Object:  Table [dbo].[Publication]    Script Date: 10/17/2020 11:02:50 PM ******/
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
/****** Object:  Table [dbo].[Publication_Activity]    Script Date: 10/17/2020 11:02:50 PM ******/
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
/****** Object:  Table [dbo].[User]    Script Date: 10/17/2020 11:02:50 PM ******/
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
/****** Object:  Table [dbo].[UserDataHistory]    Script Date: 10/17/2020 11:02:50 PM ******/
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
/****** Object:  StoredProcedure [dbo].[validate_login]    Script Date: 10/17/2020 11:02:50 PM ******/
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
/****** Object:  Trigger [dbo].[tr_userdatahistory_ins]    Script Date: 10/17/2020 11:02:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [dbo].[tr_userdatahistory_ins]
   ON  [dbo].[User] 
   AFTER INSERT
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	declare @id int;
	declare @i int;
	declare @w decimal;
	declare @h decimal;
	declare @t varchar(1);
	select @id = a.idUser, @w = a.weight, @h = a.height,@t = rol from inserted a;
	if @t = 'C' begin
		select @i = idUserHistory + 1 from UserDataHistory;
		if @i is null begin
			set @i = 1;
		end;
		insert into UserDataHistory values (@i,@w,@h,GETDATE(),@id);
	end;

END

GO
ALTER TABLE [dbo].[User] ENABLE TRIGGER [tr_userdatahistory_ins]
GO
/****** Object:  Trigger [dbo].[tr_userdatahistory_upd]    Script Date: 10/17/2020 11:02:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [dbo].[tr_userdatahistory_upd]
   ON  [dbo].[User] 
   AFTER Update
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	declare @id int;
	declare @i int;
	declare @w decimal;
	declare @h decimal;
	declare @t varchar(1);
	select @id = a.idUser, @w = a.weight, @h = a.height,@t = rol from inserted a;
	if @t = 'C' begin
		select @i = idUserHistory + 1 from UserDataHistory;
		if @i is null begin
			set @i = 1;
		end;
		insert into UserDataHistory values (@i,@w,@h,GETDATE(),@id);
	end;
END


GO
ALTER TABLE [dbo].[User] ENABLE TRIGGER [tr_userdatahistory_upd]
GO
