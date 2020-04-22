IF OBJECT_ID(N'__EFMigrationsHistory') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [actor] (
    [actor_id] int NOT NULL IDENTITY,
    [first_name] varchar(45) NOT NULL,
    [last_name] varchar(45) NOT NULL,
    [last_update] datetime NOT NULL DEFAULT (getdate()),
    CONSTRAINT [PK_actor] PRIMARY KEY ([actor_id])
);

GO

CREATE TABLE [film] (
    [film_id] int NOT NULL IDENTITY,
    [release_year] varchar(4) NULL,
    [description] text NULL DEFAULT N'null',
    [length] smallint NOT NULL,
    [title] varchar(255) NOT NULL,
    [last_update] datetime NOT NULL,
    CONSTRAINT [PK_film] PRIMARY KEY ([film_id])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20180126161912_Inicial', N'2.0.0-rtm-26452');

GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'film') AND [c].[name] = N'description');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [film] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [film] ALTER COLUMN [description] text NULL;

GO

CREATE TABLE [category] (
    [category_id] tinyint NOT NULL,
    [name] varchar(25) NOT NULL,
    [last_update] datetime NOT NULL DEFAULT (getdate()),
    CONSTRAINT [PK_category] PRIMARY KEY ([category_id])
);

GO

CREATE TABLE [film_actor] (
    [actor_id] int NOT NULL,
    [film_id] int NOT NULL,
    [last_update] AS getdate(),
    CONSTRAINT [PK_film_actor] PRIMARY KEY ([actor_id], [film_id]),
    CONSTRAINT [FK_film_actor_actor_actor_id] FOREIGN KEY ([actor_id]) REFERENCES [actor] ([actor_id]) ON DELETE CASCADE,
    CONSTRAINT [FK_film_actor_film_film_id] FOREIGN KEY ([film_id]) REFERENCES [film] ([film_id]) ON DELETE CASCADE
);

GO

CREATE TABLE [language] (
    [language_id] tinyint NOT NULL,
    [name] char(20) NOT NULL,
    [last_update] datetime NOT NULL,
    CONSTRAINT [PK_language] PRIMARY KEY ([language_id])
);

GO

CREATE TABLE [film_category] (
    [film_id] int NOT NULL,
    [category_id] tinyint NOT NULL,
    [last_update] datetime2 NOT NULL,
    CONSTRAINT [PK_film_category] PRIMARY KEY ([film_id], [category_id]),
    CONSTRAINT [FK_film_category_category_category_id] FOREIGN KEY ([category_id]) REFERENCES [category] ([category_id]) ON DELETE CASCADE,
    CONSTRAINT [FK_film_category_film_film_id] FOREIGN KEY ([film_id]) REFERENCES [film] ([film_id]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_film_actor_film_id] ON [film_actor] ([film_id]);

GO

CREATE INDEX [IX_film_category_category_id] ON [film_category] ([category_id]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20180129230417_CategoriaEIdioma', N'2.0.0-rtm-26452');

GO

ALTER TABLE [film] ADD [language_id] tinyint NOT NULL DEFAULT 0;

GO

ALTER TABLE [film] ADD [original_language_id] tinyint NULL;

GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'language') AND [c].[name] = N'last_update');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [language] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [language] ALTER COLUMN [last_update] datetime NOT NULL;
ALTER TABLE [language] ADD DEFAULT (getdate()) FOR [last_update];

GO

CREATE INDEX [IX_film_language_id] ON [film] ([language_id]);

GO

CREATE INDEX [IX_film_original_language_id] ON [film] ([original_language_id]);

GO

ALTER TABLE [film] ADD CONSTRAINT [FK_film_language_language_id] FOREIGN KEY ([language_id]) REFERENCES [language] ([language_id]) ON DELETE CASCADE;

GO

ALTER TABLE [film] ADD CONSTRAINT [FK_film_language_original_language_id] FOREIGN KEY ([original_language_id]) REFERENCES [language] ([language_id]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20180130001801_Idioma', N'2.0.0-rtm-26452');

GO

