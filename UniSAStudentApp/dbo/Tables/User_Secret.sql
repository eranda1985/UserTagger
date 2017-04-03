CREATE TABLE [dbo].[User_Secret] (
    [Id]          INT          IDENTITY (1, 1) NOT NULL,
    [ChannelId]   VARCHAR (50) NOT NULL,
    [Secret]      VARCHAR (64) NOT NULL,
    [CreatedDate] DATETIME     NOT NULL,
    [ActiveFlag]  BIT          DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_User_Secret_UA_AssociatedDevice] FOREIGN KEY ([ChannelId]) REFERENCES [dbo].[UA_AssociatedDevice] ([ChannelId])
);

