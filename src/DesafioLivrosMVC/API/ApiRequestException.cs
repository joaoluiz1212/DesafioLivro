namespace DesafioLivrosMVC.API;

public class ApiRequestException : Exception
{
    public ApiRequestException(string mensagem, Exception exception) : base(mensagem, exception)
    {
        
    }
}
