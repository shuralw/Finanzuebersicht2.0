using Contract.Architecture.Backend.Core.Persistence.Tests.Modules.Bankwesen.Banken;
using Contract.Architecture.Backend.Core.Persistence.Tests.Modules.MandantenTrennung.Mandanten;
using System;

namespace Contract.Architecture.Backend.Core.Persistence.Tests.Modules.Kundenstamm.Kunden
{
    public class KundeTestValues
    {
        public static readonly Guid IdDbDefault = Guid.Parse("16a4b64a-50f4-4305-8d04-055d9e3d8463");
        public static readonly Guid IdDbDefault2 = Guid.Parse("b69a5188-f94c-4579-88f7-b636a23cbe32");
        public static readonly Guid IdForCreate = Guid.Parse("af3baab3-6b5e-42c2-92fb-c86c1da51b3f");

        public static readonly Guid MandantIdDbDefault = MandantTestValues.IdDbDefault;

        public static readonly string NameDbDefault = "NameDbDefault";
        public static readonly string NameDbDefault2 = "NameDbDefault2";
        public static readonly string NameForCreate = "NameForCreate";
        public static readonly string NameForUpdate = "NameForUpdate";

        public static readonly int BalanceDbDefault = 364;
        public static readonly int BalanceDbDefault2 = 624;
        public static readonly int BalanceForCreate = 714;
        public static readonly int BalanceForUpdate = 347;

        public static readonly Guid BankIdDbDefault = BankTestValues.IdDbDefault;
        public static readonly Guid BankIdDbDefault2 = BankTestValues.IdDbDefault2;
        public static readonly Guid BankIdForCreate = BankTestValues.IdDbDefault;
        public static readonly Guid BankIdForUpdate = BankTestValues.IdDbDefault2;
    }
}