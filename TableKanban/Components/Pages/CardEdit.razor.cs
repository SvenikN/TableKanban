using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TableKanban.Model;

namespace TableKanban.Components.Pages
{
  /// <summary>
  /// Компонента для радектирования карточки.
  /// </summary>
  public partial class CardEdit
  {
    #region Поля и свойства

    /// <summary>
    /// ИД карточки.
    /// </summary>
    [Parameter]
    public int CardId { get; set; }

    /// <summary>
    /// Карточка.
    /// </summary>
    public Card? editCard;

    /// <summary>
    /// Список пользователей.
    /// </summary>
    public List<User>? users;

    /// <summary>
    /// Список столбцов.
    /// </summary>
    public List<Stolb>? stolbs;

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
      editCard = await CardService.GetCardAsync(CardId);
      if (editCard != null)
      {
        users = await UserService.GetUsersAsync();
        stolbs = StolbService.GetStolbsByTableId(editCard.TableId);
      }
      else
      { }
    }

    #endregion]

    #region Методы

    /// <summary>
    /// Сохранить.
    /// </summary>
    public async Task HandleSave()
    {
      errorMessage = null!;

      try
      { 
        await CardService.UpdateCardAsync(editCard);
        NavigationManager.NavigateTo($"/table/{editCard.TableId}");
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
      NavigationManager.NavigateTo($"/table/{editCard.TableId}");
    }

    #endregion
  }
}
