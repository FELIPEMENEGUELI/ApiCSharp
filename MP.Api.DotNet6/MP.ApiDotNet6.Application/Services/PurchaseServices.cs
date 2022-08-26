using AutoMapper;
using MP.ApiDotNet6.Application.DTOs;
using MP.ApiDotNet6.Application.DTOs.Validations;
using MP.ApiDotNet6.Application.Services.Interfaces;
using MP.ApiDotNet6.Domain.Entities;
using MP.ApiDotNet6.Domain.Repositories;

namespace MP.ApiDotNet6.Application.Services
{
    public class PurchaseServices : IPurchaseServices
    {
        private readonly IProductRepository _productRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOffWork _unitOffWork;

        public PurchaseServices(IProductRepository productRepository,
                                IPersonRepository personRepository,
                                IPurchaseRepository purchaseRepository,
                                IMapper mapper,
                                IUnitOffWork unitOffWork)
        {
            _productRepository = productRepository;
            _personRepository = personRepository;
            _purchaseRepository = purchaseRepository;
            _mapper = mapper;
            _unitOffWork = unitOffWork;
        }

        public async Task<ResultServices<PurchaseDTO>> CreateAsync(PurchaseDTO purchaseDTO)
        {
            //validando se objeto esta vindo
            if (purchaseDTO == null)
                return ResultServices.Fail<PurchaseDTO>("Objeto deve ser informado");

            //validar esses dados do objeto
            var validation = new PurchaseDTOValidator().Validate(purchaseDTO);
            if (!validation.IsValid)
                return ResultServices.RequestError<PurchaseDTO>("Problema de validação", validation);

            try
            {
                await _unitOffWork.BeginTransaction();

                //buscar o produto e pessoas
                var productId = await _productRepository.GetIdByCodErpAsync(purchaseDTO.CodErp);
                if (productId == 0)
                {
                    var product = new Product(purchaseDTO.Name, purchaseDTO.CodErp, purchaseDTO.Price ?? 0);
                    await _productRepository.CreateAsync(product);
                    productId = product.Id;
                }
                var personId = await _personRepository.GetIdByDocumentAsync(purchaseDTO.Document);
                var purchase = new Purchase(productId, personId);

                //criar objeto de compra
                var data = await _purchaseRepository.CreateAsync(purchase);
                purchaseDTO.Id = data.Id;
                await _unitOffWork.Commit();
                return ResultServices.Ok<PurchaseDTO>(purchaseDTO);

            }
            catch(Exception ex)
            {
                await _unitOffWork.RollBack();
                return ResultServices.Fail<PurchaseDTO>($"Error {ex.Message}");
            }
        }

        public async Task<ResultServices<ICollection<PurchaseDetailDTO>>> GetAsync()
        {
            var purchases = await _purchaseRepository.GetAllAsync();
            return ResultServices.Ok(_mapper.Map<ICollection<PurchaseDetailDTO>>(purchases));
        }

        public async Task<ResultServices<PurchaseDetailDTO>> GetByIdAsync(int id)
        {
            var purchase = await _purchaseRepository.GetByIdAsync(id);
            if (purchase == null)
                return ResultServices.Fail<PurchaseDetailDTO>("Compra nao encontrada");

            return ResultServices.Ok(_mapper.Map<PurchaseDetailDTO>(purchase));
        }
       
        public async Task<ResultServices<PurchaseDTO>> UpdateAsync(PurchaseDTO purchaseDTO)
        {
            if (purchaseDTO == null)
                return ResultServices.Fail<PurchaseDTO>("Objeto deve ser informado");

            var validation = new PurchaseDTOValidator().Validate(purchaseDTO);
            if (!validation.IsValid)
                return ResultServices.RequestError<PurchaseDTO>("Problema com a validação", validation);

            var purchase = await _purchaseRepository.GetByIdAsync(purchaseDTO.Id);
            if (purchase == null)
                return ResultServices.Fail<PurchaseDTO>("Compra nao encontrada");

            var productId = await _productRepository.GetIdByCodErpAsync(purchaseDTO.CodErp);
            var personId = await _personRepository.GetIdByDocumentAsync(purchaseDTO.Document);
            purchase.Edit(purchase.Id, productId, personId);
            await _purchaseRepository.EditAsync(purchase);
            return ResultServices.Ok(purchaseDTO);
        }

        public async Task<ResultServices> RemoveAsync(int id)
        {
            var purchase = await _purchaseRepository.GetByIdAsync(id);
            if (purchase == null)
                return ResultServices.Fail("Compra nao encontrada");

            await _purchaseRepository.DeleteAsync(purchase);
            return ResultServices.Ok($"Compra do id: {id} deletada");
        }
    }
}
