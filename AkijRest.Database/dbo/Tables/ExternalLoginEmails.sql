CREATE TABLE [dbo].[ExternalLoginEmails] (
    [Id]       INT            NOT NULL,
    [Google]   NVARCHAR (MAX) NULL,
    [Facebook] NVARCHAR (MAX) NULL,
    [UserId]   INT            NOT NULL,
    CONSTRAINT [PK_dbo.ExternalLoginEmails] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 80),
    CONSTRAINT [FK_dbo.ExternalLoginEmails_dbo.Users_Id] FOREIGN KEY ([Id]) REFERENCES [dbo].[Users] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Id]
    ON [dbo].[ExternalLoginEmails]([Id] ASC) WITH (FILLFACTOR = 80);

