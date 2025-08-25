using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TableKanban.Model;

namespace TableKanban.Components.Pages
{
  public partial class TableForm
  {

    [Parameter]
    public Table Table { get; set; }

    public User User { get; set; }

    public List<Stolb> FormStolbs { get; set; } = [];

    public List<User> FormUsers { get; set; } = [];

    [Parameter]
    public string FormName { get; set; } = "TableForm";

    [Parameter]
    public EventCallback OnSave { get; set; }

    [Parameter]
    public EventCallback OnCancel { get; set; }

    [Parameter]
    public bool EditMode { get; set; }

    protected override void OnParametersSet()
    {
      if (Table.Stolbs != null)
      {
        FormStolbs = Table.Stolbs.ToList();
      }
      else
      {
        FormStolbs = new List<Stolb>();
        AddInputStolbs();
      }
    }

    public void AddInputStolbs()
    {
      this.FormStolbs.Add(new Stolb());
    }

    private void RemoveStolb(Stolb formStolb)
    {
      this.FormStolbs.Remove(formStolb);
    }


    private async Task HandleValidSubmit()
    {
      Table.Stolbs = FormStolbs;
      await OnSave.InvokeAsync();
    }

    private async Task Cancel()
    {
      await OnCancel.InvokeAsync();
    }
  }
}
