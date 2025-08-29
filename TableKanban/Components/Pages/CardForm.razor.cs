using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using TableKanban.Model;

namespace TableKanban.Components.Pages
{
  /// <summary>
  /// Компонента формы для карточки.
  /// </summary>
  public partial class CardForm
  {
    #region Поля и свойства

    /// <summary>
    /// Карточка.
    /// </summary>
    [Parameter]
    public Card Card { get; set; } = new Card();

    /// <summary>
    /// Список пользователей.
    /// </summary>
    [Parameter]
    public List<User> Users { get; set; } = new();

    /// <summary>
    /// Список столбцов.
    /// </summary>
    [Parameter]
    public List<Stolb> Stolbs { get; set; } = new();

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

