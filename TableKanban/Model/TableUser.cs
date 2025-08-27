namespace TableKanban.Model
{
  /// <summary>
  /// Связь таблиц и пользователей.
  /// </summary>
  public class TableUser
  {
    /// <summary>
    /// ИД.
    /// </summary>
    public int TableUserId { get; set; }

    /// <summary>
    /// ИД таблицы.
    /// </summary>
    public int TableId { get; set; }

    /// <summary>
    /// Навигационное свойство таблицы.
    /// </summary>
    public Table Table { get; set; } = null!;

    /// <summary>
    /// ИД пользователя.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Навигационное свойство пользователя.
    /// </summary>
    public User User { get; set; } = null!;

    /// <summary>
    /// Роль пользователя в таблице.
    /// </summary>
    public string Role { get; set; }
  }
}
