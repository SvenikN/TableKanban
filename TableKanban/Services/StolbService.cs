using System.Collections.Generic;
using System.Linq;
using TableKanban.Interfaces;
using TableKanban.Model;

namespace TableKanban.Services
{
  /// <summary>
  /// Сервис для работы со столбцами.
  /// </summary>
  public class StolbService : IStolbService
  {
    private readonly TableDbContext db;

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="db">Контекст БД.</param>
    public StolbService(TableDbContext db)
    {
      this.db = db;
    }

    /// <summary>
    /// Получить стобцы для таблицы.
    /// </summary>
    /// <param name="tableId">ИД таблицы.</param>
    /// <returns>Список столбцов.</returns>
    public List<Stolb> GetStolbsByTableId(int tableId)
    {
      return db.Stolbs
          .Where(s => s.TableId == tableId)
          .ToList();
    }
  }
}
