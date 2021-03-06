USE [hanhuynhba_H094_onlinestore]
GO
/****** Object:  Table [dbo].[ecom_ProductGroups]    Script Date: 8/24/2016 6:58:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ecom_ProductGroups](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nchar](200) NOT NULL,
	[Description] [nchar](500) NULL,
	[Status] [int] NULL,
 CONSTRAINT [PK_ecom_ProductGroups] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ecom_Products_ProductGroups]    Script Date: 8/24/2016 6:58:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ecom_Products_ProductGroups](
	[ProductId] [int] NOT NULL,
	[GroupId] [int] NOT NULL,
 CONSTRAINT [PK_ecom_Products_ProductGroups] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC,
	[GroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ecom_Products_ProductGroups]  WITH CHECK ADD  CONSTRAINT [FK_ecom_Products_ProductGroups_ecom_ProductGroups] FOREIGN KEY([GroupId])
REFERENCES [dbo].[ecom_ProductGroups] ([Id])
GO
ALTER TABLE [dbo].[ecom_Products_ProductGroups] CHECK CONSTRAINT [FK_ecom_Products_ProductGroups_ecom_ProductGroups]
GO
ALTER TABLE [dbo].[ecom_Products_ProductGroups]  WITH CHECK ADD  CONSTRAINT [FK_ecom_Products_ProductGroups_ecom_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[ecom_Products] ([Id])
GO
ALTER TABLE [dbo].[ecom_Products_ProductGroups] CHECK CONSTRAINT [FK_ecom_Products_ProductGroups_ecom_Products]
GO
