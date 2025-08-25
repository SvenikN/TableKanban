using System.Collections.Generic;
using System.Threading.Tasks;
using TableKanban.Model;

namespace TableKanban.Components.Pages
{
  public partial class Home
  {
    public List<Table> tables;

    protected override async Task OnInitializedAsync()
    {
      tables = TableService.GetListTable();
    }

    private void AddNewTable()
    {
      this.NavigationManager.NavigateTo("/table/create");
    }

    public void OpenTableById(int id)
    {
      NavigationManager.NavigateTo($"/table/{id}");
    }
  }
}
