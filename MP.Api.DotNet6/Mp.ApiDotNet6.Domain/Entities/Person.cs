using MP.ApiDotNet6.Domain.Validations;

namespace MP.ApiDotNet6.Domain.Entities
{
    //colocamos "sealed" para nenhum projeto herde ele
    public sealed class Person
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Document { get; private set; }
        public string Phone { get; private set; }
        public ICollection<Purchase> Purchase { get; set; } // uma pessoa pode ter mais de uma conta

        //construtor usado para adicionar uma pessoa
        public Person(string name, string document, string phone)
        {
            Validation(name, document, phone);
            Purchase = new List<Purchase>();
        }

        //construtor usado para editar uma pessoa
        public Person(int id, string name, string document, string phone)
        {
            DoMainValidationException.When(id < 0, "Id invalido");
            Id = id;
            Validation(name, document, phone);
            Purchase = new List<Purchase>();
        }

        private void Validation(string name, string document, string phone)
        {
            DoMainValidationException.When(string.IsNullOrEmpty(name), "Nome deve ser informado!");
            DoMainValidationException.When(string.IsNullOrEmpty(document), "Documento deve ser informado");
            DoMainValidationException.When(string.IsNullOrEmpty(phone), "Telefone deve ser informado");

            Name = name;
            Document = document;
            Phone = phone;
        }
    }
}
