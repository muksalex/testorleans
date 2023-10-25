using Orleans.Providers;

namespace BankAccountCommon
{
    [StorageProvider(ProviderName = "SqlServer")]
    public class BankAccountGrain : Grain<AccountBalance>, IBankAccountGrain
    {
        public Task<decimal> GetBalance()
        {
            return Task.FromResult(State.Balance);
        }

        public async Task Deposit(decimal amount)
        {
            if (amount > 0)
            {
                State.Balance += amount;
                await WriteStateAsync();
            }
        }

        public async Task Withdraw(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Withdrawal amount must be greater than zero.");

            if (amount > State.Balance)
                throw new InsufficientFundsException("Insufficient funds for withdrawal.");
            State.Balance -= amount;
            await WriteStateAsync();
        }
    }
}
