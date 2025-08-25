using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using TableKanban.Model;

namespace TableKanban.Components.Pages
{
  public partial class CardForm
  {
    [Parameter]
    public Card Card { get; set; } = new Card();

    [Parameter]
    public List<User> Users { get; set; } = new();

    [Parameter]
    public List<Stolb> Stolbs { get; set; } = new();

    [Parameter]
    public EventCallback OnSave { get; set; }

    [Parameter]
    public EventCallback OnCancel { get; set; }

    [Parameter]
    public string FormName { get; set; } = "CardForm";

    [Parameter]
    public string ButtonText { get; set; } = "Save";

    [Parameter]
    public bool EditMode { get; set; }


    private async Task HandleValidSubmit()
    {
      await OnSave.InvokeAsync();
    }

    private async Task Cancel()
    {
      await OnCancel.InvokeAsync();
    }
  }
}

