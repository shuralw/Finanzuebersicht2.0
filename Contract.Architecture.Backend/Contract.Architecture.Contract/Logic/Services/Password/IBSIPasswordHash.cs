namespace Contract.Architecture.Contract.Logic.Services.Password
{
    public interface IBSIPasswordHash
    {
        string PasswordHash { get; set; }

        string Salt { get; set; }
    }
}