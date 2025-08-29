using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TableKanban.Interfaces;
using TableKanban.Model;

namespace TableKanban.Services
{
  /// <summary>
  /// Сервис для работы с карточками.
  /// </summary>
  public class CardService : ICardService
  {
    private readonly TableDbContext db;

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="db">Контекст БД.</param>
    public CardService(TableDbContext db)
    {
      this.db = db;
    }

    /// <summary>
    /// Получить все карточки для таблицы.
    /// </summary>
    /// <param name="tableId">ИД таблицы.</param>
    /// <returns>Список карточек.</returns>
    public List<Card> GetCardsByTableId(int tableId)
    {
        return db.Cards
            .Include(c => c.Stolb)
            .Include(c => c.User)
            .Where(c => c.TableId == tableId)
            .ToList();
    }

    /// <summary>
    /// Получить карточку.
    /// </summary>
    /// <param name="id">ИД карточки.</param>
    /// <returns>Данные карточки.</returns>
    public async Task<Card?> GetCardAsync(int id)
    {
        var card = await db.Cards
              .Include(c => c.Stolb)
              .Include(c => c.User)
              .FirstOrDefaultAsync(c => c.CardId == id);

        if (card == null) return null;

        return card;
    }

    /// <summary>
    /// Добавить карточку.
    /// </summary>
    /// <param name="card">Данные карточки.</param>
    public async Task CreateCardAsync(Card card)
    {
      try
      {
        if (string.IsNullOrEmpty(card.Title))
        {
          throw new InvalidOperationException("Заполни название карточки.");
        }
        if (card.StolbId == 0)
        {
          throw new InvalidOperationException("Выбери статус карточки.");
        }

        db.Cards.Add(card);
        await db.SaveChangesAsync();
      }
      catch (Exception)
      {
        throw;
      }
    }

    /// <summary>
    /// Обновить карточку.
    /// </summary>
    /// <param name="card">Обновленные данные карточки.</param>
    public async Task UpdateCardAsync(Card card)
    {
      try
      {
        if (string.IsNullOrEmpty(card.Title))
        {
          throw new InvalidOperationException("Заполни название карточки.");
        }
        if (card.StolbId == 0)
        {
          throw new InvalidOperationException("Выбери статус карточки.");
        }

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
      catch (Exception)
      {
        throw;
      }
    }

    /// <summary>
    /// Удалить карточку.
    /// </summary>
    /// <param name="id">ИД карточки.</param>
    public async Task DeleteCardAsync(int id)
    {
      try
      {
        var card = await db.Cards.FindAsync(id);

        if (card != null)
        {
          db.Cards.Remove(card);
          await db.SaveChangesAsync();
        }
      }
      catch (InvalidOperationException ex)
      {
        throw;
      }
      catch (Exception ex)
      {
        throw new Exception($"Произошла ошибка при удалении таблицы: {ex.Message}", ex);
      }
    }
  }
}
