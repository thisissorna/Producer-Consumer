
-- ----------------------------
-- Table structure for Persons
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[Consumer].[Persons]') AND type IN ('U'))
	DROP TABLE [Consumer].[Persons]
GO

CREATE TABLE [Consumer].[Persons] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [FirstName] nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [LastName] nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [Age] int  NULL,
  [CreatedAt] datetime2(7)  NULL,
  [ModifiedAt] datetime2(7)  NULL
)
GO

ALTER TABLE [Consumer].[Persons] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Primary Key structure for table Persons
-- ----------------------------
ALTER TABLE [Consumer].[Persons] ADD CONSTRAINT [PK_YourTable] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO

