using System.Collections.Generic;
using TableKanban.Model;

namespace TableKanban.Interfaces
{
  public interface IStolbService
  {
    List<Stolb> GetStolbsByTableId(int tableId);
  }
}
