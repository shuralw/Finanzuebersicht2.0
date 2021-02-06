using Contract.Architecture.Contract.Logic.Model.Users.EmailUsers;

namespace Contract.Architecture.API.Model.Users.EmailUsers
{
    public class EmailUserCreate : IEmailUserCreate
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}