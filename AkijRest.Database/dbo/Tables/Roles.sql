CREATE TABLE [dbo].[Roles] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (MAX) NULL,
    [DisplayName] NVARCHAR (MAX) NULL,
    [Controller]  NVARCHAR (MAX) NULL,
    [Action]      NVARCHAR (MAX) NULL,
    [Icon]        NVARCHAR (MAX) NULL,
    [RoleGroupId] INT            NOT NULL,
    CONSTRAINT [PK_dbo.Roles] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 80),
    CONSTRAINT [FK_dbo.Roles_dbo.RoleGroups_RoleGroupId] FOREIGN KEY ([RoleGroupId]) REFERENCES [dbo].[RoleGroups] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_RoleGroupId]
    ON [dbo].[Roles]([RoleGroupId] ASC) WITH (FILLFACTOR = 80);

