CREATE TABLE [dbo].[Kunden] (
    [Id]                   UNIQUEIDENTIFIER NOT NULL,
	[Name]                 NVARCHAR (256)   NOT NULL,
	[Balance]              INT              NOT NULL,
	[BankId]               UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Kunden_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Kunden_BankId] FOREIGN KEY ([BankId]) REFERENCES [dbo].[Banken] ([Id]),
);
