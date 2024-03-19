namespace Playstore.Core.Exceptions

{
    public class SqlException : Exception
    {
        public SqlException(string message) : base(message)
        {}
    }
}