namespace Spg.BowlingKata.BowlingGame
{
    public class BowlingServiceException : Exception
    {
        public BowlingServiceException()
        { }
        public BowlingServiceException(string? message)
            : base(message)
        { }
        public BowlingServiceException(string? message, Exception innerException)
            : base(message, innerException)
        { }
    }
}