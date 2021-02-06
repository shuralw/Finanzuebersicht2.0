namespace Contract.Architecture.Contract.Logic.Model.Users.EmailUserPasswordReset
{
    public interface IBrowserInfo
    {
        string Browser { get; set; }

        string OperatingSystem { get; set; }
    }
}