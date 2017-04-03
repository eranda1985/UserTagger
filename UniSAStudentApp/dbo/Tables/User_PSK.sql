CREATE TABLE [dbo].[User_PSK] (
    [Id]          INT          IDENTITY (1, 1) NOT NULL,
    [ChannelId]   VARCHAR (50) NOT NULL,
    [VersionId]   INT          NOT NULL,
    [Key]         VARCHAR (64) NOT NULL,
    [CreatedDate] DATETIME     NOT NULL,
    [ActiveFlag]  BIT          DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_User_PSK_ApplicationVersion] FOREIGN KEY ([VersionId]) REFERENCES [dbo].[ApplicationVersions] ([Id]),
    CONSTRAINT [FK_User_PSK_UA_AssociatedDevice] FOREIGN KEY ([ChannelId]) REFERENCES [dbo].[UA_AssociatedDevice] ([ChannelId]),
    CONSTRAINT [AK_User_PSK_ChannelId] UNIQUE NONCLUSTERED ([ChannelId] ASC, [VersionId] ASC)
);

