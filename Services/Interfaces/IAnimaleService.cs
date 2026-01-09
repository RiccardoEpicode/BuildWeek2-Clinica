using BuildWeek2.Models.Dto.Animale;

namespace BuildWeek2.Services.Interfaces
{
    public interface IAnimaleService
    {
        Task<List<GetAnimaleDto>> GetAllAnimaliAsync();
        Task<Animale> GetByIdAsNoTracking(Guid Id);

        Task<Animale> CreateAnimaleAsync(Animale animale);

        Task DeleteAnimaleAsync(Guid id);

        Task Save(Animale animale);


    }
}
