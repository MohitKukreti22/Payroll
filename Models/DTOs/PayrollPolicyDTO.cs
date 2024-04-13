
namespace PayRoll.DTOs
{
    public class PayrollPolicyDTO
    {
        public int PolicyID { get; set; }
        public int AdminID { get; set; }
        public string PolicyName { get; set; }
        public string Description { get; set; }
        public string CalculationFormula { get; set; }
        public string ApplicableComponents { get; set; }
    }
}
