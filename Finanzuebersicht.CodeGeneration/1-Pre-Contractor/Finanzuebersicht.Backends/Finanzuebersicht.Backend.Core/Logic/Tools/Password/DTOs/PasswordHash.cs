using Finanzuebersicht.Backend.Core.Contract.Logic.Tools.Password;

namespace Finanzuebersicht.Backend.Core.Logic.Tools.Password
{
    internal class PasswordHash : IPasswordHash
    {
        public string Hash { get; set; }

        public string Salt { get; set; }
    }
}