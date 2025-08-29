using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableKanban.Model
{
  /// <summary>
  /// Карточки.
  /// </summary>
  public class Card
  {
    /// <summary>
    /// ИД карточки.
    /// </summary>
    public int CardId { get; set; }

    /// <summary>
    /// ИД столбца, в котором находится карточка.
    /// </summary>
    [ForeignKey("Stolb")]
    [Required(ErrorMessage = "Статус обязателен")]
    public int StolbId { get; set; }

    /// <summary>
    /// ИД пользователя, на которого назначили карточку.
    /// </summary>
    public int? UserId { get; set; }

    /// <summary>
    /// ИД таблицы, в которой находится карточка.
    /// </summary>
    [ForeignKey("Table")]
    public int TableId { get; set; }

    /// <summary>
    /// Название карточки.
    /// </summary>
    [Required(ErrorMessage = "Название обязательно")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Описание карточки.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Статус карточки.
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// Дата создания карточки.
    /// </summary>
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Навигационное свойство связанного столбца.
    /// </summary>
    public Stolb Stolb { get; set; } = null!;

    /// <summary>
    /// Навигационное свойство связанного пользователя.
    /// </summary>
    public User? User { get; set; } = null!;

    /// <summary>
    /// Навигационное свойство связанной таблицы.
    /// </summary>
    public Table Table { get; set; } = null!;
  }
}
