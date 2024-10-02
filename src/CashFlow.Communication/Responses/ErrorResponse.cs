namespace CashFlow.Communication.Responses
{
    public class ErrorResponse
    {
        public ErrorResponse(string errorMessage)
        {
            ErrorMessages = [errorMessage];
        }

        public ErrorResponse(List<string> errorMessages)
        {
            ErrorMessages = errorMessages;
        }

        public List<string> ErrorMessages { get; set; }
    }
}
