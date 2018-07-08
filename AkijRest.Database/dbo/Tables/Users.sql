CREATE TABLE [dbo].[Users] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [FullName]     NVARCHAR (MAX) NULL,
    [UserName]     NVARCHAR (MAX) NULL,
    [Password]     NVARCHAR (MAX) NULL,
    [Email]        NVARCHAR (MAX) NULL,
    [Approved]     BIT            NOT NULL,
    [SuperVisorId] INT            DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_dbo.Users] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 80)
);

