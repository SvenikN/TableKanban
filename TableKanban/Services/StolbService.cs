using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TableKanban.Model;

namespace TableKanban.Services
{
  public class StolbService
  {
    private readonly TableDbContext db;

    public StolbService(TableDbContext db)
    {
      this.db = db;
    }

    public List<Stolb> GetStolbsByTableId(int tableId)
    {
      return db.Stolbs
          .Where(s => s.TableId == tableId)
          .ToList();
    }
  }
}
