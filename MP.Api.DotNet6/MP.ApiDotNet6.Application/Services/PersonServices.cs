using AutoMapper;
using MP.ApiDotNet6.Application.DTOs;
using MP.ApiDotNet6.Application.DTOs.Validations;
using MP.ApiDotNet6.Application.Services.Interfaces;
using MP.ApiDotNet6.Domain.Entities;
using MP.ApiDotNet6.Domain.FiltersDb;
using MP.ApiDotNet6.Domain.Repositories;

namespace MP.ApiDotNet6.Application.Services
{
    public class PersonServices : IPersonServices
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public PersonServices(IPersonRepository personRepository, IMapper mapper)
        {
            _personRepository = personRepository;
            _mapper = mapper;
        }

        public async Task<ResultServices<PersonDTO>> CreateAsync(PersonDTO personDTO)
        {
            if (personDTO == null)
                return ResultServices.Fail<PersonDTO>("Objeto deve ser informado");

            var result = new PersonDTOValidator().Validate(personDTO);
            if (!result.IsValid)
                return ResultServices.RequestError<PersonDTO>("Problemas de validade", result);

            var person = _mapper.Map<Person>(personDTO);
            var data = await _personRepository.CreateAsync(person);
            return ResultServices.Ok<PersonDTO>(_mapper.Map<PersonDTO>(data));
         }


        public async Task<ResultServices<ICollection<PersonDTO>>> GetAsync()
        {
            var people = await _personRepository.GetPeopleAsync();
            return ResultServices.Ok<ICollection<PersonDTO>>(_mapper.Map<ICollection<PersonDTO>>(people));
        }

        public async Task<ResultServices<PersonDTO>> GetByIdAsync(int id)
        {
            var person = await _personRepository.GetbByIdAsync(id);
            if (person == null)
                return ResultServices.Fail<PersonDTO>("Pessoa nao encontrada");
            return ResultServices.Ok(_mapper.Map<PersonDTO>(person));
        }
        public async Task<ResultServices> DeleteAsync(int id)
        {
            var person = await _personRepository.GetbByIdAsync(id);
            if (person == null)
                return ResultServices.Fail("Id invalido");
            await _personRepository.DeleteAsync(person);
            return ResultServices.Ok($"Pessoa do id:{id} foi deletada");
        }

        public async Task<ResultServices> UpdateAsync(PersonDTO personDTO)
        {
            if (personDTO == null)
                return ResultServices.Fail("Objeto deve ser informado");

            var validation = new PersonDTOValidator().Validate(personDTO);
            if (!validation.IsValid)
                return ResultServices.RequestError("Problema com a validação do campo", validation);

            var person = await _personRepository.GetbByIdAsync(personDTO.Id);
            if (person == null)
                return ResultServices.Fail("Pessoa não encontrada");

            //var person = _mapper.Map<Person>(personDTO);              //Usado para inserir
            person = _mapper.Map<PersonDTO, Person>(personDTO, person); //Usado para editar
            await _personRepository.EditAsync(person);
            return ResultServices.Ok("Pessoa editada com sucesso");
        }

        public async Task<ResultServices<PagedBaseResponseDTO<PersonDTO>>> GetPagedAsync(PersonFilterDb personFilterDb)
        {
            var peoplePaged = await _personRepository.GetPagedAsync(personFilterDb);
            var result = new PagedBaseResponseDTO<PersonDTO>(peoplePaged.TotalRegisters,
                _mapper.Map<List<PersonDTO>>(peoplePaged.Data));
        
            return ResultServices.Ok(result);
        }
    }
}
