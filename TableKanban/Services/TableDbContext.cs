using Microsoft.EntityFrameworkCore;
using TableKanban.Model;

namespace TableKanban.Services
{
  /// <summary>
  /// DBContext для рецептов.
  /// </summary>
  /// <param name="options">Настройка контекста.</param>
  public class TableDbContext(DbContextOptions<TableDbContext> options)
    : DbContext(options)
  {
    #region Поля и свойства

    /// <summary>
    /// Таблицы.
    /// </summary>
    public DbSet<Table> Tables { get; set; }

    /// <summary>
    /// Столбцы.
    /// </summary>
    public DbSet<Stolb> Stolbs { get; set; }

    /// <summary>
    /// Тикет.
    /// </summary>
    public DbSet<Card> Cards { get; set; }

    /// <summary>
    /// Пользователи.
    /// </summary>
    public DbSet<User> Users { get; set; }

    /// <summary>
    /// Связь таблицы с пользователями
    /// </summary>
    //public DbSet<TableUser> TableUsers { get; set; }

    #endregion

    #region Базовый класс

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Stolb>()
        .HasOne(s => s.Table)
        .WithMany(t => t.Stolbs)
        .HasForeignKey(s => s.TableId)
        .OnDelete(DeleteBehavior.Cascade);

      modelBuilder.Entity<Card>()
        .HasOne(c => c.Table)
        .WithMany(t => t.Cards)
        .HasForeignKey(c => c.TableId)
        .OnDelete(DeleteBehavior.Restrict);

      modelBuilder.Entity<Card>()
        .HasOne(c => c.Stolb)
        .WithMany(s => s.Cards)
        .HasForeignKey(c => c.StolbId)
        .OnDelete(DeleteBehavior.Restrict);

      modelBuilder.Entity<Card>()
        .HasOne(c => c.User)
        .WithMany(u => u.Cards)
        .HasForeignKey(c => c.UserId)
        .OnDelete(DeleteBehavior.SetNull);

      //modelBuilder.Entity<TableUser>()
      //  .HasOne(tu => tu.User)
      //  .WithMany(u => u.TableUsers)
      //  .HasForeignKey(tu => tu.UserId)
      //  .OnDelete(DeleteBehavior.NoAction);

      //modelBuilder.Entity<TableUser>()
      //  .HasOne(tu => tu.Table)
      //  .WithMany(t => t.TableUsers)
      //  .HasForeignKey(tu => tu.TableId)
      //  .OnDelete(DeleteBehavior.Restrict);
    }

    #endregion
  }
}
