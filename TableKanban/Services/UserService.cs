using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TableKanban.Model;

namespace TableKanban.Services
{
  public class UserService
  {
    private readonly TableDbContext db;

    public UserService(TableDbContext db)
    {
      this.db = db;
    }

    public async Task<List<User>> GetUsersAsync()
    {
      return await db.Users.ToListAsync();
    }
  }
}
