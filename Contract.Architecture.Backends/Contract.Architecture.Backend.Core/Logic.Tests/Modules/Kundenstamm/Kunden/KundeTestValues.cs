using Contract.Architecture.Backend.Core.Logic.Tests.Modules.Bankwesen.Banken;
using System;

namespace Contract.Architecture.Backend.Core.Logic.Tests.Modules.Kundenstamm.Kunden
{
    public class KundeTestValues
    {
        public static readonly Guid IdDefault = Guid.Parse("61f79b8c-05af-4005-8cc7-f6ab0ff18d13");
        public static readonly Guid IdDefault2 = Guid.Parse("2680dfaf-8a7c-4c97-a3cf-f4b80534a0d0");
        public static readonly Guid IdForCreate = Guid.Parse("613c2c38-a62a-4a20-81a4-73ac1648eb5b");

        public static readonly string NameDefault = "NameDefault";
        public static readonly string NameDefault2 = "NameDefault2";
        public static readonly string NameForCreate = "NameForCreate";
        public static readonly string NameForUpdate = "NameForUpdate";

        public static readonly int BalanceDefault = 507;
        public static readonly int BalanceDefault2 = 489;
        public static readonly int BalanceForCreate = 416;
        public static readonly int BalanceForUpdate = 655;

        public static readonly Guid BankIdDefault = BankTestValues.IdDefault;
        public static readonly Guid BankIdDefault2 = BankTestValues.IdDefault2;
        public static readonly Guid BankIdForCreate = BankTestValues.IdDefault;
        public static readonly Guid BankIdForUpdate = BankTestValues.IdDefault2;
    }
}