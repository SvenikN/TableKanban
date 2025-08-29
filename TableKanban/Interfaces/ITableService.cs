using System.Collections.Generic;
using System.Threading.Tasks;
using TableKanban.Model;
using static TableKanban.Model.TableUserFormModel;

namespace TableKanban.Interfaces
{
  public interface ITableService
  {
    Task<List<Table>?> GetListTableAsync();

    Task<Table?> GetTableByIdAsync(int tableId);

    Task CreateTableAsync(TableFormModel model);

    Task UpdateTableAsync(Table table, List<UserModel> formUsers);

    Task<List<UserModel>> GetUsersForTableAsync(int tableId);
    Task DeleteTableAsync(int tableId);
  }
}
