using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TableKanban.Model;

namespace TableKanban.Components.Pages
{
  /// <summary>
  /// Компонента создания карточки.
  /// </summary>
  public partial class CardCreate
  {
    #region Поля и свойства

    /// <summary>
    /// ИД таблицы.
    /// </summary>
    [Parameter]
    public int TableId { get; set; }

    /// <summary>
    /// Экземпляр карточки.
    /// </summary>
    public Card newCard = new();

    /// <summary>
    /// Список пользователей.
    /// </summary>
    public List<User> users = new();

    /// <summary>
    /// Список столбцов.
    /// </summary>
    public List<Stolb> stolbs = new();

    /// <summary>
    /// Сообщение об ошибке.
    /// </summary>
    private string? errorMessage;

    #endregion

    #region Базовый класс

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    protected override async Task OnInitializedAsync()
    {
      newCard.TableId = TableId;
      users = await UserService.GetUsersAsync();
      stolbs = StolbService.GetStolbsByTableId(TableId);
    }

    #endregion

    #region Методы

    /// <summary>
    /// Сохранить.
    /// </summary>
    private async Task HandleSave()
    {
      errorMessage = null!;

      try
      {
        await CardService.CreateCardAsync(newCard);
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
    private void HandleCancel()
    {
      NavigationManager.NavigateTo($"/table/{TableId}");
    }

    #endregion
  }
}
