CREATE TABLE [dbo].[UserDetail] (
    [Username]       VARCHAR (250) NOT NULL,
    [DateRegistered] DATETIME2 (7) DEFAULT (getdate()) NOT NULL,
    [Email]          VARCHAR (250) NULL,
    [UserType]       VARCHAR (20)  NULL,
    [LastAuthDate]   DATETIME2 (7) NULL,
    PRIMARY KEY CLUSTERED ([Username] ASC)
);

