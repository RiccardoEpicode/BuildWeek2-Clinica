using BuildWeek2.Models.Dto.Fornitore;
using BuildWeek2.Models.Dto.Prodotti;
using BuildWeek2.Models.Entities;

namespace BuildWeek2.Services.Interfaces
{
    public interface IFornitoreService
    {
        Task<List<GetFornitoreDto>> GetAllFornitori();
        Task<Fornitore> GetFornitoreById(Guid Id);

        Task<Fornitore> UpdateFornitoreAsync(Fornitore fornitore);

        Task CreateFornitoreAsync(Fornitore existingProdotto);
        Task DeleteFornitoreAsync(Guid id);
    }
}
