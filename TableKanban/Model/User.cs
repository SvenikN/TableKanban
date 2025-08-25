using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TableKanban.Model
{

  public class User
  {
    public int UserId { get; set; }

    [Required(ErrorMessage = "Заполните имя")]
    [Display(Name = "Имя")]
    [MaxLength(50)]
    public string UserName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Заполните email")]
    [Display(Name = "Email")]
    [MaxLength(100)]
    public string Email { get; set; } = string.Empty;

    public DateTime DateCreated { get; set; } = DateTime.UtcNow;

    public ICollection<Card> Cards { get; set; } = [];

    //public ICollection<TableUser> TableUsers { get; set; } = [];
  }
}
