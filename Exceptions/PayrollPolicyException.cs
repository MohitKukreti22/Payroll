namespace PayRoll.Exceptions
{
    public class PayrollPolicyException:Exception
    {
        string message;
        public PayrollPolicyException()
        {
            message = "NO Payrollpolicy with the given id";
        }
        public override string Message => message;
    }
}