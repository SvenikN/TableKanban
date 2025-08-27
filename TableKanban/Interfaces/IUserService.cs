using System.Collections.Generic;
using System.Threading.Tasks;
using TableKanban.Model;

namespace TableKanban.Interfaces
{
  public interface IUserService
  {
    Task<List<User>> GetUsersAsync();
  }
}
