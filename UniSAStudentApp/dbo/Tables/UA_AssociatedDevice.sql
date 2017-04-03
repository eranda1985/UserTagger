CREATE TABLE [dbo].[UA_AssociatedDevice] (
    [ChannelId]       VARCHAR (50) NOT NULL,
    [Username]        VARCHAR (50) NOT NULL,
    [DeviceType]      VARCHAR (10) NOT NULL,
    [DateRegistered]  DATETIME     NOT NULL,
    [PushAssociation] BIT          DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([ChannelId] ASC),
    CONSTRAINT [FK_UA_AssociatedDevice_DeviceTypes] FOREIGN KEY ([DeviceType]) REFERENCES [dbo].[Ref_DeviceTypes] ([Name])
);

