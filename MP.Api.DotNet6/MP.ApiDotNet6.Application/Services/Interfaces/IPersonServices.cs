using MP.ApiDotNet6.Application.DTOs;
using MP.ApiDotNet6.Domain.FiltersDb;
using MP.ApiDotNet6.Domain.Repositories;

namespace MP.ApiDotNet6.Application.Services.Interfaces
{
    public interface IPersonServices
    {
        Task<ResultServices<PersonDTO>> CreateAsync(PersonDTO personDTO);
        Task<ResultServices<ICollection<PersonDTO>>> GetAsync();
        Task<ResultServices<PersonDTO>> GetByIdAsync(int id);
        Task<ResultServices> UpdateAsync(PersonDTO personDTO);
        Task<ResultServices> DeleteAsync(int id);
        Task<ResultServices<PagedBaseResponseDTO<PersonDTO>>> GetPagedAsync(PersonFilterDb personFilterDb);

    }
}
