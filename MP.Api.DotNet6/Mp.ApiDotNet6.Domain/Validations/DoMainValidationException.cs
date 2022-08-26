
namespace MP.ApiDotNet6.Domain.Validations
{
    public class DoMainValidationException : Exception
    {
        public DoMainValidationException(string error) : base(error)
        {
        }
        //criar um metodo para fazer a validação/ condição se é verdadeira ou não
        public static void When(bool hasError, string message)
        {
            if(hasError)
                throw new DoMainValidationException(message);

        }
    }
}
