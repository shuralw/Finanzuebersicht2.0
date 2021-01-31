namespace Contract.Architecture.Contract.Logic.Services.Email
{
    public interface IEmail
    {
        string To { get; set; }

        string Subject { get; set; }

        string Message { get; set; }
    }
}