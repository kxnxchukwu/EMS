USE [Employee]
GO

/****** Object:  Table [dbo].[deletedEmployees]    Script Date: 2/5/2019 1:14:23 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[deletedEmployees](
	[ID] [int] NOT NULL,
	[fullName] [varchar](250) NOT NULL,
	[email] [varchar](50) NOT NULL,
	[phoneNumber] [varchar](50) NOT NULL,
	[age] [int] NOT NULL,
	[salary] [int] NOT NULL,
	[gender] [varchar](50) NOT NULL,
	[department] [varchar](50) NOT NULL,
 CONSTRAINT [PK_deletedEmployees] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


