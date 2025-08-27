using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using TableKanban.Model;

namespace TableKanban.Components.Pages
{
  /// <summary>
  /// Компонента для просмотра карточки.
  /// </summary>
  public partial class CardView
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
    public Card? card;

    #endregion

    #region Базовй класс

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    protected override async Task OnInitializedAsync()
    {
      card = await CardService.GetCardAsync(CardId);
    }

    #endregion

    #region Методы

    /// <summary>
    /// Вернуться назад.
    /// </summary>
    private void Cancel()
    {
      NavigationManager.NavigateTo($"/table/{card.TableId}");
    }

    /// <summary>
    /// Удалить карточку.
    /// </summary>
    private void DeleteCard()
    {
      CardService.DeleteCardAsync(CardId);
      NavigationManager.NavigateTo($"/table/{card.TableId}");
    }

    #endregion
  }
}
