using Microsoft.AspNetCore.Components;
using System;
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
    public List<Table>? tables;

    /// <summary>
    /// Словарь для хранения сообщений об ошибках.
    /// </summary>
    private Dictionary<int, string> tableErrorMessages = new Dictionary<int, string>();

    /// <summary>
    /// Сообщение об ошибке.
    /// </summary>
    private string? errorMessages;

    #endregion

    #region Базовый класс

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    protected override async Task OnInitializedAsync()
    {
      try
      {
        tables = await TableService.GetListTableAsync();
      }
      catch (Exception ex)
      {
        errorMessages = $"Произошла ошибка при загрузке таблицы: {ex.Message}";
      }
    }

    #endregion

    #region Методы

    /// <summary>
    /// Добавить таблицу.
    /// </summary>
    private void AddNewTable()
    {
      NavigationManager.NavigateTo("/table/create");
    }

    /// <summary>
    /// Открыть таблицу.
    /// </summary>
    /// <param name="id">ИД таблицы.</param>
    public void OpenTableById(int tableId)
    {
      NavigationManager.NavigateTo($"/table/{tableId}");
    }

    /// <summary>
    /// Открыть таблицу на редактирование.
    /// </summary>
    /// <param name="tableId">ИД таблицы.</param>
    public void OpenEditTableById(int tableId)
    {
      NavigationManager.NavigateTo($"/table/edit/{tableId}");
    }

    /// <summary>
    /// Удалить таблицу.
    /// </summary>
    /// <param name="tableId">ИД таблицы.</param>
    public async Task DeleteTableById(int tableId)
    {
      tableErrorMessages.Remove(tableId);

      try
      {
        await TableService.DeleteTableAsync(tableId);
        tables = await TableService.GetListTableAsync();
      }
      catch (InvalidOperationException ex)
      {
        tableErrorMessages[tableId] = ex.Message;
      }
      catch (Exception ex)
      {
        tableErrorMessages[tableId] = ex.Message;
      }
      StateHasChanged();
    }

    #endregion
  }
}
