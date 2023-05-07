namespace TreeApi.Services.Implementation
{
	public class SecureException : Exception
    {
        public SecureException(string message) : base(message)
        {
        }
    }
}