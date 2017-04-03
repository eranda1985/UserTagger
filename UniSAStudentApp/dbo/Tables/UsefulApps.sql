CREATE TABLE [dbo].[UsefulApps] (
    [Title]      VARCHAR (50)  NOT NULL,
    [Icon]       VARCHAR (50)  NULL,
    [IosUrl]     VARCHAR (100) NULL,
    [AndroidUrl] VARCHAR (100) NULL,
    [ActiveFlag] VARCHAR (1)   NOT NULL,
    PRIMARY KEY CLUSTERED ([Title] ASC)
);

