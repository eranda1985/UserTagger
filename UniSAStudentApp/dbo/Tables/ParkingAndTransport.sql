CREATE TABLE [dbo].[ParkingAndTransport] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [CampusCode]  VARCHAR (10)   NOT NULL,
    [Title]       VARCHAR (200)  NOT NULL,
    [Description] VARCHAR (2000) NOT NULL,
    [LinkId]      VARCHAR (10)   NULL,
    [SortOrder]   INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ParkingAndTransport_Links] FOREIGN KEY ([LinkId]) REFERENCES [dbo].[Links] ([Id]),
    CONSTRAINT [FK_ParkingAndTransport_Ref_Campuses] FOREIGN KEY ([CampusCode]) REFERENCES [dbo].[Ref_Campuses] ([CampusCode]),
    CONSTRAINT [AK_ParkingAndTransport_CampusCode_Title] UNIQUE NONCLUSTERED ([CampusCode] ASC, [Title] ASC)
);

