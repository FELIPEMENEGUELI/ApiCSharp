using MP.ApiDotNet6.Domain.Validations;

namespace MP.ApiDotNet6.Domain.Entities
{
    public sealed class Product
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string CodErp { get; private set; }
        public decimal Price { get; private set; }
        public ICollection<Purchase> Purchase { get; set; }

        //construtor usado para adicionar
        public Product(string name, string codErp, decimal price)
        {
            Validation(name, codErp, price);
            Purchase = new List<Purchase>();
        }

        //construtor usado para editar
        public Product(int id, string name, string codErp, decimal price)
        {
            DoMainValidationException.When(id < 0, "ID invalido!");
            Id = id;
            Validation(name, codErp, price);
        }

        private void Validation(string name, string codErp, decimal price)
        {
            DoMainValidationException.When(string.IsNullOrEmpty(name), "Nome deve ser informado!");
            DoMainValidationException.When(string.IsNullOrEmpty(codErp), "Codigo deve ser informado!");
            DoMainValidationException.When(price < 0, "Preço deve ser informado!");

            Name = name;
            CodErp = codErp;
            Price = price;
        }
    }
}
