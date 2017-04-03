CREATE TABLE [dbo].[KeyContacts] (
    [Id]              INT           IDENTITY (1, 1) NOT NULL,
    [Title]           VARCHAR (100) NOT NULL,
    [Description]     VARCHAR (500) NULL,
    [Room]            VARCHAR (50)  NULL,
    [Level]           VARCHAR (50)  NULL,
    [BuildingCode]    VARCHAR (20)  NULL,
    [CampusCode]      VARCHAR (10)  NULL,
    [Phone]           VARCHAR (20)  NULL,
    [Email]           VARCHAR (50)  NULL,
    [OpenHours]       VARCHAR (100) NULL,
    [WebsiteUrl]      VARCHAR (200) NULL,
    [OnlineFormTitle] VARCHAR (100) NULL,
    [OnlineFormUrl]   VARCHAR (200) NULL,
    [ActiveFlag]      VARCHAR (1)   NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_KeyContacts_Ref_Campuses] FOREIGN KEY ([CampusCode]) REFERENCES [dbo].[Ref_Campuses] ([CampusCode]),
    CONSTRAINT [AK_KeyContacts_CampusCode_Title] UNIQUE NONCLUSTERED ([CampusCode] ASC, [Title] ASC)
);

