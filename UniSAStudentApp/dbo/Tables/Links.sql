CREATE TABLE [dbo].[Links] (
    [Id]         VARCHAR (10)  NOT NULL,
    [Title]      VARCHAR (100) NOT NULL,
    [Url]        VARCHAR (200) NOT NULL,
    [ActiveFlag] VARCHAR (1)   NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

