CREATE TABLE [dbo].[BoolReport]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [DateOfGiving] DATE NOT NULL , 
    [ReturnStatus] BIT NOT NULL , 
    [StateOfBook] NVARCHAR(50) NULL, 
    [BookId] INT NOT NULL, 
    [SubscriberId] INT NOT NULL, 
    CONSTRAINT [FK_BoolReport_Subscriber] FOREIGN KEY ([SubscriberId]) REFERENCES [Subcriber]([Id]), 
    CONSTRAINT [FK_BoolReport_Book] FOREIGN KEY ([BookId]) REFERENCES [Book]([Id]), 
    
)
