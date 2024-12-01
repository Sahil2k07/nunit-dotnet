namespace nunit.errors
{
    public class NunitApiException(int code, string message) : Exception(message)
    {
        public int StatusCode { get; } = code;
    }
}
