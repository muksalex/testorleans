using Orleans;
using System.Threading.Tasks;

namespace BankAccountCommon
{
    public interface IBankAccountGrain: IGrainWithIntegerKey
    {
        Task<decimal> GetBalance();
        Task Deposit(decimal amount);
        Task Withdraw(decimal amount);
    }
}
