CREATE TABLE [dbo].[AccountingEntries] (
    [Id]                   UNIQUEIDENTIFIER NOT NULL,
	[EmailUserId]	   UNIQUEIDENTIFIER NOT NULL,
	[CategoryId]           UNIQUEIDENTIFIER NOT NULL,
	[Auftragskonto]        NVARCHAR (30)   NOT NULL,
	[Buchungsdatum]        DATETIME         NOT NULL,
	[ValutaDatum]          DATETIME         NOT NULL,
	[Buchungstext]         NVARCHAR (300)   NOT NULL,
	[Verwendungszweck]     NVARCHAR (300)   NOT NULL,
	[GlaeubigerId]         NVARCHAR (100)   NOT NULL,
	[Mandatsreferenz]      NVARCHAR (100)   NOT NULL,
	[Sammlerreferenz]      NVARCHAR (100)   NOT NULL,
	[LastschriftUrsprungsbetrag] FLOAT            NOT NULL,
	[AuslagenersatzRuecklastschrift] NVARCHAR (100)   NOT NULL,
	[Beguenstigter]        NVARCHAR (200)   NOT NULL,
	[IBAN]                 NVARCHAR (22)   NOT NULL,
	[BIC]                  NVARCHAR (15)   NOT NULL,
	[Betrag]               FLOAT            NOT NULL,
	[Waehrung]             NVARCHAR (10)   NOT NULL,
	[Info]                 NVARCHAR (100)   NOT NULL,
    CONSTRAINT [PK_AccountingEntries_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccountingEntries_EmailUserId] FOREIGN KEY ([EmailUserId]) REFERENCES [dbo].[EmailUsers] ([Id]),
    CONSTRAINT [FK_AccountingEntries_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Categories] ([Id]),
);

GO
