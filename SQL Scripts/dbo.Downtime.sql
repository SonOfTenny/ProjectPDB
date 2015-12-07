CREATE TABLE [dbo].[Downtime] (
    [DowntimeID]     INT            IDENTITY (1, 1) NOT NULL,
    [UserID]         INT            NOT NULL,
    [ShiftID]        INT            NOT NULL,
    [PlantID]        INT            NOT NULL,
    [DowntimeTypeID] INT            NOT NULL,
    [DowntimeType]   NVARCHAR (MAX) NULL,
    [Reason]         NVARCHAR (MAX) NULL,
    [Action]         NVARCHAR (MAX) NULL,
    [Date]           DATETIME       NOT NULL,
    CONSTRAINT [PK_dbo.Downtime] PRIMARY KEY CLUSTERED ([DowntimeID] ASC),
    CONSTRAINT [FK_dbo.Downtime_dbo.User_UserID] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([UserID]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.Downtime_dbo.Shift_ShiftID] FOREIGN KEY ([ShiftID]) REFERENCES [dbo].[Shift] ([ShiftID]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.Downtime_dbo.Plant_PlantID] FOREIGN KEY ([PlantID]) REFERENCES [dbo].[Plant] ([PlantID]) ON DELETE CASCADE,
	CONSTRAINT [FK_dbo.Downtime_dbo.DowntimeType_DowntimeTypeID] FOREIGN KEY (DowntimeTypeID) REFERENCES [dbo].[DowntimeType] (DowntimeTypeID) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_UserID]
    ON [dbo].[Downtime]([UserID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ShiftID]
    ON [dbo].[Downtime]([ShiftID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PlantID]
    ON [dbo].[Downtime]([PlantID] ASC);

GO
CREATE NONCLUSTERED INDEX [IX_DowntimeTypeID]
    ON [dbo].[Downtime](DowntimeTypeID ASC);



