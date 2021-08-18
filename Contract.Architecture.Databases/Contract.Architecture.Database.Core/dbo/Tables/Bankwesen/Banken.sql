CREATE TABLE [dbo].[Banken] (
    [Id]                   UNIQUEIDENTIFIER NOT NULL,
	[MandantId]	   UNIQUEIDENTIFIER NOT NULL,
	[Name]                 NVARCHAR (256)   NOT NULL,
	[EroeffnetAm]          DATETIME         NOT NULL,
	[IsPleite]             BIT              NOT NULL,
    CONSTRAINT [PK_Banken_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Banken_MandantId] FOREIGN KEY ([MandantId]) REFERENCES [dbo].[Mandanten] ([Id]),
);
