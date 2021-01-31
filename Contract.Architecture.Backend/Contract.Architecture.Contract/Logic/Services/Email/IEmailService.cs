namespace Contract.Architecture.Contract.Logic.Services.Email
{
    public interface IEmailService
    {
        void Send(IEmail email);
    }
}