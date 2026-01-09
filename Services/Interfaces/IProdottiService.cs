using BuildWeek2.Models.Dto.Prodotti;
using BuildWeek2.Models.Entities;

namespace BuildWeek2.Services.Interfaces
{
    public interface IProdottiService
    {
        Task<List<GetProdottiDto>> GetAllProducts();
        Task<Prodotti> GetProdottoById(Guid Id);

        Task<Prodotti> CreateProdottoAsync(Prodotti prodotto);       
       
        Task UpdateProductAsync(Prodotti existingProdotto);
        Task DeleteProdottoAsync(Guid id);
    }
}
