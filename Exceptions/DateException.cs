namespace PayRoll.Exceptions
{
    public class DateException:Exception
    {
        string message;
        public DateException()
        {
            message = "Invaild Date ";
        }
        public override string Message => message;
    }
}

