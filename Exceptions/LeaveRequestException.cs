namespace PayRoll.Exceptions
{
    public class LeaveRequestException:Exception
    {
        string message;
        public LeaveRequestException()
        {
            message = "NO Leave Request with the given id";
        }
        public override string Message => message;
    }
}
