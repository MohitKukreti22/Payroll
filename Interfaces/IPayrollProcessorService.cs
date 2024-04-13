
using Castle.Core.Resource;
using PayRoll.Models;
using PayRoll.Services;




namespace PayRoll.Interfaces
{
    public interface IPayrollProcessorService
    {
        public Task<PayrollProcessor> ChangePayrollProcessorPhoneAsync(int id, string phone);
        public Task<PayrollProcessor> ChangePayrollProcessorName(int id, string name);

        public Task<PayrollProcessor> DeletePayrollProcessor(int id);

        public Task<List<PayrollProcessor>> GetAllPayrollProcessor();

        public Task<PayrollProcessor> GetPayrollProcessorByEmail(string email);
        
    }
}