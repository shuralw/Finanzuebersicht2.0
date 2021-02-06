namespace Contract.Architecture.Logic.Model.Sessions.Sessions
{
    internal class SessionExpirationOptions : OptionsFromConfiguration
    {
        public override string Position => "ContractArchitecture:Sessions:Expiration";

        public bool RunOnInitialization { get; set; }

        public int ExpirationTimeInMinutes { get; set; }
    }
}