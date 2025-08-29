using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using TableKanban.Model;

namespace TableKanban.Components.Pages
{
  /// <summary>
  /// Компонента для отображения таблицы.
  /// </summary>
  public partial class TableView
  {
    #region Поля и свойства

    /// <summary>
    /// Ид таблицы.
    /// </summary>
    [Parameter]
    public int TableId { get; set; }

    /// <summary>
    /// Таблица, которая будет отображаться.
    /// </summary>
    public Table? table { get; set; }

    #endregion

    #region Базовый класс

    /// <inheritdoc/>
    protected override async Task OnInitializedAsync()
    {
      table = await TableService.GetTableByIdAsync(TableId);
    }

    #endregion

    #region Методы

    /// <summary>
    /// Переход на страницу создания карточки.
    /// </summary>
    /// <param name="id">Ид таблицы.</param>
    private void AddNewCard(int tableId)
    {
      this.NavigationManager.NavigateTo($"/card/create/{tableId}");
    }

    #endregion
  }
}
