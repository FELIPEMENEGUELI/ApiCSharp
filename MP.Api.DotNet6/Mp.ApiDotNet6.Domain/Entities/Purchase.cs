
using MP.ApiDotNet6.Domain.Validations;

namespace MP.ApiDotNet6.Domain.Entities
{
    public class Purchase
    {
        public int Id { get; private set; }
        public int ProductId { get; private set; }
        public int PersonId { get; private set; }
        public DateTime Date { get; private set; }
        public Person Person { get; set; }
        public Product Product { get; set; }

        //construtor usado para adicionar
        public Purchase(int productId, int personId)
        {
            Validation(productId, personId);
        }


        //construtor usado para editar
        public Purchase(int id, int productId, int personId)
        {
            DoMainValidationException.When(id <= 0, "Id invalido");
            Id = id;
            Validation(productId, personId);
        }

        public void Edit(int id, int productId, int personId)
        {
            DoMainValidationException.When(id <= 0, "Id invalido");
            Id = id;
            Validation(productId, personId);
        }

        private void Validation(int productId, int personId)
        {
            DoMainValidationException.When(productId <= 0, "Id produto deve ser informado!");
            DoMainValidationException.When(personId <= 0, "Id pessoa deve ser informado");

            ProductId = productId;
            PersonId = personId;
            Date = DateTime.Now;
        }
    }
}
