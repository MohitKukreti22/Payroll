namespace PayRoll.Exceptions
{
    public class AdminException : Exception
    {
        string message;
        public AdminException()
        {
            message = "NO admin with the given id";
        }
        public override string Message => message;
    }
}