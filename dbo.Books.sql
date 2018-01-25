CREATE TABLE [dbo].[Books] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [Title]           NVARCHAR (200) NOT NULL,
    [Pages]           INT            NOT NULL,
    [ISBN]            NVARCHAR (20)  DEFAULT ('') NOT NULL,
    [PictureName]     NVARCHAR (200) NULL,
    [AuthorId]        INT            NULL,
    [StringsAsString] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.Books] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Books_dbo.Authors_AuthorId] FOREIGN KEY ([AuthorId]) REFERENCES [dbo].[Authors] ([AuthorId]) ON DELETE CASCADE ON UPDATE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_AuthorId]
    ON [dbo].[Books]([AuthorId] ASC);

