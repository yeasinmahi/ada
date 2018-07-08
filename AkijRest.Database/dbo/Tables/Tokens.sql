CREATE TABLE [dbo].[Tokens] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [UserName]     NVARCHAR (MAX) NULL,
    [TokenContent] NVARCHAR (MAX) NULL,
    [TimeExpiry]   DATETIME       NOT NULL,
    CONSTRAINT [PK_dbo.Tokens] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 80)
);

