using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore;
using System;
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
    public async Task<List<Table>?> GetListTableAsync()
    {
      try
      {
        var table = await db.Tables.ToListAsync();
      
        if (table == null) return null; 

        return table;
      }
      catch (DbUpdateException ex)
      {
        throw new InvalidOperationException($"Произошла ошибка при получении таблиц {ex.Message}", ex);
      }
      catch (Exception ex)
      {
        throw new Exception($"Произошла ошибка {ex.Message}.", ex);
      }
    }

    /// <summary>
    /// Получить таблицу.
    /// </summary>
    /// <param name="tableId">ИД таблицы.</param>
    /// <returns>Данные таблицы.</returns>
    public async Task<Table?> GetTableByIdAsync(int tableId)
    {
      try
      {
        var table = await db.Tables
                 .Include(t => t.Stolbs)
                   .ThenInclude(s => s.Cards)
                 .FirstOrDefaultAsync(t => t.TableId == tableId);

        if (table == null) return null;

        return table;
      }
      catch (DbUpdateException ex)
      {
        throw new InvalidOperationException($"Произошла ошибка при получении таблицы {ex.Message}", ex);
      }
      catch (Exception ex)
      {
        throw new Exception($"Произошла ошибка {ex.Message}.", ex);
      }

    }

    /// <summary>
    /// Добавить таблицу.
    /// </summary>
    /// <param name="newTable">Данные таблицы.</param>
    public async Task CreateTableAsync(TableFormModel model)
    {
      try 
      {
        if (string.IsNullOrEmpty(model.TableName))
        {
          throw new InvalidOperationException("Заполни название таблицы.");
        }
        if (model.Stolbs.Count <= 2)
        {
          throw new InvalidOperationException("В таблице должно  быть не менее 2 столбцов.");
        }
        else
        {
          var table = new Table
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
          }

          await db.SaveChangesAsync();
        }
      }
      catch (InvalidOperationException ex)
      {
        throw new Exception(ex.Message);
      }
      catch (DbUpdateException ex)
      {
        throw new InvalidOperationException($"Произошла ошибка при сохранении {ex.Message}", ex);
      }
      catch (Exception ex)
      {
        throw new Exception($"Произошла ошибка при сохранении {ex.Message}", ex);
      }
    }

    /// <summary>
    /// Обновить таблицу.
    /// </summary>
    /// <param name="table">Данные таблицы.</param>
    /// <param name="formUsers">Данные пользователей.</param>
    public async Task UpdateTableAsync(Table table, List<UserModel> formUsers)
    {
      try
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
      catch (DbUpdateException ex)
      {
        throw new InvalidOperationException($"Нельзя удалить столбец, он содержит карточки {ex.Message}", ex);
      }
      catch (Exception ex)
      {
        throw new InvalidOperationException($"Нельзя удалить столбец, он содержит карточки");
        throw new Exception($"Произошла ошибка при сохранении {ex.Message}", ex);
      }
    }

    /// <summary>
    /// Получить список пользователей для таблицы.
    /// </summary>
    /// <param name="tableId">ИД таблицы.</param>
    /// <returns>Список пользователей.</returns>
    public async Task<List<UserModel>> GetUsersForTableAsync(int tableId)
    {
      try 
      {
        if (tableId <= 0)
        {
          throw new ArgumentException("Таблицы не существует", nameof(tableId));
        }

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
      catch (ArgumentException ex)
      {
        throw new ArgumentException($"Талицы не существует: {ex.Message}", ex);
      }
      catch (DbUpdateException ex)
      {
        throw new Exception($"Ошибка при получении списка {ex.Message}", ex);
      }
      catch (Exception ex)
      {
        throw new Exception($"Произошла непредвиденная ошибка: {ex.Message}", ex);
      }
    }

    /// <summary>
    /// Удалить таблицу.
    /// </summary>
    /// <param name="tableId">ИД таблицы.</param>
    public async Task DeleteTableAsync(int tableId)
    {
      try
      {
        var table = await db.Tables.FindAsync(tableId);

        if (table == null)
        {
          throw new InvalidOperationException("Таблицы не существует.");
        }

        var hasCards = await db.Cards.AnyAsync(c => c.TableId == tableId);

        if (hasCards)
        {
          throw new InvalidOperationException($"Нельзя удалить таблицу. Таблица {table.TableName} содедежит карточки.");
        }

        db.Tables.Remove(table);
        await db.SaveChangesAsync();
      }
      catch (InvalidOperationException ex)
      {
        throw;
      }
      catch (DbUpdateException ex)
      {
        throw new Exception($"Ошибка при удалении таблицы: {ex.Message}", ex);
      }
      catch (Exception ex)
      {
        throw new Exception($"Произошла ошибка при удалении таблицы: {ex.Message}", ex);
      }
    }
  }
}
