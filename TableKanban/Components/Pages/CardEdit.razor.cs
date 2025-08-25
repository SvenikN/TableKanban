using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.Threading.Tasks;
using TableKanban.Model;
using TableKanban.Services;

namespace TableKanban.Components.Pages
{
  public partial class CardEdit
  {
    [Parameter]
    public int CardId { get; set; }

    public Card? editCard;

    public List<User> users;

    public List<Stolb> stolbs;

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

    /// <summary>
    /// Редактирование карточки.
    /// </summary>
    /// <returns>Новая карточка.</returns>
    public async Task HandleSave()
    {
      await CardService.UpdateCardAsync(editCard);
      NavigationManager.NavigateTo($"/table/{editCard.TableId}");
    }

    /// <summary>
    /// Отмена.
    /// </summary>
    public void Cancel()
    {
      NavigationManager.NavigateTo($"/table/{editCard.TableId}");
    }
  }
}
