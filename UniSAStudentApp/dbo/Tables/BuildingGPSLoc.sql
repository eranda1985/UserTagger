CREATE TABLE [dbo].[BuildingGPSLoc] (
    [Id]            INT          IDENTITY (1, 1) NOT NULL,
    [CampusCode]    VARCHAR (10) NULL,
    [BuildingCode]  VARCHAR (20) NOT NULL,
    [BuildingTitle] VARCHAR (50) NOT NULL,
    [Coords]        VARCHAR (30) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BuildingGPSLoc_Ref_Campuses] FOREIGN KEY ([CampusCode]) REFERENCES [dbo].[Ref_Campuses] ([CampusCode]),
    CONSTRAINT [AK_BuildingGPSLoc_CampusCode_BuildingCode] UNIQUE NONCLUSTERED ([CampusCode] ASC, [BuildingCode] ASC)
);

