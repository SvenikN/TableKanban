using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableKanban.Model
{
  public class Card
  {
    /// <summary>
    /// ИД таски.
    /// </summary>
    public int CardId { get; set; }

    /// <summary>
    /// ИД столбца, в котором находится стик.
    /// </summary>
    [ForeignKey("Stolb")]
    public int StolbId { get; set; }

    /// <summary>
    /// ИД пользователя, на которого назначили таску.
    /// </summary>
    public int? UserId { get; set; }

    [ForeignKey("Table")]
    public int TableId { get; set; }

    /// <summary>
    /// Название таски.
    /// </summary>
    [Required(ErrorMessage = "Название обязательно")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Описание таски.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Статус задачи.
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// Дата создания таски.
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

    public Table Table { get; set; } = null!;
  }
}
