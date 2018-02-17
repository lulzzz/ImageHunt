using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageHunt.Model
{
  public class Team : DbObject
  {
    public Team()
    {
      Players = new List<Player>();
    }
    public string Name { get; set; }
    public List<Player> Players { get; set; }
  }
}
