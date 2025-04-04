namespace DesafioLivrosMVC.Exceptions;

public class ServiceException : Exception
{
    public ServiceException(string mensagem, Exception exception) : base(mensagem, exception)
    {
    }
}
