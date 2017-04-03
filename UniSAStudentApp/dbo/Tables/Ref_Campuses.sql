CREATE TABLE [dbo].[Ref_Campuses] (
    [CampusCode] VARCHAR (10) NOT NULL,
    [Title]      VARCHAR (50) NOT NULL,
    [SortOrder]  INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([CampusCode] ASC)
);

