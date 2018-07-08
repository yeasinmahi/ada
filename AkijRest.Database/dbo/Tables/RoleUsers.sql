CREATE TABLE [dbo].[RoleUsers] (
    [Role_Id] INT NOT NULL,
    [User_Id] INT NOT NULL,
    CONSTRAINT [PK_dbo.RoleUsers] PRIMARY KEY CLUSTERED ([Role_Id] ASC, [User_Id] ASC) WITH (FILLFACTOR = 80),
    CONSTRAINT [FK_dbo.RoleUsers_dbo.Roles_Role_Id] FOREIGN KEY ([Role_Id]) REFERENCES [dbo].[Roles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.RoleUsers_dbo.Users_User_Id] FOREIGN KEY ([User_Id]) REFERENCES [dbo].[Users] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Role_Id]
    ON [dbo].[RoleUsers]([Role_Id] ASC) WITH (FILLFACTOR = 80);


GO
CREATE NONCLUSTERED INDEX [IX_User_Id]
    ON [dbo].[RoleUsers]([User_Id] ASC) WITH (FILLFACTOR = 80);

