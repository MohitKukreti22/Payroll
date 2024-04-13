namespace PayRoll.Exceptions
{
    public class DeductionException:Exception
    {
       
        
            string message;
            public DeductionException()
            {
            message = "Total earnings must be greater than total deductions.";
            }
            public override string Message => message;
        }
    }


