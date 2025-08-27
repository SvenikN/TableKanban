using System.Collections.Generic;
using System.Threading.Tasks;
using TableKanban.Model;

namespace TableKanban.Components.Pages
{
  /// <summary>
  /// Компонента списка таблиц.
  /// </summary>
  public partial class Home
  {
    #region Поля и свойства

    /// <summary>
    /// Список таблиц.
    /// </summary>
    public List<Table> tables;

    #endregion

    #region Базовый класс

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    protected override async Task OnInitializedAsync()
    {
      tables = TableService.GetListTable();
    }

    #endregion

    #region Методы

    /// <summary>
    /// Добавить таблицу.
    /// </summary>
    private void AddNewTable()
    {
      this.NavigationManager.NavigateTo("/table/create");
    }

    /// <summary>
    /// Открыть таблицу.
    /// </summary>
    /// <param name="id">ИД таблицы.</param>
    public void OpenTableById(int id)
    {
      NavigationManager.NavigateTo($"/table/{id}");
    }

    #endregion
  }
}
