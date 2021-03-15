IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Produtos] (
    [Id] int NOT NULL IDENTITY,
    [Nome] nvarchar(100) NOT NULL,
    [Valor] decimal(17,2) NOT NULL,
    [Imagem] nvarchar(255) NULL,
    CONSTRAINT [PK_Produtos] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210311033616_InitialCreate', N'5.0.4');
GO

COMMIT;
GO

