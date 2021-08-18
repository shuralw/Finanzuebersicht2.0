using System;

namespace Contract.Architecture.Backend.Core.Logic.Tests.Modules.Bankwesen.Banken
{
    public class BankTestValues
    {
        public static readonly Guid IdDefault = Guid.Parse("fad18917-3676-4294-82b2-1801c6974360");
        public static readonly Guid IdDefault2 = Guid.Parse("d17ff694-bed4-4883-9e57-440daadd6592");
        public static readonly Guid IdForCreate = Guid.Parse("3d04431a-3542-49aa-a082-f388c27b0fcf");

        public static readonly string NameDefault = "NameDefault";
        public static readonly string NameDefault2 = "NameDefault2";
        public static readonly string NameForCreate = "NameForCreate";
        public static readonly string NameForUpdate = "NameForUpdate";

        public static readonly DateTime EroeffnetAmDefault = new DateTime(2019, 1, 31);
        public static readonly DateTime EroeffnetAmDefault2 = new DateTime(2014, 8, 20);
        public static readonly DateTime EroeffnetAmForCreate = new DateTime(2016, 8, 19);
        public static readonly DateTime EroeffnetAmForUpdate = new DateTime(2018, 6, 22);

        public static readonly bool IsPleiteDefault = false;
        public static readonly bool IsPleiteDefault2 = false;
        public static readonly bool IsPleiteForCreate = false;
        public static readonly bool IsPleiteForUpdate = false;
    }
}