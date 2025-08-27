using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TableKanban.Interfaces;
using TableKanban.Model;
using static TableKanban.Model.TableUserFormModel;

namespace TableKanban.Services
{
  /// <summary>
  /// Сервис для работы с таблицами.
  /// </summary>
  public class TableService : ITableService
  {
    private readonly TableDbContext db;

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="db">Контекст БД.</param>
    public TableService(TableDbContext db)
    {
      this.db = db;
    }

    /// <summary>
    /// Получить все таблицы.
    /// </summary>
    /// <returns>Список таблиц.</returns>
    public List<Table> GetListTable()
    {
      return db.Tables.ToList();
    }

    /// <summary>
    /// Получить таблицу.
    /// </summary>
    /// <param name="tableId">ИД таблицы.</param>
    /// <returns>Данные таблицы.</returns>
    public Table? GetTableById(int tableId)
    {
      try
      {
        return db.Tables.FirstOrDefault(t => t.TableId == tableId);
      }
      catch 
      {
        return null;
      }
    }

    /// <summary>
    /// Добавить таблицу.
    /// </summary>
    /// <param name="newTable">Данные таблицы.</param>
    public async Task CreateTableAsync(TableFormModel model)
    {
      var table = new Table()
      {
        TableName = model.TableName,
        Description = model.Description,
        Stolbs = model.Stolbs
      };
      db.Tables.Add(table);
      await db.SaveChangesAsync();

      foreach (var formUser in model.FormUsers)
      {
        var newUser = new User
        {
          UserName = formUser.UserName,
          Email = formUser.Email
        };
        db.Users.Add(newUser);
        await db.SaveChangesAsync();

        var tableUser = new TableUser
        {
          TableId = table.TableId,
          UserId = newUser.UserId
        };
        db.TableUsers.Add(tableUser);
        await db.SaveChangesAsync();
      }
    }

    /// <summary>
    /// Обновить таблицу.
    /// </summary>
    /// <param name="table">Данные таблицы.</param>
    /// <param name="formUsers">Донные пользователей.</param>
    public async Task UpdateTableAsync(Table table, List<UserModel> formUsers)
    {
      var existingTable = await db.Tables
        .Include(t => t.Stolbs)
        .FirstOrDefaultAsync(t => t.TableId == table.TableId);
      if (existingTable != null) 
      {
        db.Entry(existingTable).CurrentValues.SetValues(table);
      }

      var existingUsers = await db.TableUsers
        .Where(tu => tu.TableId == table.TableId)
        .ToListAsync();

      db.TableUsers.RemoveRange(existingUsers);

      foreach (var formUser in formUsers)
      {
        var newUser = new User
        {
          UserName = formUser.UserName,
          Email = formUser.Email
        };
        db.Users.Add(newUser);
        await db.SaveChangesAsync(); 

        var tableUser = new TableUser
        {
          TableId = table.TableId,
          UserId = newUser.UserId
        };
        db.TableUsers.Add(tableUser);
      }

      await db.SaveChangesAsync();
    }

    /// <summary>
    /// Получить список пользователей для таблицы.
    /// </summary>
    /// <param name="tableId">ИД таблицы.</param>
    /// <returns>Список пользователей.</returns>
    public async Task<List<UserModel>> GetUsersForTableAsync(int tableId)
    {
      var users = await db.TableUsers
          .Where(tu => tu.TableId == tableId)
          .Include(tu => tu.User)
          .Select(tu => new UserModel 
          {
            Email = tu.User!.Email,
            UserName = tu.User!.UserName
          })
          .ToListAsync();

      return users;
    }
  }
}
