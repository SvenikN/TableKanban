using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using TableKanban.Model;
using TableKanban.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TableKanban.Components.Pages
{
  public partial class TableEdit
  {
    [Parameter]
    public int TableId { get; set; }

    public Table editTable;

    public List<Stolb> stolbs { get; set; }

    protected override void OnInitialized()
    {
      editTable = TableService.GetTableById(TableId);
      if (editTable != null)
      {
        stolbs = StolbService.GetStolbsByTableId(TableId);
      }
      else
      { }
    }

    public async Task HandleSave()
    {
      await TableService.UpdateTableAsync(editTable);
      NavigationManager.NavigateTo($"/table/{TableId}");
    }

    /// <summary>
    /// Отмена.
    /// </summary>
    public void Cancel()
    {
      NavigationManager.NavigateTo($"/table/{TableId}");
    }
  }
}
