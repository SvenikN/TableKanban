using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TableKanban.Model;

namespace TableKanban.Services
{
  public class TableService
  {
    private readonly TableDbContext db;

    public TableService(TableDbContext db)
    {
      this.db = db;
    }

    public List<Table> GetListTable()
    {
      return db.Tables.ToList();
    }

    public async Task CreateTableAsync(Table newTable)
    {
      db.Tables.Add(newTable);
      await db.SaveChangesAsync();
    }

    public Table GetTableById(int tableId)
    {
      return db.Tables.FirstOrDefault(t => t.TableId == tableId);
    }

    public async Task UpdateTableAsync(Table table)
    {
      var existingTable = await db.Tables
        .Include(t => t.Stolbs)
        .FirstOrDefaultAsync(t => t.TableId == table.TableId);
      if (existingTable != null) 
      {
        db.Entry(existingTable).CurrentValues.SetValues(table);

      }

      await db.SaveChangesAsync();
    }
  }
}
