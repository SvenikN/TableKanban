using Microsoft.AspNetCore.Components;
using System;
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

    /// <summary>
    /// Сообщение об ошибке.
    /// </summary>
    private string? errorMessage;

    #endregion

    #region Методы

    /// <summary>
    /// Сохранить.
    /// </summary>
    private async Task HandleSave()
    {
      errorMessage = null;

      try
      {
        await TableService.CreateTableAsync(newTable);
        NavigationManager.NavigateTo($"/");
      }
      catch (Exception ex)
      {
        errorMessage = ex.Message;
      }
      StateHasChanged();
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
