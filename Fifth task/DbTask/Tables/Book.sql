﻿CREATE TABLE [dbo].[Book]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Author] NVARCHAR(50) NOT NULL, 
    [Title] NVARCHAR(50) NOT NULL, 
    [Genre] NVARCHAR(50) NOT NULL
)