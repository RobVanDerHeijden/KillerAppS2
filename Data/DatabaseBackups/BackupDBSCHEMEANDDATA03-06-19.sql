USE [master]
GO
/****** Object:  Database [dbi413117]    Script Date: 3-6-2019 18:29:21 ******/
CREATE DATABASE [dbi413117]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'dbi413117', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\dbi413117.mdf' , SIZE = 8192KB , MAXSIZE = 1048576KB , FILEGROWTH = 2048KB )
 LOG ON 
( NAME = N'dbi413117_log', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\dbi413117_log.ldf' , SIZE = 1064KB , MAXSIZE = 262144KB , FILEGROWTH = 10%)
GO
ALTER DATABASE [dbi413117] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [dbi413117].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [dbi413117] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [dbi413117] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [dbi413117] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [dbi413117] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [dbi413117] SET ARITHABORT OFF 
GO
ALTER DATABASE [dbi413117] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [dbi413117] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [dbi413117] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [dbi413117] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [dbi413117] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [dbi413117] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [dbi413117] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [dbi413117] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [dbi413117] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [dbi413117] SET  ENABLE_BROKER 
GO
ALTER DATABASE [dbi413117] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [dbi413117] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [dbi413117] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [dbi413117] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [dbi413117] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [dbi413117] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [dbi413117] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [dbi413117] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [dbi413117] SET  MULTI_USER 
GO
ALTER DATABASE [dbi413117] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [dbi413117] SET DB_CHAINING OFF 
GO
ALTER DATABASE [dbi413117] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [dbi413117] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [dbi413117] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'dbi413117', N'ON'
GO
USE [dbi413117]
GO
/****** Object:  Table [dbo].[Achievement]    Script Date: 3-6-2019 18:29:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Achievement](
	[AchievementID] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[description] [nvarchar](50) NULL,
 CONSTRAINT [PK_Achievement] PRIMARY KEY CLUSTERED 
(
	[AchievementID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Gang]    Script Date: 3-6-2019 18:29:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gang](
	[GangID] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[tag] [nvarchar](8) NOT NULL,
	[description] [nvarchar](100) NULL,
	[money] [decimal](13, 2) NOT NULL,
	[income] [decimal](13, 2) NOT NULL,
	[dateFounded] [datetime] NOT NULL,
 CONSTRAINT [PK_Gang] PRIMARY KEY CLUSTERED 
(
	[GangID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GangMember]    Script Date: 3-6-2019 18:29:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GangMember](
	[GangMemberID] [int] IDENTITY(1,1) NOT NULL,
	[PlayerID] [int] NOT NULL,
	[GangID] [int] NOT NULL,
	[dateFrom] [datetime] NOT NULL,
	[dateTo] [datetime] NULL,
	[GangMemberRoleID] [int] NOT NULL,
 CONSTRAINT [PK_GangMember] PRIMARY KEY CLUSTERED 
(
	[GangMemberID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GangMemberAction]    Script Date: 3-6-2019 18:29:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GangMemberAction](
	[GangMemberActionID] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NULL,
 CONSTRAINT [PK_GangMemberAction] PRIMARY KEY CLUSTERED 
(
	[GangMemberActionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GangMemberHistory]    Script Date: 3-6-2019 18:29:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GangMemberHistory](
	[GangMemberHistoryID] [int] IDENTITY(1,1) NOT NULL,
	[GangMemberID] [int] NOT NULL,
	[GangMemberRoleID] [int] NOT NULL,
	[dateFrom] [datetime] NOT NULL,
	[dateTo] [datetime] NULL,
 CONSTRAINT [PK_GangMemberHistory] PRIMARY KEY CLUSTERED 
(
	[GangMemberHistoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GangMemberRole]    Script Date: 3-6-2019 18:29:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GangMemberRole](
	[GangMemberRoleID] [int] IDENTITY(1,1) NOT NULL,
	[role] [nvarchar](50) NULL,
 CONSTRAINT [PK_GangMemberRole] PRIMARY KEY CLUSTERED 
(
	[GangMemberRoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GangMemberRoleActions]    Script Date: 3-6-2019 18:29:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GangMemberRoleActions](
	[GangMemberRoleActionsID] [int] IDENTITY(1,1) NOT NULL,
	[GangMemberRoleID] [int] NOT NULL,
	[GangMemberActionID] [int] NOT NULL,
 CONSTRAINT [PK_GangMemberRoleActions] PRIMARY KEY CLUSTERED 
(
	[GangMemberRoleActionsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Hack]    Script Date: 3-6-2019 18:29:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hack](
	[HackID] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[description] [nvarchar](50) NULL,
	[baseDifficulty] [int] NOT NULL,
	[SkillCategoryID] [int] NOT NULL,
	[skillDifficulty] [int] NOT NULL,
	[energyCost] [int] NOT NULL,
	[reward] [int] NOT NULL,
	[RewardTypeID] [int] NOT NULL,
	[minimalLevel] [int] NULL,
 CONSTRAINT [PK_Hack] PRIMARY KEY CLUSTERED 
(
	[HackID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoginHistory]    Script Date: 3-6-2019 18:29:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoginHistory](
	[LoginHistoryID] [int] IDENTITY(1,1) NOT NULL,
	[PlayerID] [int] NOT NULL,
	[loginTime] [datetime] NOT NULL,
	[logoutTime] [datetime] NULL,
	[loginData] [nvarchar](200) NULL,
 CONSTRAINT [PK_LoginHistory] PRIMARY KEY CLUSTERED 
(
	[LoginHistoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Message]    Script Date: 3-6-2019 18:29:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Message](
	[MessageID] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](50) NOT NULL,
	[body] [nvarchar](250) NOT NULL,
	[PlayerIDSendBy] [int] NOT NULL,
	[PlayerIDSentTo] [int] NOT NULL,
	[isRead] [tinyint] NOT NULL,
	[dateSend] [datetime] NOT NULL,
 CONSTRAINT [PK_Message] PRIMARY KEY CLUSTERED 
(
	[MessageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Mission]    Script Date: 3-6-2019 18:29:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mission](
	[MissionID] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[description] [nvarchar](50) NULL,
	[reward] [int] NOT NULL,
	[RewardTypeID] [int] NOT NULL,
 CONSTRAINT [PK_Mission] PRIMARY KEY CLUSTERED 
(
	[MissionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Player]    Script Date: 3-6-2019 18:29:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Player](
	[PlayerID] [int] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](50) NOT NULL,
	[password] [nvarchar](50) NOT NULL,
	[RoleID] [int] NOT NULL,
	[playerLevel] [int] NOT NULL,
	[experience] [decimal](13, 2) NOT NULL,
	[skillPoints] [int] NOT NULL,
	[money] [decimal](13, 2) NOT NULL,
	[income] [decimal](13, 2) NOT NULL,
	[energy] [int] NOT NULL,
	[maxEnergy] [int] NULL,
	[isActive] [tinyint] NOT NULL,
	[realName] [nvarchar](50) NULL,
	[dateStartedPlaying] [datetime] NULL,
	[country] [nvarchar](50) NULL,
	[city] [nvarchar](50) NULL,
	[dateOfBirth] [date] NULL,
 CONSTRAINT [PK_Player] PRIMARY KEY CLUSTERED 
(
	[PlayerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PlayerAchievement]    Script Date: 3-6-2019 18:29:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlayerAchievement](
	[PlayerAchievementID] [int] IDENTITY(1,1) NOT NULL,
	[PlayerID] [int] NOT NULL,
	[AchievementID] [int] NOT NULL,
 CONSTRAINT [PK_PlayerAchievement] PRIMARY KEY CLUSTERED 
(
	[PlayerAchievementID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PlayerHack]    Script Date: 3-6-2019 18:29:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlayerHack](
	[PlayerHackID] [int] IDENTITY(1,1) NOT NULL,
	[PlayerID] [int] NOT NULL,
	[HackID] [int] NOT NULL,
	[timesSucces] [int] NOT NULL,
	[timesFailed] [int] NOT NULL,
	[totalMoneyGained] [int] NOT NULL,
 CONSTRAINT [PK_PlayerHack] PRIMARY KEY CLUSTERED 
(
	[PlayerHackID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PlayerMission]    Script Date: 3-6-2019 18:29:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlayerMission](
	[PlayerMissionID] [int] IDENTITY(1,1) NOT NULL,
	[PlayerID] [int] NOT NULL,
	[MissionID] [int] NOT NULL,
	[isCompleted] [tinyint] NOT NULL,
 CONSTRAINT [PK_PlayerMission] PRIMARY KEY CLUSTERED 
(
	[PlayerMissionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PlayerSkill]    Script Date: 3-6-2019 18:29:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlayerSkill](
	[PlayerSkillID] [int] IDENTITY(1,1) NOT NULL,
	[PlayerID] [int] NOT NULL,
	[SkillID] [int] NOT NULL,
	[skillPoints] [int] NOT NULL,
	[maxSkillPoints] [int] NOT NULL,
 CONSTRAINT [PK_PlayerSkill] PRIMARY KEY CLUSTERED 
(
	[PlayerSkillID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Relation]    Script Date: 3-6-2019 18:29:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Relation](
	[RelationID] [int] IDENTITY(1,1) NOT NULL,
	[PlayerID1] [int] NOT NULL,
	[PlayerID2] [int] NOT NULL,
	[RelationStatusID] [int] NOT NULL,
	[since] [datetime] NOT NULL,
 CONSTRAINT [PK_Relation] PRIMARY KEY CLUSTERED 
(
	[RelationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RelationStatus]    Script Date: 3-6-2019 18:29:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RelationStatus](
	[RelationStatusID] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[description] [nvarchar](50) NULL,
 CONSTRAINT [PK_RelationStatus] PRIMARY KEY CLUSTERED 
(
	[RelationStatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RewardType]    Script Date: 3-6-2019 18:29:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RewardType](
	[RewardTypeID] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_RewardType] PRIMARY KEY CLUSTERED 
(
	[RewardTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 3-6-2019 18:29:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[RoleID] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[description] [nvarchar](50) NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Skill]    Script Date: 3-6-2019 18:29:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Skill](
	[SkillID] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[description] [nvarchar](150) NULL,
	[SkillCategoryID] [int] NOT NULL,
 CONSTRAINT [PK_Skill] PRIMARY KEY CLUSTERED 
(
	[SkillID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SkillCategory]    Script Date: 3-6-2019 18:29:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SkillCategory](
	[SkillCategoryID] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[description] [nvarchar](50) NULL,
 CONSTRAINT [PK_SkillCategory] PRIMARY KEY CLUSTERED 
(
	[SkillCategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Gang] ON 

INSERT [dbo].[Gang] ([GangID], [name], [tag], [description], [money], [income], [dateFounded]) VALUES (1, N'Naga', N'NAGA', N'Een Zeer Coole Gang', CAST(12.50 AS Decimal(13, 2)), CAST(1.50 AS Decimal(13, 2)), CAST(N'2008-11-11T13:23:44.000' AS DateTime))
INSERT [dbo].[Gang] ([GangID], [name], [tag], [description], [money], [income], [dateFounded]) VALUES (2, N'Zephyr Exodia', N'Zephyr', N'Eerste Team van Zephyr', CAST(50.25 AS Decimal(13, 2)), CAST(10.75 AS Decimal(13, 2)), CAST(N'2010-10-10T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Gang] OFF
SET IDENTITY_INSERT [dbo].[GangMember] ON 

INSERT [dbo].[GangMember] ([GangMemberID], [PlayerID], [GangID], [dateFrom], [dateTo], [GangMemberRoleID]) VALUES (1, 1, 1, CAST(N'2008-11-11T11:12:01.000' AS DateTime), CAST(N'2008-12-12T11:12:01.000' AS DateTime), 1)
INSERT [dbo].[GangMember] ([GangMemberID], [PlayerID], [GangID], [dateFrom], [dateTo], [GangMemberRoleID]) VALUES (2, 2, 1, CAST(N'2009-11-11T00:00:00.000' AS DateTime), NULL, 1)
INSERT [dbo].[GangMember] ([GangMemberID], [PlayerID], [GangID], [dateFrom], [dateTo], [GangMemberRoleID]) VALUES (3, 3, 1, CAST(N'2010-11-11T00:00:00.000' AS DateTime), NULL, 1)
INSERT [dbo].[GangMember] ([GangMemberID], [PlayerID], [GangID], [dateFrom], [dateTo], [GangMemberRoleID]) VALUES (4, 4, 1, CAST(N'2011-11-11T00:00:00.000' AS DateTime), NULL, 1)
INSERT [dbo].[GangMember] ([GangMemberID], [PlayerID], [GangID], [dateFrom], [dateTo], [GangMemberRoleID]) VALUES (5, 5, 1, CAST(N'2012-11-11T00:00:00.000' AS DateTime), NULL, 1)
INSERT [dbo].[GangMember] ([GangMemberID], [PlayerID], [GangID], [dateFrom], [dateTo], [GangMemberRoleID]) VALUES (6, 6, 1, CAST(N'2013-11-11T00:00:00.000' AS DateTime), NULL, 1)
INSERT [dbo].[GangMember] ([GangMemberID], [PlayerID], [GangID], [dateFrom], [dateTo], [GangMemberRoleID]) VALUES (7, 7, 2, CAST(N'2015-01-01T00:00:00.000' AS DateTime), NULL, 1)
INSERT [dbo].[GangMember] ([GangMemberID], [PlayerID], [GangID], [dateFrom], [dateTo], [GangMemberRoleID]) VALUES (8, 8, 2, CAST(N'2015-01-01T00:00:00.000' AS DateTime), NULL, 1)
INSERT [dbo].[GangMember] ([GangMemberID], [PlayerID], [GangID], [dateFrom], [dateTo], [GangMemberRoleID]) VALUES (9, 9, 2, CAST(N'2015-01-01T00:00:00.000' AS DateTime), NULL, 1)
INSERT [dbo].[GangMember] ([GangMemberID], [PlayerID], [GangID], [dateFrom], [dateTo], [GangMemberRoleID]) VALUES (10, 10, 2, CAST(N'2015-01-01T00:00:00.000' AS DateTime), NULL, 1)
INSERT [dbo].[GangMember] ([GangMemberID], [PlayerID], [GangID], [dateFrom], [dateTo], [GangMemberRoleID]) VALUES (11, 11, 2, CAST(N'2015-01-01T00:00:00.000' AS DateTime), NULL, 1)
SET IDENTITY_INSERT [dbo].[GangMember] OFF
SET IDENTITY_INSERT [dbo].[GangMemberRole] ON 

INSERT [dbo].[GangMemberRole] ([GangMemberRoleID], [role]) VALUES (1, N'Member')
INSERT [dbo].[GangMemberRole] ([GangMemberRoleID], [role]) VALUES (2, N'Leader')
SET IDENTITY_INSERT [dbo].[GangMemberRole] OFF
SET IDENTITY_INSERT [dbo].[Hack] ON 

INSERT [dbo].[Hack] ([HackID], [name], [description], [baseDifficulty], [SkillCategoryID], [skillDifficulty], [energyCost], [reward], [RewardTypeID], [minimalLevel]) VALUES (1, N'SQL-Injection', N'Inject Malicious Code with the help of SQL', 5, 1, 5, 5, 10, 1, 1)
INSERT [dbo].[Hack] ([HackID], [name], [description], [baseDifficulty], [SkillCategoryID], [skillDifficulty], [energyCost], [reward], [RewardTypeID], [minimalLevel]) VALUES (2, N'DDOS', N'Overwelm a server with requests', 10, 2, 5, 10, 25, 1, 2)
INSERT [dbo].[Hack] ([HackID], [name], [description], [baseDifficulty], [SkillCategoryID], [skillDifficulty], [energyCost], [reward], [RewardTypeID], [minimalLevel]) VALUES (3, N'Sneak Peak', N'Look over shoulder of Expert Hacker', 0, 3, 0, 3, 3, 3, 0)
SET IDENTITY_INSERT [dbo].[Hack] OFF
SET IDENTITY_INSERT [dbo].[Player] ON 

INSERT [dbo].[Player] ([PlayerID], [username], [password], [RoleID], [playerLevel], [experience], [skillPoints], [money], [income], [energy], [maxEnergy], [isActive], [realName], [dateStartedPlaying], [country], [city], [dateOfBirth]) VALUES (1, N'FriendlyRob', N'1234', 1, 2, CAST(100.00 AS Decimal(13, 2)), 1, CAST(1.00 AS Decimal(13, 2)), CAST(1.00 AS Decimal(13, 2)), 100, 100, 1, N'Rob Heijden', CAST(N'2008-11-09T15:45:21.000' AS DateTime), N'NL', N'Oss', CAST(N'1993-03-03' AS Date))
INSERT [dbo].[Player] ([PlayerID], [username], [password], [RoleID], [playerLevel], [experience], [skillPoints], [money], [income], [energy], [maxEnergy], [isActive], [realName], [dateStartedPlaying], [country], [city], [dateOfBirth]) VALUES (2, N'ION8', N'1234', 1, 0, CAST(20.00 AS Decimal(13, 2)), 2, CAST(1.00 AS Decimal(13, 2)), CAST(1.00 AS Decimal(13, 2)), 100, 100, 1, N'Rens', CAST(N'2015-06-06T00:00:00.000' AS DateTime), N'NL', N'Eindhoven', CAST(N'1970-01-01' AS Date))
INSERT [dbo].[Player] ([PlayerID], [username], [password], [RoleID], [playerLevel], [experience], [skillPoints], [money], [income], [energy], [maxEnergy], [isActive], [realName], [dateStartedPlaying], [country], [city], [dateOfBirth]) VALUES (3, N'Blitherjaguar45', N'1234', 1, 1, CAST(45.00 AS Decimal(13, 2)), 5, CAST(1.00 AS Decimal(13, 2)), CAST(1.00 AS Decimal(13, 2)), 100, 100, 1, N'Wessel', CAST(N'2014-06-06T00:00:00.000' AS DateTime), N'NL', N'Eindhoven', CAST(N'1970-02-01' AS Date))
INSERT [dbo].[Player] ([PlayerID], [username], [password], [RoleID], [playerLevel], [experience], [skillPoints], [money], [income], [energy], [maxEnergy], [isActive], [realName], [dateStartedPlaying], [country], [city], [dateOfBirth]) VALUES (4, N'All Skill', N'1234', 1, 2, CAST(140.00 AS Decimal(13, 2)), 10, CAST(1.00 AS Decimal(13, 2)), CAST(1.00 AS Decimal(13, 2)), 100, 100, 1, N'Christopher', CAST(N'2013-06-06T00:00:00.000' AS DateTime), N'MEX', N'Mexico City', CAST(N'1970-03-01' AS Date))
INSERT [dbo].[Player] ([PlayerID], [username], [password], [RoleID], [playerLevel], [experience], [skillPoints], [money], [income], [energy], [maxEnergy], [isActive], [realName], [dateStartedPlaying], [country], [city], [dateOfBirth]) VALUES (5, N'TAC Blinder', N'1234', 1, 1, CAST(80.00 AS Decimal(13, 2)), 8, CAST(1.00 AS Decimal(13, 2)), CAST(1.00 AS Decimal(13, 2)), 100, 100, 1, N'Hoang', CAST(N'2012-06-06T00:00:00.000' AS DateTime), N'Vietnam', N'Vietnam City', CAST(N'1970-04-01' AS Date))
INSERT [dbo].[Player] ([PlayerID], [username], [password], [RoleID], [playerLevel], [experience], [skillPoints], [money], [income], [energy], [maxEnergy], [isActive], [realName], [dateStartedPlaying], [country], [city], [dateOfBirth]) VALUES (6, N'KeizersPingu', N'1234', 1, 2, CAST(185.00 AS Decimal(13, 2)), 17, CAST(1.00 AS Decimal(13, 2)), CAST(1.00 AS Decimal(13, 2)), 100, 100, 1, N'Sander', CAST(N'2011-06-06T00:00:00.000' AS DateTime), N'NL', N'Eindhoven', CAST(N'1970-05-01' AS Date))
INSERT [dbo].[Player] ([PlayerID], [username], [password], [RoleID], [playerLevel], [experience], [skillPoints], [money], [income], [energy], [maxEnergy], [isActive], [realName], [dateStartedPlaying], [country], [city], [dateOfBirth]) VALUES (7, N'Dodging Dummy', N'1234', 1, 2, CAST(150.00 AS Decimal(13, 2)), 10, CAST(1.00 AS Decimal(13, 2)), CAST(1.00 AS Decimal(13, 2)), 100, 100, 1, N'Rik', CAST(N'2015-05-05T00:00:00.000' AS DateTime), N'NL', N'Eindhoven', CAST(N'1970-06-01' AS Date))
INSERT [dbo].[Player] ([PlayerID], [username], [password], [RoleID], [playerLevel], [experience], [skillPoints], [money], [income], [energy], [maxEnergy], [isActive], [realName], [dateStartedPlaying], [country], [city], [dateOfBirth]) VALUES (8, N'Daan Stylez', N'1234', 1, 6, CAST(925.00 AS Decimal(13, 2)), 99, CAST(1.00 AS Decimal(13, 2)), CAST(1.00 AS Decimal(13, 2)), 100, 100, 1, N'Daan', CAST(N'2014-05-05T00:00:00.000' AS DateTime), N'NL', N'Eindhoven', CAST(N'1970-07-01' AS Date))
INSERT [dbo].[Player] ([PlayerID], [username], [password], [RoleID], [playerLevel], [experience], [skillPoints], [money], [income], [energy], [maxEnergy], [isActive], [realName], [dateStartedPlaying], [country], [city], [dateOfBirth]) VALUES (9, N'Riron', N'1234', 1, 0, CAST(10.00 AS Decimal(13, 2)), 3, CAST(1.00 AS Decimal(13, 2)), CAST(1.00 AS Decimal(13, 2)), 100, 100, 1, N'Rian', CAST(N'2013-05-05T00:00:00.000' AS DateTime), N'NL', N'Eindhoven', CAST(N'1970-08-01' AS Date))
INSERT [dbo].[Player] ([PlayerID], [username], [password], [RoleID], [playerLevel], [experience], [skillPoints], [money], [income], [energy], [maxEnergy], [isActive], [realName], [dateStartedPlaying], [country], [city], [dateOfBirth]) VALUES (10, N'Nomis', N'1234', 1, 3, CAST(325.00 AS Decimal(13, 2)), 25, CAST(1.00 AS Decimal(13, 2)), CAST(1.00 AS Decimal(13, 2)), 100, 100, 1, N'Simon', NULL, NULL, NULL, CAST(N'1970-09-01' AS Date))
INSERT [dbo].[Player] ([PlayerID], [username], [password], [RoleID], [playerLevel], [experience], [skillPoints], [money], [income], [energy], [maxEnergy], [isActive], [realName], [dateStartedPlaying], [country], [city], [dateOfBirth]) VALUES (11, N'Vaips', N'1234', 1, 3, CAST(295.00 AS Decimal(13, 2)), 22, CAST(1.00 AS Decimal(13, 2)), CAST(1.00 AS Decimal(13, 2)), 100, 100, 1, NULL, NULL, NULL, NULL, CAST(N'1970-10-01' AS Date))
INSERT [dbo].[Player] ([PlayerID], [username], [password], [RoleID], [playerLevel], [experience], [skillPoints], [money], [income], [energy], [maxEnergy], [isActive], [realName], [dateStartedPlaying], [country], [city], [dateOfBirth]) VALUES (101, N'Faker', N'1234', 1, 19, CAST(9999.00 AS Decimal(13, 2)), 999, CAST(1.00 AS Decimal(13, 2)), CAST(1.00 AS Decimal(13, 2)), 100, 100, 1, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Player] ([PlayerID], [username], [password], [RoleID], [playerLevel], [experience], [skillPoints], [money], [income], [energy], [maxEnergy], [isActive], [realName], [dateStartedPlaying], [country], [city], [dateOfBirth]) VALUES (102, N'Looking4Team', N'1234', 1, 0, CAST(1.00 AS Decimal(13, 2)), 1, CAST(1.00 AS Decimal(13, 2)), CAST(1.00 AS Decimal(13, 2)), 100, 100, 1, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Player] ([PlayerID], [username], [password], [RoleID], [playerLevel], [experience], [skillPoints], [money], [income], [energy], [maxEnergy], [isActive], [realName], [dateStartedPlaying], [country], [city], [dateOfBirth]) VALUES (103, N'TheOddOne', N'1234', 1, 0, CAST(2.00 AS Decimal(13, 2)), 1, CAST(1.00 AS Decimal(13, 2)), CAST(1.00 AS Decimal(13, 2)), 100, 100, 1, N'TheOdd', NULL, NULL, NULL, NULL)
INSERT [dbo].[Player] ([PlayerID], [username], [password], [RoleID], [playerLevel], [experience], [skillPoints], [money], [income], [energy], [maxEnergy], [isActive], [realName], [dateStartedPlaying], [country], [city], [dateOfBirth]) VALUES (105, N'player', N'1234', 1, 4, CAST(540.00 AS Decimal(13, 2)), 1, CAST(1.00 AS Decimal(13, 2)), CAST(1.00 AS Decimal(13, 2)), 60, 100, 1, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Player] ([PlayerID], [username], [password], [RoleID], [playerLevel], [experience], [skillPoints], [money], [income], [energy], [maxEnergy], [isActive], [realName], [dateStartedPlaying], [country], [city], [dateOfBirth]) VALUES (106, N'admin', N'1234', 2, 0, CAST(1.00 AS Decimal(13, 2)), 1, CAST(1.00 AS Decimal(13, 2)), CAST(1.00 AS Decimal(13, 2)), 65, 100, 1, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Player] ([PlayerID], [username], [password], [RoleID], [playerLevel], [experience], [skillPoints], [money], [income], [energy], [maxEnergy], [isActive], [realName], [dateStartedPlaying], [country], [city], [dateOfBirth]) VALUES (108, N'testtrigger2', N'1234', 1, 0, CAST(100.00 AS Decimal(13, 2)), 1, CAST(1.00 AS Decimal(13, 2)), CAST(1.00 AS Decimal(13, 2)), 100, 100, 1, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Player] OFF
SET IDENTITY_INSERT [dbo].[PlayerSkill] ON 

INSERT [dbo].[PlayerSkill] ([PlayerSkillID], [PlayerID], [SkillID], [skillPoints], [maxSkillPoints]) VALUES (1, 105, 3, 4, 10)
INSERT [dbo].[PlayerSkill] ([PlayerSkillID], [PlayerID], [SkillID], [skillPoints], [maxSkillPoints]) VALUES (2, 105, 4, 5, 10)
INSERT [dbo].[PlayerSkill] ([PlayerSkillID], [PlayerID], [SkillID], [skillPoints], [maxSkillPoints]) VALUES (3, 105, 5, 6, 10)
INSERT [dbo].[PlayerSkill] ([PlayerSkillID], [PlayerID], [SkillID], [skillPoints], [maxSkillPoints]) VALUES (4, 105, 6, 0, 10)
INSERT [dbo].[PlayerSkill] ([PlayerSkillID], [PlayerID], [SkillID], [skillPoints], [maxSkillPoints]) VALUES (5, 105, 7, 1, 10)
INSERT [dbo].[PlayerSkill] ([PlayerSkillID], [PlayerID], [SkillID], [skillPoints], [maxSkillPoints]) VALUES (6, 105, 8, 2, 10)
INSERT [dbo].[PlayerSkill] ([PlayerSkillID], [PlayerID], [SkillID], [skillPoints], [maxSkillPoints]) VALUES (7, 1, 3, 1, 10)
INSERT [dbo].[PlayerSkill] ([PlayerSkillID], [PlayerID], [SkillID], [skillPoints], [maxSkillPoints]) VALUES (8, 1, 4, 1, 10)
INSERT [dbo].[PlayerSkill] ([PlayerSkillID], [PlayerID], [SkillID], [skillPoints], [maxSkillPoints]) VALUES (9, 1, 5, 1, 10)
INSERT [dbo].[PlayerSkill] ([PlayerSkillID], [PlayerID], [SkillID], [skillPoints], [maxSkillPoints]) VALUES (10, 1, 6, 1, 10)
INSERT [dbo].[PlayerSkill] ([PlayerSkillID], [PlayerID], [SkillID], [skillPoints], [maxSkillPoints]) VALUES (11, 1, 7, 1, 10)
INSERT [dbo].[PlayerSkill] ([PlayerSkillID], [PlayerID], [SkillID], [skillPoints], [maxSkillPoints]) VALUES (12, 1, 8, 1, 10)
INSERT [dbo].[PlayerSkill] ([PlayerSkillID], [PlayerID], [SkillID], [skillPoints], [maxSkillPoints]) VALUES (13, 108, 3, 0, 10)
INSERT [dbo].[PlayerSkill] ([PlayerSkillID], [PlayerID], [SkillID], [skillPoints], [maxSkillPoints]) VALUES (14, 108, 4, 0, 10)
INSERT [dbo].[PlayerSkill] ([PlayerSkillID], [PlayerID], [SkillID], [skillPoints], [maxSkillPoints]) VALUES (15, 108, 5, 0, 10)
INSERT [dbo].[PlayerSkill] ([PlayerSkillID], [PlayerID], [SkillID], [skillPoints], [maxSkillPoints]) VALUES (16, 108, 6, 0, 10)
INSERT [dbo].[PlayerSkill] ([PlayerSkillID], [PlayerID], [SkillID], [skillPoints], [maxSkillPoints]) VALUES (17, 108, 7, 0, 10)
INSERT [dbo].[PlayerSkill] ([PlayerSkillID], [PlayerID], [SkillID], [skillPoints], [maxSkillPoints]) VALUES (18, 108, 8, 0, 10)
SET IDENTITY_INSERT [dbo].[PlayerSkill] OFF
SET IDENTITY_INSERT [dbo].[RewardType] ON 

INSERT [dbo].[RewardType] ([RewardTypeID], [name]) VALUES (1, N'Money')
INSERT [dbo].[RewardType] ([RewardTypeID], [name]) VALUES (2, N'Skill Points')
INSERT [dbo].[RewardType] ([RewardTypeID], [name]) VALUES (3, N'Experience')
SET IDENTITY_INSERT [dbo].[RewardType] OFF
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([RoleID], [name], [description]) VALUES (1, N'Player', N'A Player of the game')
INSERT [dbo].[Role] ([RoleID], [name], [description]) VALUES (2, N'Admin', N'The Admin of the Game')
SET IDENTITY_INSERT [dbo].[Role] OFF
SET IDENTITY_INSERT [dbo].[Skill] ON 

INSERT [dbo].[Skill] ([SkillID], [name], [description], [SkillCategoryID]) VALUES (3, N'Draft DB-Diagram', N'Set up a Database diagram to better understand its structure', 1)
INSERT [dbo].[Skill] ([SkillID], [name], [description], [SkillCategoryID]) VALUES (4, N'SQL Query building', N'Learn how to write different SQL queries', 1)
INSERT [dbo].[Skill] ([SkillID], [name], [description], [SkillCategoryID]) VALUES (5, N'PHP Server management', N'Learn how PHP sites work on servers', 2)
INSERT [dbo].[Skill] ([SkillID], [name], [description], [SkillCategoryID]) VALUES (6, N'ASP Server management', N'Learn how ASP sites work on servers', 2)
INSERT [dbo].[Skill] ([SkillID], [name], [description], [SkillCategoryID]) VALUES (7, N'Programmer Lingo Knowledge', N'Knowledge about different words in the programming world to learn faster', 3)
INSERT [dbo].[Skill] ([SkillID], [name], [description], [SkillCategoryID]) VALUES (8, N'Sleep schedule', N'Studying is easier when you are awake', 3)
SET IDENTITY_INSERT [dbo].[Skill] OFF
SET IDENTITY_INSERT [dbo].[SkillCategory] ON 

INSERT [dbo].[SkillCategory] ([SkillCategoryID], [name], [description]) VALUES (1, N'Database', N'Know how databases work')
INSERT [dbo].[SkillCategory] ([SkillCategoryID], [name], [description]) VALUES (2, N'Servers', N'Server description')
INSERT [dbo].[SkillCategory] ([SkillCategoryID], [name], [description]) VALUES (3, N'Study', N'Learn and grow')
SET IDENTITY_INSERT [dbo].[SkillCategory] OFF
ALTER TABLE [dbo].[GangMember]  WITH CHECK ADD  CONSTRAINT [FK_GangMember_Gang] FOREIGN KEY([GangID])
REFERENCES [dbo].[Gang] ([GangID])
GO
ALTER TABLE [dbo].[GangMember] CHECK CONSTRAINT [FK_GangMember_Gang]
GO
ALTER TABLE [dbo].[GangMember]  WITH CHECK ADD  CONSTRAINT [FK_GangMember_GangMemberRole] FOREIGN KEY([GangMemberRoleID])
REFERENCES [dbo].[GangMemberRole] ([GangMemberRoleID])
GO
ALTER TABLE [dbo].[GangMember] CHECK CONSTRAINT [FK_GangMember_GangMemberRole]
GO
ALTER TABLE [dbo].[GangMember]  WITH CHECK ADD  CONSTRAINT [FK_GangMember_Player] FOREIGN KEY([PlayerID])
REFERENCES [dbo].[Player] ([PlayerID])
GO
ALTER TABLE [dbo].[GangMember] CHECK CONSTRAINT [FK_GangMember_Player]
GO
ALTER TABLE [dbo].[GangMemberHistory]  WITH CHECK ADD  CONSTRAINT [FK_GangMemberHistory_GangMember] FOREIGN KEY([GangMemberID])
REFERENCES [dbo].[GangMember] ([GangMemberID])
GO
ALTER TABLE [dbo].[GangMemberHistory] CHECK CONSTRAINT [FK_GangMemberHistory_GangMember]
GO
ALTER TABLE [dbo].[GangMemberHistory]  WITH CHECK ADD  CONSTRAINT [FK_GangMemberHistory_GangMemberRole] FOREIGN KEY([GangMemberRoleID])
REFERENCES [dbo].[GangMemberRole] ([GangMemberRoleID])
GO
ALTER TABLE [dbo].[GangMemberHistory] CHECK CONSTRAINT [FK_GangMemberHistory_GangMemberRole]
GO
ALTER TABLE [dbo].[GangMemberRoleActions]  WITH CHECK ADD  CONSTRAINT [FK_GangMemberRoleActions_GangMemberAction] FOREIGN KEY([GangMemberActionID])
REFERENCES [dbo].[GangMemberAction] ([GangMemberActionID])
GO
ALTER TABLE [dbo].[GangMemberRoleActions] CHECK CONSTRAINT [FK_GangMemberRoleActions_GangMemberAction]
GO
ALTER TABLE [dbo].[GangMemberRoleActions]  WITH CHECK ADD  CONSTRAINT [FK_GangMemberRoleActions_GangMemberRole] FOREIGN KEY([GangMemberRoleID])
REFERENCES [dbo].[GangMemberRole] ([GangMemberRoleID])
GO
ALTER TABLE [dbo].[GangMemberRoleActions] CHECK CONSTRAINT [FK_GangMemberRoleActions_GangMemberRole]
GO
ALTER TABLE [dbo].[Hack]  WITH CHECK ADD  CONSTRAINT [FK_Hack_RewardType] FOREIGN KEY([RewardTypeID])
REFERENCES [dbo].[RewardType] ([RewardTypeID])
GO
ALTER TABLE [dbo].[Hack] CHECK CONSTRAINT [FK_Hack_RewardType]
GO
ALTER TABLE [dbo].[Hack]  WITH CHECK ADD  CONSTRAINT [FK_Hack_SkillCategory] FOREIGN KEY([SkillCategoryID])
REFERENCES [dbo].[SkillCategory] ([SkillCategoryID])
GO
ALTER TABLE [dbo].[Hack] CHECK CONSTRAINT [FK_Hack_SkillCategory]
GO
ALTER TABLE [dbo].[LoginHistory]  WITH CHECK ADD  CONSTRAINT [FK_LoginHistory_Player] FOREIGN KEY([PlayerID])
REFERENCES [dbo].[Player] ([PlayerID])
GO
ALTER TABLE [dbo].[LoginHistory] CHECK CONSTRAINT [FK_LoginHistory_Player]
GO
ALTER TABLE [dbo].[Message]  WITH CHECK ADD  CONSTRAINT [FK_Message_Player] FOREIGN KEY([PlayerIDSendBy])
REFERENCES [dbo].[Player] ([PlayerID])
GO
ALTER TABLE [dbo].[Message] CHECK CONSTRAINT [FK_Message_Player]
GO
ALTER TABLE [dbo].[Message]  WITH CHECK ADD  CONSTRAINT [FK_Message_Player1] FOREIGN KEY([PlayerIDSentTo])
REFERENCES [dbo].[Player] ([PlayerID])
GO
ALTER TABLE [dbo].[Message] CHECK CONSTRAINT [FK_Message_Player1]
GO
ALTER TABLE [dbo].[Mission]  WITH CHECK ADD  CONSTRAINT [FK_Mission_RewardType] FOREIGN KEY([RewardTypeID])
REFERENCES [dbo].[RewardType] ([RewardTypeID])
GO
ALTER TABLE [dbo].[Mission] CHECK CONSTRAINT [FK_Mission_RewardType]
GO
ALTER TABLE [dbo].[Player]  WITH CHECK ADD  CONSTRAINT [FK_Player_Role] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Role] ([RoleID])
GO
ALTER TABLE [dbo].[Player] CHECK CONSTRAINT [FK_Player_Role]
GO
ALTER TABLE [dbo].[PlayerAchievement]  WITH CHECK ADD  CONSTRAINT [FK_PlayerAchievement_Achievement] FOREIGN KEY([AchievementID])
REFERENCES [dbo].[Achievement] ([AchievementID])
GO
ALTER TABLE [dbo].[PlayerAchievement] CHECK CONSTRAINT [FK_PlayerAchievement_Achievement]
GO
ALTER TABLE [dbo].[PlayerAchievement]  WITH CHECK ADD  CONSTRAINT [FK_PlayerAchievement_Player] FOREIGN KEY([PlayerID])
REFERENCES [dbo].[Player] ([PlayerID])
GO
ALTER TABLE [dbo].[PlayerAchievement] CHECK CONSTRAINT [FK_PlayerAchievement_Player]
GO
ALTER TABLE [dbo].[PlayerHack]  WITH CHECK ADD  CONSTRAINT [FK_PlayerHack_Hack] FOREIGN KEY([HackID])
REFERENCES [dbo].[Hack] ([HackID])
GO
ALTER TABLE [dbo].[PlayerHack] CHECK CONSTRAINT [FK_PlayerHack_Hack]
GO
ALTER TABLE [dbo].[PlayerHack]  WITH CHECK ADD  CONSTRAINT [FK_PlayerHack_Player] FOREIGN KEY([PlayerID])
REFERENCES [dbo].[Player] ([PlayerID])
GO
ALTER TABLE [dbo].[PlayerHack] CHECK CONSTRAINT [FK_PlayerHack_Player]
GO
ALTER TABLE [dbo].[PlayerMission]  WITH CHECK ADD  CONSTRAINT [FK_PlayerMission_Mission] FOREIGN KEY([MissionID])
REFERENCES [dbo].[Mission] ([MissionID])
GO
ALTER TABLE [dbo].[PlayerMission] CHECK CONSTRAINT [FK_PlayerMission_Mission]
GO
ALTER TABLE [dbo].[PlayerMission]  WITH CHECK ADD  CONSTRAINT [FK_PlayerMission_Player] FOREIGN KEY([PlayerID])
REFERENCES [dbo].[Player] ([PlayerID])
GO
ALTER TABLE [dbo].[PlayerMission] CHECK CONSTRAINT [FK_PlayerMission_Player]
GO
ALTER TABLE [dbo].[PlayerSkill]  WITH CHECK ADD  CONSTRAINT [FK_PlayerSkill_Player] FOREIGN KEY([PlayerID])
REFERENCES [dbo].[Player] ([PlayerID])
GO
ALTER TABLE [dbo].[PlayerSkill] CHECK CONSTRAINT [FK_PlayerSkill_Player]
GO
ALTER TABLE [dbo].[PlayerSkill]  WITH CHECK ADD  CONSTRAINT [FK_PlayerSkill_Skill] FOREIGN KEY([SkillID])
REFERENCES [dbo].[Skill] ([SkillID])
GO
ALTER TABLE [dbo].[PlayerSkill] CHECK CONSTRAINT [FK_PlayerSkill_Skill]
GO
ALTER TABLE [dbo].[Relation]  WITH CHECK ADD  CONSTRAINT [FK_Relation_Player] FOREIGN KEY([PlayerID1])
REFERENCES [dbo].[Player] ([PlayerID])
GO
ALTER TABLE [dbo].[Relation] CHECK CONSTRAINT [FK_Relation_Player]
GO
ALTER TABLE [dbo].[Relation]  WITH CHECK ADD  CONSTRAINT [FK_Relation_Player1] FOREIGN KEY([PlayerID2])
REFERENCES [dbo].[Player] ([PlayerID])
GO
ALTER TABLE [dbo].[Relation] CHECK CONSTRAINT [FK_Relation_Player1]
GO
ALTER TABLE [dbo].[Relation]  WITH CHECK ADD  CONSTRAINT [FK_Relation_RelationStatus] FOREIGN KEY([RelationStatusID])
REFERENCES [dbo].[RelationStatus] ([RelationStatusID])
GO
ALTER TABLE [dbo].[Relation] CHECK CONSTRAINT [FK_Relation_RelationStatus]
GO
ALTER TABLE [dbo].[Skill]  WITH CHECK ADD  CONSTRAINT [FK_Skill_SkillCategory] FOREIGN KEY([SkillCategoryID])
REFERENCES [dbo].[SkillCategory] ([SkillCategoryID])
GO
ALTER TABLE [dbo].[Skill] CHECK CONSTRAINT [FK_Skill_SkillCategory]
GO
/****** Object:  StoredProcedure [dbo].[calcLevel]    Script Date: 3-6-2019 18:29:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Bunky,,Rob van der Heijden>
-- Create date: <27-05-19,,>
-- Description:	<Level Calculation,,>
-- =============================================
CREATE PROCEDURE [dbo].[calcLevel]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE Player 
	SET playerLevel = FLOOR(SQRT(experience)) * 0.2
END
GO
USE [master]
GO
ALTER DATABASE [dbi413117] SET  READ_WRITE 
GO
