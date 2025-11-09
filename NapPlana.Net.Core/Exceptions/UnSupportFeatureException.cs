namespace NapPlana.Core.Exceptions;

public class UnSupportFeatureException: Exception
{
    public UnSupportFeatureException() : base() { }
    public UnSupportFeatureException(string message) : base(message) { }
    public UnSupportFeatureException(string message, Exception inner) : base(message, inner) { }
}