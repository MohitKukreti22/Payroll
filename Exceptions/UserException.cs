namespace PayRoll.Exceptions
{
    public class UserException:Exception
    {
        string message;
        public UserException()
        {
            message = "NO user with the given id";
        }
        public override string Message => message;
    }
}