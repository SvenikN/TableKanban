using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using TableKanban.Model;
using TableKanban.Services;

namespace TableKanban.Components.Pages
{
  public partial class TableCreate
  {
    [Parameter]
    public int TableId { get; set; }

    public Table newTable = new Table();

    private async Task HandleSave()
    {
      await TableService.CreateTableAsync(newTable);
      NavigationManager.NavigateTo($"/");
    }

    private void HandleCansel()
    {
      NavigationManager.NavigateTo($"/");
    }
  }
}
