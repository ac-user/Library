namespace Library.Services.Services
{
    public interface IValidate
    {
        Task<bool> ValidateAccountAsync(int accountId, CancellationToken cancellationToken);
    }
}
