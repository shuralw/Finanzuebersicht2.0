using Finanzuebersicht.Backend.Core.Contract.Logic.Modules.UserManagement.EmailUsers;

namespace Finanzuebersicht.Backend.Core.Logic.Tests.Modules.UserManagement.EmailUsers
{
    public class EmailUserCreate : IEmailUserCreate
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}