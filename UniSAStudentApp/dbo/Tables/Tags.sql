CREATE TABLE [dbo].[Tags] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [Name]      VARCHAR (250) NULL,
    [ToInstall] BIT           NULL,
    [IsNew]     BIT           NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC) 
);

