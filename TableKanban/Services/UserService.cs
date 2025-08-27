using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TableKanban.Interfaces;
using TableKanban.Model;

namespace TableKanban.Services
{
  /// <summary>
  /// Сервис для работы с пользователями.
  /// </summary>
  public class UserService : IUserService
  {
    private readonly TableDbContext db;

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="db">Контекст БД.</param>
    public UserService(TableDbContext db)
    {
      this.db = db;
    }

    /// <summary>
    /// Получить список пользователей.
    /// </summary>
    /// <returns>Список пользователей.</returns>
    public async Task<List<User>> GetUsersAsync()
    {
      return await db.Users.ToListAsync();
    }
  }
}
