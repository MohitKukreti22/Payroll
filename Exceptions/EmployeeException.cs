namespace PayRoll.Exceptions
{
    public class EmployeeException:Exception
    {
        string message;
        public EmployeeException()
        {
            message = "NO employee with the given id";
        }
        public override string Message => message;
    }
}
    

