using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PayRoll.Models
{
    public class PayrollPolicy:IEquatable<PayrollPolicy>
    {


        [Key]
        public int PolicyID { get; set; }

          public int AdminID {  get; set; }
        [ForeignKey("AdminID")]
            public string PolicyName { get; set; } = string.Empty;

       
            public string Description { get; set; } = string.Empty;

          
            public string CalculationFormula { get; set; } = string.Empty;

            
            public string ApplicableComponents { get; set; } = string.Empty;


        public Admin ? Admin { get; set; }
           
           




        public PayrollPolicy()
        {
         
        }

        public PayrollPolicy(int policyId, string policyName, string description, string calculationFormula, string applicableComponents)
        {
            PolicyID = policyId;
            PolicyName = policyName;
            Description = description;
            CalculationFormula = calculationFormula;
            ApplicableComponents = applicableComponents;
        }

        public bool Equals(PayrollPolicy? other)
        {
            var policyId = other ?? new PayrollPolicy();
            return this.PolicyID.Equals(policyId.PolicyID);
        }

    }

}

