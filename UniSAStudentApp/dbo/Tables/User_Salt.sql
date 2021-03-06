﻿CREATE TABLE [dbo].[User_Salt] (
    [Id]          INT          IDENTITY (1, 1) NOT NULL,
    [ChannelId]   VARCHAR (50) NOT NULL,
    [Salt]        VARCHAR (64) NOT NULL,
    [CreatedDate] DATETIME     NOT NULL,
    [ActiveFlag]  BIT          DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_User_Salt_UA_AssociatedDevice] FOREIGN KEY ([ChannelId]) REFERENCES [dbo].[UA_AssociatedDevice] ([ChannelId])
);

