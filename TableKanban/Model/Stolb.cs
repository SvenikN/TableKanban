using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TableKanban.Model
{
  /// <summary>
  /// Столбцы.
  /// </summary>
  public class Stolb
  {
    /// <summary>
    /// ИД столбца.
    /// </summary>
    public int StolbId { get; set; }

    /// <summary>
    /// ИД таблицы, в которой находится столбец.
    /// </summary>
    public int TableId { get; set; }

    /// <summary>
    /// Название столбца.
    /// </summary>
    [Display(Name = "Название столбца")]
    [MaxLength(20)]
    [Required(ErrorMessage = "Название обязательно")]
    public string StolbName { get; set; } = string.Empty;

    /// <summary>
    /// Порядок столбцов.
    /// </summary>
    public int Order { get; set; }

    /// <summary>
    /// Описание столбца.
    /// </summary>
    [Display(Name = "Описание")]
    [MaxLength(500)]
    public string? Description { get; set; }

    /// <summary>
    /// Навигационное свойство связанной таблицы.
    /// </summary>
    public Table Table { get; set; } = null!;

    /// <summary>
    /// Карточки столбца.
    /// </summary>
    public ICollection<Card> Cards { get; set; } = [];
  }
}
