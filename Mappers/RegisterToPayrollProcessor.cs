using PayRoll.Models;
using PayRoll.Models.DTOs;

namespace PayRoll.Mappers
{
    public class RegisterToPayrollProcessor
    {

        PayrollProcessor payrollProcessor;   
        public RegisterToPayrollProcessor(RegisterPayrollProcessorDTO register)
        {
            payrollProcessor = new PayrollProcessor();
            payrollProcessor.Name = register.Name;
            payrollProcessor.Email = register.Email;
            payrollProcessor.Phone = register.Phone;
        }
        public PayrollProcessor GetPayrollProcessor()
        {
            return payrollProcessor;
        }
    }
}