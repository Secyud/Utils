namespace Secyud.Utils.EntityFrameworkCore.Exceptions;

public class EfCoreModelException : Exception
{
    public EfCoreModelException()
    {
    }

    public EfCoreModelException(string message)
        : base(message)
    {
    }
}