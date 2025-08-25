using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using TableKanban.Model;

namespace TableKanban.Components.Pages
{
  public partial class CardCreate
  {
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

    protected override async Task OnInitializedAsync()
    {
      newCard.TableId = TableId;
      users = await UserService.GetUsersAsync();
      stolbs = StolbService.GetStolbsByTableId(TableId);
    }

    private async Task HandleSave()
    {
      CardService.CreateCard(newCard);
      NavigationManager.NavigateTo($"/table/{TableId}");
    }

    private void HandleCancel()
    {
      NavigationManager.NavigateTo($"/table/{TableId}");
    }
  }
}
