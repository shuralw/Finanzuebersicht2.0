using Finanzuebersicht.Backend.Core.Contract.Logic.Modules.Accounting.Categories;
using System;

namespace Finanzuebersicht.Backend.Core.Contract.Logic.Modules.Accounting.AccountingEntries
{
    public interface IAccountingEntryListItem
    {
        Guid Id { get; set; }

        ICategory Category { get; set; }

        string Auftragskonto { get; set; }

        DateTime Buchungsdatum { get; set; }

        DateTime ValutaDatum { get; set; }

        string Buchungstext { get; set; }

        string Verwendungszweck { get; set; }

        string GlaeubigerId { get; set; }

        string Mandatsreferenz { get; set; }

        string Sammlerreferenz { get; set; }

        double LastschriftUrsprungsbetrag { get; set; }

        string AuslagenersatzRuecklastschrift { get; set; }

        string Beguenstigter { get; set; }

        string IBAN { get; set; }

        string BIC { get; set; }

        double Betrag { get; set; }

        string Waehrung { get; set; }

        string Info { get; set; }
    }
}