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
    [last_update] datetime NOT NULL,
    CONSTRAINT [PK_actor] PRIMARY KEY ([actor_id])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20180125234359_Inicial', N'2.0.0-rtm-26452');

GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'actor') AND [c].[name] = N'last_update');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [actor] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [actor] ALTER COLUMN [last_update] datetime NOT NULL;
ALTER TABLE [actor] ADD DEFAULT (getdate()) FOR [last_update];

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20180125235243_UpdateModelAtor', N'2.0.0-rtm-26452');

GO

