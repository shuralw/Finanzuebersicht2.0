using Contract.Architecture.Backend.Core.Logic.Tests.Modules.Bankwesen.Banken;
using System;

namespace Contract.Architecture.Backend.Core.Logic.Tests.Modules.Kundenstamm.Kunden
{
    public class KundeTestValues
    {
        public static readonly Guid IdDefault = Guid.Parse("95d78e9e-0e30-4a38-aab8-55331a0adce7");
        public static readonly Guid IdDefault2 = Guid.Parse("a76a34fd-9d62-459c-89ae-8392529f53ce");
        public static readonly Guid IdForCreate = Guid.Parse("d7e4b14b-af2d-428f-9f8b-758d28cceee6");

        public static readonly string NameDefault = "NameDefault";
        public static readonly string NameDefault2 = "NameDefault2";
        public static readonly string NameForCreate = "NameForCreate";
        public static readonly string NameForUpdate = "NameForUpdate";

        public static readonly int BalanceDefault = 144;
        public static readonly int BalanceDefault2 = 626;
        public static readonly int BalanceForCreate = 463;
        public static readonly int BalanceForUpdate = 313;

        public static readonly Guid BankIdDefault = BankTestValues.IdDefault;
        public static readonly Guid BankIdDefault2 = BankTestValues.IdDefault2;
        public static readonly Guid BankIdForCreate = BankTestValues.IdDefault;
        public static readonly Guid BankIdForUpdate = BankTestValues.IdDefault2;
    }
}