using Contract.Architecture.Backend.Core.Persistence.Tests.Modules.MandantenTrennung.Mandanten;
using System;

namespace Contract.Architecture.Backend.Core.Persistence.Tests.Modules.Bankwesen.Banken
{
    public class BankTestValues
    {
        public static readonly Guid IdDbDefault = Guid.Parse("e9b5aa8c-4624-4b5c-8794-6f39c070ca78");
        public static readonly Guid IdDbDefault2 = Guid.Parse("d3b6d67b-1603-4883-b19c-cff79cb28040");
        public static readonly Guid IdForCreate = Guid.Parse("4613e9a1-7ccb-4664-b213-39cef17d430d");

        public static readonly Guid MandantIdDbDefault = MandantTestValues.IdDbDefault;

        public static readonly string NameDbDefault = "NameDbDefault";
        public static readonly string NameDbDefault2 = "NameDbDefault2";
        public static readonly string NameForCreate = "NameForCreate";
        public static readonly string NameForUpdate = "NameForUpdate";

        public static readonly DateTime EroeffnetAmDbDefault = new DateTime(2012, 3, 27);
        public static readonly DateTime EroeffnetAmDbDefault2 = new DateTime(2012, 5, 14);
        public static readonly DateTime EroeffnetAmForCreate = new DateTime(2018, 7, 10);
        public static readonly DateTime EroeffnetAmForUpdate = new DateTime(2018, 9, 5);

        public static readonly bool IsPleiteDbDefault = true;
        public static readonly bool IsPleiteDbDefault2 = false;
        public static readonly bool IsPleiteForCreate = false;
        public static readonly bool IsPleiteForUpdate = false;
    }
}