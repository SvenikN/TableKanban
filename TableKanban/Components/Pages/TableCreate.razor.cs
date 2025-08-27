using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using static TableKanban.Model.TableUserFormModel;

namespace TableKanban.Components.Pages
{
  /// <summary>
  /// Компонента для создания таблицы.
  /// </summary>
  public partial class TableCreate
  {
    #region Поля и свойства

    /// <summary>
    /// Данные для создания таблицы.
    /// </summary>
    public TableFormModel newTable = new TableFormModel();

    #endregion

    #region Методы

    /// <summary>
    /// Сохранить.
    /// </summary>
    private async Task HandleSave()
    {
      await TableService.CreateTableAsync(newTable);
      NavigationManager.NavigateTo($"/");
    }

    /// <summary>
    /// Отмена.
    /// </summary>
    private void HandleCansel()
    {
      NavigationManager.NavigateTo($"/");
    }

    #endregion
  }
}
