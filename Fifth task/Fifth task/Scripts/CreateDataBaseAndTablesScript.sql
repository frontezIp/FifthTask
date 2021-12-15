CREATE DATABASE USERDB
GO

USE USERDB

CREATE TABLE [Book] (
    [Id]     INT           IDENTITY (1, 1) NOT NULL,
    [Author] NVARCHAR (50) NOT NULL,
    [Title]  NVARCHAR (50) NOT NULL,
    [Genre]  NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE BookReport(
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [DateOfGiving] DATE          NOT NULL,
    [ReturnStatus] BIT           NOT NULL,
    [StateOfBook]  NVARCHAR (50) NULL,
    [BookId]       INT           NOT NULL,
    [SubscriberId] INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BoolReport_Subscriber] FOREIGN KEY ([SubscriberId]) REFERENCES [dbo].[Subscriber] ([Id]),
    CONSTRAINT [FK_BoolReport_Book] FOREIGN KEY ([BookId]) REFERENCES [dbo].[Book] ([Id])
);

CREATE TABLE [Subscriber] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [FirstName]   NVARCHAR (50) NOT NULL,
    [MiddleName]  NVARCHAR (50) NOT NULL,
    [LastName]    NVARCHAR (50) NOT NULL,
    [DateOfBirth] DATE          NOT NULL,
    [Sex]         BIT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
