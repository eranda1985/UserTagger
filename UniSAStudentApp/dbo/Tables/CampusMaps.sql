CREATE TABLE [dbo].[CampusMaps] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [CampusCode] VARCHAR (10)  NOT NULL,
    [Url]        VARCHAR (200) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CampusMaps_Ref_Campuses] FOREIGN KEY ([CampusCode]) REFERENCES [dbo].[Ref_Campuses] ([CampusCode]),
    CONSTRAINT [AK_CampusMaps_CampusCode] UNIQUE NONCLUSTERED ([CampusCode] ASC)
);

