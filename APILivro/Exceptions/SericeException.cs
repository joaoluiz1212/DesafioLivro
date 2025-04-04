namespace APILivro.Exceptions
{
    public class SericeException : Exception
    {
        public SericeException(string mensagem, Exception exception) : base(mensagem, exception)
        {

        }
    }
}
