using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TableKanban.Model
{
  /// <summary>
  /// Пользователи.
  /// </summary>
  public class User
  {
    /// <summary>
    /// ИД пользователя.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Имя пользователя.
    /// </summary>
    [Required(ErrorMessage = "Имя пользователя обязательно")]
    [Display(Name = "Имя")]
    [MaxLength(50)]
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// Email пользователя.
    /// </summary>
    [Required(ErrorMessage = "Email пользователя обязателен")]
    [EmailAddress(ErrorMessage = "Некорректный формат Email")]
    [Display(Name = "Email")]
    [MaxLength(100)]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Дата добавления пользователя.
    /// </summary>
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Карточки пользователя.
    /// </summary>
    public ICollection<Card> Cards { get; set; } = [];

    /// <summary>
    /// Связь с таблицой, через промежуточную таблицу.
    /// </summary>
    public ICollection<TableUser> TableUsers { get; set; } = [];
  }
}
