namespace PayRoll.Exceptions
{
    public class AuditTrailException:Exception
    {

        string message;
        public AuditTrailException()
        {
            message = "NO Audit Trail with the given id";
        }
        public override string Message => message;
    }
}
    

