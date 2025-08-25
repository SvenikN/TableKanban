using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using TableKanban.Model;

namespace TableKanban.Components.Pages
{
  public partial class TableView
  {

    [Parameter]
    public int TableId { get; set; }

    public Table table { get; set; }

    public List<Stolb> columns = new List<Stolb>();

    public List<Card> cards = new List<Card>();

    protected override async Task OnInitializedAsync()
    {
      table = TableService.GetTableById(TableId);
      columns = StolbService.GetStolbsByTableId(TableId);
      cards = CardService.GetCardsByTableId(TableId);
    }

    private void AddNewCard(int id)
    {
      this.NavigationManager.NavigateTo($"/card/create/{id}");
    }
  }
}
