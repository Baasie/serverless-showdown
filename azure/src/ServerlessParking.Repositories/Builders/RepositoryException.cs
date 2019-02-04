using System;

namespace ServerlessParking.Repositories.Builders
{
    public class RepositoryException : Exception
    {
        public RepositoryException(string message) : base(message)
        {
        }

        public RepositoryException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
