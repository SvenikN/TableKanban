using Microsoft.AspNetCore.Components;
using System;
using System.Linq;
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
    public TableFormModel tableForm = new TableFormModel();

    /// <summary>
    /// Таблица, которая будет отображаться.
    /// </summary>
    public Table? editTable;

    /// <summary>
    /// Сообщение об ошибке.
    /// </summary>
    private string? errorMessage;

    #endregion

    #region Базовый класс

    ///<inheritdoc/>
    protected override async Task OnInitializedAsync()
    {
      try
      {
        editTable = await TableService.GetTableByIdAsync(TableId);

        if (editTable != null)
        {
          tableForm.TableName = editTable.TableName;
          tableForm.Description = editTable.Description!;
          tableForm.Stolbs = editTable.Stolbs.ToList();

          tableForm.FormUsers = await TableService.GetUsersForTableAsync(TableId);
        }
        else
        {
          errorMessage = "Таблицы не существует.";
        }
      }
      catch (Exception ex)
      {
        errorMessage = ex.Message; 
      }
    }

    #endregion

    #region Методы

    /// <summary>
    /// Сохранить.
    /// </summary>
    public async Task HandleSave()
    {
      try 
      {
        editTable.TableName = tableForm.TableName;
        editTable.Description = tableForm.Description;
        editTable.Stolbs = tableForm.Stolbs;

        await TableService.UpdateTableAsync(editTable, tableForm.FormUsers);
        NavigationManager.NavigateTo($"/table/{TableId}");
      }
      catch (InvalidOperationException ex)
      {
        errorMessage = ex.Message;
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
    public void Cancel()
    {
      NavigationManager.NavigateTo($"/table/{TableId}");
    }

    #endregion
  }
}
