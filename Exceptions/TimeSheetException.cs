namespace PayRoll.Exceptions
{
    public class TimeSheetException : Exception
    {

        string message;
        public TimeSheetException()
        {
            message = "Timesheet already exists for the specified week";
        }
        public override string Message => message;
    }
}
