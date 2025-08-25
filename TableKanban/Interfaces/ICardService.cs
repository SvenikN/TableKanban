using System.Collections.Generic;
using System.Threading.Tasks;
using TableKanban.Model;

namespace TableKanban.Interfaces
{
  public interface ICardService
  {
    Task<List<Card>> GetCardsAsync();
    Task<Card> GetCardAsync(int id);
    Task CreateCardAsync(Card card);
    Task UpdateCardAsync(Card card);
    Task DeleteCardAsync(int id);
  }
}
