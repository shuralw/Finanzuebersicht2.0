using Contract.Architecture.Backend.Core.Persistence.Tests.Modules.Bankwesen.Banken;
using System;

namespace Contract.Architecture.Backend.Core.Persistence.Tests.Modules.Kundenstamm.Kunden
{
    public class KundeTestValues
    {
        public static readonly Guid IdDbDefault = Guid.Parse("3f7a9f42-8d28-4c19-bde7-3e4865fcacdb");
        public static readonly Guid IdDbDefault2 = Guid.Parse("0627760b-2ede-463b-b81b-875d37788657");
        public static readonly Guid IdForCreate = Guid.Parse("dc056c41-7be9-45e2-97ec-da9b74425d36");

        public static readonly string NameDbDefault = "NameDbDefault";
        public static readonly string NameDbDefault2 = "NameDbDefault2";
        public static readonly string NameForCreate = "NameForCreate";
        public static readonly string NameForUpdate = "NameForUpdate";

        public static readonly int BalanceDbDefault = 266;
        public static readonly int BalanceDbDefault2 = 552;
        public static readonly int BalanceForCreate = 932;
        public static readonly int BalanceForUpdate = 842;

        public static readonly Guid BankIdDbDefault = BankTestValues.IdDbDefault;
        public static readonly Guid BankIdDbDefault2 = BankTestValues.IdDbDefault2;
        public static readonly Guid BankIdForCreate = BankTestValues.IdDbDefault;
        public static readonly Guid BankIdForUpdate = BankTestValues.IdDbDefault2;
    }
}