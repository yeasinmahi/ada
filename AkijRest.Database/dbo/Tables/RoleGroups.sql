CREATE TABLE [dbo].[RoleGroups] (
    [Id]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (MAX) NULL,
    [Icon] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.RoleGroups] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 80)
);

