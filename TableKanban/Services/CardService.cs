using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TableKanban.Model;

namespace TableKanban.Services
{
  public class CardService
  {
    private readonly TableDbContext db;

    public CardService(TableDbContext db)
    {
      this.db = db;
    }

    public List<Card> GetCardsByTableId(int tableId)
    {
      return db.Cards
          .Include(c => c.Stolb)
          .Include(c => c.User)
          .Where(c => c.TableId == tableId)
          .ToList();
    }

    public async Task<Card> GetCardAsync(int id)
    {
      return await db.Cards
            .Include(c => c.Stolb)
            .Include(c => c.User)
            .FirstOrDefaultAsync(c => c.CardId == id);
    }

    public void CreateCard(Card card)
    {
      if (card.Description == null)
      {
        throw new ArgumentNullException(nameof(card));
      }

      db.Cards.Add(card);
      db.SaveChangesAsync();
    }

    public async Task UpdateCardAsync(Card card)
    {
      var existingCard = await db.Cards.FindAsync(card.CardId);

      if (existingCard != null)
      {
        existingCard.Title = card.Title;
        existingCard.Description = card.Description;
        existingCard.StolbId = card.StolbId;
        existingCard.UserId = card.UserId;

        await db.SaveChangesAsync();
      }
    }

    //public async Task DeleteCardAsync(int id)
    //{
    //  using (var context = db.CreateDbContext())
    //  {
    //    var card = await context.Cards.FindAsync(id);
    //    if (card != null)
    //    {
    //      context.Cards.Remove(card);
    //      await context.SaveChangesAsync();
    //    }
    //  }
    //}
  }
}
