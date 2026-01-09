using BuildWeek2.Models.Dto.RicoveroAnimale;
using BuildWeek2.Models.Entities;

namespace BuildWeek2.Services.Interfaces
{
    public interface IRicoveroAnimaleService
    {
        Task<List<GetRicoveroAnimaleDto>> GetAllAnimaliRicoverati();
        Task<RicoveroAnimale> GetAnimaliRicoveratiById(Guid Id);
        Task<RicoveroAnimale> UpdateAnimaliRicoveratiAsync(RicoveroAnimale ricoveroAnimale);
        Task CreateAnimaliRicoveratiAsync(RicoveroAnimale ricoveroAnimale);
        Task DeleteAnimaliRicoveratiAsync(Guid id);
    }
}
