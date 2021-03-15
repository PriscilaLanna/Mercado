IF OBJECT_ID(N'[Produtos]') IS NULL
BEGIN		

BEGIN TRANSACTION;

CREATE TABLE [Produtos] (
[Id] int NOT NULL IDENTITY,
[Nome] nvarchar(100) NOT NULL,
[Valor] decimal(17,2) NOT NULL,
[Imagem] nvarchar(255) NULL,
CONSTRAINT [PK_Produtos] PRIMARY KEY ([Id])
);

COMMIT;

END;