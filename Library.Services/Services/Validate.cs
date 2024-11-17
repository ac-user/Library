namespace Library.Services.Services
{
    public class Validate : IValidate
    {
        public Validate() { }

        public async Task<bool> ValidateAccountAsync(int accountId, CancellationToken cancellationToken)
        {
            return accountId > 0;
        }
    }
}
