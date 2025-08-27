using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TableKanban.Model
{
  /// <summary>
  /// Таблицы.
  /// </summary>
  public class Table
  {
    /// <summary>
    /// ИД таблицы.
    /// </summary>
    public int TableId { get; set; }

    /// <summary>
    /// Название таблицы.
    /// </summary>
    [Display(Name = "Название таблицы")]
    [MaxLength(50)]
    [Required(ErrorMessage = "Название обязательно")]
    public string TableName { get; set; } = string.Empty;

    /// <summary>
    /// Описание таблицы.
    /// </summary>
    [Display(Name = "Описание таблицы")]
    [MaxLength(500)]
    public string? Description { get; set; }

    /// <summary>
    /// Дата создания таблицы.
    /// </summary>
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Столбцы таблицы.
    /// </summary>
    public ICollection<Stolb> Stolbs { get; set; } = [];

    /// <summary>
    /// Карточки таблицы.
    /// </summary>
    public ICollection<Card> Cards { get; set; } = [];

    /// <summary>
    /// Связь с пользователями, через промежуточную таблицу.
    /// </summary>
    public ICollection<TableUser> TableUsers { get; set; } = [];
  }
}
