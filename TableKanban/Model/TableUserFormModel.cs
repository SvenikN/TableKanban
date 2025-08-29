using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TableKanban.Model
{
  /// <summary>
  /// Модель таблицы.
  /// </summary>
  public class TableUserFormModel
  {
    /// <summary>
    /// Модель таблицы.
    /// </summary>
    public class TableFormModel
    {
      /// <summary>
      /// Название таблицы.
      /// </summary>
      [Required(ErrorMessage = "Название таблицы обязательно")]
      [MaxLength(50)]
      [Display(Name = "Название таблицы")]
      public string TableName { get; set; } = string.Empty;

      /// <summary>
      /// Описание таблицы.
      /// </summary>
      [Display(Name = "Описание таблицы")]
      public string Description { get; set; } = string.Empty;

      /// <summary>
      /// Список пользователей.
      /// </summary>
      public List<UserModel> FormUsers { get; set; } = new List<UserModel>();

      /// <summary>
      /// Список столбцов.
      /// </summary>
      public ICollection<Stolb> Stolbs { get; set; } = [];
    }

    /// <summary>
    /// Модель пользователей.
    /// </summary>
    public class UserModel
    {
      /// <summary>
      /// Email пользователя.
      /// </summary>
      [Required(ErrorMessage = "Email пользователя обязателен")]
      [EmailAddress(ErrorMessage = "Некорректный формат Email")]
      [Display(Name = "Email")]
      [MaxLength(100)]
      public string Email { get; set; } = string.Empty;

      /// <summary>
      /// Имя пользователя.
      /// </summary>
      [Required(ErrorMessage = "Имя пользователя обязательно")]
      [Display(Name = "Имя")]
      [MaxLength(50)]
      public string UserName { get; set; } = string.Empty;
    }
  }
}
