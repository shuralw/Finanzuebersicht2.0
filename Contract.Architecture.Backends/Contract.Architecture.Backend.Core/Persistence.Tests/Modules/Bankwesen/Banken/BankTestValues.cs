using System;

namespace Contract.Architecture.Backend.Core.Persistence.Tests.Modules.Bankwesen.Banken
{
    public class BankTestValues
    {
        public static readonly Guid IdDbDefault = Guid.Parse("61c7e876-f8a2-4cfc-85a6-86f37378c45d");
        public static readonly Guid IdDbDefault2 = Guid.Parse("9d5bd607-eee7-4cc3-a952-cbf6e2b97924");
        public static readonly Guid IdForCreate = Guid.Parse("1e24ddb1-b7e4-4c67-a77a-e09554022e2f");

        public static readonly string NameDbDefault = "NameDbDefault";
        public static readonly string NameDbDefault2 = "NameDbDefault2";
        public static readonly string NameForCreate = "NameForCreate";
        public static readonly string NameForUpdate = "NameForUpdate";

        public static readonly DateTime EroeffnetAmDbDefault = new DateTime(2015, 4, 7);
        public static readonly DateTime EroeffnetAmDbDefault2 = new DateTime(2013, 7, 22);
        public static readonly DateTime EroeffnetAmForCreate = new DateTime(2019, 11, 26);
        public static readonly DateTime EroeffnetAmForUpdate = new DateTime(2014, 4, 14);

        public static readonly bool IsPleiteDbDefault = true;
        public static readonly bool IsPleiteDbDefault2 = false;
        public static readonly bool IsPleiteForCreate = false;
        public static readonly bool IsPleiteForUpdate = false;
    }
}