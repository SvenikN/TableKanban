using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using TableKanban.Model;
using static TableKanban.Model.TableUserFormModel;

namespace TableKanban.Components.Pages
{
  /// <summary>
  /// Компонент формы таблицы.
  /// </summary>
  public partial class TableForm
  {
    #region Поля и свойства

    /// <summary>
    /// Данные для создания таблицы.
    /// </summary>
    [Parameter]
    public TableFormModel FormTable { get; set; } = new TableFormModel();

    /// <summary>
    /// Обработчик события на сохранение.
    /// </summary>
    [Parameter]
    public EventCallback OnSave { get; set; }

    /// <summary>
    /// Обработчик события отмены.
    /// </summary>
    [Parameter]
    public EventCallback OnCancel { get; set; }

    /// <summary>
    /// Признак того, что таблица открыта на редактирование.
    /// </summary>
    [Parameter]
    public bool EditMode { get; set; }

    #endregion

    #region Методы

    /// <summary>
    /// Добавить поле для добавления столбца.
    /// </summary>
    public void AddInputStolbs()
    {
      FormTable.Stolbs.Add(new Stolb());
    }

    /// <summary>
    /// Удалить поле добавления столбца.
    /// </summary>
    /// <param name="formStolb">Поле для столбца.</param>
    private void RemoveStolb(Stolb formStolb)
    {
      FormTable.Stolbs.Remove(formStolb);
    }

    /// <summary>
    /// Добавить поле для добавления пользователя.
    /// </summary>
    public void AddInputUser()
    {
      FormTable.FormUsers.Add(new UserModel());
    }

    /// <summary>
    /// Удалить поле для добавления пользователя.
    /// </summary>
    /// <param name="formUser">Поле для пользователя.</param>
    private void RemoveUser(UserModel formUser)
    {
     FormTable.FormUsers.Remove(formUser);
    }

    /// <summary>
    /// Сохранить.
    /// </summary>
    private async Task HandleValidSubmit()
    {
      await OnSave.InvokeAsync();
    }

    /// <summary>
    /// Отмена.
    /// </summary>
    private async Task Cancel()
    {
      await OnCancel.InvokeAsync();
    }

    #endregion
  }
}
