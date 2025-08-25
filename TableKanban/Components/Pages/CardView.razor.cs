using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Threading.Tasks;
using TableKanban.Model;

namespace TableKanban.Components.Pages
{
  public partial class CardView
  {
    [Parameter]
    public int CardId { get; set; }

    public Card? card;

    protected override async Task OnInitializedAsync()
    {
      card = await CardService.GetCardAsync(CardId);
    }

    private void Cancel()
    {
      NavigationManager.NavigateTo($"/table/{card.TableId}");
    }
  }
}
