using PayRoll.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PayRoll.Interfaces
{
    public interface IAdminPayrollPolicyService
    {
        Task<List<PayrollPolicyDTO>> GetAllPayrollPolicies();
        Task<PayrollPolicyDTO> GetPayrollPolicy(int policyId);
        Task<PayrollPolicyDTO> CreatePayrollPolicy(PayrollPolicyDTO payrollPolicyDTO);
        Task<PayrollPolicyDTO> UpdatePayrollPolicy(int policyId, PayrollPolicyDTO payrollPolicyDTO);
        Task<bool> DeletePayrollPolicy(int policyId);
    }
}
