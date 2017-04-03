CREATE TABLE [dbo].[ApplicationVersions] (
    [Id]          INT          IDENTITY (1, 1) NOT NULL,
    [DeviceType]  VARCHAR (10) NOT NULL,
    [Version]     VARCHAR (20) NOT NULL,
    [Hash]        VARCHAR (64) NOT NULL,
    [CreatedDate] DATETIME     NOT NULL,
    [AllowedFlag] VARCHAR (1)  NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ApplicationVersions_DeviceTypes] FOREIGN KEY ([DeviceType]) REFERENCES [dbo].[Ref_DeviceTypes] ([Name]),
    CONSTRAINT [AK_ApplicationVersions_Version_DeviceType] UNIQUE NONCLUSTERED ([Version] ASC, [DeviceType] ASC)
);

