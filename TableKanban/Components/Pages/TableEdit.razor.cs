using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using TableKanban.Model;
using static TableKanban.Model.TableUserFormModel;

namespace TableKanban.Components.Pages
{
  /// <summary>
  /// Компонента для редактирования таблицы.
  /// </summary>
  public partial class TableEdit
  {
    #region Поля и свойства

    /// <summary>
    /// ИД таблицы, получает из маршрута.
    /// </summary>
    [Parameter]
    public int TableId { get; set; }

    /// <summary>
    /// Данные для создания таблицы.
    /// </summary>
    public TableFormModel tableForm;

    /// <summary>
    /// Таблица, которая будет отображаться.
    /// </summary>
    public Table editTable;

    #endregion

    #region Базовый класс

    ///<inheritdoc/>
    protected override async Task OnInitializedAsync()
    {
      editTable = TableService.GetTableById(TableId);

      if (editTable != null)
      {
        tableForm.TableName = editTable.TableName;
        tableForm.Description = editTable.Description;

        tableForm.FormUsers = await TableService.GetUsersForTableAsync(TableId);
      }
      else
      {
      }
    }

    #endregion

    #region Методы

    /// <summary>
    /// Сохранить.
    /// </summary>
    public async Task HandleSave()
    {
      if (editTable != null)
      {
        editTable.TableName = tableForm.TableName;
        editTable.Description = tableForm.Description;

        await TableService.UpdateTableAsync(editTable, tableForm.FormUsers);
        NavigationManager.NavigateTo($"/table/{TableId}");
      }
      else { }
    }

    /// <summary>
    /// Отмена.
    /// </summary>
    public void Cancel()
    {
      NavigationManager.NavigateTo($"/table/{TableId}");
    }

    #endregion
  }
}
