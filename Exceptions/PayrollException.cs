namespace PayRoll.Exceptions
{
    public class PayrollException:Exception
    {
        string message;
        public PayrollException()
        {
            message = "NO Payroll with the given id";
        }
        public override string Message => message;
    }
}