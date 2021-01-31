using Contract.Architecture.Contract.Logic.Services.Password;

namespace Contract.Architecture.Logic.Services.Password
{
    internal class BsiPasswordHash : IBSIPasswordHash
    {
        public string PasswordHash { get; set; }

        public string Salt { get; set; }
    }
}