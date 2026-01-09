using BuildWeek2.Models.Dto.RicoveroAnimaleSmarrito;
using BuildWeek2.Models.Entities;

namespace BuildWeek2.Services.Interfaces
{
    public interface IRicoveroAnimaleSmarritoService
    {
        Task<List<GetAnimaleSmarritoDto>> GetAllAnimaliRicoverati();
        Task<RicoveroAnimaleSmarrito> GetAnimaliSmarritiRicoveratiById(Guid Id);
        Task<RicoveroAnimaleSmarrito> UpdateAnimaliSmarritiRicoveratiAsync(RicoveroAnimaleSmarrito ricoveroAnimaleSmarrito);
        Task CreateAnimaliSmarritiRicoveratiAsync(RicoveroAnimaleSmarrito ricoveroAnimaleSmarrito);
        Task DeleteAnimaliRicoveratiAsync(Guid id);
    }
}
