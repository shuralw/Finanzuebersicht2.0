using Contract.Architecture.Contract.Logic.Services.Email;

namespace Contract.Architecture.Logic.Services.Email
{
    internal class Email : IEmail
    {
        public string To { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }
    }
}