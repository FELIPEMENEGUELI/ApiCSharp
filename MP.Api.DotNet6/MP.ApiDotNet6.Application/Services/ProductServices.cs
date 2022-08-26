using AutoMapper;
using MP.ApiDotNet6.Application.DTOs;
using MP.ApiDotNet6.Application.DTOs.Validations;
using MP.ApiDotNet6.Application.Services.Interfaces;
using MP.ApiDotNet6.Domain.Entities;
using MP.ApiDotNet6.Domain.Repositories;

namespace MP.ApiDotNet6.Application.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductServices(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ResultServices<ProductDTO>> CreateAsync(ProductDTO productDTO)
        {
            if (productDTO == null)
                return ResultServices.Fail<ProductDTO>("Objeto deve ser informado!");

            var result = new ProductDTOValidator().Validate(productDTO);
            if (!result.IsValid)
                return ResultServices.RequestError<ProductDTO>("Problema na validação", result);

            //convertendo dto para entidade
            var product = _mapper.Map<Product>(productDTO);
            var data = await _productRepository.CreateAsync(product);
            return ResultServices.Ok<ProductDTO>(_mapper.Map<ProductDTO>(data));
        }

        public async Task<ResultServices<ICollection<ProductDTO>>> GetAsync()
        {
            var products = await _productRepository.GetProductsAsync();
            return ResultServices.Ok<ICollection<ProductDTO>>(_mapper.Map<ICollection<ProductDTO>>(products));
        }

        public async Task<ResultServices<ProductDTO>> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                return ResultServices.Fail<ProductDTO>("Produto nao encontrado");

            return ResultServices.Ok<ProductDTO>(_mapper.Map<ProductDTO>(product));
            
        }
        public async Task<ResultServices> DeleteAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                return ResultServices.Fail("Id invalido");

            await _productRepository.DeleteAsync(product);
            return ResultServices.Ok($"Produto do id:{id} foi deletado");
        }


        public async Task<ResultServices> UpdateAsync(ProductDTO productDTO)
        {
            if (productDTO == null)
                return ResultServices.Fail("Produto deve ser informado");

            var validation = new ProductDTOValidator().Validate(productDTO);
            if (!validation.IsValid)
                return ResultServices.RequestError("Problema com a validação", validation);

            var product = await _productRepository.GetByIdAsync(productDTO.Id);
            if (product == null)
                return ResultServices.Fail("Produto nao encontrado");

            product = _mapper.Map<ProductDTO, Product>(productDTO, product);
            await _productRepository.EditAsync(product);
            return ResultServices.Ok("Produto editado com sucesso!");
        }
    }
}
