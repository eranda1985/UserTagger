CREATE TABLE [dbo].[Tags] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [Name]         VARCHAR (250) NOT NULL,
    [Description]  VARCHAR (250) NULL,
    [ToInstall]    BIT           DEFAULT ((1)) NULL,
    [IsActivated]  BIT           DEFAULT ((0)) NULL,
    [TagGroup]     INT           DEFAULT ((1)) NULL,
    [CreatedDate]  DATETIME      DEFAULT (getdate()) NULL,
    [ModifiedDate] DATETIME      NULL,
    [IsDeleted]    BIT           DEFAULT ((0)) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Tags_To_TagGroups] FOREIGN KEY ([TagGroup]) REFERENCES [dbo].[TagGroups] ([Id]) ON DELETE CASCADE
);



