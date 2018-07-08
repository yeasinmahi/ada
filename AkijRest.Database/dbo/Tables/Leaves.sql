CREATE TABLE [dbo].[Leaves] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [LeaveTypeId]  TINYINT        NOT NULL,
    [UserId]       INT            NOT NULL,
    [Date]         DATETIME       NOT NULL,
    [LeaveCause]   NVARCHAR (MAX) NULL,
    [LeaveAddress] NVARCHAR (MAX) NULL,
    [LeaveType_Id] INT            NULL,
    CONSTRAINT [PK_dbo.Leaves] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 80),
    CONSTRAINT [FK_dbo.Leaves_dbo.LeaveTypes_LeaveType_Id] FOREIGN KEY ([LeaveType_Id]) REFERENCES [dbo].[LeaveTypes] ([Id]),
    CONSTRAINT [FK_dbo.Leaves_dbo.Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_LeaveType_Id]
    ON [dbo].[Leaves]([LeaveType_Id] ASC) WITH (FILLFACTOR = 80);


GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[Leaves]([UserId] ASC) WITH (FILLFACTOR = 80);

