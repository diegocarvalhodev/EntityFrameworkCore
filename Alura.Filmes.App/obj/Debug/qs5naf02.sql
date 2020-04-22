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
VALUES (N'20180126161231_Inicial', N'2.0.0-rtm-26452');

GO

